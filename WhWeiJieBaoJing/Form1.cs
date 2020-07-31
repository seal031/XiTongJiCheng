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

namespace WhWeiJieBaoJing
{
    public partial class Form1 : Form
    {
        string localIp;
        int localPort;
        string remoteIp;
        List<int> remotePortList = new List<int>();
        Dictionary<int, TcpClientSession> clientDic = new Dictionary<int, TcpClientSession>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var alarmEntity= AlarmParseTool.parseAlarm("<ROOT><Event logid=\"1170\" time=\"2019 / 09 / 21 13:54:14\" mdlid=\"1006\" code=\"1\" cid=\"130\" grade=\"3\" desc=\"报警\" mrk=\"防区报警\" mdlname=\"综合管廊\\AF 08\\VICTRIX - 8_172.28.161.207\\VICTRIX - 防区_4\"/></ROOT>");
            //string message = alarmEntity.toJson();
            //KafkaWorker.sendAlarmMessage(message);
            var dec = MessageParser.HexToDec("0f");
            if (loadConfig() == false)
            {
                MessageBox.Show("配置文件中的配置项解析失败，请检查配置文件内容");
                return;
            }
            foreach (int remotePort in remotePortList)
            {
                TcpClientSession client = new AsyncTcpSession();
                client.LocalEndPoint = new IPEndPoint(IPAddress.Parse(localIp), remotePort+20000);
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
            FileWorker.LogHelper.WriteLog("连接错误:" + e.Exception.Message);
        }

