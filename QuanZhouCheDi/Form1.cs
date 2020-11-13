using FluentFTP;
using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanZhouCheDi
{
    public partial class Form1 : Form,IJobWorke
    {
        const int WM_COPYDATA = 0x004A;
        [DllImport("User32.dll")]
        public static extern int SendMessage(int hwnd, int msg, int wParam, ref COPYDATASTRUCT IParam);
        [DllImport("User32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        IScheduler scheduler;
        IJobDetail job;
        JobKey jobKey;
        public Form1()
        {
            InitializeComponent();
            //string path = "D:\\QQ和微信的聊天记录\\微信\\WeChat Files\\wxid_avcxcz39c3f722\\FileStorage\\File\\2020-11\\道口过车车底.txt";
            //string str = System.IO.File.ReadAllText(path, Encoding.Default);
            //int len = str.Length;
            //KafkaWorker.sendCarRecordMessage(str);
            FtpHelper.processChangeEvent += FtpHelper_processChangeEvent;
            initJob();
        }

        private void FtpHelper_processChangeEvent(FtpProgress process)
        {
        }

        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                    Type mytype = mystr.GetType();
                    mystr = (COPYDATASTRUCT)m.GetLParam(mytype);
                    string[] messColl = mystr.lpData.Split(new char[] { '=' });
                    if (messColl[0] == "D00")
                    {
                        //MessageBox.Show(mystr.lpData);
                        FileWorker.LogHelper.WriteLog("接受车底信息记录D00:" + mystr.lpData);
                        MessCommand messComm = Utils.ParseCarScanMess(messColl);

                        string jsonMess = messComm.toJson();
                        KafkaWorker.sendCarRecordMessage(jsonMess);
                        Task.Run(() => {
                            //在上传图片之前先进行压缩,保存到本地上,然后拿到压缩后的路径,进行FTP的上传
                            //string path = string.Format("/{0}/{1}", DateTime.Now.ToString("yyyy.MM.dd"), Path.GetFileName(messComm.body.vechicleInUvssPicpath));
                            FtpStatus ftpStatus = FtpHelper.upload(messColl[4], messComm.body.vechicleInUvssPicpath);
                            if (ftpStatus == FtpStatus.Success)
                            {
                                FileWorker.LogHelper.WriteLog("上传文件" + messColl[4] + "成功");
                            }
                            else if (ftpStatus == FtpStatus.Skipped)
                            {
                                FileWorker.LogHelper.WriteLog("上传文件" + messColl[4] + "在服务端已存在，跳过传输");
                            }
                            else
                            {
                                FileWorker.LogHelper.WriteLog("上传文件" + messColl[4] + "失败");
                                string txtPath = Application.StartupPath + "\\imageErrorMess.txt";
                                string txt = string.Format("{0}|{1}", messColl[4],messComm.body.vechicleInUvssPicpath);
                                WriteLogFile(txtPath, txt);
                            }
                            //string txtPath = Application.StartupPath + "\\imageErrorMess.txt";
                            //string txt = string.Format("{0}|{1}", messComm.body.vechicleInUvssPicpath, path);
                            //WriteLogFile(txtPath, txt);
                        });
                    }
                    else if (messColl[0] == "D01")
                    {
                        //FileWorker.LogHelper.WriteLog("基本工作状态D01:" + mystr.lpData);
                        //WorkingState work = Utils.ParseWorkMess(messColl);
                        //string jsonMess = work.toJson();
                        //KafkaTest.SendMessCommand(jsonMess);
                    }
                    else if (messColl[0] == "D02")
                    {
                        FileWorker.LogHelper.WriteLog("第3方自定义功能D02:" + mystr.lpData);
                        if (messColl[1] == "0")
                        {
                            this.Close();
                        }
                        else if (messColl[1] == "1")
                        {
                            this.TopMost = true;
                        }
                        else if (messColl[1] == "2")//D02 = 2 = N = EOF
                        {
                            //N 最大接入许可数量
                            //MessageOrder work = Utils.ParseMessOrder(messColl);
                            //上行,发送"U02=2=EOF"
                            SendResponOrder();
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        FileWorker.LogHelper.WriteLog("未知命令:" + mystr.lpData);
                        //MessageBox.Show(mystr.lpData);
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        private void WriteLogFile(string path,string input)
        {
            try
            {

                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(input);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("写入文件报错:" + ex.Message);
            }
        }

        private void SendResponOrder()
        {
            int handle = FindWindow(null, "fmvsm100main");
            if (handle != 0)
            {
                byte[] byArr = Encoding.Default.GetBytes("U02=2=EOF");
                int len = byArr.Length;
                COPYDATASTRUCT cdata;
                cdata.cbData = len;
                cdata.dwData = (IntPtr)100;
                cdata.lpData = "U02=2=EOF";
                SendMessage(handle, WM_COPYDATA, 0, ref cdata);
            }
        }
        private void initJob()
        {
            try
            {
                if (scheduler == null && job == null)
                {
                    scheduler = Quartz.Impl.StdSchedulerFactory.GetDefaultScheduler();
                    JobWorker.jobHolder = this;
                    job = JobBuilder.Create<JobWorker>().WithIdentity("connectJob", "jobs").Build();
                    string time = ConfigWorker.GetConfigValue("timeTask");
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("connectTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(1))
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(60*int.Parse(time)).RepeatForever()).Build();
                    //ITrigger trigger = TriggerBuilder.Create().WithIdentity("connectTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(1))
                    //    .WithCronSchedule(time).Build();
                    scheduler.ScheduleJob(job, trigger);//把作业，触发器加入调度器。  
                    jobKey = job.Key;
                    //scheduler.DeleteJob
                }
                scheduler.Start();
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("定时任务异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 定时任务
        /// </summary>
        public void circleWork()
        {
            string path = Application.StartupPath + "\\imageErrorMess.txt";
            List<string> txtList = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null && ((line = sr.ReadLine()) != ""))
                    {
                        //根据文本里面的图片路径,并上传到FTP,如果上传失败,则记录到string[]中,并重新给txt赋值
                        string[] pathArray = line.Split('|');
                        FtpStatus ftpStatus = FtpHelper.upload(pathArray[0], pathArray[1]);
                        if (ftpStatus == FtpStatus.Success)
                        {
                            FileWorker.LogHelper.WriteLog("上传文件" + pathArray[0] + "成功");
                        }
                        else if (ftpStatus == FtpStatus.Skipped)
                        {
                            FileWorker.LogHelper.WriteLog("上传文件" + pathArray[0] + "在服务端已存在，跳过传输");
                        }
                        else
                        {
                            FileWorker.LogHelper.WriteLog("上传文件" + pathArray[0] + "失败");
                            txtList.Add(pathArray[0] + "|" + pathArray[1]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("定时任务文件打开报错:" + ex.Message);
            }
            string[] arr = ListToArray(txtList);
            if (arr.Length == 1 && arr[0] == "")
            {
                File.WriteAllText(path, "");
            }
            else
            {
                File.WriteAllLines(path, arr);
            }
        }

        private string[] ListToArray(List<string> list)
        {
            string[] array = null;
            if (list.Count == 0)
            {
                array = new string[1];
                array[0] = "";
            }
            else
            {
                array = new string[list.Count];
            }
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            } 
            return array;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                
                scheduler.DeleteJob(jobKey);
                scheduler.Shutdown();
                FtpHelper.closeFTP();
                Application.Exit();
                //scheduler.DeleteJob(jobKey);
                //scheduler.PauseJob(jobKey);
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("定时任务异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 压缩图片
        /// ImageCompression(@"D:\CarImage\压缩后\2.bmp", "D:\\CarImage\\压缩后\\4.bmp");
        /// </summary>
        /// <param name="filePath">要压缩的图片</param>
        /// <param name="createPath">制定生成后的图片路径</param>
        public void ImageCompression(string filePath, string createPath)
        {
            Bitmap bmp = null;
            ImageCodecInfo ici = null;
            System.Drawing.Imaging.Encoder ecd = null;
            EncoderParameter ept = null;
            EncoderParameters eptS = null;
            try
            {
                bmp = new Bitmap(filePath);
                ici = this.getImageCoderInfo("image/jpeg");
                ecd = System.Drawing.Imaging.Encoder.Quality;
                eptS = new EncoderParameters(1);
                ept = new EncoderParameter(ecd, 50L);
                eptS.Param[0] = ept;
                bmp.Save(createPath, ici, eptS);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                bmp.Dispose();
                ept.Dispose();
                eptS.Dispose();
            }
        }
        /// <summary>
        /// 获取图片编码类型信息
        /// </summary>
        /// <param name="coderType"></param>
        /// <returns></returns>
        private ImageCodecInfo getImageCoderInfo(string coderType) 
        {
            ImageCodecInfo[] iciS = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo retIci = null;
            foreach (ImageCodecInfo ici in iciS)
            {
                if (ici.MimeType.Equals(coderType))
                    retIci = ici;
            }
            return retIci;
        }
    }
}
