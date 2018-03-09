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
            wsdePort.Handshake();
            autoResetEvent.WaitOne();
            Console.WriteLine("初始化配置完成");
            wsdePort.PushScore(1, "10");
            Console.Read();
        }

        private static void OnHandshake(WsdePort response)
        {
            response.InitGroup(new List<ulong> { 1, 2, 3, 4 });
            response.InitConf(new ConfAction() { channel=1, date=DateTime.Now, frequency=FrequencyEnum.dBM0 });
            response.shareAction1P.clientCanAnswer = true;
            response.UpdateDynamicConf();
            response.PushAnswer(1, "A");
            autoResetEvent.Set();
        }
    }
}
