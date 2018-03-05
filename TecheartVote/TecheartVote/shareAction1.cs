using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote
{
    public class shareAction1
    {
        /// <summary>
        /// 固化主机配置
        /// </summary>
        public bool persistenceConfiguration { get; set; }

        /// <summary>
        /// 子机是否可以快速清除
        /// </summary>
        public bool clientCanClearSoon { get; set; }

        /// <summary>
        /// 子机是否可以答题
        /// </summary>
        public bool clientCanAnswer { get; set; }

        /// <summary>
        /// 终端擦除内存
        /// </summary>
        public bool eraseClientMemory { get; set; }

        /// <summary>
        /// 终端是否可以查看答案
        /// </summary>
        public bool clientCanSeeSolution { get; set; }

        public Byte GetShare1Byte(Share1Enum enu)
        {
            Byte b = 0;
            if (persistenceConfiguration)
            {
                b += 128;
            }
            if (clientCanClearSoon)
            {
                b += 64;
            }
            if (clientCanAnswer)
            {
                b += 32;
            }
            if (eraseClientMemory)
            {
                b += 16;
            }
            if (clientCanSeeSolution)
            {
                b += 8;
            }
            b+=(Byte)enu;
            return b;
        }
    }
    /// <summary>
    /// share1 的动作枚举
    /// </summary>
    public enum Share1Enum
    {
        WriteEncrypted=2,
        WriteConfiguration = 1,
        WriteSolution=4,
        WriteScore=3,
    }
}
