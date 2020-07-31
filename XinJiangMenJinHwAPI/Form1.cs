using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accw;
using System.Threading;
using System.Text.RegularExpressions;

namespace XinJiangMenJinHwAPI
{
    public partial class Form1 : Form
    {
        private WPCallbackClient client;
        private MTSCBServerClass server;

        private string showAllMessage;
        private int historyAlarmFilterDuration;
        private bool filterHistoryAlarm = true;
        private int historyAlarmJudge;
        private string userName;
        private string password;
        private string airportIata;
        private string airportName;
        Dictionary<string, Tuple<string, string>> alarmRuleDic = new Dictionary<string, Tuple<string, string>>();
        Dictionary<string, string> accessRuleDic = new Dictionary<string, string>();

        private bool isConnected = false;
        private Thread messageProcessor;

        private static Regex regexIdx = new Regex(@"<Idx>\d{0,10}</Idx>");
        private static Regex regexEventID = new Regex(@"<EventID>\d\d\d</EventID>");

        public Form1()
        {
            InitializeComponent();
            //string s = @"[NLZ][AckStatus]1[/AckStatus][Idx]2[/Idx][TranID]1[/TranID][CommSrvID]3[/CommSrvID][Account]Test[/Account][DeviceID]4[/DeviceID][HID]74[/HID][Prio]20[/Prio][Date]2/16/2007[/Date][Time]14:43:10[/Time][Cnt]1[/Cnt][Note][/Note][SS]1[/SS][Status]DoorNormal[/Status][EventID]719[/EventID][HIDSubType1]53[/HIDSubType1][Point]1[/Point][Site]Pro-2200[/Site][RP]Pro-2200-Reader2[/RP][/NLZ]";
            //string s1 = @"<NLZ><AckStatus>1</AckStatus><Idx>2</Idx><TranID>1</TranID><CommSrvID>3</CommSrvID><Account>Test</Account><DeviceID>4</DeviceID><HID>74</HID><Prio>20</Prio><Date>2/16/2007</Date><Time>14:43:10</Time><Cnt>1</Cnt><Note></Note><SS>1</SS><Status>DoorNormal</Status><EventID>719</EventID><HIDSubType1>53</HIDSubType1><Point>1</Point><Site>Pro-2200</Site><RP>Pro-2200-Reader2</RP></NLZ>";
            //var b = isAlarm(s1);
            //var eventId = getEventId(s1);
            //var a = AlarmParseTool.parseAlarm(s,"","");
            //FileWorker.LogHelper.WriteLog(s);

            //string ss = "<NLZ><AckStatus>1</AckStatus><Idx>-1</Idx><TranID> </TranID><CommSrvID>1</CommSrvID><Account>Account1</Account><DeviceID>15</DeviceID><HID>30</HID><Prio>79</Prio><Date>07/07/2020</Date><Time>15:09:00</Time><Cnt>1</Cnt><Oper></Oper><Note>卡有效，允许进入。 卡有效，允许进入。</Note><SS></SS><Status>有效卡</Status><EventID>701</EventID><HIDSubType1>51</HIDSubType1><Point>0</Point><Site>读卡器</Site><Account>Account1</Account><CardNumber>3016217654</CardNumber><FullName>向 跃明</FullName><RP>一号廊桥 - 入</RP></NLZ>";
            //var access = AccessParseTool.parseAccess(ss,"");
            //var idx = getIdx(ss);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (loadLocalConfig())
            {
                if (initSDK())
                {
                    login(); 
                }
            }
        }

