using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace XinJiangShouBaoBsCpp
{
    public class BoShi
    {
        public delegate void TRANDATAPROC(uint pObject, System.String sRcvData, int iDataLen);

        [DllImport("BS7400Ctl.dll")]
        public static extern void SetLnkIntval(System.IntPtr pObject, int iInterval);

        [DllImport("BS7400Ctl.dll")]
        public static extern System.IntPtr New_Object();
        [DllImport("BS7400Ctl.dll")]
        public static extern void Delete_Object(System.IntPtr pObject);
        [DllImport("BS7400Ctl.dll")]
        public static extern void ArrangeRcvAddress(System.IntPtr pObject, System.String pRcvAddress, System.String pCtlAddress);
        [DllImport("BS7400Ctl.dll")]
        public static extern int Execute(System.IntPtr pObject, System.String sIPAdress, System.String sCommand, System.String sPara, int iPanelType = 0);
        [DllImport("BS7400Ctl.dll")]
        public static extern void CloseReciever(System.IntPtr pObject);
        [DllImport("BS7400Ctl.dll")]
        public static extern System.UInt32 OpenReceiver(System.IntPtr pObject, TRANDATAPROC pTranFunc, System.UInt32 pPara);
        [DllImport("BS7400Ctl.dll")]
        public static extern bool CanControlPanel(System.IntPtr pObject);
        [DllImport("BS7400Ctl.dll")]
        public static extern bool CanReceiveEvent(System.IntPtr pObject);
        [DllImport("BS7400Ctl.dll")]
        public static extern bool SetPanelControlCodes(System.IntPtr pObject, System.String sPanelAddress, System.String sAgencyCode = null, System.String sPasscode = null);
    }
}
