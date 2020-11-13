using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
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

public abstract class BaseParseTool
{
    protected static Regex regexDate = new Regex(@"<Date>\d{1,2}/\d{1,2}/\d\d\d\d</Date>");
    protected static Regex regexTime = new Regex(@"<Time>\d\d:\d\d:\d\d</Time>");
    protected static Regex regexHID = new Regex(@"<HID>\w{0,20}</HID>");
    protected static Regex regexDeviceID = new Regex(@"<DeviceID>\w{0,20}</DeviceID>");
    protected static Regex regexCardNumber = new Regex(@"<CardNumber>\d{0,20}</CardNumber>");
    protected static Regex regexFullName = new Regex(@"<FullName>[\u4e00-\u9fbb]$</FullName>");

    protected static string matchRegex(Regex regex, string message,List<string> replaceList)
    {
        Match match = regex.Match(message);
        if (match.Success)
        {
            string result = match.Value;
            foreach (string s in replaceList)
            {
                result = result.Replace(s, "");
            }
            return result;
        }
        else
        {
            return string.Empty;
        }
    }

    protected static string parseDate(string oldDateStr)
    {
        try
        {
            string[] list = oldDateStr.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
            string month = list[0].PadLeft(2,'0');
            string day = list[1].PadLeft(2, '0');
            string year = list[2];
            return year + "-" + month + "-" + day;
        }
        catch (Exception)
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}

/// <summary>
/// 接收的报警消息解析工具
/// </summary>
public class AlarmParseTool: BaseParseTool
{
    public static AlarmEntity parseAlarm(List<string> alarmMessList,string alarmTime,string alarmEquCode)
    {
        AlarmEntity alarmEntity = null;
        try
        {
            alarmEntity = new AlarmEntity();
            alarmEntity.meta.eventType = "ACS_ALARM";
            alarmEntity.meta.msgType = "ALARM";
            alarmEntity.meta.receiver = "";
            alarmEntity.meta.recvSequence = "";
            alarmEntity.meta.recvTime = "";
            alarmEntity.meta.sender = "DHMJ";
            alarmEntity.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            alarmEntity.meta.sequence = "";

            alarmEntity.body.alarmClassCode = "AC02";
            alarmEntity.body.alarmClassName = "门禁报警事件";
            alarmEntity.body.alarmStateCode = "AS01";
            alarmEntity.body.alarmStateName = "未解除";
            //alarmEntity.body.airportIata = ConfigWorker.GetConfigValue("airportIata");
            //alarmEntity.body.airportName = ConfigWorker.GetConfigValue("airportName");
            alarmEntity.body.alarmTime = alarmTime;
            alarmEntity.body.alarmEquCode = alarmEquCode;
            alarmEntity.body.alarmNameCode = alarmMessList[0];
            alarmEntity.body.alarmName = alarmMessList[1];//报警类型编码对应的名称
            //alarmEntity.body.alarmNameCode = "AC0201";//报警类型编码


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
public class AccessParseTool:BaseParseTool
{
    public static AccessEntity parseAccess(string message, string airportIata,string openType)
    {
        AccessEntity accessEntity = null;
        try
        {
            accessEntity = new AccessEntity();
            accessEntity.meta.eventType = "ACSHN_RECORD_CARD";
            accessEntity.meta.msgType = "RECORD_CARD";
            accessEntity.meta.receiver = "";
            accessEntity.meta.recvSequence = "";
            accessEntity.meta.recvTime = "";
            accessEntity.meta.sender = "MJRECORD";
            accessEntity.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            accessEntity.meta.sequence = "";


            accessEntity.body.cardNumber = matchRegex(regexCardNumber, message, new List<string>() { "<CardNumber>", "</CardNumber>" });
            accessEntity.body.cardStatus = "";
            accessEntity.body.cardType = "";
            accessEntity.body.channelCode = "";
            accessEntity.body.channelName = getRP(message); 
            accessEntity.body.deptName = "";// accessInfo.sUserDepartment;
            accessEntity.body.deviceCode = airportIata + "-" + matchRegex(regexDeviceID, message, new List<string>() { "<DeviceID>", "</DeviceID>" });// accessInfo.sEventDevice;
            accessEntity.body.deviceName = "";// accessInfo.sEventDevice;
            accessEntity.body.enterOrExit = "3";
            accessEntity.body.id = "";
            accessEntity.body.openResult = "1";
            accessEntity.body.openType = openType;
            accessEntity.body.paperNumber = "";
            accessEntity.body.personCode = "";
            accessEntity.body.personId = "";
            accessEntity.body.personName = getFullName(message);
            //刷卡时间
            string date = matchRegex(regexDate, message, new List<string>() { "</Date>", "<Date>" });
            if (date == string.Empty)
            {
                accessEntity.body.swingTime = "";
            }
            else
            {
                string time = matchRegex(regexTime, message, new List<string>() { "</Time>", "<Time>" });
                if (time == string.Empty)
                {
                    accessEntity.body.swingTime = "";
                }
                else
                {
                    var dateReal = parseDate(date);
                    accessEntity.body.swingTime = dateReal + " " + time;
                }
            }
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析刷卡失败，" + ex.Message);
        }
        return accessEntity;
    }
    /// <summary>
    /// 获取刷卡人姓名
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private static string getFullName(string message)
    {
        string name = string.Empty;
        int b_index = message.IndexOf("<FullName>");
        int e_index = message.IndexOf("</FullName>");
        try
        {
            name = message.Substring(b_index + "<FullName>".Length, e_index - b_index - "<FullName>".Length);
        }
        catch (Exception)
        { }
        return name;
    }
    private static string getRP(string message)
    {
        string rp = string.Empty;
        int b_index = message.IndexOf("<RP>");
        int e_index = message.IndexOf("</RP>");
        try
        {
            rp = message.Substring(b_index + "<RP>".Length, e_index - b_index - "<RP>".Length);
        }
        catch (Exception)
        { }
        return rp;
    }
}

/// <summary>
/// 接收的设备状态信息解析工具
/// </summary>
public class DeviceStateParseTool : BaseParseTool
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
