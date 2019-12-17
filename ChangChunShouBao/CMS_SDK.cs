using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public class CMS_SDK
{
    public delegate void CMSCALLPROC(IntPtr a, int b);
    public delegate void RCVEVTSPROC(IntPtr a, int b);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern void GetModemParameters(IntPtr handle, ref int piPort, string pPhoneNu, string pModemInitStr);


    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern void Delete_Object(IntPtr handle);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern void FetchEvent(IntPtr handle, string pEventData, ref int piEventDataLen);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern void FetchPanelInfo(IntPtr handle, int iAccountNo, int iInfoType, string pEventData, ref int piEventDataLen);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern bool FetchData(IntPtr handle);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern bool SetCurParaData(IntPtr handle, int iPanelType, string sConfigData);


    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern bool SetModemParameters(IntPtr handle, int iPort, string sPhoneNo, string sModemInitStr = null);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern bool ShowInfor(IntPtr handle, IntPtr pParam, int iWinType = 0, CMSCALLPROC lpfnProcess = null);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern bool RegistEventPort(IntPtr handle, int iEventPort, int iIntervalTime);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern bool UnregistEventPort(IntPtr handle, int iEventPort);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern bool SetEventProc(IntPtr handle, IntPtr pParam, RCVEVTSPROC lpfnProcess);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern bool SetInformationProc(IntPtr handle, IntPtr pParam, CMSCALLPROC lpfnProcess);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern IntPtr New_Object();

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern string GetParaData(IntPtr handle, ref int piPanelType);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern int ExecCmdThroughIP(IntPtr handle, int iPanelType, int iAccountNo, int iInstallCode, String pPanelIP, int iPanelPort, String pLocalIP, int iLocalPort, String sCommand, String sPara);

    [DllImport("CMS_RPS.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern int ExecCmdWith(IntPtr handle, int iPanelType, int iAccountNo, int iInstallCode, String pPanelPhoneNo, int iSerialPort, String sCommand, String sPara);
}
