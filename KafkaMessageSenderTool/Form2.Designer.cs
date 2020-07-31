namespace KafkaMessageSenderTool
{
    partial class Form2
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_user = new System.Windows.Forms.TextBox();
            this.txt_pwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_sendChannel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_sendQueue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rtb_message = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_login = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.btn_sendOne = new System.Windows.Forms.Button();
            this.btn_sendMany = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_sendCount = new System.Windows.Forms.TextBox();
            this.txt_threadCount = new System.Windows.Forms.TextBox();
            this.btn_sendManyMulThread = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.ckb_showResult = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "账号";
            // 
            // txt_user
            // 
            this.txt_user.Location = new System.Drawing.Point(66, 21);
            this.txt_user.Name = "txt_user";
            this.txt_user.Size = new System.Drawing.Size(100, 27);
            this.txt_user.TabIndex = 1;
            // 
            // txt_pwd
            // 
            this.txt_pwd.Location = new System.Drawing.Point(251, 21);
            this.txt_pwd.Name = "txt_pwd";
            this.txt_pwd.Size = new System.Drawing.Size(100, 27);
            this.txt_pwd.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码";
            // 
            // txt_sendChannel
            // 
            this.txt_sendChannel.Location = new System.Drawing.Point(467, 21);
            this.txt_sendChannel.Name = "txt_sendChannel";
            this.txt_sendChannel.Size = new System.Drawing.Size(145, 27);
            this.txt_sendChannel.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(393, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "发送通道";
            // 
            // txt_sendQueue
            // 
            this.txt_sendQueue.Location = new System.Drawing.Point(730, 21);
            this.txt_sendQueue.Name = "txt_sendQueue";
            this.txt_sendQueue.Size = new System.Drawing.Size(186, 27);
            this.txt_sendQueue.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(655, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "发送队列";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(1052, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "平台地址在配置文件中配置";
            // 
            // rtb_message
            // 
            this.rtb_message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_message.Location = new System.Drawing.Point(17, 93);
            this.rtb_message.Name = "rtb_message";
            this.rtb_message.Size = new System.Drawing.Size(659, 557);
            this.rtb_message.TabIndex = 10;
            this.rtb_message.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "消息内容";
            // 
            // btn_login
            // 
            this.btn_login.AutoSize = true;
            this.btn_login.Location = new System.Drawing.Point(941, 19);
            this.btn_login.Name = "btn_login";
            this.btn_login.Size = new System.Drawing.Size(79, 30);
            this.btn_login.TabIndex = 12;
            this.btn_login.Text = "登录平台";
            this.btn_login.UseVisualStyleBackColor = true;
            this.btn_login.Click += new System.EventHandler(this.btn_login_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(694, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "日志";
            // 
            // rtb_log
            // 
            this.rtb_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_log.Location = new System.Drawing.Point(698, 93);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.Size = new System.Drawing.Size(666, 557);
            this.rtb_log.TabIndex = 14;
            this.rtb_log.Text = "";
            // 
            // btn_sendOne
            // 
            this.btn_sendOne.AutoSize = true;
            this.btn_sendOne.Location = new System.Drawing.Point(66, 686);
            this.btn_sendOne.Name = "btn_sendOne";
            this.btn_sendOne.Size = new System.Drawing.Size(79, 30);
            this.btn_sendOne.TabIndex = 15;
            this.btn_sendOne.Text = "单条发送";
            this.btn_sendOne.UseVisualStyleBackColor = true;
            this.btn_sendOne.Click += new System.EventHandler(this.btn_sendOne_Click);
            // 
            // btn_sendMany
            // 
            this.btn_sendMany.AutoSize = true;
            this.btn_sendMany.Location = new System.Drawing.Point(240, 19);
            this.btn_sendMany.Name = "btn_sendMany";
            this.btn_sendMany.Size = new System.Drawing.Size(94, 30);
            this.btn_sendMany.TabIndex = 16;
            this.btn_sendMany.Text = "单线程发送";
            this.btn_sendMany.UseVisualStyleBackColor = true;
            this.btn_sendMany.Click += new System.EventHandler(this.btn_sendMany_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "连续发送条数";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_threadCount);
            this.groupBox1.Controls.Add(this.btn_sendManyMulThread);
            this.groupBox1.Controls.Add(this.txt_sendCount);
            this.groupBox1.Controls.Add(this.btn_sendMany);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.Location = new System.Drawing.Point(216, 669);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(772, 58);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "多条发送";
            // 
            // txt_sendCount
            // 
            this.txt_sendCount.Location = new System.Drawing.Point(122, 21);
            this.txt_sendCount.Name = "txt_sendCount";
            this.txt_sendCount.Size = new System.Drawing.Size(98, 27);
            this.txt_sendCount.TabIndex = 18;
            this.txt_sendCount.Text = "1000";
            // 
            // txt_threadCount
            // 
            this.txt_threadCount.Location = new System.Drawing.Point(585, 20);
            this.txt_threadCount.Name = "txt_threadCount";
            this.txt_threadCount.Size = new System.Drawing.Size(42, 27);
            this.txt_threadCount.TabIndex = 20;
            this.txt_threadCount.Text = "10";
            // 
            // btn_sendManyMulThread
            // 
            this.btn_sendManyMulThread.AutoSize = true;
            this.btn_sendManyMulThread.Location = new System.Drawing.Point(633, 19);
            this.btn_sendManyMulThread.Name = "btn_sendManyMulThread";
            this.btn_sendManyMulThread.Size = new System.Drawing.Size(94, 30);
            this.btn_sendManyMulThread.TabIndex = 19;
            this.btn_sendManyMulThread.Text = "多线程发送";
            this.btn_sendManyMulThread.UseVisualStyleBackColor = true;
            this.btn_sendManyMulThread.Click += new System.EventHandler(this.btn_sendManyMulThread_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(525, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 20);
            this.label9.TabIndex = 21;
            this.label9.Text = "并发数";
            // 
            // ckb_showResult
            // 
            this.ckb_showResult.AutoSize = true;
            this.ckb_showResult.Location = new System.Drawing.Point(745, 68);
            this.ckb_showResult.Name = "ckb_showResult";
            this.ckb_showResult.Size = new System.Drawing.Size(196, 24);
            this.ckb_showResult.TabIndex = 19;
            this.ckb_showResult.Text = "多条发送时显示发送结果";
            this.ckb_showResult.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1376, 739);
            this.Controls.Add(this.ckb_showResult);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_sendOne);
            this.Controls.Add(this.rtb_log);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btn_login);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rtb_message);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_sendQueue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_sendChannel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_pwd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_user);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form2";
            this.Text = "IBM MQ测试工具";
            this.Shown += new System.EventHandler(this.Form2_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_user;
        private System.Windows.Forms.TextBox txt_pwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_sendChannel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_sendQueue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtb_message;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_login;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox rtb_log;
        private System.Windows.Forms.Button btn_sendOne;
        private System.Windows.Forms.Button btn_sendMany;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_sendCount;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_threadCount;
        private System.Windows.Forms.Button btn_sendManyMulThread;
        private System.Windows.Forms.CheckBox ckb_showResult;
    }
}