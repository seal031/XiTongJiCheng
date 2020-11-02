using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinJiangShouBaoSanRun
{
    public partial class Form1 : Form
    {
        private string remoteIp;
        private short port;
        private string username;
        private string password;

        public Form1()
        {
            InitializeComponent();
            //接 口：void EventReady() 说明：通知用户控件已经初始化完成，建议登录操作和设置工作在本事件里调用。
            sdk.EventReady += Sdk_EventReady;
            //登录结果
            sdk.EventLoginServer += Sdk_EventLoginServer;
            
            sdk.EventALCOperator += Sdk_EventALCOperator;
            sdk.EventCameraOperator += Sdk_EventCameraOperator;
        }

        //设备变化通知
        //参 数：lOpType 操作类型，0:添加 1:移除 2:状态改变 3:更名 4:移动
        //ulCameraID 设备 ID
        //ulParentGroupID 父分组 ID
        //ulCameraType 设备类型（报警设备、联动设备）
        //strName 设备名称
        //ulState 当前状态，1:正常状态 2:离线状态 3:报警状态 4:报警处理状态
        private void Sdk_EventCameraOperator(object sender, AxVSPOcxClientLib._DVSPOcxClientEvents_EventCameraOperatorEvent e)
        {
            if (e.ulState == 3 && e.ulCameraType==1)
            {
                AlarmEntity entity = MessageTransfor.getAlarm(e);
                if (entity != null)
                {
                    string message = entity.toJson();
                    KafkaWorker.sendAlarmMessage(message);
                }
            }
            if ((e.ulState == 1 || e.ulState == 2) && e.ulCameraType == 1)
            {
                DeviceStateEntity deviceState = MessageTransfor.getDeviceStateChange(e);
                if (deviceState != null)
                {
                    string message = deviceState.toJson();
                    KafkaWorker.sendAlarmMessage(message);
                }
            }
        }

        //报警服务器变化通知
        //参 数：lOpType 操作类型，0:添加 1:移除 2:状态改变 3:更名
        //lALCHandle 报警服务器句柄
        //strName 报警服务器名称
        //ulState 当前状态，1:在线 2:离线
        private void Sdk_EventALCOperator(object sender, AxVSPOcxClientLib._DVSPOcxClientEvents_EventALCOperatorEvent e)
        {
            //DeviceStateEntity entity = MessageTransfor.getDeviceStateChange(e);

            //string message = entity.toJson();
            //KafkaWorker.sendAlarmMessage(message);
        }

        private void Sdk_EventLoginServer(object sender, AxVSPOcxClientLib._DVSPOcxClientEvents_EventLoginServerEvent e)
        {
            FileWorker.LogHelper.WriteLog("登录回调返回结果为：" + e.bSuccess);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
           
        }

        private void Sdk_EventReady(object sender, EventArgs e)
        {
            if (loadLocalConfig())
            {
                login();
            }
        }

        private bool loadLocalConfig()
        {
            try
            {
                remoteIp = ConfigWorker.GetConfigValue("remoteIp");
                port = short.Parse(ConfigWorker.GetConfigValue("port"));
                username = ConfigWorker.GetConfigValue("username");
                password = ConfigWorker.GetConfigValue("password");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("本地配置文件配置项不正确，" + ex.Message);
                return false;
            }
        }

        private void login()
        {
            var loginResult = sdk.LoginServer(remoteIp, port, username, password);
            FileWorker.LogHelper.WriteLog("登录即时返回结果为：" + loginResult);
        }

        private void logout()
        {
            var logoutResult = sdk.LogoutServer();
            FileWorker.LogHelper.WriteLog("登出返回结果为：" + logoutResult);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            logout();
        }
    }
}
