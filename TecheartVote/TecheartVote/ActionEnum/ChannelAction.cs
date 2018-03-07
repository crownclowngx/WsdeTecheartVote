using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote
{
    public class ChannelAction
    {
        public static Byte GetRenewAction(int channelNumber)
        {
            if(channelNumber<1 || channelNumber > 99)
            {
                throw new Exception("通道必须是1-99之间的值");
            }
            return Convert.ToByte(128 + channelNumber);
        }
        public static Byte GetNuRenewAction()
        {
            return 0;
        }
    }
}
