using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static TecheartVote.WsdePort;

namespace TecheartVote.UsbManager
{
    public class WsdeUsbManager
    {
        bool handtrue = false;
        USB ezUSB = new USB();
        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        public delegate void OnWsdeUsbHandler(WsdePort wsdePort);
        public event OnWsdeUsbHandler OnWsdeUsbComed;

        public event OnWsdeUsbHandler OnWsdeUsbExited;
        private static Dictionary<String, WsdePort> wsdePortUsbDic = new Dictionary<string, WsdePort>();
        public WsdeUsbManager()
        {
            ezUSB.AddUSBEventWatcher(USBEventHandler, USBEventHandler, new TimeSpan(0, 0, 3));
        }
        public void DeleteWatcher()
        {
            ezUSB.RemoveUSBEventWatcher();
        }
        private  void USBEventHandler(object sender, EventArrivedEventArgs e)
        {
            if (e.NewEvent.ClassPath.ClassName == "__InstanceCreationEvent")
            {
                foreach (USBControllerDevice Device in USB.WhoUSBControllerDevice(e))
                {
                    String s = Device.Dependent;
                    var kk = DevManager.GetPortNum(s);
                    if (kk == -1)
                    {
                        continue;
                    }
                    WsdePort wsdePort = new WsdePort("COM"+kk.ToString());
                    wsdePort.HandshakeEvent += new HandshakeHandler(OnHandshake);
                    wsdePort.Handshake();
                    autoResetEvent.WaitOne();
                    if (handtrue)
                    {
                        wsdePortUsbDic.Add(s, wsdePort);
                        OnWsdeUsbComed(wsdePort);
                    }
                }

            }
            else if (e.NewEvent.ClassPath.ClassName == "__InstanceDeletionEvent")
            {
                foreach (USBControllerDevice Device in USB.WhoUSBControllerDevice(e))
                {
                    try {
                        String s = Device.Dependent;
                        OnWsdeUsbExited(wsdePortUsbDic[s]);
                    }
                    catch (Exception ex)
                    {

                    }
                    
                }
            }
        }

        private void OnHandshake(WsdePort response)
        {
            handtrue = true;
            autoResetEvent.Set();
        }
    }
}
