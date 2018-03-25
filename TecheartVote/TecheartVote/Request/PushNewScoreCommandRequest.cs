using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartVote.Response;

namespace TecheartVote.Request
{
    public class PushNewScoreCommandRequest : BaseCommandRequest
    {
        public PushNewScoreCommandRequest(HandshakeResponse response, shareAction1 s1, shareAction2 s2, long subNumber,String score,int number) :base(response, s1, s2)
        {
            this.dataBelong = 0;
            this.number = number;
            this.dotPwoer = SubVoteDisplayAction.GetSubVoteDisplayAction(score);
            this.request = SubVoteDisplayAction.GetDisplayData(score) + (((ulong)subNumber) << 32);
        }
        public override Share1Enum GetShare1Enum()
        {
            return Share1Enum.WriteScore;
        }
    }
}
