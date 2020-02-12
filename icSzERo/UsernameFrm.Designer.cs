namespace icSzERo
{
    partial class UsernameFrm
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
            this.lblUser = new System.Windows.Forms.Label();
            this.TBUser = new System.Windows.Forms.TextBox();
            this.BtnUser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(12, 98);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(104, 13);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "Choose a username:";
            // 
            // TBUser
            // 
            this.TBUser.Location = new System.Drawing.Point(12, 114);
            this.TBUser.Name = "TBUser";
            this.TBUser.Size = new System.Drawing.Size(260, 20);
            this.TBUser.TabIndex = 1;
            // 
            // BtnUser
            // 
            this.BtnUser.Location = new System.Drawing.Point(12, 226);
            this.BtnUser.Name = "BtnUser";
            this.BtnUser.Size = new System.Drawing.Size(260, 23);
            this.BtnUser.TabIndex = 2;
            this.BtnUser.Text = "Done";
            this.BtnUser.UseVisualStyleBackColor = true;
            this.BtnUser.Click += new System.EventHandler(this.BtnUser_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Only letters(lowercase or upercase) are allowed";
            // 
            // UsernameFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnUser);
            this.Controls.Add(this.TBUser);
            this.Controls.Add(this.lblUser);
            this.Name = "UsernameFrm";
            this.Text = "UsernameFrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox TBUser;
        private System.Windows.Forms.Button BtnUser;
        private System.Windows.Forms.Label label1;
    }
}