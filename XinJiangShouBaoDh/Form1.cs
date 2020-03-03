using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetClient;

namespace XinJiangShouBaoDh
{
    public partial class Form1 : Form
    {
        private AlarmClient client;
        private string ip;
        private ushort port;
        private string username;
        private string password;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (loadLocalConfig())
            {
                try
                {
                    client = new AlarmClient();
                    client.Login(ip, port, username, password);
                    client.AlarmReceived += Client_AlarmReceived;
                    client.DisConnectChanged += Client_DisConnectChanged;
                    client.StartListen();//开始监听
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    FileWorker.LogHelper.WriteLog("SDK调用错误：" + ex.Message);
                }
            }
        }

        private void Client_DisConnectChanged(object sender, DeviceEventArgs args)
        {
            FileWorker.LogHelper.WriteLog("连接已断开");
        }

        private void Client_AlarmReceived(object sender, AlarmEventArgs args)
        {
            if (args.AlarmType == EM_ALARM_TYPE.COMM_ALARM)
            {
                AlarmEntity alarm = new AlarmEntity();
                FileWorker.LogHelper.WriteLog(args.AlarmInfo.ToString());
                string message = alarm.toJson();
                KafkaWorker.sendAlarmMessage(message);
            }
        }

        private bool loadLocalConfig()
        {
            try
            {
                ip = ConfigWorker.GetConfigValue("remoteIp");
                string portStr = ConfigWorker.GetConfigValue("port");
                username = ConfigWorker.GetConfigValue("username");
                password = ConfigWorker.GetConfigValue("password");
                if (ushort.TryParse(portStr, out port)==false)
                {
                    MessageBox.Show("端口配置不正确，应为数字。");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取本地配置错误：" + ex.Message);
                return false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (client != null)
            {
                client.StopListen();
                client.Logout();
            }
        }
    }
}
