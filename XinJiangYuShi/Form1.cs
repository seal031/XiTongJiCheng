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

namespace XinJiangYuShi
{
    public partial class Form1 : Form
    {   
        //登录成功返回信息
        public static LOGIN_INFO_S stLoginInfo;

        //回调函数指针
        public IMOSSDK.CALL_BACK_PROC_PF CallBackFunc;

        //临时储存实时告警
        AS_ALARMPUSH_UI_S tempAsAlarmPush = new AS_ALARMPUSH_UI_S();

        private uint EX_SUCCESS = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //注册回调函数
            RegCallBackFunc(stLoginInfo.stUserLoginIDInfo);
        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="stLoginInfo"></param>
        /// <returns></returns>
        public uint RegCallBackFunc(USER_LOGIN_ID_INFO_S stLoginInfo)
        {
            uint ulResult = 0;
            //先订阅推送
            ulResult = IMOSSDK.IMOS_SubscribePushInfo(ref stLoginInfo, (uint)SUBSCRIBE_PUSH_TYPE_E.SUBSCRIBE_PUSH_TYPE_ALL);
            if (0 != ulResult)
            {
                MessageBox.Show("启动告警接受失败。");
                return ulResult;
            }

            CallBackFunc = ProcessCallBack;
            IntPtr ptrCB = Marshal.GetFunctionPointerForDelegate(CallBackFunc);
            //注册回调
            ulResult = IMOSSDK.IMOS_RegCallBackPrcFunc(ref stLoginInfo, ptrCB);
            if (0 != ulResult)
            {
                MessageBox.Show("注册回调函数出错！");
                return ulResult;
            }
            return ulResult;
        }
        /// <summary>
        /// 回调函数处理
        /// </summary>
        /// <param name="ulProcType"></param>
        /// <param name="ptrParam"></param>
        public void ProcessCallBack(UInt32 ulProcType, IntPtr ptrParam)
        {
            try
            {
                switch (ulProcType)
                {
                    case (uint)CALL_BACK_PROC_TYPE_E.PROC_TYPE_LOGOUT:
                        LoginAgain();
                        break;
                    case (uint)CALL_BACK_PROC_TYPE_E.PROC_TYPE_ALARM:
                        AS_ALARMPUSH_UI_S stAlarmInfo = new AS_ALARMPUSH_UI_S();
                        stAlarmInfo = (AS_ALARMPUSH_UI_S)Marshal.PtrToStructure(ptrParam, typeof(AS_ALARMPUSH_UI_S));

                        //把实时告警信息赋值到临时储存
                        tempAsAlarmPush = stAlarmInfo;

                       
                        break;
                    case (uint)CALL_BACK_PROC_TYPE_E.PROC_TYPE_DEV_STATUS:
                        AS_STAPUSH_UI_S stStaPushInfo = new AS_STAPUSH_UI_S();
                        stStaPushInfo = (AS_STAPUSH_UI_S)Marshal.PtrToStructure(ptrParam, typeof(AS_STAPUSH_UI_S));

                        break;
                    default:
                        break;
                }
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 重新登录
        /// </summary>
        public void LoginAgain()
        {
            MessageBox.Show("保活失败！");
            LogoutMethod();

            //断线重连
            //stLoginInfo = LoginMethod(strUsrLoginName, strUsrLoginPsw, strSrvIpAddr, "N/A");
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        private uint LogoutMethod()
        {

            //1.注销登录
            if (null != stLoginInfo.stUserLoginIDInfo.szUserLoginCode)
            {
                IMOSSDK.IMOS_LogoutEx(ref stLoginInfo.stUserLoginIDInfo);

            }
            else
            {
                MessageBox.Show("你还没有登录!");
                Application.Exit();
            }

            return EX_SUCCESS;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="usrLoginName"></param>
        /// <param name="usrLoginPsw"></param>
        /// <param name="srvIpAddr"></param>
        /// <param name="cltIpAddr"></param>
        /// <returns></returns>
        public LOGIN_INFO_S LoginMethod(String usrLoginName, String usrLoginPsw, String srvIpAddr, String cltIpAddr)
        {
            UInt32 ulRet = 0;
            uint srvPort = 8800;

            //1.初始化
            ulRet = IMOSSDK.IMOS_Initiate("N/A", srvPort, 1, 1);
            if (0 != ulRet)
            {
                MessageBox.Show("初始化失败!" + ulRet.ToString());
            }

            //2.加密密码
            IntPtr ptr_MD_Pwd = Marshal.AllocHGlobal(sizeof(char) * IMOSSDK.IMOS_PASSWD_ENCRYPT_LEN);
            ulRet = IMOSSDK.IMOS_Encrypt(usrLoginPsw, (UInt32)usrLoginPsw.Length, ptr_MD_Pwd);

            if (0 != ulRet)
            {
                MessageBox.Show("加密密码失败!" + ulRet.ToString());
                Application.Exit();
            }

            String MD_PWD = Marshal.PtrToStringAnsi(ptr_MD_Pwd);
            Marshal.FreeHGlobal(ptr_MD_Pwd);

            //3.登录方法
            IntPtr ptrLoginInfo = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(LOGIN_INFO_S)));
            ulRet = IMOSSDK.IMOS_LoginEx(usrLoginName, MD_PWD, srvIpAddr, cltIpAddr, ptrLoginInfo);
            if (0 != ulRet)
            {
                MessageBox.Show("IMOS_Login" + ulRet.ToString());
                Application.Exit();
            }

            stLoginInfo = (LOGIN_INFO_S)Marshal.PtrToStructure(ptrLoginInfo, typeof(LOGIN_INFO_S));
            Marshal.FreeHGlobal(ptrLoginInfo);

            //4.保活
            return stLoginInfo;
        }
    }
}
