using System;
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
    public static AlarmEntity parseAlarm(string alarmStr)
    {
        AlarmEntity alarmEntity = null;
        try
        {
            int timeBeginIndex = alarmStr.IndexOf("time=");
            int timeEndIndex = alarmStr.IndexOf("\" mdlid=");
            string alarmTimeStr = "";
            if (timeBeginIndex >= 0 && timeEndIndex >= 0 && timeEndIndex > (timeBeginIndex + 6))
            {
                string timePartStr = alarmStr.Substring(timeBeginIndex + 6, (timeEndIndex - timeBeginIndex - 6));
                DateTime alarmTime = DateTime.Parse(timePartStr);
                alarmTimeStr = alarmTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            int mdlnameBeginIndex = alarmStr.IndexOf("mdlname=");
            int mdlnameEndIndex = alarmStr.IndexOf("\"/></ROOT>");
            string deviceName = "";
            if (mdlnameBeginIndex >= 0 && mdlnameEndIndex >= 0 && mdlnameEndIndex > (mdlnameBeginIndex + 8))
            {
                //string mdlnamePartStr = alarmStr.Substring(mdlnameBeginIndex + 8, (mdlnameEndIndex - mdlnameBeginIndex - 8));
                //string[] mdlnameArray = mdlnamePartStr.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                //if (mdlnameArray.Length != 4)
                //{
                //    FileWorker.LogHelper.WriteLog("mdlname解析错误:"+mdlnamePartStr);
                //    return alarmEntity;
                //}
                //else
                //{
                //    string afNo = mdlnameArray[1].Replace(" ", "").Trim();
                //    string fangquNo = mdlnameArray[3].Replace(" ","").Replace("VICTRIX-防区_", "").Trim();
                //    deviceName = "GLBJ-" + afNo + "-" + fangquNo;
                //}
                string mdlnamePartStr = alarmStr.Substring(mdlnameBeginIndex + 8, (mdlnameEndIndex - mdlnameBeginIndex - 8));
                string[] mdlnameArray = mdlnamePartStr.Split(new string[] { @"\" }, StringSplitOptions.RemoveEmptyEntries);
                if (mdlnameArray.Length != 3)
                {
                    FileWorker.LogHelper.WriteLog("mdlname解析错误:" + mdlnamePartStr);
                    return alarmEntity;
                }
                else
                {
                    string afNo = mdlnameArray[1].Split(new string[] { @"_" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace("-", "").Trim();
                    string fangquNo = mdlnameArray[2].Replace(" ", "").Replace("VICTRIX-防区_", "").Trim();
                    deviceName = "GLBJ-" + afNo + "-" + fangquNo;
                }
            }
            alarmEntity = new AlarmEntity();
            alarmEntity.meta.eventType = "GL_ALARM";
            alarmEntity.meta.msgType = "ALARM";
            alarmEntity.meta.receiver = "";
            alarmEntity.meta.recvSequence = "";
            alarmEntity.meta.recvTime = "";
            alarmEntity.meta.sender = "GLALARM";
            alarmEntity.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            alarmEntity.meta.sequence = "";
            alarmEntity.body.alarmClassCode = "AC04";
            alarmEntity.body.alarmClassName = "管廊报警";
            alarmEntity.body.alarmTypeCode = "AC0401";
            alarmEntity.body.alarmTypeName = "管廊报警";
            alarmEntity.body.alarmName = "管廊报警";
            alarmEntity.body.alarmTime = (alarmTimeStr==string.Empty?DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") : alarmTimeStr);
            alarmEntity.body.alarmLevelCode = "AL01";
            alarmEntity.body.alarmLevelName = "一级";
            alarmEntity.body.alarmEquCode = deviceName;
        }
        catch (Exception ex)
        {
            FileWorker.LogHelper.WriteLog("解析报警失败，" + ex.Message);
        }

        return alarmEntity;
    }
}