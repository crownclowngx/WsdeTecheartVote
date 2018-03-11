using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TecheartVote;
using TecheartVote.Response;
using static TecheartVote.WsdePort;

namespace Test
{
    class Program
    {
        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            WsdePort wsdePort = new WsdePort("COM3");
            wsdePort.HandshakeEvent += new HandshakeHandler(OnHandshake);
            wsdePort.OnDateCome += new OnDateComeHandler(OnDateComeHandler);
            wsdePort.Handshake();
            autoResetEvent.WaitOne();
            Console.WriteLine("初始化配置完成");
            wsdePort.PushScore(1, "10");
            Console.Read();
        }

        private static void OnHandshake(WsdePort response)
        {
            response.InitGroup(new List<ulong> { 1, 2, 3, 4 });
            response.handshakeRespone.SecretKey = 4;
            Thread.Sleep(200);
            response.InitConf(new ConfAction() { channel=1, date=DateTime.Now, frequency=FrequencyEnum.dBM0 });
            Thread.Sleep(200);
            //response.shareAction1P.clientCanSeeSolution = false;
            //response.shareAction2P.clientCanWriteABC = false;
            response.UpdateDynamicConf();
            Thread.Sleep(200);
            response.PushAnswer(129, "A");
            Thread.Sleep(200);
            response.PushScore(1, "100");
            autoResetEvent.Set();
        }

        private static void OnDateComeHandler(WsdePort handshake, SubSelect subselect)
        {
            Console.WriteLine("{0}:{2}:{1}", subselect.address, subselect.selectDate, subselect.number);
        }
    }
}
