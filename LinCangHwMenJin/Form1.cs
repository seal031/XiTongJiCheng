using HoneywellAccess.HSDK.oBIX;
using HoneywellAccess.SmartPlus.IntegrationServer.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinCangHwMenJin
{
    public partial class Form1 : Form
    {
        private string airportIata;
        private string airportName;
        Dictionary<string, Tuple<string, string>> alarmRuleDic = new Dictionary<string, Tuple<string, string>>();
        Watch watch;
        string watchName = "";
        WatchType type = WatchType.Alarm;
        long interval = 1000;
        double lease = 600;
        
        public HttpManager httpManager;

        public Form1()
        {
            //var date = DataFormatTool.formatDatetime("2020/10/23 19:12:32");
            InitializeComponent();
            httpManager = new HttpManager(LogMessage, ShowStatusBar);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            loadLocalConfig();
            watch = new Watch(watchName, type, interval, lease, WindowsFormsSynchronizationContext.Current, 
                UpdateTestClient, LogMessage, httpManager);
            watch.CreateWatch();
            watch.SetLease();
            if (watch.type == WatchType.Alarm)
            {
                Obj obj = watch.SubscribeToWatch();
                FileWorker.LogHelper.WriteLog("watch订阅");
            }
            watch.StartPolling();
        }

        private void loadLocalConfig()
        {
            try
            {
                watchName = ConfigWorker.GetConfigValue("watchName");
                interval = int.Parse(ConfigWorker.GetConfigValue("interval"));
                lease = double.Parse( ConfigWorker.GetConfigValue("lease"));
                airportIata = ConfigWorker.GetConfigValue("airportIata");
                airportName = ConfigWorker.GetConfigValue("airportName");
                string alarmFilterRule = ConfigWorker.GetConfigValue("alarmFilterRule");
                foreach (string filterRule in alarmFilterRule.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string eventCode = filterRule.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0];
                    string sodbCode = filterRule.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1];
                    string alarmNameCode = sodbCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0];
                    string alarmName = sodbCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[1];
                    alarmRuleDic.Add(eventCode, new Tuple<string, string>(alarmNameCode, alarmName));
                }
                FileWorker.LogHelper.WriteLog("配置加载完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show("配置文件设置不正确"+ex.Message);
            }
        }


        public void UpdateTestClient(Obj obixObj, Watch watch)
        {
            //FileWorker.LogHelper.WriteLog("收到数据："+watch.type.ToString()+";;长度是"+obixObj.list().Length.ToString());
            if (watch.type == WatchType.Alarm)
            {
                if (obixObj.list().Length > 0)
                    if (obixObj.list()[0].list().Length > 0)
                    {
                        //FileWorker.LogHelper.WriteLog("obixObj.list()[0].list().Length长度是" + obixObj.list()[0].list().Length.ToString());
                        if (obixObj.list()[0].list()[0].list().Length > 0)
                        {
                            //FileWorker.LogHelper.WriteLog("obixObj.list()[0].list()[0].list().Length长度是" + obixObj.list()[0].list()[0].list().Length.ToString());
                            foreach (Obj obj in obixObj.list()[0].list()[0].list())
                            {
                                try
                                {
                                    string time = ((Abstime)obj.get("timestamp")).Val.ToString();
                                    string type = ((Int)obj.get("eventType")).Val.ToString();
                                    string desc = obj.Display.ToString();
                                    object id = obj.Href;
                                    object ack = obj.get("ack");
                                    object clear = obj.get("clear");
                                    object pointDescription = null;
                                    object priority = ((Int)obj.get("priority")).Val;
                                    string source = string.Empty;
                                    string alarmType = null;
                                    string fname = string.Empty;
                                    string lname = string.Empty;
                                    object cardnumber = null;
                                    string user = string.Empty;
                                    string card = string.Empty;
                                    string deviceName = string.Empty;
                                    string deptName = string.Empty;

                                    if (obj.get("companyname") != null)
                                        deptName = obj.get("companyname").ToString();
                                    if (obj.get("devicedescription") != null)
                                        deviceName = obj.get("devicedescription").ToString();
                                    if (obj.get("source") != null)
                                        source = obj.get("source").Href.ToString();
                                    if (obj.get("fname") != null)
                                        fname = obj.get("fname").ToString();
                                    if (obj.get("lname") != null)
                                        lname = obj.get("lname").ToString();
                                    if (obj.get("cardnumber") != null)
                                        cardnumber = obj.get("cardnumber").Href;
                                    if (obj.get("accessUser") != null)
                                        user = obj.get("accessUser").Href.ToString();
                                    if (obj.get("accessCredential") != null)
                                        card = obj.get("accessCredential").Href.ToString();
                                    if (obj.get("AlarmStatus") != null)
                                        alarmType = ((Str)obj.get("AlarmStatus")).Val;
                                    if (obj.get("pointdescription") != null)
                                        pointDescription = ((Str)obj.get("pointdescription")).Val;
                                    FileWorker.LogHelper.WriteLog(string.Format("eventType:{0};desc:{1};source:{2};user:{3};credential:{4}",
                                    type, desc, source, user, card));

                                    if (source != string.Empty)
                                    {
                                        source = DataFormatTool.pickTailFromString(source);
                                    }
                                    time = DataFormatTool.formatDatetime(time);
                                    //event type=500是刷卡,只有刷卡时才产生卡号、人员编号
                                    if (type == "500")
                                    {
                                        if (card != string.Empty)
                                        {
                                            card = DataFormatTool.pickTailFromString(card);
                                        }
                                        if (user != string.Empty)
                                        {
                                            user = DataFormatTool.pickTailFromString(user);
                                        }
                                        AccessEntity access = AccessParseTool.parseAccess(card, source, "1", user, time, deptName, deviceName, lname + fname,"","刷卡+密码开门");
                                        KafkaWorker.sendAccessMessage(access.toJson());
                                        continue;
                                    }
                                    if (type == "608")//按钮开门，无卡号、人员编号
                                    {
                                        AccessEntity access = AccessParseTool.parseAccess("", source, "2", "", time, "", deviceName, "","","按钮开门");
                                        KafkaWorker.sendAccessMessage(access.toJson());
                                        continue;
                                    }

                                    if (alarmRuleDic.ContainsKey(type))//报警
                                    {
                                        AlarmEntity alarm = AlarmParseTool.parseAlarm(source, alarmRuleDic[type].Item1, alarmRuleDic[type].Item2, time, airportIata, airportName);
                                        KafkaWorker.sendAlarmMessage(alarm.toJson());
                                        continue;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    FileWorker.LogHelper.WriteLog("消息接收及解析、发送过程中出现异常："+ex.Message);
                                    continue;
                                }
                            }
                        }
                    }
            }
            #region 暂时不用
            else //watch.type == WatchType.Object
            {
                if (obixObj.list().Length > 0)
                {
                    DateTime updateTime = DateTime.Now;
                    foreach (Obj obj in obixObj.list()[0].list())
                    {
                        //bool rowFound = false;
                        //for (int i = 0; i < grd.Rows.Count; i++)
                        //{
                        //    if (grd.Rows[i].Cells[0].Value.Equals(obj.Href))
                        //    {
                        //        if (obj.isErr())
                        //        {
                        //            //Object deleted in PW
                        //            grd.Rows.RemoveAt(i);
                        //        }
                        //        else
                        //        {
                        //            grd.Rows[i].Cells[1].Value = DateTime.Now;
                        //            updateTime = (DateTime)grd.Rows[i].Cells[1].Value;
                        //        }
                        //        rowFound = true;
                        //        break;
                        //    }
                        //}
                        //if (!rowFound)
                        //{
                        //    updateTime = DateTime.Now;
                        //    //grd.Rows.Add(obj.Href, updateTime);
                        //}

                        ////when object's parent url is subscribed.
                        //System.Uri uri = obj.Href.toUri();
                        //if (uri.Segments.Length >= 5)
                        //{
                        //    string strUri = uri.ToString();
                        //    if (strUri.EndsWith("/"))
                        //        strUri = strUri.Substring(0, strUri.Length - 1);
                        //    int nIndex = strUri.LastIndexOf("/");
                        //    strUri = strUri.Substring(0, nIndex);
                        //    for (int i = 0; i < grd.Rows.Count; i++)
                        //    {
                        //        string strObjHref = Convert.ToString(grd.Rows[i].Cells[0].Value);
                        //        if (strObjHref.EndsWith("/"))
                        //            strObjHref = strObjHref.Substring(0, strObjHref.Length - 1);
                        //        if (strObjHref.Equals(strUri))
                        //        {
                        //            grd.Rows[i].Cells[1].Value = updateTime;
                        //            break;
                        //        }
                        //    }
                        //}
                    }
                }
            }
                #endregion
        }
        private void LogMessage(string message, SmartPlus_LOG_TYPE logType)
        { }
        private void ShowStatusBar(string message, MessageType messageType)
        { }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
