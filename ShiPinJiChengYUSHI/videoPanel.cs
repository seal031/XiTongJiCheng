using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using IMOS_SDK_DEMO.sdk;

namespace ShiPinJiChengYUSHI
{
    public partial class videoPanel : UserControl
    {
        public USER_LOGIN_ID_INFO_S stUserLoginIDInfo;//用户登录信息
        public string channelCode;
        public byte[] CameraCode;

        public videoPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 播放实时视频
        /// </summary>
        /// <param name="CameraCode"></param>
        /// <returns></returns>
        public UInt32 StartLive(byte[] CameraCode)
        {

            UInt32 ulRet = 0;
            //此处用于拿到不同用户信息
            //selectedSubCtrl = m_player.m_mainForm.g_userCtrlList[m_player.m_mainForm.tabControl1.SelectedIndex];
            //selectedPanel = selectedSubCtrl.imosPlayer.m_playerUnit[PlayerPanel.SelectedIndex];

            this.CameraCode = CameraCode;
            String str1 = Encoding.UTF8.GetString(CameraCode);
            //若已经获取ChannelCode则不重新获取
            if (null == channelCode)
            {
                IntPtr channelCodeIntPtr = new IntPtr();
                channelCodeIntPtr = Marshal.AllocHGlobal(25 * Marshal.SizeOf(typeof(PLAY_WND_INFO_S)));
                IMOSSDK.IMOS_GetChannelCode(ref stUserLoginIDInfo, channelCodeIntPtr);

                //将通道号和选中窗格绑定
                // 若调试发现 channelCode为乱码，则在之前没有调用 IMOS_StartPlayer接口
                channelCode = Marshal.PtrToStringAnsi(channelCodeIntPtr);
                Marshal.FreeHGlobal(channelCodeIntPtr);
            }

            //此处绑定每个不同用户的窗口句柄
            //IntPtr ptrHwnd = new IntPtr();
            //selectedSubCtrl.imosPlayer.GetHwnd(PlayerPanel.SelectedIndex, ref ptrHwnd);
            ulRet = IMOSSDK.IMOS_SetPlayWnd(ref stUserLoginIDInfo, Encoding.Default.GetBytes(channelCode), this.panel1.Handle);
            //_parseVideoCallBackFunc = ParseVideoProcessCallBack;
            //IntPtr ptrCB = Marshal.GetFunctionPointerForDelegate(_parseVideoCallBackFunc);
            //ulRet = IMOSSDK.IMOS_SetParseVideoDataCB(ref selectedSubCtrl.sdkManager.stLoginInfo.stUserLoginIDInfo, IMOSSDK.UnicodeToUTF8(selectedPanel.channelCode), ptrCB, true, 0);
            
            ulRet = IMOSSDK.IMOS_StartMonitor(ref stUserLoginIDInfo, CameraCode, Encoding.Default.GetBytes(channelCode), 1, 0);
            
            return ulRet;
        }
    }
}
