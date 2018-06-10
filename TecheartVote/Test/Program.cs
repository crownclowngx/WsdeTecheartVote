using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TecheartVote;
using TecheartVote.Response;
using TecheartVote.UsbManager;
using static TecheartVote.WsdePort;

namespace Test
{
    class Program
    {

        static void Main(string[] args)
        {
            WsdeUsbManager manager = new WsdeUsbManager();
            manager.OnWsdeUsbComed += new WsdeUsbManager.OnWsdeUsbHandler(OnWsdeUsbComed);
            manager.OnWsdeUsbExited += new WsdeUsbManager.OnWsdeUsbHandler(OnWsdeUsbExited);
            Console.Read();
        }

        public static void OnWsdeUsbComed(WsdePort wsdePort)
        {
            wsdePort.OnDataCome += new OnDataComeHandler(OnDataComeHandler2);
            wsdePort.SetAccessPasswords(new List<ulong> { 1, 2, 3, 4 });
            Thread.Sleep(1000);
            wsdePort.InitConf(new ConfAction() { channel = 1, date = DateTime.Now, frequency = FrequencyEnum.dBM0 });
            Thread.Sleep(1000);
            wsdePort.UpdateDynamicConf();
            Thread.Sleep(1000);
            wsdePort.subAnswerDic.SetAnswer(2, "A");
            wsdePort.subAnswerDic.SetAnswer(3, "B");
            wsdePort.PushAnswer();
        }

        public static void OnWsdeUsbExited(WsdePort wsdePort)
        {
            Console.WriteLine("exit:{0}",wsdePort.handshakeRespone.Address);
        }


        private static void OnDataComeHandler2(WsdePort handshake, SubSelect subselect)
        {
            Console.WriteLine("{0}:{2}:{1}", subselect.address, subselect.selectData, subselect.subjectNumber);
        }
    }
}
