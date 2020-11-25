using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinJiangShouBaoSanRun
{
    public partial class Form2 : Form
    {
        ChromiumWebBrowser wb;

        public Form2()
        {
            InitializeComponent();
            string SDKClientWebPage = ConfigWorker.GetConfigValue("SDKClientWebPage");
            wb = new ChromiumWebBrowser(SDKClientWebPage);
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            wb.JavascriptObjectRepository.Register("chromeWb", new JsCallbackManager(), true);
            JsCallbackManager.setBrowser(wb);
            wb.Dock = DockStyle.Fill;
            this.Controls.Add(wb);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            wb.Dispose();
            wb = null;
        }
    }
}
