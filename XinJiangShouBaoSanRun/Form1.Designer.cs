namespace XinJiangShouBaoSanRun
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
            this.sdk = new AxVSPOcxClientLib.AxVSPOcxClient();
            ((System.ComponentModel.ISupportInitialize)(this.sdk)).BeginInit();
            this.SuspendLayout();
            // 
            // sdk
            // 
            this.sdk.Enabled = true;
            this.sdk.Location = new System.Drawing.Point(0, 0);
            this.sdk.Name = "sdk";
            this.sdk.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("sdk.OcxState")));
            this.sdk.Size = new System.Drawing.Size(800, 600);
            this.sdk.TabIndex = 0;
            this.sdk.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 520);
            this.Controls.Add(this.sdk);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.sdk)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxVSPOcxClientLib.AxVSPOcxClient sdk;
    }
}

