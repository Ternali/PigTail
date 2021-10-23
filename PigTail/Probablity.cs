using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigTail
{
    class Probablity
    {
        private double pokerNext;
        private string pokerType;
        public Probablity(double pokerNext, string pokerType)
        {
            this.pokerNext = pokerNext;
            this.pokerType = pokerType;
        }
        /// <summary>
        /// 自定义比较函数用于排序可能性
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public static int Compare(Probablity one, Probablity two)
        {
            if (one.pokerNext < two.pokerNext)return 1;
            else if (one.pokerNext == two.pokerNext)return 0;
            else return -1;
        }

        public string getPokerType()
        {
            return this.pokerType;
        }
    }
}
