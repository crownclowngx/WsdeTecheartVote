using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartVote.Response;

namespace TecheartVote.Request
{
    [Obsolete("因为下发分数有严格的时间限制，所以该方法已不适用 使用此方法将不会出现异常，但是不能在子机上显示相关 注意该类将不会生成正确的结果也将不能导致正确的行为")]
    public class PushScoreCommandRequest : BaseCommandRequest
    {
        public PushScoreCommandRequest(HandshakeResponse response, shareAction1 s1, shareAction2 s2,long problemNumber, String answer) : base(response, s1, s2)
        {
            this.dataBelong = 64;
            this.number = 130;
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
