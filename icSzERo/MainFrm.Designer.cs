namespace icSzERo
{
    partial class MainFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.NewGameButton = new System.Windows.Forms.Button();
            this.HighScoresButton = new System.Windows.Forms.Button();
            this.QuitButton = new System.Windows.Forms.Button();
            this.PVPButton = new System.Windows.Forms.Button();
            this.PVAIButton = new System.Windows.Forms.Button();
            this.PVPOnlineBtn = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.StatusUpdater = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poor Richard", 72F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(227, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 135);
            this.label1.TabIndex = 0;
            this.label1.Text = "X si 0";
            // 
            // NewGameButton
            // 
            this.NewGameButton.BackColor = System.Drawing.Color.White;
            this.NewGameButton.Location = new System.Drawing.Point(200, 308);
            this.NewGameButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.NewGameButton.Name = "NewGameButton";
            this.NewGameButton.Size = new System.Drawing.Size(380, 90);
            this.NewGameButton.TabIndex = 1;
            this.NewGameButton.Text = "Joc Nou";
            this.NewGameButton.UseVisualStyleBackColor = false;
            this.NewGameButton.Click += new System.EventHandler(this.NewGameButton_Click);
            // 
            // HighScoresButton
            // 
            this.HighScoresButton.BackColor = System.Drawing.Color.White;
            this.HighScoresButton.Location = new System.Drawing.Point(200, 431);
            this.HighScoresButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.HighScoresButton.Name = "HighScoresButton";
            this.HighScoresButton.Size = new System.Drawing.Size(380, 90);
            this.HighScoresButton.TabIndex = 2;
            this.HighScoresButton.Text = "Clasament";
            this.HighScoresButton.UseVisualStyleBackColor = false;
            // 
            // QuitButton
            // 
            this.QuitButton.BackColor = System.Drawing.Color.White;
            this.QuitButton.Location = new System.Drawing.Point(200, 562);
            this.QuitButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(380, 90);
            this.QuitButton.TabIndex = 3;
            this.QuitButton.Text = "Iesire";
            this.QuitButton.UseVisualStyleBackColor = false;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // PVPButton
            // 
            this.PVPButton.BackColor = System.Drawing.Color.White;
            this.PVPButton.Location = new System.Drawing.Point(200, 308);
            this.PVPButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PVPButton.Name = "PVPButton";
            this.PVPButton.Size = new System.Drawing.Size(127, 90);
            this.PVPButton.TabIndex = 4;
            this.PVPButton.Text = "Player vs Player Local";
            this.PVPButton.UseVisualStyleBackColor = false;
            this.PVPButton.Visible = false;
            this.PVPButton.Click += new System.EventHandler(this.PVPButton_Click);
            // 
            // PVAIButton
            // 
            this.PVAIButton.BackColor = System.Drawing.Color.White;
            this.PVAIButton.Location = new System.Drawing.Point(453, 308);
            this.PVAIButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PVAIButton.Name = "PVAIButton";
            this.PVAIButton.Size = new System.Drawing.Size(127, 90);
            this.PVAIButton.TabIndex = 5;
            this.PVAIButton.Text = " vs A.I.";
            this.PVAIButton.UseVisualStyleBackColor = false;
            this.PVAIButton.Visible = false;
            this.PVAIButton.Click += new System.EventHandler(this.PVAIButton_Click);
            // 
            // PVPOnlineBtn
            // 
            this.PVPOnlineBtn.BackColor = System.Drawing.Color.White;
            this.PVPOnlineBtn.Location = new System.Drawing.Point(327, 308);
            this.PVPOnlineBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PVPOnlineBtn.Name = "PVPOnlineBtn";
            this.PVPOnlineBtn.Size = new System.Drawing.Size(127, 90);
            this.PVPOnlineBtn.TabIndex = 6;
            this.PVPOnlineBtn.Text = "Player vs Player Online";
            this.PVPOnlineBtn.UseVisualStyleBackColor = false;
            this.PVPOnlineBtn.Visible = false;
            this.PVPOnlineBtn.Click += new System.EventHandler(this.PVPOnlineBtn_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(327, 410);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Visible = false;
            // 
            // StatusUpdater
            // 
            this.StatusUpdater.Enabled = true;
            this.StatusUpdater.Interval = 10;
            this.StatusUpdater.Tick += new System.EventHandler(this.StatusUpdater_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(779, 690);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.PVPOnlineBtn);
            this.Controls.Add(this.PVAIButton);
            this.Controls.Add(this.PVPButton);
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.HighScoresButton);
            this.Controls.Add(this.NewGameButton);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button NewGameButton;
        private System.Windows.Forms.Button HighScoresButton;
        private System.Windows.Forms.Button QuitButton;
        private System.Windows.Forms.Button PVPButton;
        private System.Windows.Forms.Button PVAIButton;
        private System.Windows.Forms.Button PVPOnlineBtn;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer StatusUpdater;
    }
}

