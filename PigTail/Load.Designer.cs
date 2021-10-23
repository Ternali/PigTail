
namespace PigTail
{
    partial class Load
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Load));
            this.loading = new System.Windows.Forms.Button();
            this.account = new System.Windows.Forms.TextBox();
            this.passwd = new System.Windows.Forms.TextBox();
            this.showPasswd = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // loading
            // 
            this.loading.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(110)))), ((int)(((byte)(123)))));
            this.loading.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loading.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.loading.Location = new System.Drawing.Point(541, 595);
            this.loading.Margin = new System.Windows.Forms.Padding(2);
            this.loading.Name = "loading";
            this.loading.Size = new System.Drawing.Size(100, 29);
            this.loading.TabIndex = 2;
            this.loading.Text = "登录";
            this.loading.UseVisualStyleBackColor = false;
            this.loading.Click += new System.EventHandler(this.Loading_Click);
            // 
            // account
            // 
            this.account.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.account.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.account.Location = new System.Drawing.Point(477, 473);
            this.account.Margin = new System.Windows.Forms.Padding(2);
            this.account.Name = "account";
            this.account.Size = new System.Drawing.Size(231, 38);
            this.account.TabIndex = 3;
            this.account.Text = "请输入学号:";
            this.account.TextChanged += new System.EventHandler(this.account_TextChanged);
            this.account.GotFocus += new System.EventHandler(this.account_Enter);
            this.account.LostFocus += new System.EventHandler(this.account_Leave);
            // 
            // passwd
            // 
            this.passwd.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.passwd.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.passwd.Location = new System.Drawing.Point(477, 537);
            this.passwd.Margin = new System.Windows.Forms.Padding(2);
            this.passwd.Name = "passwd";
            this.passwd.Size = new System.Drawing.Size(231, 38);
            this.passwd.TabIndex = 4;
            this.passwd.Text = "请输入密码:";
            this.passwd.TextChanged += new System.EventHandler(this.passwd_TextChanged);
            this.passwd.GotFocus += new System.EventHandler(this.passwd_Enter);
            this.passwd.LostFocus += new System.EventHandler(this.passwd_Leave);
            // 
            // showPasswd
            // 
            this.showPasswd.Location = new System.Drawing.Point(731, 548);
            this.showPasswd.Name = "showPasswd";
            this.showPasswd.Size = new System.Drawing.Size(95, 21);
            this.showPasswd.TabIndex = 5;
            this.showPasswd.Text = "显示密码";
            this.showPasswd.UseVisualStyleBackColor = true;
            this.showPasswd.CheckedChanged += new System.EventHandler(this.showPasswd_CheckedChanged);
            // 
            // Load
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1250, 650);
            this.Controls.Add(this.showPasswd);
            this.Controls.Add(this.passwd);
            this.Controls.Add(this.account);
            this.Controls.Add(this.loading);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Load";
            this.Text = "PigTail";
            this.Load += new System.EventHandler(this.Load_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button loading;
        private System.Windows.Forms.TextBox account;
        private System.Windows.Forms.TextBox passwd;
        private System.Windows.Forms.CheckBox showPasswd;
    }
}

