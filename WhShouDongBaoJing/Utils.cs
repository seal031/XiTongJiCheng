using AxIPModuleLib;
using System;
using System.Collections;
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
    public static AlarmEntity parseAlarm(_ICooMonitorEvents_VistaCIDReportEvent alarmInfo,string airportIata,string airportName)
    {
    //  strMac： 设备的Mac地址；
    //lPlayback：0为实时上报，1为回放；
    //Acct：帐号；
    //IsNewEvent：是否为新事件，0为恢复；
    //CID：ContactID；
    //SubSystemID：子系统编号；
    //IsZone：strCode是否为防区号；1为防区号，0为用户号；
    //strCode，防区号或者用户号；
        AlarmEntity alarmEntity = null;
        try
        {
            alarmEntity = new AlarmEntity();
            alarmEntity.meta.eventType = "SB_ALARM";
            alarmEntity.meta.msgType = "ALARM";
            alarmEntity.meta.receiver = "";
            alarmEntity.meta.recvSequence = "";
            alarmEntity.meta.recvTime = "";
            alarmEntity.meta.sender = "SBALARM";
            alarmEntity.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            alarmEntity.meta.sequence = "";
            alarmEntity.body.alarmClassCode = "AC03";
            alarmEntity.body.alarmClassName = "手动报警事件";
            //alarmEntity.body.alarmTypeCode = "";
            //alarmEntity.body.alarmTypeName = "";
            //alarmEntity.body.alarmName = "";
            alarmEntity.body.alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //alarmEntity.body.alarmLevelCode = "AL01";
            //alarmEntity.body.alarmLevelName = "一级";
            alarmEntity.body.alarmEquCode = alarmInfo.strMac + "," + alarmInfo.strCode;
            alarmEntity.body.alarmName = "手动报警新事件";
            alarmEntity.body.alarmNameCode = "AC0301";
            alarmEntity.body.alarmStateCode = "AS01";
            alarmEntity.body.alarmStateName = "未解除";
            alarmEntity.body.airportIata = airportIata;
            alarmEntity.body.airportName = airportName;
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析报警失败，" + ex.Message);
        }

        return alarmEntity;
    }

    public static Dictionary<string, string> stateDic = new Dictionary<string, string>();
    static AlarmParseTool()
    {
        stateDic.Add("ES01", "在线");
        stateDic.Add("ES02", "离线");
    }
    public static DeviceStateEntity parseDeviceState(_ICooMonitorEvents_VistaKeypadInfoEvent alarmInfo, string stateId)
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
            deviceStateEntity.meta.sender = "SBALARM";
            deviceStateEntity.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            deviceStateEntity.meta.sequence = "";

            deviceStateEntity.body.createDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            deviceStateEntity.body.equCode = alarmInfo.strMac;
            deviceStateEntity.body.timeStateId = stateId;
            deviceStateEntity.body.timeStateName = stateDic[stateId];
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析设备状态失败，" + ex.Message);
            return null;
        }
        return deviceStateEntity;
    }
}