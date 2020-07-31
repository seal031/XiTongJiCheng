using System;
using System.Configuration;

internal static class HSDKConfiguration
{
    private static string domain;
    private static string username;
    private static string password;
    private static string lobbyUrl;
    private static string watchUrl;
    private static string feedUrl;
    private static string protocol;
    private static string serverName;
    private static string port;
    private static string vDName;
    private static string certFilePath;
    private static bool createMultipleAlarms;
    private static bool addObjToAlarmWatch;
    private static int maxAlarms;
    private static int maxTreeLevel;
    private static bool showPropertiesInTree;
    private static bool showComboForEnum;
    private static bool soapRequest;
    public static string Port
    {
        get
        {
            return HSDKConfiguration.port;
        }
        set
        {
            HSDKConfiguration.port = value;
        }
    }
    public static string Domain
    {
        get
        {
            return HSDKConfiguration.domain;
        }
    }
    public static string Username
    {
        get
        {
            return HSDKConfiguration.username;
        }
        set
        {
            HSDKConfiguration.username = value;
        }
    }
    public static string Password
    {
        get
        {
            return HSDKConfiguration.password;
        }
        set
        {
            HSDKConfiguration.password = value;
        }
    }
    public static string LobbyUrl
    {
        get
        {
            return HSDKConfiguration.lobbyUrl;
        }
        set
        {
            HSDKConfiguration.lobbyUrl = value;
        }
    }
    public static string WatchUrl
    {
        get
        {
            return HSDKConfiguration.watchUrl;
        }
        set
        {
            HSDKConfiguration.watchUrl = value;
        }
    }
    public static string FeedUrl
    {
        get
        {
            return HSDKConfiguration.feedUrl;
        }
        set
        {
            HSDKConfiguration.feedUrl = value;
        }
    }
    public static string Protocol
    {
        get
        {
            return HSDKConfiguration.protocol;
        }
        set
        {
            HSDKConfiguration.protocol = value;
        }
    }
    public static string ServerName
    {
        get
        {
            return HSDKConfiguration.serverName;
        }
        set
        {
            HSDKConfiguration.serverName = value;
        }
    }
    public static string VDName
    {
        get
        {
            return HSDKConfiguration.vDName;
        }
        set
        {
            HSDKConfiguration.vDName = value;
        }
    }
    public static string CertFilePath
    {
        get
        {
            return HSDKConfiguration.certFilePath;
        }
        set
        {
            HSDKConfiguration.certFilePath = value;
        }
    }
    public static bool CreateMultipleAlarms
    {
        get
        {
            return HSDKConfiguration.createMultipleAlarms;
        }
        set
        {
            HSDKConfiguration.createMultipleAlarms = value;
        }
    }
    public static bool AddObjToAlarmWatch
    {
        get
        {
            return HSDKConfiguration.addObjToAlarmWatch;
        }
        set
        {
            HSDKConfiguration.addObjToAlarmWatch = value;
        }
    }
    public static int MaxAlarms
    {
        get
        {
            return HSDKConfiguration.maxAlarms;
        }
        set
        {
            HSDKConfiguration.maxAlarms = value;
        }
    }
    public static int MaxTreeLevel
    {
        get
        {
            return HSDKConfiguration.maxTreeLevel;
        }
        set
        {
            HSDKConfiguration.maxTreeLevel = value;
        }
    }
    public static bool ShowPropertiesInTree
    {
        get
        {
            return HSDKConfiguration.showPropertiesInTree;
        }
        set
        {
            HSDKConfiguration.showPropertiesInTree = value;
        }
    }
    public static bool ShowComboForEnum
    {
        get
        {
            return HSDKConfiguration.showComboForEnum;
        }
        set
        {
            HSDKConfiguration.showComboForEnum = value;
        }
    }
    public static bool SoapRequest
    {
        get
        {
            return HSDKConfiguration.soapRequest;
        }
        set
        {
            HSDKConfiguration.soapRequest = value;
        }
    }
    static HSDKConfiguration()
    {
        HSDKConfiguration.domain = "";
        HSDKConfiguration.username = ConfigurationManager.AppSettings["Username"];
        HSDKConfiguration.password = ConfigurationManager.AppSettings["Password"];
        HSDKConfiguration.LoadUrl(ConfigurationManager.AppSettings["Protocol"], ConfigurationManager.AppSettings["ServerName"], ConfigurationManager.AppSettings["Port"], ConfigurationManager.AppSettings["VDName"]);
        HSDKConfiguration.certFilePath = ConfigurationManager.AppSettings["CertFilePath"];
        HSDKConfiguration.createMultipleAlarms = ConfigurationManager.AppSettings["CreateMultipleAlarms"].Equals("1");
        HSDKConfiguration.addObjToAlarmWatch = ConfigurationManager.AppSettings["AddObjToAlarmWatch"].Equals("1");
        HSDKConfiguration.maxAlarms = int.Parse(ConfigurationManager.AppSettings["MaxAlarms"]);
        HSDKConfiguration.maxTreeLevel = int.Parse(ConfigurationManager.AppSettings["MaxTreeLevel"]);
        HSDKConfiguration.showPropertiesInTree = ConfigurationManager.AppSettings["ShowPropertiesInTree"].Equals("1");
        HSDKConfiguration.showComboForEnum = ConfigurationManager.AppSettings["ShowComboForEnum"].Equals("1");
        HSDKConfiguration.soapRequest = ConfigurationManager.AppSettings["SoapRequest"].Equals("1");
    }
    public static void LoadUrl(string hsdkProtocol, string hsdkServerName, string hsdkPort, string hsdkVDName)
    {
        HSDKConfiguration.protocol = hsdkProtocol;
        HSDKConfiguration.serverName = hsdkServerName;
        HSDKConfiguration.vDName = hsdkVDName;
        HSDKConfiguration.port = hsdkPort;
        Uri uri = new Uri(string.Concat(new string[]
        {
                HSDKConfiguration.protocol,
                "://",
                HSDKConfiguration.serverName,
                ":",
                HSDKConfiguration.port,
                "/",
                HSDKConfiguration.vDName,
                "/"
        }));
        Uri uri2;
        Uri.TryCreate(uri, "/" + HSDKConfiguration.vDName + "/watchService/make/", out uri2);
        Uri uri3;
        Uri.TryCreate(uri, "/" + HSDKConfiguration.vDName + "/pacs/alarms/feed/", out uri3);
        HSDKConfiguration.lobbyUrl = uri.AbsoluteUri;
        HSDKConfiguration.watchUrl = uri2.AbsoluteUri;
        HSDKConfiguration.feedUrl = uri3.AbsoluteUri;
    }
    public static void SaveConfigurationToFile()
    {
        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        configuration.AppSettings.Settings["Protocol"].Value = HSDKConfiguration.Protocol;
        configuration.AppSettings.Settings["ServerName"].Value = HSDKConfiguration.ServerName;
        configuration.AppSettings.Settings["Port"].Value = HSDKConfiguration.Port;
        configuration.AppSettings.Settings["VDName"].Value = HSDKConfiguration.VDName;
        configuration.AppSettings.Settings["CertFilePath"].Value = HSDKConfiguration.CertFilePath;
        configuration.AppSettings.Settings["CreateMultipleAlarms"].Value = (HSDKConfiguration.CreateMultipleAlarms ? "1" : "0");
        configuration.AppSettings.Settings["AddObjToAlarmWatch"].Value = (HSDKConfiguration.AddObjToAlarmWatch ? "1" : "0");
        configuration.AppSettings.Settings["MaxAlarms"].Value = HSDKConfiguration.MaxAlarms.ToString();
        configuration.AppSettings.Settings["MaxTreeLevel"].Value = HSDKConfiguration.MaxTreeLevel.ToString();
        configuration.AppSettings.Settings["ShowPropertiesInTree"].Value = (HSDKConfiguration.ShowPropertiesInTree ? "1" : "0");
        configuration.AppSettings.Settings["ShowComboForEnum"].Value = (HSDKConfiguration.ShowComboForEnum ? "1" : "0");
        configuration.AppSettings.Settings["SoapRequest"].Value = (HSDKConfiguration.SoapRequest ? "1" : "0");
        configuration.AppSettings.Settings["Username"].Value = HSDKConfiguration.Username;
        configuration.AppSettings.Settings["Password"].Value = HSDKConfiguration.Password;
        configuration.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("appSettings");
    }
}
