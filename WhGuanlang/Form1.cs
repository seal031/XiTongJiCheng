using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using SuperSocket.ClientEngine;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using Quartz;
using Quartz.Impl;

namespace WhGuanlang
{
    public partial class Form1 : Form
    {
        public bool isConnected = false;
        string localIp;
        int localPort;
        string remoteIp;
        int remotePort;
        TcpClientSession client;
        IScheduler scheduler;
        IJobDetail job;
        JobKey jobKey;

        TcpClient tcpClient;
        Thread thrRecv;

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
            initJob();
            client = new AsyncTcpSession(); 
            client.LocalEndPoint = new IPEndPoint(IPAddress.Parse(localIp), localPort);
            client.Connected += Client_Connected;
            client.Closed += Client_Closed;
            client.DataReceived += Client_DataReceived;
            client.Error += Client_Error;
            connectToServer();

            //tcpClient = new TcpClient(localIp, localPort);
            //thrRecv = new Thread(ReceiveMessage);
            //thrRecv.Start();
            //FileWorker.LogHelper.WriteLog("TCP监听线程已启动");
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
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("connectTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(1))
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever()).Build();
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
            string message = MessageParser.byteToStr(e.Data);
            FileWorker.LogHelper.WriteLog(Environment.NewLine + "接收的数据是" + message);
            if (message.Contains("$HBT$"))//heart beat
            {
                sendHearBeat(e.Data);
                return;
            }
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

        //private void ReceiveMessage(object obj)
        //{
        //    IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse(remoteIp), remotePort);
        //    while (true)
        //    {
        //        try
        //        {
        //            byte[] bytRecv = tcpClient.Client.Receive(ref remoteIpep);
        //            string bytRecvStr = string.Join(" ", bytRecv);
        //            FileWorker.LogHelper.WriteLog("接收到的原始数据是：" + bytRecvStr);
        //            string msg = Encoding.Default.GetString(bytRecv);
        //            FileWorker.LogHelper.WriteLog("原始数据解析后是：" + msg);

        //            AlarmEntity alarm = udpToAlarm(msg);
        //            string messageAlarm = alarm.toJson();
        //            KafkaWorker.sendAlarmMessage(messageAlarm);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //            break;
        //        }
        //    }
        //}

        public bool connectToServer()
        {
            if (client != null)
            {
                try
                {
                    client.Connect(new IPEndPoint(IPAddress.Parse(remoteIp), remotePort));
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
                return false;
            }
        }
        public void checkAndReconnectToServer()
        {
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

        private void sendMessage(string message)
        {
            ArraySegment<byte> bytes = new ArraySegment<byte>();
            client.Send(bytes);
        }
        private void sendMessage(byte[] message)
        {
            try
            {
                client.Send(message, 0, 9);
                FileWorker.LogHelper.WriteLog("发送数据成功,发送的数据是:"+MessageParser.byteToStr(message));
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("发送数据错误"+ex.Message);
            }
        }

        private void sendHearBeat(byte[] hb)
        {
            try
            {
                client.Send(hb, 0, 9);
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
                client.Send(loginBytes, 0, loginBytes.Length);
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("发送登录错误"+ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login();
        }
    }
}
