
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangChunShouBao
{
    public partial class Form1 : Form
    {
        string localIp;
        int localPort;
        string serverIp;
        int serverPort;
        UdpClient udpcRecv;
        Thread thrRecv;

        CloseForm cf = new CloseForm();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //a();
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            localIp = ConfigWorker.GetConfigValue("localIp");
            string localPortStr = ConfigWorker.GetConfigValue("localPort");
            serverIp = ConfigWorker.GetConfigValue("serverIp");
            string serverPortStr = ConfigWorker.GetConfigValue("serverPort");
            if (int.TryParse(localPortStr, out localPort) == false)
            {
                FileWorker.LogHelper.WriteLog("本地端口设置不正确");
                MessageBox.Show("本地端口设置不正确");
                return;
            }
            else if (int.TryParse(serverPortStr, out serverPort) == false)
            {
                FileWorker.LogHelper.WriteLog("服务器端口设置不正确");
                MessageBox.Show("服务器端口设置不正确");
                return;
            }
            else
            {
                startListener();
            }
        }

        private void startListener()
        {
            try
            {
                IPEndPoint localIpep = new IPEndPoint(IPAddress.Parse(localIp), localPort); // 本机IP和监听端口号  
                udpcRecv = new UdpClient(localIpep);
                thrRecv = new Thread(ReceiveMessage);
                thrRecv.Start();
                FileWorker.LogHelper.WriteLog("UDP监听线程已启动");
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("UDP监听线程启动失败");
            }
        }

        private void ReceiveMessage(object obj)
        {
            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);
            while (true)
            {
                try
                {
                    byte[] bytRecv = udpcRecv.Receive(ref remoteIpep);
                    string bytRecvStr = string.Join(" ", bytRecv);
                    FileWorker.LogHelper.WriteLog("接收到的原始数据是：" + bytRecvStr);
                    string msg = Encoding.Default.GetString(bytRecv);
                    FileWorker.LogHelper.WriteLog("原始数据解析后是：" + msg);

                    AlarmEntity alarm = udpToAlarm(msg);
                    string messageAlarm = alarm.toJson();
                    KafkaWorker.sendAlarmMessage(messageAlarm);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }
            }
        }

        private AlarmEntity udpToAlarm(string udpMessage)
        {
            AlarmEntity alarm = new AlarmEntity();
            var itemArray = udpMessage.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in itemArray)
            {
                var kvPair=item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (kvPair.Length >= 2)
                {
                    switch (kvPair[0])
                    {
                        case "日期":
                            alarm.body.alarmTime = kvPair[1]+" ";
                            break;
                        case "时间":
                            if (kvPair.Length >=4)
                            {
                                alarm.body.alarmTime += kvPair[1] + ":" + kvPair[2] + ":" + kvPair[3];
                            }
                            break;
                        case "用户编号":
                            alarm.body.alarmEquCode = kvPair[1].Replace("\r\n","");
                            break;
                        case "用户名称":
                            alarm.body.alarmEquName = kvPair[1].Replace("\r\n", "");
                            break;
                        case "用户防区":
                            alarm.body.alarmEquCode += "-" + kvPair[1].Replace("\r\n", "");
                            break;
                        case "警情":
                            alarm.body.alarmDescibe = kvPair[1].Replace("\r\n", "");
                            break;
                        case "通道描述":
                            alarm.body.alarmAddress = kvPair[1].Replace("\r\n", "");
                            break;
                        default:
                            break;
                    }
                }
            }
            return alarm;
        }

        void a()
        {
            byte[] packet_bytes = new byte[]{
  0xd0, 0xc5, 0xcf, 0xa2, 0x20, 0x0d, 0x0a, 0x0d,
  0x0a, 0x20, 0xc8, 0xd5, 0xc6, 0xda, 0x3a, 0x32,
  0x30, 0x31, 0x39, 0x2d, 0x31, 0x32, 0x2d, 0x30,
  0x36, 0x20, 0x20, 0x20, 0x20, 0x20, 0xca, 0xb1,
  0xbc, 0xe4, 0x3a, 0x31, 0x36, 0x3a, 0x33, 0x37,
  0x3a, 0x31, 0x32, 0x20, 0x20, 0x20, 0x20, 0x20,
  0x20, 0xd3, 0xc3, 0xbb, 0xa7, 0xb1, 0xe0, 0xba,
  0xc5, 0x3a, 0x30, 0x30, 0x31, 0x30, 0x20, 0x20,
  0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
  0x20, 0xd3, 0xc3, 0xbb, 0xa7, 0xc3, 0xfb, 0xb3,
  0xc6, 0x3a, 0x49, 0x44, 0x36, 0x61, 0x62, 0x63,
  0x41, 0x42, 0x43, 0xb5, 0xab, 0xca, 0xc7, 0x0d,
  0x0a, 0x20, 0xd3, 0xc3, 0xbb, 0xa7, 0xb7, 0xc0,
  0xc7, 0xf8, 0x3a, 0x30, 0x30, 0x31, 0x20, 0x20,
  0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20,
  0x20, 0x20, 0xbe, 0xaf, 0xc7, 0xe9, 0x3a, 0xbd,
  0xf4, 0xbc, 0xb1, 0xc7, 0xf3, 0xd6, 0xfa, 0xb1,
  0xa8, 0xbe, 0xaf, 0x20, 0x20, 0x31, 0x32, 0x3a,
  0xb0, 0xb2, 0xbc, 0xec, 0x36, 0xba, 0xc5, 0xd1,
  0xe9, 0xd6, 0xa4, 0x0d, 0x0a, 0x20, 0x5f, 0x5f,
  0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f,
  0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f,
  0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f,
  0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f,
  0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f,
  0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f,
  0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f,
  0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f,
  0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f,
  0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f,
  0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f, 0x5f,
  0x5f
};

            byte[] packet_bytes1 = new byte[] {211,195,187,167,177,168,190,175,207,234,207,184,208,197,207,162,32,13,10,13,10,32,200,213,198,218,58,50,48,49,57,45,49,50,45,49,48,32,32,32,32,32,202,177,188,228,58,49,53,58,51,50,58,51,56,32,32,32,32,32,32,211,195,187,167,177,224,186,197,58,49,49,50,57,32,32,32,32,32,32,32,32,32,32,32,211,195,187,167,195,251,179,198,58,50,45,55,197,228,207,223,188,228,177,168,190,175,214,247,187,250,13,10,32,211,195,187,167,183,192,199,248,58,48,48,52,32,32,32,32,32,32,32,32,32,32,32,32,190,175,199,233,58,189,244,188,177,199,243,214,250,177,168,190,175,32,205,168,181,192,195,232,202,246,58,49,55,35,176,178,188,236,205,168,181,192,209,233,214,164,185,241,204,168,13,10,32,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95,95};


            byte[] bytRecvHex = new byte[packet_bytes1.Length];
            //for (int i = 0; i < packet_bytes1.Length; i++)
            //{
            //    int d = NumberTrans.decToHex(packet_bytes1[i]);
            //    bytRecvHex[i] = (byte)d;
            //}
            string msg = System.Text.Encoding.Default.GetString(packet_bytes1);
            AlarmEntity alarm = udpToAlarm(msg);
            string message = alarm.toJson();
            //int length = msg.Length;
            //var str =(cutSubstring(msg, length));

        }

        private static string cutSubstring(string str, int length)
        {
            if (str == null || str.Length == 0 || length < 0)
            {
                return "";
            }

            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(str);
            int n = 0;  //  表示当前的字节数
            int i = 0;  //  要截取的字节数
            for (; i < bytes.GetLength(0) && n < length; i++)
            {
                //  偶数位置，如0、2、4等，为UCS2编码中两个字节的第一个字节
                if (i % 2 == 0)
                {
                    n++;      //  在UCS2第一个字节时n加1
                }
                else
                {
                    //  当UCS2编码的第二个字节大于0时，该UCS2字符为汉字，一个汉字算两个字节
                    if (bytes[i] > 0)
                    {
                        n++;
                    }
                }
            }
            //  如果i为奇数时，处理成偶数
            if (i % 2 == 1)
            {
                //  该UCS2字符是汉字时，去掉这个截一半的汉字
                if (bytes[i] > 0)
                    i = i - 1;
                //  该UCS2字符是字母或数字，则保留该字符
                else
                    i = i + 1;
            }
            return System.Text.Encoding.Unicode.GetString(bytes, 0, i);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                notifyIcon1.Visible = false;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                notifyIcon1.Visible = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            //{
            //    // 关闭所有的线程
            //    this.Dispose();
            //    this.Close();
            //}
            //else
            //{
            //    e.Cancel = true;
            //}
            if (cf.canClose == false)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                notifyIcon1.Visible = true;
                e.Cancel = true;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cf = new CloseForm();
            cf.Show();
        }
    }
}
