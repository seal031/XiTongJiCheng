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
    public partial class Form1 : Form,IJobWorke
    {
        const int WM_COPYDATA = 0x004A;
        [DllImport("User32.dll")]
        public static extern int SendMessage(int hwnd, int msg, int wParam, ref COPYDATASTRUCT IParam);
        [DllImport("User32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        public Form1()
        {
            InitializeComponent();
            //string path = "C:\\Users\\40640\\Desktop\\zzz.txt";
            //string str = System.IO.File.ReadAllText(path, Encoding.Default);
            //int len = str.Length;
            //KafkaWorker.sendCarRecordMessage(str);
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
                        //MessageBox.Show(mystr.lpData);
                        FileWorker.LogHelper.WriteLog("接受车底信息记录D00:" + mystr.lpData);
                        MessCommand messComm = Utils.ParseCarScanMess(messColl);
                        string jsonMess = messComm.toJson();
                        KafkaWorker.sendCarRecordMessage(jsonMess);
                    }
                    else if (messColl[0] == "D01")
                    {
                        //FileWorker.LogHelper.WriteLog("基本工作状态D01:" + mystr.lpData);
                        //WorkingState work = Utils.ParseWorkMess(messColl);
                        //string jsonMess = work.toJson();
                        //KafkaTest.SendMessCommand(jsonMess);
                    }
                    else if (messColl[0] == "D02")
                    {
                        FileWorker.LogHelper.WriteLog("第3方自定义功能D02:" + mystr.lpData);
                        if (messColl[1] == "0")
                        {
                            this.Close();
                        }
                        else if (messColl[1] == "1")
                        {
                            this.TopMost = true;
                        }
                        else if (messColl[1] == "2")//D02 = 2 = N = EOF
                        {
                            //N 最大接入许可数量
                            //MessageOrder work = Utils.ParseMessOrder(messColl);
                            //上行,发送"U02=2=EOF"
                            SendResponOrder();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        FileWorker.LogHelper.WriteLog("未知命令:" + mystr.lpData);
                        //MessageBox.Show(mystr.lpData);
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        private void SendResponOrder()
        {
            int handle = FindWindow(null, "fmvsm100main");
            if (handle != 0)
            {
                byte[] byArr = Encoding.Default.GetBytes("U02=2=EOF");
                int len = byArr.Length;
                COPYDATASTRUCT cdata;
                cdata.cbData = len;
                cdata.dwData = (IntPtr)100;
                cdata.lpData = "U02=2=EOF";
                SendMessage(handle, WM_COPYDATA, 0, ref cdata);
            }
        }

        /// <summary>
        /// 定时任务，检查是否有
        /// </summary>
        public void circleWork()
        {
            
        }
    }
}
