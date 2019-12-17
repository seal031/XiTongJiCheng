using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoFangBaoJing
{
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

    public class MathTransfer
    {
        public static char[] Ten2Tow(int ten)
        {
            char[] s= Convert.ToString(ten, 2).ToCharArray();
            char[] r = new char[16];
            for (int i = s.Length - 1; i >= 0; i--)
            {
                r[16 - (s.Length - i)] = s[i];
                //r[i] = s[i];
            }
            return r.Reverse().ToArray() ;
        }
    }

    public class FileWorker
    {
        public static List<string> readTxt(string filePath)
        {
            List<string> list = new List<string>();
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath, Encoding.UTF8);
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line); 
                }
                sr.Close();
            }
            return list;
        }
    }
}
