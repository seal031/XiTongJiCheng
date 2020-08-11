using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

static class HSDKConfiguration
{
    #region Members

    static string domain;
    static string username;
    static string password;
    static string lobbyUrl;
    static string watchUrl;
    static string feedUrl;

    static string protocol;
    static string serverName;
    static string port;
    static string vDName;
    static string certFilePath;

    static bool createMultipleAlarms;
    static bool addObjToAlarmWatch;
    static int maxAlarms;
    static int maxTreeLevel;
    static bool showPropertiesInTree;
    static bool showComboForEnum;
    static bool soapRequest;
    #endregion

    #region Properties

    public static string Port
    {
        get { return port; }
        set { port = value; }
    }
    public static string Domain
    {
        get { return domain; }
    }
    public static string Username
    {
        get { return username; }
        set { username = value; }
    }
    public static string Password
    {
        get { return password; }
        set { password = value; }
    }
    public static string LobbyUrl
    {
        get { return lobbyUrl; }
        set { lobbyUrl = value; }
    }
    public static string WatchUrl
    {
        get { return watchUrl; }
        set { watchUrl = value; }
    }
    public static string FeedUrl
    {
        get { return feedUrl; }
        set { feedUrl = value; }
    }

    public static string Protocol
    {
        get { return protocol; }
        set { protocol = value; }
    }
    public static string ServerName
    {
        get { return serverName; }
        set { serverName = value; }
    }
    public static string VDName
    {
        get { return vDName; }
        set { vDName = value; }
    }
    public static string CertFilePath
    {
        get { return certFilePath; }
        set { certFilePath = value; }
    }

    public static bool CreateMultipleAlarms
    {
        get { return createMultipleAlarms; }
        set { createMultipleAlarms = value; }
    }
    public static bool AddObjToAlarmWatch
    {
        get { return addObjToAlarmWatch; }
        set { addObjToAlarmWatch = value; }
    }
    public static int MaxAlarms
    {
        get { return maxAlarms; }
        set { maxAlarms = value; }
    }
    public static int MaxTreeLevel
    {
        get { return maxTreeLevel; }
        set { maxTreeLevel = value; }
    }
    public static bool ShowPropertiesInTree
    {
        get { return showPropertiesInTree; }
        set { showPropertiesInTree = value; }
    }
    public static bool ShowComboForEnum
    {
        get { return showComboForEnum; }
        set { showComboForEnum = value; }
    }
    public static bool SoapRequest
    {
        get { return soapRequest; }
        set { soapRequest = value; }
    }

    #endregion

    #region Constructor
    static HSDKConfiguration()
    {
        domain = "";
        username = System.Configuration.ConfigurationManager.AppSettings["Username"];
        password = System.Configuration.ConfigurationManager.AppSettings["Password"];
        LoadUrl(System.Configuration.ConfigurationManager.AppSettings["Protocol"],
            System.Configuration.ConfigurationManager.AppSettings["ServerName"],
            System.Configuration.ConfigurationManager.AppSettings["Port"],
            System.Configuration.ConfigurationManager.AppSettings["VDName"]);
        certFilePath = System.Configuration.ConfigurationManager.AppSettings["CertFilePath"];
        createMultipleAlarms = System.Configuration.ConfigurationManager.AppSettings["CreateMultipleAlarms"].Equals("1") ? true : false;
        addObjToAlarmWatch = System.Configuration.ConfigurationManager.AppSettings["AddObjToAlarmWatch"].Equals("1") ? true : false;
        maxAlarms = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxAlarms"]);
        maxTreeLevel = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxTreeLevel"]);
        showPropertiesInTree = System.Configuration.ConfigurationManager.AppSettings["ShowPropertiesInTree"].Equals("1") ? true : false;
        showComboForEnum = System.Configuration.ConfigurationManager.AppSettings["ShowComboForEnum"].Equals("1") ? true : false;
        soapRequest = System.Configuration.ConfigurationManager.AppSettings["SoapRequest"].Equals("1") ? true : false;
    }
    #endregion

    public static void LoadUrl(string hsdkProtocol, string hsdkServerName, string hsdkPort, string hsdkVDName)
    {
        protocol = hsdkProtocol;
        serverName = hsdkServerName;
        vDName = hsdkVDName;
        port = hsdkPort;

        System.Uri tempWatchUrl;
        System.Uri tempFeedUrl;

        System.Uri tempLobbyUrl = new System.Uri(protocol + "://" + serverName + ":" + port + "/" + vDName + "/");
        System.Uri.TryCreate(tempLobbyUrl, "/" + vDName + "/watchService/make/", out tempWatchUrl);
        System.Uri.TryCreate(tempLobbyUrl, "/" + vDName + "/pacs/alarms/feed/", out tempFeedUrl);

        lobbyUrl = tempLobbyUrl.AbsoluteUri;
        watchUrl = tempWatchUrl.AbsoluteUri;
        feedUrl = tempFeedUrl.AbsoluteUri;
    }

    /// <summary>
    /// Saves configuration to .config file.
    /// </summary>
    public static void SaveConfigurationToFile()
    {
        System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        config.AppSettings.Settings["Protocol"].Value = Protocol;
        config.AppSettings.Settings["ServerName"].Value = ServerName;
        config.AppSettings.Settings["Port"].Value = Port;
        config.AppSettings.Settings["VDName"].Value = VDName;
        config.AppSettings.Settings["CertFilePath"].Value = CertFilePath;
        config.AppSettings.Settings["CreateMultipleAlarms"].Value = CreateMultipleAlarms == true ? "1" : "0";
        config.AppSettings.Settings["AddObjToAlarmWatch"].Value = AddObjToAlarmWatch == true ? "1" : "0";
        config.AppSettings.Settings["MaxAlarms"].Value = MaxAlarms.ToString();
        config.AppSettings.Settings["MaxTreeLevel"].Value = MaxTreeLevel.ToString();
        config.AppSettings.Settings["ShowPropertiesInTree"].Value = ShowPropertiesInTree == true ? "1" : "0";
        config.AppSettings.Settings["ShowComboForEnum"].Value = ShowComboForEnum == true ? "1" : "0";
        config.AppSettings.Settings["SoapRequest"].Value = SoapRequest == true ? "1" : "0";
        config.AppSettings.Settings["Username"].Value = Username;
        config.AppSettings.Settings["Password"].Value = Password;

        config.Save(ConfigurationSaveMode.Modified);
        ConfigurationManager.RefreshSection("appSettings");
    }
}