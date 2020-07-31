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
/// 字符串和byte[]转换
/// </summary>
public class MessageParser
{
    public static string byteToStr(byte[] bytes)
    {
        return System.Text.Encoding.Default.GetString(bytes);
    }

    public static byte[] strToByte(string str)
    {
        return System.Text.Encoding.Default.GetBytes(str);
    }
    /// <summary>
    /// byte转16进制
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string byteToHex(byte[] bytes)
    {
        StringBuilder sb = new StringBuilder(bytes.Length * 3);
        for (int i = 0; i < bytes.Length; i++)
        {
            sb.Append(Convert.ToString(bytes[i], 16).PadLeft(2, '0'));
        }
        var sbStr = sb.ToString();
        return sbStr;
    }

    public static string HexToDec(string hex)
    {
        try
        {
            return Int32.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier).ToString("D2");//不足2位左侧补零
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("十六进制的值" + hex + "转换为十进制失败：" + ex.Message);
            return "";
        } 
    }
}


/// <summary>
/// 接收的报警消息解析工具
/// </summary>
public class AlarmParseTool
{
    public static Dictionary<string, string> stateDic = new Dictionary<string, string>();
    public static Dictionary<string, string> alarmDic = new Dictionary<string, string>();

    static AlarmParseTool()
    {
        stateDic.Add("ES01", "在线");
        stateDic.Add("ES02", "离线");
        stateDic.Add("AS01", "未解除");
        stateDic.Add("AS02", "已解除");

        alarmDic.Add("AC0601", "围界入侵报警");
        alarmDic.Add("AC0602", "围界防拆报警");
        alarmDic.Add("AC0603", "围界故障报警");
        alarmDic.Add("AC0604", "围界离线报警");
    }
    public static AlarmEntity parseAlarm(string equCode,string alarmCode,string stateCode)
    {
        AlarmEntity alarmEntity = new AlarmEntity();
        alarmEntity.meta.eventType = "WJ_ALARM";
        alarmEntity.meta.msgType = "ALARM";
        alarmEntity.meta.receiver = "";
        alarmEntity.meta.recvSequence = "";
        alarmEntity.meta.recvTime = "";
        alarmEntity.meta.sender = "WJALARM";
        alarmEntity.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        alarmEntity.meta.sequence = "";
        alarmEntity.body.alarmClassCode = "AC06";
        alarmEntity.body.alarmClassName = "围界报警";
        alarmEntity.body.alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        alarmEntity.body.alarmEquCode = equCode;
        alarmEntity.body.alarmName = alarmDic[alarmCode];
        alarmEntity.body.alarmNameCode = alarmCode;
        alarmEntity.body.alarmStateCode = stateCode;
        alarmEntity.body.alarmStateName = stateDic[stateCode];
        alarmEntity.body.airportIata = "WUH";
        alarmEntity.body.airportName = "武汉";
        return alarmEntity;
    }
    public static DeviceStateEntity parseDeviceState(string equCode,string stateId)
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
            deviceStateEntity.meta.sender = "WJALARM";
            deviceStateEntity.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            deviceStateEntity.meta.sequence = "";

            deviceStateEntity.body.createDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            deviceStateEntity.body.equCode = equCode;
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