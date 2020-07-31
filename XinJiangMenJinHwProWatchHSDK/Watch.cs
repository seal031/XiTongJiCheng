using HoneywellAccess.HSDK.oBIX;
using HoneywellAccess.SmartPlus.IntegrationServer.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ServiceProcess;

public static class Watches
{
    public static Dictionary<string, Watch> ActiveWatches = new Dictionary<string, Watch>();
    public static Dictionary<string, Watch> LastAdded = new Dictionary<string, Watch>();
    public static Dictionary<string, Watch> Selected = new Dictionary<string, Watch>();
}

public class Watch : ServiceBase
{
    public string name;
    public WatchType type;
    public long pollInterval;
    public double leaseInterval;
    private SynchronizationContext synchronizationContext;
    public UpdateUIHandler updateUIHandler;
    public LogHandler logMessage;
    public HttpManager httpManager;
    public Timer tmrPollingManager;
    public static object pollingInProgress = new object();
    private string watchUrl = "";
    public bool stopped;
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
        this.logMessage("Creating Watch [" + this.name + "]", SmartPlus_LOG_TYPE.TRACE);
        Obj obj = this.httpManager.SendRequest(HSDKConfiguration.WatchUrl, "", MethodType.POST);
        if (obj.isErr())
        {
            throw new Exception(obj.Display);
        }
        this.watchUrl = obj.NormalizedHref.ToString();
    }
    public void SetLease()
    {
        this.logMessage("Setting Lease for Watch [" + this.name + "]", SmartPlus_LOG_TYPE.TRACE);
        this.httpManager.SendRequest(this.watchUrl + "lease/", "<reltime val='PT" + this.leaseInterval.ToString() + "S'/>", MethodType.PUT);
    }
    public Obj SubscribeToWatch()
    {
        this.logMessage("Subscribing event feed to Watch [" + this.name + "]", SmartPlus_LOG_TYPE.TRACE);
        return this.httpManager.SendRequest(this.watchUrl + "add/", this.GetWatchXml(), MethodType.POST);
    }
    public Obj SubscribeToWatch(string url)
    {
        this.logMessage(string.Concat(new string[]
        {
                "Subscribing object (",
                url,
                ") to Watch [",
                this.name,
                "]"
        }), SmartPlus_LOG_TYPE.TRACE);
        return this.httpManager.SendRequest(this.watchUrl + "add/", this.GetWatchXml(url), MethodType.POST);
    }
    public Obj SubscribeToWatch(string[] urls)
    {
        this.logMessage(string.Concat(new string[]
        {
                "Subscribing objects (",
                this.GetFormattedUrls(urls),
                ") to Watch [",
                this.name,
                "]"
        }), SmartPlus_LOG_TYPE.TRACE);
        return this.httpManager.SendRequest(this.watchUrl + "add/", this.GetWatchXml(urls), MethodType.POST);
    }
    public void UnSubscribeToWatch(string url)
    {
        this.logMessage(string.Concat(new string[]
        {
                "Un-Subscribing object (",
                url,
                ") from Watch [",
                this.name,
                "]"
        }), SmartPlus_LOG_TYPE.TRACE);
        this.httpManager.SendRequest(this.watchUrl + "remove/", this.GetWatchXml(url), MethodType.POST);
    }
    public void UnSubscribeToWatch(string[] urls)
    {
        this.logMessage(string.Concat(new string[]
        {
                "Un-Subscribing objects (",
                this.GetFormattedUrls(urls),
                ") from Watch [",
                this.name,
                "]"
        }), SmartPlus_LOG_TYPE.TRACE);
        this.httpManager.SendRequest(this.watchUrl + "remove/", this.GetWatchXml(urls), MethodType.POST);
    }
    public void DeleteWatch()
    {
        this.logMessage("Deleting Watch [" + this.name + "]", SmartPlus_LOG_TYPE.TRACE);
        this.httpManager.SendRequest(this.watchUrl + "delete/", "", MethodType.POST);
    }
    public void StartPolling()
    {
        TimerCallback callback = new TimerCallback(this.tmrPollingInterval_Elapsed);
        this.tmrPollingManager = new Timer(callback, this, 0L, this.pollInterval);
    }
    private void tmrPollingInterval_Elapsed(object state)
    {
        if (this.pollInterval.Equals(-1L))
        {
            return;
        }
        object obj;
        Monitor.Enter(obj = Watch.pollingInProgress);
        try
        {
            if (!this.pollInterval.Equals(-1L))
            {
                try
                {
                    this.PollHsdkServer((Watch)state);
                }
                catch (Exception ex)
                {
                    this.logMessage(ex.ToString(), SmartPlus_LOG_TYPE.EXCEPTION);
                }
            }
        }
        finally
        {
            Monitor.Exit(obj);
        }
    }
    private void PollHsdkServer(Watch watch)
    {
        this.logMessage("Polling Watch [" + this.name + "]", SmartPlus_LOG_TYPE.TRACE);
        Obj obj = this.httpManager.SendRequest(this.watchUrl + "pollChanges/", "", MethodType.POST);
        this.synchronizationContext.Send(delegate (object state)
        {
            this.updateUIHandler(obj, watch);
        }, null);
    }
    public void PollRefresh()
    {
        this.logMessage("Poll Refresh for Watch [" + this.name + "]", SmartPlus_LOG_TYPE.TRACE);
        Obj obj = this.httpManager.SendRequest(this.watchUrl + "pollRefresh/", "", MethodType.POST);
        this.synchronizationContext.Send(delegate (object state)
        {
            this.updateUIHandler(obj, this);
        }, null);
    }
    private string GetWatchXml()
    {
        return this.GetWatchXml(HSDKConfiguration.FeedUrl);
    }
    private string GetWatchXml(string url)
    {
        return this.GetWatchXml(new string[]
        {
                url
        });
    }
    private string GetWatchXml(string[] urls)
    {
        string text = "";
        for (int i = 0; i < urls.Length; i++)
        {
            string str = urls[i];
            text = text + "<uri val='" + str + "'/>";
        }
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<obj is='obix:WatchIn'>\r\n                        <list name='hrefs'>\r\n                            " + text + "\r\n                        </list>\r\n                       </obj>");
        return stringBuilder.ToString();
    }
    private string GetFormattedUrls(string[] urls)
    {
        string text = Environment.NewLine;
        for (int i = 0; i < urls.Length; i++)
        {
            string str = urls[i];
            text = text + str + Environment.NewLine;
        }
        return text;
    }
}
public delegate void UpdateUIHandler(Obj obj, Watch watch);
