
namespace PigTail
{
    partial class Menu
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
            this.localMode_button = new System.Windows.Forms.Button();
            this.interMode_button = new System.Windows.Forms.Button();
            this.aiMode_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // localMode_button
            // 
            this.localMode_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(110)))), ((int)(((byte)(123)))));
            this.localMode_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.localMode_button.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.localMode_button.Location = new System.Drawing.Point(270, 202);
            this.localMode_button.Margin = new System.Windows.Forms.Padding(2);
            this.localMode_button.Name = "localMode_button";
            this.localMode_button.Size = new System.Drawing.Size(161, 35);
            this.localMode_button.TabIndex = 0;
            this.localMode_button.Text = "本地对战";
            this.localMode_button.UseVisualStyleBackColor = false;
            this.localMode_button.Click += new System.EventHandler(this.Local_Mode_Loading);
            // 
            // interMode_button
            // 
            this.interMode_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(110)))), ((int)(((byte)(123)))));
            this.interMode_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.interMode_button.ForeColor = System.Drawing.Color.White;
            this.interMode_button.Location = new System.Drawing.Point(270, 255);
            this.interMode_button.Margin = new System.Windows.Forms.Padding(2);
            this.interMode_button.Name = "interMode_button";
            this.interMode_button.Size = new System.Drawing.Size(161, 35);
            this.interMode_button.TabIndex = 1;
            this.interMode_button.Text = "联网对战";
            this.interMode_button.UseVisualStyleBackColor = false;
            this.interMode_button.Click += new System.EventHandler(this.Internet_Mode_Loading);
            // 
            // aiMode_button
            // 
            this.aiMode_button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(110)))), ((int)(((byte)(123)))));
            this.aiMode_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aiMode_button.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.aiMode_button.Location = new System.Drawing.Point(270, 309);
            this.aiMode_button.Margin = new System.Windows.Forms.Padding(2);
            this.aiMode_button.Name = "aiMode_button";
            this.aiMode_button.Size = new System.Drawing.Size(161, 35);
            this.aiMode_button.TabIndex = 2;
            this.aiMode_button.Text = "AI挑战";
            this.aiMode_button.UseVisualStyleBackColor = false;
            this.aiMode_button.Click += new System.EventHandler(this.AI_Mode_Loading);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::PigTail.Properties.Resources.menu;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1250, 650);
            this.Controls.Add(this.aiMode_button);
            this.Controls.Add(this.interMode_button);
            this.Controls.Add(this.localMode_button);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.Text = "模式选择";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button localMode_button;
        private System.Windows.Forms.Button interMode_button;
        private System.Windows.Forms.Button aiMode_button;
    }
}