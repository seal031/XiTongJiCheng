using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
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
                #region meta
                messCommand.meta.eventType = "CROSSING_CAR_PASS_INFO";
                messCommand.meta.msgType = "CROSSING_DATA";
                messCommand.meta.receiver = "";
                messCommand.meta.recvSequence = "";
                messCommand.meta.recvTime = "";
                messCommand.meta.sender = "CROSSING";
                messCommand.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                messCommand.meta.sequence = "";
                #endregion
                #region body
                #region
                messCommand.body.cardNo = "";
                messCommand.body.carTypeCode = "";
                messCommand.body.carType = "";
                messCommand.body.capPlace = "";
                messCommand.body.imgName = "";
                messCommand.body.capRemark = "";
                messCommand.body.vechicleShape = "";
                messCommand.body.vechicleShapeName = "";
                messCommand.body.vechicleColour = "";
                messCommand.body.vechicleColourName = "";
                messCommand.body.vechicleInUvssImg = "";
                messCommand.body.vechicleInAnprImg = "";
                messCommand.body.vechicleInTopImg = "";
                messCommand.body.vechicleInTopPicpath = "";
                messCommand.body.vechicleOutTime = "";
                messCommand.body.vechicleInState = "";
                messCommand.body.vechicleInStateName = "";
                messCommand.body.driverName = "";
                messCommand.body.driverCard = "";
                messCommand.body.driverLicense = "";
                messCommand.body.vechicleDept = "";
                messCommand.body.createDate = "";
                messCommand.body.updateDate = "";
                messCommand.body.airportIata = "";
                messCommand.body.airportIame = "";
                #endregion

                messCommand.body.capTime = inforColl[1];
                messCommand.body.parkCode = inforColl[3];
                //messCommand.body.vechicleInUvssPicpath = inforColl[4];
                string test = "https://img-blog.csdnimg.cn/2020091110132619.jpg";
                //test = "https://img-blog.csdnimg.cn/20191016215757571.png";
                //inforColl[4] = test;
                //messCommand.body.vechicleInUvssPicpath = BaseHelper.ImgToBase64String(inforColl[4]); 
                //string base64Str = BaseHelper.ImgToBase64Test();
                if (inforColl[4].Contains("http"))
                {
                    messCommand.body.vechicleInUvssPicpath = BaseHelper.WebImageToBase64(inforColl[4]);
                }
                else
                {
                    messCommand.body.vechicleInUvssPicpath = BaseHelper.ImgToBase64String(inforColl[4]);
                }
                //messCommand.body.vechicleInUvssPicpath = BaseHelper.ImgToBase64String(test);
                messCommand.body.vechicleInAnprPicpath = inforColl[5];
                messCommand.body.plateNo = inforColl[6];
                if (inforColl[7] == "0")
                {
                    messCommand.body.capFlag = "1";
                }
                else
                {
                    messCommand.body.capFlag = "0";
                }
                messCommand.body.driverJobNo = inforColl[11];
                #endregion
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

        public static string UrlEncode(string str)
        {
            StringBuilder stringBuild = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                stringBuild.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (stringBuild.ToString());
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
    public static class BaseHelper
    {
        public static string WebImageToBase64(string urlAddress)
        {
            try
            {
                Uri url = new Uri(urlAddress);
                System.Net.WebRequest webRequest = System.Net.WebRequest.Create(url);
                System.Net.WebResponse webResponse = webRequest.GetResponse();
                Bitmap myImage = new Bitmap(webResponse.GetResponseStream());
                MemoryStream ms = new MemoryStream();
                myImage.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch
            {
                return string.Empty;
            }
        }
        //图片 转为    base64编码的文本
        public static string ImgToBase64String(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);
                MemoryStream ms = new MemoryStream();

                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] arr = new byte[ms.Length];
                //byte[] arr = ms.GetBuffer();
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                String strbaser64 = Convert.ToBase64String(arr);
                return strbaser64;
                // MessageBox.Show("转换成功!");

            }
            catch (Exception ex)
            {
                //MessageBox.Show("ImgToBase64String 转换失败\nException:" + ex.Message);
                return "";
            }
        }
        public static Image Base64StringToImage(string code)
        {
            Image ima = null;
            try
            {
                byte[] arr = Convert.FromBase64String(code);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);

                ms.Close();
                ima = bmp;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Base64StringToImage 转换失败\nException：" + ex.Message);
            }
            return ima;
        }

        public static string ImgToBase64Test()
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Title = "选择要转换的图片";
            String strbaser64 = "";
            dlg.Filter = "Image files (*.jpg;*.bmp;*.gif;*.png)|*.jpg*.jpeg;*.gif;*.bmp|AllFiles (*.*)|*.*";
            if (System.Windows.Forms.DialogResult.OK == dlg.ShowDialog())
            {
                for (int i = 0; i < dlg.FileNames.Length; i++)
                {
                    string Imagefilename = dlg.FileNames[i].ToString();
                    Bitmap bmp = new Bitmap(Imagefilename);
                    //this.pictureBox1.Image = bmp;
                    MemoryStream ms = new MemoryStream();
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] arr = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(arr, 0, (int)ms.Length);
                    ms.Close();
                    strbaser64 = Convert.ToBase64String(arr);

                }
            }
            return strbaser64;
        }
    }
}
