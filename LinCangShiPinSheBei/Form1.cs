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
using static LinCangShiPinSheBei.SDK;

namespace LinCangShiPinSheBei
{
    //172.20.151.11   9000  system system123
    public partial class Form1 : Form
    {
        public IntPtr nPDLLHandle = (IntPtr)0;
        public IntPtr nGroupLen = IntPtr.Zero;
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
                videoPort=uint.Parse(ConfigWorker.GetConfigValue("videoPort"));
                videoUser = ConfigWorker.GetConfigValue("videoUser");
                videoPwd = ConfigWorker.GetConfigValue("videoPwd");
                return true;
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("读取本地配置文件出现异常"+ex.Message);
                return false;
            }
        }

        public bool login()
        {
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
            IntPtr result = SDK.DPSDK_LoadDGroupInfo(nPDLLHandle, ref nGroupLen, (IntPtr)60000);
            if (result == (IntPtr)0)
            {
                FileWorker.LogHelper.WriteLog("加载组织结构成功");
            }
            else
            {
                FileWorker.LogHelper.WriteLog("加载组织结构失败，错误码：" + result.ToString());
                MessageBox.Show("加载组织结构失败，错误码：" + result.ToString());
            }
            byte[] szGroupStr = new byte[(int)nGroupLen + 1];
            result = SDK.DPSDK_GetDGroupStr(nPDLLHandle, szGroupStr, nGroupLen, (IntPtr)10000);
            if (result == IntPtr.Zero)
            {
                string strXML = System.Text.Encoding.UTF8.GetString(szGroupStr);
                FileWorker.LogHelper.WriteLog(strXML);
            }
            else
            {
                FileWorker.LogHelper.WriteLog("获取组织树XML失败，错误码：" + result.ToString());
                MessageBox.Show("获取组织树XML失败，错误码：" + result.ToString());
            }
        }

        public void getDeviceState()
        {
            IntPtr result = (IntPtr)10;
            IntPtr pUser = default(IntPtr);
            fDevStatus = DevStatusCallback;
            //开启设备状态上报监听
            result = SDK.DPSDK_SetDPSDKDeviceStatusCallback(nPDLLHandle, fDevStatus, pUser);
            if (result == (IntPtr)0)
            {
                FileWorker.LogHelper.WriteLog("设置设备状态回调成功");
            }
            else
            {
                FileWorker.LogHelper.WriteLog("设置设备状态回调失败，错误码：" + result.ToString());
                MessageBox.Show("设置设备状态回调失败，错误码：" + result.ToString());
            }
        }

        /// 设备状态回调函数委托。
        ///@param   								nPDLLHandle				SDK句柄
        ///@param   								szDeviceId              设备Id
        ///@param   								nStatus		            状态  1在线  2离线
        ///@param   								pUserParam              用户数据
        ///@remark									
        /// </summary>
        public delegate IntPtr fDPSDKDevStatusCallback(IntPtr nPDLLHandle,[MarshalAs(UnmanagedType.LPStr)] StringBuilder szDeviceId,IntPtr nStatus,IntPtr pUserParam);
        private static SDK.fDPSDKDevStatusCallback fDevStatus;
        /// <summary>
        /// 设备状态回调函数
        /// </summary>
        /// <param name="nPDLLHandle"></param>
        /// <param name="szDeviceId"></param>
        /// <param name="nStatus"></param>
        /// <param name="pUserParam"></param>
        /// <returns></returns>
        public static IntPtr DevStatusCallback(IntPtr nPDLLHandle, [MarshalAs(UnmanagedType.LPStr)] StringBuilder szDeviceId, IntPtr nStatus, IntPtr pUserParam)
        {
            // String status = "未知";
            if (nStatus == (IntPtr)1)
            {
                // status = "上线";
            }
            else if (nStatus == (IntPtr)2)
            {
                // status = "离线";
            }
            //MessageBox.Show("设备ID：" + szDeviceId.ToString() + "  状态：" + status);
            return (IntPtr)0;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
