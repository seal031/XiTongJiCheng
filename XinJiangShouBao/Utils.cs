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

/// <summary>
/// 字符串和byte[]转换
/// </summary>
public class MessageParser
{
    public static string byteToStr(byte[] bytes)
    {
        return System.Text.ASCIIEncoding.GetEncoding("GB2312").GetString(bytes);
    }

    public static byte[] strToByte(string str)
    {
        return System.Text.Encoding.Default.GetBytes(str);
    }
}

public class NumberTrans
{

    public static int hexToDec(string hex)
    {
        return Convert.ToInt32(hex, 16);
    }

    public static int decToHex(int dec)
    {
        string hexStr = Convert.ToString(dec, 16);
        int hex;
        if (int.TryParse("0x"+hexStr,out hex) == false)
        {
            throw new Exception("无法转换" + hexStr + "为Int");
        }
        return hex;
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

public class AlarmParseTool
{
    public static AlarmEntity parseAlarm(string tcpMsg,string eventCode,string airportIata, string airportName)
    {
        AlarmEntity alarm = new AlarmEntity();

        switch (eventCode)
        {
            case "A202":
                alarm.body.alarmName = "手报报警事件";
                alarm.body.alarmNameCode = "AC0301";
                alarm.body.alarmStateCode = "AS01";
                alarm.body.alarmStateName = "未解除";
                break;
            case "A203":
                alarm.body.alarmName = "手报报警事件";
                alarm.body.alarmNameCode = "AC0301";
                alarm.body.alarmStateCode = "AS02";
                alarm.body.alarmStateName = "已解除";
                break;

            case "A206":
                alarm.body.alarmName = "手报故障事件";
                alarm.body.alarmNameCode = "AC0302";
                alarm.body.alarmStateCode = "AS01";
                alarm.body.alarmStateName = "未解除";
                break;

            case "A207":
                alarm.body.alarmName = "手报故障事件";
                alarm.body.alarmNameCode = "AC0302";
                alarm.body.alarmStateCode = "AS02";
                alarm.body.alarmStateName = "已解除";
                break;

            case "A208":
                alarm.body.alarmName = "手报防拆事件";
                alarm.body.alarmNameCode = " ";
                alarm.body.alarmStateCode = "AS01";
                alarm.body.alarmStateName = "未解除";
                break;
            case "A209":
                alarm.body.alarmName = "手报防拆事件";
                alarm.body.alarmNameCode = "AC0303";
                alarm.body.alarmStateCode = "AS02";
                alarm.body.alarmStateName = "已解除";
                break;
            default:
                return null;
        }
        alarm.body.airportIata = airportIata;
        alarm.body.airportName = airportName;
        foreach (string item in tcpMsg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
        {
            var kvpair = item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            if (kvpair.Length >= 2)
            {
                switch (kvpair[0].Trim().Replace("\"",""))
                {
                    case "EventDateTime":
                        if (kvpair.Length >= 4)
                        {
                            alarm.body.alarmTime = kvpair[1].Trim().Replace("\"", "") + ":" + kvpair[2].Trim().Replace("\"", "") + ":" + kvpair[3].Trim().Replace("\"", "");
                        }
                        else
                        {
                            alarm.body.alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        break;
                    case "AlertZoneNumber":
                        alarm.body.alarmEquCode = airportIata + "-" + kvpair[1].Trim().Replace("\"", "");
                        break;
                    case "DeviceName":
                        //alarm.body.alarmEquName = kvpair[1].Trim();
                        //alarm.body.alarmName = kvpair[1].Trim();
                        break;
                    case "AlertZoneName":
                        //alarm.body.alarmAddress = kvpair[1].Trim();
                        break;
                    case "EventCode":
                        if (kvpair[1].Trim().Replace("\"", "") != eventCode)
                        { return null; }
                        break;
                    case "EventName":
                        //alarm.body.alarmDescibe = kvpair[1].Trim();
                        break;
                    default:
                        break;
                }
            }
        }
        if (alarm.body.alarmTime == null || alarm.body.alarmTime == string.Empty)
        {
            alarm.body.alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        return alarm;
    }

    public static DeviceStateEntity parseDeviceState(string tcpMsg,string eventCode, string airportIata)
    {
        DeviceStateEntity deviceStateEntity = null;
        try
        {
            deviceStateEntity = new DeviceStateEntity();

            foreach (string item in tcpMsg.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                var kvpair = item.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                if (kvpair.Length >= 2)
                {
                    switch (kvpair[0].Trim().Replace("\"", ""))
                    {
                        case "EventDateTime":
                            if (kvpair.Length >= 4)
                            {
                                deviceStateEntity.body.operateTime = kvpair[1].Trim().Replace("\"", "") + ":" + kvpair[2].Trim().Replace("\"", "") + ":" + kvpair[3].Trim().Replace("\"", "");
                            }
                            else
                            {
                                deviceStateEntity.body.operateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            }
                            break;
                        case "AlertZoneNumber":
                            deviceStateEntity.body.equNameCode = airportIata + "-" + kvpair[1].Trim().Replace("\"", "");
                            break;
                        case "DeviceName":
                            //alarm.body.alarmEquName = kvpair[1].Trim();
                            //alarm.body.alarmName = kvpair[1].Trim();
                            break;
                        case "AlertZoneName":
                            //alarm.body.alarmAddress = kvpair[1].Trim();
                            break;
                        case "EventCode":
                            if (kvpair[1].Trim().Replace("\"", "") != eventCode)
                            { return null; }
                            break;
                        case "EventName":
                            //alarm.body.alarmDescibe = kvpair[1].Trim();
                            break;
                        default:
                            break;
                    }
                }
            }
            switch (eventCode)
            {
                case "A206":
                    deviceStateEntity.body.timeStateId = "ES03";
                    deviceStateEntity.body.timeStateName = "故障";
                    break;
                case "A207":
                    deviceStateEntity.body.timeStateId = "ES01";
                    deviceStateEntity.body.timeStateName = "在线";
                    break;
                default:
                    return null;
            }
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析设备状态失败，" + ex.Message);
            return null;
        }
        if (deviceStateEntity.body.operateTime == null || deviceStateEntity.body.operateTime == string.Empty)
        {
            deviceStateEntity.body.operateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        return deviceStateEntity;
    }
}