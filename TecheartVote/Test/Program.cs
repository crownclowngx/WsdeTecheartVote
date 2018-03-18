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
            wsdePort.OnDataCome += new OnDateComeHandler(OnDateComeHandler2);
            wsdePort.InitGroup(new List<ulong> { 1, 2, 3, 4 });
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


        private static void OnDateComeHandler2(WsdePort handshake, SubSelect subselect)
        {
            Console.WriteLine("{0}:{2}:{1}", subselect.address, subselect.selectData, subselect.subjectNumber);
        }

        #region usbtest
        static void UebTest()
        {
            USB ezUSB = new USB();
            ezUSB.AddUSBEventWatcher(USBEventHandler, USBEventHandler, new TimeSpan(0, 0, 3));
            Console.Read();
            ezUSB.RemoveUSBEventWatcher();
        }
        private static void USBEventHandler(object sender, EventArrivedEventArgs e)
        {
            if (e.NewEvent.ClassPath.ClassName == "__InstanceCreationEvent")
            {
                Console.WriteLine("USB插入时间：" + DateTime.Now + "\r\n");
            }
            else if (e.NewEvent.ClassPath.ClassName == "__InstanceDeletionEvent")
            {
                Console.WriteLine("USB拔出时间：" + DateTime.Now + "\r\n");
            }

            foreach (USBControllerDevice Device in USB.WhoUSBControllerDevice(e))
            {
                Console.WriteLine("\tAntecedent：" + Device.Antecedent + "\r\n");
                Console.WriteLine("\tDependent：" + Device.Dependent + "\r\n");
                String s = Device.Dependent;
                var kk=DevManager.GetPortNum(s);
            }
            
        }
        #endregion

        #region comtest
        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        static void TeatComWsde()
        {
            WsdePort wsdePort = new WsdePort("COM3");
            wsdePort.HandshakeEvent += new HandshakeHandler(OnHandshake);
            wsdePort.OnDataCome += new OnDateComeHandler(OnDateComeHandler);
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
            //response.PushAnswer(129, "A");
            Thread.Sleep(200);
            response.PushScore(1, "100");
            autoResetEvent.Set();
        }

        private static void OnDateComeHandler(WsdePort handshake, SubSelect subselect)
        {
            Console.WriteLine("{0}:{2}:{1}", subselect.address, subselect.selectData, subselect.subjectNumber);
        }
        #endregion
    }
}
