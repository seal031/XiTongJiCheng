﻿namespace ShaoGuanShouBao
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
            this.SDK = new AxIPModuleLib.AxCooMonitor();
            ((System.ComponentModel.ISupportInitialize)(this.SDK)).BeginInit();
            this.SuspendLayout();
            // 
            // SDK
            // 
            this.SDK.Enabled = true;
            this.SDK.Location = new System.Drawing.Point(309, 104);
            this.SDK.Name = "SDK";
            this.SDK.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("SDK.OcxState")));
            this.SDK.Size = new System.Drawing.Size(240, 240);
            this.SDK.TabIndex = 0;
            this.SDK.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 485);
            this.Controls.Add(this.SDK);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.SDK)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxIPModuleLib.AxCooMonitor SDK;
    }
}

