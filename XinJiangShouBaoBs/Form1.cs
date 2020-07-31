using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinJiangShouBaoBs
{
    public partial class Form1 : Form
    {
        string localIp;
        int localPort;
        string serverIp;
        int serverPort;
        string airportIata;
        string airportName;
        UdpClient udpcRecv;
        Thread thrRecv;

        public Form1()
        {
            InitializeComponent();
            //发生报警 警情:紧急求助报警 时间:18:52:10 用户: 航站楼 防区/周界:007
            //发生报警 警情:紧急求助防区故障 时间:19:14:31 用户:航站楼 防区/周界:006
            //发生报警 警情:紧急求助报警 时间:17:31:56 用户:航站楼 防区/周界:007
            //string a = "发生报警 警情:紧急求助报警 时间:17:31:56 用户:航站楼 防区/周界:007";
            //string a = "发生报警 警情:紧急求助报警 时间:17:43:19 用户:航站楼 防区/周界:008";
            //var alarm = udpToAlarm(a, "手报故障事件", "AC0302", "AS01", "未解除");
            //var device = udpToFault(a);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            localIp = ConfigWorker.GetConfigValue("localIp");
            string localPortStr = ConfigWorker.GetConfigValue("localPort");
            serverIp = ConfigWorker.GetConfigValue("serverIp");
            string serverPortStr = ConfigWorker.GetConfigValue("serverPort");
            airportIata = ConfigWorker.GetConfigValue("airportIata");
            airportName = ConfigWorker.GetConfigValue("airportName");
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
                FileWorker.LogHelper.WriteLog("UDP监听线程启动失败:"+ex.Message);
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
                    //FileWorker.LogHelper.WriteLog("接收到的原始数据是：" + bytRecvStr);
                    string msg = Encoding.Default.GetString(bytRecv);
                    FileWorker.LogHelper.WriteLog("原始数据解析后是：" + msg);
                    if (msg.Contains("紧急求助报警"))//报警
                    {
                        AlarmEntity alarm = udpToAlarm(msg, "手报报警事件", "AC0301", "AS01", "未解除");
                        if (alarm != null)
                        {
                            string messageAlarm = alarm.toJson();
                            KafkaWorker.sendAlarmMessage(messageAlarm);
                        }
                        //return;
                    }
                    if (msg.Contains("紧急求助防区故障"))//故障
                    {
                        AlarmEntity alarm = udpToAlarm(msg, "手报故障事件", "AC0302", "AS01", "未解除");
                        if (alarm != null)
                        {
                            string messageAlarm = alarm.toJson();
                            KafkaWorker.sendAlarmMessage(messageAlarm);
                        }
                        DeviceStateEntity deviceState = udpToFault(msg);
                        if (deviceState != null)
                        {
                            string messageDeviceState = deviceState.toJson();
                            KafkaWorker.sendDeviceMessage(messageDeviceState);
                        }
                        //return;
                    }
                }
                catch (Exception ex)
                {
                    FileWorker.LogHelper.WriteLog("消息接收出现异常：" + ex.Message);
                }
            }
        }

        private static Regex regexTime = new Regex(@"时间:\d\d:\d\d:\d\d");
        private static Regex regexFangqu = new Regex(@"防区/周界:\d\d\d");
        private static Regex regexZhuji = new Regex(@"用户:\w{2,20}防区/周界");
        private AlarmEntity udpToAlarm(string msg, string alarmName, string alarmNameCode, string alarmStateCode, string alarmStateName)
        {
            msg = msg.Replace(" ","").Replace(Environment.NewLine, "");
            AlarmEntity alarm = new AlarmEntity();
            Match timeMatch = regexTime.Match(msg);
            Match fangquMatch = regexFangqu.Match(msg);
            Match zhujiMatch=regexZhuji.Match(msg);
            string zhuji = zhujiMatch.Value.Replace("用户:", "").Replace("防区/周界", "").Trim();
            string fangqu = fangquMatch.Value.Replace(@"防区/周界:", "").Trim();
            string time = timeMatch.Value.Replace(@"时间:", "").Trim();
            try
            {
                DateTime dt = DateTime.ParseExact(time, "HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                alarm.body.alarmTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception)
            {
                alarm.body.alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            alarm.body.alarmName = alarmName;// "手报报警事件";
            alarm.body.alarmNameCode = alarmNameCode;// "AC0301";
            alarm.body.alarmStateCode = alarmStateCode;// "AS01";
            alarm.body.alarmStateName = alarmStateName;// "未解除";
            alarm.body.airportIata = airportIata;
            alarm.body.airportName = airportName;
            alarm.body.alarmEquCode = airportIata + "-" + zhuji + "-" + fangqu;
            return alarm;
        }

        private DeviceStateEntity udpToFault(string msg)
        {
            msg = msg.Replace(" ", "").Replace(Environment.NewLine, "");
            DeviceStateEntity deviceState = new DeviceStateEntity();
            Match timeMatch = regexTime.Match(msg);
            Match fangquMatch = regexFangqu.Match(msg);
            Match zhujiMatch = regexZhuji.Match(msg);
            string zhuji = zhujiMatch.Value.Replace("用户:", "").Replace("防区/周界", "").Trim();
            string fangqu = fangquMatch.Value.Replace(@"防区/周界:", "").Trim();
            string time = timeMatch.Value.Replace(@"时间:", "").Trim();
            try
            {
                DateTime dt = DateTime.ParseExact(time, "HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture);
                deviceState.body.operateTime = dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception)
            {
                deviceState.body.operateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            deviceState.body.equNameCode = airportIata + "-" + zhuji + "-" + fangqu;
            deviceState.body.timeStateId = "ES03";
            deviceState.body.timeStateName = "故障";
            return deviceState;
        }
    }
}
