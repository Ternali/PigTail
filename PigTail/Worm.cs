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
    public partial class  Worm: Form
    {
        public Worm()
        {
            InitializeComponent();
        }

        private void uuidBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.uuidBox.ReadOnly = true;
        }

        /// <summary>
        /// 获取随机的uuid，由于技术能力限制仅能对返回的uuid列表采用
        /// 搭配随机数的方式随机获取uuid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void random_Click(object sender, EventArgs e)
        {
            string rec = FetchInfo.executeOperation("fetchPublicContest");
            JObject jo = (JObject)JsonConvert.DeserializeObject(rec);
            if(jo["code"].ToString() == "200")
            {
                MessageBox.Show("操作成功");
                JArray items = (JArray)jo["data"]["games"];
                Random r = new Random();
                int which = r.Next(0, items.Count);
                this.uuidBox.Text = ((JObject)items[which])["uuid"].ToString();
            }
            else
            {
                MessageBox.Show("登录失效了，请重新登录");
            }
        }
    }
}
