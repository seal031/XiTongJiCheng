/////////////////////////////////////////////////////////////////////////////////////////////////////
//				COPYRIGHT (c) 2009
//				  HONEYWELL INC.,
// 			    ALL RIGHTS RESERVED
//
//    This software is a copyrighted work and/or information protected 
//    as a trade secret. Legal rights of Honeywell Inc. in this software 
//    is distinct from ownership of any medium in which the software is 
//    embodied. Copyright or trade secret notices included must be reproduced
//    in any copies authorized by Honeywell Inc. The information in 
//    this software is subject to change without notice and should not 
//    be considered as a commitment by Honeywell Inc.
//
//
//
// 	File Name			:	Watch.cs
// 	Project Title		:	HSDK Test Client
//	Author(s)			:   Anand Patil
//	Function        	:	Represent an oBIX Watch on PACS Objects / Events
/////////////////////////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Runtime.Serialization;
//using System.ServiceProcess;
using System.Xml;
using HoneywellAccess.SmartPlus.IntegrationServer.Logging;
using HoneywellAccess.SmartPlus.IntegrationServer.Helpers;
using HoneywellAccess.HSDK.oBIX;
using HoneywellAccess.HSDK.oBIX.IO;
using System.Windows.Forms;
using System.Collections;

    public delegate void UpdateUIHandler(Obj obj, Watch watch);

    public partial class Watch
    {
        #region Members
        public string name;
        public WatchType type;
        public long pollInterval;
        public double leaseInterval;
        SynchronizationContext synchronizationContext;
        public UpdateUIHandler updateUIHandler;
        public LogHandler logMessage;
        public HttpManager httpManager;

        public System.Threading.Timer tmrPollingManager;
        public static object pollingInProgress = new object();//For Locking
        string watchUrl = "";
        public bool stopped = false;
        #endregion

        public Watch(string name, WatchType type, long pollInterval, double leaseInterval, SynchronizationContext synchronizationContext, UpdateUIHandler updateUIHandler, LogHandler logMessage, HttpManager httpManager)
        {
            this.name = name;
            this.type = type;
            this.pollInterval = pollInterval;
            this.leaseInterval = leaseInterval;
            this.synchronizationContext = synchronizationContext;
            this.updateUIHandler = updateUIHandler;
            this.logMessage = logMessage;
            this.httpManager = httpManager;
        }

        public void CreateWatch()
        {
            logMessage("Creating Watch [" + name + "]", SmartPlus_LOG_TYPE.TRACE);
            Obj watchObj = httpManager.SendRequest(HSDKConfiguration.WatchUrl, "", MethodType.POST);

            if (watchObj.isErr())//No License to access WatchService
                throw new Exception(watchObj.Display);

            watchUrl = watchObj.NormalizedHref.ToString();
        }

        public void SetLease()
        {
            logMessage("Setting Lease for Watch [" + name + "]", SmartPlus_LOG_TYPE.TRACE);
            Obj leaseObj = httpManager.SendRequest(watchUrl + "lease/", "<reltime val='PT" + leaseInterval.ToString() + "S'/>", MethodType.PUT);
        }

        public Obj SubscribeToWatch()
        {
            //WatchType.Alarm
            logMessage("Subscribing event feed to Watch [" + name + "]", SmartPlus_LOG_TYPE.TRACE);
            Obj obj = httpManager.SendRequest(watchUrl + "add/", GetWatchXml(), MethodType.POST);
            return obj;
        }
        public Obj SubscribeToWatch(string url)
        {
            //WatchType.Object;
            logMessage("Subscribing object (" + url + ") to Watch [" + name + "]", SmartPlus_LOG_TYPE.TRACE);
            Obj obj = httpManager.SendRequest(watchUrl + "add/", GetWatchXml(url), MethodType.POST);
            return obj;
        }
        public Obj SubscribeToWatch(string[] urls)
        {
            //WatchType.Object;
            logMessage("Subscribing objects (" + GetFormattedUrls(urls) + ") to Watch [" + name + "]", SmartPlus_LOG_TYPE.TRACE);
            Obj obj = httpManager.SendRequest(watchUrl + "add/", GetWatchXml(urls), MethodType.POST);
            return obj;
        }

        public void UnSubscribeToWatch(string url)
        {
            logMessage("Un-Subscribing object (" + url + ") from Watch [" + name + "]", SmartPlus_LOG_TYPE.TRACE);
            Obj obj = httpManager.SendRequest(watchUrl + "remove/", GetWatchXml(url), MethodType.POST);
        }
        public void UnSubscribeToWatch(string[] urls)
        {
            logMessage("Un-Subscribing objects (" + GetFormattedUrls(urls) + ") from Watch [" + name + "]", SmartPlus_LOG_TYPE.TRACE);
            Obj obj = httpManager.SendRequest(watchUrl + "remove/", GetWatchXml(urls), MethodType.POST);
        }

        public void DeleteWatch()
        {
            logMessage("Deleting Watch [" + name + "]", SmartPlus_LOG_TYPE.TRACE);
            Obj obj = httpManager.SendRequest(watchUrl + "delete/", "", MethodType.POST);
        }

        public void StartPolling()
        {
            TimerCallback timerCallback = new TimerCallback(tmrPollingInterval_Elapsed);
            tmrPollingManager = new System.Threading.Timer(timerCallback, this, 0, pollInterval);
        }

        /// <summary>
        /// Fires after Timer Interval is Elapsed
        /// </summary>
        /// <param name="state"><para>Represents watch to be polled</para></param>
        void tmrPollingInterval_Elapsed(object state)
        {
            if (pollInterval.Equals(-1)) //User has stopped the watch
                return;

            lock (pollingInProgress)
            {
                //One thread is polling,
                //dont allow another thread.

                if (pollInterval.Equals(-1)) //User has stopped the watch
                    return;

                try
                {
                    PollHsdkServer((Watch)state);
                }
                catch (Exception ex)
                {
                    logMessage(ex.ToString(), SmartPlus_LOG_TYPE.EXCEPTION);
                }
            }
        }

        private void PollHsdkServer(Watch watch)
        {
            logMessage("Polling Watch [" + name + "]", SmartPlus_LOG_TYPE.TRACE);
            Obj obj = httpManager.SendRequest(watchUrl + "pollChanges/", "", MethodType.POST);

            // Execute on the UI thread
            synchronizationContext.Send(new SendOrPostCallback(delegate (object state)
            {
                updateUIHandler(obj, watch);
            }), null);
        }
        public void PollRefresh()
        {
            logMessage("Poll Refresh for Watch [" + name + "]", SmartPlus_LOG_TYPE.TRACE);
            Obj obj = httpManager.SendRequest(watchUrl + "pollRefresh/", "", MethodType.POST);

            // Execute on the UI thread
            synchronizationContext.Send(new SendOrPostCallback(delegate (object state)
            {
                updateUIHandler(obj, this);
            }), null);
        }

        string GetWatchXml()
        {
            return GetWatchXml(HSDKConfiguration.FeedUrl);
        } 
        string GetWatchXml(string url)
        {
            return GetWatchXml(new string[] { url });
        }
        string GetWatchXml(string[] urls)
        {
            string uri = "";
            foreach (string url in urls)
            {
                uri = uri + "<uri val='" + url + @"'/>";
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(@"<obj is='obix:WatchIn'>
                        <list name='hrefs'>
                            " + uri + @"
                        </list>
                       </obj>");
            return sb.ToString();
        }

        string GetFormattedUrls(string[] urls)
        {
            string uri = Environment.NewLine;
            foreach (string url in urls)
            {
                uri = uri + url + Environment.NewLine;
            }
            return uri;
        }
    }

public static class Watches
{
    public static Dictionary<string, Watch> ActiveWatches = new Dictionary<string, Watch>();
    public static Dictionary<string, Watch> LastAdded = new Dictionary<string, Watch>();
    public static Dictionary<string, Watch> Selected = new Dictionary<string, Watch>();
}