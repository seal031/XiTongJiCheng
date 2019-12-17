using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinJiangShouBao
{
    public partial class Form1 : Form
    {
        const string loginCmd = "login";
        const string setFormateCmd = "SetFormate";
        string localIp;
        int localPort;
        string remoteIp;
        int remotePort;
        string username;
        string password;
        TcpClientSession client;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (loadConfig() == false)
            {
                MessageBox.Show("配置文件中的配置项解析失败，请检查配置文件内容");
                return;
            }
            client = new AsyncTcpSession();
            client.LocalEndPoint = new IPEndPoint(IPAddress.Parse(localIp), localPort);
            client.Connected += Client_Connected;
            client.Closed += Client_Closed;
            client.DataReceived += Client_DataReceived;
            client.Error += Client_Error;
        }
        private bool loadConfig()
        {
            try
            {
                localIp = ConfigWorker.GetConfigValue("localIp");
                localPort = int.Parse(ConfigWorker.GetConfigValue("localPort"));
                remoteIp = ConfigWorker.GetConfigValue("remoteIp");
                remotePort = int.Parse(ConfigWorker.GetConfigValue("remotePort"));
                username = ConfigWorker.GetConfigValue("username");
                password = ConfigWorker.GetConfigValue("password");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void Client_Connected(object sender, EventArgs e)
        {
            FileWorker.LogHelper.WriteLog("已连接到服务器");
            login();
            setFormate();
        }

        private void Client_Closed(object sender, EventArgs e)
        {
            FileWorker.LogHelper.WriteLog("连接关闭");
        }

        private void Client_Error(object sender, ErrorEventArgs e)
        {
            FileWorker.LogHelper.WriteLog("client error" + e.Exception.Message);
        }

        private void Client_DataReceived(object sender, DataEventArgs e)
        {
            string message = MessageParser.byteToStr(e.Data);
            FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收的数据是" + message);
            Debug.WriteLine(message);
            if (message.Contains("防区报警"))
            {
                FileWorker.LogHelper.WriteLog("接收到报警" + message);
            }
            if (message.Contains("ACK Login"))
            {
                FileWorker.LogHelper.WriteLog("登录成功");
            }
        }

        private void login()
        {
            sendCmd(string.Format("{0} {1} {2}", loginCmd, username, password));
        }
        private void setFormate()
        {
            sendCmd(string.Format(setFormateCmd + " 1"));
        }

        private void sendCmd(string cmdStr)
        {
            try
            {
                byte[] loginBytes = MessageParser.strToByte(cmdStr);
                client.Send(loginBytes, 0, loginBytes.Length);
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("发送登录错误" + ex.Message);
            }
        }




    }



    public class DaHuaShowBao
    {

    }
}
