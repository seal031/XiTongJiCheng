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
        Watch watch;
        WatchType type = WatchType.Alarm;
        long interval = 1000;
        double lease = 600;
        
        public HttpManager httpManager;

        public Form1()
        {
            InitializeComponent();
            httpManager = new HttpManager(LogMessage, ShowStatusBar);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            watch = new Watch("watch", type, interval, lease, WindowsFormsSynchronizationContext.Current, UpdateTestClient, LogMessage, httpManager);
            watch.StartPolling();
        }


        public void UpdateTestClient(Obj obixObj, Watch watch)
        {
            if (watch.type == WatchType.Alarm)
            {
                if (obixObj.list().Length > 0)
                    if (obixObj.list()[0].list().Length > 0)
                        if (obixObj.list()[0].list()[0].list().Length > 0)
                        {
                            foreach (Obj obj in obixObj.list()[0].list()[0].list())
                            {
                                object time = ((Abstime)obj.get("timestamp")).Val;
                                object type = ((Int)obj.get("eventType")).Val;
                                object desc = obj.Display;
                                object id = obj.Href;
                                object ack = obj.get("ack");
                                object clear = obj.get("clear");
                                object pointDescription = null;
                                object priority = ((Int)obj.get("priority")).Val;
                                object source = null;
                                object alarmType = null;
                                object fname = null;
                                object lname = null;
                                object cardnumber = null;
                                object user = null;
                                object card = null;

                                if (obj.get("source") != null)
                                    source = obj.get("source").Href;
                                if (obj.get("fname") != null)
                                    fname = obj.get("fname").Href;
                                if (obj.get("lname") != null)
                                    lname = obj.get("lname").Href;
                                if (obj.get("cardnumber") != null)
                                    cardnumber = obj.get("cardnumber").Href;
                                if (obj.get("accessUser") != null)
                                    user = obj.get("accessUser").Href;
                                if (obj.get("accessCredential") != null)
                                    card = obj.get("accessCredential").Href;
                                if (obj.get("AlarmStatus") != null)
                                    alarmType = ((Str)obj.get("AlarmStatus")).Val;
                                if (obj.get("pointdescription") != null)
                                    pointDescription = ((Str)obj.get("pointdescription")).Val;

                                //grd.Rows.Insert(0, time, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), type, desc, alarmType, id, source, fname, lname, cardnumber, user, card, ack, clear, pointDescription, priority);
                                //if (grd.Rows.Count > Configuration.MaxAlarms)
                                //    grd.Rows.RemoveAt(Configuration.MaxAlarms);
                            }
                        }
            }
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
        }
        private void LogMessage(string message, SmartPlus_LOG_TYPE logType)
        { }
        private void ShowStatusBar(string message, MessageType messageType)
        { }
    }
}
