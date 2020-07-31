namespace KafkaMessageSenderTool
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_url = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_topic = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_send = new System.Windows.Forms.Button();
            this.rtb_message = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_clearLog = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.rtb_log = new System.Windows.Forms.RichTextBox();
            this.ckb_showMessage = new System.Windows.Forms.CheckBox();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.txt_interval = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_sheetInfo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbb_sheet = new System.Windows.Forms.ComboBox();
            this.btn_beforeLoad = new System.Windows.Forms.Button();
            this.txt_filePath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_showIBM = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(751, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kafka服务器地址，格式为IP：端口号。如是集群，多个地址间用英文逗号隔开，例如 kafka1:9092,kafka2:9092";
            // 
            // txt_url
            // 
            this.txt_url.Location = new System.Drawing.Point(33, 48);
            this.txt_url.Name = "txt_url";
            this.txt_url.Size = new System.Drawing.Size(1104, 27);
            this.txt_url.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Kafka Topic主题名";
            // 
            // txt_topic
            // 
            this.txt_topic.Location = new System.Drawing.Point(200, 91);
            this.txt_topic.Name = "txt_topic";
            this.txt_topic.Size = new System.Drawing.Size(386, 27);
            this.txt_topic.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(608, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 27);
            this.button1.TabIndex = 7;
            this.button1.Text = "初始化";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(698, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(411, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "如果首次或更换了url、topic，请先点击初始化，再进行发送";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(0, 144);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1376, 595);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_send);
            this.tabPage1.Controls.Add(this.rtb_message);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1368, 562);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "手工单条发送";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_send
            // 
            this.btn_send.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_send.Location = new System.Drawing.Point(666, 514);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(84, 26);
            this.btn_send.TabIndex = 9;
            this.btn_send.Text = "发 送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // rtb_message
            // 
            this.rtb_message.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_message.Location = new System.Drawing.Point(29, 36);
            this.rtb_message.Name = "rtb_message";
            this.rtb_message.Size = new System.Drawing.Size(1315, 470);
            this.rtb_message.TabIndex = 8;
            this.rtb_message.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "消息内容";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_clearLog);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.rtb_log);
            this.tabPage2.Controls.Add(this.ckb_showMessage);
            this.tabPage2.Controls.Add(this.btn_stop);
            this.tabPage2.Controls.Add(this.btn_start);
            this.tabPage2.Controls.Add(this.txt_interval);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.lbl_sheetInfo);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.cbb_sheet);
            this.tabPage2.Controls.Add(this.btn_beforeLoad);
            this.tabPage2.Controls.Add(this.txt_filePath);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1368, 562);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "读取文件发送";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_clearLog
            // 
            this.btn_clearLog.Location = new System.Drawing.Point(856, 101);
            this.btn_clearLog.Name = "btn_clearLog";
            this.btn_clearLog.Size = new System.Drawing.Size(89, 27);
            this.btn_clearLog.TabIndex = 16;
            this.btn_clearLog.Text = "清除日志";
            this.btn_clearLog.UseVisualStyleBackColor = true;
            this.btn_clearLog.Click += new System.EventHandler(this.btn_clearLog_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(304, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "数据请放在第0列";
            // 
            // rtb_log
            // 
            this.rtb_log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb_log.Location = new System.Drawing.Point(8, 150);
            this.rtb_log.Name = "rtb_log";
            this.rtb_log.Size = new System.Drawing.Size(1352, 388);
            this.rtb_log.TabIndex = 14;
            this.rtb_log.Text = "";
            // 
            // ckb_showMessage
            // 
            this.ckb_showMessage.AutoSize = true;
            this.ckb_showMessage.Location = new System.Drawing.Point(318, 101);
            this.ckb_showMessage.Name = "ckb_showMessage";
            this.ckb_showMessage.Size = new System.Drawing.Size(211, 24);
            this.ckb_showMessage.TabIndex = 13;
            this.ckb_showMessage.Text = "消息内容是否显示在日志中";
            this.ckb_showMessage.UseVisualStyleBackColor = true;
            // 
            // btn_stop
            // 
            this.btn_stop.Enabled = false;
            this.btn_stop.Location = new System.Drawing.Point(664, 101);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(89, 27);
            this.btn_stop.TabIndex = 12;
            this.btn_stop.Text = "停止发送";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(559, 101);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(99, 27);
            this.btn_start.TabIndex = 11;
            this.btn_start.Text = "开始发送";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // txt_interval
            // 
            this.txt_interval.Location = new System.Drawing.Point(190, 101);
            this.txt_interval.Name = "txt_interval";
            this.txt_interval.Size = new System.Drawing.Size(100, 27);
            this.txt_interval.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(159, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "每条发送间隔（毫秒）";
            // 
            // lbl_sheetInfo
            // 
            this.lbl_sheetInfo.AutoSize = true;
            this.lbl_sheetInfo.Location = new System.Drawing.Point(444, 54);
            this.lbl_sheetInfo.Name = "lbl_sheetInfo";
            this.lbl_sheetInfo.Size = new System.Drawing.Size(123, 20);
            this.lbl_sheetInfo.TabIndex = 8;
            this.lbl_sheetInfo.Text = "-------------------";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Sheet选择";
            // 
            // cbb_sheet
            // 
            this.cbb_sheet.FormattingEnabled = true;
            this.cbb_sheet.Location = new System.Drawing.Point(107, 51);
            this.cbb_sheet.Name = "cbb_sheet";
            this.cbb_sheet.Size = new System.Drawing.Size(191, 28);
            this.cbb_sheet.TabIndex = 6;
            this.cbb_sheet.SelectedIndexChanged += new System.EventHandler(this.cbb_sheet_SelectedIndexChanged);
            // 
            // btn_beforeLoad
            // 
            this.btn_beforeLoad.Location = new System.Drawing.Point(1156, 6);
            this.btn_beforeLoad.Name = "btn_beforeLoad";
            this.btn_beforeLoad.Size = new System.Drawing.Size(104, 32);
            this.btn_beforeLoad.TabIndex = 5;
            this.btn_beforeLoad.Text = "预加载文件";
            this.btn_beforeLoad.UseVisualStyleBackColor = true;
            this.btn_beforeLoad.Click += new System.EventHandler(this.btn_beforeLoad_Click);
            // 
            // txt_filePath
            // 
            this.txt_filePath.Location = new System.Drawing.Point(95, 7);
            this.txt_filePath.Name = "txt_filePath";
            this.txt_filePath.Size = new System.Drawing.Size(1038, 27);
            this.txt_filePath.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "文件路径";
            // 
            // btn_showIBM
            // 
            this.btn_showIBM.AutoSize = true;
            this.btn_showIBM.Location = new System.Drawing.Point(1196, 13);
            this.btn_showIBM.Name = "btn_showIBM";
            this.btn_showIBM.Size = new System.Drawing.Size(168, 30);
            this.btn_showIBM.TabIndex = 10;
            this.btn_showIBM.Text = "打开IBM MQ测试界面";
            this.btn_showIBM.UseVisualStyleBackColor = true;
            this.btn_showIBM.Click += new System.EventHandler(this.btn_showIBM_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1376, 739);
            this.Controls.Add(this.btn_showIBM);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txt_topic);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_url);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SODB产品模拟数据发送程序";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_url;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_topic;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.RichTextBox rtb_message;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_beforeLoad;
        private System.Windows.Forms.TextBox txt_filePath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbb_sheet;
        private System.Windows.Forms.Label lbl_sheetInfo;
        private System.Windows.Forms.CheckBox ckb_showMessage;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.TextBox txt_interval;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox rtb_log;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btn_clearLog;
        private System.Windows.Forms.Button btn_showIBM;
    }
}

