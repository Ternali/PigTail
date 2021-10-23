
namespace PigTail
{
    partial class LocalMode
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
            this.components = new System.ComponentModel.Container();
            this.cardPublished = new System.Windows.Forms.PictureBox();
            this.cardPile = new System.Windows.Forms.PictureBox();
            this.spadeRight = new System.Windows.Forms.Button();
            this.heartRight = new System.Windows.Forms.Button();
            this.clubRight = new System.Windows.Forms.Button();
            this.diamondRight = new System.Windows.Forms.Button();
            this.diamondLeft = new System.Windows.Forms.Button();
            this.clubLeft = new System.Windows.Forms.Button();
            this.heartLeft = new System.Windows.Forms.Button();
            this.spadeLeft = new System.Windows.Forms.Button();
            this.cardPileNum = new System.Windows.Forms.TextBox();
            this.cardPublishedNum = new System.Windows.Forms.TextBox();
            this.playerDown = new System.Windows.Forms.PictureBox();
            this.playerUp = new System.Windows.Forms.PictureBox();
            this.downSpade = new System.Windows.Forms.TextBox();
            this.downHeart = new System.Windows.Forms.TextBox();
            this.downClub = new System.Windows.Forms.TextBox();
            this.downDiamond = new System.Windows.Forms.TextBox();
            this.upDiamond = new System.Windows.Forms.TextBox();
            this.upClub = new System.Windows.Forms.TextBox();
            this.upHeart = new System.Windows.Forms.TextBox();
            this.upSpade = new System.Windows.Forms.TextBox();
            this.turnCue = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cardPublished)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardPile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerUp)).BeginInit();
            this.SuspendLayout();
            // 
            // cardPublished
            // 
            this.cardPublished.Image = global::PigTail.Properties.Resources.pigtail;
            this.cardPublished.InitialImage = global::PigTail.Properties.Resources.pigtail;
            this.cardPublished.Location = new System.Drawing.Point(772, 181);
            this.cardPublished.Margin = new System.Windows.Forms.Padding(0);
            this.cardPublished.Name = "cardPublished";
            this.cardPublished.Size = new System.Drawing.Size(149, 231);
            this.cardPublished.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.cardPublished.TabIndex = 1;
            this.cardPublished.TabStop = false;
            // 
            // cardPile
            // 
            this.cardPile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cardPile.Image = global::PigTail.Properties.Resources.pigtail;
            this.cardPile.InitialImage = global::PigTail.Properties.Resources.pigtail;
            this.cardPile.Location = new System.Drawing.Point(292, 181);
            this.cardPile.Margin = new System.Windows.Forms.Padding(0);
            this.cardPile.Name = "cardPile";
            this.cardPile.Size = new System.Drawing.Size(149, 231);
            this.cardPile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.cardPile.TabIndex = 2;
            this.cardPile.TabStop = false;
            this.cardPile.Click += new System.EventHandler(this.cardPile_Click);
            // 
            // spadeRight
            // 
            this.spadeRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.spadeRight.Cursor = System.Windows.Forms.Cursors.No;
            this.spadeRight.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.spadeRight.Location = new System.Drawing.Point(878, 0);
            this.spadeRight.Margin = new System.Windows.Forms.Padding(2);
            this.spadeRight.Name = "spadeRight";
            this.spadeRight.Size = new System.Drawing.Size(81, 125);
            this.spadeRight.TabIndex = 3;
            this.spadeRight.Text = "♠";
            this.spadeRight.UseVisualStyleBackColor = true;
            this.spadeRight.Click += new System.EventHandler(this.spadeRight_Click);
            // 
            // heartRight
            // 
            this.heartRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.heartRight.Cursor = System.Windows.Forms.Cursors.No;
            this.heartRight.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.heartRight.ForeColor = System.Drawing.Color.Red;
            this.heartRight.Location = new System.Drawing.Point(715, 0);
            this.heartRight.Margin = new System.Windows.Forms.Padding(2);
            this.heartRight.Name = "heartRight";
            this.heartRight.Size = new System.Drawing.Size(81, 125);
            this.heartRight.TabIndex = 4;
            this.heartRight.Text = "♥";
            this.heartRight.UseVisualStyleBackColor = true;
            this.heartRight.Click += new System.EventHandler(this.heartRight_Click);
            // 
            // clubRight
            // 
            this.clubRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.clubRight.Cursor = System.Windows.Forms.Cursors.No;
            this.clubRight.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clubRight.Location = new System.Drawing.Point(552, 0);
            this.clubRight.Margin = new System.Windows.Forms.Padding(2);
            this.clubRight.Name = "clubRight";
            this.clubRight.Size = new System.Drawing.Size(81, 125);
            this.clubRight.TabIndex = 5;
            this.clubRight.Text = "♣";
            this.clubRight.UseVisualStyleBackColor = true;
            this.clubRight.Click += new System.EventHandler(this.clubRight_Click);
            // 
            // diamondRight
            // 
            this.diamondRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.diamondRight.Cursor = System.Windows.Forms.Cursors.No;
            this.diamondRight.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.diamondRight.ForeColor = System.Drawing.Color.Red;
            this.diamondRight.Location = new System.Drawing.Point(390, 0);
            this.diamondRight.Margin = new System.Windows.Forms.Padding(2);
            this.diamondRight.Name = "diamondRight";
            this.diamondRight.Size = new System.Drawing.Size(81, 125);
            this.diamondRight.TabIndex = 6;
            this.diamondRight.Text = "♦";
            this.diamondRight.UseVisualStyleBackColor = true;
            this.diamondRight.Click += new System.EventHandler(this.diamondRight_Click);
            // 
            // diamondLeft
            // 
            this.diamondLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.diamondLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.diamondLeft.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.diamondLeft.ForeColor = System.Drawing.Color.Red;
            this.diamondLeft.Location = new System.Drawing.Point(780, 524);
            this.diamondLeft.Margin = new System.Windows.Forms.Padding(2);
            this.diamondLeft.Name = "diamondLeft";
            this.diamondLeft.Size = new System.Drawing.Size(81, 125);
            this.diamondLeft.TabIndex = 10;
            this.diamondLeft.Text = "♦";
            this.diamondLeft.UseVisualStyleBackColor = true;
            this.diamondLeft.Click += new System.EventHandler(this.diamondLeft_Click);
            // 
            // clubLeft
            // 
            this.clubLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.clubLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clubLeft.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clubLeft.Location = new System.Drawing.Point(618, 524);
            this.clubLeft.Margin = new System.Windows.Forms.Padding(2);
            this.clubLeft.Name = "clubLeft";
            this.clubLeft.Size = new System.Drawing.Size(81, 125);
            this.clubLeft.TabIndex = 9;
            this.clubLeft.Text = "♣";
            this.clubLeft.UseVisualStyleBackColor = true;
            this.clubLeft.Click += new System.EventHandler(this.clubLeft_Click);
            // 
            // heartLeft
            // 
            this.heartLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.heartLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.heartLeft.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.heartLeft.ForeColor = System.Drawing.Color.Red;
            this.heartLeft.Location = new System.Drawing.Point(455, 524);
            this.heartLeft.Margin = new System.Windows.Forms.Padding(2);
            this.heartLeft.Name = "heartLeft";
            this.heartLeft.Size = new System.Drawing.Size(81, 125);
            this.heartLeft.TabIndex = 8;
            this.heartLeft.Text = "♥";
            this.heartLeft.UseVisualStyleBackColor = true;
            this.heartLeft.Click += new System.EventHandler(this.heartLeft_Click);
            // 
            // spadeLeft
            // 
            this.spadeLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.spadeLeft.Cursor = System.Windows.Forms.Cursors.Hand;
            this.spadeLeft.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.spadeLeft.Location = new System.Drawing.Point(292, 524);
            this.spadeLeft.Margin = new System.Windows.Forms.Padding(2);
            this.spadeLeft.Name = "spadeLeft";
            this.spadeLeft.Size = new System.Drawing.Size(81, 125);
            this.spadeLeft.TabIndex = 7;
            this.spadeLeft.Text = "♠";
            this.spadeLeft.UseVisualStyleBackColor = true;
            this.spadeLeft.Click += new System.EventHandler(this.spadeLeft_Click);
            // 
            // cardPileNum
            // 
            this.cardPileNum.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cardPileNum.Location = new System.Drawing.Point(496, 316);
            this.cardPileNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cardPileNum.Name = "cardPileNum";
            this.cardPileNum.ReadOnly = true;
            this.cardPileNum.Size = new System.Drawing.Size(35, 27);
            this.cardPileNum.TabIndex = 11;
            this.cardPileNum.Text = "X52";
            // 
            // cardPublishedNum
            // 
            this.cardPublishedNum.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cardPublishedNum.Location = new System.Drawing.Point(716, 316);
            this.cardPublishedNum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cardPublishedNum.Name = "cardPublishedNum";
            this.cardPublishedNum.ReadOnly = true;
            this.cardPublishedNum.Size = new System.Drawing.Size(35, 27);
            this.cardPublishedNum.TabIndex = 12;
            this.cardPublishedNum.Text = "X0";
            // 
            // playerDown
            // 
            this.playerDown.Image = global::PigTail.Properties.Resources.Humanity;
            this.playerDown.Location = new System.Drawing.Point(100, 492);
            this.playerDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.playerDown.Name = "playerDown";
            this.playerDown.Size = new System.Drawing.Size(109, 156);
            this.playerDown.TabIndex = 13;
            this.playerDown.TabStop = false;
            // 
            // playerUp
            // 
            this.playerUp.Image = global::PigTail.Properties.Resources.Humanity;
            this.playerUp.Location = new System.Drawing.Point(1035, 0);
            this.playerUp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.playerUp.Name = "playerUp";
            this.playerUp.Size = new System.Drawing.Size(109, 156);
            this.playerUp.TabIndex = 14;
            this.playerUp.TabStop = false;
            // 
            // downSpade
            // 
            this.downSpade.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.downSpade.Location = new System.Drawing.Point(315, 492);
            this.downSpade.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.downSpade.Name = "downSpade";
            this.downSpade.ReadOnly = true;
            this.downSpade.Size = new System.Drawing.Size(35, 27);
            this.downSpade.TabIndex = 15;
            this.downSpade.Text = "X0";
            // 
            // downHeart
            // 
            this.downHeart.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.downHeart.Location = new System.Drawing.Point(476, 492);
            this.downHeart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.downHeart.Name = "downHeart";
            this.downHeart.ReadOnly = true;
            this.downHeart.Size = new System.Drawing.Size(35, 27);
            this.downHeart.TabIndex = 16;
            this.downHeart.Text = "X0";
            // 
            // downClub
            // 
            this.downClub.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.downClub.Location = new System.Drawing.Point(640, 492);
            this.downClub.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.downClub.Name = "downClub";
            this.downClub.ReadOnly = true;
            this.downClub.Size = new System.Drawing.Size(35, 27);
            this.downClub.TabIndex = 17;
            this.downClub.Text = "X0";
            // 
            // downDiamond
            // 
            this.downDiamond.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.downDiamond.Location = new System.Drawing.Point(801, 492);
            this.downDiamond.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.downDiamond.Name = "downDiamond";
            this.downDiamond.ReadOnly = true;
            this.downDiamond.Size = new System.Drawing.Size(35, 27);
            this.downDiamond.TabIndex = 18;
            this.downDiamond.Text = "X0";
            // 
            // upDiamond
            // 
            this.upDiamond.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.upDiamond.Location = new System.Drawing.Point(411, 131);
            this.upDiamond.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.upDiamond.Name = "upDiamond";
            this.upDiamond.ReadOnly = true;
            this.upDiamond.Size = new System.Drawing.Size(35, 27);
            this.upDiamond.TabIndex = 19;
            this.upDiamond.Text = "X0";
            // 
            // upClub
            // 
            this.upClub.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.upClub.Location = new System.Drawing.Point(574, 131);
            this.upClub.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.upClub.Name = "upClub";
            this.upClub.ReadOnly = true;
            this.upClub.Size = new System.Drawing.Size(35, 27);
            this.upClub.TabIndex = 20;
            this.upClub.Text = "X0";
            // 
            // upHeart
            // 
            this.upHeart.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.upHeart.Location = new System.Drawing.Point(739, 131);
            this.upHeart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.upHeart.Name = "upHeart";
            this.upHeart.ReadOnly = true;
            this.upHeart.Size = new System.Drawing.Size(35, 27);
            this.upHeart.TabIndex = 21;
            this.upHeart.Text = "X0";
            // 
            // upSpade
            // 
            this.upSpade.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.upSpade.Location = new System.Drawing.Point(899, 131);
            this.upSpade.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.upSpade.Name = "upSpade";
            this.upSpade.ReadOnly = true;
            this.upSpade.Size = new System.Drawing.Size(35, 27);
            this.upSpade.TabIndex = 22;
            this.upSpade.Text = "X0";
            // 
            // turnCue
            // 
            this.turnCue.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.turnCue.Location = new System.Drawing.Point(558, 316);
            this.turnCue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.turnCue.Name = "turnCue";
            this.turnCue.ReadOnly = true;
            this.turnCue.Size = new System.Drawing.Size(124, 27);
            this.turnCue.TabIndex = 23;
            this.turnCue.Text = "下边玩家回合!";
            this.turnCue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 2000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // LocalMode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImage = global::PigTail.Properties.Resources.local_mode;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1250, 650);
            this.Controls.Add(this.turnCue);
            this.Controls.Add(this.upSpade);
            this.Controls.Add(this.upHeart);
            this.Controls.Add(this.upClub);
            this.Controls.Add(this.upDiamond);
            this.Controls.Add(this.downDiamond);
            this.Controls.Add(this.downClub);
            this.Controls.Add(this.downHeart);
            this.Controls.Add(this.downSpade);
            this.Controls.Add(this.playerUp);
            this.Controls.Add(this.playerDown);
            this.Controls.Add(this.cardPublishedNum);
            this.Controls.Add(this.cardPileNum);
            this.Controls.Add(this.diamondLeft);
            this.Controls.Add(this.clubLeft);
            this.Controls.Add(this.heartLeft);
            this.Controls.Add(this.spadeLeft);
            this.Controls.Add(this.diamondRight);
            this.Controls.Add(this.clubRight);
            this.Controls.Add(this.heartRight);
            this.Controls.Add(this.spadeRight);
            this.Controls.Add(this.cardPile);
            this.Controls.Add(this.cardPublished);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "LocalMode";
            this.Text = "本地对战";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LocalMode_FormClosing);
            this.Load += new System.EventHandler(this.Local_Mode_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cardPublished)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardPile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerUp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox cardPublished;
        private System.Windows.Forms.PictureBox cardPile;
        private System.Windows.Forms.Button spadeRight;
        private System.Windows.Forms.Button heartRight;
        private System.Windows.Forms.Button clubRight;
        private System.Windows.Forms.Button diamondRight;
        private System.Windows.Forms.Button diamondLeft;
        private System.Windows.Forms.Button clubLeft;
        private System.Windows.Forms.Button heartLeft;
        private System.Windows.Forms.Button spadeLeft;
        private System.Windows.Forms.TextBox cardPileNum;
        private System.Windows.Forms.TextBox cardPublishedNum;
        private System.Windows.Forms.PictureBox playerDown;
        private System.Windows.Forms.PictureBox playerUp;
        private System.Windows.Forms.TextBox downSpade;
        private System.Windows.Forms.TextBox downHeart;
        private System.Windows.Forms.TextBox downClub;
        private System.Windows.Forms.TextBox downDiamond;
        private System.Windows.Forms.TextBox upDiamond;
        private System.Windows.Forms.TextBox upClub;
        private System.Windows.Forms.TextBox upHeart;
        private System.Windows.Forms.TextBox upSpade;
        private System.Windows.Forms.TextBox turnCue;
        private System.Windows.Forms.Timer timer;
    }
}