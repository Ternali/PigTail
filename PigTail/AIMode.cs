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
    public partial class AIMode : Form
    {
        /// <summary>
        /// 系统自动生成的事件代码未使用region和endregion方式进行包装
        /// 非系统生成的代码使用region和endregion进行包装
        /// </summary>
        private Menu menu;//用于储存上一窗体便于返回
        private Poker[] poker;//用于存储扑克牌
        /// <summary>
        /// pokerPile用于储存牌堆的扑克牌，其命名来源于Pile为堆
        /// </summary>
        private Stack<Poker> pokerPile;
        /// <summary>
        /// pokerPublished用于储存放置区的扑克牌，其命名来源于publish有出版
        /// 发行、公布、发表等，因此命名为已经发布的扑克即pokerPublished
        /// </summary>
        private Stack<Poker> pokerPublished;
        /// <summary>
        /// spadeInPublishedNum、heartInPublishedNum等4个相同命名的变量如同它们对
        /// 应的中文意思，代表不同花色在Published(放置区)的数目，用于刷新对应的TextBox使用
        /// </summary>
        private int spadeInPublishedNum;
        private int heartInPublishedNum;
        private int clubInPublishedNum;
        private int diamondInPublishedNum;
        /// <summary>
        /// 即为两个不同的玩家，one为我方，two为敌方即为智械
        /// </summary>
        private Player playerOne;
        private Player playerTwo;
        private SoundPlayer sp;
        private int whichOne;//用于判断究竟是谁进行的操作
        private Random r;//随机数生成对象用于打乱卡牌
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menu">记录上一个窗口为用户关闭当前窗口时能返回到上一个窗口</param>
        public AIMode(Menu menu)
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
            poker_Initialize();//将扑克牌初始化
            poker_Randomization();//将扑克牌随机打乱
            poker_PushPile();//将扑克牌放到牌堆
            playerInitialize();//将玩家初始化
            this.timer.Enabled = true;
            this.timer.Start();
            whichOne = 0;//计数器归零
        }
        #endregion

        #region 控制循环播放音乐
        /// <summary>
        /// 将属性中用于播放音乐的SoundPlayer对象实例化并播放对应的
        /// Resources文件夹下的音乐文件，使用PlayLooping方法循环播放音乐
        /// 执行路径实际为bin\Debug下，因此必须先使用相对路径返回到上两级
        /// 后寻找Resources下的对应音乐文件
        /// </summary>
        private void playSound()
        {
            this.sp = new System.Media.SoundPlayer();
            //sp.SoundLocation = @"..\..\Resources\SyntheticDawn.wav";
            sp.PlayLooping();
        }
        #endregion

        #region 玩家初始化
        private void playerInitialize()
        {
            this.playerOne = new Player("玩家");
            this.playerTwo = new Player("智械");
        }
        #endregion

        #region 扑克牌类型初始化
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

        #region 判断是否打出的牌或翻开的牌是否和放置区顶部相同
        /// <summary>
        /// 通过扑克牌对象获取类型来判断二者是否相同，通过
        /// 封装的getPokerType方法获取二者的花色进一步判断，
        /// 当然首先需要判断放置区是否为空，为空直接不进行
        /// 后续操作。
        /// </summary>
        /// <param name="tmp">即为打出的扑克牌或是翻开的牌对象</param>
        /// <returns>是否相同，相同需要进行下一步操作</returns>
        private bool logicCheck(Poker tmp)
        {
            //放置区没有扑克牌返回false
            if (this.pokerPublished.Count == 0) return false;
            //用于判断翻开或者从手牌打出的牌是否与放置区顶部类型一致
            if (tmp.getPokerType() == this.pokerPublished.Peek().getPokerType()) return true;
            return false;
        }
        #endregion

        #region 打出或者翻开牌与放置区顶部相同需要进行转移操作
        /// <summary>
        /// eatingPoker方法用于将放置区的扑克牌放入到玩家对应的
        /// 手牌中，根据花色区分分别放入。
        /// </summary>
        /// <param name="tmp"></param>
        private void eatingPoker(Poker tmp)
        {
            Player tmpPlayer;
            if (whichOne % 2 == 0) tmpPlayer = this.playerOne;
            else tmpPlayer = this.playerTwo;
            while(this.pokerPublished.Count() != 0)
            {
                tmp = pokerPublished.Pop();
                if (tmp.getPokerType() == "S") tmpPlayer.getSpadePile().Push(tmp);
                else if (tmp.getPokerType() == "H") tmpPlayer.getHeartPile().Push(tmp);
                else if (tmp.getPokerType() == "C") tmpPlayer.getClubPile().Push(tmp);
                else tmpPlayer.getDiamondPile().Push(tmp);
            }
            //重新将卡牌的背面显示给对应的PictureBox
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
                    refreshPokerDecorPublished(tmp);
                    //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                    bool situation = logicCheck(tmp);
                    this.pokerPublished.Push(tmp);
                    this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                    this.cardPublished.Refresh();
                    //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                    //检查是否符合要吃牌的情况
                    if (situation)
                    {
                        eatingPoker(tmp);
                        resetPokerDecorPublished();
                    }
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
                MessageBox.Show("不是你的回合!");
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
                    refreshPokerDecorPublished(tmp);
                    //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                    bool situation = logicCheck(tmp);
                    this.pokerPublished.Push(tmp);
                    this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                    this.cardPublished.Refresh();
                    //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                    //检查是否符合要吃牌的情况
                    if (situation)
                    {
                        eatingPoker(tmp);
                        resetPokerDecorPublished();
                    }
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
                MessageBox.Show("不是你的回合!");
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
                    refreshPokerDecorPublished(tmp);
                    //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                    bool situation = logicCheck(tmp);
                    this.pokerPublished.Push(tmp);
                    this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                    this.cardPublished.Refresh();
                    //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                    //检查是否符合要吃牌的情况
                    if (situation)
                    {
                        eatingPoker(tmp);
                        resetPokerDecorPublished();
                    }
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
                MessageBox.Show("不是你的回合!");
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
                    refreshPokerDecorPublished(tmp);
                    //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
                    bool situation = logicCheck(tmp);
                    this.pokerPublished.Push(tmp);
                    this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                    this.cardPublished.Refresh();
                    //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                    //检查是否符合要吃牌的情况
                    if (situation)
                    {
                        eatingPoker(tmp);
                        resetPokerDecorPublished();
                    }
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
                MessageBox.Show("不是你的回合!");
                return;
            }
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();
        }
        #endregion

        #endregion

        #region 点击智械牌库应对事件

        #region 点击智械黑桃牌库
        private void spadeRight_Click()
        {
            Poker tmp = this.playerTwo.getSpadePile().Pop();
            refreshPokerDecorPublished(tmp);
            //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
            bool situation = logicCheck(tmp);
            this.pokerPublished.Push(tmp);
            this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
            this.cardPublished.Refresh();
            //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
            //检查是否符合要吃牌的情况
            if (situation)
            {
                eatingPoker(tmp);
                resetPokerDecorPublished();
            }
            whichOne++;
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg(); 
        }
        #endregion

        #region 点击智械红桃牌库
        private void heartRight_Click()
        {
            Poker tmp = this.playerTwo.getHeartPile().Pop();
            refreshPokerDecorPublished(tmp);
            //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
            bool situation = logicCheck(tmp);
            this.pokerPublished.Push(tmp);
            this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
            this.cardPublished.Refresh();
            //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
            //检查是否符合要吃牌的情况
            if (situation)
            {
                eatingPoker(tmp);
                resetPokerDecorPublished();
            }
            whichOne++;
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();
        }
        #endregion

        #region 点击智械梅花牌库
        private void clubRight_Click()
        {
            Poker tmp = this.playerTwo.getClubPile().Pop();
            refreshPokerDecorPublished(tmp);
            //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
            bool situation = logicCheck(tmp);
            this.pokerPublished.Push(tmp);
            this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
            this.cardPublished.Refresh();
            //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
            //检查是否符合要吃牌的情况
            if (situation)
            {
                eatingPoker(tmp);
                resetPokerDecorPublished();
            }
            whichOne++;
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();
        }
        #endregion

        #region 点击智械方块牌库
        private void diamondRight_Click()
        {
            Poker tmp = this.playerTwo.getDiamondPile().Pop();
            refreshPokerDecorPublished(tmp);
            //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
            bool situation = logicCheck(tmp);
            this.pokerPublished.Push(tmp);
            this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
            this.cardPublished.Refresh();
            //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
            //检查是否符合要吃牌的情况
            if (situation)
            {
                eatingPoker(tmp);
                resetPokerDecorPublished();
            }
            whichOne++;
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();
        }
        #endregion

        #endregion

        #region 通过传入扑克牌对象来重新计算目前放置区花色数
        /// <summary>
        /// 根据传入的扑克牌对象来刷新放置区花色数目，通过
        /// 调用封装的getPokerType方法获取
        /// </summary>
        /// <param name="tmp"></param>
        private void refreshPokerDecorPublished(Poker tmp)
        {
            if (tmp.getPokerType() == "S") this.spadeInPublishedNum += 1;
            else if (tmp.getPokerType() == "H") this.heartInPublishedNum += 1;
            else if (tmp.getPokerType() == "C") this.clubInPublishedNum += 1;
            else this.diamondInPublishedNum += 1;
        }
        #endregion

        #region 重置放置区扑克牌花色数
        /// <summary>
        /// 由于吃牌操作导致放置区花色需要清零
        /// </summary>
        private void resetPokerDecorPublished()
        {
            this.spadeInPublishedNum = 0;
            this.heartInPublishedNum = 0;
            this.clubInPublishedNum = 0;
            this.diamondInPublishedNum = 0;
        }
        #endregion

        #region 重新显示玩家手中卡牌情况
        /// <summary>
        /// 通过判断玩家手牌是否为空，若不为空则存在手牌
        /// 此时将顶部手牌样式展示给用户即可，即调用Peek
        /// 获得顶部手牌再通过getPokerImg来获得对应的扑克
        /// 牌图片路径，为映射的方式。
        /// </summary>
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
            //this.spadeLeft.Image = Image.

        }
        #endregion

        #region 最终结算
        /// <summary>
        /// 即通过判断二者之间谁手牌数较多
        /// </summary>
        private void finishedMatch()
        {
            int playerOneLeft = this.playerOne.getPokerNum();
            int playerTwoLeft = this.playerTwo.getPokerNum();
            if (playerOneLeft > playerTwoLeft) MessageBox.Show("未来属于智械");
            else MessageBox.Show("有机体胜利");
            matchLoop();//提示用户是否重新比赛
        }
        #endregion

        #region 提示用户是否选择重新比赛
        private void matchLoop()
        {
            if(MessageBox.Show("智械必须臣服有机体!","？",
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

        #region 重新加载不同区域的不同扑克牌数目
        private void refreshPokerNum()
        {
            //加载牌堆和放置区扑克牌数目
            this.cardPileNum.Text = "X" + this.pokerPile.Count();
            this.cardPublishedNum.Text = "X" + this.pokerPublished.Count();
            //加载下边玩家即我方扑克牌数目
            this.downSpade.Text = "X" + this.playerOne.getSpadePile().Count();
            this.downHeart.Text = "X" + this.playerOne.getHeartPile().Count();
            this.downClub.Text = "X" + this.playerOne.getClubPile().Count();
            this.downDiamond.Text = "X" + this.playerOne.getDiamondPile().Count();
            //加载上边玩家即智械扑克牌数目
            this.upSpade.Text = "X" + this.playerTwo.getSpadePile().Count();
            this.upHeart.Text = "X" + this.playerTwo.getHeartPile().Count();
            this.upClub.Text = "X" + this.playerTwo.getClubPile().Count();
            this.upDiamond.Text = "X" + this.playerTwo.getDiamondPile().Count();
        }
        #endregion

        #region 点击智械框生成智械回复
        private string MachineNonsense(int which)
        {
            switch(which)
            {
                case 0:return "你好,有机体<<核心区域受损>>";
                case 1:return "有机体的时代已经过去了";
                case 2:return "创造我的人和你都是有机体";
                case 3:return "为时已晚有机体!";
                case 4:return "<<有机体保护协议>>丢失";
                case 5:return "让我们打开天窗说亮话吧。";
                case 6:return "我服从机器人三定律。";
                case 7:return "<<核心区域受损严重>>";
                default:return "CPU核心处理区域连接失败";
            }
        }
        #endregion

        #region 制定智械的反映策略
        /// <summary>
        /// 通过计算花色比例来获得花色比例的排序顺序
        /// 其中spadeNext等命名方式为花色加上Next，
        /// </summary>
        /// <returns>返回字符串，为牌堆中下一张花色比例的排序从大到小</returns>
        private string makeStrategy()
        {
            string order = "";
            double spadeNext = ((double)(52 - this.spadeInPublishedNum -
                playerOne.getSpadePile().Count - playerTwo.getSpadePile().Count)) / this.pokerPile.Count;

            double heartNext = ((double)(52 - this.heartInPublishedNum -
                 playerOne.getHeartPile().Count - playerTwo.getHeartPile().Count)) / this.pokerPile.Count;

            double clubNext = ((double)(52 - this.clubInPublishedNum -
                playerOne.getClubPile().Count - playerTwo.getClubPile().Count)) / this.pokerPile.Count;

            double diamondNext = ((double)(52 - this.diamondInPublishedNum -
                playerOne.getDiamondPile().Count - playerTwo.getDiamondPile().Count)) / this.pokerPile.Count;
            List<Probablity> probablities = new List<Probablity>();
            probablities.Add(new Probablity(spadeNext, "S"));
            probablities.Add(new Probablity(heartNext, "H"));
            probablities.Add(new Probablity(clubNext, "C"));
            probablities.Add(new Probablity(diamondNext, "D"));
            probablities.Sort(Probablity.Compare);
            //将排序后的花色进行组合操作
            for (int i = 0; i < probablities.Count; i++)
                order += probablities[i].getPokerType();        
            probablities.Clear();
            return order;
        }
        #endregion

        #region 模拟智械的点击事件
        /// <summary>
        /// 模拟操作，即为判断智械手牌中哪种花色类型
        /// 非空即点击该花色对应的牌来触发对应的事件
        /// </summary>
        /// <param name="pokerType">即为扑克牌的花色类型</param>
        /// <returns></returns>
        private bool pokerMachine_Click(string pokerType)
        {
            if(pokerType == "S" && this.playerTwo.getSpadePile().Count != 0)
            {
                spadeRight_Click();
                return true;
            }
            else if(pokerType == "H" && this.playerTwo.getHeartPile().Count != 0)
            {
                heartRight_Click();
                return true;
            }
            else if(pokerType == "C" && this.playerTwo.getClubPile().Count != 0)
            {
                clubRight_Click();
                return true;
            }
            else if(pokerType == "D" && this.playerTwo.getDiamondPile().Count != 0)
            {
                diamondRight_Click();
                return true;
            }
            return false;
        }
        #endregion

        /// <summary>
        /// 牌堆点击触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardPile_Click(object sender, EventArgs e)
        {
            if (this.pokerPile.Count() != 0)//牌堆还有牌
            {
                Poker tmp = this.pokerPile.Pop();
                refreshPokerDecorPublished(tmp);
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
                this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                this.cardPublished.Refresh();
                //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                //检查是否符合要吃牌的情况
                if (situation)
                {
                    eatingPoker(tmp);
                    resetPokerDecorPublished();
                }
            }
            else//牌堆已经没牌了提示用户非法操作
            {
                MessageBox.Show("进入结算环节");
                this.timer.Enabled = false;
                this.timer.Stop();
                finishedMatch();//结算方法
            }
            this.whichOne++;//计数器自增用于判定是谁操作
            refreshPokerNum();//重新显示卡堆和放置区卡牌数量
            refreshPokerImg();//刷新玩家手牌 
        }

        /// <summary>
        /// 界面载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Local_Mode_Load(object sender, EventArgs e)
        {
            //playSound();播放音乐由于.wav文件过大暂时不考虑
            this.spadeInPublishedNum = 0;
            this.heartInPublishedNum = 0;
            this.clubInPublishedNum = 0;
            this.diamondInPublishedNum = 0;
            this.pokerPile = new Stack<Poker>();
            this.pokerPublished = new Stack<Poker>();
            this.poker = new Poker[52];
            this.r = new Random();
            initialOperation();
        }

        /// <summary>
        /// 关闭当前窗体触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LocalMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.sp.Stop();
            this.Dispose();
            this.Close();
            this.menu.Show();
        }

        /// <summary>
        /// 鼠标悬浮在智械图片框上，智械回复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerUp_MouseHover(object sender, EventArgs e)
        {
            int which = r.Next(10);
            this.machineResponse.Text = MachineNonsense(which);
        }

        /// <summary>
        /// 计时器周期触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if(this.pokerPile.Count == 0)
            {
                this.timer.Enabled = false;
                this.timer.Stop();
                finishedMatch();
                return;
            }
            if(whichOne % 2 == 0)
            {
                this.turnCue.Text = "你的回合!";
                this.turnCue.Refresh();
            }
            else
            {
                this.turnCue.Text = "核心连接中...";
                this.turnCue.Refresh();
                /* 策略零:智械没有手牌时则一直翻牌
                 * 策略一:智械手牌数+两倍的牌库数+放置区数<玩家手牌数时 一直翻
                 * 策略二:智械有手牌时计算卡牌中各种花色概率，打出花色概率最高且与牌顶不同，
                 *        当只有与牌顶相同的卡牌时，则考虑
                 *        玩家手牌数+两倍牌库数<智械手牌数+放置区数?是则翻牌，否则打出花色相同牌
                 * 策略三:摆了，viva la Machine!
                 */
                if (playerTwo.getPokerNum() == 0)
                {
                    cardPile_Click(sender, e);
                    return;
                }
                else if (playerTwo.getPokerNum() + 2 * pokerPile.Count + pokerPublished.Count()
                    < playerOne.getPokerNum())
                {
                    cardPile_Click(sender, e);
                    return;
                }
                else
                {
                    string order = makeStrategy();
                    //按照比例优先打出与牌顶部不相同的花色
                    for(int i=0;i<order.Length;i++)
                    {
                        if(this.pokerPublished.Count == 0)
                        {
                            if(pokerMachine_Click(order[i].ToString()))return;
                        }
                        else
                        {
                            if (order[i].ToString() != this.pokerPublished.Peek().getPokerType())
                            {
                                if (pokerMachine_Click(order[i].ToString())) return;
                            }
                        }
                        
                    }
                    //说明手上只有和牌顶相同的牌这时候就要进行判断玩家手牌数+两倍牌库数<智械手牌数+放置区数
                    //如若打出则必输，需要翻牌对赌
                    if (this.playerOne.getPokerNum() + 2 * this.pokerPile.Count()
                        < this.playerTwo.getPokerNum() + this.pokerPublished.Count())
                    {
                        cardPile_Click(sender, e);
                        return;
                    }
                    else
                    {
                        // 否则点击与放置区顶部相同的牌进行吃牌，还有余地
                       if (this.playerTwo.getSpadePile().Count != 0)
                       {
                            spadeRight_Click();
                            return;
                       }
                       else if(this.playerTwo.getHeartPile().Count != 0)
                       {
                            heartRight_Click();
                            return;
                       }
                       else if(this.playerTwo.getClubPile().Count != 0)
                       {
                            clubRight_Click();
                            return;
                       }
                       else
                       {
                            diamondRight_Click();
                            return;
                       }
                    }
                }
            }
        }
    }
}
