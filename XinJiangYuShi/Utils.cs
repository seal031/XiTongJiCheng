﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WhGuanlang;

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
    public static AlarmEntity parseAlarm()
    {
        AlarmEntity alarmEntity = null;
        try
        {
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
            alarmEntity.body.alarmTypeCode = "";
            alarmEntity.body.alarmTypeName = "";
            alarmEntity.body.alarmName = "";
            alarmEntity.body.alarmTime = "";
            alarmEntity.body.alarmLevelCode = "AL01";
            alarmEntity.body.alarmLevelName = "一级";
            alarmEntity.body.alarmEquCode = "";
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析报警失败，" + ex.Message);
        }

        return alarmEntity;
    }
}