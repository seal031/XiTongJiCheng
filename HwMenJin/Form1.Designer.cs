namespace HwMenJin
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
            this.axHSCEventSDK1 = new AxSMSDKPWEventInfo.AxHSCEventSDK();
            this.axHSCReaderSDK1 = new AxSMSDKPWReaderControl.AxHSCReaderSDK();
            ((System.ComponentModel.ISupportInitialize)(this.axHSCEventSDK1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axHSCReaderSDK1)).BeginInit();
            this.SuspendLayout();
            // 
            // axHSCEventSDK1
            // 
            this.axHSCEventSDK1.Enabled = true;
            this.axHSCEventSDK1.Location = new System.Drawing.Point(195, 143);
            this.axHSCEventSDK1.Name = "axHSCEventSDK1";
            this.axHSCEventSDK1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axHSCEventSDK1.OcxState")));
            this.axHSCEventSDK1.Size = new System.Drawing.Size(41, 44);
            this.axHSCEventSDK1.TabIndex = 0;
            this.axHSCEventSDK1.NewEvent += new System.EventHandler(this.axHSCEventSDK1_NewEvent);
            // 
            // axHSCReaderSDK1
            // 
            this.axHSCReaderSDK1.Enabled = true;
            this.axHSCReaderSDK1.Location = new System.Drawing.Point(640, 198);
            this.axHSCReaderSDK1.Name = "axHSCReaderSDK1";
            this.axHSCReaderSDK1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axHSCReaderSDK1.OcxState")));
            this.axHSCReaderSDK1.Size = new System.Drawing.Size(56, 48);
            this.axHSCReaderSDK1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 359);
            this.Controls.Add(this.axHSCReaderSDK1);
            this.Controls.Add(this.axHSCEventSDK1);
            this.Name = "Form1";
            this.Text = "门禁数据接入程序";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.axHSCEventSDK1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axHSCReaderSDK1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxSMSDKPWEventInfo.AxHSCEventSDK axHSCEventSDK1;
        private AxSMSDKPWReaderControl.AxHSCReaderSDK axHSCReaderSDK1;
    }
}

