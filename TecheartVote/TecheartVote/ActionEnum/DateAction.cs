using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote
{
    public class DateAction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static UInt64 GetRenewAction(DateTime time)
        {
            List<Byte> lb = new List<byte>() { 0,0};
            lb.Add(Convert.ToByte(128 + time.Year%100));
            lb.Add(Convert.ToByte(time.Month));
            lb.Add(Convert.ToByte(time.Day));

            lb.Add(Convert.ToByte(128 + time.Hour));
            lb.Add(Convert.ToByte(time.Minute));
            lb.Add(Convert.ToByte(time.Second));
            int start = 56;
            UInt64 response = 0;
            lb.ForEach(k => 
            {
                response+=Convert.ToUInt64(k) << start;
                start -= 8;
            });
            return 0;
        }
        public static UInt64 GetNuRenewAction()
        {
            return 0;
        }
    }
}
