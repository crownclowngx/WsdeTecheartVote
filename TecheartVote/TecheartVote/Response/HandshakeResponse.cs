using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote.Response

{
    /// <summary>
    /// 握手协议返回值
    /// </summary>
    public class HandshakeResponse
    {
        /// <summary>
        /// 信道，无限通道，主机和投票器需要相同 
        /// </summary>
        public int Channel { get; set; }

        /// <summary>
        /// 无限密钥  
        /// </summary>
        public int SecretKey { get; set; }

        /// <summary>
        /// 主机id 
        /// </summary>
        public long Address { get; set; }

        /// <summary>
        /// 备注 最多23个汉字
        /// </summary>
        public string Remark { get; set; }
    }

    
}
