using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PigTail
{
    public partial class Load : Form
    {
        public Load()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 登录操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Loading_Click(object sender, EventArgs e)
        {
            if (this.account.Text == "" || this.account.Text == "请输入学号:")
            {
                MessageBox.Show("啥，没有学号，难道你是野鸡大学学生？");
                return;
            }
            if(this.passwd.Text == "" || this.passwd.Text == "请输入密码:")
            {
                MessageBox.Show("对着ATM插卡不输入密码还想着取钱，警察叔叔都不愁业绩了。");
                return;
            }
            string rec = FetchInfo.executeOperation("logIn " + account.Text + " " + passwd.Text);
            JObject jo = (JObject)JsonConvert.DeserializeObject(rec);
            if (jo["status"].ToString() == "200") MessageBox.Show("登录成功");
            else
            {
                MessageBox.Show("学号或密码错误!");
                return;
            }
            Menu menu_screen = new Menu();
            menu_screen.StartPosition = FormStartPosition.Manual;
            menu_screen.Location = this.Location;
            this.Hide();           
            menu_screen.ShowDialog();
            this.Dispose();
        }  

        private void account_Leave(object sender, EventArgs e)
        {
            //退出失去焦点，显示提示文本
            if(string.IsNullOrEmpty(account.Text))
            {
                account.ForeColor = Color.FromArgb(200, 200, 200);
                this.account.Text = "请输入学号:";
            }
            //account.ForeColor = Color.FromArgb(200, 200, 200);
        }

        private void account_Enter(object sender,EventArgs e)
        {
            //进入获得焦点，清空提示文本
            if(account.Text == "请输入学号:")
            {
                account.ForeColor = Color.Black;
                this.account.Text = "";
            }
            //account.ForeColor = Color.Black;
        }

        private void passwd_Leave(object sender, EventArgs e)
        {
            //退出失去焦点，显示提示文本
            if(string.IsNullOrEmpty(passwd.Text))
            {
                passwd.ForeColor = Color.FromArgb(200, 200, 200);
                this.passwd.Text = "请输入密码:";
            }
            //passwd.ForeColor = Color.FromArgb(200, 200, 200);
        }

        private void passwd_Enter(object sender, EventArgs e)
        {
            //进入获得焦点，清空提示文本
            if(passwd.Text == "请输入密码:")
            {
                passwd.ForeColor = Color.Black;
                this.passwd.Text = "";
            }
            //passwd.ForeColor = Color.Black;
        }

        private void account_TextChanged(object sender, EventArgs e)
        {

        }

        private void passwd_TextChanged(object sender, EventArgs e)
        {
            if(this.passwd.Text == "" && this.passwd.Text != "请输入密码:")
            {
                this.passwd.PasswordChar = Convert.ToChar("*");
            }
        }

        private void Load_Load(object sender, EventArgs e)
        {
            this.AcceptButton = loading;
        }

        private void showPasswd_CheckedChanged(object sender, EventArgs e)
        {
            if(showPasswd.Checked)
            {
                //复选框被勾选，明文显示
                this.passwd.PasswordChar = new char();
            }
            else
            {
                this.passwd.PasswordChar = '*';
            }
        }
    }
}
