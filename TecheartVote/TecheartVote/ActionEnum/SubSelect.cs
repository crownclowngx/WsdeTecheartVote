using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote
{
    public class SubSelect
    {
        /// <summary>
        /// 子机地址
        /// </summary>
        public long address { get; set; }

        /// <summary>
        /// 不明意义可能是题号？
        /// </summary>
        public int subjectNumber { get; set; }

        /// <summary>
        /// 选择的数据
        /// </summary>
        public String selectData { get; set; }

        public long key { get; set; }
    }
}
