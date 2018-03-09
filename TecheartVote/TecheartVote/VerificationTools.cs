using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote.Verification
{
    /// <summary>
    /// 验证工具类
    /// </summary>
    public class VerificationTools
    {
        /// <summary>
        /// 握手协议返回值头部验证
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool HandshakeHeadVerification(List<int> list)
        {
            if(list == null || list.Count!=82)
            {
                return false;
            }
            return list[0] == 170 && list[1] == 17;
        }

        public static bool HashCheck(List<int> list)
        {
            if (list == null || list.Count != 82)
            {
                return false;
            }
            long last=list.Last();
            return last== HashCalc(list);
        }
        public static bool HashCheck(List<Byte> list)
        {
            return HashCheck(list.ConvertAll<int>(new Converter<byte, int>(new Func<Byte, int>(k => Convert.ToInt32(k)))));
        }
        public static long HashCalc(List<int> list)
        {
            if (list == null)
            {
                throw new Exception ("试图对一个空数组进行和校验");
            }
            long last = list.Last();
            long sum = list.Sum() - last;
            return sum & 255;
        }
        public static long HashCalc(List<Byte> list)
        {
            return HashCalcNew(list.ConvertAll<int>(new Converter<byte, int>(new Func<Byte, int>(k => Convert.ToInt32(k)))));
        }

        public static long HashCalcNew(List<int> list)
        {
            if (list == null)
            {
                throw new Exception("试图对一个空数组进行和校验");
            }
            long sum = list.Sum();
            return sum & 255;
        }

        public static bool StringIsNumber(String str)
        {
            double k = 0;
            if(double.TryParse(str, out k))
            {
                return true;
            }
            return false;

        }
    }
}
