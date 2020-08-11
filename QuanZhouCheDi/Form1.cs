using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanZhouCheDi
{
    public partial class Form1 : Form
    {
        const int WM_COPYDATA = 0x004A;
        public Form1()
        {
            InitializeComponent();
        }
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                    Type mytype = mystr.GetType();
                    mystr = (COPYDATASTRUCT)m.GetLParam(mytype);
                    string[] messColl = mystr.lpData.Split(new char[] { '=' });
                    if (messColl[0] == "D00")
                    {
                        MessageBox.Show(mystr.lpData);
                        //FileWorker.LogHelper.WriteLog("接受车底信息记录:" + mystr.lpData);
                        //MessCommand messComm = Utils.ParseCarScanMess(messColl);
                    }
                    else if (messColl[0] == "D02")
                    {
                        MessageBox.Show(mystr.lpData);
                        if (messColl[1] == "0")
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(mystr.lpData);
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

    }
}
