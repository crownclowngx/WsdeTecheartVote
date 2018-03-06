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
        /// 初始化的第一步 初始化组
        /// </summary>
        /// <returns></returns>
        public bool InitGroup(List<UInt64> secrets)
        {
            if (!handshaked)
            {
                throw new Exception("请首先握手调用Handshake函数");
            }
            shareAction1 s1 = shareAction1.GetAllAllowShare();
            shareAction2 s2 = shareAction2.GetAllAllowShare();
            
            GroupingCommandRequest groupingCommandRequest = new GroupingCommandRequest(handshakeRespone, secrets, 1, s1, s2);
            var postdata = groupingCommandRequest.GetFinalArray();
            serialPort.Write(postdata, 0, 21);
            return true;
        }

    }
}
