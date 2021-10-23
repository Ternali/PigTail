
namespace PigTail
{
    partial class Worm
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
            this.random = new System.Windows.Forms.Button();
            this.uuidBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // random
            // 
            this.random.Location = new System.Drawing.Point(341, 149);
            this.random.Name = "random";
            this.random.Size = new System.Drawing.Size(78, 25);
            this.random.TabIndex = 0;
            this.random.Text = "随机UUID";
            this.random.UseVisualStyleBackColor = true;
            this.random.Click += new System.EventHandler(this.random_Click);
            // 
            // uuidBox
            // 
            this.uuidBox.Location = new System.Drawing.Point(90, 149);
            this.uuidBox.Name = "uuidBox";
            this.uuidBox.Size = new System.Drawing.Size(233, 25);
            this.uuidBox.TabIndex = 1;
            this.uuidBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.uuidBox_KeyPress);
            // 
            // Worm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::PigTail.Properties.Resources.worm;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(462, 223);
            this.Controls.Add(this.uuidBox);
            this.Controls.Add(this.random);
            this.MaximizeBox = false;
            this.Name = "Worm";
            this.Text = "获取公开对局uuid";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button random;
        private System.Windows.Forms.TextBox uuidBox;
    }
}