        private bool loadLocalConfig()
        {
            try
            {
                historyAlarmFilterDuration = int.Parse(ConfigWorker.GetConfigValue("historyAlarmFilterDuration"));
                historyAlarmJudge = int.Parse(ConfigWorker.GetConfigValue("historyAlarmJudge"));
                showAllMessage = ConfigWorker.GetConfigValue("showAllMessage");
                userName = ConfigWorker.GetConfigValue("userName");
                password = ConfigWorker.GetConfigValue("password");
                airportIata = ConfigWorker.GetConfigValue("airportIata");
                airportName = ConfigWorker.GetConfigValue("airportName");
                string alarmFilterRule = ConfigWorker.GetConfigValue("alarmFilterRule");
                foreach (string filterRule in alarmFilterRule.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string eventCode = filterRule.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    string sodbCode = filterRule.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1];
                    string alarmNameCode=sodbCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0];
                    string alarmName = sodbCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[1];
                    alarmRuleDic.Add(eventCode, new Tuple<string, string>(alarmNameCode, alarmName));
                }
                string accessFilterRule = ConfigWorker.GetConfigValue("accessFilterRule");
                foreach (string accessRule in accessFilterRule.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string eventCode = accessRule.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    string sodbName = accessRule.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1];
                    accessRuleDic.Add(eventCode, sodbName);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载本地配置文件错误：" + ex.Message);
                return false;
            }
        }
        private bool initSDK()
        {
            try
            {
                server = new MTSCBServerClass();
                client = new WPCallbackClient();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化SDK错误：" + ex.Message);
                return false;
            }
        }

        private void login()
        {
            int iUserID = 1;
            try
            {
                isConnected = server.InitServer(client, 3, userName, password, iUserID);
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("登录异常:"+ex.Message);
            }

            if (isConnected)
            {
                FileWorker.LogHelper.WriteLog("登录成功");
                messageProcessor = new Thread(ProcessMessages);
                messageProcessor.IsBackground = true;
                messageProcessor.Start();
                getAllDevice();
            }
        }

        private void logout()
        {
            int iConnected = 0;
            server.IsConnected(out iConnected);
            if (iConnected > 0)
                server.DoneServer(client);
        }
        private void ProcessMessages()
        {
            DateTime startTime = DateTime.Now;
            while (true)
            {
                lock (client.messageQueue)
                {
                    if (client.messageQueue.Count > 0)
                    {
                        string sMessage = client.messageQueue.Dequeue();
                        if (showAllMessage != "0")
                        {
                            FileWorker.LogHelper.WriteLog(sMessage.Replace("<", "[").Replace(">", "]"));
                        }
                        string eventId = getEventId(sMessage);
                        string idx = getIdx(sMessage);
                        if (alarmRuleDic.ContainsKey(eventId))//报警
                        {
                            if (idx != string.Empty && idx != "-1")
                            {
                                FileWorker.LogHelper.WriteLog("收到报警数据 " + sMessage.Replace("<", "[").Replace(">", "]"));
                                AlarmEntity alarm = AlarmParseTool.parseAlarm(sMessage, airportIata, airportName, alarmRuleDic[eventId].Item1, alarmRuleDic[eventId].Item2);
                                if (filterHistoryAlarm)//是否需要判定历史数据
                                {
                                    DateTime alarmTime;
                                    if (DateTime.TryParse(alarm.body.alarmTime, out alarmTime))
                                    {
                                        if ((DateTime.Now - alarmTime).Minutes > historyAlarmJudge)//如果报警时间早于historyAlarmJudge设置值，则认为是历史数据，不发出报警
                                        {
                                            FileWorker.LogHelper.WriteLog("报警时间过早，判定为历史报警，不发送消息。"+alarm.body.alarmTime);
                                        }
                                        else
                                        {
                                            KafkaWorker.sendAlarmMessage(alarm.toJson());
                                        }
                                    }
                                    else
                                    {
                                        FileWorker.LogHelper.WriteLog("以下报警时间无法转化为时间类型数据："+alarm.body.alarmTime);
                                    }
                                    if ((DateTime.Now - startTime).Minutes > historyAlarmFilterDuration)//当超过historyAlarmFilterDuration设置时间后，不再判定历史数据
                                    {
                                        filterHistoryAlarm = false;
                                        FileWorker.LogHelper.WriteLog("历史报警数据过滤时间已到，后续的数据不再进行历史数据过滤");
                                    }
                                }
                                else
                                {
                                    KafkaWorker.sendAlarmMessage(alarm.toJson());
                                }
                                continue;
                            }
                        }
                        if (accessRuleDic.ContainsKey(eventId))//开门
                        {
                            FileWorker.LogHelper.WriteLog("刷卡数据 " + sMessage.Replace("<", "[").Replace(">", "]"));
                            AccessEntity access = AccessParseTool.parseAccess(sMessage, airportIata,accessRuleDic[eventId]);
                            KafkaWorker.sendAccessMessage(access.toJson());
                            continue;
                        }
                    }
                }
            }
        }

        private string getIdx(string message)
        {
            Match matchIdx = regexIdx.Match(message);
            if (matchIdx.Success)
            {
                return matchIdx.Value.Replace(@"<Idx>", "").Replace(@"</Idx>", "");
            }
            else
            {
                return string.Empty;
            }
        }

        private string getEventId(string message)
        {
            Match matchEventID = regexEventID.Match(message);
            if (matchEventID.Success)
            {
                return matchEventID.Value.Replace(@"<EventID>", "").Replace(@"</EventID>", "");
            }
            else
            {
                return string.Empty;
            }
        }

        private void openDoor(int hid)
        {
            try
            {
                server.PulseByHID(hid);
                FileWorker.LogHelper.WriteLog("完成对HID为" + hid + "门禁开门操作");
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("对HID为" + hid + "的门禁开门操作时出现异常:" + ex.Message);
            }
        }

        private void getAllDevice()
        {
            FileWorker.LogHelper.WriteLog("调用ListConnectedDevices方法");
            try
            {
                var devices = server.ListConnectedDevices();
                FileWorker.LogHelper.WriteLog("devices类型是" + devices.GetType().FullName);
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("获取变量devices类型时异常："+ex.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            logout();
        }
    }
}
