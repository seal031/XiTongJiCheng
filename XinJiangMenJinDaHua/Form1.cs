﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace XinJiangMenJinDaHua
{
    public partial class Form1 : Form
    {
        public string videoUrl = string.Empty;
        public uint videoPort = 9000;
        public string videoUser = string.Empty;
        public string videoPwd = string.Empty;
        private string airportIata = string.Empty;
        private string airportName = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (loadLocalConfig())
            {
                if (login())
                {
                    getDeviceState();
                    loadOrganization();
                }
            }

        }

        private void loadOrganization()
        {
            IntPtr result = DPSDK_LoadDGroupInfo(nPDLLHandle, ref nGroupLen, (IntPtr)60000);
            if (result == (IntPtr)0)
            {
                FileWorker.LogHelper.WriteLog("加载组织结构成功");
                byte[] szGroupStr = new byte[(int)nGroupLen + 1];
                // IntPtr iRet = DPSDK_GetDGroupStr(nPDLLHandle, ref szGroupStr[0], nGroupLen, (IntPtr)10000);
                IntPtr iRet = DPSDK_GetDGroupStr(nPDLLHandle, szGroupStr, nGroupLen, (IntPtr)10000);
                if (iRet == IntPtr.Zero)
                {
                }
                else
                {
                    FileWorker.LogHelper.WriteLog("获取组织树XML失败，错误码：" + result.ToString());
                }
            }
            else
            {
                FileWorker.LogHelper.WriteLog("获取组织树XML失败，错误码：" + result.ToString());
            }
        }

        private bool loadLocalConfig()
        {
            try
            {
                videoUrl = ConfigWorker.GetConfigValue("videoUrl");
                videoPort = uint.Parse(ConfigWorker.GetConfigValue("videoPort"));
                videoUser = ConfigWorker.GetConfigValue("videoUser");
                videoPwd = ConfigWorker.GetConfigValue("videoPwd");
                return true;
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("读取本地配置文件出现异常" + ex.Message);
                return false;
            }
        }
        public bool login()
        {
            IntPtr result1 = DPSDK_Create(dpsdk_sdk_type_e.DPSDK_CORE_SDK_SERVER, ref this.nPDLLHandle);
            Login_Info_t loginInfo = new Login_Info_t();
            loginInfo.szIp = videoUrl;
            loginInfo.nPort = videoPort;
            loginInfo.szUsername = videoUser;
            loginInfo.szPassword = videoPwd;
            loginInfo.nProtocol = dpsdk_protocol_version_e.DPSDK_PROTOCOL_VERSION_II;
            loginInfo.iType = 1;
            IntPtr result = DPSDK_Login(this.nPDLLHandle, ref loginInfo, (IntPtr)10000);
            IntPtr result2 = DPSDK_InitExt();//初始化解码播放接口
            if (result == (IntPtr)0 && result2 == (IntPtr)0)
            {
                FileWorker.LogHelper.WriteLog("登录成功");
                return true;
            }
            else
            {
                FileWorker.LogHelper.WriteLog("登录失败，错误码：" + result.ToString());
                return false;
            }
        }

        public void getDeviceState()
        {
            IntPtr result = (IntPtr)10;
            IntPtr pUser = default(IntPtr);
            result = DPSDK_SetSyncTimeOpen(nPDLLHandle, 1);//开启校时
            fDPSDKAlarmCallback  alarmCallback= AlarmStatusCallback;
            result = DPSDK_SetDPSDKAlarmCallback(nPDLLHandle, alarmCallback, pUser);
            if (result == (IntPtr)0)
            {
                FileWorker.LogHelper.WriteLog("报警状态回调成功");
            }
            else
            {
                FileWorker.LogHelper.WriteLog("报警状态回调成功，错误码：" + result.ToString());
            }
        }

        private IntPtr AlarmStatusCallback(IntPtr nPDLLHandle, StringBuilder szAlarmId, IntPtr nDeviceType, StringBuilder szCameraId, StringBuilder szDeviceName, StringBuilder szChannelName, StringBuilder szCoding, StringBuilder szMessage, IntPtr nAlarmType, IntPtr nEventType, IntPtr nLevel, long nTime, StringBuilder pAlarmData, IntPtr nAlarmDataLen, StringBuilder pPicData, IntPtr nPicDataLen, IntPtr pUserParam)
        {
            FileWorker.LogHelper.WriteLog("信息开始接收");
            byte[] bDevName = System.Text.Encoding.Default.GetBytes(szDeviceName.ToString());
            string szDevName = System.Text.Encoding.UTF8.GetString(bDevName);//设备名称

            byte[] bName = System.Text.Encoding.Default.GetBytes(szChannelName.ToString());
            string strname = System.Text.Encoding.UTF8.GetString(bName);//通道名称

            DateTime dAlarmTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dtAlarmTime = dAlarmTime.AddSeconds((UInt64)nTime);
            string uAlarmTime = dtAlarmTime.ToString("yyyy-MM-dd HH:mm:ss"); //报警时间

            byte[] bMessage = System.Text.Encoding.Default.GetBytes(szMessage.ToString());
            string alarmMessage = System.Text.Encoding.UTF8.GetString(bMessage);//报警信息

            string str = string.Format("报警Id:{0},设备类型:{1},通道Id:{2},设备名称:{3},通道名称:{4},编码:{5},报警信息:{6},报警类型:{7},发生类型:{8},报警时间(时间戳):{9},报警数据:{10},报警数据长度:{11},用户数据:{12}",
                szAlarmId.ToString(), nDeviceType.ToInt32(), szCameraId.ToString(), szDevName, strname, szCoding.ToString(), alarmMessage, nAlarmType, nEventType, uAlarmTime, pAlarmData, nAlarmDataLen, pUserParam);
            //FileWorker.LogHelper.WriteLog(str);
            AlarmEntity alarmEnt = AlarmParseTool.parseAlarm(uAlarmTime, szCameraId.ToString());
            string mess = alarmEnt.toJson();
            KafkaWorker.sendAlarmMessage(mess);
            return (IntPtr)0;
        }

    }
}
