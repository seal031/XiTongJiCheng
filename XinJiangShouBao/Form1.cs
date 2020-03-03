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
        const string getUserCmd = "getUser";
        const string getUsersCmd = "getUsers";
        List<string> userIdList = new List<string>();
        List<DeviceEntity> deviceList = new List<DeviceEntity>();

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
            //var a = pickUserId("}}}{\"AlertUserList\":\"0001, 0002\"}寋\"ProcUserName\":\"admin\",\"LogGuid\":\"log_e2b9186fb4a443ef8953154315534906\",\"ProcResultType\":3,\"ProcResultTypeText\":\"预处理\",\"ProcContenet\":\"\"}");
            string tcpMessage = "!寋\"UserId\":1,\"UserNumber\":1,\"UserName\":\"IP7400\",\"UserType\":0,\"UserTypeText\":\"主机用户\",\"CanControl\":true,\"ZoneNumberList\":null,\"Enable\":true}#閧\"UserId\":1,\"UserNumber\":1,\"ZoneNumber\":\"9\",\"ElementId\":2,\"ZoneName\":\"防区9\",\"ZoneType\":0,\"ZoneTypeText\":\"即时防区\",\"MapElementImg\":\"/Images/ready.png\",\"DetectorType\":0,\"DetectorTypeText\":\"双鉴探测器\",\"CanControl\":true,\"Enable\":true}#雥\"UserId\":1,\"UserNumber\":1,\"ZoneNumber\":\"10\",\"ElementId\":3,\"ZoneName\":\"防区10\",\"ZoneType\":0,\"ZoneTypeText\":\"即时防区\",\"MapElementImg\":\"/Images/ready.png\",\"DetectorType\":0,\"DetectorTypeText\":\"双鉴探测器\",\"CanControl\":true,\"Enable\":true}#雥\"UserId\":1,\"UserNumber\":1,\"ZoneNumber\":\"11\",\"ElementId\":4,\"ZoneName\":\"防区11\",\"ZoneType\":0,\"ZoneTypeText\":\"即时防区\",\"MapElementImg\":\"/Images/ready.png\",\"DetectorType\":0,\"DetectorTypeText\":\"双鉴探测器\",\"CanControl\":true,\"Enable\":true}";
            var devices = tcpToDevices(tcpMessage);
            foreach (DeviceEntity device in devices)
            {
                pushDeviceEntity(device);
            }
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
        private void Form1_Shown(object sender, EventArgs e)
        {
            client.Connect(new IPEndPoint(IPAddress.Parse(remoteIp), remotePort));
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
            setFormate();
            login();
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
            try
            {
                //FileWorker.LogHelper.WriteLog("接收到了数据");
                string tcpMessage = MessageParser.byteToStr(e.Data);
                FileWorker.LogHelper.WriteLog("接收的数据是" + tcpMessage);
                string tcpMessageClean = tcpMessage.Replace("$", "").Replace("{", "").Replace("}", "").Replace("\"", "").Replace("\0", "");
                //Debug.WriteLine(tcpMessage);
                foreach (string subTcpMessage in tcpMessageClean.Split(new string[] { "IsDisposed" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (subTcpMessage.Contains("A202") && subTcpMessage.Contains("EventCode"))
                    {
                        AlarmEntity alarm = tcpToAlarm(subTcpMessage);
                        if (alarm != null)
                        {
                            if (alarm.body.alarmTime==null||alarm.body.alarmEquCode==null||alarm.body.alarmEquName==null)
                            {
                                FileWorker.LogHelper.WriteLog("通过tcp消息转换出的alarm对象信息不全："+subTcpMessage);
                            }
                            else
                            {
                                string message = alarm.toJson();
                                KafkaWorker.sendAlarmMessage(message);
                            }
                        }
                    }
                }
                if (tcpMessageClean.Contains("AlertUserList"))//获取用户列表的返回结果
                {
                    userIdList = pickUserId(tcpMessage);
                    foreach (string userId in userIdList)
                    {
                        getUser(userId.Trim());
                    }
                }
                //getuser返回的信息，即设备信息。包含某些关键字，但要与包含另外一些相似关键字的消息区分
                if (tcpMessageClean.Contains("ZoneTypeText") && tcpMessageClean.Contains("ZoneName") && tcpMessageClean.Contains("ZoneNumber") && tcpMessageClean.Contains("AlertZoneName") == false && tcpMessageClean.Contains("AlertZoneNumber") == false)
                {
                    var devices = tcpToDevices(tcpMessage);
                    foreach (DeviceEntity device in devices)
                    {
                        pushDeviceEntity(device);
                    }
                }
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("接收数据时出现异常："+ex.Message);
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
        private void getUsers()
        {
            sendCmd(getUsersCmd);
        }
        private void getUser(string userId)
        {
            sendCmd(string.Format("{0} {1}", getUserCmd, userId));
        }

        private void sendCmd(string cmdStr)
        {
            try
            {
                byte[] loginBytes = MessageParser.strToByte(cmdStr + "\r\n");
                client.Send(loginBytes, 0, loginBytes.Length);
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("发送登录错误" + ex.Message);
            }
        }


        public AlarmEntity tcpToAlarm(string tcpMsg)
        {
            AlarmEntity alarm = new AlarmEntity();
            foreach (string item in tcpMsg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                var kvpair = item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (kvpair.Length >= 2)
                {
                    switch (kvpair[0].Trim())
                    {
                        case "EventDateTime":
                            if (kvpair.Length >= 4)
                            {
                                alarm.body.alarmTime = kvpair[1].Trim() + ":" + kvpair[2].Trim() + ":" + kvpair[3].Trim();
                            }
                            break;
                        case "DeviceId":
                            alarm.body.alarmEquCode = kvpair[1].Trim();
                            break;
                        case "DeviceName":
                            alarm.body.alarmEquName = kvpair[1].Trim();
                            alarm.body.alarmName = kvpair[1].Trim();
                            break;
                        case "AlertZoneName":
                            alarm.body.alarmAddress = kvpair[1].Trim();
                            break;
                        case "EventCode":
                            if (kvpair[1].Trim() != "A202")
                            { return null; }
                            break;
                        case "EventName":
                            alarm.body.alarmDescibe = kvpair[1].Trim();
                            break;
                        default:
                            break;
                    }
                }
            }
            return alarm;
        }
        public List<string> pickUserId(string tcpMessage)
        {
            tcpMessage = tcpMessage.Replace("\"", "");
            List<string> userIdList = new List<string>();
            int startIndex = tcpMessage.IndexOf("AlertUserList") + "AlertUserList:".Length;
            int endIndex = tcpMessage.IndexOf("}", startIndex);
            string userIdStr = tcpMessage.Substring(startIndex, (endIndex - startIndex)).Trim();
            userIdList = userIdStr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            return userIdList;
        }
        private List<DeviceEntity> tcpToDevices(string tcpMessage)
        {
            tcpMessage = tcpMessage.Replace("\"", "");
            List<DeviceEntity> devices = new List<global::DeviceEntity>();
            foreach (string deviceStr in tcpMessage.Split(new string[] { "}" }, StringSplitOptions.RemoveEmptyEntries))
            {
                DeviceEntity device = new DeviceEntity();
                foreach (string item in deviceStr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    var kvpair = item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    if (kvpair.Length >= 2)
                    {
                        switch (kvpair[0].Trim())
                        {
                            case "ZoneNumber":
                                device.body.equCode = kvpair[1].Trim();
                                break;
                            case "ZoneName":
                                device.body.equName = kvpair[1].Trim();
                                break;
                            case "Enable"://Enable表示离线在线??

                                break;
                            default:
                                break;
                        }
                    }
                }
                if (device.body.equCode != null && device.body.equName != null)
                {
                    devices.Add(device);
                }
            }
            return devices;
        }
        private void pushDeviceEntity(DeviceEntity device)
        {
            var existDevice = deviceList.FirstOrDefault(d => d.body.equCode == device.body.equCode);
            if (existDevice == null)
            {
                deviceList.Add(device);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            KafkaWorker.sendAlarmMessage("1");
        }
    }



    public class DaHuaShowBao
    {

    }
}
