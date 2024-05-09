namespace MessengerServer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            tbPort = new TextBox();
            label1 = new Label();
            btnListen = new Button();
            tbResponces = new TextBox();
            lbUsers = new ListBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(tbPort);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 22);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(154, 63);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Settings:";
            // 
            // tbPort
            // 
            tbPort.Location = new Point(44, 28);
            tbPort.Name = "tbPort";
            tbPort.Size = new Size(100, 23);
            tbPort.TabIndex = 1;
            tbPort.Text = "9999";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 31);
            label1.Name = "label1";
            label1.Size = new Size(32, 15);
            label1.TabIndex = 0;
            label1.Text = "Port:";
            // 
            // btnListen
            // 
            btnListen.Location = new Point(12, 91);
            btnListen.Name = "btnListen";
            btnListen.Size = new Size(154, 35);
            btnListen.TabIndex = 1;
            btnListen.Text = "Listen";
            btnListen.UseVisualStyleBackColor = true;
            btnListen.Click += btnListen_Click;
            // 
            // tbResponces
            // 
            tbResponces.Location = new Point(187, 22);
            tbResponces.Multiline = true;
            tbResponces.Name = "tbResponces";
            tbResponces.ReadOnly = true;
            tbResponces.ScrollBars = ScrollBars.Vertical;
            tbResponces.Size = new Size(359, 308);
            tbResponces.TabIndex = 2;
            // 
            // lbUsers
            // 
            lbUsers.FormattingEnabled = true;
            lbUsers.ItemHeight = 15;
            lbUsers.Location = new Point(12, 131);
            lbUsers.Name = "lbUsers";
            lbUsers.Size = new Size(154, 199);
            lbUsers.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(558, 342);
            Controls.Add(lbUsers);
            Controls.Add(tbResponces);
            Controls.Add(btnListen);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private TextBox tbPort;
        private Label label1;
        private Button btnListen;
        private TextBox tbResponces;
        private ListBox lbUsers;
    }
}