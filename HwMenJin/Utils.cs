using AxSMSDKPWEventInfo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

public class ControlTextSetter
{
    private delegate void delInfoList(Control control, string text);
    public static void setControlText(Control control, string value)
    {
        value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "  " + value + Environment.NewLine;
        if (control.InvokeRequired)//其它线程调用  
        {
            delInfoList d = new delInfoList(setControlText);
            control.Invoke(d, new object[]{ control, value });
        }
        else//本线程调用  
        {
            control.Text += value;
            control.Refresh();
        }
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
    public static void PrintLog(string text)
    {
        if (control != null)
        {
            ControlTextSetter.setControlText(control, text);
        }
    }
    public static void WriteLog(string text)
    {
        LogHelper.WriteLog(text);
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

/// <summary>
/// 接收的报警消息解析工具
/// </summary>
public class AlarmParseTool
{
    public static AlarmEntity parseAlarm(AxHSCEventSDK alarmInfo, Dictionary<string, string> eventDic)
    {
        AlarmEntity alarmEntity = null;
        try
        {
            //首先确定是哪种报警
            string alarmTypeCode = string.Empty;
            if (eventDic.ContainsKey(alarmInfo.sEventName))
            {
                alarmTypeCode = eventDic[alarmInfo.sEventName];
            }
            else
            {
                FileWorker.LogHelper.WriteLog("收到了未配置的报警类型:"+ alarmInfo.sEventName);
            }

            alarmEntity = new AlarmEntity();
            alarmEntity.meta.eventType = "MJ_ALARM";
            alarmEntity.meta.msgType = "ALARM";
            alarmEntity.meta.receiver = "";
            alarmEntity.meta.recvSequence = "";
            alarmEntity.meta.recvTime = "";
            alarmEntity.meta.sender = "MJALARM";
            alarmEntity.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            alarmEntity.meta.sequence = "";
            alarmEntity.body.alarmClassCode = "AC02";
            alarmEntity.body.alarmClassName = "门禁报警事件";
            //alarmEntity.body.alarmTypeCode = alarmTypeCode;
            //alarmEntity.body.alarmTypeName = alarmInfo.sEventName;
            //alarmEntity.body.alarmName = alarmInfo.sEventDes;
            //alarmEntity.body.alarmTime = DateTime.Parse(alarmInfo.sEventTime).ToString("yyyy-MM-dd HH:mm:ss"); //alarmInfo.sEventTime.Replace("/","-");
            //alarmEntity.body.alarmLevelCode = "AL01";
            //alarmEntity.body.alarmLevelName = "一级";
            alarmEntity.body.alarmEquCode = alarmInfo.sEventLocation;
            alarmEntity.body.alarmName = alarmInfo.sEventName;
            alarmEntity.body.alarmNameCode = alarmTypeCode;
            alarmEntity.body.alarmStateCode = "AS01";
            alarmEntity.body.alarmStateName = "未解除";
            alarmEntity.body.airportIata = "WUH";
            alarmEntity.body.airportName = "武汉";
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析报警失败，" + ex.Message);
        }

        return alarmEntity;
    }
}