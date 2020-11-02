using System;
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
                    getDeviceInfo();
                    getDeviceState();
                }
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
        public void getDeviceInfo()
        {
            IntPtr result = DPSDK_LoadDGroupInfo(nPDLLHandle, ref nGroupLen, (IntPtr)60000);
            if (result == (IntPtr)0)
            {
                FileWorker.LogHelper.WriteLog("加载组织结构成功");
            }
            else
            {
                FileWorker.LogHelper.WriteLog("加载组织结构失败，错误码：" + result.ToString());
            }
            byte[] szGroupStr = new byte[(int)nGroupLen + 1];
            result = DPSDK_GetDGroupStr(nPDLLHandle, szGroupStr, nGroupLen, (IntPtr)10000);
            if (result == IntPtr.Zero)
            {
                string strXML = System.Text.Encoding.UTF8.GetString(szGroupStr);
                //FileWorker.LogHelper.WriteLog(strXML);
                try
                {
                    var doc = new XmlDocument();
                    strXML = strXML.Trim('\0');
                    doc.LoadXml(strXML);
                    XmlNodeList xmlNodeList = doc.GetElementsByTagName("Channel");//获取节点
                    List<String> channList = new List<string>();
                    string pattern = "[\\[ \\] \\^ \\-_*×――(^)%~!@#…&%￥—+<>《》!！??？:：•`·、。，；,;\"‘’“”-]";
                    for (int i = 0; i < xmlNodeList.Count; i++)
                    {
                        string[] devArr = Regex.Replace(xmlNodeList[i].OuterXml, pattern, "").Split(new char[] { '=' });
                        string channID = devArr[1].Substring(0, 7);
                        if (xmlNodeList[i].OuterXml.Length > 40 && channID != "1000016" && channID != "1000017")
                        {
                            channList.Add(xmlNodeList[i].OuterXml);
                        }
                    }
                    for (int j = 0; j < channList.Count; j++)
                    {
                        string[] strArr = channList[j].Split(new char[] { ' ' });
                        string channType = Regex.Replace(strArr[5], pattern, "").Split(new char[] { '=' })[1];
                        string code = Regex.Replace(strArr[8], pattern, "").Split(new char[] { '=' })[1];
                        int cameraType = int.Parse(code);
                        if (channType == "1" && cameraType <= 4)
                        {
                            //DeviceResourEntity devResEnt = DeviceResourceParseTool.parseDeviceRec(strArr);
                            //string jsonMess = devResEnt.toJson();
                            //KafkaWorker.sendDeviceMessage(jsonMess, "基本信息");
                        }
                    }

                }
                catch (Exception ex)
                {
                    FileWorker.LogHelper.WriteLog("解析XML数据失败：" + ex.Message);
                }
            }
            else
            {
                FileWorker.LogHelper.WriteLog("获取组织树XML失败，错误码：" + result.ToString());
            }
        }

        public void getDeviceState()
        {
            IntPtr result = (IntPtr)10;
            IntPtr pUser = default(IntPtr);
            fDevStatus = DevStatusCallback;
            //开启设备状态上报监听
            result = DPSDK_SetDPSDKDeviceStatusCallback(nPDLLHandle, fDevStatus, pUser);
            if (result == (IntPtr)0)
            {
                FileWorker.LogHelper.WriteLog("设置设备状态回调成功");
            }
            else
            {
                FileWorker.LogHelper.WriteLog("设置设备状态回调失败，错误码：" + result.ToString());
                MessageBox.Show("设置设备状态回调失败，错误码：" + result.ToString());
            }
            fNVRChnlStatus = NvrStatusCallback;
            result = DPSDK_SetDPSDKNVRChnlStatusCallback(nPDLLHandle, fNVRChnlStatus, pUser);
            if (result == (IntPtr)0)
            {
                //MessageBox.Show("设置设备状态回调成功");
                FileWorker.LogHelper.WriteLog("设置Nvr通道状态回调成功");
            }
            else
            {
                //return
                //MessageBox.Show("设置设备状态回调失败，错误码：" + result.ToString());
                FileWorker.LogHelper.WriteLog("设置设备状态回调失败，错误码：" + result.ToString());
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nPDLLHandle">SDK句柄</param>
        /// <param name="szDeviceId">设备Id</param>
        /// <param name="nStatus">状态  1在线  2离线</param>
        /// <param name="pUserParam">用户数据</param>
        public static IntPtr DevStatusCallback(IntPtr nPDLLHandle, [MarshalAs(UnmanagedType.LPStr)] StringBuilder szDeviceId, IntPtr nStatus, IntPtr pUserParam)
        {
            String status = "未知";
            if (nStatus == (IntPtr)1)
            {
                status = "上线";
            }
            else if (nStatus == (IntPtr)2)
            {
                status = "离线";
            }
            string mess = string.Format("设备ID:{0}  状态:{1}", szDeviceId.ToString(), status);
            //MessageBox.Show("设备ID：" + szDeviceId.ToString() + "  状态：" + status);
            return (IntPtr)0;
        }

        public static IntPtr NvrStatusCallback(IntPtr nPDLLHandle,
                                 [MarshalAs(UnmanagedType.LPStr)] StringBuilder szChnlId,
                                 IntPtr nStatus,
                                 IntPtr pUserParam)
        {
            String status = "未知";
            if (nStatus == (IntPtr)1)
            {
                status = "上线";
            }
            else
            {
                status = "离线";
            }
            string str = "NvrStatusCallback ChannelId:" + szChnlId.ToString() + "  nStatus:" + status;
            //DeviceStateEntity devEnt = DeviceStateParseTool.parseDeviceState(szChnlId.ToString(), status);
            //string jsonMess = devEnt.toJson();
            //KafkaWorker.sendDeviceMessage(jsonMess, "状态");
            return (IntPtr)0;
        }
    }
}
