using Quartz;
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
            client.StringEncoder = Encoding.ASCII;
            client.DataReceived += Client_DataReceived;
            connectToServer();
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

        public void cyclicWork()
        {
           
        }
    }
}
