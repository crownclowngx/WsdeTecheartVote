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
    }
}
