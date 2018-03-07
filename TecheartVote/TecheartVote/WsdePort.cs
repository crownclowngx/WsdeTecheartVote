using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TecheartVote.Request;
using TecheartVote.Response;
using TecheartVote.Verification;

namespace TecheartVote
{
    public class WsdePort
    {
        /// <summary>
        /// 是否已握手
        /// </summary>
        public bool handshaked { get; set; }
        /// <summary>
        /// 串口类
        /// </summary>
        public SerialPort serialPort { get; set; }

        /// <summary>
        /// 内存list
        /// </summary>
        public List<Int32> memList { get; set; }

        /// <summary>
        /// 握手消息专用内存
        /// </summary>
        public List<Int32> handMemList { get; set; }
        /// <summary>
        /// 握手事件解析
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public Func<List<int>,HandshakeResponse> HandshakeAnalysis;

        /// <summary>
        /// 握手协议返回值
        /// </summary>
        public HandshakeResponse handshakeRespone { get; set; }

        /// <summary>
        /// 握手委托
        /// </summary>
        public delegate void HandshakeHandler(WsdePort handshake);
        /// <summary>
        /// 握手事件
        /// </summary>
        public event HandshakeHandler HandshakeEvent;

        public shareAction1 shareAction1P { get; set; }
        public shareAction2 shareAction2P { get; set; }

        public WsdePort(String port)
        {
            handMemList = new List<int>();
            memList = new List<int>();
            serialPort = new SerialPort();
            serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(OnDataReceived);
            serialPort.BaudRate = 115200;
            serialPort.PortName = port;
            serialPort.DataBits = 8;
            serialPort.Open();
            HandshakeAnalysis = k => { return HandshakeTools.AnalysisHandshake(k.ToArray()); };
            shareAction1P = shareAction1.GetAllAllowShare();
            shareAction2P = shareAction2.GetAllAllowShare();
        }
        private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int trynum = 0;
            int count = serialPort.BytesToRead;
            if (!handshaked)
            {
                countcheck:
                if (count > 82 || trynum>1)
                {
                    throw new Exception("握手失败");
                }
                if (count < 82)
                {
                    Thread.Sleep(20);
                    count = serialPort.BytesToRead;
                }
                if (count < 82)
                {
                    trynum++;
                    goto countcheck;
                }
                for (int i = 0; i < count; i++)
                {
                    handMemList.Add(serialPort.ReadByte());
                }
                handshaked = true;
                handshakeRespone = HandshakeAnalysis(handMemList);
                HandshakeEvent(this);
            }
            else //包含设置通道等
            {
                for (int i = 0; i < count; i++)
                {
                    memList.Add(serialPort.ReadByte());
                }
                Console.Read();
            }
            
        }

        /// <summary>
        /// 握手协议可能会抛出异常
        /// </summary>
        /// <returns></returns>
        public bool Handshake()
        {

            Byte[] TxData = { };
            List<Byte> l = new List<byte>();
            l.Add(0xAA);
            l.Add(0x10);
            Random random = new Random();
            
            for (int i = 0; i < 79; i++)
            {
                l.Add(Convert.ToByte(random.Next(0, 255)));
            }
            var last=VerificationTools.HashCalc(l);
            l.Add(Convert.ToByte(last));
            TxData = l.ToArray();
            serialPort.Write(TxData, 0, 82);
            return true;
        }

        /// <summary>
        /// 主机获取剋通讯的所有投票器组
        /// </summary>
        /// <returns></returns>
        public bool InitGroup(List<UInt64> secrets)
        {
            
            if (!handshaked)
            {
                throw new Exception("请首先握手调用Handshake函数");
            }
            Dictionary<String, List<UInt64>> secretsList = new Dictionary<String, List<ulong>>() { { "1",new List<ulong> ()} };
            int numGroup = 1;
            for(int i=0;i< secrets.Count; i++)
            {
                if (i % 4 == 0 && i>0)
                {
                    numGroup += 1;
                }
                secretsList[numGroup.ToString()].Add(secrets[i]);
            }

            foreach (var v in secretsList)
            {
                GroupingCommandRequest groupingCommandRequest = new GroupingCommandRequest(handshakeRespone, v.Value, Convert.ToInt32(v.Key), shareAction1P, shareAction2P);
                var postdata = groupingCommandRequest.GetFinalArray();
                serialPort.Write(postdata, 0, 21);
            }
            return true;
        }
        
        /// <summary>
        /// 初始化主机配置包含 频率 信道等可以多次设置 ，但因为一般在一个生存周期内 只会设置一次 所以标识为Init
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        public bool InitConf(ConfAction conf)
        {
            ConfigureCommandRequest request = new ConfigureCommandRequest(handshakeRespone, shareAction1P, shareAction2P);
            if (conf.channel > 0)
            {
                request.SetChannel(conf.channel);
            }
            if (conf.frequency != FrequencyEnum.Null)
            {
                request.SetFrequency(conf.frequency);
            }
            if(conf.date!=null && conf.date > DateTime.MinValue)
            {
                request.SetDate(conf.date);
            }
            var postdata = request.GetFinalArray();
            serialPort.Write(postdata, 0, 21);
            return true;
        }

        /// <summary>
        /// 将当前的动态配置发送给主机 动态配置的属性是  shareAction1P 和 shareAction2P
        /// </summary>
        /// <returns></returns>
        public bool UpdateDynamicConf()
        {
            ConfigureCommandRequest request = new ConfigureCommandRequest(handshakeRespone, shareAction1P, shareAction2P);
            var postdata = request.GetFinalArray();
            serialPort.Write(postdata, 0, 21);
            return true;
        }
    }
}
