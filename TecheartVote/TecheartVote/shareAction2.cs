using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote
{
    public class shareAction2
    {
        /// <summary>
        /// 客户端是否允许输入数字
        /// </summary>
        public bool clientCanWriteNumber { get; set; }

        /// <summary>
        /// 是否允许输入英语字母
        /// </summary>
        public bool clientCanWriteABC { get; set; }

        /// <summary>
        /// 是否允许客户端修改信道
        /// </summary>
        public bool clientCanChangeChannel { get; set; }

        /// <summary>
        /// 是否允许擦除内存数据
        /// </summary>
        public bool clientCanErase { get; set; }

        /// <summary>
        /// 是否能够修改日期
        /// </summary>
        public bool clientCanChangeDate { get; set; }

        /// <summary>
        /// 是否允许修改具体事件
        /// </summary>
        public bool clientCanCnahgeTime { get; set; }

        /// <summary>
        /// 客户端是否可以查看答案
        /// </summary>
        public bool clinetCanSeeAnswer { get; set; }

        public Byte GetShare2Byte()
        {
            Byte b = 0;
            if (!clientCanWriteNumber)
            {
                b += 128;
            }
            if (!clientCanWriteABC)
            {
                b += 64;
            }
            if (!clientCanChangeChannel)
            {
                b += 32;
            }
            if (!clientCanErase)
            {
                b += 16;
            }
            if (!clientCanChangeDate)
            {
                b += 8;
            }
            if (!clientCanCnahgeTime)
            {
                b += 4;
            }
            if (!clinetCanSeeAnswer)
            {
                b += 2;
            }
            return b;
        }

        public static shareAction2 GetAllAllowShare()
        {
            return new shareAction2()
            {
                clientCanChangeChannel = true,
                clientCanChangeDate = true,
                clientCanCnahgeTime = true,
                clientCanErase = true,
                clientCanWriteABC = true,
                clientCanWriteNumber = true,
                clinetCanSeeAnswer = true,
            };
        }
    }
}
