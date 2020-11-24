using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinJiangShouBaoSanRun
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 是否第一次进行回调。ture时只发送设备基本信息；false时发送设备状态变化信息
        /// </summary>
        private bool isFirstCallback = true;

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
            sdk.EventGroupOperator += Sdk_EventGroupOperator;
        }

        //分组变化通知
        //参 数：lOpType 操作类型，0:添加 1:移除 3:更名 4:移动
        //ulGroupID 分组 ID
        //ulParentGroupID 父分组 ID  107-139
        //ulGroupType 分组类型（正常分组、联动分组）
        //strName 分组名称
        private void Sdk_EventGroupOperator(object sender, AxVSPOcxClientLib._DVSPOcxClientEvents_EventGroupOperatorEvent e)
        {
            FileWorker.LogHelper.WriteLog($"分组变化通知{e.lOpType}&{e.strName}&{e.ulGroupID}&{e.ulGroupType}&{e.ulParentGroupID}");
        }

        //设备变化通知
        //参 数：lOpType 操作类型，0:添加 1:移除 2:状态改变 3:更名 4:移动
        //ulCameraID 设备 ID
        //ulParentGroupID 父分组 ID
        //ulCameraType 设备类型（报警设备0、联动设备）
        //strName 设备名称
        //ulState 当前状态，1:正常状态 2:离线状态 3:报警状态 4:报警处理状态
        private void Sdk_EventCameraOperator(object sender, AxVSPOcxClientLib._DVSPOcxClientEvents_EventCameraOperatorEvent e)
        {
            FileWorker.LogHelper.WriteLog($"设备变化通知{e.lOpType};{e.strName};{e.ulCameraID};{e.ulCameraType};{e.ulParentGroupID};{e.ulState}");
            if (e.ulCameraType == 0)//只关注报警设备
            {
                if (e.ulState == 3|| e.ulState == 4)//报警
                {
                    AlarmEntity entity = MessageTransfor.getAlarm(e);
                    if (entity != null)
                    {
                        string message = entity.toJson();
                        KafkaWorker.sendAlarmMessage(message);
                    }
                }
                if (e.ulState == 1 || e.ulState == 2)//设备状态变化，如果第一次运行，则同时要发送设备基本信息
                {
                    if (isFirstCallback)
                    {
                        //设备基本信息
                        DeviceEntity device = MessageTransfor.getDevice(e);
                        if (device != null)
                        {
                            string message = device.toJson();
                            KafkaWorker.sendDeviceMessage(message);
                        }
                    }
                    else
                    {
                        //设备状态变化
                        DeviceStateEntity deviceState = MessageTransfor.getDeviceStateChange(e);
                        if (deviceState != null)
                        {
                            string message = deviceState.toJson();
                            KafkaWorker.sendDeviceStateMessage(message);
                        }
                    }
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
            FileWorker.LogHelper.WriteLog($"服务器变化通知{e.lALCHandle}&{e.lOpType}&{e.strName}&{e.ulState}");
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
            Task.Run(() => {
                Thread.Sleep(2 * 60 * 1000);
                FileWorker.LogHelper.WriteLog("isFirstCallback设置为false，后续将不再发送设备基本信息");
                isFirstCallback = false;//留出10秒钟给获取设备基本信息
            });
        }

        private void Sdk_EventReady(object sender, EventArgs e)
        {
            FileWorker.LogHelper.WriteLog("SDK初始化完成");
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

        private void button1_Click(object sender, EventArgs e)
        {
           MessageBox.Show(sdk.GetLastError().ToString());
        }
    }
}
