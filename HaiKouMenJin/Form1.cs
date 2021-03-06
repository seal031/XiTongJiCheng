﻿using Quartz;
using Quartz.Impl;
using SimpleTCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HaiKouMenJin
{
    public partial class Form1 : Form,IForm
    {
        string remoteIp;
        int remotePort;
        SimpleTcpClient client;
        IScheduler scheduler;
        IJobDetail job;
        JobKey jobKey;
        Dictionary<string, string> anoAlarm = new Dictionary<string, string>();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (loadConfig() == false)
            {
                MessageBox.Show("配置文件中的配置项解析失败，请检查配置文件内容");
                return;
            }
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.Default;
            client.DataReceived += Client_DataReceived;
            connectToServer();
            cyclicWork();
            initJob();
        }
        private bool loadConfig()
        {
            try
            {
                remoteIp = ConfigWorker.GetConfigValue("remoteIp");
                remotePort = int.Parse(ConfigWorker.GetConfigValue("remotePort"));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Client_DataReceived(object sender, SimpleTCP.Message e)
        {
            FileWorker.LogHelper.WriteLog("刷卡信息:" + e.MessageString);
            string[] messageArr = e.MessageString.Split(new string[] { "TRDS" }, StringSplitOptions.RemoveEmptyEntries);
            for (int j = 0; j < messageArr.Length; j++)
            {
                messageArr[j] = "TRDS" + messageArr[j];
            }
            for (int i = 0; i < messageArr.Length; i++)
            {
                string[] receMessage = messageArr[i].Split(new char[] { '|' });
                if (receMessage[0] == "TRDS")
                {
                    if (receMessage.Length >= 10)
                    {
                        AccessEntity accessEnt = AccessParseTool.parseAccess(receMessage);
                        string jsonMess = accessEnt.toJson();
                        KafkaWorker.sendAccessMessage(jsonMess);
                        anoAlarm = GetConfigMess();
                        if (anoAlarm.Keys.Contains(receMessage[9]))
                        {
                            AlarmEntity alarmEnt = AlarmParseTool.parseAlarm(receMessage, anoAlarm);
                            string jsonAlarm = alarmEnt.toJson();
                            KafkaWorker.sendAlarmMessage(jsonAlarm);
                        }

                    }
                    else
                    {
                        FileWorker.LogHelper.WriteLog("刷卡信息不全，" + messageArr[i]);
                    }
                }
            }
        }
        private Dictionary<string, string> GetConfigMess()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            string str = ConfigWorker.GetConfigValue("alarmMessage");
            string[] keyPair = str.Split(new char[] { ',' });
            for (int i = 0; i < keyPair.Length; i++)
            {
                string[] item = keyPair[i].Split(new char[] { ':' });
                if (item.Length >= 2)
                {
                    dict.Add(item[0], item[1]);
                }
            }
            return dict;
        }
        public bool connectToServer()
        {
            if (client != null)
            {
                try
                {
                    //client.Connect(new IPEndPoint(IPAddress.Parse(remoteIp), remotePort));
                    client.Connect(remoteIp, remotePort);
                    return true;
                }
                catch (Exception ex)
                {
                    FileWorker.LogHelper.WriteLog("登陆服务器错误:" + ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        ///定时任务
        private void initJob()
        {
            try
            {
                if (scheduler == null && job == null)
                {
                    scheduler = StdSchedulerFactory.GetDefaultScheduler();
                    JobWorker.form = this;
                    job = JobBuilder.Create<JobWorker>().WithIdentity("connectJob", "jobs").Build();
                    //ITrigger trigger = TriggerBuilder.Create().WithIdentity("connectTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(1))
                    //    .WithSimpleSchedule(x => x.WithIntervalInSeconds(60).RepeatForever()).Build();
                    string time = ConfigWorker.GetConfigValue("timeTask");
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("connectTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(1))
                        .WithCronSchedule(time).Build();
                    scheduler.ScheduleJob(job, trigger);//把作业，触发器加入调度器。  
                    jobKey = job.Key;
                    //scheduler.DeleteJob
                }
                scheduler.Start();
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("定时任务异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 定时查询数据
        /// </summary>
        public void cyclicWork()
        {
            string strConn = ConfigWorker.GetConfigValue("connectString");
            //strConn = "server=127.0.0.1;database=CEMData;uid=sa;pwd=qq.123456";
            string tableName = ConfigWorker.GetConfigValue("devTableName");
            string sql = string.Format("select DoorID,dev_addr,DoorName from {0} ", tableName);
            FileWorker.LogHelper.WriteLog("定时查询设备信息");
            try
            {
                DataSet ds = null;
                using (SqlDataAdapter da = new SqlDataAdapter(sql, strConn))
                {
                    ds = new DataSet();
                    da.Fill(ds);
                }
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    List<string> devList = new List<string>();
                    devList.Add(item.ItemArray[0].ToString());
                    devList.Add(item.ItemArray[1].ToString());
                    devList.Add(item.ItemArray[2].ToString());
                    ResourStateEntity accessEnt = StateParseTool.parseState(devList);
                    string jsonMess = accessEnt.toJson();
                    KafkaWorker.sendDeviceMessage(jsonMess);
                }
            }
            catch (Exception e)
            {
                FileWorker.LogHelper.WriteLog("解析设备信息表失败:" + e.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                scheduler.Shutdown();
                //scheduler.DeleteJob(jobKey);
                //scheduler.PauseJob(jobKey);
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("定时任务异常：" + ex.Message);
            }
        }
    }
}
