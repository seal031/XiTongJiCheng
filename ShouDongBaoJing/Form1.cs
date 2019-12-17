using AxIPModuleLib;
using IPMALARM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPMALARM
{
    public partial class Form1 : Form
    {
        private delegate void delInfoList(string text);
        private KafkaWorker kafka;
        private int sdkPort = 0;
        public Form1()
        {
            try
            {
                FileWorker.WriteLog("页面初始化开始");
                this.InitializeComponent();
                this.kafka = new KafkaWorker();
                FileWorker.WriteLog("页面初始化结束");
            }
            catch (Exception ex)
            {
                FileWorker.WriteLog("页面初始化异常");
                this.SetrichTextBox("******" + ex.Message);
                FileWorker.WriteLog(ex.Message);
            }
            try
            {
                this.sdkPort = int.Parse(ConfigWorker.GetConfigValue("sdkPort"));
                this.SDK.Port = this.sdkPort;
            }
            catch (Exception ex)
            {
                FileWorker.WriteLog("SDK端口配置不正确");
                this.SetrichTextBox("****** SDK端口配置不正确");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                FileWorker.WriteLog("页面加载开始");
                this.SDK.NewAlarm += new _ICooMonitorEvents_NewAlarmEventHandler(this.SDK_NewAlarm);
                this.SDK.PanelStatusEx += new _ICooMonitorEvents_PanelStatusExEventHandler(this.SDK_PanelStatusEx);
                this.SDK.ArmReport += new _ICooMonitorEvents_ArmReportEventHandler(this.SDK_ArmReport);
                this.SDK.Startup();
                FileWorker.WriteLog("页面加载结束");
            }
            catch (Exception ex)
            {
                FileWorker.WriteLog("页面加载异常");
                this.SetrichTextBox("******" + ex.Message);
                FileWorker.WriteLog(ex.Message);
            }
        }

        private void SDK_ArmReport(object sender, _ICooMonitorEvents_ArmReportEvent e)
        {
            this.SetrichTextBox("接收到23系列主机布/撤防事件");
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
            this.SetrichTextBox(value);
            FileWorker.WriteLog("接收到23系列主机布/撤防事件");
        }
        private void SDK_PanelStatusEx(object sender, _ICooMonitorEvents_PanelStatusExEvent e)
        {
            FileWorker.WriteLog("接收到TL/PLUS主机报警事件 或 23系列主机状态信息事件");
            this.SetrichTextBox("接收到TL/PLUS主机报警事件 或 23系列主机状态信息事件");
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
                //this.SetrichTextBox(text);
                //MessageEntity messageEntity = new MessageEntity();
                //messageEntity.meta = new MessageEntity.MessageHead
                //{
                //    receiver = "",
                //    sequence = sequence,
                //    recvSequence = "",
                //    sendTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                //    recvTime = recvTime,
                //    forward = DateTime.Now.ToString("yyyyMMddHHmmss")
                //};
                //messageEntity.body = new MessageEntity.MessageBody();
                //messageEntity.body.buildFromEvent(e);
                //messageEntity.body.alarmTime = alarmTime;
                //string msg = JsonConvert.SerializeObject(messageEntity);
                //this.kafka.sendMsg(msg);
                //LogEntity logEntity = new LogEntity
                //{
                //    uuid = uuid,
                //    content = e,
                //    receiver = "",
                //    sequence = sequence,
                //    recvSequence = "",
                //    sendTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                //    recvTime = recvTime,
                //    forwardTime = DateTime.Now.ToString("yyyyMMddHHmmss")
                //};
                //string log = JsonConvert.SerializeObject(logEntity);
                //this.kafka.sendLog(log);
                FileWorker.WriteLog(text.Replace("\r\n", " "));
            }
            catch (Exception ex)
            {
                FileWorker.WriteLog("获取报警信息及发送消息异常");
                this.SetrichTextBox("******" + ex.Message);
                FileWorker.WriteLog(ex.Message);
            }
        }
        private void SDK_NewAlarm(object sender, _ICooMonitorEvents_NewAlarmEvent e)
        {
            bool flag = e.lPlayback == 0 && e.lState == 1;
            FileWorker.WriteLog("e.lPlayback:" + e.lPlayback.ToString() + "  e.lState:" + e.lState.ToString());
            if (flag)
            {
                FileWorker.WriteLog("接收到PLUS II / SUPER主机报警事件");
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
                    this.SetrichTextBox(text);
                    MessageEntity messageEntity = new MessageEntity();
                    messageEntity.meta = new MessageEntity.MessageHead
                    {
                        receiver = "",
                        sequence = sequence,
                        recvSequence = "",
                        sendTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                        recvTime = recvTime,
                        forward = DateTime.Now.ToString("yyyyMMddHHmmss")
                    };
                    messageEntity.body = new MessageEntity.MessageBody();
                    messageEntity.body.buildFromEvent(e);
                    messageEntity.body.alarmTime = alarmTime;
                    string msg = JsonConvert.SerializeObject(messageEntity);
                    this.kafka.sendMsg(msg);
                    LogEntity value = new LogEntity
                    {
                        uuid = uuid,
                        content = e,
                        receiver = "",
                        sequence = sequence,
                        recvSequence = "",
                        sendTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                        recvTime = recvTime,
                        forwardTime = DateTime.Now.ToString("yyyyMMddHHmmss")
                    };
                    string log = JsonConvert.SerializeObject(value);
                    this.kafka.sendLog(log);
                    FileWorker.WriteLog(text.Replace("\r\n", " "));
                }
                catch (Exception ex)
                {
                    FileWorker.WriteLog("获取报警信息及发送消息异常");
                    this.SetrichTextBox("******" + ex.Message);
                    FileWorker.WriteLog(ex.Message);
                }
                this.SetrichTextBox("接收到PLUS II / SUPER主机报警事件");
            }
        }
        private void SDK_VistaKeypadInfo(object sender, _ICooMonitorEvents_VistaKeypadInfoEvent e)
        {
            FileWorker.WriteLog("获取到了一个KeypadInfo事件");
        }
        private void SDK_VistaCIDReport(object sender, _ICooMonitorEvents_VistaCIDReportEvent e)
        {
            FileWorker.WriteLog("获取到了一个CIDReport事件");
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
                this.SetrichTextBox(text);
                MessageEntity messageEntity = new MessageEntity();
                messageEntity.meta = new MessageEntity.MessageHead
                {
                    receiver = "",
                    sequence = sequence,
                    recvSequence = "",
                    sendTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    recvTime = recvTime,
                    forward = DateTime.Now.ToString("yyyyMMddHHmmss")
                };
                messageEntity.body = new MessageEntity.MessageBody();
                messageEntity.body.buildFromEvent(e);
                messageEntity.body.alarmTime = alarmTime;
                string msg = JsonConvert.SerializeObject(messageEntity);
                this.kafka.sendMsg(msg);
                LogEntity value = new LogEntity
                {
                    uuid = uuid,
                    content = e,
                    receiver = "",
                    sequence = sequence,
                    recvSequence = "",
                    sendTime = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    recvTime = recvTime,
                    forwardTime = DateTime.Now.ToString("yyyyMMddHHmmss")
                };
                string log = JsonConvert.SerializeObject(value);
                this.kafka.sendLog(log);
                FileWorker.WriteLog(text.Replace("\r\n", " "));
            }
            catch (Exception ex)
            {
                FileWorker.WriteLog("获取报警信息及发送消息异常");
                this.SetrichTextBox("******" + ex.Message);
                FileWorker.WriteLog(ex.Message);
            }
        }
        private void SetrichTextBox(string value)
        {
            bool invokeRequired = this.richTextBox1.InvokeRequired;
            if (invokeRequired)
            {
                Form1.delInfoList method = new Form1.delInfoList(this.SetrichTextBox);
                this.richTextBox1.Invoke(method, new object[]
                {
                    DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " " + value + "\n"
                });
            }
            else
            {
                bool flag = this.richTextBox1.Lines.Length > 100;
                if (flag)
                {
                    this.richTextBox1.Clear();
                }
                this.richTextBox1.Focus();
                this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
                this.richTextBox1.ScrollToCaret();
                this.richTextBox1.AppendText(DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + " " + value + "\n");
            }
        }

    }
}
