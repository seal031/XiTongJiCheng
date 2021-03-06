﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
    public static AlarmEntity parseAlarm(string alarmTime,string equCode,List<string> alarmList)
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
            alarmEntity.body.alarmStateCode = "AS01";
            alarmEntity.body.alarmStateName = "未解除";
            alarmEntity.body.alarmEquCode = equCode;
            alarmEntity.body.alarmNameCode = alarmList[0];
            alarmEntity.body.alarmName = alarmList[1];
            alarmEntity.body.alarmTime = alarmTime;
            //alarmEntity.body.airportIata = airportIata;
            //alarmEntity.body.airportName = airportName;
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
    public static AccessEntity parseAccess(string cardId,string time,string deviceCode)
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

            
            accessEntity.body.channelCode = deviceCode;
            accessEntity.body.deviceCode = deviceCode;
            accessEntity.body.deviceName = deviceCode;
            accessEntity.body.openResult = "1";
            accessEntity.body.swingTime = time;



            accessEntity.body.cardNumber = cardId;
            if (cardId != "")
            {
                Dictionary<string, string> seleDict = SelePersonTable(cardId);
                accessEntity.body.personCode = seleDict["人员编号"];
                accessEntity.body.personName = seleDict["人员名称"];
                accessEntity.body.deptName = seleDict["部分名称"];
            }
            else
            {
                accessEntity.body.personCode = "";
                accessEntity.body.personName = "";
                accessEntity.body.deptName = "";
            }


            accessEntity.body.id = "";
            accessEntity.body.cardTypeName = "";
            accessEntity.body.enterOrExit = "";
            accessEntity.body.cardType = "";
            accessEntity.body.cardStatus = "";
            accessEntity.body.channelName = "";
            accessEntity.body.openType = "";
            accessEntity.body.openTypeName = "";
            accessEntity.body.paperNumber = "";
            accessEntity.body.personId = "";
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析刷卡失败，" + ex.Message);
        }
        return accessEntity;
    }
    private static Dictionary<string, string> SelePersonTable(string cardId)
    {
        string strConn = ConfigWorker.GetConfigValue("connectString");
        Dictionary<string, string> sqlResult = new Dictionary<string, string>();
        string sql = string.Format("select C.CardID,U.UserID,U.UserName,D.DepartmentName from Card as C,UserList as U,Department as D where C.UserID = U.UserID and D.DepartmentID = U.DepartmentID and C.CardID = '{0}'",cardId);
        using (SqlDataAdapter da = new SqlDataAdapter(sql, strConn))
        {
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].Rows[0];
            sqlResult.Add("卡号", row.ItemArray[0].ToString());
            sqlResult.Add("人员编号", row.ItemArray[1].ToString());
            sqlResult.Add("人员名称", row.ItemArray[2].ToString());
            sqlResult.Add("部分名称", row.ItemArray[3].ToString());
        }
        return sqlResult;
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

    public static DeviceStateEntity parseDeviceState(string alarmTime, string equCode, string state)
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
            deviceStateEntity.body.equCode =equCode;
            deviceStateEntity.body.operateTime = alarmTime;
            if (state == "28.门开时间过长")
            {
                deviceStateEntity.body.timeStateId = "ES02";
                deviceStateEntity.body.timeStateName = "开门";
            }
            else if (state == "26.门重新进入安全状态")
            {
                deviceStateEntity.body.timeStateId = "ES01";
                deviceStateEntity.body.timeStateName = "关门";
            }
            else
            {
                deviceStateEntity.body.timeStateId = "ES03";
                deviceStateEntity.body.timeStateName = "故障";
            }
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析设备状态失败，" + ex.Message);
        }
        return deviceStateEntity;
    } }

public class DataFormatTool
{
    public static string formatDatetime(string inputStr)
    {
        string result=string.Empty;
        try
        {
            DateTime datetime = DateTime.ParseExact(inputStr, "yyyy/M/d H:mm:ss", CultureInfo.InvariantCulture);
            result = datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog(inputStr + "日期时间转换失败，" + ex.Message);
            result = string.Empty;
        }
        return result;
    }

    public static string pickTailFromString(string wholeString)
    {
        string tail = string.Empty;
        tail = wholeString.Split(new string[] {"/" },StringSplitOptions.RemoveEmptyEntries).Last();
        return tail;
    }
}