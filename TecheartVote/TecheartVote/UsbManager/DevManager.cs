using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TecheartVote.UsbManager
{
    public class DevManager
    {
        /* 
            自动查找VID/PID匹配的USB设备当作Virutal COM Port口  
            输入值：   字符串，如VID_0483&PID_5740 
            返回值：   0 - 没有找到对应设备 
        */
        public static int GetPortNum(String vid_pid)
        {
            ManagementObjectCollection USBControllerDeviceCollection
                = new ManagementObjectSearcher("SELECT * FROM Win32_USBControllerDevice").Get();
            if (USBControllerDeviceCollection != null)
            {
                foreach (ManagementObject USBControllerDevice in USBControllerDeviceCollection)
                {
                    String Dependent = (USBControllerDevice["Dependent"] as String).Split(new Char[] { '=' })[1];

                    if (Dependent.Contains(vid_pid))
                    {
                        ManagementObjectCollection PnPEntityCollection
                            = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE DeviceID=" + Dependent).Get();
                        if (PnPEntityCollection != null)
                        {
                            foreach (ManagementObject Entity in PnPEntityCollection)
                            {
                                String DevName = Entity["Name"] as String;// 设备名称  
                                String PortNum = Regex.Replace(DevName, @"[^\d.\d]", "");
                                if (String.IsNullOrEmpty(PortNum))
                                {
                                    return -1;
                                }
                                return Convert.ToInt32(PortNum);
                            }
                        }
                    }
                }
            }

            return -1;
        }

        /* 
            删除指定的usb虚拟串口  
            输入值：   设备管理器下的设备名称，不包括后面的(COMXX) 
            返回值：   成功/失败 
        */
        /*!!!BUG: 删除port后重新扫描，port number会加一；插拔或者通过设备管理器Uninstall port没有这个问题。!!!*/
        public static bool DeletePort(String portName = null)
        {
            bool ret = false;

            Guid classGuid = Guid.Empty;
            IntPtr hDevInfo = Win32.SetupDiGetClassDevs(ref classGuid, null, IntPtr.Zero, Win32.DIGCF_ALLCLASSES | Win32.DIGCF_PRESENT);
            if (hDevInfo.ToInt32() == Win32.INVALID_HANDLE_VALUE)
            {
                Console.WriteLine("访问硬件设备失败");
            }
            else
            {
                int i = 0;
                int selected = 0;

                StringBuilder deviceName = new StringBuilder();
                deviceName.Capacity = Win32.MAX_DEV_LEN;
                do
                {
                    SP_DEVINFO_DATA devInfoData = new SP_DEVINFO_DATA();
                    StringBuilder _DeviceName = new StringBuilder("");
                    _DeviceName.Capacity = 1000;

                    devInfoData.cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
                    devInfoData.classGuid = Guid.Empty;
                    devInfoData.devInst = 0;
                    devInfoData.reserved = IntPtr.Zero;
                    bool result = Win32.SetupDiEnumDeviceInfo(hDevInfo, i, devInfoData);
                    if (false == result)
                    {
                        break;
                    }

                    Win32.SetupDiGetDeviceRegistryProperty(hDevInfo, devInfoData, 0, 0, _DeviceName, (uint)_DeviceName.Capacity, IntPtr.Zero);

                    if (_DeviceName.ToString().Equals(portName))
                    {

                        selected = i;
                    }
                    ++i;
                } while (true);

                ret = Win32.Remove(selected, hDevInfo);

            }
            Win32.SetupDiDestroyDeviceInfoList(hDevInfo);

            return ret;
        }

        /* 
            重新扫描硬件  
            输入值：   无 
            返回值：   成功/失败 
        */
        public static bool Rescan()
        {
            UInt32 devRoot = 0;

            if (CM_Locate_DevNode_Ex(ref devRoot, null, 0, IntPtr.Zero) != CR_SUCCESS)
            {
                return false;
            }
            if (CM_Reenumerate_DevNode_Ex(devRoot, 0, IntPtr.Zero) != CR_SUCCESS)
            {
                return false;
            }

            return true;
        }
        public static bool DisablePort(String portName = null)
        {
            bool ret = false;

            Guid classGuid = Guid.Empty;
            IntPtr hDevInfo = Win32.SetupDiGetClassDevs(ref classGuid, null, IntPtr.Zero, Win32.DIGCF_ALLCLASSES | Win32.DIGCF_PRESENT);
            if (hDevInfo.ToInt32() == Win32.INVALID_HANDLE_VALUE)
            {
                Console.WriteLine("访问硬件设备失败");
            }
            else
            {
                int i = 0;
                int selected = 0;

                StringBuilder deviceName = new StringBuilder();
                deviceName.Capacity = Win32.MAX_DEV_LEN;
                do
                {
                    SP_DEVINFO_DATA devInfoData = new SP_DEVINFO_DATA();
                    StringBuilder _DeviceName = new StringBuilder("");
                    _DeviceName.Capacity = 1000;

                    devInfoData.cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
                    devInfoData.classGuid = Guid.Empty;
                    devInfoData.devInst = 0;
                    devInfoData.reserved = IntPtr.Zero;
                    bool result = Win32.SetupDiEnumDeviceInfo(hDevInfo, i, devInfoData);
                    if (false == result)
                    {
                        break;
                    }

                    Win32.SetupDiGetDeviceRegistryProperty(hDevInfo, devInfoData, 0, 0, _DeviceName, (uint)_DeviceName.Capacity, IntPtr.Zero);

                    if (_DeviceName.ToString().Equals("USB Serial Port"))
                    {

                        selected = i;
                    }
                    ++i;
                } while (true);

                ret = Win32.StateChange(false, selected, hDevInfo);

            }
            Win32.SetupDiDestroyDeviceInfoList(hDevInfo);

            return ret;
        }

        #region setupapi.dll elements  
        [StructLayout(LayoutKind.Sequential)]
        public struct SP_BROADCAST_HANDLE
        {
            public int dbch_size;
            public int dbch_devicetype;
            public int dbch_reserved;
            public IntPtr dbch_handle;
            public IntPtr dbch_hdevnotify;
            public Guid dbch_eventguid;
            public long dbch_nameoffset;
            public byte dbch_data;
            public byte dbch_data1;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class DEV_BROADCAST_DEVICEINTERFACE
        {
            public int dbcc_size;
            public int dbcc_devicetype;
            public int dbcc_reserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class SP_DEVINFO_DATA
        {
            public int cbSize;
            public Guid classGuid;
            public int devInst;
            public IntPtr reserved;
        };

        [StructLayout(LayoutKind.Sequential)]
        public class SP_DEVINSTALL_PARAMS
        {
            public int cbSize;
            public int Flags;
            public int FlagsEx;
            public IntPtr hwndParent;
            public IntPtr InstallMsgHandler;
            public IntPtr InstallMsgHandlerContext;
            public IntPtr FileQueue;
            public IntPtr ClassInstallReserved;
            public int Reserved;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string DriverPath;
        };

        [StructLayout(LayoutKind.Sequential)]
        public class SP_PROPCHANGE_PARAMS
        {
            public SP_CLASSINSTALL_HEADER ClassInstallHeader = new SP_CLASSINSTALL_HEADER();
            public int StateChange;
            public int Scope;
            public int HwProfile;
        };

        [StructLayout(LayoutKind.Sequential)]
        public class SP_CLASSINSTALL_HEADER
        {
            public int cbSize;
            public int InstallFunction;
        };

        public class Win32
        {
            [DllImport("kernel32.dll")]
            public static extern uint GetLastError();
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, DEV_BROADCAST_DEVICEINTERFACE NotificationFilter, UInt32 Flags);

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern UInt32 UnregisterDeviceNotification(IntPtr hHandle);

            [DllImport("setupapi.dll", SetLastError = true)]
            public static extern IntPtr SetupDiGetClassDevs(ref Guid ClassGuid,
            [MarshalAs(UnmanagedType.LPStr)]String Enumerator, IntPtr hwndParent, Int32 Flags);

            [DllImport("setupapi.dll")]
            public static extern IntPtr SetupDiGetClassDevsEx(ref Guid ClassGuid,
            [MarshalAs(UnmanagedType.LPStr)]String Enumerator,
            IntPtr hwndParent, Int32 Flags, IntPtr DeviceInfoSet,
            [MarshalAs(UnmanagedType.LPStr)]String MachineName,
            IntPtr Reserved);

            [DllImport("setupapi.dll", SetLastError = true)]
            public static extern Int32 SetupDiDestroyDeviceInfoList(IntPtr lpInfoSet);

            [DllImport("setupapi.dll", SetLastError = true)]
            public static extern Boolean SetupDiEnumDeviceInfo(IntPtr lpInfoSet, Int32 dwIndex, SP_DEVINFO_DATA devInfoData);

            [DllImport("setupapi.dll", SetLastError = true)]
            public static extern Boolean SetupDiGetDeviceRegistryProperty(IntPtr lpInfoSet, SP_DEVINFO_DATA DeviceInfoData, UInt32 Property,
            UInt32 PropertyRegDataType, StringBuilder PropertyBuffer, UInt32 PropertyBufferSize, IntPtr RequiredSize);

            [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
            public static extern Boolean SetupDiSetClassInstallParams(IntPtr DeviceInfoSet, SP_DEVINFO_DATA DeviceInfoData, SP_PROPCHANGE_PARAMS ClassInstallParams, int ClassInstallParamsSize);

            [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
            public static extern Boolean SetupDiCallClassInstaller(UInt32 InstallFunction, IntPtr DeviceInfoSet, SP_DEVINFO_DATA DeviceInfoData);
            [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
            public static extern Boolean SetupDiRemoveDevice(IntPtr DeviceInfoSet, SP_DEVINFO_DATA DeviceInfoData);

            [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
            public static extern Boolean SetupDiClassNameFromGuid(ref Guid ClassGuid, StringBuilder className, Int32 ClassNameSize, ref Int32 RequiredSize);

            [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
            public static extern Boolean SetupDiGetClassDescription(ref Guid ClassGuid, StringBuilder classDescription, Int32 ClassDescriptionSize, ref Int32 RequiredSize);

            [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
            public static extern Boolean SetupDiGetDeviceInstanceId(IntPtr DeviceInfoSet, SP_DEVINFO_DATA DeviceInfoData, StringBuilder DeviceInstanceId, Int32 DeviceInstanceIdSize, ref Int32 RequiredSize);

            [DllImport("setupapi.dll", SetLastError = true)]
            public static extern bool SetupDiGetDeviceRegistryProperty(IntPtr DeviceInfoSet, SP_DEVINFO_DATA DeviceInfoData, int Property, uint PropertyRegDataType, StringBuilder PropertyBuffer, uint PropertyBufferSize, IntPtr RequiredSize);

            public const int DIGCF_ALLCLASSES = (0x00000004);
            public const int DIGCF_PRESENT = (0x00000002);
            public const int INVALID_HANDLE_VALUE = -1;
            public const int SPDRP_DEVICEDESC = (0x00000000);
            public const int MAX_DEV_LEN = 200;
            public const int DEVICE_NOTIFY_WINDOW_HANDLE = (0x00000000);
            public const int DEVICE_NOTIFY_SERVICE_HANDLE = (0x00000001);
            public const int DEVICE_NOTIFY_ALL_INTERFACE_CLASSES = (0x00000004);
            public const int DBT_DEVTYP_DEVICEINTERFACE = (0x00000005);
            public const int DBT_DEVNODES_CHANGED = (0x0007);
            public const int WM_DEVICECHANGE = (0x0219);
            public const int DIF_REMOVE = (0x00000005);
            public const int DIF_PROPERTYCHANGE = (0x00000012);
            public const int DICS_FLAG_GLOBAL = (0x00000001);
            public const int DICS_FLAG_CONFIGSPECIFIC = (0x00000002);
            public const int DICS_ENABLE = (0x00000001);
            public const int DICS_DISABLE = (0x00000002);
            public static bool Remove(int SelectedItem, IntPtr DevInfo)
            {
                bool result = false;
                SP_DEVINFO_DATA devInfoData = new SP_DEVINFO_DATA();
                devInfoData.cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA));
                if (true == SetupDiEnumDeviceInfo(DevInfo, SelectedItem, devInfoData))
                {
                    if (true == SetupDiCallClassInstaller(Win32.DIF_REMOVE, DevInfo, devInfoData))
                    { result = true; }
                    else
                    {
                        uint ret = (uint)Win32.GetLastError();
                    }
                }
                return result;
            }
            public static bool StateChange(bool Enable, int SelectedItem, IntPtr DevInfo) { bool result = false; SP_DEVINFO_DATA devInfoData = new SP_DEVINFO_DATA(); ; devInfoData.cbSize = Marshal.SizeOf(typeof(SP_DEVINFO_DATA)); if (true == SetupDiEnumDeviceInfo(DevInfo, SelectedItem, devInfoData)) { SP_PROPCHANGE_PARAMS pcp = new SP_PROPCHANGE_PARAMS(); ; pcp.ClassInstallHeader.cbSize = Marshal.SizeOf(typeof(SP_CLASSINSTALL_HEADER)); pcp.ClassInstallHeader.InstallFunction = DIF_PROPERTYCHANGE; pcp.Scope = DICS_FLAG_CONFIGSPECIFIC; pcp.StateChange = (Enable ? DICS_ENABLE : DICS_DISABLE); if (true == SetupDiSetClassInstallParams(DevInfo, devInfoData, pcp, Marshal.SizeOf(pcp))) { if (true == SetupDiCallClassInstaller(DIF_PROPERTYCHANGE, DevInfo, devInfoData)) { result = true; } else { uint ret = (uint)Win32.GetLastError(); } } } return result; }
            public static String GetClassNameFromGuid(Guid guid) { String result = String.Empty; StringBuilder className = new StringBuilder(); Int32 iRequiredSize = 0; Int32 iSize = 0; bool b = SetupDiClassNameFromGuid(ref guid, className, iSize, ref iRequiredSize); className = new StringBuilder(iRequiredSize); iSize = iRequiredSize; b = SetupDiClassNameFromGuid(ref guid, className, iSize, ref iRequiredSize); if (true == b) { result = className.ToString(); } return result; }
            public static String GetClassDescriptionFromGuid(Guid guid) { String result = String.Empty; StringBuilder classDesc = new StringBuilder(0); Int32 iRequiredSize = 0; Int32 iSize = 0; bool b = SetupDiGetClassDescription(ref guid, classDesc, iSize, ref iRequiredSize); classDesc = new StringBuilder(iRequiredSize); iSize = iRequiredSize; b = SetupDiGetClassDescription(ref guid, classDesc, iSize, ref iRequiredSize); if (true == b) { result = classDesc.ToString(); } return result; }
            public static String GetDeviceInstanceId(IntPtr DeviceInfoSet, SP_DEVINFO_DATA DeviceInfoData) { String result = String.Empty; StringBuilder id = new StringBuilder(0); Int32 iRequiredSize = 0; Int32 iSize = 0; bool b = SetupDiGetDeviceInstanceId(DeviceInfoSet, DeviceInfoData, id, iSize, ref iRequiredSize); id = new StringBuilder(iRequiredSize); iSize = iRequiredSize; b = SetupDiGetDeviceInstanceId(DeviceInfoSet, DeviceInfoData, id, iSize, ref iRequiredSize); if (true == b) { result = id.ToString(); } return result; }
        }
        #endregion #region setupapi.dll elements used by Rescan 
        [DllImport("setupapi.dll", SetLastError = true)]
        static extern int CM_Locate_DevNode_Ex(ref UInt32 pdnDevInst, string pDeviceID, int ulFlags, IntPtr hMachine);
        [DllImport("setupapi.dll")]
        static extern UInt32 CM_Reenumerate_DevNode_Ex(UInt32 dnDevInst, UInt32 ulFlags, IntPtr hMachine);
        const uint CR_SUCCESS = 0x00000000;
    }


}
