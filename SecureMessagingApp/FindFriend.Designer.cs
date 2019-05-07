namespace SecureMessagingApp
{
    partial class FindFriend
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
            this.friendIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.connectFriend = new System.Windows.Forms.Button();
            this.ConnectionMessages = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // friendIP
            // 
            this.friendIP.Location = new System.Drawing.Point(22, 30);
            this.friendIP.Multiline = true;
            this.friendIP.Name = "friendIP";
            this.friendIP.Size = new System.Drawing.Size(217, 26);
            this.friendIP.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Friend\'s IP address:";
            // 
            // connectFriend
            // 
            this.connectFriend.Location = new System.Drawing.Point(273, 15);
            this.connectFriend.Name = "connectFriend";
            this.connectFriend.Size = new System.Drawing.Size(204, 41);
            this.connectFriend.TabIndex = 2;
            this.connectFriend.Text = "Open Connection";
            this.connectFriend.UseVisualStyleBackColor = true;
            // 
            // ConnectionMessages
            // 
            this.ConnectionMessages.Location = new System.Drawing.Point(273, 94);
            this.ConnectionMessages.Multiline = true;
            this.ConnectionMessages.Name = "ConnectionMessages";
            this.ConnectionMessages.ReadOnly = true;
            this.ConnectionMessages.Size = new System.Drawing.Size(204, 72);
            this.ConnectionMessages.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(269, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Connection Status";
            // 
            // FindFriend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 194);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ConnectionMessages);
            this.Controls.Add(this.connectFriend);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.friendIP);
            this.Name = "FindFriend";
            this.Text = "FindFriend";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox friendIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button connectFriend;
        private System.Windows.Forms.TextBox ConnectionMessages;
        private System.Windows.Forms.Label label2;
    }
}