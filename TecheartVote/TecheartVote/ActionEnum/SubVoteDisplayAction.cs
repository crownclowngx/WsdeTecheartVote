using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote
{
    /// <summary>
    /// 子机展示控制
    /// </summary>
    public class SubVoteDisplayAction
    {
        static Dictionary<String, Byte> dic = new Dictionary<string, Byte>()
        {
            { " ",0},
            { "0",1},
            { "1",2},
            { "2",3},
            { "3",4},
            { "4",5},
            { "5",6},
            { "6",7},
            { "7",8},
            { "8",9},
            { "9",10},
            { "j",1},
            { "a",2},
            { "b",3},
            { "c",4},
            { "d",5},
            { "e",6},
            { "f",7},
            { "g",8},
            { "h",9},
            { "i",10},
        };

        static Dictionary<int, String> dicreverse = new Dictionary<int, String>()
        {
            {0, ""},
            {1, "0"},
            {2, "1"},
            {3, "2"},
            {4, "3"},
            {5, "4"},
            {6, "5"},
            {7, "6"},
            {8, "7"},
            {9, "8"},
            {10,"9"},
            {100,""},
            {101,"j"},
            {102,"a"},
            {103,"b"},
            {104,"c"},
            {105,"d"},
            {106,"e"},
            {107,"f"},
            {108,"g"},
            {109,"h"},
            {110,"i"},
            {9999,"."},
        };
        public static Byte GetSubVoteDisplayAction(String str)
        {
            Byte response = 0;
            if (Verification.VerificationTools.StringIsNumber(str))
            {
                response += 128;
            }
            var spstr = str.Split('.');
            if (spstr.Count() == 1)
            {
                return response;
            }
            return Convert.ToByte(response + ((spstr.Count() + 1)<<4));
        }

        /// <summary>
        /// yes=yes,no=no,login=login
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static UInt64 GetDisplayData(String data)
        {
            UInt64 ul = 0;
            if (data.ToLower() == "yes")
            {
                return 768955;
            }
            if (data.ToLower() == "no")
            {
                return 838860;
            }
            if (data.ToLower() == "login")
            {
                return 908765;
            }
            List<Byte> lby = new List<byte>();
            foreach(var v in data)
            {
                if (!dic.ContainsKey(v.ToString().ToLower()))
                {
                    throw new Exception("所传入的值，中有不能被识别的字符"+v.ToString());
                }
                lby.Add(dic[v.ToString().ToLower()]);
            }
            lby.Reverse();
            int Offset = 0;
            foreach (var v in lby)
            {
                ul += Convert.ToUInt64(v) << Offset;
                Offset += 4;
            }
            return ul;
        }

        public static String AnalysisDisplayData(byte[] arr)
        {
            return "";
        }
        public static long AnalysisSubAddress(byte[] arr)
        {
            return (Convert.ToInt64(arr[0]) << 32) +
             (Convert.ToInt64(arr[1]) << 16) +
             (Convert.ToInt64(arr[2]) << 8) +
             Convert.ToInt64(arr[3]);
        }

        public static SubDisplaySign AnalysisSubDisplaySign(Byte sign)
        {
            SubDisplaySign ana = new SubDisplaySign();
            if ((sign & 0x80) !=0)
            {
                ana.isLetter = true;
            }
            ana.floatLocation=(sign & 0x70) >> 4;
            ana.batteryLevel = (sign & 0xF);
            return ana;
        }

        /// <summary>
        /// byte数组是一个 8Byte的数组
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static String AnalysisDisplayData(SubDisplaySign sign, byte[] arr)
        {
            List<int> lreturn = new List<int>();
            lreturn.Add( arr[5]);
            if (sign.floatLocation == 5)
            {
                lreturn.Add(9999);
            }
            lreturn.Add( (arr[6] & 0xf0)>>4);
            if (sign.floatLocation == 4)
            {
                lreturn.Add(9999);
            }
            lreturn.Add( arr[6] & 0xf);
            if (sign.floatLocation == 3)
            {
                lreturn.Add(9999);
            }
            lreturn.Add( (arr[7] & 0xf0)>>4);
            if (sign.floatLocation == 2)
            {
                lreturn.Add(9999);
            }
            lreturn.Add( arr[7] & 0xf);
            if (sign.floatLocation == 1)
            {
                lreturn.Add(9999);
            }
            if (sign.isLetter)
            {
                for (int i = 0; i < lreturn.Count; i++)
                {
                    lreturn[i] += 100;
                }
            }
            List<String> lresp = new List<string>();
            if (lreturn.TrueForAll(k => k == 13 || k == 113))
            {
                return "login";
            }
            if(lreturn.TrueForAll(k => k == 12 || k == 112)){
                return "no";
            }
            if (lreturn.TrueForAll(k => k == 11 || k == 111))
            {
                return "yes";
            }
            lreturn.ForEach(k => lresp.Add(dicreverse[k]));

            StringBuilder sb = new StringBuilder();
            lresp.ForEach(k => sb.Append(k));
            return sb.ToString();
        }
    }

    public class SubDisplaySign
    {
        /// <summary>
        /// 是否展示成字母
        /// </summary>
        public bool isLetter { get; set; }

        /// <summary>
        /// 浮点位置 0 表示没有小数点 
        /// </summary>
        public int floatLocation { get; set; }

        /// <summary>
        /// 电池电量
        /// </summary>
        public int batteryLevel { get; set; }
    }
}
