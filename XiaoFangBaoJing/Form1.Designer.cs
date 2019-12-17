namespace XiaoFangBaoJing
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ip = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.num_slaveId = new System.Windows.Forms.NumericUpDown();
            this.num_startAddress = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.num_pointNumber = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.readHodingRegister = new System.Windows.Forms.Button();
            this.readInputRegister = new System.Windows.Forms.Button();
            this.btn_startJob = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.num_slaveId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_startAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_pointNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // txt_ip
            // 
            this.txt_ip.Location = new System.Drawing.Point(64, 43);
            this.txt_ip.Name = "txt_ip";
            this.txt_ip.Size = new System.Drawing.Size(253, 25);
            this.txt_ip.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(862, 558);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "SlaveID";
            // 
            // num_slaveId
            // 
            this.num_slaveId.Location = new System.Drawing.Point(121, 104);
            this.num_slaveId.Name = "num_slaveId";
            this.num_slaveId.Size = new System.Drawing.Size(68, 25);
            this.num_slaveId.TabIndex = 4;
            // 
            // num_startAddress
            // 
            this.num_startAddress.Location = new System.Drawing.Point(121, 158);
            this.num_startAddress.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.num_startAddress.Name = "num_startAddress";
            this.num_startAddress.Size = new System.Drawing.Size(146, 25);
            this.num_startAddress.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "StartAddress";
            // 
            // num_pointNumber
            // 
            this.num_pointNumber.Location = new System.Drawing.Point(121, 230);
            this.num_pointNumber.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.num_pointNumber.Name = "num_pointNumber";
            this.num_pointNumber.Size = new System.Drawing.Size(146, 25);
            this.num_pointNumber.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "PointNumber";
            // 
            // readHodingRegister
            // 
            this.readHodingRegister.Location = new System.Drawing.Point(15, 275);
            this.readHodingRegister.Name = "readHodingRegister";
            this.readHodingRegister.Size = new System.Drawing.Size(131, 23);
            this.readHodingRegister.TabIndex = 9;
            this.readHodingRegister.Text = "读保持寄存器";
            this.readHodingRegister.UseVisualStyleBackColor = true;
            this.readHodingRegister.Visible = false;
            this.readHodingRegister.Click += new System.EventHandler(this.btn_readHodingRegister_Click);
            // 
            // readInputRegister
            // 
            this.readInputRegister.Location = new System.Drawing.Point(163, 275);
            this.readInputRegister.Name = "readInputRegister";
            this.readInputRegister.Size = new System.Drawing.Size(130, 23);
            this.readInputRegister.TabIndex = 10;
            this.readInputRegister.Text = "读输入寄存器";
            this.readInputRegister.UseVisualStyleBackColor = true;
            this.readInputRegister.Visible = false;
            this.readInputRegister.Click += new System.EventHandler(this.btn_readInputRegister_Click);
            // 
            // btn_startJob
            // 
            this.btn_startJob.Location = new System.Drawing.Point(101, 315);
            this.btn_startJob.Name = "btn_startJob";
            this.btn_startJob.Size = new System.Drawing.Size(111, 24);
            this.btn_startJob.TabIndex = 11;
            this.btn_startJob.Text = "开始接入";
            this.btn_startJob.UseVisualStyleBackColor = true;
            this.btn_startJob.Click += new System.EventHandler(this.btn_startJob_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(862, 558);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btn_startJob);
            this.Controls.Add(this.readInputRegister);
            this.Controls.Add(this.readHodingRegister);
            this.Controls.Add(this.num_pointNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.num_startAddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.num_slaveId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_ip);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "消防报警接入程序";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_slaveId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_startAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_pointNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ip;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown num_slaveId;
        private System.Windows.Forms.NumericUpDown num_startAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown num_pointNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button readHodingRegister;
        private System.Windows.Forms.Button readInputRegister;
        private System.Windows.Forms.Button btn_startJob;
    }
}