        private void Client_DataReceived(object sender, DataEventArgs e)
        {
            //入侵：06
            //防拆：04
            //故障：03
            //网络：03
            var remote = (sender as TcpClientSession);
            var port = clientDic.FirstOrDefault(c => c.Value == remote).Key;
            string nm = string.Empty;
            switch (port)
            {
                case 850:
                    nm = "NM1-";
                    break;
                case 851:
                    nm = "NM2-";
                    break;
                case 852:
                    nm = "NM3-";
                    break;
                case 853:
                    nm = "NM4-";
                    break;
                default:
                    break;
            }
            string message = MessageParser.byteToHex(e.Data);
            if (message.Substring(4, 2) == "01")//忽略心跳
                return;
            FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收的数据是:" + message.Substring(0, 40));
            string modelNo_hex = message.Substring(10, 2);
            string modelNo_dec = MessageParser.HexToDec(modelNo_hex);
            if (modelNo_dec == string.Empty) return;//如果设备编号无法转换为10进制，返回.
            string modelNo = nm + modelNo_dec;
            string sideNo = string.Empty;
            string equCode = string.Empty;
            if (message.Substring(8, 2) == "06")//入侵
            {
                switch (message[15])//确定防区A/B
                {
                    case '3':
                        sideNo = "A";
                        break;
                    case '4':
                        sideNo = "B";
                        break;
                    default:
                        return;
                }
                equCode = modelNo + "-" + sideNo;
                if (message.Substring(18, 2) == "01")//入侵报警
                {
                    FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收入侵报警" + equCode);
                    AlarmEntity alarm = AlarmParseTool.parseAlarm(equCode, "AC0601", "AS01");
                    KafkaWorker.sendAlarmMessage(alarm.toJson());
                    return;
                }
                if (message.Substring(18, 2) == "00")//入侵消警
                {
                    FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收入侵消警" + equCode);
                    AlarmEntity alarm = AlarmParseTool.parseAlarm(equCode, "AC0601", "AS02");
                    KafkaWorker.sendAlarmMessage(alarm.toJson());
                    return;
                }
            }
            if (message.Substring(8, 2) == "03")//故障
            {
                if (message.Substring(34, 2) == "0f")//由离线至某防区恢复
                {
                    switch (message.Substring(16, 2))//确定防区A/B
                    {
                        case "01":
                            sideNo = "B";//显示01时说明B恢复了
                            equCode = modelNo + "-" + sideNo;
                            FileWorker.LogHelper.WriteLog(Environment.NewLine + "主机在线，接收故障消警" + equCode);
                            //故障消警
                            AlarmEntity alarm = AlarmParseTool.parseAlarm(equCode, "AC0603", "AS02");
                            KafkaWorker.sendAlarmMessage(alarm.toJson());
                            //离线消警
                            AlarmEntity alarm_offline = AlarmParseTool.parseAlarm(modelNo, "AC0604", "AS02");
                            KafkaWorker.sendAlarmMessage(alarm_offline.toJson());
                            //设备状态上线
                            DeviceStateEntity deviceState = AlarmParseTool.parseDeviceState(modelNo,"ES01");
                            KafkaWorker.sendDeviceMessage(deviceState.toJson());
                            return;
                        case "02":
                            sideNo = "A";//显示02时说明A恢复了
                            equCode = modelNo + "-" + sideNo;
                            FileWorker.LogHelper.WriteLog(Environment.NewLine + "主机在线，接收故障消警" + equCode);
                            //故障消警
                            AlarmEntity alarm1 = AlarmParseTool.parseAlarm(equCode, "AC0603", "AS02");
                            KafkaWorker.sendAlarmMessage(alarm1.toJson());
                            //离线消警
                            AlarmEntity alarm_offline1 = AlarmParseTool.parseAlarm(modelNo, "AC0604", "AS02");
                            KafkaWorker.sendAlarmMessage(alarm_offline1.toJson());
                            //设备状态上线
                            DeviceStateEntity deviceState1 = AlarmParseTool.parseDeviceState(modelNo, "ES01");
                            KafkaWorker.sendDeviceMessage(deviceState1.toJson());
                            return;
                        default:
                            break;
                    }
                }
                else//由正常至防区故障
                {
                    switch (message.Substring(16, 2))//确定防区A/B
                    {
                        case "01":
                            sideNo = "A";
                            equCode = modelNo + "-" + sideNo;
                            FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收故障报警" + equCode);
                            // 故障报警
                            AlarmEntity alarm = AlarmParseTool.parseAlarm(equCode, "AC0603", "AS01");
                            KafkaWorker.sendAlarmMessage(alarm.toJson());
                            return;
                        case "02":
                            sideNo = "B";
                            equCode = modelNo + "-" + sideNo;
                            FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收故障报警" + equCode);
                            // 故障报警
                            AlarmEntity alarm1 = AlarmParseTool.parseAlarm(equCode, "AC0603", "AS01");
                            KafkaWorker.sendAlarmMessage(alarm1.toJson());
                            return;
                        case "03"://03说明AB都故障，主机离线
                            equCode = modelNo;
                            FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收AB故障，主机离线" + equCode);
                            // 故障报警
                            AlarmEntity alarmA = AlarmParseTool.parseAlarm(equCode + "-A", "AC0603", "AS01");
                            KafkaWorker.sendAlarmMessage(alarmA.toJson());
                            AlarmEntity alarmB = AlarmParseTool.parseAlarm(equCode + "-B", "AC0603", "AS01");
                            KafkaWorker.sendAlarmMessage(alarmB.toJson());
                            //离线报警
                            AlarmEntity alarm_offline = AlarmParseTool.parseAlarm(equCode, "AC0604", "AS01");
                            KafkaWorker.sendAlarmMessage(alarm_offline.toJson());
                            //设备离线
                            DeviceStateEntity deviceState = AlarmParseTool.parseDeviceState(equCode,"ES02");
                            KafkaWorker.sendDeviceMessage(deviceState.toJson());
                            return;
                        case "00"://00说明AB都恢复，主机在线
                            equCode = modelNo;
                            FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收AB故障恢复，主机在线" + equCode);
                            // 故障消警
                            AlarmEntity alarmA1 = AlarmParseTool.parseAlarm(equCode + "-A", "AC0603", "AS02");
                            KafkaWorker.sendAlarmMessage(alarmA1.toJson());
                            AlarmEntity alarmB1 = AlarmParseTool.parseAlarm(equCode + "-B", "AC0603", "AS02");
                            KafkaWorker.sendAlarmMessage(alarmB1.toJson());
                            //离线消警
                            AlarmEntity alarm_offline1 = AlarmParseTool.parseAlarm(equCode, "AC0604", "AS02");
                            KafkaWorker.sendAlarmMessage(alarm_offline1.toJson());
                            //设备上线
                            DeviceStateEntity deviceState1 = AlarmParseTool.parseDeviceState(equCode, "ES01");
                            KafkaWorker.sendDeviceMessage(deviceState1.toJson());
                            return;
                        default:
                            return;
                    }
                }
            }
            if (message.Substring(8, 2) == "04")//防拆
            {
                equCode = modelNo;
                if (message.Substring(18, 2) == "01")//防拆报警
                {
                    FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收防拆报警" + equCode);
                    AlarmEntity alarm = AlarmParseTool.parseAlarm(equCode, "AC0602", "AS01");
                    KafkaWorker.sendAlarmMessage(alarm.toJson());
                }
                if (message.Substring(18, 2) == "00")//防拆消警
                {
                    FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收防拆消警" + equCode);
                    AlarmEntity alarm = AlarmParseTool.parseAlarm(equCode, "AC0602", "AS02");
                    KafkaWorker.sendAlarmMessage(alarm.toJson());
                }
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
