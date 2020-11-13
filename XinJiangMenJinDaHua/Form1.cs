using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        Dictionary<string, List<string>> alarmRuleDic = new Dictionary<string, List<string>>();
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
                    getAlarmState();

                    loadOrganization();

                    //EnableAlarm();
                }
            }

        }

        private void EnableAlarm()
        {
            Alarm_Enable_Info_t pSourceInfo = new Alarm_Enable_Info_t();
            pSourceInfo.nCount = 1;
            pSourceInfo.pSources = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Alarm_Single_Enable_Info_t)));

            Alarm_Single_Enable_Info_t EnableInfo = new Alarm_Single_Enable_Info_t();

            EnableInfo.szAlarmDevId = "1000000";//"1000605"; 
            EnableInfo.nVideoNo = Convert.ToInt32("-1");// -1;
            EnableInfo.nAlarmInput = Convert.ToInt32("-1");//-1;

            //EnableInfo.nAlarmType = Test_DPSDK_Core_CSharp.dpsdk_alarm_type_e.DPSDK_CORE_ALARM_TYPE_MOTION_DETECT;
            UInt32 nAlarm = Convert.ToUInt32(ConfigWorker.GetConfigValue("alarmType"));
            EnableInfo.nAlarmType = (dpsdk_alarm_type_e)nAlarm;

            Marshal.StructureToPtr(EnableInfo, pSourceInfo.pSources, false);

            IntPtr result = DPSDK_EnableAlarm(nPDLLHandle, ref pSourceInfo, (IntPtr)10000);
            if (result == IntPtr.Zero)
            {
                FileWorker.LogHelper.WriteLog("布控报警成功");
            }
            else
            {
                FileWorker.LogHelper.WriteLog("布控报警失败,错误码:" + result.ToString());
            }
            Marshal.FreeHGlobal(pSourceInfo.pSources);
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
                FileWorker.LogHelper.WriteLog("获取组织结构失败，错误码：" + result.ToString());
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
                string path = Application.StartupPath + "\\alarmType.txt";
                try
                {
                    string[] alarmArr = File.ReadAllLines(path, Encoding.UTF8);
                    foreach (var item in alarmArr)
                    {
                        if (item != null && item != "")
                        {
                            string[] split = item.Split(new char[] { '=' });
                            string[] alarmMess = split[1].Split(new char[] { ',' });
                            List<string> mess = new List<string> { alarmMess[0], alarmMess[1] };
                            alarmRuleDic.Add(split[0], mess);
                        }
                    }
                }
                catch (Exception ex)
                {
                    FileWorker.LogHelper.WriteLog("解析报警类型文件失败:" + ex.Message);
                }
                //string alarmFilterRule = ConfigWorker.GetConfigValue("alarmFilterRule");
                //foreach (string filterRule in alarmFilterRule.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                //{
                //    string[] arr = filterRule.Split(new char[] { '=' });
                //    alarmRuleDic.Add(arr[0], arr[1]);
                //    //string eventCode = filterRule.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0];
                //    //string sodbCode = filterRule.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[1];
                //    //string alarmNameCode = sodbCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0];
                //    //string alarmName = sodbCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)[1];
                //    //alarmRuleDic.Add(eventCode, new Tuple<string, string>(alarmNameCode, alarmName));
                //}
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
            IntPtr result2 = DPSDK_InitExt();//初始化解码播放接口
            if (result1 == (IntPtr)0 && result2 == (IntPtr)0)
            {
                FileWorker.LogHelper.WriteLog("初始化成功");
                //MessageBox.Show("初始化成功");
            }
            else
            {
                FileWorker.LogHelper.WriteLog("初始化失败,错误码:" + result1.ToString() + "    " + result2.ToString());
                //MessageBox.Show("初始化失败");

            }
            Login_Info_t loginInfo = new Login_Info_t();
            loginInfo.szIp = videoUrl;
            loginInfo.nPort = videoPort;
            loginInfo.szUsername = videoUser;
            loginInfo.szPassword = videoPwd;
            loginInfo.nProtocol = dpsdk_protocol_version_e.DPSDK_PROTOCOL_VERSION_II;
            loginInfo.iType = 1;
            IntPtr result = DPSDK_Login(this.nPDLLHandle, ref loginInfo, (IntPtr)10000);
            if (result == (IntPtr)0)
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

        public void getAlarmState()
        {
            IntPtr result = (IntPtr)10;
            IntPtr pUser = default(IntPtr);
            result = DPSDK_SetSyncTimeOpen(nPDLLHandle, 1);//开启校时
            nFun = AlarmCallback;
            //MessageBox.Show(nPDLLHandle.ToString() + "____" + pUser.ToString());
            result = DPSDK_SetDPSDKAlarmCallback(nPDLLHandle, nFun, pUser);
            if (result == (IntPtr)0)
            {
                FileWorker.LogHelper.WriteLog("报警状态回调成功");
            }
            else
            {
                FileWorker.LogHelper.WriteLog("报警状态回调成功，错误码：" + result.ToString());
            }
        }

        private IntPtr AlarmCallback(IntPtr nPDLLHandle,
                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szAlarmId,
                    IntPtr nDeviceType,
                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szCameraId,
                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szDeviceName,
                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szChannelName,
                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szCoding,
                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szMessage,
                    Int32 nAlarmType,
                    IntPtr nEventType,
                    IntPtr nLevel,
                    Int64 nTime,
                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder pAlarmData,
                    IntPtr nAlarmDataLen,
                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder pPicData,
                    IntPtr nPicDataLen,
                   IntPtr pUserParam)
        {
            FileWorker.LogHelper.WriteLog("信息开始接收");
            
            string strAlarmType = Convert.ToString(nAlarmType);//报警类型
            string szDevName = "";
            string strname = "";
            string alarmMessage = "";
            try
            {
                byte[] bDevName = System.Text.Encoding.Default.GetBytes(szDeviceName.ToString());
                szDevName = System.Text.Encoding.UTF8.GetString(bDevName);//设备名称

                byte[] bName = System.Text.Encoding.Default.GetBytes(szChannelName.ToString());
                strname = System.Text.Encoding.UTF8.GetString(bName);//通道名称


                byte[] bMessage = System.Text.Encoding.Default.GetBytes(szMessage.ToString());
                alarmMessage = System.Text.Encoding.UTF8.GetString(bMessage);//报警信息
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("部分程序数据解析失败:" + ex.Message);
            }
            DateTime dAlarmTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dtAlarmTime = dAlarmTime.AddSeconds((UInt64)nTime);
            string uAlarmTime = dtAlarmTime.ToString("yyyy-MM-dd HH:mm:ss"); //报警时间
            
            string str = string.Format("报警Id:{0},设备类型:{1},通道Id:{2},设备名称:{3},通道名称:{4},编码:{5},报警信息:{6},报警类型:{7},发生类型:{8},报警时间(时间戳):{9},报警数据:{10},报警数据长度:{11},用户数据:{12}",
                szAlarmId.ToString(), nDeviceType.ToInt32(), szCameraId.ToString(), szDevName, strname, szCoding.ToString(), alarmMessage, strAlarmType, nEventType, uAlarmTime, pAlarmData, nAlarmDataLen, pUserParam);
            FileWorker.LogHelper.WriteLog(str);
            if (alarmRuleDic.Keys.Contains(strAlarmType))
            {
                AlarmEntity alarmEnt = AlarmParseTool.parseAlarm(alarmRuleDic[strAlarmType],uAlarmTime, szCameraId.ToString());
                string mess = alarmEnt.toJson();
                KafkaWorker.sendAlarmMessage(mess);
            }
            return (IntPtr)0;
        }

    }
}
