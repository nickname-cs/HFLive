namespace HFLive
{
    partial class HFLive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HFLive));
            this.pictureBoxPause = new System.Windows.Forms.PictureBox();
            this.pictureBoxPlay = new System.Windows.Forms.PictureBox();
            this.pictureBoxStop = new System.Windows.Forms.PictureBox();
            this.panelHome = new System.Windows.Forms.Panel();
            this.panelPower = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelUp = new System.Windows.Forms.Panel();
            this.panelDown = new System.Windows.Forms.Panel();
            this.panelMute = new System.Windows.Forms.Panel();
            this.labelUpdate = new System.Windows.Forms.Label();
            this.panelCamera = new System.Windows.Forms.Panel();
            this.labelArtist = new System.Windows.Forms.Label();
            this.panelMeta = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPause)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStop)).BeginInit();
            this.panelMeta.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxPause
            // 
            this.pictureBoxPause.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxPause.Image = global::HFLive.Properties.Resources.pause;
            this.pictureBoxPause.Location = new System.Drawing.Point(36, 336);
            this.pictureBoxPause.Name = "pictureBoxPause";
            this.pictureBoxPause.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxPause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPause.TabIndex = 0;
            this.pictureBoxPause.TabStop = false;
            this.pictureBoxPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // pictureBoxPlay
            // 
            this.pictureBoxPlay.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxPlay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxPlay.Image = global::HFLive.Properties.Resources.play;
            this.pictureBoxPlay.Location = new System.Drawing.Point(112, 336);
            this.pictureBoxPlay.Name = "pictureBoxPlay";
            this.pictureBoxPlay.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPlay.TabIndex = 1;
            this.pictureBoxPlay.TabStop = false;
            this.pictureBoxPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // pictureBoxStop
            // 
            this.pictureBoxStop.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxStop.Image = global::HFLive.Properties.Resources.stop;
            this.pictureBoxStop.Location = new System.Drawing.Point(191, 336);
            this.pictureBoxStop.Name = "pictureBoxStop";
            this.pictureBoxStop.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxStop.TabIndex = 2;
            this.pictureBoxStop.TabStop = false;
            this.pictureBoxStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // panelHome
            // 
            this.panelHome.BackColor = System.Drawing.Color.Transparent;
            this.panelHome.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelHome.Location = new System.Drawing.Point(115, 420);
            this.panelHome.Name = "panelHome";
            this.panelHome.Size = new System.Drawing.Size(50, 50);
            this.panelHome.TabIndex = 3;
            this.panelHome.Click += new System.EventHandler(this.panelHome_Click);
            // 
            // panelPower
            // 
            this.panelPower.BackColor = System.Drawing.Color.Transparent;
            this.panelPower.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelPower.Location = new System.Drawing.Point(172, 2);
            this.panelPower.Name = "panelPower";
            this.panelPower.Size = new System.Drawing.Size(62, 23);
            this.panelPower.TabIndex = 4;
            this.panelPower.Click += new System.EventHandler(this.panelPower_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 250;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.AutoSize = true;
            this.labelTitle.BackColor = System.Drawing.Color.Transparent;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.ForeColor = System.Drawing.SystemColors.Control;
            this.labelTitle.Location = new System.Drawing.Point(97, 13);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(32, 13);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "Title";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTitle.SizeChanged += new System.EventHandler(this.labelTitle_SizeChanged);
            // 
            // panelUp
            // 
            this.panelUp.BackColor = System.Drawing.Color.Transparent;
            this.panelUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelUp.Location = new System.Drawing.Point(5, 126);
            this.panelUp.Name = "panelUp";
            this.panelUp.Size = new System.Drawing.Size(20, 32);
            this.panelUp.TabIndex = 7;
            this.panelUp.Click += new System.EventHandler(this.panelUp_Click);
            // 
            // panelDown
            // 
            this.panelDown.BackColor = System.Drawing.Color.Transparent;
            this.panelDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelDown.Location = new System.Drawing.Point(6, 170);
            this.panelDown.Name = "panelDown";
            this.panelDown.Size = new System.Drawing.Size(19, 32);
            this.panelDown.TabIndex = 8;
            this.panelDown.Click += new System.EventHandler(this.panelDown_Click);
            // 
            // panelMute
            // 
            this.panelMute.BackColor = System.Drawing.Color.Transparent;
            this.panelMute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelMute.Location = new System.Drawing.Point(6, 79);
            this.panelMute.Name = "panelMute";
            this.panelMute.Size = new System.Drawing.Size(20, 32);
            this.panelMute.TabIndex = 8;
            this.panelMute.Click += new System.EventHandler(this.panelMute_Click);
            // 
            // labelUpdate
            // 
            this.labelUpdate.AutoSize = true;
            this.labelUpdate.BackColor = System.Drawing.Color.Transparent;
            this.labelUpdate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUpdate.ForeColor = System.Drawing.Color.Lime;
            this.labelUpdate.Location = new System.Drawing.Point(96, 88);
            this.labelUpdate.Name = "labelUpdate";
            this.labelUpdate.Size = new System.Drawing.Size(104, 13);
            this.labelUpdate.TabIndex = 9;
            this.labelUpdate.Text = "Update Available";
            this.labelUpdate.Visible = false;
            this.labelUpdate.Click += new System.EventHandler(this.labelUpdate_Click);
            // 
            // panelCamera
            // 
            this.panelCamera.BackColor = System.Drawing.Color.Transparent;
            this.panelCamera.Cursor = System.Windows.Forms.Cursors.Hand;
            this.panelCamera.Location = new System.Drawing.Point(117, 26);
            this.panelCamera.Name = "panelCamera";
            this.panelCamera.Size = new System.Drawing.Size(47, 19);
            this.panelCamera.TabIndex = 10;
            this.panelCamera.Click += new System.EventHandler(this.panelCamera_Click);
            // 
            // labelArtist
            // 
            this.labelArtist.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelArtist.AutoSize = true;
            this.labelArtist.BackColor = System.Drawing.Color.Transparent;
            this.labelArtist.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelArtist.ForeColor = System.Drawing.SystemColors.Control;
            this.labelArtist.Location = new System.Drawing.Point(95, 28);
            this.labelArtist.Name = "labelArtist";
            this.labelArtist.Size = new System.Drawing.Size(36, 13);
            this.labelArtist.TabIndex = 11;
            this.labelArtist.Text = "Artist";
            this.labelArtist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelArtist.SizeChanged += new System.EventHandler(this.labelArtist_SizeChanged);
            // 
            // panelMeta
            // 
            this.panelMeta.BackColor = System.Drawing.Color.Transparent;
            this.panelMeta.Controls.Add(this.labelArtist);
            this.panelMeta.Controls.Add(this.labelTitle);
            this.panelMeta.Location = new System.Drawing.Point(25, 270);
            this.panelMeta.Name = "panelMeta";
            this.panelMeta.Size = new System.Drawing.Size(227, 60);
            this.panelMeta.TabIndex = 12;
            // 
            // HFLive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.BackgroundImage = global::HFLive.Properties.Resources.Phone;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(277, 500);
            this.Controls.Add(this.panelCamera);
            this.Controls.Add(this.labelUpdate);
            this.Controls.Add(this.panelMute);
            this.Controls.Add(this.panelDown);
            this.Controls.Add(this.panelUp);
            this.Controls.Add(this.panelPower);
            this.Controls.Add(this.panelHome);
            this.Controls.Add(this.pictureBoxStop);
            this.Controls.Add(this.pictureBoxPlay);
            this.Controls.Add(this.pictureBoxPause);
            this.Controls.Add(this.panelMeta);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HFLive";
            this.Opacity = 0D;
            this.Text = "HFLive";
            this.TransparencyKey = System.Drawing.SystemColors.ControlLight;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HFLive_FormClosing);
            this.Load += new System.EventHandler(this.HFLive_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.HFLive_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPause)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStop)).EndInit();
            this.panelMeta.ResumeLayout(false);
            this.panelMeta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxPause;
        private System.Windows.Forms.PictureBox pictureBoxPlay;
        private System.Windows.Forms.PictureBox pictureBoxStop;
        private System.Windows.Forms.Panel panelHome;
        private System.Windows.Forms.Panel panelPower;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelUp;
        private System.Windows.Forms.Panel panelDown;
        private System.Windows.Forms.Panel panelMute;
        private System.Windows.Forms.Label labelUpdate;
        private System.Windows.Forms.Panel panelCamera;
        private System.Windows.Forms.Label labelArtist;
        private System.Windows.Forms.Panel panelMeta;
    }
}