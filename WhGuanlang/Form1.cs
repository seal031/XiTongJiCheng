using Quartz;
using Quartz.Impl;
using SimpleTCP;
using SuperSocket.ClientEngine;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WhGuanlang
{
    public partial class Form1 : Form,IForm
    {
        public bool isConnected = false;
        string localIp;
        int localPort;
        string remoteIp;
        int remotePort;
        //TcpClientSession client;
        IScheduler scheduler;
        IJobDetail job;
        JobKey jobKey;

        SimpleTcpClient client;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //var alarmEntity = AlarmParseTool.parseAlarm("<ROOT><Event logid=\"1170\" time=\"2019 / 09 / 21 13:54:14\" mdlid=\"1006\" code=\"1\" cid=\"130\" grade=\"3\" desc=\"报警\" mrk=\"防区报警\" mdlname=\"综合管廊\\AF 08\\VICTRIX - 8_172.28.161.207\\VICTRIX - 防区_4\"/></ROOT>");
            //var alarmEntity = AlarmParseTool.parseAlarm("<ROOT><Event logid=\"1190488\" time=\"2020 / 05 / 28 10:32:55\" mdlid=\"1018\" code=\"1\" cid=\"130\" grade=\"3\" desc=\"报警\" mrk=\"防区报警\" mdlname=\"管廊报警\\AF - 01_172.28.161.200\\VICTRIX - 防区_5\"/></ROOT>");
            //string message = alarmEntity.toJson();
            //KafkaWorker.sendAlarmMessage(message);
            //var a = AlarmParseTool.parseDeviceState("<ROOT><Event logid=\"1190495\" time=\"2020 / 05 / 28 10:33:26\" mdlid=\"1212\" code=\"1\" cid=\"\" grade=\"0\" desc=\"连接成功\" mrk=\"通讯连接成功\" mdlname=\"管廊报警\\AF - 24_172.28.162.211\"/></ROOT>", "");
            if (loadConfig() == false)
            {
                MessageBox.Show("配置文件中的配置项解析失败，请检查配置文件内容");
                return; 
            }
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.Default;
            //client.LocalEndPoint = new IPEndPoint(IPAddress.Parse(localIp), localPort);
            //client.Connected += Client_Connected;
            //client.Closed += Client_Closed;
            //client.DataReceived += Client_DataReceived;
            client.DataReceived += Client_DataReceived;
            //client.Error += Client_Error;
            connectToServer();

            //tcpClient = new TcpClient(localIp, localPort);
            //thrRecv = new Thread(ReceiveMessage);
            //thrRecv.Start();
            //FileWorker.LogHelper.WriteLog("TCP监听线程已启动");
            initJob();
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            string message = e.MessageString;
            //FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收的数据是" + message);
            if (message.Contains("$HBT$"))//heart beat
            {
                sendHearBeat();
            }
            if (message.Contains("防区报警"))
            {
                //FileWorker.LogHelper.WriteLog("接收到报警" + message);
                var alarmEntity = AlarmParseTool.parseAlarm(message);
                if (alarmEntity != null)
                {
                    string alarmMessage = alarmEntity.toJson();
                    FileWorker.LogHelper.WriteLog("向kafka发送报警" + alarmMessage);
                    KafkaWorker.sendAlarmMessage(alarmMessage);
                }
                return;
            }
            if (message.Contains("ACK Login"))
            {
                FileWorker.LogHelper.WriteLog("登录成功");
                return;
            }
            if (message.Contains("连接成功"))
            {
                //FileWorker.LogHelper.WriteLog("接收到设备状态变更" + message);
                var deviceStateEntity = AlarmParseTool.parseDeviceState(message, "ES01");
                if (deviceStateEntity != null)
                {
                    string deviceStateMessage = deviceStateEntity.toJson();
                    FileWorker.LogHelper.WriteLog("向kafka发送设备状态" + deviceStateMessage);
                    KafkaWorker.sendDeviceMessage(deviceStateMessage);
                }
                return;
            }
            if (message.Contains("断开连接"))
            {
                //FileWorker.LogHelper.WriteLog("接收到设备状态变更" + message);
                var deviceStateEntity = AlarmParseTool.parseDeviceState(message, "ES02");
                if (deviceStateEntity != null)
                {
                    string deviceStateMessage = deviceStateEntity.toJson();
                    FileWorker.LogHelper.WriteLog("向kafka发送设备状态" + deviceStateMessage);
                    KafkaWorker.sendDeviceMessage(deviceStateMessage);
                }
                return;
            }
        }

        private bool loadConfig()
        {
            try
            {
                localIp = ConfigWorker.GetConfigValue("localIp");
                localPort = int.Parse(ConfigWorker.GetConfigValue("localPort"));
                remoteIp = ConfigWorker.GetConfigValue("remoteIp");
                remotePort = int.Parse(ConfigWorker.GetConfigValue("remotePort"));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void initJob()
        {
            try
            {
                if (scheduler == null && job == null)
                {
                    scheduler = StdSchedulerFactory.GetDefaultScheduler();
                    JobWorker.form = this;
                    job = JobBuilder.Create<JobWorker>().WithIdentity("connectJob", "jobs").Build();
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("connectTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(300))
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(120).RepeatForever()).Build();
                    scheduler.ScheduleJob(job, trigger);//把作业，触发器加入调度器。  
                    jobKey = job.Key;
                }
                //scheduler.Start();
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("定时任务异常：" + ex.Message);
            }
        }
        public void startJob()
        {
            try
            {
                if (scheduler != null && job != null && jobKey != null)
                {
                    if (scheduler.IsStarted == false)
                    {
                        FileWorker.LogHelper.WriteLog("定时任务初次开启");
                        scheduler.Start();
                    }
                    else
                    {
                        FileWorker.LogHelper.WriteLog("定时任务恢复运行");
                        scheduler.ResumeJob(jobKey);
                    }
                }
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("定时任务开启/恢复过程中出现异常：" + ex.Message);
            }
        }
        public void stopJob()
        {
            try
            {
                if (scheduler != null && job != null && jobKey != null)
                {
                    if (scheduler.IsStarted)
                    {
                        FileWorker.LogHelper.WriteLog("定时任务暂停运行");
                        scheduler.PauseJob(jobKey);
                    }
                    else
                    {
                        FileWorker.LogHelper.WriteLog("定时任务尚未初次启动，所以不需要暂停");
                    }
                }
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("定时任务暂停过程中出现异常："+ex.Message);
            }
        }

        private void Client_Connected(object sender, EventArgs e)
        {
            FileWorker.LogHelper.WriteLog("已连接到服务器");
            isConnected = true;
            stopJob();//连接成功后，不再执行重试连接的定时任务
            login();
        }

        private void Client_Closed(object sender, EventArgs e)
        {
            isConnected = false;
            FileWorker.LogHelper.WriteLog("连接关闭");
            startJob();//连接断开后，开始执行重试连接的定时任务
        }

        private void Client_Error(object sender, ErrorEventArgs e)
        {
            isConnected = false;
            FileWorker.LogHelper.WriteLog("client error"+e.Exception.Message);
            startJob();//连接断开后，开始执行重试连接的定时任务
        }
        
        private void Client_DataReceived(object sender, DataEventArgs e)
        {
            //string message = MessageParser.byteToStr(e.Data);
            //FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收的数据是" + message);
            //if (message.Contains("$HBT$"))//heart beat
            //{
            //    sendHearBeat(e.Data);
            //    return;
            //}
            //if (message.Contains("防区报警"))
            //{
            //    FileWorker.LogHelper.WriteLog("接收到报警" + message);
            //    var alarmEntity = AlarmParseTool.parseAlarm(message);
            //    if (alarmEntity != null)
            //    {
            //        string alarmMessage = alarmEntity.toJson();
            //        FileWorker.LogHelper.WriteLog("向kafka发送报警" + alarmMessage);
            //        KafkaWorker.sendAlarmMessage(alarmMessage);
            //    }
            //}
            //if (message.Contains("ACK Login"))
            //{
            //    FileWorker.LogHelper.WriteLog("登录成功");
            //}
            //if (message.Contains("连接成功"))
            //{
            //    FileWorker.LogHelper.WriteLog("接收到设备状态变更" + message);
            //    var deviceStateEntity = AlarmParseTool.parseDeviceState(message, "ES01");
            //    if (deviceStateEntity != null)
            //    {
            //        string deviceStateMessage = deviceStateEntity.toJson();
            //        FileWorker.LogHelper.WriteLog("向kafka发送报警" + deviceStateMessage);
            //        KafkaWorker.sendDeviceMessage(deviceStateMessage);
            //    }
            //}
            //if (message.Contains("断开连接"))
            //{
            //    FileWorker.LogHelper.WriteLog("接收到设备状态变更" + message);
            //    var deviceStateEntity = AlarmParseTool.parseDeviceState(message, "ES02");
            //    if (deviceStateEntity != null)
            //    {
            //        string deviceStateMessage = deviceStateEntity.toJson();
            //        FileWorker.LogHelper.WriteLog("向kafka发送报警" + deviceStateMessage);
            //        KafkaWorker.sendDeviceMessage(deviceStateMessage);
            //    }
            //}
        }

        public bool connectToServer()
        {
            FileWorker.LogHelper.WriteLog("开始登陆服务器");
            if (client != null)
            {
                try
                {
                    //client.Connect(new IPEndPoint(IPAddress.Parse(remoteIp), remotePort));
                    client.Connect(remoteIp, remotePort);
                    Thread.Sleep(1000);
                    login();
                    return true; 
                }
                catch (Exception ex)
                {
                    FileWorker.LogHelper.WriteLog("登陆服务器错误:"+ex.Message);
                    return false;
                }
            }
            else
            {
                FileWorker.LogHelper.WriteLog("无法登陆服务器，client对象为空");
                return false;
            }
        }
        public void checkAndReconnectToServer()
        {
            isConnected = client.TcpClient.Connected;
            FileWorker.LogHelper.WriteLog("定时任务执行重连检查，当前连接状态为"+isConnected );
            if (isConnected == false)
            {
                connectToServer();
            }
        }

        private void login()
        {
            //string loginStr = "REQ Login<0x\0d><0x\0a>"
            //                + "userid:admin<0x\0d><0x\0a>"
            //                + "notify:true<0x\0d><0x\0a>"
            //                + "password:<0x\0d><0x\0a>"
            //                + "answer:true<0x\0d><0x\0a>"
            //                + "content_length:0<0x\0d><0x\0a><0x\0d><0x\0a>";
            string loginStr1 = "REQ Login" + Environment.NewLine
                            + "userid:admin" + Environment.NewLine
                            + "notify:true" + Environment.NewLine
                            + "password:" + Environment.NewLine
                            + "answer:true" + Environment.NewLine
                            + "content_length:0" + Environment.NewLine + Environment.NewLine;
            sendLogin(loginStr1);
        }
        
        private void sendHearBeat()
        {
            try
            {
                byte[] heartBytes = MessageParser.strToByte("$HBT$" + Environment.NewLine + Environment.NewLine);
                client.Write(heartBytes);
                FileWorker.LogHelper.WriteLog(string.Format("sendBufferSize:{0},receiveBuffSize:{1}",client.TcpClient.SendBufferSize,client.TcpClient.ReceiveBufferSize));
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("发送心跳错误"+ex.Message);
            }
        }

        private void sendLogin(string loginStr)
        {
            try
            {
                byte[] loginBytes = MessageParser.strToByte(loginStr);
                client.Write(loginBytes);
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("发送登录错误"+ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //login();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                client = client.Disconnect();
                client.Dispose();
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("执行清理工作异常:"+ex.Message);
            }
        }
    }
}
