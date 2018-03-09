using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartVote.Response;

namespace TecheartVote.Request
{
    public class PushAnswerCommandRequest : BaseCommandRequest
    {
        public PushAnswerCommandRequest(HandshakeResponse response, shareAction1 s1, shareAction2 s2,int questionNumber,String answer) : base(response, s1, s2)
        {
            this.dataBelong = 0;
            this.number = questionNumber;
            this.dotPwoer = SubVoteDisplayAction.GetSubVoteDisplayAction(answer);
            this.request = SubVoteDisplayAction.GetDisplayData(answer);
        }

        public override Share1Enum GetShare1Enum()
        {
            return Share1Enum.WriteSolution;
        }
    }
}
