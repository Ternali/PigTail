using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigTail
{
    public partial class LocalMode : Form
    {
        private Menu menu;
        private Poker[] poker;
        private Stack<Poker> pokerPile;
        private Stack<Poker> pokerPublished;
        private Player playerOne;
        private Player playerTwo;
        private SoundPlayer sp;
        private int whichOne;//用于判断究竟是谁进行的操作
        private Random r;//随机数生成对象用于打乱卡牌
        public LocalMode(Menu menu)
        {
            InitializeComponent();
            this.menu = menu;
        }

        #region 初始化操作
        private void initialOperation()
        {
            this.cardPileNum.Text = "X52";
            this.cardPublishedNum.Text = "X0";
            this.downSpade.Text = "X0";
            this.downHeart.Text = "X0";
            this.downClub.Text = "X0";
            this.downDiamond.Text = "X0";
            this.upSpade.Text = "X0";
            this.upHeart.Text = "X0";
            this.upClub.Text = "X0";
            this.upDiamond.Text = "X0";
            //buttonTextInitialize();//将按钮文字初始化
            poker_Initialize();//将扑克牌初始化
            poker_Randomization();//将扑克牌随机打乱
            poker_PushPile();//将扑克牌放到牌堆
            playerInitialize();//将玩家初始化
            whichOne = 0;//计数器归零
        }
        #endregion

        #region 播放音乐
        private void playSound()
        {
            this.sp = new System.Media.SoundPlayer();
            sp.SoundLocation = @"..\..\Resources\CreationAndBeyond.wav";
            sp.PlayLooping();
        }
        #endregion

        #region 玩家初始化
        private void playerInitialize()
        {
            this.playerOne = new Player("玩家一");
            this.playerTwo = new Player("玩家二");
        }
        #endregion

        #region 扑克牌初始化
        private void poker_Initialize()
        {
            //牌堆背面初始化操作 
            this.cardPile.Image = Image.FromFile(@"..\..\Resources\pigtail.png" );
            this.cardPublished.Image = Image.FromFile(@"..\..\Resources\pigtail.png");
            int countNumber = 0;
            //初始化黑桃类型扑克
            for (int i = 1; i <= 13; i++, countNumber++)
            {
                poker[countNumber] = new Poker("S", Convert.ToString(i));
            }
            //初始化红心类型扑克
            for (int i = 1; i <= 13; i++, countNumber++)
            {
                poker[countNumber] = new Poker("H", Convert.ToString(i));
            }
            //初始化草花类型扑克
            for (int i = 1; i <= 13; i++, countNumber++)
            {
                poker[countNumber] = new Poker("C", Convert.ToString(i));
            }
            //初始化方块类型扑克
            for (int i = 1; i <= 13; i++, countNumber++)
            {
                poker[countNumber] = new Poker("D", Convert.ToString(i));
            }
        }
        #endregion

        #region 扑克牌堆打乱
        private void poker_Randomization()
        { 
            for(int i=0;i<poker.Length;i++)
            {
                int index = r.Next(poker.Length);
                Poker tmp = poker[i];
                poker[i] = poker[index];
                poker[index] = tmp;
            }
        }
        #endregion

        #region 将扑克牌放入牌堆
        private void poker_PushPile()
        {
            for(int i=0;i<52;i++)
            {
                pokerPile.Push(poker[i]);
            }
        }
        #endregion

        #region 点击牌堆做出相应
        private void cardPile_Click(object sender, EventArgs e)
        {
            if(this.pokerPile.Count()!=0)//牌堆还有牌
            {
                Poker tmp = this.pokerPile.Pop();
                //替换牌堆的牌让用户看到翻到了什么类型的牌
                this.cardPile.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                this.cardPile.Refresh();
                //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                Thread.Sleep(1000);
                //重新将卡牌的背面显示给牌堆
                this.cardPile.Image = Image.FromFile(@"..\..\Resources\pigtail.png");
                this.cardPile.Refresh();
                //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                bool situation = logicCheck(tmp);                
                this.pokerPublished.Push(tmp);
                this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" +tmp.getPokerImg());
                this.cardPublished.Refresh();
                Thread.Sleep(1000);
                //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                //检查是否符合要吃牌的情况
                if (situation) eatingPoker(tmp);           
            }
            else//牌堆已经没牌了提示用户非法操作
            {
                MessageBox.Show("没牌啦别抽抽，算账了");
                finishedMatch();//结算方法
            }
            this.whichOne++;//计数器自增用于判定是谁操作
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();//刷新玩家手牌
        }
        #endregion

        #region 判断是否打出的牌和翻开的牌和顶部相同
        private bool logicCheck(Poker tmp)
        {
            //一张都没有当然要返回false
            if (this.pokerPublished.Count == 0) return false;
            //用于判断翻开或者从手牌打出的牌是否与已经亮相的牌顶部类型一致
            if (tmp.getPokerType() == this.pokerPublished.Peek().getPokerType())return true;
            return false;
        }
        #endregion

        #region 无可奈何的吃牌
        private void eatingPoker(Poker tmp)
        {
            Player tmpPlayer;
            if (whichOne % 2 == 0) tmpPlayer = this.playerOne;
            else tmpPlayer = this.playerTwo;
            //this.pokerPublished.Push(tmp); BUG修复
            while(this.pokerPublished.Count() != 0)
            {
                tmp = pokerPublished.Pop();
                if (tmp.getPokerType() == "S") tmpPlayer.getSpadePile().Push(tmp);
                else if (tmp.getPokerType() == "H") tmpPlayer.getHeartPile().Push(tmp);
                else if (tmp.getPokerType() == "C") tmpPlayer.getClubPile().Push(tmp);
                else tmpPlayer.getDiamondPile().Push(tmp);
            }
            //重新将卡牌的背面显示给牌堆
            this.cardPublished.Image = Image.FromFile(@"..\..\Resources\pigtail.png");
            this.cardPublished.Refresh();
        }
        #endregion

        #region 点击玩家一牌库应对事件

        #region 点击玩家一黑桃牌库
        private void spadeLeft_Click(object sender, EventArgs e)
        {
            if (whichOne % 2 == 0)
            {
                //判断黑桃牌牌堆是否非空
                if (this.playerOne.getSpadePile().Count() != 0)
                {
                    Poker tmp = this.playerOne.getSpadePile().Pop();
                    //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                    bool situation = logicCheck(tmp);
                    this.pokerPublished.Push(tmp);
                    this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                    this.cardPublished.Refresh();
                    Thread.Sleep(1000);
                    //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                    //检查是否符合要吃牌的情况
                    if (situation) eatingPoker(tmp);
                }
                else
                {
                    MessageBox.Show("没牌没准是件好事");
                    return;
                }
                whichOne++;
            }
            else
            {
                MessageBox.Show("嘿怎么能乱动对手的牌，这简直比玛丽阿姨做的蛋糕还要糟糕。");
                return;
            }
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();
        }
        #endregion

        #region 点击玩家一红桃牌库
        private void heartLeft_Click(object sender, EventArgs e)
        { 
            if (whichOne % 2 == 0)
            {
                //判断黑桃牌牌堆是否非空
                if (this.playerOne.getHeartPile().Count() != 0)
                {
                    Poker tmp = this.playerOne.getHeartPile().Pop();
                    //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                    bool situation = logicCheck(tmp);
                    this.pokerPublished.Push(tmp);
                    this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                    this.cardPublished.Refresh();
                    Thread.Sleep(1000);
                    //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                    //检查是否符合要吃牌的情况
                    if (situation) eatingPoker(tmp);
                }
                else
                {
                    MessageBox.Show("没牌没准是件好事");
                    return;
                }
                whichOne++;
            }
            else
            {
                MessageBox.Show("嘿怎么能乱动对手的牌，这简直比玛丽阿姨做的蛋糕还要糟糕。");
                return;
            }
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();
        }
        #endregion

        #region 点击玩家一梅花牌库
        private void clubLeft_Click(object sender, EventArgs e)
        {
            if (whichOne % 2 == 0)
            {
                //判断黑桃牌牌堆是否非空
                if (this.playerOne.getClubPile().Count() != 0)
                {
                    Poker tmp = this.playerOne.getClubPile().Pop();
                    //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                    bool situation = logicCheck(tmp);
                    this.pokerPublished.Push(tmp);
                    this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                    this.cardPublished.Refresh();
                    Thread.Sleep(1000);
                    //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                    //检查是否符合要吃牌的情况
                    if (situation) eatingPoker(tmp);
                }
                else
                {
                    MessageBox.Show("没牌没准是件好事");
                    return; 
                }
                whichOne++;
            }
            else
            {
                MessageBox.Show("嘿怎么能乱动对手的牌，这简直比玛丽阿姨做的蛋糕还要糟糕。");
                return;
            }
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();
        }
        #endregion

        #region 点击玩家一方块牌库
        private void diamondLeft_Click(object sender, EventArgs e)
        {
            if (whichOne % 2 == 0)
            {
                //判断黑桃牌牌堆是否非空
                if (this.playerOne.getDiamondPile().Count() != 0)
                {
                    Poker tmp = this.playerOne.getDiamondPile().Pop();
                    //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                    bool situation = logicCheck(tmp);
                    this.pokerPublished.Push(tmp);
                    this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                    this.cardPublished.Refresh();
                    Thread.Sleep(1000);
                    //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                    //检查是否符合要吃牌的情况
                    if (situation) eatingPoker(tmp);
                }
                else
                {
                    MessageBox.Show("没牌没准是件好事");
                    return;
                }
                whichOne++;
            }
            else
            {
                MessageBox.Show("嘿怎么能乱动对手的牌，这简直比玛丽阿姨做的蛋糕还要糟糕。");
                return;
            }
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();
        }
        #endregion

        #endregion

        #region 点击玩家二牌库应对事件

        #region 点击玩家二黑桃牌库
        private void spadeRight_Click(object sender, EventArgs e)
        {
            if (whichOne%2==1)
            {
                //判断黑桃牌牌堆是否非空
                if (this.playerTwo.getSpadePile().Count() != 0)
                {
                    Poker tmp = this.playerTwo.getSpadePile().Pop();
                    //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                    bool situation = logicCheck(tmp);
                    this.pokerPublished.Push(tmp);
                    this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                    this.cardPublished.Refresh();
                    Thread.Sleep(1000);
                    //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                    //检查是否符合要吃牌的情况
                    if (situation) eatingPoker(tmp);
                }
                else
                {
                    MessageBox.Show("没牌没准是件好事");
                    return;
                }
                whichOne++;
            }
            else
            {
                MessageBox.Show("嘿怎么能乱动对手的牌，这简直比玛丽阿姨做的蛋糕还要糟糕。");
                return;
            }
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg(); 
        }
        #endregion

        #region 点击玩家二红桃牌库
        private void heartRight_Click(object sender, EventArgs e)
        {
            if (whichOne % 1 == 1)
            {
                //判断黑桃牌牌堆是否非空
                if (this.playerTwo.getHeartPile().Count() != 0)
                {
                    Poker tmp = this.playerTwo.getHeartPile().Pop();
                    //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                    bool situation = logicCheck(tmp);
                    this.pokerPublished.Push(tmp);
                    this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                    this.cardPublished.Refresh();
                    Thread.Sleep(1000);
                    //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                    //检查是否符合要吃牌的情况
                    if (situation) eatingPoker(tmp);
                }
                else
                {
                    MessageBox.Show("没牌没准是件好事");
                    return;
                }
                whichOne++;
            }
            else
            {
                MessageBox.Show("嘿怎么能乱动对手的牌，这简直比玛丽阿姨做的蛋糕还要糟糕。");
                return;
            }
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();
        }
        #endregion

        #region 点击玩家二梅花牌库
        private void clubRight_Click(object sender, EventArgs e)
        {
            if (whichOne % 2 == 1)
            {
                //判断黑桃牌牌堆是否非空
                if (this.playerTwo.getClubPile().Count() != 0)
                {
                    Poker tmp = this.playerTwo.getClubPile().Pop();
                    //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                    bool situation = logicCheck(tmp);
                    this.pokerPublished.Push(tmp);
                    this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                    this.cardPublished.Refresh();
                    Thread.Sleep(1000);
                    //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                    //检查是否符合要吃牌的情况
                    if (situation) eatingPoker(tmp);
                }
                else
                {
                    MessageBox.Show("没牌没准是件好事");
                    return;
                }
                whichOne++;
            }
            else
            {
                MessageBox.Show("嘿怎么能乱动对手的牌，这简直比玛丽阿姨做的蛋糕还要糟糕。");
                return;
            }
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();
        }
        #endregion

        #region 点击玩家二方块牌库
        private void diamondRight_Click(object sender, EventArgs e)
        {
            if (whichOne % 2 == 1)
            {
                //判断黑桃牌牌堆是否非空
                if (this.playerTwo.getDiamondPile().Count() != 0)
                {
                    Poker tmp = this.playerTwo.getDiamondPile().Pop();
                    //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                    bool situation = logicCheck(tmp);
                    this.pokerPublished.Push(tmp);
                    this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                    this.cardPublished.Refresh();
                    Thread.Sleep(1000);
                    //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                    //检查是否符合要吃牌的情况
                    if (situation) eatingPoker(tmp);
                }
                else
                {
                    MessageBox.Show("没牌没准是件好事");
                    return;
                }
                whichOne++;
            }
            else
            {
                MessageBox.Show("嘿怎么能乱动对手的牌，这简直比玛丽阿姨做的蛋糕还要糟糕。");
                return;
            }
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();
        }
        #endregion

        #endregion

        #region 重新显示玩家手中卡牌情况
        private void refreshPokerImg()
        {
            if (this.playerOne.getSpadePile().Count != 0)
                this.spadeLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.playerOne.getSpadePile().Peek().getPokerImg());
            else this.spadeLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.playerOne.getHeartPile().Count != 0)
                this.heartLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.playerOne.getHeartPile().Peek().getPokerImg());
            else this.heartLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.playerOne.getClubPile().Count != 0)
                this.clubLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.playerOne.getClubPile().Peek().getPokerImg());
            else this.clubLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.playerOne.getDiamondPile().Count != 0)
                this.diamondLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.playerOne.getDiamondPile().Peek().getPokerImg());
            else this.diamondLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.playerTwo.getSpadePile().Count != 0)
                this.spadeRight.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.playerTwo.getSpadePile().Peek().getPokerImg());
            else this.spadeRight.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.playerTwo.getHeartPile().Count != 0)
                this.heartRight.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.playerTwo.getHeartPile().Peek().getPokerImg());
            else this.heartRight.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.playerTwo.getClubPile().Count != 0)
                this.clubRight.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.playerTwo.getClubPile().Peek().getPokerImg());
            else this.clubRight.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.playerTwo.getDiamondPile().Count != 0)
                this.diamondRight.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.playerTwo.getDiamondPile().Peek().getPokerImg());
            else this.diamondRight.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

        }
        #endregion

        #region 最终结算
        private void finishedMatch()
        {
            int playerOneLeft = this.playerOne.getPokerNum();
            int playerTwoLeft = this.playerTwo.getPokerNum();
            if (playerOneLeft > playerTwoLeft) MessageBox.Show("下边玩家失败，上边玩家胜利");
            else MessageBox.Show("上边玩家失败，下边玩家胜利");
            matchLoop();//提示用户是否重新比赛
        }
        #endregion

        #region 重新来过
        private void matchLoop()
        {
            if(MessageBox.Show("重塑赌神荣光我辈义不容辞？","？",
                MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                disposeResource();//释放运行资源
                initialOperation();//重新比赛
            }
            else
            {
                this.Close();
                this.menu.Show();
            }
        }
        #endregion

        #region 释放运行资源后为重新比赛打下基础
        private void disposeResource()
        {
            this.whichOne = 0;
            for (int i = 0; i < 52; i++)
                this.poker[i].Dispose();
            this.playerOne.Dispose();
            this.playerTwo.Dispose();
            this.pokerPile.Clear();
            this.pokerPublished.Clear();
        }
        #endregion

        #region 重新加载卡堆数目
        private void refreshPokerNum()
        {
            this.cardPileNum.Text = "X" + this.pokerPile.Count();
            this.cardPublishedNum.Text = "X" + this.pokerPublished.Count();
            this.downSpade.Text = "X" + this.playerOne.getSpadePile().Count();
            this.downHeart.Text = "X" + this.playerOne.getHeartPile().Count();
            this.downClub.Text = "X" + this.playerOne.getClubPile().Count();
            this.downDiamond.Text = "X" + this.playerOne.getDiamondPile().Count();
            this.upSpade.Text = "X" + this.playerTwo.getSpadePile().Count();
            this.upHeart.Text = "X" + this.playerTwo.getHeartPile().Count();
            this.upClub.Text = "X" + this.playerTwo.getClubPile().Count();
            this.upDiamond.Text = "X" + this.playerTwo.getDiamondPile().Count();
        }
        #endregion

        private void Local_Mode_Load(object sender, EventArgs e)
        {
            //playSound();由于.wav文件格式过大暂时不考虑播放音乐
            this.pokerPile = new Stack<Poker>();
            this.pokerPublished = new Stack<Poker>();
            this.poker = new Poker[52];
            this.r = new Random();
            initialOperation();
        }

        private void LocalMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.sp.Stop();
            this.Dispose();
            this.Close();
            this.menu.Show();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (whichOne % 2 == 0) this.turnCue.Text = "下边玩家回合!";
            else this.turnCue.Text = "上边玩家回合!";
        }
    }
}
