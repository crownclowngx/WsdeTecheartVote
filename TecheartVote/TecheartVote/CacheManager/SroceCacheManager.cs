using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote
{
    public class SroceCacheManager
    {
        private Dictionary<int, String> dic = new Dictionary<int, string>();
        public SroceCacheManager()
        {
            Clear();
        }

        public void SetAnswer(int subjecjNumber, String answer)
        {
            if (dic.ContainsKey(subjecjNumber))
            {
                dic[subjecjNumber] = answer;
            }
        }

        public String GetAnswer(int num)
        {
            if (dic.ContainsKey(num))
                return dic[num];
            else
                return "0";
        }
        public void Clear()
        {
            dic.Clear();
        }
    }
}
