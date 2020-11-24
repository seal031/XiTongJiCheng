using OPCBRIDGELib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShaoGuanMenJin
{
    public partial class Form1 : Form
    {
        public static uint m_ServerHandle, m_GroupHandle;
        private OPCBRIDGELib.Subscription oSubscription;
        Dictionary<string, List<string>> alarmRuleDic = new Dictionary<string, List<string>>();
        private OPCBRIDGELib.EventGroups oAvailableGroups;
        private List<string> groupID = new List<string>();
        private string lastMess = "";
        /// <summary>
        /// Gallagher OPC Bridge服务器地址或名称,初始化值为localhost
        /// </summary>
        public string sOPCBridgeServer = "localhost";
        public List<EventGroup> eventGroupList = new List<EventGroup>(); //用来存储事件组,类似demo的列表框
        public Form1()
        {
            InitializeComponent();
            Initial();
        }
        private void Initial()
        {
            oSubscription = new Subscription();
            oSubscription.OnEvent += new IEventSink_OnEventEventHandler(oSubscription_OnEvent);
            oSubscription.OnFailure += new IEventSink_OnFailureEventHandler(oSubscription_OnFailure);
        }

        private void oSubscription_OnFailure(int failedtype, string serverFailed)
        {
            FileWorker.LogHelper.WriteLog(string.Format("failedtype:{0},serverFailed:{1}", failedtype, serverFailed));
        }
        [HandleProcessCorruptedStateExceptions]
        [SecurityCritical]
        private void oSubscription_OnEvent(EventNotification pNotification)
        {
            try
            {
                #region  去除重复的刷卡记录
                string sProcessState = "";
                if (pNotification.Processed == 1)
                {
                    //sProcessState = "Processed";
                }
                else if (pNotification.Processed == 0)
                {
                }
                else if (pNotification.Acknowledged == 1)
                {
                    sProcessState = "Acknowledged";
                }
                else
                {
                    sProcessState = "Unacknowledged";
                }
                string str = string.Format("{0}___{1}___{2}___{3}___{4}___{5}___{6}___{7}___{8}___{9}___{10}___{11}___{12}",
                    pNotification.Time.ToString("yyyy-MM-dd HH:mm:ss"),
                    pNotification.ArrivalTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    pNotification.Message,
                    pNotification.Description,
                    pNotification.Details,
                    oAvailableGroups[pNotification.Group.ID].Description,
                    pNotification.Group.ID.ToString(),
                    pNotification.Source,
                    pNotification.Priority.ToString(),
                    pNotification.Active == 1 ? "Active" : "Inactive",
                    sProcessState,
                    pNotification.Alarm.ToString(),
                    pNotification.Cookie.ToString());
                if (str == lastMess)
                {
                    return;
                }
                else
                {
                    FileWorker.LogHelper.WriteLog(str);
                    lastMess = str;
                }
                #endregion
                string deviceMess = string.Format("{0}.{1}", pNotification.Group.ID.ToString(), pNotification.Description);
                if (pNotification.Alarm == 0)
                {
                    string deviceCode = pNotification.Source.Replace(" ", "");
                    //if (pNotification.Details != "" && pNotification.Details.Contains("(") && pNotification.Details.Contains(")"))
                    if ( deviceMess == "23.门通行准许" )
                    {

                        string cardId = pNotification.Details;
                        int arr = cardId.IndexOf(")") - 1 - cardId.IndexOf("(");
                        cardId = cardId.Substring(cardId.IndexOf("(") + 1, arr);
                        AccessEntity accessEnt = AccessParseTool.parseAccess(cardId, pNotification.Time.ToString("yyyy-MM-dd HH:mm:ss"), deviceCode);
                        //FileWorker.LogHelper.WriteLog(accessEnt.toJson());
                        string mess = accessEnt.toJson();
                        KafkaWorker.sendAccessMessage(mess);
                    }
                    else if (deviceMess == "27.按钮开门通行准许")//按钮开门数据
                    {
                        AccessEntity accessEnt = AccessParseTool.parseAccess("", pNotification.Time.ToString("yyyy-MM-dd HH:mm:ss"), deviceCode);
                        string mess = accessEnt.toJson();
                        KafkaWorker.sendAccessMessage(mess);
                    }
                    if (deviceMess == "26.门重新进入安全状态")
                    {
                        DeviceStateEntity stateEntity = DeviceStateParseTool.parseDeviceState(pNotification.Time.ToString("yyyy-MM-dd HH:mm:ss"), pNotification.Source, deviceMess);
                        string stateMess = stateEntity.toJson();
                        KafkaWorker.sendDeviceMessage(stateMess);
                    }
                    //2020-11-22 17:48:47___2020-11-22 17:48:48___AC-1-2-01已重新进入安全状态.___门重新进入安全状态___
                    //___Door Status___26___AC-1-2-01___1___Inactive______0___0
                    // 2020-11-22 14:20:06___2020-11-22 14:20:06___中航测试卡-2被准许通过AC-1-2-06进入进入区域 1.___门通行准许___卡号(3946934)
                    //___Card Event___23___AC-1 - 2 - 06___1___Inactive______0___0
                }
                else
                {
                    //2020 - 11 - 22 14:28:31___2020 - 11 - 22 14:28:31___AC - 1 - 2 - 01已被强制打开.___强制开门___""___Forced Door___29___AC-1 - 2 - 01___8___Inactive______1___1
                    //2020 - 11 - 22 14:28:37___2020 - 11 - 22 14:28:37___AC - 1 - 2 - 01已重新进入安全状态.___门重新进入安全状态___""___Door Status___26___AC-1 - 2 - 01___1___Inactive______0___0

                    //2020-11-22 14:42:26___2020-11-22 14:42:26___AC-1-2-06开启时间过长.___门开时间过长___""___Door Open Too Long___28___AC-1-2-06___5___Inactive______1___1 
                    FileWorker.LogHelper.WriteLog(pNotification.Group.ID.ToString());

                    if (alarmRuleDic.ContainsKey(deviceMess))
                    {
                        AlarmEntity alarmEnt = AlarmParseTool.parseAlarm(pNotification.Time.ToString("yyyy-MM-dd HH:mm:ss"), pNotification.Source,alarmRuleDic[deviceMess]);
                        string mess = alarmEnt.toJson();
                        KafkaWorker.sendAlarmMessage(mess);
                        if (deviceMess == "28.门开时间过长")
                        {
                            DeviceStateEntity stateEntity = DeviceStateParseTool.parseDeviceState(pNotification.Time.ToString("yyyy-MM-dd HH:mm:ss"), pNotification.Source,deviceMess);
                            string stateMess = stateEntity.toJson();
                            KafkaWorker.sendDeviceMessage(stateMess);
                        }
                    }
                }

                //共13列
                //Time,ArrivalTime,Message,Description,Details,
                //GroupName,GroupID,Source,Priority,Active,
                //ProcessState,Alarm,Cookie

            }
            catch (System.AccessViolationException exp) //捕获cse类型的异常
            {
                FileWorker.LogHelper.WriteLog("oSubscription_OnEvent-捕获cse类型的异常:" + exp.ToString());
            }
            catch (Exception exp)
            {
                FileWorker.LogHelper.WriteLog("oSubscription_OnEvent:" + exp.ToString());
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (loadLocalConfig())
            {
                #region  新版本,仿照GallaherOPCBridge
                try
                {
                    FileWorker.LogHelper.WriteLog("软件启动");
                    SetServerAndAttribute(sOPCBridgeServer);
                    GetEventCategory();
                }
                catch (Exception exp)
                {
                    FileWorker.LogHelper.WriteLog("连接失败:" + exp.ToString());
                }
                #endregion
            }
            else
            {
                FileWorker.LogHelper.WriteLog("本地配置文件参数不正确");
                return;
            }
            try
            {
                if (oSubscription != null && oSubscription.Enabled == 1)
                {
                    //尝试断开上一次连接
                    oSubscription.Deactivate();
                    FileWorker.LogHelper.WriteLog("断开连接成功");
                }
                string sServerIp = sOPCBridgeServer;
                //设置Gallagher OPC Bridge服务地址和oSubscription属性
                SetServerAndAttribute(sServerIp);
                //设置需要订阅的事件组
                GetEventFilter();
                //激活与OPC服务器的连接
                oSubscriptionActivate();
            }
            catch (Exception exp)
            {
                FileWorker.LogHelper.WriteLog("第二次连接失败:" + exp.ToString());
            }
        }

        private bool loadLocalConfig()
        {
            try
            {
                //host = ConfigWorker.GetConfigValue("host");
                //classId = ConfigWorker.GetConfigValue("classId");
                sOPCBridgeServer = ConfigWorker.GetConfigValue("OPCBridgeServer");
                string path = Application.StartupPath + "\\alarmType.txt";
                string[] alarmArr = File.ReadAllLines(path, Encoding.UTF8);
                foreach (var item in alarmArr)
                {
                    if (item != null && item != "")
                    {
                        string[] split = item.Split(new char[] { '=' });
                        string[] alarmMess = split[1].Split(new char[] { ',' });
                        List<string> mess = new List<string> { alarmMess[0], alarmMess[1] };
                        alarmRuleDic.Add(split[0], mess);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void SetServerAndAttribute(string serverIp)
        {
            try
            {
                oSubscription.Server = serverIp;
                oSubscription.AttemptsBeforeNotify = 0;
                oSubscription.NotifyOnFailure = 1;
                oSubscription.MinimumPriority = 1;
                oSubscription.MaximumPriority = 9;
                oSubscription.CheckEnabled = 1;
                oSubscription.CheckInterval = 1;
                FileWorker.LogHelper.WriteLog("设置Gallagher OPC Bridge服务地址和oSubscription属性完成");
            }
            catch (Exception exp)
            {
                FileWorker.LogHelper.WriteLog("设置Gallagher OPC Bridge服务地址和oSubscription属性失败:"+exp.ToString());
            }
        }
        /// <summary>
        /// 获取事件组
        /// </summary>
        private void GetEventCategory()
        {
            try
            {
                oAvailableGroups = oSubscription.GetAvailableGroups();
                foreach (OPCBRIDGELib.EventGroup eGroup in oAvailableGroups)
                {
                    groupID.Add(eGroup.ID.ToString());
                    //oAvailableGroups.Add(eGroup)

                    //ListViewItem lsvItem = new ListViewItem();
                    //lsvItem.Text = eGroup.Description;
                    //lsvItem.Tag = eGroup.ID;

                    //lvEventFilter.Items.Add(lsvItem);
                }
            }
            catch (Exception exp)
            {
                switch (exp.GetType().ToString())
                {
                    case "System.UnauthorizedAccessException":
                        FileWorker.LogHelper.WriteLog("请检查Gallagher Command Centre的核心服务FT Command Centre Service是否已经启动，详细错误如下:\r\n" + exp.ToString());
                        break;
                    default:
                        FileWorker.LogHelper.WriteLog("获取事件组失败:" + exp.ToString());
                        break;
                }
            }
        }
        /// <summary>
        /// 设置需要订阅的事件组
        /// </summary>
        private void GetEventFilter()
        {
            try
            {
                EventGroups oGroups;
                oGroups = oSubscription.Groups;

                //先移除上一次设置的事件组
                foreach (EventGroup oGroup in oGroups)
                {
                    oGroups.Remove(oGroup.ID);
                }
                //将选中的事件组添加到oGroups对象中
                //selectEventGroups = new   
                foreach (string item in groupID)
                {
                    int iEventGroupID = 0;
                    if (int.TryParse(item, out iEventGroupID) && iEventGroupID > 0)
                    {
                        oGroups.Add(oAvailableGroups[iEventGroupID]);
                    }
                }

            }
            catch (Exception exp)
            {
                FileWorker.LogHelper.WriteLog("设置订阅组失败:" + exp.ToString());
            }
        }
        /// <summary>
        /// 激活oSubscription对象
        /// </summary>
        private void oSubscriptionActivate()
        {
            try
            {
                if (oSubscription != null)
                {
                    //重新订阅事件 Added by Youngber 20190817
                    oSubscription.OnEvent += new IEventSink_OnEventEventHandler(oSubscription_OnEvent);
                    oSubscription.OnFailure += new IEventSink_OnFailureEventHandler(oSubscription_OnFailure);
                    oSubscription.Activate();
                    FileWorker.LogHelper.WriteLog("激活连接成功");
                }

            }
            catch (Exception exp)
            {
                FileWorker.LogHelper.WriteLog("激活连接失败:" + exp.ToString());
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (oSubscription != null)
                {
                    oSubscription.Deactivate();
                    FileWorker.LogHelper.WriteLog("关闭软件,断开与OPC服务连接");
                }
            }
            catch (Exception exp)
            {
                FileWorker.LogHelper.WriteLog("关闭软件报错:" + exp.ToString());

            }
        }

    }
}
