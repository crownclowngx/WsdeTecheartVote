using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartVote.Response;

namespace TecheartVote.Request
{
    public class PushScoreCommandRequest : BaseCommandRequest
    {
        public PushScoreCommandRequest(HandshakeResponse response, shareAction1 s1, shareAction2 s2,long subNumber,String answer) : base(response, s1, s2)
        {
            this.dataBelong = 64;
            this.number = 130;
            this.machineAddress = subNumber;
            this.dotPwoer = SubVoteDisplayAction.GetSubVoteDisplayAction(answer);
            this.request = SubVoteDisplayAction.GetDisplayData(answer);
            this.handshakeSecretKey = 2;
        }

        public override Share1Enum GetShare1Enum()
        {
            return Share1Enum.WriteScore;
        }
    }
}
