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
        static void Main(string[] args)
        {
            WsdePort wsdePort = new WsdePort("COM3");
            wsdePort.HandshakeEvent += new HandshakeHandler(OnHandshake);
            wsdePort.Handshake();
            Console.Read();
        }

        private static void OnHandshake(WsdePort response)
        {
            response.InitGroup(new List<ulong> { 1, 2, 3, 4 }, new shareAction1() { persistenceConfiguration = true }, new shareAction2() { clientCanWriteABC = true , clientCanChangeChannel=true, clientCanChangeDate=true, clientCanCnahgeTime=true, clientCanErase=true, clientCanWriteNumber=true, clinetCanSeeAnswer=true });
        }
    }
}
