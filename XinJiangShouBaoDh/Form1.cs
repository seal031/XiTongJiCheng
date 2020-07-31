using NetSDKCS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinJiangShouBaoDh
{
    /// <summary>
    /// *****************************************************
    /// 注意。大华手报用x64编译******************************
    /// *****************************************************
    /// </summary>
    public partial class Form1 : Form
    {
        private static fDisConnectCallBack m_DisConnectCallBack;
        private static fHaveReConnectCallBack m_ReConnectCallBack;
        private static fMessCallBackEx m_AlarmCallBack;
        private const int ALARM_START = 1;
        private const int ALARM_STOP = 0;

        private IntPtr m_LoginID = IntPtr.Zero;
        private NET_DEVICEINFO_Ex m_DeviceInfo;
        private bool m_IsListen = false;
        private Int64 m_ID = 1;
        private byte[] data;

        private string ip;
        private ushort port;
        private string username;
        private string password;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (loadLocalConfig())
            {
                m_DisConnectCallBack = new fDisConnectCallBack(DisConnectCallBack);
                m_ReConnectCallBack = new fHaveReConnectCallBack(ReConnectCallBack);
                m_AlarmCallBack = new fMessCallBackEx(AlarmCallBackEx);
                try
                {
                    NETClient.Init(m_DisConnectCallBack, IntPtr.Zero, null);
                    NETClient.SetAutoReconnect(m_ReConnectCallBack, IntPtr.Zero);
                    NETClient.SetDVRMessCallBack(m_AlarmCallBack, IntPtr.Zero);
                    login();
                    startListener();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    FileWorker.LogHelper.WriteLog("SDK初始化错误：" + ex.Message);
                }
            }
        }

        private void DisConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            FileWorker.LogHelper.WriteLog("连接已断开");
        }
        private void ReConnectCallBack(IntPtr lLoginID, IntPtr pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            FileWorker.LogHelper.WriteLog("重连成功");
        }

        private bool AlarmCallBackEx(int lCommand, IntPtr lLoginID, IntPtr pBuf, uint dwBufLen, IntPtr pchDVRIP, int nDVRPort, bool bAlarmAckFlag, int nEventID, IntPtr dwUser)
        {
            EM_ALARM_TYPE type = (EM_ALARM_TYPE)lCommand;
            switch (type)
            {
                case EM_ALARM_TYPE.ALARM_INPUT_SOURCE_SIGNAL:  //12675  0x3183
                    string deviceIp = Marshal.PtrToStringAnsi(pchDVRIP);
                    //MessageBox.Show(deviceIp);
                    data = new byte[dwBufLen];
                    Marshal.Copy(pBuf, data, 0, (int)dwBufLen);
                    for (int i = 0; i < dwBufLen; i++)
                    {
                        if (data[i] == ALARM_START) // alarm start 报警开始
                        {
                            AlarmEntity alarmEntity = new AlarmEntity();
                            alarmEntity.body.alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            alarmEntity.body.alarmEquCode = deviceIp + "-" + i.ToString();
                            alarmEntity.body.alarmName = "手动报警新事件";
                            alarmEntity.body.alarmNameCode = "AC0301";
                            alarmEntity.body.alarmStateCode = "AS01";
                            alarmEntity.body.alarmStateName = "未解除";
                            alarmEntity.body.airportIata = "KCA";
                            alarmEntity.body.airportName = "库车";
                            //info.ID = m_ID;
                            //info.Channel = i;
                            string alarmMessage = alarmEntity.toJson();
                            FileWorker.LogHelper.WriteLog("收到ALARM_INPUT_SOURCE_SIGNAL报警");
                            KafkaWorker.sendAlarmMessage(alarmMessage);
                        }
                        else //alarm stop 报警停止
                        {
                        }
                    }
                    break;
                case EM_ALARM_TYPE.ALARM_ALARM_EX2:
                    NET_ALARM_ALARM_INFO_EX2 info = (NET_ALARM_ALARM_INFO_EX2)Marshal.PtrToStructure(pBuf, typeof(NET_ALARM_ALARM_INFO_EX2));
                    string deviceIp1 = Marshal.PtrToStringAnsi(pchDVRIP);
                    FileWorker.LogHelper.WriteLog("收到外部报警，ip是" + deviceIp1 + "，通道号是" + info.nChannelID);
                    AlarmEntity alarmEntity1 = new AlarmEntity();
                    alarmEntity1.body.alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    alarmEntity1.body.alarmEquCode = deviceIp1 + "-" + info.nChannelID.ToString();
                    alarmEntity1.body.alarmName = "手动报警新事件";
                    alarmEntity1.body.alarmNameCode = "AC0301";
                    alarmEntity1.body.alarmStateCode = "AS01";
                    alarmEntity1.body.alarmStateName = "未解除";
                    alarmEntity1.body.airportIata = "KCA";
                    alarmEntity1.body.airportName = "库车";
                    string alarmMessage1 = alarmEntity1.toJson();
                    KafkaWorker.sendAlarmMessage(alarmMessage1);
                    //data = new byte[dwBufLen];
                    //Marshal.Copy(pBuf, data, 0, (int)dwBufLen);
                    //for (int i = 0; i < dwBufLen; i++)
                    //{
                    //    if (data[i] == ALARM_START) // alarm start 报警开始
                    //    {
                    //        AlarmEntity alarmEntity = new AlarmEntity();
                    //        alarmEntity.body.alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    //        alarmEntity.body.alarmEquCode = deviceIp1 + "-" + i.ToString();
                    //        alarmEntity.body.alarmName = "手动报警新事件";
                    //        alarmEntity.body.alarmNameCode = "AC0301";
                    //        alarmEntity.body.alarmStateCode = "AS01";
                    //        alarmEntity.body.alarmStateName = "未解除";
                    //        alarmEntity.body.airportIata = "KCA";
                    //        alarmEntity.body.airportName = "库车";
                    //        string alarmMessage = alarmEntity.toJson();
                    //        FileWorker.LogHelper.WriteLog("收到外部报警");
                    //        KafkaWorker.sendAlarmMessage(alarmMessage);
                    //    }
                    //    else //alarm stop 报警停止
                    //    {
                    //    }
                    //}
                    break;
                default:
                    break;
            }

            return true;
        }

        private void login()
        {
            m_DeviceInfo = new NET_DEVICEINFO_Ex();
            m_LoginID = NETClient.LoginWithHighLevelSecurity(ip, port, username, password, EM_LOGIN_SPAC_CAP_TYPE.TCP, IntPtr.Zero, ref m_DeviceInfo);
            if (IntPtr.Zero == m_LoginID)
            {
                FileWorker.LogHelper.WriteLog("登录返回异常：" + NETClient.GetLastError());
                return;
            }
        }
        private void startListener()
        {
            bool ret = NETClient.StartListen(m_LoginID);
            if (!ret)
            {
                FileWorker.LogHelper.WriteLog("开启监听返回异常：" + NETClient.GetLastError());
                return;
            }
        }
        private void stopListener()
        {
            bool ret = NETClient.StopListen(m_LoginID);
            if (!ret)
            {
                FileWorker.LogHelper.WriteLog("停止监听返回异常：" + NETClient.GetLastError());
                return;
            }
        }
        private void logout()
        {
            bool result = NETClient.Logout(m_LoginID);
            if (!result)
            {
                FileWorker.LogHelper.WriteLog("登出返回异常：" + NETClient.GetLastError());
                return;
            }
            m_LoginID = IntPtr.Zero;
        }

        private bool loadLocalConfig()
        {
            try
            {
                ip = ConfigWorker.GetConfigValue("remoteIp");
                string portStr = ConfigWorker.GetConfigValue("port");
                username = ConfigWorker.GetConfigValue("username");
                password = ConfigWorker.GetConfigValue("password");
                if (ushort.TryParse(portStr, out port)==false)
                {
                    MessageBox.Show("端口配置不正确，应为数字。");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取本地配置错误：" + ex.Message);
                return false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopListener();
            logout();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            NETClient.Cleanup();
        }
    }
}
