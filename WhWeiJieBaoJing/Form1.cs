using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhWeiJieBaoJing
{
    public partial class Form1 : Form
    {
        string localIp;
        int localPort;
        string remoteIp;
        List<int> remotePortList;
        Dictionary<int,TcpClientSession> clientDic;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var alarmEntity= AlarmParseTool.parseAlarm("<ROOT><Event logid=\"1170\" time=\"2019 / 09 / 21 13:54:14\" mdlid=\"1006\" code=\"1\" cid=\"130\" grade=\"3\" desc=\"报警\" mrk=\"防区报警\" mdlname=\"综合管廊\\AF 08\\VICTRIX - 8_172.28.161.207\\VICTRIX - 防区_4\"/></ROOT>");
            //string message = alarmEntity.toJson();
            //KafkaWorker.sendAlarmMessage(message);
            if (loadConfig() == false)
            {
                MessageBox.Show("配置文件中的配置项解析失败，请检查配置文件内容");
                return;
            }
            foreach (int remotePort in remotePortList)
            {
                TcpClientSession client = new AsyncTcpSession();
                client.LocalEndPoint = new IPEndPoint(IPAddress.Parse(localIp), localPort);
                client.Connected += Client_Connected;
                client.Closed += Client_Closed;
                client.DataReceived += Client_DataReceived;
                client.Error += Client_Error;
                clientDic.Add(remotePort,client);
            }
            
            connectToServer();
        }

        private bool loadConfig()
        {
            try
            {
                localIp = ConfigWorker.GetConfigValue("localIp");
                localPort = int.Parse(ConfigWorker.GetConfigValue("localPort"));
                remoteIp = ConfigWorker.GetConfigValue("remoteIp");
                string[] remotePortListStr = ConfigWorker.GetConfigValue("remotePort").Split(new string[] { ","},StringSplitOptions.RemoveEmptyEntries);
                foreach (string remotePortStr in remotePortListStr)
                {
                    int remotePort = int.Parse(remotePortStr);
                    remotePortList.Add(remotePort);
                }
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
            if (message.Contains("防区报警"))
            {
                FileWorker.LogHelper.WriteLog("接收到报警" + message);
                var alarmEntity = AlarmParseTool.parseAlarm(message);
                if (alarmEntity != null)
                {
                    string alarmMessage = alarmEntity.toJson();
                    FileWorker.LogHelper.WriteLog("向kafka发送报警" + alarmMessage);
                    KafkaWorker.sendAlarmMessage(alarmMessage);
                }
            }
            if (message.Contains("ACK Login"))
            {
                FileWorker.LogHelper.WriteLog("登录成功");
            }
        }


        private bool connectToServer()
        {
            foreach (int remotePort in remotePortList)
            {
                TcpClientSession client = clientDic[remotePort];
                if (client != null)
                {
                    try
                    {
                        client.Connect(new IPEndPoint(IPAddress.Parse(remoteIp), remotePort));
                    }
                    catch (Exception ex)
                    {
                        FileWorker.LogHelper.WriteLog("登陆服务器错误，端口:"+remotePort+"  " + ex.Message);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
