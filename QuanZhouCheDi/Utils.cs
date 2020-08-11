using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanZhouCheDi
{
    public static class Utils
    {
        public static MessCommand ParseCarScanMess(string[] inforColl)
        {
            MessCommand messCommand = null;
            try
            {
                //FileWorker.LogHelper.WriteLog("解析汽车记录失败，" + ex.Message);
                //messCommand.
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("解析汽车记录失败，" + ex.Message);
                throw;
            }
            return messCommand;
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
        //public static void PrintLog(string text)
        //{
        //    if (control != null)
        //    {
        //        ControlTextSetter.setControlText(control, text);
        //    }
        //}
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
}
