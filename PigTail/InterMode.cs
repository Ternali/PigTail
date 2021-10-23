using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class InterMode : Form
    {
        /// <summary>
        /// 系统自动生成的事件代码未使用region和endregion方式进行包装
        /// 非系统生成的代码使用region和endregion进行包装
        /// </summary>
        private InternetMenu internetMenu;//用于储存上一窗体便于返回
        private bool host;
        private SoundPlayer sp;//用于播放音乐的sp对象
        private bool matchBegin;//用于判断比赛是否开始的标志
        private bool matchEnd;//用于判断比赛是否结束的标志，主要用于最后结束后告知结果弹窗循环的问题
        private bool yourTurn;//由于判断是否为自己的回合以便在非自己回合时触发事件提醒用户
        private string uuid;//用于获取后续操作的uuid
        /// <summary>
        /// freshFlag用于判断是否需要刷新界面，监听器不断监听仅需刷新一次即可
        /// 因此需要对比获得的操作，存储内容为获得的上一步操作即last_msg
        /// </summary>
        private string freshFlag;
        /// <summary>
        /// pokerPublished用于储存放置区的扑克牌，其命名来源于publish有出版
        /// 发行、公布、发表等，因此命名为已经发布的扑克即pokerPublished
        /// </summary>
        private Stack<Poker> pokerPublished;
        /// <summary>
        /// pokerPileNum用于存储当前还在牌库未抽取的扑克牌数目，用于后续刷新
        /// 对应的TextBox显示计数使用，初始化为52
        /// </summary>
        private int pokerPileNum;
        /// <summary>
        /// pokerPublishedNum用于存储当前在放置区的扑克牌数目，用于后续刷新
        /// 对应的TextBox显示计数使用，初始化为0
        /// </summary>
        private int pokerPublishedNum;
        /// <summary>
        /// Steve、Norch分别为两个玩家类，命名来源于Minecraft中Steve主角和Norch
        /// 创始人
        /// </summary>
        private Player Steve;
        private Player Norch;
        /// <summary>
        /// spadeInPublishedNum、heartInPublishedNum等4个相同命名的变量如同它们对
        /// 应的中文意思，代表不同花色在Published(放置区)的数目，用于刷新对应的TextBox使用
        /// </summary>
        private int spadeInPublishedNum;
        private int heartInPublishedNum;
        private int clubInPublishedNum;
        private int diamondInPublishedNum;
        /// <summary>
        /// 用于判断用户是否勾选对应的启用托管功能，以便算法能正常运行
        /// 命名来源于群星的Crisis事件天灾，不过目前看不够智能仅仅是算法流程
        /// </summary>
        private bool machineCrisis;

        /// <summary>
        /// 用于对设定属性的初始化操作
        /// </summary>
        /// <param name="uuid">记录比赛的uuid为后续执行一系列操作需要</param>
        /// <param name="internetMenu">记录上一个窗口为用户关闭当前窗口时能返回到上一个窗口</param>
        /// <param name="host">用于判断是否主场，目前无使用途径后续会删除</param>
        public InterMode(string uuid, InternetMenu internetMenu, bool host)
        {
            InitializeComponent();
            //this.machineCrisis = true;用于测试是否托管是否正常
            this.host = host;
            this.internetMenu = internetMenu;
            this.pokerPileNum = 52;
            this.pokerPublishedNum = 0;
            this.pokerPublished = new Stack<Poker>();
            this.uuid = uuid;
            this.uuidBox.Text = "UUID:" + uuid;
            this.freshFlag = "";//初始化为空，后续根据返回的last_msg进行更新
            this.matchBegin = false;//表明人数还没满
            this.matchEnd = false;//表明还未结束
            this.timer = new System.Windows.Forms.Timer();//创建计时器对象用于保证操作同步性
            this.timer.Enabled = true;
            this.timer.Interval = 2000;//设置请求的时间每3秒一次
            this.Steve = new Player("Steve");
            this.Norch = new Player("Norch");
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
            this.spadePublishedNum.Text = "♠X0";
            this.heartPublishedNum.Text = "♥X0";
            this.clubPublishedNum.Text = "♣X0";
            this.diamondPublishedNum.Text = "♦X0";
            this.spadeInPublishedNum = 0;
            this.heartInPublishedNum = 0;
            this.clubInPublishedNum = 0;
            this.diamondInPublishedNum = 0;
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
            this.sp = new SoundPlayer();
            sp.SoundLocation = @"..\..\Resources\FTL.wav";
            sp.PlayLooping();
        }
        #endregion

        #region 解析字符串获得扑克牌对象
        /// <summary>
        /// 将传入的字符串进行解析获得对应的扑克牌对象，传入的字符串为调用
        /// 获取上一步操作的接口返回的值中的last_code的末尾的卡牌值，例如
        /// last_code: 0 0 C8，此时仅传入C8进行解析
        /// </summary>
        /// <param name="pokerValue">传入的字符串内容</param>
        /// <returns>返回扑克牌对象</returns>
        private Poker transformPoker(string pokerValue)
        {
            string pokerType = pokerValue[0].ToString();//获取扑克牌对应花色，即C8中的C，对应为Club
            string pokerNumber;//获取扑克牌对应数字大小
            if (pokerValue.Length == 3) pokerNumber = "10";//例如C10，则数字为10
            else
            {
                //当数值在1~9时可以直接赋值
                if (pokerValue[1] <= '9' && pokerValue[1] >= '1') pokerNumber = pokerValue[1].ToString();
                else
                {
                    //由于素材命名映射的问题，需要将J、Q、K转换为对应的数字
                    if (pokerValue[1] == 'J') pokerNumber = "11";
                    else if (pokerValue[1] == 'Q') pokerNumber = "12";
                    else pokerNumber = "13";
                }
            }
            return new Poker(pokerType, pokerNumber);
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
            if (yourTurn) tmpPlayer = this.Steve;
            else tmpPlayer = this.Norch;
            while (this.pokerPublished.Count() != 0)
            {
                tmp = pokerPublished.Pop();
                if (tmp.getPokerType() == "S") tmpPlayer.getSpadePile().Push(tmp);
                else if (tmp.getPokerType() == "H") tmpPlayer.getHeartPile().Push(tmp);
                else if (tmp.getPokerType() == "C") tmpPlayer.getClubPile().Push(tmp);
                else tmpPlayer.getDiamondPile().Push(tmp);
            }
            //重新将卡牌的背面显示给放置区对应的PictureBox
            this.cardPublished.Image = Image.FromFile(@"..\..\Resources\pigtail.png");
            this.cardPublished.Refresh();
        }
        #endregion

        #region 根据传入的扑克牌以及类型刷新当前对局情况
        /// <summary>
        /// refreshContest为刷新对局情况，根据传入的扑克牌类型级last_code的C8
        /// 以及扑克牌流入放置区的方式来进行操作，0为翻牌，1为打出
        /// 其中需要注意的是，how为0时需要将牌库数自减即pokerPileNum--
        /// 而how为0、1放置区数总是先自增
        /// </summary>
        /// <param name="pokerValue">对应的扑克牌类型</param>
        /// <param name="how">将扑克牌传入放置区的方式，0为翻牌，1为打出</param>
        private void refreshContest(string pokerValue, int how)
        {
            if (how == 0)//说明进行从卡牌堆中翻牌操作
            {
                this.pokerPileNum--;
                this.pokerPublishedNum++;
            }
            else this.pokerPublishedNum++;//说明从玩家手牌中进行翻牌操作
            Poker tmp = transformPoker(pokerValue);//获取字符串C8对应的扑克牌对象
            refreshPokerDecorPublished(tmp);//通过传入放到放置区的扑克牌来刷新放置区花色数比例
            //替换牌堆的牌让用户看到翻到了什么类型的牌
            if(how == 0)
            {
                this.cardPile.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
                this.cardPile.Refresh();
                //让当前线程睡眠避免替换牌过快用户无法看到翻到什么类型的牌，基本动画阉割版
                Thread.Sleep(1000);
                //重新将卡牌的背面显示给牌堆
                this.cardPile.Image = Image.FromFile(@"..\..\Resources\pigtail.png");
                this.cardPile.Refresh();
            }          
            //将翻到的卡牌丢到亮相的牌堆里面并展示之前先检测后丢到牌堆
            bool situation = logicCheck(tmp);
            this.pokerPublished.Push(tmp);
            //刷新放置区图片表明将扑克牌进入到放置区
            this.cardPublished.Image = Image.FromFile(@"..\..\Resources\" + tmp.getPokerImg());
            this.cardPublished.Refresh();
            //检查是否符合要吃牌的情况，如果形势正确(situation==true)则执行相应的操作
            if (situation)
            {
                eatingPoker(tmp);
                resetPokerDecorPublished();//放置区牌对应花色数需要清空
                this.pokerPublishedNum = 0;//放置区牌需要清空所以数量为0
            }
        }
        #endregion

        #region 通过传入扑克牌对象来刷新目前放置区的花色数
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
            if (this.Steve.getSpadePile().Count != 0)
                this.spadeLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.Steve.getSpadePile().Peek().getPokerImg());
            else this.spadeLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.Steve.getHeartPile().Count != 0)
                this.heartLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.Steve.getHeartPile().Peek().getPokerImg());
            else this.heartLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.Steve.getClubPile().Count != 0)
                this.clubLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.Steve.getClubPile().Peek().getPokerImg());
            else this.clubLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.Steve.getDiamondPile().Count != 0)
                this.diamondLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.Steve.getDiamondPile().Peek().getPokerImg());
            else this.diamondLeft.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.Norch.getSpadePile().Count != 0)
                this.spadeRight.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.Norch.getSpadePile().Peek().getPokerImg());
            else this.spadeRight.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.Norch.getHeartPile().Count != 0)
                this.heartRight.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.Norch.getHeartPile().Peek().getPokerImg());
            else this.heartRight.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.Norch.getClubPile().Count != 0)
                this.clubRight.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.Norch.getClubPile().Peek().getPokerImg());
            else this.clubRight.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");

            if (this.Norch.getDiamondPile().Count != 0)
                this.diamondRight.BackgroundImage = Image.FromFile(@"..\..\Resources\" + this.Norch.getDiamondPile().Peek().getPokerImg());
            else this.diamondRight.BackgroundImage = Image.FromFile(@"..\..\Resources\white.png");
        }
        #endregion

        #region 重新加载不同区域的扑克牌数目
        /// <summary>
        /// 硬核刷新，通过对TextBox的Text内容进行修
        /// 改来刷新扑克牌数目，以及花色对应的数目，
        /// 其中down表示下边玩家即我方，up表示上边
        /// 玩家即敌方。
        /// </summary>
        private void refreshPokerNum()
        {
            //对放置区和牌库的扑克牌数进行刷新操作
            this.cardPublishedNum.Text = "X" + this.pokerPublished.Count();
            this.cardPileNum.Text = "X" + Convert.ToString(this.pokerPileNum);
            //对下边玩家手中的不同花色扑克牌数刷新操作
            this.downSpade.Text = "X" + this.Steve.getSpadePile().Count();
            this.downHeart.Text = "X" + this.Steve.getHeartPile().Count();
            this.downClub.Text = "X" + this.Steve.getClubPile().Count();
            this.downDiamond.Text = "X" + this.Steve.getDiamondPile().Count();
            //对上边玩家手中的不同花色扑克牌数刷新操作
            this.upSpade.Text = "X" + this.Norch.getSpadePile().Count();
            this.upHeart.Text = "X" + this.Norch.getHeartPile().Count();
            this.upClub.Text = "X" + this.Norch.getClubPile().Count();
            this.upDiamond.Text = "X" + this.Norch.getDiamondPile().Count();
            //对放置区不同花色扑克牌数进行刷新操作
            this.spadePublishedNum.Text = "♠X" + this.spadeInPublishedNum.ToString();
            this.heartPublishedNum.Text = "♥X" + this.heartInPublishedNum.ToString();
            this.clubPublishedNum.Text = "♣X" + this.clubInPublishedNum.ToString();
            this.diamondPublishedNum.Text = "♦X" + this.diamondInPublishedNum.ToString();

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
            double spadeNext = ((double)(52 - this.spadeInPublishedNum - Steve.getSpadePile().Count - Norch.getSpadePile().Count)) / this.pokerPileNum;

            double heartNext = ((double)(52 - this.heartInPublishedNum - Steve.getHeartPile().Count - Norch.getHeartPile().Count)) / this.pokerPileNum;

            double clubNext = ((double)(52 - this.clubInPublishedNum - Steve.getClubPile().Count - Norch.getClubPile().Count)) / this.pokerPileNum;

            double diamondNext = ((double)(52 - this.diamondInPublishedNum - Steve.getDiamondPile().Count - Norch.getDiamondPile().Count)) / this.pokerPileNum;
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
        /// 其中sender和e为模仿winform事件自动生成
        /// 时的参数非必须但加上后能更好说明这是个
        /// 模拟操作，即为判断智械手牌中哪种花色类型
        /// 非空即点击该花色对应的牌来触发对应的事件
        /// </summary>
        /// <param name="pokerType">即为扑克牌的花色类型</param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool pokerMachine_Click(string pokerType,object sender, EventArgs e)
        {
            if (pokerType == "S" && this.Steve.getSpadePile().Count != 0)
            {
                spadeLeft_Click(sender, e);
                return true;
            }
            else if (pokerType == "H" && this.Steve.getHeartPile().Count != 0)
            {
                heartLeft_Click(sender, e);
                return true;
            }
            else if (pokerType == "C" && this.Steve.getClubPile().Count != 0)
            {
                clubLeft_Click(sender, e);
                return true;
            }
            else if (pokerType == "D" && this.Steve.getDiamondPile().Count != 0)
            {
                diamondLeft_Click(sender, e);
                return true;
            }
            return false;
        }
        #endregion

        #region 智械操作过程即托管的执行如何操作
        /// <summary>
        /// 其中描述了智械的策略(为何将托管美名其曰
        /// 智械，这次我采用的所有素材来源于对Stellaris
        /// 和SpaceEnginee游戏的加工，智械来源于Stellaris
        /// 虽然达不到对应的智力。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void machineOperation(object sender, EventArgs e)
        {
            /* 策略零:智械没有手牌时则一直翻牌
                 * 策略一:智械手牌数+两倍的牌库数+放置区数<玩家手牌数时 一直翻
                 * 策略二:智械有手牌时计算卡牌中各种花色概率，优先打出花色概率最高且与牌顶不同，
                 *        当只有与牌顶相同的卡牌时，则考虑
                 *        玩家手牌数+两倍牌库数<智械手牌数+放置区数?是则翻牌，否则打出花色相同牌
                 * 策略三:摆了，viva la Machine!
                 */
            if (Steve.getPokerNum() == 0)
            {
                cardPile_Click(sender, e);
                return;
            }
            else if (Steve.getPokerNum() + 2 * pokerPileNum + pokerPublished.Count()
                < Norch.getPokerNum())
            {
                cardPile_Click(sender, e);
                return;
            }
            else
            {
                //用于获取花色比例排序
                string order = makeStrategy();
                //按照比例顺序优先打出与放置区顶部不同花色的牌
                for (int i = 0; i < order.Length; i++)
                {
                    //放置区没有牌的情况
                    if (this.pokerPublished.Count == 0)
                    {
                        //判断是否有无对应的卡牌
                        if (pokerMachine_Click(order[i].ToString(), sender, e)) return;
                    }
                    //放置区有牌的情况
                    else
                    {
                        if (order[i].ToString() != this.pokerPublished.Peek().getPokerType())
                        {
                            if (pokerMachine_Click(order[i].ToString(), sender, e)) return;
                        }
                    }
                }
                //说明手上只有和牌顶相同的牌这时候就要进行判断玩家手牌数+两倍牌库数<智械手牌数+放置区数
                //如若打出则必输，需要翻牌对赌
                if (this.Norch.getPokerNum() + 2 * this.pokerPileNum
                    < this.Steve.getPokerNum() + this.pokerPublished.Count())
                {
                    cardPile_Click(sender, e);
                    return;
                }
                //否则点击与放置区顶部相同的牌进行吃牌，还有余地
                else
                {
                    if (this.Steve.getSpadePile().Count != 0)
                    {
                        spadeLeft_Click(sender, e);
                        return;
                    }
                    else if (this.Steve.getHeartPile().Count != 0)
                    {
                        heartLeft_Click(sender, e);
                        return;
                    }
                    else if (this.Steve.getClubPile().Count != 0)
                    {
                        clubLeft_Click(sender, e);
                        return;
                    }
                    else
                    {
                        diamondLeft_Click(sender, e);
                        return;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 计时器事件，turnCue，Cue为信号暗示线索的意思，即为提示回合
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, System.EventArgs e)
        {
            string rec;
            JObject jo;
            //rec用于获取调用接口返回的内容并转换成JObject对象
            rec = FetchInfo.executeOperation("getPreviousOperation " + this.uuid);
            jo = (JObject)JsonConvert.DeserializeObject(rec);
            if (jo["code"].ToString() == "403")
            {
                //对局尚未开始
                matchBegin = false;
                return;
            }
            else 
            {
                if (jo["code"].ToString() == "200")//对局开始
                {
                    matchBegin = true;
                    //用于判断是否我方回合
                    if (jo["data"]["your_turn"].ToString() == "False")
                    {
                        //通过freshFlag和last_msg进行比较判断是否需要刷新界面
                        if (jo["data"]["last_msg"].ToString() != this.freshFlag)
                        {
                            //对方回合说明上回为我方回合无需进行刷新，因为我方执行
                            //对应操作后会刷新
                            this.freshFlag = jo["data"]["last_msg"].ToString();
                            this.turnCue.Text = "对方抉择中!";
                            this.turnCue.Refresh();
                        }
                        yourTurn = false;//当前为对方回合
                    }
                    else
                    {
                        if (jo["data"]["last_code"].ToString() == "")//回合刚开始且为我方回合
                        {
                            yourTurn = true;
                            this.turnCue.Text = "你的回合!";
                            this.turnCue.Refresh();
                            if (machineCrisis) machineOperation(sender, e);//如果为托管(智械)则进行操作
                            return;
                        }
                        //目前是我方回合，说明对方操作结束需要刷新界面，需要判断是否已经刷新
                        string[] results = jo["data"]["last_code"].ToString().Split(' ');
                        //last_code格式为0 0 C8，玩家 出牌方式 扑克类型
                        char how = Convert.ToChar(results[1]);//获取出牌方式
                        if (jo["data"]["last_msg"].ToString() != this.freshFlag)
                        {
                            //当碰到"烧条"玩家，多次监听返回值相同仅需刷新一次
                            this.freshFlag = jo["data"]["last_msg"].ToString();
                            if (how == '1')
                            {
                                if (results[2][0] == 'S')Norch.getSpadePile().Pop();
                                else if (results[2][0] == 'H')Norch.getHeartPile().Pop();
                                else if (results[2][0] == 'C')Norch.getClubPile().Pop();
                                else Norch.getDiamondPile().Pop();
                            }
                            //刷新界面操作
                            refreshContest(results[2], how - '0');
                            //刷新不同区域的扑克牌数目
                            refreshPokerNum();
                            //刷新不同区域的扑克牌图片
                            refreshPokerImg();
                            this.turnCue.Text = "你的回合!";
                            this.turnCue.Refresh();
                        }
                        yourTurn = true;
                        //如果为托管(智械)，刷新后自动操作
                        if (machineCrisis) machineOperation(sender, e);
                        return;
                    }
                }
                else if (jo["code"].ToString() == "400")//对局已经结束
                {
                    this.timer.Enabled = false;
                    this.timer.Stop();//停止监听                   
                    rec = FetchInfo.executeOperation("fetchContestInfo " + this.uuid);
                    jo = (JObject)JsonConvert.DeserializeObject(rec);
                    if(!this.matchEnd)
                    {
                        string ending = "";
                        if (jo["data"]["winner"].ToString() == "0" && this.host)
                            ending = "恭喜你，你获胜了。";
                        else if(jo["data"]["winner"].ToString() == "0" && !this.host)
                        {
                            ending = "胜败乃兵家常事很正常的啦。";
                        }
                        else if(jo["data"]["winner"].ToString() == "1" && !this.host)
                        {
                            ending = "恭喜你，你获胜了。";
                        }
                        else
                        {
                            ending = "胜败乃兵家常事很正常的啦。";
                        }
                        MessageBox.Show(ending);
                        matchEnd = true;
                    }
                    this.Dispose();
                    this.Close();//关闭当前窗体
                    this.internetMenu.Show();//显示上一个窗体
                }
            }
        }

        /// <summary>
        /// 牌堆点击触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardPile_Click(object sender, EventArgs e)
        {
            string rec;
            JObject jo;
            if (matchBegin == false)
            {
                this.turnCue.Text = "人还没齐哦~";
                this.turnCue.Refresh();
                return;
            }
            else
            {
                if (yourTurn == false)
                {
                    this.turnCue.Text = "不是你的回合哦~";
                    this.turnCue.Refresh();
                    return;
                }
                else
                {
                    rec = FetchInfo.executeOperation("executeOperation " + this.uuid + " 0");
                    jo = (JObject)JsonConvert.DeserializeObject(rec);
                    if (jo["code"].ToString() == "200")//操作成功
                    {
                        string[] results = jo["data"]["last_code"].ToString().Split(' ');
                        refreshContest(results[2], 0);
                    }
                    yourTurn = false;
                }
            }
            refreshPokerNum();
            refreshPokerImg();
        }

        /// <summary>
        /// 点击下边黑桃触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void spadeLeft_Click(object sender, EventArgs e)
        {
            string rec;
            JObject jo;
            if (matchBegin == false)
            {
                this.turnCue.Text = "人还没齐哦~";
                this.turnCue.Refresh();
                return;
            }
            else
            {
                if (yourTurn == false)
                {
                    this.turnCue.Text = "不是你的回合哦~";
                    this.turnCue.Refresh();
                    return;
                }
                else
                {
                    if (this.Steve.getSpadePile().Count == 0)
                    {
                        MessageBox.Show("没准没牌是件好事情");
                        return;
                    }
                    Poker tmp = this.Steve.getSpadePile().Pop();
                    //通过调用方法转换成请求的扑克牌类型，即转换为C8
                    string pokerInfo = tmp.getPokerTypeInternet();
                    rec = FetchInfo.executeOperation("executeOperation " + this.uuid + " 1 " + pokerInfo);
                    jo = (JObject)JsonConvert.DeserializeObject(rec);
                    if (jo["code"].ToString() == "200")//操作成功
                    {
                        string[] results = jo["data"]["last_code"].ToString().Split(' ');
                        refreshContest(results[2], 1);
                    }
                    yourTurn = false;
                }
            }
            refreshPokerNum();
            refreshPokerImg();
        }

        /// <summary>
        /// 点击下边红桃触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void heartLeft_Click(object sender, EventArgs e)
        {
            string rec;
            JObject jo;
            if (matchBegin == false)
            {
                this.turnCue.Text = "人还没齐哦~";
                this.turnCue.Refresh();
                return;
            }
            else
            {
                if (yourTurn == false)
                {
                    this.turnCue.Text = "不是你的回合哦~";
                    this.turnCue.Refresh();
                    return;
                }
                else
                {
                    if (this.Steve.getHeartPile().Count == 0)
                    {
                        MessageBox.Show("没准没牌是件好事情");
                        return;
                    }
                    Poker tmp = this.Steve.getHeartPile().Pop();
                    //同样通过转换成C8为后续使用接口服务
                    string pokerInfo = tmp.getPokerTypeInternet();
                    rec = FetchInfo.executeOperation("executeOperation " + this.uuid + " 1 " + pokerInfo);
                    jo = (JObject)JsonConvert.DeserializeObject(rec);
                    if (jo["code"].ToString() == "200")//操作成功
                    {
                        string[] results = jo["data"]["last_code"].ToString().Split(' ');
                        refreshContest(results[2], 1);
                    }
                    yourTurn = false;
                }
            }
            refreshPokerNum();
            refreshPokerImg();
        }

        /// <summary>
        /// 点击下边梅花触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clubLeft_Click(object sender, EventArgs e)
        {
            string rec;
            JObject jo;
            if (matchBegin == false)
            {
                this.turnCue.Text = "人还没齐哦~";
                this.turnCue.Refresh();
                return;
            }
            else
            {
                if (yourTurn == false)
                {
                    this.turnCue.Text = "不是你的回合哦~";
                    this.turnCue.Refresh();
                    return;
                }
                else
                {
                    if (this.Steve.getClubPile().Count == 0)
                    {
                        MessageBox.Show("没准没牌是件好事情");
                        return;
                    }
                    Poker tmp = this.Steve.getClubPile().Pop();
                    string pokerInfo = tmp.getPokerTypeInternet();
                    rec = FetchInfo.executeOperation("executeOperation " + this.uuid + " 1 " + pokerInfo);
                    jo = (JObject)JsonConvert.DeserializeObject(rec);
                    if (jo["code"].ToString() == "200")//操作成功
                    {
                        string[] results = jo["data"]["last_code"].ToString().Split(' ');
                        refreshContest(results[2], 1);
                    }
                    yourTurn = false;
                }
            }
            refreshPokerNum();
            refreshPokerImg();
        }

        /// <summary>
        /// 点击下边方块触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void diamondLeft_Click(object sender, EventArgs e)
        {
            string rec;
            JObject jo;
            if (matchBegin == false)
            {
                this.turnCue.Text = "人还没齐哦~";
                this.turnCue.Refresh();
                return;
            }
            else
            {
                if (yourTurn == false)
                {
                    this.turnCue.Text = "不是你的回合哦~";
                    this.turnCue.Refresh();
                    return;
                }
                else
                {
                    if (this.Steve.getDiamondPile().Count == 0)
                    {
                        MessageBox.Show("没准没牌是件好事情");
                        return;
                    }
                    Poker tmp = this.Steve.getDiamondPile().Pop();
                    string pokerInfo = tmp.getPokerTypeInternet();
                    rec = FetchInfo.executeOperation("executeOperation " + this.uuid + " 1 " + pokerInfo);
                    jo = (JObject)JsonConvert.DeserializeObject(rec);
                    if (jo["code"].ToString() == "200")//操作成功
                    {
                        string[] results = jo["data"]["last_code"].ToString().Split(' ');
                        refreshContest(results[2], 1);
                    }
                    yourTurn = false;
                }
            }
            refreshPokerNum();
            refreshPokerImg();
        }

        private void Inter_Mode_Load(object sender, EventArgs e)
        {
            //playSound();由于.wav格式音乐文件过大暂时不考虑播放音乐
            this.timer.Start();
            initialOperation();
        }

        private void InterMode_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.sp.Stop();
            this.Dispose();
            this.Close();
            this.internetMenu.Show();
        }

        private void machineFuture_CheckedChanged(object sender, EventArgs e)
        {
            if(machineFuture.Checked)
            {
                this.machineCrisis = true;
                this.playerDown.Image = Image.FromFile(@"..\..\Resources\MachineCrisis.png");
                this.playerDown.Refresh();
            }
            else
            {
                this.machineCrisis = false;
                this.playerDown.Image = Image.FromFile(@"..\..\Resources\Humanity.png");
                this.playerDown.Refresh();
            }
        }
    }
}
