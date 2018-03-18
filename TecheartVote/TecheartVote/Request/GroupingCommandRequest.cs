using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TecheartVote.Response;

namespace TecheartVote.Request
{
    /// <summary>
    /// 分组命令传入值，可以对client的组进行任意分配
    /// </summary>
    public class GroupingCommandRequest:BaseCommandRequest
    {

        /// <summary>
        /// 分组命令
        /// </summary>
        /// <param name="response">握手返回值</param>
        /// <param name="secrets">密码列表</param>
        /// <param name="groupNumber">当前组数量 从1 开始</param>
        public GroupingCommandRequest(HandshakeResponse response,List<UInt64> secrets,int groupNumber, shareAction1 s1, shareAction2 s2) :base(response, s1, s2)
        {
            this.dataBelong = 0;
            this.number = groupNumber;
            this.dotPwoer = secrets.Count();
            var baseOffset=(groupNumber - 1) * 4;
            var maxOffset = baseOffset + 4;
            List<UInt64> secretsInt = new List<UInt64>();
            if(baseOffset+4>= secrets.Count())
            {
                maxOffset = secrets.Count();
            }
            for(int i= baseOffset;i< maxOffset; i++)
            {
                secretsInt.Add(secrets[i]);
            }
            if (secretsInt.Count() < 4)
            {
                var count = secretsInt.Count();
                for (int i=0;i<4- count; i++)
                {
                    secretsInt.Add(0);
                }
            }
            this.request += secretsInt[0] << 48;
            this.request += secretsInt[1] << 32;
            this.request += secretsInt[2] << 16;
            this.request += secretsInt[3];

        }

        public override Share1Enum GetShare1Enum()
        {
            return Share1Enum.WriteEncrypted;
        }
    }
}
