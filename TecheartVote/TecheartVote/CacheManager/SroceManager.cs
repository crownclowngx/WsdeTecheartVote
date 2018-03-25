using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TecheartVote.CacheManager
{
    public class SroceManager
    {
        private IList<Sroce> sroces { get; set; }
        public SroceManager()
        {
            sroces= new List<Sroce>();
        }
        /// <summary>
        /// 注意：该方法非线程安全
        /// </summary>
        /// <param name="subNumber"></param>
        /// <param name="sroce"></param>
        public void Add(long subNumber,String sroce)
        {
            if(sroces.Any(k=>k.subNumber== subNumber))
            {
                throw new Exception("该方法不允许修改，修改请使用Update");
            }
            if (sroces.Count() > 95)
            {
                throw new Exception("缓存只可装在95个分数，不支持更多分数");
            }
            sroces.Add(new Sroce() { number= sroces.Count+1, sroce=sroce, subNumber=subNumber });
        }

        /// <summary>
        /// 注意：该方法非线程安全
        /// </summary>
        /// <param name="subNumber"></param>
        /// <param name="sroce"></param>
        public void Update(long subNumber, String sroce)
        {
            if (!sroces.Any(k => k.subNumber == subNumber))
            {
                throw new Exception("没有找到该子机编号，尝试修改失败");
            }
            sroces.Where(k => k.subNumber == subNumber).First().sroce = sroce;
        }

        /// <summary>
        /// 检查subnum是否存在
        /// </summary>
        /// <param name="subNum"></param>
        /// <returns></returns>
        public bool HasSubNumber(long subNum)
        {
            return sroces.Any(k => k.subNumber == subNum);
        }
        /// <summary>
        /// 获取成绩列表
        /// </summary>
        /// <returns></returns>
        public IList<Sroce> GetSroce()
        {
            IList<Sroce> srocesNew = new List<Sroce>();
            for(int i = 0; i < 95; i++)
            {
                srocesNew.Add(new Sroce() { number = srocesNew.Count + 1, subNumber=0, sroce="0"});
            }
            if (sroces != null)
            {
                sroces.ToList().ForEach(k =>
                {
                    var sub = srocesNew.First(s => s.number == k.number);
                    sub.sroce = k.sroce;
                    sub.subNumber = k.subNumber;
                });
            }
            
            return srocesNew;
        }
    }

    public class Sroce
    {
        /// <summary>
        /// 子机地址
        /// </summary>
        public long subNumber { get; set; }

        /// <summary>
        /// 顺序编号
        /// </summary>
        public int number { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public String sroce { get; set; }
    }
}
