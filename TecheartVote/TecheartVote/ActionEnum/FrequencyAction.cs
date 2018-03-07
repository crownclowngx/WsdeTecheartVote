using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote
{
    /// <summary>
    /// 频率更新命令
    /// </summary>
    public class FrequencyAction
    {
        public static Byte GetRenewAction(FrequencyEnum enu)
        {
            return Convert.ToByte(8 + (int)enu);
        }
        public static Byte GetNuRenewAction()
        {
            return 0;
        }
    }

    public enum FrequencyEnum
    {
        dBM7=7,
        dBM4 = 6,
        dBM3 = 5,
        dBM1 = 4,
        dBM0 = 3,
        dBMNegative4 = 2,
        dBMNegative6 = 1,
        dBMNegative12 = 0,
    }
}
