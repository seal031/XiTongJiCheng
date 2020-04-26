using IntegrationClient.Model;
using IntegrationClient.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeiJieBaoJing.Entity;

namespace WeiJieBaoJing
{
    public partial class Form1 : Form
    {
        private DeviceSDKAPUModel m_Model = new DeviceSDKAPUModel();
        private ConnectedDevicesViewModel model;

        public Form1()
        {
            InitializeComponent();
            KafkaWorker.control = this.rtb;
            FileWorker.control = this.rtb;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            FileWorker.PrintLog("开始扫描围界设备");
            FileWorker.WriteLog("开始扫描围界设备");
            scanDevice();
            FileWorker.PrintLog("扫描围界设备完毕");
            FileWorker.WriteLog("扫描围界设备完毕");
            Task taskGetMessage = new Task(getMessage);
            KafkaWorker.OnGetMessage += KafkaWorker_OnGetMessage;
            taskGetMessage.Start();
        }

        private void KafkaWorker_OnGetMessage(string message)
        {
            FileWorker.WriteLog("////////////////收到的消息是" + message + "//////////////////");
            try
            {
                if (message.Contains("plan"))//如果是布放撤防命令
                {
                    PlanMessage command = PlanMessage.fromJson(message);
                    PlanEntity.insertPlan(command);
                }
                else
                {
                    MessageCommand command = MessageCommand.fromJson(message);
                    if (command != null)
                    {
                        if (command.meta.receiver == "FiberDefender")//
                        {
                            string deviceFullName = command.body.alarmEquName;
                            //if (m_Model.deviceAlarmStates.FirstOrDefault(d => d.deviceName == deviceFullName) == null)
                            //{
                            //    return;//当分布式运行时，如果收到的消息中指定的设备不属于本程序所在的分组，则忽略此消息
                            //}
                            string[] strArray = deviceFullName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            string deviceName = strArray[0];
                            string channelName = strArray[1];
                            string openFlag = command.body.openFlag;
                            switch (openFlag)//0关闭1开启
                            {
                                case "0":
                                    FileWorker.WriteLog("%%%%%%%%停止向设备" + deviceFullName + "输出端口持续发送信号%%%%%%");
                                    switch (channelName)
                                    {
                                        case "CHA":
                                            m_Model.ToggleSetting(deviceName, DeviceSDKAPUModel.ALARMA, DeviceSDKAPUModel.DISABLED_PROPERTY);
                                            break;  
                                        case "CHB":
                                            m_Model.ToggleSetting(deviceName, DeviceSDKAPUModel.ALARMB, DeviceSDKAPUModel.DISABLED_PROPERTY);
                                            break;
                                    }
                                    break;
                                case "1":
                                    FileWorker.WriteLog("%%%%%%%%开始向设备" + deviceFullName + "输出端口持续发送信号%%%%%%");
                                    switch (channelName)
                                    {
                                        case "CHA":
                                            m_Model.ToggleSetting(deviceName, DeviceSDKAPUModel.ALARMA, DeviceSDKAPUModel.ENABLED_PROPERTY);
                                            break;
                                        case "CHB":
                                            m_Model.ToggleSetting(deviceName, DeviceSDKAPUModel.ALARMB, DeviceSDKAPUModel.ENABLED_PROPERTY);
                                            break;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                FileWorker.PrintLog("此消息无法反序列化" + e.Message);
                FileWorker.WriteLog("此消息无法反序列化" + e.Message);
            }
        }

        private void getMessage()
        {
            KafkaWorker.startGetMessage();//开始接收消息
        }

        private void scanDevice()
        {
            model = new ConnectedDevicesViewModel(m_Model);
            model.StartScan();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_Model.ToggleSetting(textBox1.Text.Trim(), DeviceSDKAPUModel.ALARMB,DeviceSDKAPUModel.ENABLED_PROPERTY);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            m_Model.ToggleSetting(textBox1.Text.Trim(), DeviceSDKAPUModel.ALARMB, DeviceSDKAPUModel.DISABLED_PROPERTY);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            m_Model.ToggleSetting(textBox1.Text.Trim(), DeviceSDKAPUModel.ALARMA, DeviceSDKAPUModel.ENABLED_PROPERTY);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            m_Model.ToggleSetting(textBox1.Text.Trim(), DeviceSDKAPUModel.ALARMA, DeviceSDKAPUModel.DISABLED_PROPERTY);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //m_Model.Stop();
            //将所有设备的人工控制关闭
            FileWorker.PrintLog("正在关闭所有设备的人工控制");
            FileWorker.WriteLog("正在关闭所有设备的人工控制");
            m_Model.closeAllDeviceUserControl();
            model.Stop();
            //e.Cancel = true;
        }

        private void btn_closeAllSetting_Click(object sender, EventArgs e)
        {
            FileWorker.PrintLog("正在关闭所有设备的下游声光报警");
            FileWorker.WriteLog("正在关闭所有设备的下游声光报警");
            m_Model.closeAllDeviceSetting();
        }
    }
}
