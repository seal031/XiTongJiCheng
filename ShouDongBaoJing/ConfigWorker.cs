using System;
using System.Configuration;
using System.IO;

namespace IPMALARM
{
    internal class ConfigWorker
    {
        internal static string GetConfigValue(string key)
        {
            bool flag = ConfigurationManager.AppSettings[key] != null;
            string result;
            if (flag)
            {
                result = ConfigurationManager.AppSettings[key];
            }
            else
            {
                result = string.Empty;
            }
            return result;
        }
        internal static void SetConfigValue(string key, string value)
        {
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[key].Value = value;
            configuration.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
    }

    internal class FileWorker
    {
        internal static string logFilePath = ConfigWorker.GetConfigValue("LogPath");
        internal static void WriteLog(string log)
        {
            bool flag = !File.Exists(FileWorker.logFilePath);
            if (flag)
            {
                FileStream fileStream = File.Create(FileWorker.logFilePath);
                fileStream.Close();
                fileStream.Dispose();
            }
            using (StreamWriter streamWriter = new StreamWriter(FileWorker.logFilePath, true))
            {
                streamWriter.WriteLine(DateTime.Now.ToString() + " " + log + "\r\n");
            }
        }
    }
}
