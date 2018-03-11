using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote.CacheManager
{
    public class SubjectCacheManger
    {
        private Dictionary<int, String> dic = new Dictionary<int, string>();
        public SubjectCacheManger()
        {
            Clear();
        }

        public void SetAnswer(int subjecjNumber,String answer)
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
                return " ";
        } 
        public void Clear()
        {
            for (int i = 1; i < 130; i++)
            {
                if (dic.ContainsKey(i))
                {
                    dic[i] = " ";
                }
                else
                {
                    dic.Add(i, " ");
                }
                
            }
        }
    }
}
