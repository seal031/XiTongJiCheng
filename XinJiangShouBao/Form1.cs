using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
        string airportIata;
        string airportName;
        int reconnectInterval=30;
        TcpClientSession client;

        private volatile bool isConnected = false;
        private volatile bool isReconecting = false;

        SpinLock sl = new SpinLock(false);
        bool gotLock = false;

        public Form1()
        {
            InitializeComponent();
            //var a = pickUserId("}}}{\"AlertUserList\":\"0001, 0002\"}寋\"ProcUserName\":\"admin\",\"LogGuid\":\"log_e2b9186fb4a443ef8953154315534906\",\"ProcResultType\":3,\"ProcResultTypeText\":\"预处理\",\"ProcContenet\":\"\"}");
            //string tcpMessage = "!寋\"UserId\":1,\"UserNumber\":1,\"UserName\":\"IP7400\",\"UserType\":0,\"UserTypeText\":\"主机用户\",\"CanControl\":true,\"ZoneNumberList\":null,\"Enable\":true}#閧\"UserId\":1,\"UserNumber\":1,\"ZoneNumber\":\"9\",\"ElementId\":2,\"ZoneName\":\"防区9\",\"ZoneType\":0,\"ZoneTypeText\":\"即时防区\",\"MapElementImg\":\"/Images/ready.png\",\"DetectorType\":0,\"DetectorTypeText\":\"双鉴探测器\",\"CanControl\":true,\"Enable\":true}#雥\"UserId\":1,\"UserNumber\":1,\"ZoneNumber\":\"10\",\"ElementId\":3,\"ZoneName\":\"防区10\",\"ZoneType\":0,\"ZoneTypeText\":\"即时防区\",\"MapElementImg\":\"/Images/ready.png\",\"DetectorType\":0,\"DetectorTypeText\":\"双鉴探测器\",\"CanControl\":true,\"Enable\":true}#雥\"UserId\":1,\"UserNumber\":1,\"ZoneNumber\":\"11\",\"ElementId\":4,\"ZoneName\":\"防区11\",\"ZoneType\":0,\"ZoneTypeText\":\"即时防区\",\"MapElementImg\":\"/Images/ready.png\",\"DetectorType\":0,\"DetectorTypeText\":\"双鉴探测器\",\"CanControl\":true,\"Enable\":true}";
            //var devices = tcpToDevices(tcpMessage);
            //foreach (DeviceEntity device in devices)
            //{
            //    pushDeviceEntity(device);
            //}
            //var b = "{ \"ProcUserName\":\"admin\",\"LogGuid\":\"log_bf9ff2e9d72b4f16bcb75eee0bb93f3a\",\"ProcResultType\":3,\"ProcResultTypeText\":\"预处理\",\"ProcContenet\":\"\"}onState\":0,\"PartitionStateText\":\"报警\",\"PartitionStateCate\":0,\"PartionStateImage\":\"/Images/StateRed.png\"}e\":\"IP7400\",\"EventCode\":\"A203\",\"EventName\":\"防区报警恢复\",\"DeviceCode\":\"\",\"AlertUserId\":1,\"AlertUserName\":\"IP7400\",\"AlertUserNumber\":\"0001\",\"AlertUserCode\":\"\",\"AlertZoneId\":5,\"AlertZoneName\":\"防区12\",\"AlertZoneNumber\":\"12\",\"AlertUserType\":0,\"AlertZoneType\":0,\"AlertDetectorType\":0,\"PartitionNumber\":1,\"Telephone\":\"\",\"DoorCardUserId\":0,\"DoorCardUserName\":null,\"DoorElementId\":0,\"DoorName\":null,\"DoorNumber\":0,\"DoorType\":-1,\"DoorCardStyle\":-1,\"DoorCardNumber\":\"\",\"DoorLockNumber\":\"\",\"ChannelId\":0,\"ChannelName\":null,\"ChannelNumber\":\"\",\"Position\":\"\",\"SystemCate\":1,\"SysProtocolCate\":3,\"ProcStyle\":0,\"ProcResultCate\":-1,\"ProcUserId\":0,\"ProcUserName\":\"\",\"ProcContent\":\"\",\"OtherInfo\":\"\",\"MachineName\":\"DESKTOP-LJ18965\",\"Enable\":true,\"LogGuid\":\"log_c3c6112b2f0c42fcb90c0edd2ab06308\",\"ParamId\":0,\"ParamName\":null,\"ParamValueType\":0,\"ParamValue\":null,\"ParamOkValue\":null,\"ResFlage\":\"12=1\",\"StationCode\":null,\"StationId\":0,\"SyncFlage\":0}serAlarm.gif\",\"PartitionState\":0,\"PartitionStateText\":\"报警\",\"PartitionStateCate\":0,\"PartionStateImage\":\"/Images/StateRed.png\"}";
            //var alarm = AlarmParseTool.parseAlarm(b, "A203", "KCA", "库车");
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
            connect();    
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
                airportIata = ConfigWorker.GetConfigValue("airportIata");
                airportName = ConfigWorker.GetConfigValue("airportName");
                reconnectInterval = int.Parse(ConfigWorker.GetConfigValue("reconnectInterval"));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void connect()
        {
            try
            {
                if (sl.IsHeld == false)
                {
                    Debug.WriteLine("开始连接服务端");
                    try
                    {
                        gotLock = false;
                        Debug.WriteLine("尝试获取锁");
                        sl.TryEnter(reconnectInterval * 1000, ref gotLock);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                    client.Connect(new IPEndPoint(IPAddress.Parse(remoteIp), remotePort));
                }
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("连接时出现异常：" + ex.Message);
                Debug.WriteLine("连接时出现异常：" + ex.Message);
            }
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("连接成功，释放锁");
                sl.Exit(true);
            }
            catch (Exception ex)
            {
                
            }
            isConnected = true;
            isReconecting = false;
            FileWorker.LogHelper.WriteLog("已连接到服务器");
            Debug.WriteLine("已连接到服务器");
            setFormate();
            login();
        }

        private void Client_Closed(object sender, EventArgs e)
        {
            FileWorker.LogHelper.WriteLog("连接关闭");
            Debug.WriteLine("连接关闭");
            isConnected = false;
            if (isReconecting == false) 
            {
                Task.Run(() =>
                {
                    reConnect();
                });
            }
        }

        private void Client_Error(object sender, ErrorEventArgs e)
        {
            try
            {
                Debug.WriteLine("client error，释放锁");
                sl.Exit(true);
            }
            catch (Exception ex)
            {

            }
            FileWorker.LogHelper.WriteLog("client error" + e.Exception.Message);
            Debug.WriteLine("client error" + e.Exception.Message);
            isConnected = false;
            if (isReconecting == false)
            {
                Task.Run(() =>
                {
                    reConnect();
                });
            }
        }

        private void reConnect()
        {
            FileWorker.LogHelper.WriteLog("执行断线重连");
            Debug.WriteLine("执行断线重连");
            isReconecting = true;
            while (true)
            {
                if (isConnected)
                {
                    Debug.WriteLine("重连成功，退出重连");
                    FileWorker.LogHelper.WriteLog("重连成功，退出重连");
                    break;
                }
                connect();
                if (isConnected)
                {
                    Debug.WriteLine("重连成功，退出重连");
                    FileWorker.LogHelper.WriteLog("重连成功，退出重连");
                    break;
                }
                Thread.Sleep(reconnectInterval * 1000);
            }
        }

        private void Client_DataReceived(object sender, DataEventArgs e)
        {
            try
            {
                //FileWorker.LogHelper.WriteLog("接收到了数据");
                string tcpMessage = MessageParser.byteToStr(e.Data);
                FileWorker.LogHelper.WriteLog("接收的数据是" + tcpMessage);
                Debug.WriteLine("接收的数据是" + tcpMessage);
                string tcpMessageClean = tcpMessage.Replace("$", "").Replace("{", "").Replace("}", "").Replace("\"", "").Replace("\0", "");
                //Debug.WriteLine(tcpMessage);
                foreach (string subTcpMessage in tcpMessageClean.Split(new string[] { "IsDisposed" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    AlarmEntity alarm = null;
                    DeviceStateEntity deviceState = null;
                    if (subTcpMessage.Contains("A202") && subTcpMessage.Contains("EventCode"))//防区报警
                    {
                        alarm = AlarmParseTool.parseAlarm(subTcpMessage, "A202", airportIata, airportName);
                    }
                    else if (subTcpMessage.Contains("A203") && subTcpMessage.Contains("EventCode"))//防区报警恢复
                    {
                        alarm = AlarmParseTool.parseAlarm(subTcpMessage, "A203", airportIata, airportName);
                    }
                    else if (subTcpMessage.Contains("A206") && subTcpMessage.Contains("EventCode"))//防区故障
                    {
                        alarm = AlarmParseTool.parseAlarm(subTcpMessage, "A206", airportIata, airportName);
                        deviceState = AlarmParseTool.parseDeviceState(subTcpMessage, "A206",airportIata);
                    }
                    else if (subTcpMessage.Contains("A207") && subTcpMessage.Contains("EventCode"))//防区故障恢复
                    {
                        alarm = AlarmParseTool.parseAlarm(subTcpMessage, "A207", airportIata, airportName);
                        deviceState = AlarmParseTool.parseDeviceState(subTcpMessage, "A207",airportIata);
                    }
                    else if (subTcpMessage.Contains("A208") && subTcpMessage.Contains("EventCode"))//防区防拆
                    {
                        alarm = AlarmParseTool.parseAlarm(subTcpMessage, "A208", airportIata, airportName);
                    }
                    else if (subTcpMessage.Contains("A209") && subTcpMessage.Contains("EventCode"))//防区防拆恢复
                    {
                        alarm = AlarmParseTool.parseAlarm(subTcpMessage, "A209", airportIata, airportName);
                    }
                    if (alarm != null)
                    {
                        if (alarm.body.alarmTime == null || alarm.body.alarmEquCode == null)
                        {
                            FileWorker.LogHelper.WriteLog("通过tcp消息转换出的alarm对象信息不全：" + subTcpMessage);
                        }
                        else
                        {
                            KafkaWorker.sendAlarmMessage(alarm.toJson());
                            if (deviceState != null)
                            {
                                KafkaWorker.sendDeviceMessage(deviceState.toJson());
                            }
                            return;
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
                Debug.WriteLine("接收数据时出现异常：" + ex.Message);
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
