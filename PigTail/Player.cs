using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PigTail
{
    class Player: IDisposable
    {
        private string playerName;
        private Stack<Poker> spadePile;//♠
        private Stack<Poker> heartPile;//♥
        private Stack<Poker> clubPile;//♣️
        private Stack<Poker> diamondPile;//♦
        public Player(string playerName)
        {
            this.playerName = playerName;
            this.spadePile = new Stack<Poker>();
            this.heartPile = new Stack<Poker>();
            this.clubPile = new Stack<Poker>();
            this.diamondPile = new Stack<Poker>();
        }
        public Stack<Poker> getSpadePile()
        {
            return this.spadePile;
        }
        public Stack<Poker> getHeartPile()
        {
            return this.heartPile;
        }
        public Stack<Poker> getClubPile()
        {
            return this.clubPile;
        }
        public Stack<Poker> getDiamondPile()
        {
            return this.diamondPile;
        }

        public int getPokerNum()
        {
            return this.getSpadePile().Count + this.getHeartPile().Count
                + this.getClubPile().Count + this.getDiamondPile().Count;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
