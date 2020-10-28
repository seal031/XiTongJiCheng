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
            this.axVSPOcxClient = new AxVSPOcxClientLib.AxVSPOcxClient();
            ((System.ComponentModel.ISupportInitialize)(this.axVSPOcxClient)).BeginInit();
            this.SuspendLayout();
            // 
            // axVSPOcxClient
            // 
            this.axVSPOcxClient.Enabled = true;
            this.axVSPOcxClient.Location = new System.Drawing.Point(0, 0);
            this.axVSPOcxClient.Name = "axVSPOcxClient";
            this.axVSPOcxClient.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVSPOcxClient.OcxState")));
            this.axVSPOcxClient.Size = new System.Drawing.Size(800, 600);
            this.axVSPOcxClient.TabIndex = 0;
            this.axVSPOcxClient.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 520);
            this.Controls.Add(this.axVSPOcxClient);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.axVSPOcxClient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxVSPOcxClientLib.AxVSPOcxClient axVSPOcxClient;
    }
}

