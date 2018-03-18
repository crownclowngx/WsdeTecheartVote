using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartVote.Response;

namespace TecheartVote.Request
{
    public class ConfigScoreCommandRequest:BaseCommandRequest
    {
        public ConfigScoreCommandRequest(HandshakeResponse response, shareAction1 s1, shareAction2 s2) :base(response, s1, s2)
        {
            this.dataBelong = 0;
            this.number = FrequencyAction.GetNuRenewAction();
            this.dotPwoer = ChannelAction.GetNuRenewAction();
            this.request = DateAction.GetNuRenewAction();
        }
        public void SetFrequency(FrequencyEnum enu)
        {
            this.number = FrequencyAction.GetRenewAction(enu);
        }

        public void SetChannel(int channelNum)
        {
            this.dotPwoer = ChannelAction.GetRenewAction(channelNum);
        }

        public void SetDate(DateTime time)
        {
            this.request = DateAction.GetRenewAction(time);
        }

        public override Share1Enum GetShare1Enum()
        {
            return Share1Enum.WriteScore;
        }
    }
}
