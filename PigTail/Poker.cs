using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigTail
{
    class Poker: IDisposable
    {
        private string pokerType;//用于定义扑克牌的类型，例如S,H,C,D
        private string pokerNumber;//用于定义扑克牌的数字，例如K
        public Poker(string pokerType,string pokerNumber)
        {
            this.pokerType = pokerType;
            this.pokerNumber = pokerNumber;
        }
        public string getPokerType()
        {
            return this.pokerType;
        }
        public string getPokerNumber()
        {
            return this.pokerNumber;
        }

        /// <summary>
        /// 服务于网络对战的获取扑克牌类型，转换为C8的格式
        /// </summary>
        /// <returns></returns>
        public string getPokerTypeInternet()
        {
            string pokerInfo = this.pokerType;
            if (int.Parse(this.pokerNumber) > 10)
            {
                if (this.pokerNumber == "11") pokerInfo += "J";
                else if (this.pokerNumber == "12") pokerInfo += "Q";
                else pokerInfo += "K";
            }
            else pokerInfo += this.pokerNumber;
            return pokerInfo;
        }
        /// <summary>
        /// 获取扑克牌对应图片名称，映射
        /// </summary>
        /// <returns></returns>
        public string getPokerImg()
        {
            string pokerTypeZH = "";
            if (this.pokerType == "S") pokerTypeZH = "黑桃";
            else if (this.pokerType == "H") pokerTypeZH = "红桃";
            else if (this.pokerType == "C") pokerTypeZH = "梅花";
            else pokerTypeZH = "方块";
            return pokerTypeZH + this.pokerNumber + ".png";
        }
        
        public bool Equals(Poker another)
        {
            if (this.pokerNumber == another.pokerNumber &&
                this.pokerType == another.pokerType) return true;
            return false;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
