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

        private Dictionary<int, int> dicscore = new Dictionary<int, int>();
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
            else
                dic.Add(subjecjNumber, answer);

            if (dicscore.ContainsKey(subjecjNumber))
            {
                dicscore[subjecjNumber] = 1;
            }
            else
                dicscore.Add(subjecjNumber, 1);
        }

        public void SetAnswer(int subjecjNumber, String answer,int score)
        {
            if (dic.ContainsKey(subjecjNumber))
            {
                dic[subjecjNumber] = answer;
            }
            else
                dic.Add(subjecjNumber, answer);

            if (dicscore.ContainsKey(subjecjNumber))
            {
                dicscore[subjecjNumber] = score;
            }
            else
                dicscore.Add(subjecjNumber, score);
                
        }

        public String GetAnswer(int num)
        {
            if (dic.ContainsKey(num))
                return dic[num];
            else
                return " ";
        } 

        public int GetScore(int num)
        {
            if (dicscore.ContainsKey(num))
                return dicscore[num];
            else
                return 0;
        }

        public bool IsRight(int subjecjNumber, String answer)
        {
            if (!dic.ContainsKey(subjecjNumber) || !dicscore.ContainsKey(subjecjNumber))
            {
                return false;
            }
            if(dic[subjecjNumber].ToLower()== answer.ToLower().Trim())
            {
                return true;
            }
            return false;
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
