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
                messCommand = new MessCommand();
                #region
                messCommand.cardNo = "";
                messCommand.carTypeCode = "";
                messCommand.carType = "";
                messCommand.capPlace = "";
                messCommand.imgName = "";
                messCommand.capRemark = "";
                messCommand.vechicleShape = "";
                messCommand.vechicleShapeName = "";
                messCommand.vechicleColour = "";
                messCommand.vechicleColourName = "";
                messCommand.vechicleInUvssImg = "";
                messCommand.vechicleInAnprImg = "";
                messCommand.vechicleInTopImg = "";
                messCommand.vechicleInTopPicpath = "";
                messCommand.vechicleOutTime = "";
                messCommand.vechicleInState = "";
                messCommand.vechicleInStateName = "";
                messCommand.driverName = "";
                messCommand.driverCard = "";
                messCommand.driverLicense = "";
                messCommand.vechicleDept = "";
                messCommand.createDate = "";
                messCommand.updateDate = "";
                messCommand.airportIata = "";
                messCommand.airportIame = "";
                #endregion

                messCommand.capTime = inforColl[1];
                messCommand.parkCode = inforColl[3];
                messCommand.vechicleInUvssPicpath = inforColl[4];
                messCommand.vechicleInAnprPicpath = inforColl[5];
                messCommand.plateNo = inforColl[6];
                if (inforColl[7] == "0")
                {
                    messCommand.capFlag = "1";
                }
                else
                {
                    messCommand.capFlag = "0";
                }
                messCommand.driverJobNo = inforColl[11];
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("解析D00记录失败," + ex.Message);
            }
            return messCommand;
        }
        public static WorkingState ParseWorkMess(string[] inforColl)
        {
            WorkingState state = null;
            try
            {
                state = new WorkingState();
                state.carVidiconState = inforColl[1];
                state.carVidiconTemp = float.Parse(inforColl[2]);
                state.conConnStateVSIO = inforColl[3];
                state.roadNumber = inforColl[4];
                state.inputState = inforColl[5];
            }
            catch (Exception e)
            {
                FileWorker.LogHelper.WriteLog("解析D01记录失败," + e.Message);
            }
            return state;
        }

        public static MessageOrder ParseMessOrder(string[] messColl)
        {
            MessageOrder order = null;
            try
            {
                order = new MessageOrder();
                order.maxJoinNumber = int.Parse(messColl[2]);
            }
            catch (Exception e) 
            {
                FileWorker.LogHelper.WriteLog("解析D02记录失败," + e.Message);
            }
            return order;
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
