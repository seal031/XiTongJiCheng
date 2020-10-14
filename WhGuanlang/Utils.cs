using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
}

/// <summary>
/// 接收的报警消息解析工具
/// </summary>
public class AlarmParseTool
{
    private static Dictionary<string, string> stateDic = new Dictionary<string, string>();
    private static Regex regexAfNo = new Regex(@"AF-\d\d");
    private static Regex regexFangqu = new Regex(@"VICTRIX-防区_");
    private static Regex regexTime = new Regex("time=\"\\d\\d\\d\\d/\\d\\d/\\d\\d\\d\\d:\\d\\d:\\d\\d");
    static AlarmParseTool()
    {
        stateDic.Add("ES01", "在线");
        stateDic.Add("ES02", "离线");
    }
    public static AlarmEntity parseAlarm(string alarmStr)
    {
        alarmStr = alarmStr.Replace(" ", "");
        AlarmEntity alarmEntity = null;
        try
        {
            string afNo = "", fangquNo = "";
            //if (alarmStr.Contains("mdlname") && alarmStr.Contains(@"</ROOT>"))
            //{
            //    var list = alarmStr.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
            //    if (list.Length == 3)
            //    {
            //        list[1]=list[1].Replace(" ", "");
            //        afNo = list[1].Substring(0, 5).Replace("-","");
            //        list[2] = list[2].Replace(" ","");
            //        fangquNo = list[2].Substring(list[2].IndexOf("VICTRIX-防区_") + "VICTRIX-防区_".Length, 1);
            //    }
            //    else
            //    {
            //        return alarmEntity;
            //    }
            //}
            //else
            //{
            //    return alarmEntity;
            //}
            Match matchAfNo = regexAfNo.Match(alarmStr);
            Match matchFangqu = regexFangqu.Match(alarmStr);
            Match matchTime = regexTime.Match(alarmStr);
            if (matchAfNo.Success && matchFangqu.Success)
            {
                afNo = matchAfNo.Value.Replace("-","");
                fangquNo = alarmStr.Substring(matchFangqu.Index + "VICTRIX-防区_".Length, 1);
            }
            else
            {
                FileWorker.LogHelper.WriteLog("解析报警失败，未找到正则匹配项" + alarmStr.Replace('<','{').Replace('>','}'));
                return alarmEntity;
            }

            string deviceName = "GLBJ-" + afNo + "-" + fangquNo;

            alarmEntity = new AlarmEntity();
            alarmEntity.meta.eventType = "GL_ALARM";
            alarmEntity.meta.msgType = "ALARM";
            alarmEntity.meta.receiver = "";
            alarmEntity.meta.recvSequence = "";
            alarmEntity.meta.recvTime = "";
            alarmEntity.meta.sender = "GLALARM";
            alarmEntity.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            alarmEntity.meta.sequence = "";
            alarmEntity.body.alarmClassCode = "AC12";
            alarmEntity.body.alarmClassName = "管廊报警";
            //alarmEntity.body.alarmTypeCode = "AC0401";
            //alarmEntity.body.alarmTypeName = "管廊报警";
            //alarmEntity.body.alarmName = "管廊报警";
            alarmEntity.body.alarmTime = matchTime.Success ? DateTime.ParseExact(matchTime.Value.Replace("time=\"", ""), "yyyy/MM/ddHH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //alarmEntity.body.alarmLevelCode = "AL01";
            //alarmEntity.body.alarmLevelName = "一级";
            alarmEntity.body.alarmEquCode = deviceName;
            alarmEntity.body.alarmName = "管廊报警新事件";
            alarmEntity.body.alarmNameCode = "AC1201";
            alarmEntity.body.alarmStateCode = "AS01";
            alarmEntity.body.alarmStateName = "未解除";
            alarmEntity.body.airportIata = "WUH";
            alarmEntity.body.airportName = "武汉";
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析报警失败，" + ex.Message);
            return null;
        }

        return alarmEntity;
    }

    public static DeviceStateEntity parseDeviceState(string alarmStr, string stateId)
    {
        DeviceStateEntity deviceStateEntity = null;
        try
        {
            alarmStr = alarmStr.Replace(" ", "");
            Match matchAfNo = regexAfNo.Match(alarmStr);
            Match matchTime = regexTime.Match(alarmStr);
            string equCode = string.Empty;
            if (matchAfNo.Success)
            {
                equCode = "GLBJ-" + matchAfNo.Value.Replace("-","");
            }
            else
            {
                FileWorker.LogHelper.WriteLog("解析设备信息失败，未找到equCode");
                return null;
            }

            deviceStateEntity = new DeviceStateEntity();
            deviceStateEntity.meta.eventType = "ACS_EQUINFO_UE";
            deviceStateEntity.meta.msgType = "EQU";
            deviceStateEntity.meta.receiver = "";
            deviceStateEntity.meta.recvSequence = "";
            deviceStateEntity.meta.recvTime = "";
            deviceStateEntity.meta.sender = "GLRECORD";
            deviceStateEntity.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            deviceStateEntity.meta.sequence = "";

            deviceStateEntity.body.createDate = matchTime.Success ? DateTime.ParseExact(matchTime.Value.Replace("time=\"", ""), "yyyy/MM/ddHH:mm:ss", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss") : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            deviceStateEntity.body.equCode = equCode;
            deviceStateEntity.body.timeStateId = stateId;
            deviceStateEntity.body.timeStateName = stateDic[stateId];
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析设备信息失败，" + ex.Message);
            return null;
        }
        return deviceStateEntity;
    }
}