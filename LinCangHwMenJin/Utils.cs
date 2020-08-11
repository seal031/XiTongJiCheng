﻿using System;
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

public class BaseParseTool
{
    protected static string airportIata { get; set; }
    protected static string airportName { get; set; }
    static BaseParseTool()
    {
        airportIata = ConfigWorker.GetConfigValue("airportIata");
        airportName = ConfigWorker.GetConfigValue("airportName");
    }
}

/// <summary>
/// 接收的报警消息解析工具
/// </summary>
public class AlarmParseTool : BaseParseTool
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
            //alarmEntity.body.alarmTime = DateTime.Parse(alarmInfo.sEventTime).ToString("yyyy-MM-dd HH:mm:ss"); 
            //alarmEntity.body.alarmEquCode = alarmInfo.sEventLocation;
            //alarmEntity.body.alarmName = alarmInfo.sEventName;
            //alarmEntity.body.alarmNameCode = alarmTypeCode;
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

/// <summary>
/// 接收的正常刷卡信息解析工具
/// </summary>
public class AccessParseTool : BaseParseTool
{
    public static AccessEntity parseAccess()
    {
        AccessEntity accessEntity = null;
        try
        {
            accessEntity = new AccessEntity();
            accessEntity.meta.eventType = "ACS_RECORD_CARD";
            accessEntity.meta.msgType = "RECORD_CARD";
            accessEntity.meta.receiver = "";
            accessEntity.meta.recvSequence = "";
            accessEntity.meta.recvTime = "";
            accessEntity.meta.sender = "MJRECORD";
            accessEntity.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            accessEntity.meta.sequence = "";

            //accessEntity.body.cardNumber = accessInfo.sUserCardID;
            //accessEntity.body.cardStatus = "";
            //accessEntity.body.cardType = "";
            //accessEntity.body.channelCode = "";
            //accessEntity.body.channelName = accessInfo.sPanelName;
            //accessEntity.body.deptName = accessInfo.sUserDepartment;
            //accessEntity.body.deviceCode = accessInfo.sEventDevice;
            //accessEntity.body.deviceName = accessInfo.sEventDevice;
            //accessEntity.body.enterOrExit = "3";
            //accessEntity.body.id = "";
            //accessEntity.body.openResult = "1";
            //accessEntity.body.openType = "";
            //accessEntity.body.paperNumber = "";
            //accessEntity.body.personCode = "";
            //accessEntity.body.personId = accessInfo.sUserID;
            //accessEntity.body.personName = accessInfo.sUserName;
            //accessEntity.body.swingTime = DateTime.Parse(accessInfo.sEventTime).ToString("yyyy-MM-dd HH:mm:ss");
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析刷卡失败，" + ex.Message);
        }
        return accessEntity;
    }
}

/// <summary>
/// 接收的设备状态信息解析工具
/// </summary>
public class DeviceStateParseTool
{
    public static Dictionary<string, string> stateDic = new Dictionary<string, string>();
    static DeviceStateParseTool()
    {
        stateDic.Add("ES01", "在线");
        stateDic.Add("ES02", "离线");
    }

    public static DeviceStateEntity parseDeviceState(string stateId)
    {
        DeviceStateEntity deviceStateEntity = null;
        try
        {
            deviceStateEntity = new DeviceStateEntity();
            deviceStateEntity.meta.eventType = "ACS_EQUINFO_UE";
            deviceStateEntity.meta.msgType = "EQU";
            deviceStateEntity.meta.receiver = "";
            deviceStateEntity.meta.recvSequence = "";
            deviceStateEntity.meta.recvTime = "";
            deviceStateEntity.meta.sender = "MJEQU";
            deviceStateEntity.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            deviceStateEntity.meta.sequence = "";

            //deviceStateEntity.body.createDate =deviceStateInfo.sEventTime;
            //deviceStateEntity.body.equCode =deviceStateInfo.sEventLocation;
            deviceStateEntity.body.timeStateId = stateId;
            deviceStateEntity.body.timeStateName = stateDic[stateId];
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析设备状态失败，" + ex.Message);
        }
        return deviceStateEntity;
    } }
