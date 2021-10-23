using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Windows.Forms;

namespace PigTail
{
    public partial class InternetMenu : Form
    {
        private Menu menu;
        public InternetMenu(Menu menu)
        {
            InitializeComponent();
            this.menu = menu;
        }

        /// <summary>
        /// 创建对局按钮触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createContest_Click(object sender, EventArgs e)
        {
            string rec = null;
            JObject jo = null;
            DialogResult dr = MessageBox.Show("是否创建私人对局？", "选择对局类型",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                rec = FetchInfo.executeOperation("createContest True");
                jo = (JObject)JsonConvert.DeserializeObject(rec);
                if (jo["code"].ToString() == "200")
                {
                    MessageBox.Show("创建成功，uuid:" + jo["data"]["uuid"] + "拉上小伙伴一起来玩耍吧！");
                }
                else
                {
                    MessageBox.Show("网络断开连接或是账户失效了");
                    return;
                }
            }
            else if (dr == DialogResult.No)
            {
                rec = FetchInfo.executeOperation("createContest True");
                jo = (JObject)JsonConvert.DeserializeObject(rec);
                if (jo["code"].ToString() == "200")
                {
                    MessageBox.Show("创建公开对局成功，您的uuid:" + jo["data"]["uuid"]);
                }
                else
                {
                    MessageBox.Show("网络断开连接或是账户失效了");
                    return;
                }
            }
            InterMode internet_mode = new InterMode(jo["data"]["uuid"].ToString(), this, true);
            this.Hide();
            internet_mode.StartPosition = FormStartPosition.Manual;
            internet_mode.Location = this.Location;
            internet_mode.ShowDialog();
        }

        /// <summary>
        /// 加入对局触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void joinContest_Click(object sender, EventArgs e)
        {
            string uuid = Interaction.InputBox("请输入uuid", "加入对局");
            if (uuid == "") return;
            else
            {
                string rec = FetchInfo.executeOperation("joinContest " + uuid);
                JObject jo = (JObject)JsonConvert.DeserializeObject(rec);
                if (jo["code"].ToString() == "200")
                {
                    MessageBox.Show("操作成功");
                    InterMode internet_mode = new InterMode(uuid, this, false);
                    this.Hide();
                    internet_mode.StartPosition = FormStartPosition.Manual;
                    internet_mode.Location = this.Location;
                    internet_mode.ShowDialog();
                }
                else if (jo["code"].ToString() == "404")
                {
                    MessageBox.Show("uuid不存在哦");
                }
                else MessageBox.Show("还是别当电灯泡了");
                return;
            }
        }

        /// <summary>
        /// 获取公开的对局uuid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fetchPublicContest_Click(object sender, EventArgs e)
        {
            Worm worm = new Worm();
            worm.StartPosition = FormStartPosition.CenterParent;
            worm.ShowDialog();
            worm.Dispose();
        }

        private void InternetMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            this.Close();
            this.menu.Show();
        }
    }
}
