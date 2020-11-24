using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinJiangShouBaoSanRun
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            CefSharpSettings.SubprocessExitIfParentProcessClosed = true;
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            Cef.EnableHighDPISupport();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
