using AxVSPOcxClientLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public class ConfigWorker
{
    public static string GetConfigValue(string key)
    {
        if (System.Configuration.ConfigurationManager.AppSettings[key] != null)
            return System.Configuration.ConfigurationManager.AppSettings[key];
        else
            return string.Empty;
    }

    public static void SetConfigValue(string key, string value)
    {
        Configuration cfa = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        if (cfa.AppSettings.Settings.AllKeys.Contains(key))
        {
            cfa.AppSettings.Settings[key].Value = value;
        }
        else
        {
            cfa.AppSettings.Settings.Add(key, value);
        }
        cfa.Save();
        ConfigurationManager.RefreshSection("appSettings");
    }
}

public class FileWorker
{
    static string txtFilePath = "";
    public static System.Windows.Forms.Control control = null;
    static FileWorker()
    {
        txtFilePath = ConfigWorker.GetConfigValue("logPath");
        if (!System.IO.Directory.Exists(txtFilePath))
        {
            System.IO.Directory.CreateDirectory(txtFilePath);//不存在就创建目录 
        }
    }
    public class LogHelper
    {
        public static readonly log4net.ILog loginfo = log4net.LogManager.GetLogger("loginfo");
        public static void WriteLog(string info)
        {
            if (loginfo.IsInfoEnabled)
            {
                loginfo.Info(info);
            }
        }
    }
}

public class MessageTransfor
{
    private static string airportIata=string.Empty;
    private static string airportName = string.Empty;

    static MessageTransfor()
    {
        airportIata = ConfigWorker.GetConfigValue("airportIata");
        airportName = ConfigWorker.GetConfigValue("airportName");
    }

    public static AlarmEntity getAlarm(_DVSPOcxClientEvents_EventCameraOperatorEvent eventAlarm)
    {
        AlarmEntity alarm = new AlarmEntity();
        alarm.body.airportIata = airportIata;
        alarm.body.airportName = airportName;
        alarm.body.alarmClassCode = "AC03";
        alarm.body.alarmClassName = "手动报警事件";
        alarm.body.alarmEquCode = eventAlarm.ulCameraID.ToString();
        alarm.body.alarmName = "手动报警新事件";
        alarm.body.alarmNameCode = "AC0301";
        alarm.body.alarmStateCode = "AS01";
        alarm.body.alarmStateName = "未解除";
        alarm.body.alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        return alarm;
    }

    public static DeviceStateEntity getDeviceStateChange(_DVSPOcxClientEvents_EventCameraOperatorEvent eventState)
    {
        DeviceStateEntity deviceState = new DeviceStateEntity();
        deviceState.body.createDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        deviceState.body.equCode = eventState.ulCameraID.ToString();
        switch (eventState.ulState)
        {
            case 1:
                deviceState.body.timeStateId = "ES01";
                deviceState.body.timeStateName = "在线";
                break;
            case 2:
                deviceState.body.timeStateId = "ES02";
                deviceState.body.timeStateName = "离线";
                break;
            default:
                break;
        }
        return deviceState;
    }
}
