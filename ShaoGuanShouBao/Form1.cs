using AxIPModuleLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShaoGuanShouBao
{
    public partial class Form1 : Form
    {
        private int sdkPort = 0;
        private string airportIata;
        private string airportName;

        public Form1()
        {
            try
            {
                FileWorker.LogHelper.WriteLog("页面初始化开始");
                this.InitializeComponent();
                FileWorker.LogHelper.WriteLog("页面初始化结束");
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("页面初始化异常" + ex.Message);
                //this.SetrichTextBox("******" + ex.Message);
                //FileWorker.LogHelper.WriteLog(ex.Message);
            }
            try
            {
                this.sdkPort = int.Parse(ConfigWorker.GetConfigValue("sdkPort"));
                this.SDK.Port = this.sdkPort;
                airportIata = ConfigWorker.GetConfigValue("airportIata");
                airportName = ConfigWorker.GetConfigValue("airportName");
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("SDK端口配置不正确" + ex.Message);
                //this.SetrichTextBox("****** SDK端口配置不正确");
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                FileWorker.LogHelper.WriteLog("页面加载开始");
                this.SDK.NewAlarm += new _ICooMonitorEvents_NewAlarmEventHandler(this.SDK_NewAlarm);
                this.SDK.Startup();
                FileWorker.LogHelper.WriteLog("页面加载结束");
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("页面加载异常" + ex.Message);
                //this.SetrichTextBox("******" + ex.Message);
                // FileWorker.LogHelper.WriteLog(ex.Message);
            }
        }
        private void SDK_NewAlarm(object sender, _ICooMonitorEvents_NewAlarmEvent e)
        {
            bool flag = e.lPlayback == 0 && e.lState == 1;
            FileWorker.LogHelper.WriteLog("e.lPlayback:" + e.lPlayback.ToString() + "  e.lState:" + e.lState.ToString());
            if (flag)
            {
                FileWorker.LogHelper.WriteLog("接收到PLUS II / SUPER主机报警事件");
                string recvTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                string alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string sequence = Guid.NewGuid().ToString("N");
                string uuid = Guid.NewGuid().ToString();
                try
                {
                    string text = string.Concat(new string[]
                    {
                        "主机:",
                        e.strMac,
                        "的PLUS II/SUPER报警信息如下：\r\n lplayback:",
                        e.lPlayback.ToString(),
                        "\r\n isZone:",
                        e.lZone.ToString(),
                        "\r\nlState:",
                        e.lState.ToString()
                    });
                    FileWorker.LogHelper.WriteLog(text.Replace("\r\n", " "));
                    if (e.lState != 0 && e.lPlayback == 0)
                    {
                        AlarmEntity alarmEntity = AlarmParseTool.parseAlarm(e, airportIata, airportName);
                        string msg = alarmEntity.toJson();
                        //Debug.WriteLine(msg);
                        KafkaWorker.sendAlarmMessage(msg);
                    }
                }
                catch (Exception ex)
                {
                    FileWorker.LogHelper.WriteLog("获取报警信息及发送消息异常" + ex.Message);
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.SDK.Shutdown();
            this.SDK.Dispose();
            this.SDK = null;
        }
    }
}
