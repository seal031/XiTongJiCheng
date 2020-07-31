using AxIPModuleLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhShouDongBaoJing
{
    public partial class Form1 : Form
    {
        private delegate void delInfoList(string text);
        private int sdkPort = 0;
        public Form1()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("页面初始化异常");
                FileWorker.LogHelper.WriteLog(ex.Message);
            }
            try
            {
                this.sdkPort = int.Parse(ConfigWorker.GetConfigValue("sdkPort"));
                this.SDK.Port = this.sdkPort;
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("SDK端口配置不正确");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                //this.SDK.NewAlarm += new _ICooMonitorEvents_NewAlarmEventHandler(this.SDK_NewAlarm);
                //this.SDK.PanelStatusEx += new _ICooMonitorEvents_PanelStatusExEventHandler(this.SDK_PanelStatusEx);
                //this.SDK.ArmReport += new _ICooMonitorEvents_ArmReportEventHandler(this.SDK_ArmReport);
                this.SDK.VistaCIDReport += SDK_VistaCIDReport;
                this.SDK.VistaKeypadInfo += SDK_VistaKeypadInfo;
                this.SDK.Startup();
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("页面加载异常");
                FileWorker.LogHelper.WriteLog(ex.Message);
            }
        }

        private void SDK_ArmReport(object sender, _ICooMonitorEvents_ArmReportEvent e)
        {
            FileWorker.LogHelper.WriteLog("接收到23系列主机布/撤防事件");
            string value = string.Concat(new string[]
            {
                "主机:",
                e.strMac,
                "的23系列布/撤防信息如下：\r\nlArmed:",
                e.lArmed.ToString(),
                "\r\nlPlayback:",
                e.lPlayback.ToString(),
                "\r\nlUser:",
                e.lUser.ToString(),
                "\r\n"
            });
            FileWorker.LogHelper.WriteLog(value);
        }
        private void SDK_PanelStatusEx(object sender, _ICooMonitorEvents_PanelStatusExEvent e)
        {
            FileWorker.LogHelper.WriteLog("接收到TL/PLUS主机报警事件 或 23系列主机状态信息事件");
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
                    "的TL/PLUS报警信息 或 23系列主机状态信息如下：\r\n lplayback:",
                    e.lPlayback.ToString(),
                    "\r\nalarmBit:",
                    e.alarmBit.ToString(),
                    "\r\nbyPassedBit:",
                    e.byPassedBit.ToString(),
                    "\r\nfaultBit:",
                    e.faultBit.ToString(),
                    "\r\ntroubleBit:",
                    e.troubleBit.ToString(),
                    "\r\n"
                });
                FileWorker.LogHelper.WriteLog(text.Replace("\r\n", " "));
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("获取报警信息及发送消息异常");
                FileWorker.LogHelper.WriteLog(ex.Message);
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
                    FileWorker.LogHelper.WriteLog(text);
                    AlarmEntity alarmEntity = new AlarmEntity();
                    string msg = alarmEntity.toJson(); 
                    KafkaWorker.sendAlarmMessage(msg);
                    FileWorker.LogHelper.WriteLog(text.Replace("\r\n", " "));
                }
                catch (Exception ex)
                {
                    FileWorker.LogHelper.WriteLog("获取报警信息及发送消息异常");
                    FileWorker.LogHelper.WriteLog("******" + ex.Message);
                    FileWorker.LogHelper.WriteLog(ex.Message);
                }
                FileWorker.LogHelper.WriteLog("接收到PLUS II / SUPER主机报警事件");
            }
        }
        private void SDK_VistaKeypadInfo(object sender, _ICooMonitorEvents_VistaKeypadInfoEvent e)
        {
            FileWorker.LogHelper.WriteLog("获取到了一个KeypadInfo事件");
            try
            {
                string text = string.Concat(new string[]
                {
                    "主机:",
                    e.strMac,
                    "的VistaKeypad如下： lplayback:",
                    e.lPlayback.ToString(),
                    " cursorPos：",
                    e.cursorPos.ToString(),
                    " lState:",
                    e.lState.ToString(),
                    " showCursor:",
                    e.showCursor.ToString(),
                    " strInfo：",
                    e.strInfo.ToString()
                });
                FileWorker.LogHelper.WriteLog(text);
                //DeviceStateEntity deviceStateEntity = AlarmParseTool.parseDeviceState(e);
                //string msg = deviceStateEntity.toJson();
                //KafkaWorker.sendDeviceMessage(msg);
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("获取设备状态信息及发送消息异常" + ex.Message);
            }

        }
        private void SDK_VistaCIDReport(object sender, _ICooMonitorEvents_VistaCIDReportEvent e)
        {
            //FileWorker.LogHelper.WriteLog("获取到了一个CIDReport事件");
            //string recvTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //string sequence = Guid.NewGuid().ToString("N");
            //string uuid = Guid.NewGuid().ToString();
            try
            {
                string text = string.Concat(new string[]
                {
                    "主机:",
                    e.strMac,
                    "的CID Report如下：\r\n lplayback:",
                    e.lPlayback.ToString(),
                    " 账号：",
                    e.acct,
                    " IsNewEvent:",
                    e.isNewEvent.ToString(),
                    "\r\n CID:",
                    e.cID.ToString(),
                    " 子系统编号：",
                    e.subSystemID.ToString(),
                    " isZone:",
                    e.isZone.ToString(),
                    " strCode:",
                    e.strCode.ToString(),
                    "\r\n"
                });
                FileWorker.LogHelper.WriteLog(text.Replace("\r\n", " "));
                AlarmEntity alarmEntity = AlarmParseTool.parseAlarm(e);
                string msg = alarmEntity.toJson();
                //Debug.WriteLine(msg);
                KafkaWorker.sendAlarmMessage(msg);
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("获取报警信息及发送消息异常"+ex.Message);
            }
        }
    }
}
