using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HwMenJin
{
    public partial class Form1 : Form
    {
        IScheduler scheduler;
        IJobDetail job;
        string connectStr = "";
        Dictionary<string,string> eventDic = new Dictionary<string,string>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (loadLocalConfig() == false)
            {
                return;
            }
            connectToServer();
            KafkaWorker.OnGetMessage += KafkaWorker_OnGetMessage;
        }

        private void KafkaWorker_OnGetMessage(string message)
        {
            string doorName = pickDoorNameFormMessage(message);
            unlockDoor(doorName);
        }

        private string pickDoorNameFormMessage(string message)
        {

            return "";
        }

        private bool loadLocalConfig()
        {
            try
            {
                connectStr = ConfigWorker.GetConfigValue("connectString");
                if (connectStr == string.Empty)
                {
                    MessageBox.Show("数据库连接字符串不能为空");
                    return false;
                }
                string alarmalarmFilterStr = ConfigWorker.GetConfigValue("alarmFilter");
                string[] alarmFilterArray = alarmalarmFilterStr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string alarmFilter in alarmFilterArray)
                {
                    if (alarmFilter.Contains(":") == false) continue;
                    string[] alarmFilterItem= alarmFilter.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    eventDic.Add(alarmFilterItem[1], alarmFilterItem[0]);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取本地配置错误");
                return false;
            }
        }
        private void connectToServer()
        {
            axHSCEventSDK1.sSQLDBLinkString = connectStr;
            axHSCReaderSDK1.sSQLDBLinkString = connectStr;
            //连接服务 0：失败 1：成功
            if (axHSCEventSDK1.lConnectSQLDB() > 0 && axHSCReaderSDK1.lConnectSQLDB() > 0)
            {
                FileWorker.LogHelper.WriteLog("连接成功");
            }
            else
            {
                FileWorker.LogHelper.WriteLog("连接失败");
                MessageBox.Show("连接服务器失败");
                Application.Exit();
            }
        }

        private void startJob()
        {
            if (scheduler == null && job == null)
            {
                scheduler = StdSchedulerFactory.GetDefaultScheduler();
                job = JobBuilder.Create<JobWorker>().WithIdentity("consumerJob", "jobs").Build();
                ITrigger trigger = TriggerBuilder.Create().WithIdentity("consumerTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(1))
                    .WithSimpleSchedule(x => x.WithRepeatCount(1)).Build();
                scheduler.ScheduleJob(job, trigger);//把作业，触发器加入调度器。
            }
            scheduler.Start();
        }

        /// <summary>
        /// 门禁开门
        /// </summary>
        /// <param name="doorName"></param>
        private void unlockDoor(string doorName)
        {
            //手工调用门禁点瞬间开锁
            try
            {
                axHSCReaderSDK1.sReaderID = doorName;
                axHSCReaderSDK1.ACSystenReaderIDMomentaryUnLock();
                string unlockResult = sReultDescrp(axHSCReaderSDK1.lErrorResult);
                Debug.WriteLine(unlockResult);
                FileWorker.LogHelper.WriteLog("开门" + doorName + "结果：" + unlockResult);
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("开门" + doorName + "失败。失败原因：" + ex.Message);
            }
        }
        private string sReultDescrp(long lErrorNum)
        {
            switch (lErrorNum)
            {
                case -1:
                    return "初始化值或者执行设备类型";
                case 0:
                    return "指令下发成功";
                case 1:
                    return "演示版或者授权过期";
                case 2:
                    return "授权门禁数量范围小于实际系统授权门禁点位数量";
                case 3:
                    return "Pro Watch 数据库连接错误";
                case 4:
                    return "Pro Watch 系统服务主机地址未设定";
                case 5:
                    return "Pro Watch 系统服务主机通讯服务不存在";
                case 6:
                    return "Pro Watch 系统服务主机通讯服务连接失败";
                case 7:
                    return "Pro Watch 系统服务主机控制指令不存在";
                case 8:
                    return "Pro Watch 系统服务主机控制指令发送失败";
                case 9:
                    return "Pro Watch 设备类型和操作模式不匹配";
                default:
                    return "未知错误";
            }
        }

        private void axHSCEventSDK1_NewEvent(object sender, EventArgs e)
        {
            if (eventDic.ContainsKey(axHSCEventSDK1.sEventName))
            {
                var alarmEntity = AlarmParseTool.parseAlarm(axHSCEventSDK1, eventDic);
                if (alarmEntity != null)
                {
                    string alarmMessage = alarmEntity.toJson();
                    FileWorker.LogHelper.WriteLog("正在向kafka发送数据"+alarmMessage);
                    Debug.WriteLine(alarmMessage);
                    KafkaWorker.sendAlarmMessage(alarmMessage);
                }
            }
        }
    }
}
