
namespace PigTail
{
    partial class InternetMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.createContest = new System.Windows.Forms.Button();
            this.joinContest = new System.Windows.Forms.Button();
            this.fetchPublicContest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // createContest
            // 
            this.createContest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(110)))), ((int)(((byte)(123)))));
            this.createContest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.createContest.ForeColor = System.Drawing.Color.White;
            this.createContest.Location = new System.Drawing.Point(275, 200);
            this.createContest.Margin = new System.Windows.Forms.Padding(2);
            this.createContest.Name = "createContest";
            this.createContest.Size = new System.Drawing.Size(161, 35);
            this.createContest.TabIndex = 2;
            this.createContest.Text = "创建对局";
            this.createContest.UseVisualStyleBackColor = false;
            this.createContest.Click += new System.EventHandler(this.createContest_Click);
            // 
            // joinContest
            // 
            this.joinContest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(110)))), ((int)(((byte)(123)))));
            this.joinContest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.joinContest.ForeColor = System.Drawing.Color.White;
            this.joinContest.Location = new System.Drawing.Point(275, 252);
            this.joinContest.Margin = new System.Windows.Forms.Padding(2);
            this.joinContest.Name = "joinContest";
            this.joinContest.Size = new System.Drawing.Size(161, 35);
            this.joinContest.TabIndex = 3;
            this.joinContest.Text = "加入对局";
            this.joinContest.UseVisualStyleBackColor = false;
            this.joinContest.Click += new System.EventHandler(this.joinContest_Click);
            // 
            // fetchPublicContest
            // 
            this.fetchPublicContest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(110)))), ((int)(((byte)(123)))));
            this.fetchPublicContest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fetchPublicContest.ForeColor = System.Drawing.Color.White;
            this.fetchPublicContest.Location = new System.Drawing.Point(275, 308);
            this.fetchPublicContest.Margin = new System.Windows.Forms.Padding(2);
            this.fetchPublicContest.Name = "fetchPublicContest";
            this.fetchPublicContest.Size = new System.Drawing.Size(161, 35);
            this.fetchPublicContest.TabIndex = 4;
            this.fetchPublicContest.Text = "查询公开对局";
            this.fetchPublicContest.UseVisualStyleBackColor = false;
            this.fetchPublicContest.Click += new System.EventHandler(this.fetchPublicContest_Click);
            // 
            // InternetMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::PigTail.Properties.Resources.menu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1250, 650);
            this.Controls.Add(this.fetchPublicContest);
            this.Controls.Add(this.joinContest);
            this.Controls.Add(this.createContest);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "InternetMenu";
            this.Text = "联网模式选择";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InternetMenu_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button createContest;
        private System.Windows.Forms.Button joinContest;
        private System.Windows.Forms.Button fetchPublicContest;
    }
}