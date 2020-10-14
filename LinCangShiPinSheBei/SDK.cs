using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LinCangShiPinSheBei
{
    public partial class Form1
    {
        /// <summary>
        /// 报警回调函数。
        ///@param   								nPDLLHandle				SDK句柄
        ///@param   								szAlarmId               报警Id
        ///@param   								nDeviceType             设备类型
        ///@param   								szCameraId              通道Id
        ///@param   								szDeviceName            设备名称
        ///@param   								szChannelName           通道名称
        ///@param   								szCoding                编码
        ///@param   								szMessage               报警信息
        ///@param   								nAlarmType              报警类型，参考dpsdk_alarm_type_e
        ///@param   								nEventType              报警发生类型，参考dpsdk_event_type_e
        ///@param   								nLevel                  报警等级
        ///@param   								nTime                   报警时间
        ///@param   								pAlarmData              报警数据
        ///@param   								nAlarmDataLen           报警数据长度
        ///@param   								pPicData                图片数据
        ///@param   								nPicDataLen             图片数据长度
        ///@param   								pUserParam              用户数据
        ///@remark									
        /// </summary>
        public delegate IntPtr fDPSDKAlarmCallback(IntPtr nPDLLHandle,
                                                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szAlarmId,
                                                    IntPtr nDeviceType,
                                                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szCameraId,
                                                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szDeviceName,
                                                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szChannelName,
                                                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szCoding,
                                                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szMessage,
                                                    IntPtr nAlarmType,
                                                    IntPtr nEventType,
                                                    IntPtr nLevel,
                                                    Int64 nTime,
                                                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder pAlarmData,
                                                    IntPtr nAlarmDataLen,
                                                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder pPicData,
                                                    IntPtr nPicDataLen,
                                                    IntPtr pUserParam);
        public static fDPSDKAlarmCallback nFun;

        /// 设备状态回调函数。
        ///@param   								nPDLLHandle				SDK句柄
        ///@param   								szDeviceId              设备Id
        ///@param   								nStatus		            状态  1在线  2离线
        ///@param   								pUserParam              用户数据
        ///@remark									
        /// </summary>
        public delegate IntPtr fDPSDKDevStatusCallback(IntPtr nPDLLHandle,
                                                   [MarshalAs(UnmanagedType.LPStr)] StringBuilder szDeviceId,
                                                   IntPtr nStatus,
                                                   IntPtr pUserParam);
        public static fDPSDKDevStatusCallback fDevStatus;

        /** 通道状态回调.
         ///@param   								nPDLLHandle				SDK句柄
        ///@param   								szChnlId                通道Id
        ///@param   								nStatus		            状态  1在线  0离线
        ///@param   								pUserParam              用户数据
        ///@remark				
        */
        public delegate IntPtr fDPSDKNVRChnlStatusCallback(IntPtr nPDLLHandle,
                                                            [MarshalAs(UnmanagedType.LPStr)] StringBuilder szChnlId,
                                                            IntPtr nStatus,
                                                            IntPtr pUserParam);
        public static fDPSDKNVRChnlStatusCallback fNVRChnlStatus;

        /** Json传输协议回调
	        @param szJson	Json字符串
        */
        public delegate IntPtr fDPSDKGeneralJsonTransportCallback(IntPtr nPDLLHandle, [MarshalAs(UnmanagedType.LPStr)] StringBuilder szJson, IntPtr pUserParam);
        public static fDPSDKGeneralJsonTransportCallback fJsonCallback;

        /// Json传输协议回调。
        ///@param   								nPDLLHandle				SDK句柄
        ///@param                                   szJson	                Json字符串
        ///@param                                   JsonLen	                Json字符串长度
        ///@param   								pUserParam              用户数据
        ///@remark									
        /// </summary>
        public delegate IntPtr fDPSDKGeneralJsonTransportCallbackEx(IntPtr nPDLLHandle,
                                                                    IntPtr ptrJson,
                                                                    int JsonLen,
                                                                    IntPtr pUserParam);

        public static fDPSDKGeneralJsonTransportCallbackEx fJsonCallbackEx;

        //违章报警回调
        public delegate IntPtr fDPSDKTrafficAlarmCallback(IntPtr nPDLLHandle, ref Traffic_Alarm_Info_t pRetInfo, IntPtr pUserParam);

        public static fDPSDKTrafficAlarmCallback fTrafficAlarmCallback;

        //卡口过车信息回调
        public delegate IntPtr fDPSDKGetBayCarInfoCallbackEx(IntPtr nPDLLHandle,
                                                            IntPtr szDeviceId,
                                                            int nDeviceIdLen,
                                                            int nDevChnId,
                                                            IntPtr szChannelId,
                                                            int nChannelIdLen,
                                                            IntPtr szDeviceName,
                                                            int nDeviceNameLen,
                                                            IntPtr szDeviceChnName,
                                                            int nChanNameLen,
                                                            IntPtr szCarNum,
                                                            int nCarNumLen,
                                                            int nCarNumType,
                                                            int nCarNumColor,
                                                            int nCarSpeed,
                                                            int nCarType,
                                                            int nCarColor,
                                                            int nCarLen,
                                                            int nCarDirect,
                                                            int nWayId,
                                                            UInt64 lCaptureTime,
                                                            UInt32 lPicGroupStoreID,
                                                            int nIsNeedStore,
                                                            int nIsStoraged,
                                                            IntPtr szCaptureOrg,
                                                            int nCaptureOrgLen,
                                                            IntPtr szOptOrg,
                                                            int nOptOrgLen,
                                                            IntPtr szOptUser,
                                                            int nOptUserLen,
                                                            IntPtr szOptNote,
                                                            int nOptNoteLen,
                                                            IntPtr szImg0Path,
                                                            int nImg0PathLen,
                                                            IntPtr szImg1Path,
                                                            int nImg1PathLen,
                                                            IntPtr szImg2Path,
                                                            int nImg2PathLen,
                                                            IntPtr szImg3Path,
                                                            int nImg3PathLen,
                                                            IntPtr szImg4Path,
                                                            int nImg4PathLen,
                                                            IntPtr szImg5Path,
                                                            int nImg5PathLen,
                                                            IntPtr szImgPlatePath,
                                                            int nImgPlatePathLen,
                                                            int icarLog,
                                                            int iPlateLeft,
                                                            int iPlateRight,
                                                            int iPlateTop,
                                                            int iPlateBottom,
                                                            IntPtr pUserParam);
        public static fDPSDKGetBayCarInfoCallbackEx fBayCarInfoEx;

        public delegate IntPtr fDPSDKGetBayCarInfoCallback(IntPtr nPDLLHandle, ref Bay_Car_Info_t pRetInfo, IntPtr pUserParam);
        public static fDPSDKGetBayCarInfoCallback fBayCarInfo;

        /** 流量上报回调函数定义
         @param	  IN	                                nPDLLHandle			     SDK句柄
         @param	  IN	                                pRetInfo				 对应请求时返回的session
         @param	  IN	                                pUserParam				 用户指针参数,由对应的DPSDK_SetxxxxCallBack传入
        */
        public delegate IntPtr fDPSDKGetTrafficFlowCallback(IntPtr nPDLLHandle, ref TrafficFlow_Info_t pRetInfo, IntPtr pUserParam);
        public static fDPSDKGetTrafficFlowCallback fTrafficFlowInfo;

        /** 车道流量状态上报回调函数定义
            @param IN                                   nPDLLHandle              SDK句柄
            @param IN                                   pRetInfo                 对应请求时返回的流量信息
            @param IN                                   pUserParam               用户指针参数,由对应的DPSDK_SetxxxxCallBack传入
        */
        public delegate IntPtr fDPSDKGetDevTrafficFlowCallback(IntPtr nPDLLHandle, ref DevTrafficFlow_Info_t pRetInfo, IntPtr pUserParam);
        public static fDPSDKGetDevTrafficFlowCallback fDevTrafficFlowInfo;

        /// 门禁状态上报接收回调函数。
        ///@param   								nPDLLHandle				SDK句柄
        ///@param   								szCameraId              通道Id
        ///@param                                   nStatus                 门状态
        ///@param   								nTime                   上报时间
        ///@param   								pUserParam              用户数据
        ///@remark									
        public delegate IntPtr fDPSDKPecDoorStarusCallBack(IntPtr nPDLLHandle,
                                                    [MarshalAs(UnmanagedType.LPStr)] StringBuilder szCameraId,
                                                    dpsdk_door_status_e nStatus,
                                                    Int64 nTime,
                                                    IntPtr pUserParam);
        public static fDPSDKPecDoorStarusCallBack fPecDoorStatus;

        /** 卡口布控告警上报回调函数定义
         @param	  IN	                                nPDLLHandle		       SDK句柄
         @param	  IN	                                pRetInfo				 对应请求时返回的session
         @param	  IN	                                pUserParam			 用户指针参数,由对应的DPSDK_SetxxxxCallBack传入
        */
        public delegate IntPtr fDPSDKBayWantedAlarmCallback(IntPtr nPDLLHandle, ref Bay_WantedAlarm_Info_t pRetInfo, IntPtr pUserParam);
        public static fDPSDKBayWantedAlarmCallback fBayWantedAlarm;

        /** 区间测速上报回调函数定义
            @param IN                                    nPDLLHandle              SDK句柄
            @param IN                                    pRetInfo                 对应请求时返回的流量信息
            @param IN                                    pUserParam               用户指针参数,由对应的DPSDK_SetxxxxCallBack传入
        */
        public delegate IntPtr fDPSDKGetAreaSpeedDetectCallback(IntPtr nPDLLHandle, ref Area_Detect_Info_t pRetInfo, IntPtr pUserParam);
        public static fDPSDKGetAreaSpeedDetectCallback fGetAreaSpeedDetect;


        /** 视频报警主机布撤防/旁路状态回调
         @param	  IN	                                nPDLLHandle		    SDK句柄
         @param	  IN	                                szDeviceId			设备Id
         @param	  IN	                                nChannelNO			通道号，设备操作通道号是-1
         @param	  IN	                                nStatus				状态，参考dpsdk_videoalarmhost_status_type_e
         @param	  IN	                                pUserParam			用户指针参数,由对应的DPSDK_SetxxxxCallBack传入
         视频报警主机在客户端登陆的时候DMS会上报状态，客户端操作会通知其他客户端。
        */
        public delegate IntPtr fDPSDKVideoAlarmHostStatusCallback(IntPtr nPDLLHandle,/*IN char* */
            [MarshalAs(UnmanagedType.LPStr)] StringBuilder szDeviceId, Int32 nChannelNO, Int32 nStatus, IntPtr pUserParam);
        public static fDPSDKVideoAlarmHostStatusCallback fVideoAlarmHostStatus;

        /** 网络报警主机布撤防/旁路状态回调
         @param	  IN	                                nPDLLHandle		    SDK句柄
         @param	  IN	                                szDeviceId			布撤防时是设备Id;旁路/取消旁路时是通道id
         @param	  IN	                                nRType				上报类型，参考dpsdk_netalarmhost_report_type_e，1布防，2旁路
         @param	  IN	                                nOperType			操作类型，参考dpsdk_netalarmhost_operator_e，1设备操作，2通道操作
         @param	  IN	                                nStatus				状态，对于防区状态1布防2撤防；对于旁路状态1旁路2取消旁路
         @param	  IN	                                pUserParam			用户指针参数,由对应的DPSDK_SetxxxxCallBack传入
         网络报警主机状态要自己查询，客户端操作会通知其他客户端。
        */
        public delegate IntPtr fDPSDKNetAlarmHostStatusCallback(IntPtr nPDLLHandle,/* IN char* */
           [MarshalAs(UnmanagedType.LPStr)] StringBuilder szDeviceId, Int32 nRType, Int32 nOperType, Int32 nStatus, IntPtr pUserParam);
        public static fDPSDKNetAlarmHostStatusCallback fNetAlarmHostStatus;

        // 电视墙屏窗口设置视频源
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [DllImport("user32.dll")]
        public extern static int GetWindowRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_Create(dpsdk_sdk_type_e nType, ref IntPtr nPDLLHandle);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        private extern static IntPtr DPSDK_Destroy(IntPtr nPDLLHandle);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        private extern static IntPtr DPSDK_SetLog(IntPtr nPDLLHandle, dpsdk_log_level_e nLevel, [MarshalAs(UnmanagedType.LPStr)] string szFilename, Boolean bScreen, Boolean bDebugger);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        private extern static IntPtr DPSDK_StartMonitor(IntPtr nPDLLHandle, [MarshalAs(UnmanagedType.LPStr)] string szFilename);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_Login(IntPtr nPDLLHandle, ref Login_Info_t pLoginInfo, IntPtr nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_Logout(IntPtr nPDLLHandle, IntPtr nTimeout);
        //////////////////////////////////////////////////////////////////////////
        //逻辑组织树start

        /** 是否有业务树
         @param   IN	nPDLLHandle		SDK句柄
         @return  函数返回true表示含有业务树，否则表示没有业务树
         @remark
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_HasLogicOrg(IntPtr nPDLLHandle);

        /** 获取业务树根节点信息.
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	pDepInfoEx		业务树根节点信息
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetLogicRootDepInfo(IntPtr nPDLLHandle, ref Dep_Info_Ex_t pDepInfoEx);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_LoadDGroupInfo(IntPtr nPDLLHandle, ref IntPtr nGroupLen, IntPtr nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        //public extern static IntPtr DPSDK_GetDGroupStr(IntPtr nPDLLHandle, ref byte szGroupStr, IntPtr nGroupLen, IntPtr nTimeout);
        public extern static IntPtr DPSDK_GetDGroupStr(IntPtr nPDLLHandle, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] szGroupStr, IntPtr nGroupLen, IntPtr nTimeout);

        /**获取最新GPS信息XML串长度.
        @param		IN		nPDLLHandle				SDK句柄
        @param		OUT		nGpsXMLLen				GPS XML的长度
        @param		IN		nTimeout				超时时间
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark 
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_AskForLastGpsStatusXMLStrCount(IntPtr nPDLLHandle, ref Int32 nCount, IntPtr nTimeout);

        /**获取最新GPS信息.
        @param		IN		nPDLLHandle				SDK句柄
        @param		OUT		LastGpsIStatus			GPS XML数据
        @param		IN		nCount					GPS XML的长度,DPSDK_AskForLastGpsStatusXMLStrCount的输出参数值
        @param		IN		nTimeout				超时时间
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark 
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_AskForLastGpsStatusXMLStr(IntPtr nPDLLHandle, ref byte LastGpsIStatus, Int32 nCount);

        /**获取最新GPS信息.
         @param		<IN>		nPDLLHandle				SDK句柄
         @param		<OUT>		LastGpsIStatus			最新GPS信息需要上层分配好空间。
         @param		<IN>		nTimeout				超时时间
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark 
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_AskForLastGpsStatus(IntPtr nPDLLHandle, ref byte LastGpsIStatus, IntPtr nTimeout);


        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetDGroupCount(IntPtr nPDLLHandle, ref Get_Dep_Count_Info_t pGetInfo);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetDGroupInfoEx(IntPtr nPDLLHandle, ref Get_Dep_Info_Ex_t pGetInfo);

        /** 获取设备下编码器通道的信息(扩展).
         @param   IN	nPDLLHandle		SDK句柄
         @param   INOUT	pGetInfo		子通道信息
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark
         1、pEncChannelnfo需要在外面创建好
         2、pEncChannelnfo的大小与DPSDK_GetDGroupInfo中通道个数返回需要一致
         */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetChannelInfoEx(IntPtr nPDLLHandle, ref Get_Channel_Info_Ex_t pGetInfo);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetCameraIdbyDevInfo(IntPtr nPDLLHandle, byte[] szDevIp, int nPort, int nChnlNum, ref byte szCameraId, dpsdk_dev_unit_type_e nUnitType);

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_InitExt();

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_UnitExt();

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartRealplay(IntPtr nPDLLHandle, out IntPtr nRealSeq, ref Get_RealStream_Info_t pGetInfo, IntPtr hwnd, IntPtr nTimeout);

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopRealplayBySeq(IntPtr nPDLLHandle, IntPtr nRealSeq, IntPtr nTimeout);

        /** 获取当前回放码流播放时间.
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nPlaybackSeq	回放请求序号 
         @param   IN	nFramTime	    返回的时间值
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetFrameTime(IntPtr nPDLLHandle, IntPtr nPlaybackSeq, out UInt64 nFramTime);

        /** 获取本地录像当前播放进度.
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nPlaybackSeq	回放请求序号 
         @param   IN	nPos	        播放进度
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetPlayPos(IntPtr nPDLLHandle, IntPtr nPlaybackSeq, out Int32 nPos);

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PicCapture(IntPtr nPDLLHandle, IntPtr nSeq, dpsdk_pic_type_e nPicType, [MarshalAs(UnmanagedType.LPStr)] string szFilename);

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetOsdTxt(IntPtr nPDLLHandle, IntPtr nSeq, [MarshalAs(UnmanagedType.LPStr)] string szOsdInfo);

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_CleanUpOsdInfo(IntPtr nPDLLHandle, IntPtr nSeq);


        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetIvsShowFlag(IntPtr nPDLLHandle, IntPtr nRealSeq, IvsInfoVisible nType, IntPtr nFlag);

        /** 获取视频属性
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nSeq   			请求序号 
         @param   OUT	videoColorInfo	视频属性
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetColor(IntPtr nPDLLHandle, IntPtr nSeq, ref Video_Color_Info_t videoColorInfo);

        /** 调整视频属性
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nSeq   			请求序号 
         @param   IN	videoColorInfo	视频属性
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_AdjustColor(IntPtr nPDLLHandle, IntPtr nSeq, Video_Color_Info_t videoColorInfo);

        /** 实时录制视频
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nSeq   			请求序号 
         @param   IN	nDataType		*- 0 表示原始视频流;  *- 1 表示转换成AVI格式, 只对大华码流有效  *- 2 表示转换成ASF格式, 只对大华码流有效
         @param   IN	szFilename		文件名
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartRecord(IntPtr nPDLLHandle, IntPtr nSeq, [MarshalAs(UnmanagedType.LPStr)] string szFilename, IntPtr nDataType);

        /** 实时录制视频录像为FLV格式
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nSeq   			请求序号 
         @param   IN	szFilename		文件名
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartRecordToFLV(IntPtr nPDLLHandle, IntPtr nSeq, [MarshalAs(UnmanagedType.LPStr)] string szFilename);

        /** 实时录制视频可定制录像文件格式
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nSeq   			请求序号 
         @param   IN	nFileType		录像文件格式
         @param   IN	szFilename		文件名

         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartRecordEx(IntPtr nPDLLHandle, IntPtr nSeq, [MarshalAs(UnmanagedType.LPStr)] string szFilename, IntPtr nFileType);

        /** 停止实时录制视频
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nSeq   			请求序号 
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopRecord(IntPtr nPDLLHandle, IntPtr nSeq);

        /** 停止实时录制视频
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nSeq   			请求序号 
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopRecordToFLV(IntPtr nPDLLHandle, IntPtr nSeq);

        /** 停止实时录制视频
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nSeq   			请求序号 
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopRecordEx(IntPtr nPDLLHandle, IntPtr nSeq);

        /**
        获取声道状态
        @param   IN	  nPDLLHandle	SDK句柄
        @param   IN	  nSeq        	播放序列号
        @param   IN	  nChannel   	声道序号
        @param   IN   bOpen         是否打开
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetAudioChannelState(IntPtr nPDLLHandle, IntPtr nSeq, IntPtr nChannel, ref bool bOpen);

        /**
        获取声道数量
        @param   IN	  nPDLLHandle	SDK句柄
        @param   IN	  nSeq        	播放序列号
        @param   IN	  nChannelNum	声道数
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetAudioChannels(IntPtr nPDLLHandle, IntPtr nSeq, out int nChannelNum);

        /**
        设置某个音频声道打开或关闭
        @param   IN	  nPDLLHandle	SDK句柄
        @param   IN	  nSeq        	播放序列号
        @param   IN	  nChannel  	声道序号
        @param   IN   bOpen         是否打开
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetAudioChannel(IntPtr nPDLLHanle, IntPtr nSeq, IntPtr nChannel, bool bOpen);

        /** 打开/关闭视频语音.
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nRealSeq		码流请求序号,可作为后续操作标识 
         @param   IN	bOpen		    true打开语音，false关闭语音
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_OpenAudio(IntPtr nPDLLHandle, IntPtr nSeq, bool bOpen);

        /** 以共享方式打开/关闭声音，只管播放本路声音而不去关闭其他路的声音.
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nRealSeq		码流请求序号,可作为后续操作标识 
         @param   IN	bOpen		    true打开语音，false关闭语音
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_OpenAudioShare(IntPtr nPDLLHandle, IntPtr nSeq, bool bOpen);

        /** 获取音量
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nRealSeq		码流请求序号,可作为后续操作标识 
         @param   OUT	nVol		    音量大小,范围[0,0xFFFF]
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetVolume(IntPtr nPDLLHandle, IntPtr nSeq, out int nVol);

        /** 调节音量
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nRealSeq		码流请求序号,可作为后续操作标识 
         @param   IN	nVol		    音量大小,范围[0,0xFFFF]
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetVolume(IntPtr nPDLLHandle, IntPtr nSeq, IntPtr nVol);

        /**单帧回放
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nSeq   			请求序号 
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PlayOneByOne(IntPtr nPDLLHandle, IntPtr nSeq);

        /**单帧后退
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nSeq   			请求序号 
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PlayOneByOneBack(IntPtr nPDLLHandle, IntPtr nSeq);

        /**恢复正常播放
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nSeq   			请求序号 
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_Play(IntPtr nPDLLHandle, IntPtr nSeq);

        /** 录像打标.
         @param   IN	nPDLLHandle		SDK句柄
         @param   INOUT	pTagInfo		录像打标信息
         @param	  IN	nOpType			打标操作，1：新增，2：修改，3：删除
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_OperatorTagInfo(IntPtr nPDLLHandle, ref Tag_Info_t pTagInfo, dpsdk_operator_type_e nOpType, IntPtr nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static int DPSDK_QueryRecord(IntPtr nPDLLHandle, ref Query_Record_Info_t pQueryInfo, out int nRecordCount, IntPtr nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static int DPSDK_GetRecordInfo(IntPtr nPDLLHandle, ref Record_Info_t pRecords);

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartPlaybackByTime(IntPtr nPDLLHandle, out IntPtr nPlaybackSeq, ref Get_RecordStream_Time_Info_t pGetInfo, IntPtr hwnd, IntPtr nTimeout);

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartPlaybackByStreamType(IntPtr nPDLLHandle, out IntPtr nPlaybackSeq, ref Get_RecordStream_Time_Info_t pGetInfo, dpsdk_stream_type_e nStreamType, IntPtr hwnd, IntPtr nTimeout, IntPtr nTranMode, byte bBack);


        /** 开启平台录像
        @param   IN    nPDLLHandle     SDK句柄
        @param   IN    szCameraId      通道ID
        @param   IN    streamType      实时码流类型
        @param   IN    nTimeout        超时时长，单位毫秒
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartPlatformReocrd(IntPtr nPDLLHandle, byte[] szCameraId, dpsdk_encdev_stream_e streamType, IntPtr nTimeout);

        /** 停止平台录像
        @param   IN    nPDLLHandle     SDK句柄
        @param   IN    szCameraId      通道ID
        @param   IN    nTimeout        超时时长，单位毫秒
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopPlatformReocrd(IntPtr nPDLLHandle, byte[] szCameraId, IntPtr nTimeout);

        /** 按文件请求录像流.
         @param	  IN	nPDLLHandle		SDK句柄
         @param   OUT	nPlaybackSeq	回放请求序号,作为后续操作标识  
         @param	  IN	pRecordInfo		录像信息 
         @param	  IN	hDestWnd		显示窗口句柄
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartPlaybackByFile(IntPtr nPDLLHandle, out IntPtr nPlaybackSeq, ref Get_RecordStream_File_Info_t pRecordInfo, IntPtr hDestWnd, IntPtr nTimeout);

        //本地dav文件回放
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartPlaybackByLocal(IntPtr nPDLLHandle, out IntPtr nPlaybackSeq, ref Get_Record_Local_Info_t pRecordInfo, IntPtr hwnd);

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetPlaybackSpeed(IntPtr nPDLLHandle, IntPtr nPlaybackSeq, dpsdk_playback_speed_e nSpeed, IntPtr nTimeout);

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PausePlaybackBySeq(IntPtr nPDLLHandle, IntPtr nPlaybackSeq, IntPtr nTimeout);

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ResumePlaybackBySeq(IntPtr nPDLLHandle, IntPtr nPlaybackSeq, IntPtr nTimeout);

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopPlaybackBySeq(IntPtr nPDLLHandle, IntPtr nPlaybackSeq, IntPtr nTimeout);

        //按文件下载录像，存储成dav格式
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_DownloadRecordByFile(IntPtr nPDLLHandle, out int nPlaybackSeq, byte[] szCameraId, ref Single_Record_Info_t pSingleRecord, byte[] szFileName, IntPtr nTimeout);

        //按文件下载录像，存储成其他格式
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_DownloadRecordByFileEx(IntPtr nPDLLHandle, out int nPlaybackSeq, byte[] szCameraId, ref Single_Record_Info_t pSingleRecord, byte[] szFileName, file_type_e nFileType, IntPtr nTimeout);


        //按时间下载录像，存储成dav格式
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_DownloadRecordByTime(IntPtr nPDLLHandle, out int nPlaybackSeq, byte[] szCameraId, dpsdk_recsource_type_e nSourceType, UInt64 uBeginTime, UInt64 uEndTime, byte[] szFileName, IntPtr nTimeout);

        //按时间下载录像，存储成其他格式
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_DownloadRecordByTimeEx(IntPtr nPDLLHandle, out int nPlaybackSeq, byte[] szCameraId, dpsdk_recsource_type_e nSourceType, UInt64 uBeginTime, UInt64 uEndTime, byte[] szFileName, file_type_e nFileType, IntPtr nTimeout);
        //停止下载
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopDownloadRecord(IntPtr nPDLLHandle, int nPlaybackSeq);

        /** 暂停录像流.
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nPlaybackSeq	回放请求序号 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PauseRecordStreamBySeq(IntPtr nPDLLHandle, Int32 nPlaybackSeq, IntPtr nTimeout);

        /** 恢复录像流.
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nPlaybackSeq	回放请求序号 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ResumeRecordStreamBySeq(IntPtr nPDLLHandle, Int32 nPlaybackSeq, IntPtr nTimeout);

        //定位录像
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SeekPlaybackBySeq(IntPtr nPDLLHandle, int nPlaybackSeq, UInt64 seekBegin, UInt64 seekEnd, IntPtr nTimeout);

        //定位本地录像
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SeekLocalPlaybackBySeq(IntPtr nPDLLHandle, int nPlaybackSeq, int nPos, IntPtr nTimeout);

        /// 录像下载进度通知。
        ///@param   								nPDLLHandle				SDK句柄
        ///@param                                   nDownloadSeq	        下载录像的序列号
        ///@param                                   nPos                    进度度，范围0--100
        ///@param   								pUserParam              用户数据
        ///@remark									
        /// </summary>
        public delegate IntPtr fDownloadProgressCallback(IntPtr nPDLLHandle,
                                                         int downloadSeq,
                                                         int position,
                                                         IntPtr pUserParam);

        public static fDownloadProgressCallback fDownLoadProcess;

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDownloadProgressCallback(IntPtr nPDLLHandle, fDownloadProgressCallback fDownLoadFinish, IntPtr pUser);

        /// 录像下载结束通知。
        ///@param   								nPDLLHandle				SDK句柄
        ///@param                                   downloadSeq	            下载序列号
        ///@param   								pUserParam              用户数据
        ///@remark									
        /// </summary>
        public delegate IntPtr fDownloadFinishedCallback(IntPtr nPDLLHandle,
                                                         int downloadSeq,
                                                         IntPtr pUserParam);

        public static fDownloadFinishedCallback fDownLoadFinish;

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDownloadFinishedCallback(IntPtr nPDLLHandle, fDownloadFinishedCallback fDownLoadFinish, IntPtr pUser);

        /* 第一帧码流回调通知。
        ///@param   								nPDLLHandle				SDK句柄
        ///@param                                   nSequence	            码流序列号
        ///@param                                   szCamearId	            码流对应的通道ID
        ///@param                                   nCameraIDLen	        通道ID字符串长度
        ///@param                                   nPlayMode	            播放类型，1实时，2回放，详细参考dpsdk_media_func_e
        ///@param                                   nFactoryType	        厂商标示*- 0表示包含大华头的厂商码流，需要内部分析是哪个厂商，目前只支持大华、海康、华三
																					*- 1表示大华厂家
																					*- 2表示海康厂家
																					*- 4表示汉邦厂商
																					*- 5表示天地伟业
																					*- 6表示恒忆
																					*- 7表示黄河
																					*- 8表示朗驰
																					*- 9表示浩特
																					*- 10表示卡尔 
																					*- 11表示景阳
																					*- 12表示中维世纪，后缀名通常为801
																					*- 13表示中维世纪板卡,后缀名通常为sv4(和通用版本是2套SDK）
																					*- 14表示东方网力
																					*- 15表示恒通
																					*- 16表示立元的DB33
																					*- 17表示环视
																					*- 18表示蓝星
        ///@param   								pUserParam              用户数据
        ///@remark									
        /// </summary>  */
        public delegate IntPtr fMediaDataFirstFrameCallback(IntPtr nPDLLHandle,
                                                            int nSequence,
                                                            IntPtr szCamearId,
                                                            int nCameraIDLen,
                                                            int nPlayMode,
                                                            int nFactoryType,
                                                            IntPtr pUserParam);

        public static fMediaDataFirstFrameCallback fMediaDataFirstFrame;

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetMediaDataFirstFrameCallback(fMediaDataFirstFrameCallback pFun, IntPtr pUser);

        /** 设置报警状态回调.
        @param   IN	nPDLLHandle		SDK句柄
        @param   IN	fun				回调函数
        @param   IN	pUser			用户参数
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark		
           1、需要和DPSDK_Create成对使用
       */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDPSDKAlarmCallback(IntPtr nPDLLHandle, fDPSDKAlarmCallback pFun, IntPtr pUser);

        /** 设置DPSDK设备状态回调.
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	fun				回调函数
         @param   IN	pUser			用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  1)登陆平台，加载组织结构以后平台会推送设备状态;2)设备状态变化会再次推送
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDPSDKDeviceStatusCallback(IntPtr nPDLLHandle, fDPSDKDevStatusCallback pFun, IntPtr pUser);

        /** 查询NVR通道状态(只能查询在线的NVR、SMART_NVR、EVS、NVR6000设备的通道状态)
        @param   IN    nPDLLHandle     SDK句柄
        @param   IN    deviceId	       设备的ID
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark 
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryNVRChnlStatus(IntPtr nPDLLHandle, byte[] deviceId, IntPtr nTimeout);

        /** 设置NVR通道状态回调
        @param   IN	nPDLLHandle		SDK句柄
        @param   IN	fun				回调函数
        @param   IN	pUser			用户参数
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark 通道状态变化的时候会推送
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDPSDKNVRChnlStatusCallback(IntPtr nPDLLHandle, fDPSDKNVRChnlStatusCallback fun, IntPtr pUser);

        /** 设置Json协议回调.
        @param   IN	nPDLLHandle		SDK句柄
        @param   IN	fun				回调函数
        @param   IN	pUser			用户参数
        @return  函数返回错误类型，参考dpsdk_retval_e	
       */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi, EntryPoint = "DPSDK_SetGeneralJsonTransportCallbackEx", ExactSpelling = false, CallingConvention = CallingConvention.StdCall)]
        public extern static IntPtr DPSDK_SetGeneralJsonTransportCallbackEx(IntPtr nPDLLHandle, fDPSDKGeneralJsonTransportCallbackEx pFun, IntPtr pUser);

        /** 报警布控.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	pSourceInfo		报警方案 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
         1、布控时需要明白不同的报警类型对应不同的参数
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_EnableAlarm(IntPtr nPDLLHandle, ref Alarm_Enable_Info_t pSourceInfo, IntPtr nTimeout);

        /** 报警撤控.
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static int DPSDK_DisableAlarm(IntPtr nPDLLHandle, int nTimeout);

        /** 报警布控(针对某个部门下的所有设备)
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	pSourceInfo		报警方案 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
         1、布控时需要明白不同的报警类型对应不同的参数
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_EnableAlarmByDepartment(IntPtr nPDLLHandle, ref Alarm_Enable_By_Dep_Info_t pSourceInfo, IntPtr nTimeout);

        /** 查询报警个数.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN    pQuery          查询信息
         @param	  OUT	nCount			报警个数返回 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryAlarmCount(IntPtr nPDLLHandle, IntPtr pQuery, out UInt32 nCount, IntPtr nTimeout);

        /** 查询报警信息.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN    pQuery          查询信息
         @param	  		pInfo			报警信息 
         @param	  IN	nFirstNum		从第几个开始获取 
         @param	  IN	nQueryCount		获取多少个 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
         1、支持分批获取
         2、此接口推荐和DPSDK_QueryAlarmCount一起使用
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        //public extern static IntPtr DPSDK_QueryAlarmInfo( IntPtr nPDLLHandle, ref Alarm_Query_Info_t pQuery,ref Alarm_Info_t pInfo,UInt32 nFirstNum,UInt32 nQueryCount,IntPtr nTimeout);
        public extern static IntPtr DPSDK_QueryAlarmInfo(IntPtr nPDLLHandle, IntPtr pQuery, ref Alarm_Info_t pInfo, UInt32 nFirstNum, UInt32 nQueryCount, IntPtr nTimeout);

        /** 查询报警图片信息.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN    optype          操作类型
         @param	  IN	szCameraId		通道ID 
         @param	  IN	szIvsURL		图片URL 
         @param	  IN	szSavePath		保存路径 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
         1、支持分批获取
         2、此接口推荐和DPSDK_QueryAlarmCount一起使用
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryIvsbAlarmPicture(IntPtr nPDLLHandle, dpsdk_operator_ftp_type_e optype, byte[] szCameraId, byte[] szIvsURL, byte[] szSavePath, IntPtr nTimeout);

        /** 发送报警到服务.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN    Client_Alarm_Info_t		报警信息
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SendAlarmToServer(IntPtr nPDLLHandle, ref Client_Alarm_Info_t pAlarmInfo, IntPtr nTimeout);

        /** 查询RFID报警信息.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN    pQuery          查询信息
         @param	  		pInfo			RFID报警信息 
         @param	  IN	nFirstNum		从第几个开始获取 
         @param	  IN	nQueryCount		获取多少个 
         @param   IN    pSwLabel		超声波探测标签
         @param   IN    pElecLabel		车用电子标签
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
         1、支持分批获取
         2、此接口推荐和DPSDK_QueryAlarmCount一起使用
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryRFIDAlarmInfo(IntPtr nPDLLHandle, ref Alarm_Query_Info_t pQuery, ref Alarm_Info_t pInfo, UInt32 nFirstNum, UInt32 nQueryCount, byte[] pSwLabel, byte[] pElecLabel, IntPtr nTimeout);

        /** 报警上报过滤
         @param		IN		nPDLLHandle			SDK句柄
         @param		IN		szCameraIdList		通道ID列表，多个通道ID用','分隔
         @param		IN		pAlarmTypeArray		报警类型列表
         @param		IN		nAlarmCount			报警类型数量
         @return  函数返回错误类型，参考dpsdk_retval_e
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        // public extern static IntPtr DPSDK_FilterAlarm(IntPtr nPDLLHandle, byte[] szCameraIdList,ref dpsdk_alarm_type_e pAlarmTypeArray, int nAlarmCount);
        public extern static IntPtr DPSDK_FilterAlarm(IntPtr nPDLLHandle, byte[] szCameraIdList, IntPtr pAlarmTypeArray, int nAlarmCount);
        /** 去除报警上报过滤
         @param		IN		nPDLLHandle			SDK句柄
         @param		IN		szCameraIdList		通道ID列表，多个通道ID用','分隔
         @param		IN		pAlarmTypeArray		报警类型列表
         @param		IN		nAlarmCount			报警类型数量
         @return  函数返回错误类型，参考dpsdk_retval_e
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        //public extern static IntPtr DPSDK_NoFilterAlarm(IntPtr nPDLLHandle, byte[] szCameraIdList,ref dpsdk_alarm_type_e pAlarmTypeArray,int nAlarmCount);
        public extern static IntPtr DPSDK_NoFilterAlarm(IntPtr nPDLLHandle, byte[] szCameraIdList, IntPtr pAlarmTypeArray, int nAlarmCount);

        /** 查询服务列表，建立服务连接（不用加载组织结构也可以控制云台、接收报警、接收设备状态上报）
        @param   IN    nPDLLHandle     SDK句柄
        @return  函数返回错误类型，参考dpsdk_retval_e
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryServerList(IntPtr nPDLLHandle);


        /** 全网校时开关
        @param   IN    nPDLLHandle     SDK句柄
        @param   IN    bOpen		   开关标志，1开，0关
        @return  函数返回错误类型，参考dpsdk_retval_e
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetSyncTimeOpen(IntPtr nPDLLHandle, byte bOpen);

        /* @fn    
         * @brief 获取电视墙任务列表总数
         * @param <IN>		nPDLLHandle		SDK句柄
         * @param <IN>		nTvWallId		电视墙ID
         * @param <OUT>		nCount			返回个数 
         * @param <IN>		nTimeout		超时时长，单位毫秒
         * @return 函数返回错误类型，参考dpsdk_retval_e.
         */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetTvWallTaskListCount(IntPtr nPDLLHandle, UInt32 nTVWallId, out UInt32 nCount, IntPtr nTimeout);

        /* @fn    
         * @brief 获取电视墙任务列表信息
         * @param <IN>		nPDLLHandle		SDK句柄
         * @param <IN>		nTvWallId		电视墙ID
         * @param <OUT>		pTVWallTaskInfoList		电视墙任务列表信息 
         * @param <IN>		nTimeout		超时时长，单位毫秒
         * @return 函数返回错误类型，参考dpsdk_retval_e.
         */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetTvWallTaskList(IntPtr nPDLLHandle, UInt32 nTVWallId, ref TvWall_Task_Info_List_t pTVWallTaskInfoList, IntPtr nTimeout);

        /* @fn    
         * @brief 获取电视墙任务信息长度
         * @param <IN>		nPDLLHandle		SDK句柄
         * @param <IN>		nTvWallId		电视墙ID
         * @param <IN>		nTaskId		    电视墙任务ID
         * @param <OUT>		pTaskInfoLen	电视墙任务信息长度 
         * @param <IN>		nTimeout		超时时长，单位毫秒
         * @return 函数返回错误类型，参考dpsdk_retval_e.
         */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetTvWallTaskInfoLen(IntPtr nPDLLHandle, UInt32 nTVWallId, UInt32 nTaskId, out UInt32 pTaskInfoLen, IntPtr nTimeout);

        /* @fn    
         * @brief 获取电视墙任务信息，需要先调用DPSDK_GetTvWallTaskInfoLen()
         * @param <IN>		nPDLLHandle		SDK句柄
         * @param <OUT>		szTaskInfoBuf	电视墙任务信息 
         * @param <IN>		nTaskInfoLen	电视墙任务信息长度 
         * @return 函数返回错误类型，参考dpsdk_retval_e.
         */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetTvWallTaskInfoStr(IntPtr nPDLLHandle, ref byte szTaskInfoBuf, UInt32 nTaskInfoLen);

        /* @fn    
         * @brief 电视墙任务上墙
         * @param <IN>		nPDLLHandle		SDK句柄
         * @param <IN>		nTvWallId		电视墙ID
         * @param <IN>		nTaskId		    电视墙任务ID
         * @param <IN>		nTimeout		超时时长，单位毫秒
         * @return 函数返回错误类型，参考dpsdk_retval_e.
         */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_MapTaskToTvWall(IntPtr nPDLLHandle, UInt32 nTVWallId, UInt32 nTaskId, IntPtr nTimeout);

        /** 查询电视墙列表个数.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  OUT	nCount			返回个数
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryTvWallList(IntPtr nPDLLHandle, ref UInt32 nCount, IntPtr nTimeout);

        /** 查询电视墙布局信息
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	nTvWallId		电视墙ID
         @param	  OUT	nCount			返回个数
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryTvWallLayout(IntPtr nPDLLHandle, Int32 nTvWallId, ref UInt32 nCount, IntPtr nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetTvWallListCount(IntPtr nPDLLHandle, ref int nCount, int nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetTvWallList(IntPtr nPDLLHandle, ref TvWall_List_Info_t pTvWallListInfo, IntPtr nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetTvWallLayoutCount(IntPtr nPDLLHandle, int nTvWallId, ref uint nCount, int nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static IntPtr DPSDK_GetTvWallLayout(IntPtr nPDLLHandle, ref TvWall_Layout_Info_t pTvWallLayoutInfo);

        /// <summary>
        /// 开窗必须是融合屏,非融合屏只能分割，融合的NVD只分割
        /// </summary>
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetTvWallScreenSplit(IntPtr nPDLLHandle, ref TvWall_Screen_Split_t pSplitInfo, int nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_TvWallScreenOpenWindow(IntPtr nPDLLHandle, ref TvWall_Screen_Open_Window_t pOpenWindowInfo, int nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_TvWallScreenColseWindow(IntPtr nPDLLHandle, ref TvWall_Screen_Close_Window_t pCloseWindowInfo, int nTimeout);

        /** 窗口移动.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  INOUT	pMoveWindowInfo		电视墙屏窗口移动信息
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_TvWallScreenMoveWindow(IntPtr nPDLLHandle, ref TvWall_Screen_Move_Window_t pMoveWindowInfo, int nTimeout);

        /** 屏窗口置顶（对于开窗有效）.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	pTopWindowInfo		电视墙屏窗口置顶信息
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_TvWallScreenSetTopWindow(IntPtr nPDLLHandle, ref TvWall_Screen_Set_Top_Window_t pTopWindowInfo, int nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetTvWallScreenWindowSource(IntPtr nPDLLHandle, ref Set_TvWall_Screen_Window_Source_t pSourceInfo, int nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_CloseTvWallScreenWindowSource(IntPtr nPDLLHandle, ref TvWall_Screen_Close_Source_t pSourceInfo, int nTimeout);

        /** 清空电视墙屏  
         @param   此接口存在问题，当电视墙绑定多个解码器id时，只能清除一个解码器id的屏，请使用接口DPSDK_ClearTvWallScreenByDecodeId
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	nTvWallId		电视墙ID
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ClearTvWallScreen(IntPtr nPDLLHandle, int nTvWallId, int nTimeout);

        /** 根据解码器ID清空电视墙屏
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	nTvWallId		电视墙ID
         @param	  IN	szDecodeId		解码器ID
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ClearTvWallScreenByDecodeId(IntPtr nPDLLHandle, int nTvWallId, byte[] szDecodeId, int nTimeout);


        /** 订阅云台报警信息
         @param	  IN	nPDLLHandle				SDK句柄
         @param   IN	szCameraId		        通道编号
         @param	  IN	nSubscribeFlag			订阅标记。0:取消订阅，1：订阅
         @param   IN	nTimeout				超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SubscribePtzSitAlarm(IntPtr nPDLLHandle, byte[] szCameralID, int nSubscribeFlag, out int nResult, IntPtr nTimeout);

        /** 云台方向控制.
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	pDirectInfo		云台方向控制信息 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PtzDirection(IntPtr nPDLLHandle, ref Ptz_Direct_Info_t pDirectInfo, IntPtr nTimeout);

        /** 云台镜头控制.
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	pOperationInfo	云台镜头控制信息 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PtzCameraOperation(IntPtr nPDLLHandle, ref Ptz_Operation_Info_t pOperationInfo, IntPtr nTimeout);


        /** 云台三维定位.
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	pSitInfo		云台三维定位信息 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PtzSit(IntPtr nPDLLHandle, ref Ptz_Sit_Info_t pSitInfo, IntPtr nTimeout);

        /** 查询云台三维信息
        @param	 IN	   nPDLLHandle	SDK句柄
        @param   INOUT pSitInfo		云台三维定位信息 
        @param   IN	   nTimeout		超时时长，单位毫秒
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryPtzSitInfo(IntPtr nPDLLHandle, ref Ptz_Sit_Info_t pSitInfo, IntPtr nTimeout);

        /** 云台锁定控制.
         @param	  IN	nPDLLHandle		SDK句柄
         @param   IN	pLockInfo	    云台锁定控制信息
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PtzLockCamera(IntPtr nPDLLHandle, ref Ptz_Lock_Info_t pLockInfo, IntPtr nTimeout);

        /** 云台灯光控制.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	szCameraId		通道ID 
         @param   IN	bOpen			开启标识
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PtzLightControl(IntPtr nPDLLHandle, ref Ptz_Open_Command_Info_t Cmd, IntPtr nTimeout);

        /** 云台雨刷控制.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	szCameraId		通道ID 
         @param   IN	bOpen			开启标识
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PtzRainBrushControl(IntPtr nPDLLHandle, ref Ptz_Open_Command_Info_t Cmd, IntPtr nTimeout);

        /** 云台红外控制.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	szCameraId		通道ID 
         @param   IN	bOpen			开启标识
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PtzInfraredControl(IntPtr nPDLLHandle, ref Ptz_Open_Command_Info_t Cmd, IntPtr nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryPrePoint(IntPtr nPDLLHandle, ref Ptz_Prepoint_Info_t pPrepoint, IntPtr nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PtzPrePointOperation(IntPtr nPDLLHandle, ref Ptz_Prepoint_Operation_Info_t pPrepoint, IntPtr nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        private extern static IntPtr DPSDK_SetDPSDKTrafficAlarmCallback(IntPtr nPDLLHandle, fDPSDKTrafficAlarmCallback pFun, IntPtr pUser);

        /** 车辆违章图片信息写入
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	pGetInfo		详细违章信息,参考Traffic_Violation_Info_t
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_WriteTrafficViolationInfo(IntPtr nPDLLHandle, ref Traffic_Violation_Info_t pGetInfo, IntPtr nTimeout);

        /** 车辆违章图片信息查询
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	pGetInfo		详细违章信息,参考Traffic_Violation_Info_t
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetTrafficViolationInfo(IntPtr nPDLLHandle, ref Traffic_Violation_Info_t pGetInfo, IntPtr nTimeout);

        /** 设置流量上报回调.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	fun				回调函数
         @param   IN	pUser			用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDPSDKGetTrafficFlowCallback(IntPtr nPDLLHandle, fDPSDKGetTrafficFlowCallback fun, IntPtr pUser);
        /** 设置车道流量状态上报回调.
         @param   IN    nPDLLHandle     SDK句柄
         @param   IN    fun             回调函数
         @param   IN    pUser           用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDPSDKGetDevTrafficFlowCallback(IntPtr nPDLLHandle, fDPSDKGetDevTrafficFlowCallback fun, IntPtr pUser);

        /** 订阅流量上报.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	pGetInfo		订阅设备流量上报请求信息，参考Subscribe_Traffic_Flow_Info_t
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SubscribeTrafficFlow(IntPtr nPDLLHandle, ref Subscribe_Traffic_Flow_Info_t pGetInfo, IntPtr nTimeout);

        /** 订阅区间测速上报.
         @param   IN    nPDLLHandle     SDK句柄
         @param   IN    nSubscribeFlag  订阅标记:1订阅；0；取消订阅
         @param   IN    nTimeout        超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SubscribeAreaSpeedDetectInfo(IntPtr nPDLLHandle, Int32 nSubscribeFlag, IntPtr nTimeout);

        /** 设置区间测速上报回调.
         @param   IN    nPDLLHandle     SDK句柄
         @param   IN    fun             回调函数
         @param   IN    pUser           用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDPSDKGetAreaSpeedDetectCallback(IntPtr nPDLLHandle, fDPSDKGetAreaSpeedDetectCallback fun, IntPtr pUser);

        /** 设置卡口布控告警上报回调.
         @param   IN    nPDLLHandle     SDK句柄
         @param   IN    fun             回调函数
         @param   IN    pUser           用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDPSDKBayWantedAlarmCallback(IntPtr nPDLLHandle, fDPSDKBayWantedAlarmCallback fun, IntPtr pUser);

        /** 设置卡口过车信息（不带图片）上报回调.
         @param   IN    nPDLLHandle     SDK句柄
         @param   IN    fun             回调函数
         @param   IN    pUser           用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  设置回调函数以后再订阅DPSDK_SubscribeBayCarInfo
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDPSDKGetBayCarInfoCallback(IntPtr nPDLLHandle, fDPSDKGetBayCarInfoCallback pFun, IntPtr pUser);


        /** 设置卡口过车信息（不带图片）上报回调.
        @param   IN    nPDLLHandle     SDK句柄
        @param   IN    fun             回调函数
        @param   IN    pUser           用户参数
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark  设置回调函数以后再订阅DPSDK_SubscribeBayCarInfo
       */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDPSDKGetBayCarInfoCallbackEx(IntPtr nPDLLHandle, fDPSDKGetBayCarInfoCallbackEx pFun, IntPtr pUser);

        /** 订阅卡口过车信息上报.
        @param   IN    nPDLLHandle   SDK句柄
        @param   IN    pGetInfo      订阅卡口过车信息上报请求信息，参考Subscribe_Bay_Car_Info_t
                                     注意: 如果订阅通道数nChnlCount为0(pEncChannelnfo要置NULL)，
                                     表示所有通道的过车数据都上报.
                                     只有订阅以后picSDK的回调才能起作用。DPSDK_SetDPSDKGetBayCarInfoCallback才能回调给上层
        @param   IN    nTimeout        超时时长，单位毫秒
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark  
       */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SubscribeBayCarInfo(IntPtr nPDLLHandle, ref Subscribe_Bay_Car_Info_t pSourceInfo, IntPtr nTimeout);

        /** 增加电子围栏
        @param  IN		nPDLLHandle     SDK句柄
        @param	IN		pAreaInfo		电子围栏信息
        @param	IN		arryDevId		设备ID的数组
        @param	IN		nDevIdCount		设备ID的数组的长度
        @param	OUT		strAreaId		围栏ID
        @param	IN		nAreaIdLen		围栏ID的字符串大小
        @return
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]        //IN char arryDevId[][DPSDK_CORE_DEV_ID_LEN]
        public extern static IntPtr DPSDK_AddAreaInfo(IntPtr nPDLLHandle, ref Area_Info_t pAreaInfo, IntPtr arryDevId, Int32 nDevIdCount, byte[] szAreaId, Int32 nAreaIdLen, IntPtr nTimeout);

        /** 删除电子围栏
        @param  IN		nPDLLHandle     SDK句柄
        @param	IN		szAreaId		电子围栏Id
        @return
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_DelAreaInfo(IntPtr nPDLLHandle, byte[] szAreaId, IntPtr nTimeout);

        /** 修改电子围栏
        @param  IN		nPDLLHandle     SDK句柄
        @param	IN		szAreaId		电子围栏Id
        @param	IN		pAreaInfo		电子围栏信息
        @return	
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ModAreaInfo(IntPtr nPDLLHandle, byte[] szAreaId, ref Area_Info_t pAreaInfo, IntPtr nTimeout);


        /** 查询设备和围栏的关系
        @param  IN		nPDLLHandle     SDK句柄
        @return	
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryDevAreaRelationLen(IntPtr nPDLLHandle, IntPtr nTimeout);

        /** 增加设备与电子围栏的关联（只修改了本地内存中的，需要Upload才上传服务）
        @param  IN		nPDLLHandle     SDK句柄
        @param	IN		szDevId			设备ID
        @param	IN		szAreaId		电子围栏ID
        @param	IN		nAreaType		电子围栏类型
        @return	
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_AddAreaInDev(IntPtr nPDLLHandle, byte[] szDevId, byte[] szAreaId, dpsdk_area_type_e nAreaType, IntPtr nTimeout);

        /** 删除设备与电子围栏的关联（只修改了本地内存中的，需要Upload才上传服务）
        @param  IN		nPDLLHandle     SDK句柄
        @param	IN		szDevId			设备ID
        @param	IN		szAreaId		电子围栏ID
        @return							
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_DelAreaInDev(IntPtr nPDLLHandle, byte[] szDevId, byte[] szAreaId, IntPtr nTimeout);

        /** 上传设备与围栏关系给服务
        @param  IN		nPDLLHandle     SDK句柄
        @return	
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_UploadRelationChange(IntPtr nPDLLHandle, IntPtr nTimeout);


        /** 获取围栏信息
        @param  IN		nPDLLHandle     SDK句柄
        @return	
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_AskForAreaInfo(IntPtr nPDLLHandle, IntPtr nTimeout);

        /** 根据停车区和停车道来获取对应通道ID
        @param  IN		nPDLLHandle     SDK句柄
        @param	IN		StopSection		停车区
        @param	IN		StopWay			停车道
        @param	OUT		CameraID		通道ID
        @param	IN		nTimeout		操作超时时间
        @return							异步顺序码,用于事件回调时,与应答事件匹配
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetCameraIDbyStopSectionandWay(IntPtr nPDLLHandle, byte[] StopSection, byte[] StopWay, byte[] CameraID, IntPtr nTimeout);

        /** 根据通道ID开关摄像头区域入侵分析功能
        @param  IN		nPDLLHandle     SDK句柄
        @param	IN		DeviceID		摄像头设备ID
        @param	IN		bFlag			开关标志 true：开  false：关
        @param	IN		nTimeout		操作超时时间
        @return							异步顺序码,用于事件回调时,与应答事件匹配
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_OpenIntrusionDetection(IntPtr nPDLLHandle, byte[] CameraID, bool bFlag, IntPtr nTimeout);


        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetRecordStreamByTime(IntPtr nPDLLHandle, out IntPtr nPlaybackSeq,
          ref Get_RecordStream_Time_Info_t pRecordInfo, fMediaDataCallback pFun, IntPtr pUser, int nTimeout);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetPecDoorStatusCallback(IntPtr nPDLLHandle, fDPSDKPecDoorStarusCallBack pFun, IntPtr pUser);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDoorCmd(IntPtr nPDLLHandle, ref SetDoorCmd_Request_t pSetDoorCmdRequest, IntPtr nTimeout);


        /** 媒体数据回调.
         @param   IN									nPDLLHandle				SDK句柄
         @param   IN									nSeq					对应请求时返回的Seq
         @param   IN									nMediaType				媒体类型，参考dpsdk_media_type_e
         @param	  IN									szNodeId				数据对应的通道/设备ID
         @param	  IN 									nParamVal				扩展值;mediaType为real时，为streamType;
         @param	  IN 									szData					媒体流数据
         @param   IN 									nDataLen				数据长度 
         @param   IN 									pUserParam				用户指针参数,由对应的DPSDK_SetxxxxCallBack传入
         @remark									
        */
        public delegate IntPtr fMediaDataCallback(IntPtr nPDLLHandle,
            IntPtr nSeq, int nMediaType, [MarshalAs(UnmanagedType.LPStr)] StringBuilder szNodeId, int nParamVal, IntPtr szData,
            int nDataLen, IntPtr pUserParam);

        public static fMediaDataCallback fMediaData;

        //////////////////////////////////////////////////////////////////////////对讲相关回调
        /** 集群对讲发起呼叫参数回调
	        @param	<IN>	                                nPDLLHandle		    SDK句柄
	        @param	<IN>	                                param				主动发起呼叫的对讲参数，用于本地音频采集
	        @param	<IN>									pUserParam			用户指针参数,由对应的DPSDK_SetxxxxCallBack传入
        */
        public delegate IntPtr fStartCallParamCallBack(IntPtr nPDLLHandle, ref StartCallParam_t param, IntPtr pUserParam);
        public static fStartCallParamCallBack fStartCallParam;

        /** 集群对讲呼叫邀请参数回调
	        @param	<IN>	                                nPDLLHandle		    SDK句柄
	        @param	<IN>	                                param				被邀请对讲的对讲参数，用于本地音频采集
	        @param	<IN>									pUserParam			用户指针参数,由对应的DPSDK_SetxxxxCallBack传入
        */
        public delegate IntPtr fInviteCallParamCallBack(IntPtr nPDLLHandle, ref InviteCallParam_t param, IntPtr pUserParam);
        public static fInviteCallParamCallBack fInviteCallParam;

        /** 可视对讲呼叫邀请参数回调
	        @param	<IN>	                                nPDLLHandle		    SDK句柄
	        @param	<IN>	                                param				被邀请对讲的对讲参数，用于本地音频采集
	        @param	<IN>									pUserParam			用户指针参数,由对应的DPSDK_SetxxxxCallBack传入
        */
        public delegate IntPtr fDPSDKInviteVtCallParamCallBack(IntPtr nPDLLHandle, ref InviteVtCallParam_t param, IntPtr pUserParam);
        public static fDPSDKInviteVtCallParamCallBack fDPSDKInviteVtCallParam;

        /** 响铃通知回调
	        @param	<IN>	                                nPDLLHandle		    SDK句柄
	        @param	<IN>	                                param				响铃参数
	        @param	<IN>									pUserParam			用户指针参数,由对应的DPSDK_SetxxxxCallBack传入
        */
        public delegate IntPtr fDPSDKRingInfoCallBack(IntPtr nPDLLHandle, ref RingInfo_t param, IntPtr pUserParam);
        public static fDPSDKRingInfoCallBack fDPSDKRingInfo;

        /** 可视对讲呼叫状态繁忙通知回调
	        @param	<IN>	                                nPDLLHandle		    SDK句柄
	        @param	<IN>	                                param				状态繁忙信息参数
	        @param	<IN>									pUserParam			用户指针参数,由对应的DPSDK_SetxxxxCallBack传入
        */
        public delegate IntPtr fDPSDKBusyVtCallCallBack(IntPtr nPDLLHandle, ref BusyVtCallInfo_t param, IntPtr pUserParam);
        public static fDPSDKBusyVtCallCallBack fDPSDKBusyVtCall;

        /** 语音对讲音频发送函数定义. 
            @param char * pData						媒体流数据
            @param int dataLen						数据长度 
            @param void * pUserParam				用户参数,见AudioUserParam
        */
        public delegate IntPtr fAudioDataCallback([MarshalAs(UnmanagedType.LPStr)] StringBuilder pData, int dataLen, IntPtr pUserParam);
        public static fAudioDataCallback fAudioData;
        //////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////语音对讲
        /** 开始广播
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	nTalkSeq		码流请求序号,可作为后续操作标识 
         @param   IN	pGetInfo		码流请求信息 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartBroadcastExt(IntPtr nPDLLHandle, ref int nTalkSeq, byte[] szJson, IntPtr nTimeout);

        /** 开始广播
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	nTalkSeq		码流请求序号,可作为后续操作标识 
         @param   IN	pGetInfo		码流请求信息 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopBroadcast(IntPtr nPDLLHandle, Int32 nTalkSeq, byte[] szJson, IntPtr nTimeout);

        /** 开始语音对讲
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	nTalkSeq		码流请求序号,可作为后续操作标识 
         @param   IN	pGetInfo		码流请求信息 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartTalk(IntPtr nPDLLHandle, out Int32 nTalkSeq, ref Get_TalkStream_Info_t pGetInfo, IntPtr nTimeout);
        /** 按请求序号停止语音对讲.
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nTalkSeq		码流请求序号,作为后续操作标识 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopTalkBySeq(IntPtr nPDLLHandle, Int32 nTalkSeq, IntPtr nTimeout);

        /** 按CameraId停止语音对讲
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	szCameraId		设备或通道编号
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  关闭所有的已经打开的CameraId	
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopTalkByCameraId(IntPtr nPDLLHandle, byte[] szCameraId, IntPtr nTimeout);


        //集群对讲

        /** 开始呼叫
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	nCallSeq		码流请求序号,作为后续操作标识 
         @param   IN	nCallType		呼叫类型 0单播；1组播；2可视对讲
         @param   IN	szGroupId		呼叫组ID
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartCallExt(IntPtr nPDLLHandle, ref Int32 nCallSeq, ref dpsdk_call_type_e nCallType, byte[] szGroupId, IntPtr nTimeout);


        /** 关闭呼叫
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nCallSeq		码流请求序号,可作为后续操作标识
         @param   IN	szGroupId		组ID
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopCallExt(IntPtr nPDLLHandle, Int32 nCallSeq, byte[] szGroupId, IntPtr nTimeout);

        /** 呼叫邀请
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	nCallSeq		码流请求序号,可作为后续操作标识
         @param   IN	pGetInfo		呼叫邀请信息 
         @param   IN    RecordParam		本地音频采集参数				
         @param   IN    pUser			码流回调用户参数
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_InviteCallExt(IntPtr nPDLLHandle, ref Int32 nCallSeq, ref Get_InviteCall_Info_t pGetInfo, ref AudioRecrodParam_t RecordParam, IntPtr pUser, IntPtr nTimeout);

        /** 呼叫被挂断，释放链接
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nCallSeq		码流请求序号,可作为后续操作标识 
         @param   IN	szGroupId		呼叫组ID
         @param   IN	nTid			T ID
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ByeCallExt(IntPtr nPDLLHandle, Int32 nCallSeq, byte[] szGroupId, Int32 nTid, IntPtr nTimeout);


        /** 打开/关闭本地麦克风.
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nRealSeq		码流请求序号,可作为后续操作标识
         @param   IN	bOpen		    true打开语音，false关闭语音
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_OpenAudioRecord(IntPtr nPDLLHandle, Int32 nSeq, bool bOpen);

        /** 打开/关闭本地麦克风.
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nRealSeq		码流请求序号,可作为后续操作标识
         @param   IN	bOpen		    true打开语音，false关闭语音
         @param   IN	nAudioType		    true打开语音，false关闭语音
         @param   IN	nBitsPerSample		    true打开语音，false关闭语音
         @param   IN	nSamplesPerSec		    true打开语音，false关闭语音
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_OpenAudioRecordExt(IntPtr nPDLLHandle, Int32 nSeq, bool bOpen, long nAudioType, long nBitsPerSample, long nSamplesPerSec);

        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetStartCallParamCallback(IntPtr nPDLLHandle, fStartCallParamCallBack pFun, IntPtr pUser);

        /** 打开呼叫
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	nCallSeq		码流请求序号,可作为后续操作标识
         @param   IN	nCallType		呼叫类型 0单播；1组播；2可视对讲
         @param   IN	szGroupId		呼叫组ID
         @param   IN    pFun			码流回调函数				
         @param   IN    pUser			码流回调用户参数
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartCall(IntPtr nPDLLHandle, ref Int32 nCallSeq, dpsdk_call_type_e nCallType, byte[] szGroupId, fMediaDataCallback pFun, IntPtr pUser, IntPtr nTimeout);


        /** 关闭呼叫
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nCallSeq		码流请求序号,可作为后续操作标识
         @param   IN	szGroupId		组ID
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopCall(IntPtr nPDLLHandle, Int32 nCallSeq, byte[] szGroupId, IntPtr nTimeout);


        /** 设置呼叫邀请参数回调
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	fun				回调函数
         @param   IN	pUser			用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
	        1、需要和DPSDK_Create成对使用
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetInviteCallParamCallBack(IntPtr nPDLLHandle, fInviteCallParamCallBack pFun, IntPtr pUser);

        /** 呼叫邀请
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	nCallSeq		码流请求序号,可作为后续操作标识
         @param   IN	pGetInfo		呼叫邀请信息 
         @param   IN    pFun			码流回调函数				
         @param   IN    pUser			码流回调用户参数
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_InviteCall(IntPtr nPDLLHandle, ref Int32 nCallSeq, ref Get_InviteCall_Info_t pGetInfo, fMediaDataCallback pFun, IntPtr pUser, IntPtr nTimeout);


        /** 呼叫被挂断，释放链接
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nCallSeq		码流请求序号,可作为后续操作标识 
         @param   IN	szGroupId		呼叫组ID
         @param   IN	nTid			T ID
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ByeCall(IntPtr nPDLLHandle, Int32 nCallSeq, byte[] szGroupId, Int32 nTid, IntPtr nTimeout);


        /** 变更呼叫状态
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nCallSeq		码流请求序号,可作为后续操作标识 
         @param   IN	szGroupId		呼叫组ID
         @param   IN	nCallStatus		呼叫状态
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ModifyCallStatus(IntPtr nPDLLHandle, Int32 nCallSeq, byte[] szGroupId, dpsdk_call_status_e nCallStatus, IntPtr nTimeout);

        /** 获取语音发送函数指针
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	pCallBackFun	回调函数
         @param   OUT	pUserParam   	回调函数用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        // public extern static IntPtr DPSDK_GetAudioSendFunCallBack(	IntPtr nPDLLHandle, void** pCallBackFun, AudioUserParam_t** pUserParam);
        public extern static IntPtr DPSDK_GetAudioSendFunCallBack(IntPtr nPDLLHandle, out IntPtr pCallBackFun, out IntPtr pUserParam);

        /** 发送消息给scs服务
         @param		IN		nPDLLHandle			SDK句柄
         @param		IN		szGroupId			呼叫组ID
         @param		IN		szStrText			发送的文本内容
         @param		IN		nTimeout			超时时间
         @return  函数返回错误类型，参考dpsdk_retval_e
         @sample
	        DPSDK_SendScsMsg(m_nPDLLHandle, "test", "test", DPSDK_CORE_DEFAULT_TIMEOUT);
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SendScsMsg(IntPtr nPDLLHandle, byte[] szGroupId, byte[] szStrText, IntPtr nTimeout);


        /** 获取本地IP
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	nLocalIP		本地IP
         @param   IN	nLen   			IP的长度，IP的最大长度46
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetLocalIp(IntPtr nPDLLHandle, byte[] nLocalIP, Int32 nLen);

        //集群对讲业务接口 结束


        //可视对讲业务接口 开始

        /** 设置可视对讲呼叫邀请参数回调
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	fun				回调函数
         @param   IN	pUser			用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetVtCallInviteCallback(IntPtr nPDLLHandle, fDPSDKInviteVtCallParamCallBack pFun, IntPtr pUser);

        /** 设置响铃参数通知回调
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	fun				回调函数
         @param   IN	pUser			用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetRingCallback(IntPtr nPDLLHandle, fDPSDKRingInfoCallBack pFun, IntPtr pUser);

        /** 设置可视对讲繁忙状态通知回调
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	fun				回调函数
         @param   IN	pUser			用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetBusyVtCallCallback(IntPtr nPDLLHandle, fDPSDKBusyVtCallCallBack pFun, IntPtr pUser);


        /** 请求可视对讲
         @param   IN	nPDLLHandle			SDK句柄
         @param   OUT	audioSessionId		音频请求序号,用于关闭对讲
         @param   OUT	videoSessionId		视频请求序号,用于关闭对讲
         @param   OUT	nStartVtCallParm	应答参数，用于本地频频采集和关闭对讲
         @param   IN	nCallType			呼叫类型 0单播；1组播；2可视对讲
         @param   IN	szUserId			呼叫者ID
         @param   IN    pFun				码流回调函数				
         @param   IN    pCBParam			码流回调用户参数
         @param   IN	nTimeout			超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartVtCall(IntPtr nPDLLHandle, ref UInt32 audioSessionId, ref UInt32 videoSessionId, ref StartVtCallParam_t nStartVtCallParm, dpsdk_call_type_e nCallType, byte[] szUserId, fMediaDataCallback funCB, IntPtr pCBParam, IntPtr nTimeout);


        /** 发送取消可视对讲
         @param   IN	nPDLLHandle			SDK句柄
         @param   IN	szUserId			呼叫者ID
         @param   IN	audioSessionId		语音请求序列号
         @param   IN	videoSessionId		视频请求序列号
         @param   IN    nCallId				呼叫ID			
         @param   IN    m_dlgId				回话ID
         @param   IN	nTimeout			超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SendCancelVtCall(IntPtr nPDLLHandle, byte[] szUserId, UInt32 audioSessionId, UInt32 videoSessionId, int callId, int dlgId, IntPtr nTimeout);

        /** 请求可视对讲成功后，停止可视对讲
         @param   IN	nPDLLHandle			SDK句柄
         @param   IN	szUserId			呼叫者ID
         @param   IN	audioSessionId		语音请求序列号
         @param   IN	videoSessionId		视频请求序列号
         @param   IN    nCallId				呼叫ID			
         @param   IN    m_dlgId				回话ID
         @param   IN	nTimeout			超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopVtCall(IntPtr nPDLLHandle, byte[] szUserId, UInt32 audioSessionId, UInt32 videoSessionId, int m_callId, int m_dlgId, IntPtr nTimeout);

        /** 接受可视对讲邀请
         @param   IN	nPDLLHandle			SDK句柄
         @param   OUT	audioSessionId		音频请求序号,用于关闭对讲
         @param   OUT	videoSessionId		视频请求序号,用于关闭对讲
         @param   IN	nInviteVtCallParam	对讲参数，由DPSDK_SetVtCallInviteCallback获取
         @param   IN	nCallType			呼叫类型 0单播；1组播；2可视对讲
         @param   IN    pFun				码流回调函数				
         @param   IN    pCBParam			码流回调用户参数
         @param   IN	nTimeout			超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        //public extern static IntPtr DPSDK_InviteVtCall(IntPtr nPDLLHandle,ref UInt32 audioSessionId,ref UInt32 videoSessionId,ref InviteVtCallParam_t nInviteVtCallParam,dpsdk_call_type_e nCallType,fMediaDataCallback funCB,IntPtr pCBParam,IntPtr nTimeout);
        public extern static IntPtr DPSDK_InviteVtCall(IntPtr nPDLLHandle, ref UInt32 audioSessionId, ref UInt32 videoSessionId, IntPtr nInviteVtCallParam, dpsdk_call_type_e nCallType, fMediaDataCallback funCB, IntPtr pCBParam, IntPtr nTimeout);

        /** 接受邀请后，挂断可视对讲
         @param   IN	nPDLLHandle			SDK句柄
         @param   IN	szUserId			呼叫者ID
         @param   IN	audioSessionId		语音请求序列号
         @param   IN	videoSessionId		视频请求序列号
         @param   IN    nTid				
         @param   IN	nTimeout			超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ByeVtCall(IntPtr nPDLLHandle, byte[] szUserId, UInt32 audioSessionId, UInt32 videoSessionId, int nTid, IntPtr nTimeout);


        /** 拒绝可视对讲邀请
         @param   IN	nPDLLHandle			SDK句柄
         @param   IN	szUserId			呼叫者ID
         @param   IN    nCallId				呼叫ID				
         @param   IN    nTid				
         @param   IN	nTimeout			超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SendRejectVtCall(IntPtr nPDLLHandle, byte[] szUserId, int nCallId, int dlgId, int nTid, IntPtr nTimeout);

        /** 与可视对讲服务json交互
         @param   IN	nPDLLHandle			SDK句柄
         @param   IN	nSendjson			发送json
         @param   OUT   nRecivejson			应答json								
         @param   IN	nTimeout			超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        //public extern static IntPtr DPSDK_sendVtCallInfo(IntPtr nPDLLHandle,byte[] nSendjson,byte[] nRecivejson,IntPtr nTimeout);
        public extern static IntPtr DPSDK_sendVtCallInfo(IntPtr nPDLLHandle, byte[] nSendjson, [MarshalAs(UnmanagedType.LPStr)] StringBuilder nRecivejson, IntPtr nTimeout);


        /** 修改可视对讲状态
         @param   IN	nPDLLHandle			SDK句柄
         @param   IN	szUserId			呼叫者ID
         @param   IN	nCallStatus			呼叫状态
         @param   IN	audioSessionId		语音请求序列号
         @param   IN	videoSessionId		视频请求序列号		
         @param   IN	nTimeout			超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ModifyVtCallStatus(IntPtr nPDLLHandle, byte[] szUserId, dpsdk_call_status_e nCallStatus, UInt32 audioSessionId, UInt32 videoSessionId, IntPtr nTimeout);


        /** 连接SCS 
         @param		IN	nPDLLHandle		SDK句柄
         @param		IN	szScsIp			服务IP地址
         @param		IN	nScsPort		服务端口号.
         @return	函数返回错误类型，参考dpsdk_retval_e
         @remark
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ConnectToSCS(IntPtr nPDLLHandle, byte[] szScsIp, Int32 nScsPort, IntPtr nTimeout);

        /** 连接SCS 
         @param		IN	nPDLLHandle		SDK句柄
         @param		IN	szScsIp			服务IP地址
         @param		IN	nScsPort		服务端口号.
         @return	函数返回错误类型，参考dpsdk_retval_e
         @remark
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ConnectToSCSWithCallNumber(IntPtr nPDLLHandle, byte[] szScsIp, Int32 nScsPort, byte[] szCallNumber, IntPtr nTimeout);

        ///** 获取语音发送函数指针
        // @param   IN	nPDLLHandle		SDK句柄
        // @param   OUT	AudioSendFun	语音发送函数指针，参见fAudioDataCallback
        // @param   OUT	pUserParam   	回调函数用户参数
        // @return  函数返回错误类型，参考dpsdk_retval_e
        // @remark		
        //*/
        //DPSDK_DLL_API int32_t DPSDK_CALL_METHOD DPSDK_GetVtCallAudioSendFunCallBack(int32_t nPDLLHandle,
        //																			void* &AudioSendFun,
        //																			AudioUserParam_t* &pUserParam);

        /** 开始语音对讲
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	nTalkSeq		码流请求序号,可作为后续操作标识 
         @param   IN	pGetInfo		码流请求信息 
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartTalk(IntPtr nPDLLHandle, out Int32 nTalkSeq, /*Get_TalkStream_Info_t*/IntPtr pGetInfo, IntPtr nTimeout);


        //集群对讲

        /** 开始呼叫
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	nCallSeq		码流请求序号,作为后续操作标识 
         @param   IN	nCallType		呼叫类型 0单播；1组播；2可视对讲
         @param   IN	szGroupId		呼叫组ID
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartCallExt(IntPtr nPDLLHandle, out Int32 nCallSeq, dpsdk_call_type_e nCallType, byte[] szGroupId, IntPtr nTimeout);

        /** 呼叫邀请
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	nCallSeq		码流请求序号,可作为后续操作标识
         @param   IN	pGetInfo		呼叫邀请信息 
         @param   IN    RecordParam		本地音频采集参数				
         @param   IN    pUser			码流回调用户参数
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_InviteCallExt(IntPtr nPDLLHandle, out Int32 nCallSeq, IntPtr pGetInfo, IntPtr RecordParam, IntPtr pUser, IntPtr nTimeout);

        /** 请求可视对讲
         @param   IN	nPDLLHandle			SDK句柄
         @param   OUT	audioSessionId		音频请求序号,用于关闭对讲
         @param   OUT	videoSessionId		视频请求序号,用于关闭对讲
         @param   OUT	nStartVtCallParm	应答参数，用于本地频频采集和关闭对讲
         @param   IN	nCallType			呼叫类型 0单播；1组播；2可视对讲
         @param   IN	szUserId			呼叫者ID
         @param   IN	nTimeout			超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StartVtCallExt(IntPtr nPDLLHandle, out UInt32 audioSessionId, out UInt32 videoSessionId, ref StartVtCallParam_t pStartVtCallParm, dpsdk_call_type_e nCallType, string szUserId, IntPtr nTimeout);

        /** 接受可视对讲邀请
         @param   IN	nPDLLHandle			SDK句柄
         @param   OUT	audioSessionId		音频请求序号,用于关闭对讲
         @param   OUT	videoSessionId		视频请求序号,用于关闭对讲
         @param   IN	nInviteVtCallParam	对讲参数，由DPSDK_SetVtCallInviteCallback获取
         @param   IN	nCallType			呼叫类型 0单播；1组播；2可视对讲
         @param	  IN	hDestWnd			显示窗口句柄
         @param   IN	nTimeout			超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_InviteVtCallExt(IntPtr nPDLLHandle, out UInt32 audioSessionId, out UInt32 videoSessionId, ref InviteVtCallParam_t pInviteVtCallParam, dpsdk_call_type_e nCallType, IntPtr nTimeout);

        [DllImport("DPSDK_Ext.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ClearVtCallSession(IntPtr nPDLLHandle, UInt32 audioSessionId);




        /** 查询统计总数.
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	szCameraId		通道ID
         @param   OUT	nQuerySeq		码流请求序号,可作为后续操作标识 
         @param   OUT	totalCount		统计总数
         @param   IN	nStartTime		开始时间 
         @param   IN    nEndTime		结束时间				
         @param   IN    nGranularity	查询粒度，0:分钟,1:小时,2:日,3:周,4:月,5:季,6:年;
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryPersonCount(IntPtr nPDLLHandle, byte[] szCameraId, out UInt32 nQuerySeq, out UInt32 totalCount, UInt32 nStartTime, UInt32 nEndTime, int nGranularity, IntPtr nTimeout);

        /** 分页查询统计结果.
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	szCameraId		通道ID
         @param   IN	nQuerySeq		码流请求序号,可作为后续操作标识 
         @param   IN	nIndex			此次查询的开始值
         @param   IN    nCount			此次查询的数量		
         @param   OUT	nPersonInfo		详细的人数统计信息，new一个nCount大小的数组
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        //public extern static IntPtr DPSDK_QueryPersonCountBypage(IntPtr nPDLLHandle, byte[] szCameraId, UInt32 nQuerySeq, UInt32 nIndex, UInt32 nCount, Person_Count_Info_t* nPersonInfo, IntPtr nTimeout);
        public extern static IntPtr DPSDK_QueryPersonCountBypage(IntPtr nPDLLHandle, byte[] szCameraId, UInt32 nQuerySeq, UInt32 nIndex, UInt32 nCount, IntPtr nPersonInfo, IntPtr nTimeout);

        /** 结束查询.
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	szCameraId		通道ID
         @param   IN	nQuerySeq		码流请求序号,可作为后续操作标识 			
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_StopQueryPersonCount(IntPtr nPDLLHandle, byte[] szCameraId, UInt32 nQuerySeq, IntPtr nTimeout);

        /** 设置Json协议回调.
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	fun				回调函数
         @param   IN	pUser			用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e	
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetGeneralJsonTransportCallback(IntPtr nPDLLHandle, fDPSDKGeneralJsonTransportCallback fun, IntPtr pUser);

        ///** 设置Json协议回调.
        // @param   IN	nPDLLHandle		SDK句柄
        // @param   IN	fun				回调函数
        // @param   IN	pUser			用户参数
        // @return  函数返回错误类型，参考dpsdk_retval_e	
        //*/
        //[DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        //public extern static IntPtr DPSDK_SetGeneralJsonTransportCallbackEx(IntPtr nPDLLHandle,fDPSDKGeneralJsonTransportCallbackEx fun,IntPtr pUser);

        /**通过Json协议发送命令给平台cms/dms等模块,返回结果是json串通过DPSDK_SetGeneralJsonTransportCallback回调
        @param   IN    nPDLLHandle		SDK句柄
        @param   IN    szJson			Json字符串,
        @param   IN    mdltype			模块,
        @param   IN    trantype			JSON 传输类型,
        @param	 IN    nTimeout			超时时间
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark 
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GeneralJsonTransport(IntPtr nPDLLHandle, byte[] szJson, dpsdk_mdl_type_e mdltype, generaljson_trantype_e trantype, IntPtr nTimeout);

        // 监狱子模块接口 开始


        /** 获取刻录实时状态信息.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	pDevBurnerCDStateRequest			请求信息
         @param	  OUT	pDevBurnerCDStateResponse			回复信息
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetDevBurnerCDState(IntPtr nPDLLHandle, ref Dev_Burner_CDState_Request_t pDevBurnerCDStateRequest, ref Dev_Burner_CDState_Reponse_t pDevBurnerCDStateResponse, IntPtr nTimeout);

        /** 刻录控制.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	pDevBurnerCDStateRequest			请求信息
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ControlDevBurner(IntPtr nPDLLHandle, ref Control_Dev_Burner_Request_t pControlDevBurnerRequest, IntPtr nTimeout);

        /** 刻录片头设置.
         @param	  IN	nPDLLHandle		SDK句柄
         @param	  IN	pInfoHeader		片头信息
         @param	  IN	pAttrName		审讯表单属性名
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetDevBurnerHeader(IntPtr nPDLLHandle, ref DevBurnerInfoHeader_t pInfoHeader, ref TrialFormAttrName_t pAttrName, IntPtr nTimeout);

        /** 获取设备磁盘信息数量
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	szDevId			设备ID
         @param   INOUT	pInfoCount		磁盘信息数量
         @param   INOUT pSequence		异步顺序码
         @param   IN	nTimeout		超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetDeviceDiskInfoCount(IntPtr nPDLLHandle, byte[] szDevId, out Int32 nInfoCount, out Int32 nSequence, IntPtr nTimeout);

        /** 获取设备磁盘信息
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN    nSequence		异步顺序码
         @param   INOUT	pDiskInfo		磁盘信息
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  需要先调用DPSDK_GetDeviceDiskInfoCount获取磁盘信息数量，pDiskInfo根据数量申请相应内存
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetDeviceDiskInfo(IntPtr nPDLLHandle, Int32 nSequence, ref Device_Disk_Info_t pDiskInfo);

        // 监狱子模块接口 结束


        /** 根据时间段查询FTP图片信息
         @param   IN    nPDLLHandle     SDK句柄
         @param   IN    szCameraId      通道编号
         @param   IN    nBeginTime      开始时间
         @param   IN    nEndTime		结束时间
         @param   IN    nTimeout        超时时长，单位毫秒
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryFtpPic(IntPtr nPDLLHandle, byte[] szCameraId, UInt64 nBeginTime, UInt64 nEndTime, IntPtr nTimeout);

        /** 获取Ftp图片信息.
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	ftpPicInfo		Ftp图片信息
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
           1、必须先查询后获取
           2、DPSDK_QueryFtpPic会返回记录个数,
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetFtpPicInfo(IntPtr nPDLLHandle, ref Ftp_Pic_Info_t ftpPicInfo);

        /** 删除FTP图片信息
         @param   IN    nPDLLHandle     SDK句柄
         @param   IN    szFtpPicPath	Ftp图片路径
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_DelFtpPic(IntPtr nPDLLHandle, byte[] szFtpPicPath);

        /** 图片上传至Ftp
        @param    IN    nPDLLHandle    SDK句柄
        @param    IN    szCameraId     截图来源的那个CameraId
        @param    IN    nCaptureTime   截图的时间
        @param    IN    szLocalFile    本地图片的全路径
        @param    IN    szFtpAddr      ftp的地址，形式如"ftp://172.7.2.249"
        @param    IN    szName         ftp的登陆名
        @param    IN    szPwd          ftp的登陆密码
        @return   函数返回错误类型，参考dpsdk_retval_e
        @remark
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_UploadFtpPic(IntPtr nPDLLHandle, byte[] szCameraId, UInt64 nCaptureTime, byte[] szLocalFile, byte[] szFtpAddr, byte[] szName, byte[] szPwd, IntPtr nTimeout);

        /** 操作Ftp文件
        @param   IN    nPDLLHandle     SDK句柄
        @param   IN    szLocalFile	   要上传或下载后的本地文件名称
        @param   IN    szFtpFile	   要操作的Ftp目录上的文件完整路径（以ftp:开头）
        @param   IN    szName	       登陆Ftp的用户名
        @param   IN    szPwd	       登陆Ftp的密码
        @param   IN    opType	       操作类型，包括上传，下载，删除。
        @return  函数返回错误类型，参考dpsdk_retval_e
        @remark 
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_OperatorFtpFile(IntPtr nPDLLHandle, byte[] szLocalFile, byte[] szFtpFile, byte[] szName, byte[] szPwd, dpsdk_operator_ftp_type_e opType, IntPtr nTimeout);


        /** 图片上传到Ftp的URL
        @param    IN    nPDLLHandle    SDK句柄
        @param    IN    szCameraId     截图来源的那个CameraId
        @param    IN    nCaptureTime   截图的时间
        @param    IN    szLocalFile    本地图片的全路径
        @param    IN    szFtpAddr      ftp的地址，形式如"ftp://172.7.2.249"
        @param	  OUT	szFtpUrl	   URL
        @param	  IN	nUrlLen		   URL字符串长度
        @return   函数返回错误类型，参考dpsdk_retval_e
        @remark
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetUploadFtpPicUrl(IntPtr nPDLLHandle, byte[] szCameraId, UInt64 nCaptureTime, byte[] szLocalFile, byte szFtpAddr, [MarshalAs(UnmanagedType.LPStr)] StringBuilder szFtpUrl, Int32 nUrlLen, IntPtr nTimeout);


        /** 根据设备ID获取报警输入通道信息
         @param   IN	nPDLLHandle		SDK句柄
         @param   OUT	pstruUserInfo	用户信息结构体
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark		
         1、pAlarmInChannelnfo需要在外面创建好
         2、pAlarmInChannelnfo的个数与DPSDK_GetDGroupInfo返回时有报警主机设备id和报警输入通道个数
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetAlarmInChannelInfo(IntPtr nPDLLHandle, /*INOUT Get_AlarmInChannel_Info_t* */ref Get_AlarmInChannel_Info_t pstruUserInfo);

        /** 查询网络报警主机状态
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	szDeviceId		设备id
         @param   IN	nChannelcount	通道个数
         @param   OUT	pDefence		通道信息结构体
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark
         1、pDefence需要在外面创建好，根据通道个数new
         2、没有单独设备的布撤防状态，需要根据通道的状态判断
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryNetAlarmHostStatus(IntPtr nPDLLHandle, byte[] szDeviceId, Int32 nChannelcount, /*OUT dpsdk_AHostDefenceStatus_t* */ IntPtr pDefence, IntPtr nTimeout);

        /** 查询网络报警主机状态包含的通道个数
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	szDeviceId		设备id
         @param   OUT	pChannelcount	通道个数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark
         1、pDefence需要在外面创建好，根据通道个数new
         2、没有单独设备的布撤防状态，需要根据通道的状态判断
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_QueryNetAlarmHostChannelCount(IntPtr nPDLLHandle, byte[] szDeviceId, IntPtr pChannelcount, IntPtr nTimeout);


        /** 获取网络报警主机所有状态
         @param   IN	nPDLLHandle		SDK句柄
         @param   IN	nChannelcount	通道个数
         @param   OUT	pDefence		通道信息结构体
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark
         1、先调用DPSDK_QueryNetAlarmHostChannelCount获取通道个数nChannelcount
         2、然后new一个nChannelcount大小的结构体数组dpsdk_AHostDefenceStatus_t指针pDefence
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_GetNetAlarmHostStatus(IntPtr nPDLLHandle, Int32 nChannelcount,/*dpsdk_AHostDefenceStatus_t*  */ IntPtr pDefence);

        /** 设置视频报警主机布撤防/旁路状态回调.
         @param   IN    nPDLLHandle     SDK句柄
         @param   IN    fun             回调函数
         @param   IN    pUser           用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
         1、登陆平台的时候平台先回调设备布撤防状态，接着回调通道旁路状态
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetVideoAlarmHostStatusCallback(IntPtr nPDLLHandle, fDPSDKVideoAlarmHostStatusCallback fun, IntPtr pUser);

        /** 设置网络报警主机布撤防/旁路状态回调.
         @param   IN    nPDLLHandle     SDK句柄
         @param   IN    fun             回调函数
         @param   IN    pUser           用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
         1、有其他客户端修改了网络报警主机状态以后，回调通知
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_SetNetAlarmHostStatusCallback(IntPtr nPDLLHandle, fDPSDKNetAlarmHostStatusCallback fun, IntPtr pUser);

        /** 报警订阅设置-报警运营平台.
         @param   IN    nPDLLHandle     SDK句柄
         @param   IN    userParam       用户参数
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_PhoneSubscribeAlarm(IntPtr nPDLLHandle, ref dpsdk_phone_subscribe_alarm_t userParam, IntPtr nTimeout);

        /** 视频报警主机：设备布撤防/通道旁路/消除通道报警.
         @param   IN    nPDLLHandle     SDK句柄
         @param   IN    szDeviceId      设备id
         @param   IN    channelId       通道号，-1表示对设备操作
         @param   IN    controlType     操作类型, 参考dpsdk_AlarmhostOperator_e
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
         1、视频报警主机类型为:1101
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ControlVideoAlarmHost(IntPtr nPDLLHandle, byte[] szDeviceId, Int32 channelId, Int32 controlType, IntPtr nTimeout);

        /** 网络报警主机：设备布撤防/通道旁路/消除通道报警.
         @param   IN    nPDLLHandle     SDK句柄
         @param   IN    szId			设备id/通道id
         @param   IN    opttype			设备/通道操作,参考dpsdk_netalarmhost_operator_e
         @param   IN    controlType     操作类型, 参考dpsdk_netalarmhost_cmd_e
         @param   IN    iStart			开始时间,默认为0
         @param   IN    iEnd			结束时间,默认为0
         @return  函数返回错误类型，参考dpsdk_retval_e
         @remark  
         1、网络报警主机类型为:601
        */
        [DllImport("DPSDK_Core.dll", CharSet = CharSet.Ansi)]
        public extern static IntPtr DPSDK_ControlNetAlarmHostCmd(IntPtr nPDLLHandle, byte[] szId, Int32 opttype, Int32 controlType, Int64 iStart, Int64 iEnd, IntPtr nTimeout);



        //////////////////////////////////////////////////////////////////////////


        // 电视墙屏窗口关闭视频源
        public struct TvWall_Screen_Close_Source_t
        {
            public UInt32 nTvWallId;									// 电视墙ID
            public UInt32 nScreenId;									// 屏ID
            public UInt32 nWindowId;									// 窗口ID(若窗口ID=-1则表示关闭该屏中所有窗口的视频源)
        }

        // 电视墙屏窗口设置视频源
        [StructLayout(LayoutKind.Sequential)]
        public struct Set_TvWall_Screen_Window_Source_t
        {
            public UInt32 nTvWallId;									// 电视墙ID
            public UInt32 nScreenId;									// 屏ID
            public UInt32 nWindowId;									// 窗口ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;			                        // 通道ID
            public dpsdk_stream_type_e enStreamType;					// 码流类型
            public UInt64 nStayTime;									// 停留时间
        }

        // 电视墙屏关窗信息
        [StructLayout(LayoutKind.Sequential)]
        public struct TvWall_Screen_Close_Window_t
        {
            public UInt32 nTvWallId;									// 电视墙ID
            public UInt32 nScreenId;									// 屏ID
            public UInt32 nWindowId;									// 窗口ID
        }

        // 编码通道信息 扩展
        [StructLayout(LayoutKind.Sequential)]
        public struct Enc_Channel_Info_Ex_t
        {
            public dpsdk_camera_type_e nCameraType;		            // 类型，参见CameraType_e
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szId;				                        // 通道ID:设备ID+通道号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szName;	                                // 名称
            public UInt64 nRight;                                   // 权限信息
            public int nChnlType;                                   // 通道类型
            public int nStatus;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szChnlSN;			                        // 互联编码SN
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szLatitude;		                        // 纬度
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szLongitude;		                        // 经度
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szMulticastIp;		                    // 组播IP
            public int nMulticastPort;		                        // 组播端口
        }

        //电子地图服务配置信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Config_Emap_Addr_Info_t
        {
            // OUT char			                szIP[DPSDK_CORE_IP_LEN];		            // 电子地图服务IP地址
            [MarshalAs(UnmanagedType.LPStr)]
            StringBuilder szIP;                                    // 电子地图服务IP地址
            public IntPtr nPort;										// 电子地图服务端口号
        }

        // 获取编码通道请求信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Get_Channel_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szDeviceId;                                   // 设备ID
            public UInt32 nEncChannelChildCount;                        // 通道个数
                                                                        //OUT Enc_Channel_Info_t*				pEncChannelnfo;								// 通道信息
            public IntPtr pEncChannelnfo;								// 通道信息
        }

        // 获取编码通道请求信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Get_Dep_Channel_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCoding;                                 // 节点code
            public UInt32 nEncChannelChildCount;                        // 通道个数
                                                                        //OUT Enc_Channel_Info_t*				pEncChannelnfo;								    // 通道信息
            public IntPtr pEncChannelnfo;								// 通道信息
        }

        // 获取通道请求信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Get_Channel_Info_Ex_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szDeviceId;                                   // 设备ID
            public UInt32 nEncChannelChildCount;                        // 通道个数
                                                                        // OUT Enc_Channel_Info_Ex_t*			pEncChannelnfo;								// 通道信息
            public IntPtr pEncChannelnfo;								// 通道信息
        }

        // 获取设备支持码流类型
        [StructLayout(LayoutKind.Sequential)]
        public struct Get_Dev_StreamType_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szDeviceId;                                   // 设备ID
            public Int32 nUnitNo;                                   // 单元序号
                                                                    //OUT dpsdk_stream_type_e				nStreamType;								// 支持的码流类型
            public dpsdk_stream_type_e nStreamType;								// 支持的码流类型
        }

        // 设备状态
        public enum dpsdk_dev_status_e
        {
            DPSDK_DEV_STATUS_UNKNOW = 0,                         // 未知状态
            DPSDK_DEV_STATUS_ONLINE = 1,					     // 在线
            DPSDK_DEV_STATUS_OFFLINE = 2,					     // 离线

            // 废弃
            DPSDK_CORE_DEVICE_STATUS_ONLINE = DPSDK_DEV_STATUS_ONLINE,
            DPSDK_CORE_DEVICE_STATUS_OFFLINE = DPSDK_DEV_STATUS_OFFLINE,
        }

        // 设备类型，需要和web统一
        public enum dpsdk_dev_type_e
        {
            DEV_TYPE_ENC_BEGIN = 0,		// 编码设备
            DEV_TYPE_DVR = DEV_TYPE_ENC_BEGIN + 1,			// DVR
            DEV_TYPE_IPC = DEV_TYPE_ENC_BEGIN + 2,			// IPC
            DEV_TYPE_NVS = DEV_TYPE_ENC_BEGIN + 3,			// NVS
            DEV_TYPE_MCD = DEV_TYPE_ENC_BEGIN + 4,			// MCD
            DEV_TYPE_MDVR = DEV_TYPE_ENC_BEGIN + 5,			// MDVR
            DEV_TYPE_NVR = DEV_TYPE_ENC_BEGIN + 6,			// NVR
            DEV_TYPE_SVR = DEV_TYPE_ENC_BEGIN + 7,			// SVR
            DEV_TYPE_PCNVR = DEV_TYPE_ENC_BEGIN + 8,			// PCNVR，PSS自带的一个小型服务
            DEV_TYPE_PVR = DEV_TYPE_ENC_BEGIN + 9,			// PVR
            DEV_TYPE_EVS = DEV_TYPE_ENC_BEGIN + 10,			// EVS
            DEV_TYPE_MPGS = DEV_TYPE_ENC_BEGIN + 11,			// MPGS
            DEV_TYPE_SMART_IPC = DEV_TYPE_ENC_BEGIN + 12,			// SMART_IPC
            DEV_TYPE_SMART_TINGSHEN = DEV_TYPE_ENC_BEGIN + 13,			// 庭审主机
            DEV_TYPE_SMART_NVR = DEV_TYPE_ENC_BEGIN + 14,			// SMART_NVR
            DEV_TYPE_PRC = DEV_TYPE_ENC_BEGIN + 15,			// 防护舱
            DEV_TYPE_JT808 = DEV_TYPE_ENC_BEGIN + 18,			// 部标JT808
            DEV_TYPE_FISH_EYE = DEV_TYPE_ENC_BEGIN + 19,			// 鱼眼设备
            DEV_TYPE_VTS = DEV_TYPE_ENC_BEGIN + 20,			// VTS
            DEV_TYPE_VTT = DEV_TYPE_ENC_BEGIN + 21,			// VTT
            DEV_TYPE_HCVR = DEV_TYPE_ENC_BEGIN + 22,			// 海康CVR类型
            DEV_TYPE_IF = DEV_TYPE_ENC_BEGIN + 23,			// 智能ATM
            DEV_TYPE_VTO = DEV_TYPE_ENC_BEGIN + 24,			// 金融VTO，当做编码器小类接入
            DEV_TYPE_VTA = DEV_TYPE_ENC_BEGIN + 25,          // VTA
            DEV_TYPE_TC = DEV_TYPE_ENC_BEGIN + 26,          // 热成像设备
            DEV_TYPE_DSJ = DEV_TYPE_ENC_BEGIN + 27,			// DSJ
            DEV_TYPE_GLASSES = DEV_TYPE_ENC_BEGIN + 28,			// 眼镜设备 
            DEV_TYPE_VTT2610C = DEV_TYPE_ENC_BEGIN + 29,			// VTT2610C
            DEV_TYPE_APP = DEV_TYPE_ENC_BEGIN + 30,			// -F保险项目新增APP编码小类
            DEV_TYPE_ENCRYPT_IPC = DEV_TYPE_ENC_BEGIN + 31,			// 加密IPC
            DEV_TYPE_ENCRYPT_NVR = DEV_TYPE_ENC_BEGIN + 32,			// 加密NVR
            DEV_TYPE_LECHENG_CLOUD = DEV_TYPE_ENC_BEGIN + 33,			// 乐橙云
            DEV_TYPE_MCS = DEV_TYPE_ENC_BEGIN + 35,			// MCS

            DEV_TYPE_MASTERSLAVE = DEV_TYPE_ENC_BEGIN + 39,          // 守望者设备
            DEV_TYPE_FIREWARN = DEV_TYPE_ENC_BEGIN + 40,			// 森林防火设备

            DEV_TYPE_360ANGLE_WATCHER = DEV_TYPE_ENC_BEGIN + 41,			// -C 360°守望者设备
            DEV_TYPE_RPU = DEV_TYPE_ENC_BEGIN + 42,			// 能源RPU设备
            DEV_TYPE_UAV = DEV_TYPE_ENC_BEGIN + 47,			// 无人机设备
            DEV_TYPE_HS = DEV_TYPE_ENC_BEGIN + 48,          // 环视摄像机
            DEV_TYPE_HXIPC = DEV_TYPE_ENC_BEGIN + 50,          // 华消烟感IPC
            DEV_TYPE_RADAR_PTZ = DEV_TYPE_ENC_BEGIN + 51,			// 雷球设备

            DEV_TYPE_ENC_END,

            DEV_TYPE_TVWALL_BEGIN = 100,
            DEV_TYPE_BIGSCREEN = DEV_TYPE_TVWALL_BEGIN + 1,		// 大屏
            DEV_TYPE_TVWALL_END,

            DEV_TYPE_DEC_BEGIN = 200,		// 解码设备
            DEV_TYPE_NVD = DEV_TYPE_DEC_BEGIN + 1,			// NVD
            DEV_TYPE_SNVD = DEV_TYPE_DEC_BEGIN + 2,			// SNVD
            DEV_TYPE_UDS = DEV_TYPE_DEC_BEGIN + 5,			// UDS
            DEV_TYPE_DS_6304D_T = DEV_TYPE_DEC_BEGIN + 6,			// DS_6304D_T
            DEV_TYPE_AB80 = DEV_TYPE_DEC_BEGIN + 8,			// AB80
            DEV_TYPE_DEC_END,

            DEV_TYPE_MATRIX_BEGIN = 300,		// 矩阵设备
            DEV_MATRIX_M60 = DEV_TYPE_MATRIX_BEGIN + 1,		// M60
            DEV_MATRIX_NVR6000 = DEV_TYPE_MATRIX_BEGIN + 2,		// NVR6000
            DEV_MATRIX_B10_INTEGRATION = DEV_TYPE_MATRIX_BEGIN + 3,		// 海康B10一体机
            DEV_MATRIX_B10_PLATFORM = DEV_TYPE_MATRIX_BEGIN + 4,		// 海康B10视频综合平台
            DEV_MATRIX_REDAPPLE = DEV_TYPE_MATRIX_BEGIN + 5,		// 红苹果设备
            //DEV_MATRIX_PEARMAIN			= DEV_TYPE_MATRIX_BEGIN + 5,
            DEV_MATRIX_MAX1000 = DEV_TYPE_MATRIX_BEGIN + 6,
            DEV_MATRIX_B20_INTEGRATION = DEV_TYPE_MATRIX_BEGIN + 7,		// 海康B20一体机
            DEV_MATRIX_B20_PLATFORM = DEV_TYPE_MATRIX_BEGIN + 8,		// 海康B20视频综合平台
            DEV_MATRIX_M70 = DEV_TYPE_MATRIX_BEGIN + 9,		// M70
            DEV_MATRIX_NVD0405 = DEV_TYPE_MATRIX_BEGIN + 10,		// NVD0405以矩阵方式接入，处理流程同NVD
            DEV_MATRIX_L80 = DEV_TYPE_MATRIX_BEGIN + 11,		// L80
            DEV_MATRIX_H265 = DEV_TYPE_MATRIX_BEGIN + 12,		// 新型审讯主机：DH-HVR0404FE-S-H
            DEV_TYPE_MATRIX_END,

            DEV_TYPE_IVS_BEGIN = 400,		// 智能设备
            DEV_TYPE_ISD = DEV_TYPE_IVS_BEGIN + 1,			// ISD 智能球
            DEV_TYPE_IVS_B = DEV_TYPE_IVS_BEGIN + 2,			// IVS-B 行为分析服务
            DEV_TYPE_IVS_V = DEV_TYPE_IVS_BEGIN + 3,			// IVS-V 视频质量诊断服务
            DEV_TYPE_IVS_FR = DEV_TYPE_IVS_BEGIN + 4,			// IVS-FR 人脸识别服务
            DEV_TYPE_IVS_PC = DEV_TYPE_IVS_BEGIN + 5,			// IVS-PC 人流量统计服务
            DEV_TYPE_IVS_M = DEV_TYPE_IVS_BEGIN + 6,			// IVS_M 主从跟踪智能盒
            DEV_TYPE_IVS_PC_BOX = DEV_TYPE_IVS_BEGIN + 7,			// IVS-PC 智能盒 
            DEV_TYPE_IVS_B_BOX = DEV_TYPE_IVS_BEGIN + 8,			// IVS-B 智能盒
            DEV_TYPE_IVS_M_BOX = DEV_TYPE_IVS_BEGIN + 9,			// IVS-M 盒子
            DEV_TYPE_IVS_PRC = DEV_TYPE_IVS_BEGIN + 10,			// 防护舱
            DEV_TYPE_IVS_IF = DEV_TYPE_IVS_BEGIN + 11,			// IVS_IF
            DEV_TYPE_IVS_IPC = DEV_TYPE_IVS_BEGIN + 12,			// IVS_IPC
            DEV_TYPE_IVS_SmartIPC = DEV_TYPE_IVS_BEGIN + 13,			// IVS_SmartIPC
            DEV_TYPE_IVS_FVM = DEV_TYPE_IVS_BEGIN + 14,			// 全景拼接
            DEV_TYPE_IVS_IVSS = DEV_TYPE_IVS_BEGIN + 15,			// IVSS人脸服务器
            DEV_TYPE_IVS_IPC8249 = DEV_TYPE_IVS_BEGIN + 16,			// 8249FR  IPC
            DEV_TYPE_IVS_NVR = DEV_TYPE_IVS_BEGIN + 17,			// 人脸NVR
            DEV_TYPE_IVS_FD = DEV_TYPE_IVS_BEGIN + 18,			// 7200人脸检测服务器
            DEV_TYPE_IVS_CS = DEV_TYPE_IVS_BEGIN + 19,			// 客流统计
            DEV_TYPE_IVS_LINGTONG = DEV_TYPE_IVS_BEGIN + 20,			// 灵瞳设备
            DEV_TYPE_IVS_NVR5X_I = DEV_TYPE_IVS_BEGIN + 21,			// NVR5X-I人脸设备
            DEV_TYPE_IVS_HFS = DEV_TYPE_IVS_BEGIN + 22,			// IPC达芬奇
            DEV_TYPE_IVS_IVSS_EX = DEV_TYPE_IVS_BEGIN + 23,			// 用于视频分析的IVSS
            DEV_TYPE_IVS_END,

            DEV_TYPE_BAYONET_BEGIN = 500,		// -C相关设备
            DEV_TYPE_CAPTURE = DEV_TYPE_BAYONET_BEGIN + 1,		// 卡口设备
            DEV_TYPE_SPEED = DEV_TYPE_BAYONET_BEGIN + 2,		// 测速设备
            DEV_TYPE_TRAFFIC_LIGHT = DEV_TYPE_BAYONET_BEGIN + 3,		// 闯红灯设备
            DEV_TYPE_INCORPORATE = DEV_TYPE_BAYONET_BEGIN + 4,		// 一体化设备
            DEV_TYPE_PLATEDISTINGUISH = DEV_TYPE_BAYONET_BEGIN + 5,		// 车牌识别设备
            DEV_TYPE_VIOLATESNAPPIC = DEV_TYPE_BAYONET_BEGIN + 6,		// 违停检测设备
            DEV_TYPE_PARKINGSTATUSDEV = DEV_TYPE_BAYONET_BEGIN + 7,		// 车位检测设备
            DEV_TYPE_ENTRANCE = DEV_TYPE_BAYONET_BEGIN + 8,		// 出入口设备
            DEV_TYPE_VIOLATESNAPBALL = DEV_TYPE_BAYONET_BEGIN + 9,		// 违停抓拍球机
            DEV_TYPE_THIRDBAYONET = DEV_TYPE_BAYONET_BEGIN + 10,		// 第三方卡口设备
            DEV_TYPE_ULTRASONIC = DEV_TYPE_BAYONET_BEGIN + 11,		// 超声波车位检测器
            DEV_TYPE_FACE_CAPTURE = DEV_TYPE_BAYONET_BEGIN + 12,		// 人脸抓拍设备
            DEV_TYPE_ITC_SMART_NVR = DEV_TYPE_BAYONET_BEGIN + 13,		// 卡口智能NVR设备
            DEV_TYPE_PARKINGAREASNAP = DEV_TYPE_BAYONET_BEGIN + 14,		// 停车场区域抓拍设备
            DEV_TYPE_ITC_EVS = DEV_TYPE_BAYONET_BEGIN + 15,		// EVS
            DEV_TYPE_FACE_RECOGNISE = DEV_TYPE_BAYONET_BEGIN + 16,		// 人脸识别系统设备
            DEV_TYPE_IPC_CAPTURE = DEV_TYPE_BAYONET_BEGIN + 17,		// 球机卡口设备
            DEV_TYPE_BAYONET_END,

            DEV_TYPE_ALARM_BEGIN = 600,		// 报警设备
            DEV_TYPE_ALARMHOST = DEV_TYPE_ALARM_BEGIN + 1,			// 报警主机
            DEV_TYPE_ALARMSTB = DEV_TYPE_ALARM_BEGIN + 2,			// 机顶盒
            DEV_TYPE_ALARMSEQUENCE = DEV_TYPE_ALARM_BEGIN + 4,			// 时序器
            DEV_TYPE_ALARM_END,

            DEV_TYPE_DOORCTRL_BEGIN = 700,
            DEV_TYPE_DOORCTRL_DOOR = DEV_TYPE_DOORCTRL_BEGIN + 1,		// 门禁
            DEV_TYPE_DOORCTRL_RFID = DEV_TYPE_DOORCTRL_BEGIN + 8,		//RFID作为门禁接入(易亿项目)
            DEV_TYPE_DOORCTRL_END,

            DEV_TYPE_PE_BEGIN = 800,
            DEV_TYPE_PE_PE = DEV_TYPE_PE_BEGIN + 1,			// 动环
            DEV_TYPE_PE_AE6016 = DEV_TYPE_PE_BEGIN + 2,			// AE6016设备
            DEV_TYPE_PE_NVS = DEV_TYPE_PE_BEGIN + 3,			// 带动环功能的NVS设备
            DEV_TYPE_PE_END,

            DEV_TYPE_VOICE_BEGIN = 900,		// ip对讲
            DEV_TYPE_VOICE_MIKE = DEV_TYPE_VOICE_BEGIN + 1,
            DEV_TYPE_VOICE_NET = DEV_TYPE_VOICE_BEGIN + 2,
            DEV_TYPE_VOICE_END,

            DEV_TYPE_IP_BEGIN = 1000,		// IP设备（通过网络接入的设备）
            DEV_TYPE_IP_SCNNER = DEV_TYPE_IP_BEGIN + 1,			// 扫描枪
            DEV_TYPE_IP_SWEEP = DEV_TYPE_IP_BEGIN + 2,			// 地磅
            DEV_TYPE_IP_POWERCONTROL = DEV_TYPE_IP_BEGIN + 3,			// 电源控制器
            DEV_TYPE_IP_END,

            DEV_TYPE_MULTIFUNALARM_BEGIN = 1100,		// 多功能报警主机
            DEV_TYPE_VEDIO_ALARMHOST = DEV_TYPE_MULTIFUNALARM_BEGIN + 1,	// 视频报警主机
            DEV_TYPE_MULTIFUNALARM_END,

            DEV_TYPE_SLUICE_BEGIN = 1200,
            DEV_TYPE_SLUICE_DEV = DEV_TYPE_SLUICE_BEGIN + 1,		// 出入口道闸设备
            DEV_TYPE_SLUICE_PARKING = DEV_TYPE_SLUICE_BEGIN + 2,		// 停车场道闸设备
            DEV_TYPE_SLUICE_STOPBUFFER = DEV_TYPE_SLUICE_BEGIN + 3,		// 视频档车器
            DEV_TYPE_SLUICE_END,

            DEV_TYPE_ELECTRIC_BEGIN = 1300,
            DEV_TYPE_ELECTRIC_DEV = DEV_TYPE_ELECTRIC_BEGIN + 1,		// 电网设备
            DEV_TYPE_ELECTRIC_END,

            DEV_TYPE_LED_BEGIN = 1400,
            DEV_TYPE_LED_DEV = DEV_TYPE_LED_BEGIN + 1,			//LED屏设备(诱导)
            DEV_TYPE_LED_DEV_REMAIN = DEV_TYPE_LED_BEGIN + 2,			//LED屏设备（余位）
            DEV_TYPE_LED_DEV_GENERAL = DEV_TYPE_LED_BEGIN + 3,			//LED屏设备（通用）
            DEV_TYPE_LED_END,

            DEV_TYPE_VIBRATIONFIBER_BEGIN = 1500,
            DEV_TYPE_VIBRATIONFIBER_DEV = DEV_TYPE_VIBRATIONFIBER_BEGIN + 1,// 震动光纤设备 
            DEV_TYPE_VIBRATIONFIBER_END,

            DEV_TYPE_PATROL_BEGIN = 1600,
            DEV_TYPE_PATROL_DEV = DEV_TYPE_PATROL_BEGIN + 1,		// 巡更棒设备
            DEV_TYPE_PATROL_SPOT = DEV_TYPE_PATROL_BEGIN + 2,		// 巡更点设备
            DEV_TYPE_PATROL_END,

            DEV_TYPE_SENTRY_BOX_BEGIN = 1700,
            DEV_TYPE_SENTRY_BOX_DEV = DEV_TYPE_SENTRY_BOX_BEGIN + 1,	// 哨位箱设备
            DEV_TYPE_SENTRY_BOX_END,

            DEV_TYPE_COURT_BEGIN = 1800,
            DEV_TYPE_COURT_DEV = DEV_TYPE_COURT_BEGIN + 1,			// 庭审设备
            DEV_TYPE_COURT_END,

            DEV_TYPE_VIDEO_TALK_BEGIN = 1900,
            DEV_TYPE_VIDEO_TALK_VTNC = DEV_TYPE_VIDEO_TALK_BEGIN + 1,
            DEV_TYPE_VIDEO_TALK_VTO = DEV_TYPE_VIDEO_TALK_BEGIN + 2,
            DEV_TYPE_VIDEO_TALK_VTH = DEV_TYPE_VIDEO_TALK_BEGIN + 3,
            DEV_TYPE_VIDEO_TALK_ANALOG_VTH = DEV_TYPE_VIDEO_TALK_BEGIN + 4,
            DEV_TYPE_VIDEO_TALK_FENCE_VTO = DEV_TYPE_VIDEO_TALK_BEGIN + 5,
            DEV_TYPE_VIDEO_TALK_DOORLOCK_VTH = DEV_TYPE_VIDEO_TALK_BEGIN + 6,
            DEV_TYPE_VIDEO_TALK_ANALOG_VTO = DEV_TYPE_VIDEO_TALK_BEGIN + 7,	// 半数字门口机 
            DEV_TYPE_VIDEO_TALK_VTS = DEV_TYPE_VIDEO_TALK_BEGIN + 8,	// VTS管理机  
            DEV_TYPE_VIDEO_TALK_SIP_PHONE = DEV_TYPE_VIDEO_TALK_BEGIN + 10,	// 第三方厂家Sip话机  
            DEV_TYPE_VIDEO_TALK_END,

            DEV_TYPE_BROADCAST_BEGIN = 2000,
            DEV_TYPE_BROADCAST_ITC_T6700R = DEV_TYPE_BROADCAST_BEGIN + 1,	// ITC_T6700R广播设备
            DEV_TYPE_BROADCAST_END,

            DEV_TYPE_VIDEO_RECORD_SERVER_BEGIN = 2100,
            DEV_TYPE_VIDEO_RECORD_SERVER_BNVR = DEV_TYPE_VIDEO_RECORD_SERVER_BEGIN + 1, // BNVR设备
            DEV_TYPE_VIDEO_RECORD_SERVER_OE = DEV_TYPE_VIDEO_RECORD_SERVER_BEGIN + 2, // 手术设备(operation equipment)
            DEV_TYPE_VIDEO_RECORD_SERVER_END,

            DEV_TYPE_PROTECT_CABIN_BEGIN = 2200,
            DEV_TYPE_PROTECT_CABIN = DEV_TYPE_PROTECT_CABIN_BEGIN + 1,		// -F,防护舱  指挥调度设备
            DEV_TYPE_PROTECT_CABIN_END,

            DEV_TYPE_RFID_BEGIN = 2300,
            DEV_TYPE_RFID_CARDDISPENSERR = DEV_TYPE_RFID_BEGIN + 1,			//发卡器
            DEV_TYPE_RFID_RECRIVER = DEV_TYPE_RFID_BEGIN + 2,			//接收器
            DEV_TYPE_RFID_WRISTBAND = DEV_TYPE_RFID_BEGIN + 3,			//手环
            DEV_TYPE_RFID_LOCATOR = DEV_TYPE_RFID_BEGIN + 4,			//定位器
            DEV_TYPE_RFID_READER = DEV_TYPE_RFID_BEGIN + 5,			//读卡器
            DEV_TYPE_RFID_ALARM = DEV_TYPE_RFID_BEGIN + 6,			//报警器
            DEV_TYPE_RFID_INTERCOM = DEV_TYPE_RFID_BEGIN + 7,			//对讲机
            DEV_TYPE_RFID_GPSTRACKER = DEV_TYPE_RFID_BEGIN + 8,			//GPS跟踪器
            DEV_TYPE_RFID_VEHICLEGPS = DEV_TYPE_RFID_BEGIN + 9,			//车载GPS
            DEV_TYPE_RFID_TALKGPS = DEV_TYPE_RFID_BEGIN + 10,			//对讲GPS
            DEV_TYPE_RFID_ELEC_FETTERS = DEV_TYPE_RFID_BEGIN + 11,			//电子脚镣
            DEV_TYPE_RFID_END,

            DEV_TYPE_ALARM_STUB_BEGIN = 3400,								// 报警柱设备类
            DEV_TYPE_ALARM_STUB_VTA = DEV_TYPE_ALARM_STUB_BEGIN + 1,
            DEV_TYPE_ALARM_STUB_END,

            DEV_TYPE_MAC_PICK_BEGIN = 3600,								// MAC地址采集设备类
            DEV_TYPE_MAC_PICK = DEV_TYPE_MAC_PICK_BEGIN + 1,
            DEV_TYPE_MAC_PICK_END,


            DEV_TYPE_POS_BEGIN = 4000,
            DEV_TYPE_POS_BOX = DEV_TYPE_POS_BEGIN + 1,		// POS盒子
            DEV_TYPE_POS_END,

            DEV_TYPE_UAV_BEGIN = 4200,								// 无人机设备大类
            DEV_TYPE_UAV_DEV = DEV_TYPE_UAV_BEGIN + 1,			// 无人机设备
            DEV_TYPE_UAV_END,

            DEV_TYPE_TRANSPORT_TOOL_BEGIN = 4500,								// 交通运输工具设备大类
            DEV_TYPE_TRANSPORT_TOOL_DEV = DEV_TYPE_TRANSPORT_TOOL_BEGIN + 1,// 车辆
            DEV_TYPE_TRANSPORT_TOOL_END,

            DEVTYPE_TRANSPORT_STANDARD_BEGIN = 4800,							// 交通部标设备大类
            DEVTYPE_TRANSPORT_STANDARD_DEV = DEVTYPE_TRANSPORT_STANDARD_BEGIN + 1,	//808设备
            DEVTYPE_TRANSPORT_STANDARD_END,

            DEV_TYPE_DISPATCHER_BEGIN = 5000,
            DEV_TYPE_DISPATCHER = DEV_TYPE_DISPATCHER_BEGIN + 1,	//指挥调度设备
            DEV_TYPE_DISPATCHER_END,

        }

        // 设备厂商类型
        public enum dpsdk_device_factory_type_e
        {
            DPSDK_CORE_DEVICE_FACTORY_UNDEFINE = 0,				 // 未定义
            DPSDK_CORE_DEVICE_FACTORY_DAHUA = 1,				 // 大华			
            DPSDK_CORE_DEVICE_FACTORY_HIK = 2,					 // 海康
            DPSDK_CORE_DEVICE_FACTORY_H264 = 16,				 // 国标
            DPSDK_CORE_DEVICE_FACTORY_H3C = 17,					 // 华三
            DPSDK_CORE_DEVICE_FACTORY_XC = 18,					 // 信产
            DPSDK_CORE_DEVICE_FACTORY_LIYUAN = 19,				 // 立元
            DPSDK_CORE_DEVICE_FACTORY_BIT = 20,					 // 比特
            DPSDK_CORE_DEVICE_FACTORY_H3TS = 21,				 // 华三ts流
            DPSDK_CORE_DEVICE_FACTORY_TIANSHI = 36,				 // 天视上传的海康流
        }

        // 图片类型
        public enum dpsdk_pic_type_e
        {
            DPSDK_CORE_PIC_FORMAT_BMP = 0,					 // BMP
            DPSDK_CORE_PIC_FORMAT_JPEG = 1,					 // JPEG
        }

        //设备信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Device_Info_Ex_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szId;                                       // 设备ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szName;	                                  // 名称
            public dpsdk_device_factory_type_e nFactory;			  // 厂商类型
            public int szModel;					                      // 模式
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szUser;			                          // 用户名
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szPassword;		                          // 密码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string szIP;					                      // 设备IP
            public dpsdk_dev_type_e nDevType;						  // 设备type 参考dpsdk_dev_type_e 
            public int nPort;										  // 设备端口
            public int szLoginType;				                      // 登陆类型
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szRegID;				                      // 主动注册设备ID
            public int nProxyPort;					                  // 代理端口
            public int nUnitNum;					                  // 单元数目--对于矩阵设备代表卡槽数
            public dpsdk_dev_status_e nStatus;						  // 设备状态
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCN;				                          // 设备序列号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szSN;   	 		                          // 互联编码SN
            public UInt64 nRight;						              // 权限信息(只有IP对讲设备中的话筒才有)
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string szDevIP;					                  // 设备真实IP
            public int nDevPort;					                  // 设备真实port
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string dev_Maintainer;                             // 设备联系人
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string dev_MaintainerPh;                           // 设备联系人号码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string dev_Location;                               // 设备所在位置
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string desc;                                       // 设备描述 
            public int nEncChannelChildCount;						  // 编码子通道个数
            public int iAlarmInChannelcount;						  // 报警输入通道个数
            public int nSort;									      // 组织排序
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string szCallNum;				                  // 设备呼叫号码
        }

        // 组织下的子组织，通道，设备信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Get_Dep_Info_Ex_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCoding;             // 节点code
            public UInt32 nDepCount;			// 组织个数
            public IntPtr pDepInfo;			    // Dep_Info_t组织信息，在外部创建，如果为NULL则只返回个数
            public UInt32 nDeviceCount;		    // 设备个数
            public IntPtr pDeviceInfo;		    // Device_Info_Ex_t设备信息
            public UInt32 nChannelCount;        // 通道个数
            public IntPtr pEncChannelnfo;		// Enc_Channel_Info_Ex_t通道信息
        }

        // 获取组织个数请求信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Get_Dep_Count_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCoding;             // 节点code
            public UInt32 nDepCount;		    // 组织个数
            public UInt32 nDeviceCount;	        // 设备个数
            public UInt32 nChannelCount;        // 通道个数
        }

        // 组织信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Dep_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCoding;                                 // 节点code
                                                                    // public IntPtr       szCoding;                                   // 节点code
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDepName;                                    // 节点名称
                                                                        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
                                                                        //public byte[] szDepName;	                                // 节点名称
            public Int32 nDepSort;                                  // 组织排序
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szOrgID;		                            // 组织结点唯一ID<海南省透明厨房项目>
        }

        // 组织信息(扩展)
        [StructLayout(LayoutKind.Sequential)]
        public struct Dep_Info_Ex_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCoding;             // 节点code
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDepName;                // 节点名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szModifyTime;         // 节点修改时间
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szSN;                 // 唯一标识码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string szMemo;                   // 备注信息 报警运营平台
            public Int32 nDepType;				// 组织节点类型 报警运营平台
            public Int32 nDepSort;				// 组织排序
            public Int32 nChargebooth;			// 收费亭标志
            public Int32 nDepExtType;           // 组织节点扩展类型
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szOrgID;		        // 组织结点唯一ID<海南省透明厨房项目>
        }


        public enum dpsdk_playback_speed_e
        {
            DPSDK_CORE_PB_NORMAL = 8,						    // 1倍数
            DPSDK_CORE_PB_FAST2 = DPSDK_CORE_PB_NORMAL * 2,     // 2倍数
            DPSDK_CORE_PB_FAST4 = DPSDK_CORE_PB_NORMAL * 4,     // 4倍数
            DPSDK_CORE_PB_FAST8 = DPSDK_CORE_PB_NORMAL * 8,     // 8倍数
            DPSDK_CORE_PB_FAST16 = DPSDK_CORE_PB_NORMAL * 16,   // 16倍数
            DPSDK_CORE_PB_SLOW2 = DPSDK_CORE_PB_NORMAL / 2,     // 1/2倍数
            DPSDK_CORE_PB_SLOW4 = DPSDK_CORE_PB_NORMAL / 4,     // 1/4倍数
            DPSDK_CORE_PB_SLOW8 = DPSDK_CORE_PB_NORMAL / 8,     // 1/8倍数
            DPSDK_CORE_PB_SLOW16 = 0,						    // 1/16倍数
        }

        // 单元类型
        public enum dpsdk_dev_unit_type_e
        {
            DPSDK_DEV_UNIT_UNKOWN,		                                        // 未知
            DPSDK_DEV_UNIT_ENC,			                                        // 编码
            DPSDK_DEV_UNIT_DEC,			                                        // 解码
            DPSDK_DEV_UNIT_ALARMIN,		                                        // 报警输入
            DPSDK_DEV_UNIT_ALARMOUT,	                                        // 报警输出
            DPSDK_DEV_UNIT_TVWALLIN,	                                        // TvWall输入
            DPSDK_DEV_UNIT_TVWALLOUT,	                                        // TvWall输出
            DPSDK_DEV_UNIT_DOORCTRL,	                                        // 门禁
            DPSDK_DEV_UNIT_VOICE,	                                          	// 对讲
        }

        // 录像打标操作
        public enum dpsdk_operator_type_e
        {
            DPSDK_CORE_OP_TYPE_UNKNOW = 0,                  // 
            DPSDK_CORE_OP_TYPE_ADD,                                             // 新增
            DPSDK_CORE_OP_TYPE_MODIFY,                                          // 修改
            DPSDK_CORE_OP_TYPE_DELETE,											// 删除
        }

        // 录像打标信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Tag_Info_t
        {
            public dpsdk_recsource_type_e source;					// 来源
            public UInt32 tagid;				// 标签ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string cameraId;		// 摄像头ID
            public UInt64 tagtime;									// tag的时间
            public UInt64 endtime;									// 结束时间
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string subject;		                            // 主题
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string content;		                            // 内容
            public UInt32 ownerid;									// 创建者的ID
            public UInt64 ownertime;								// 创建时间(修改时间)
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string url;						                // 存放图片的url
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Record_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;           // 通道ID
            public uint nBegin;                 // 录像起始
            public uint nCount;                 // 请求录像数
            public uint nRetCount;              //实际返回个数
            public IntPtr pSingleRecord;
        }
        public struct Single_Record_Info_t
        {
            public uint nFileIndex;                                 // 文件索引
            public dpsdk_recsource_type_e nSource;                  // 录像源类型
            public dpsdk_record_type_e nRecordType;                 // 录像类型
            public UInt64 uBeginTime;                               // 起始时间
            public UInt64 uEndTime;                                 // 结束时间
            public UInt64 uLength;                                  // 文件大小
        }

        public struct Query_Record_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;                                 // 通道ID
            public dpsdk_check_right_e nRight;                        // 是否检查通道权限
            public dpsdk_recsource_type_e nSource;                    // 录像源类型
            public dpsdk_record_type_e nRecordType;                   // 录像类型
            public UInt64 uBeginTime;                                 // 起始时间
            public UInt64 uEndTime;                                   // 结束时间
        }
        public enum dpsdk_record_type_e
        {
            DPSDK_CORE_PB_RECORD_UNKONWN = 0,         // 全部录像
            DPSDK_CORE_PB_RECORD_MANUAL = 1,          // 手动录像
            DPSDK_CORE_PB_RECORD_ALARM = 2,           // 报警录像
            DPSDK_CORE_PB_RECORD_MOTION_DETECT = 3,   // 移动侦测
            DPSDK_CORE_PB_RECORD_VIDEO_LOST = 4,      // 视频丢失
            DPSDK_CORE_PB_RECORD_VIDEO_SHELTER = 5,   // 视频遮挡
            DPSDK_CORE_PB_RECORD_TIMER = 6,           // 定时录像
            DPSDK_CORE_PB_RECORD_ALL_DAY = 7,         // 全天录像
            DPSDK_CORE_PB_RECORD_CARD = 25,           // 卡号录像
            CENTER_DISK_FULL = 36,				      // 硬盘满
            CENTER_DISK_FAULT = 37,				 	  // 硬盘故障
        }

        public enum dpsdk_playback_type_e
        {
            PLAYBACK_TYPE_UNKOWN = 0,
            PLAYBACK_TYPE_FILE = 1,
            PLAYBACK_TYPE_TIME = 2
        }

        //厂商类型 流类型与播放库自带的vaxplayer.ini做一一映射
        public enum stream_type_e
        {
            STREAM_TYPE_UNKNOW = 0,			//未知
            STREAM_TYPE_DAHUA = 1,			//大华流
            STREAM_TYPE_HIK = 2,			//海康流
            STREAM_TYPE_H3C = 3,			//华三流
            STREAM_TYPE_HANBANG = 4,		//汉邦私有流
            STREAM_TYPE_KAER = 10,			//卡尔
        }

        /*帧类型*/
        public enum MediaFrameType_e
        {
            MEDIA_FRAME_TYPE_UNKNOWN = 0,			/*帧类型不可知*/
            MEDIA_FRAME_TYPE_VIDEO,					/*帧类型是视频帧*/
            MEDIA_FRAME_TYPE_AUDIO,					/*帧类型是音频帧*/
            MEDIA_FRAME_TYPE_DATA					/*帧类型是数据帧*/
        }

        //录像文件类型
        public enum file_type_e
        {
            FILE_TYPE_NONE = -1,        //原始格式dav
            FILE_TYPE_TS = 0,
            FILE_TYPE_PS = 1,
            FILE_TYPE_RTP = 2,
            FILE_TYPE_MP4 = 3,          //mp4格式
            FILE_TYPE_GDPS = 4,
            FILE_TYPE_GAYSPS = 5,
            FILE_TYPE_FLV = 6,          //FLV格式
            FILE_TYPE_ASF_FILE = 7,
            FILE_TYPE_ASF_STREAM = 8,
            FILE_TYPE_FLV_STREAM = 9,
            FILE_TYPE_AVI = 10,         //AVI格式
        }

        // 摄像头类型
        public enum dpsdk_camera_type_e
        {
            CAMERA_TYPE_NORMAL = 1,                  // 枪机
            CAMERA_TYPE_SD = 2,                      // 球机
            CAMERA_TYPE_HALFSD = 3,                      // 半球
            CAMERA_TYPE_EVIDENCE = 4,                    // 证据通道
            CAMERA_TYPE_NORMAL_HD = 5,                   // 网络高清枪机，福建高速项目新增摄像头类型begin
            CAMERA_TYPE_SD_HD = 6,                   // 网络高清球机
            CAMERA_TYPE_NORMAL_SIMULATION_SD = 8,                    // 模拟标清枪机
            CAMERA_TYPE_SD_SIMULATION_SD = 9,                    // 模拟标清球机
            CAMERA_TYPE_HALFSD_SIMULATION_SD = 10,                   // 模拟标清半球
            CAMERA_TYPE_NORMAL_SIMULATION_HD = 11,                   // 模拟高清枪机
            CAMERA_TYPE_SD_SIMULATION_HD = 12,                   // 模拟高清球机
            CAMERA_TYPE_HALFSD_SIMULATION_HD = 13,                   // 模拟高清半球
            CAMERA_TYPE_HALFSD_HD = 14,					 // 网络高清半球，福建高速项目新增摄像头类型end
        }


        //本地录像回放信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Get_Record_Local_Info_t
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            public byte[] szFilePath;           // 文件全路径和文件名 
            //[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            //public string szFilePath;			// 通道ID:设备ID+通道号
        }

        // 编码通道信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Enc_Channel_Info_t
        {
            public dpsdk_camera_type_e nCameraType;	            // 类型，参见CameraType_e
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szId;			                        // 通道ID:设备ID+通道号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szName;	                            // 名称
            public int nSort;			                        // 组织排序
        }

        // 车辆违章图片信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Traffic_Violation_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szRecordId;         // 记录ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szDeviceId;            // 设备ID
            public Int32 nChannel;			 // 通道
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szChannelId;       // 通道编号
            public Int32 ntype;				 // 违章报警类型 见EnumCarRule 枚举定义	
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDeviceName;	     // 设备名称,UTF8编码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDeviceChnName;   // 通道名称,UTF8编码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szCarNum;          // 车牌号码，UTF8编码
            public Int32 nCarNumType;        // 车牌类型
            public Int32 nCarNumColor;       // 车牌颜色
            public Int32 nCarColor;          // 车身颜色
            public Int32 nCarType;           // 车类型
            public Int32 nCarLogo;           // 车标类型
            public Int32 nWay;               // 车道号
            public Int32 nCarSpeed;          // 车速
            public Int32 nCarLen;            // 车身长度，单位cm,不能超过4位
            public Int32 nCardirect;             // 行车方向
            public Int32 nMaxSpeed;          // 限制速度,用于超速判断
            public Int32 nMinSpeed;			 // 最低限制速度,用于超速判断
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCapturedate;	 // 精确到秒,如 2013-09-25 12:04:08
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szOptNote;             // 备注信息
            public Int32 nPicNum;             // 图片张数，最大支持6张
            //public char                                szPicName[DPSDK_CORE_BAY_IMG_NUM][DPSDK_CORE_FILEPATH_LEN]; // 图片文件命名，最大支持6张。如果为空，则由PTS生成。
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
            //public string[]                         szPicName;           // 图片文件命名，最大支持6张。如果为空，则由PTS生成。
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 6)]
            public Pic_Path_t[] szPicName;             // 图片文件命名，最大支持6张。如果为空，则由PTS生成。
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] nRtPlate;           // 车牌坐标,left,top, right, bottom,不能超过4位		
            public Int32 nDataSource;	     // 数据来源,不能超过2位
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Pic_Path_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szPicPath;                // 图片URL
                                                    // public byte[] szPicPath;                       // 图片
        }


        // 交通流量上报数据结构信息
        [StructLayout(LayoutKind.Sequential)]
        public struct TrafficFlow_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szChannelId;	        // 通道编号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string szDirect;              // 方向编号，见字典表
            public UInt64 nInterval;             // 上报间隔，单位秒
            public UInt64 nTrafficFlow;         // 交通流量（包括从设备接收到的所有数据，违章也包括在内）	
            public UInt64 nTime;                 // 上报时间，1970的秒数
        }

        // 每个车道交通流量状态
        [StructLayout(LayoutKind.Sequential)]
        public struct TrafficFlow_Lane_State
        {
            public UInt64 nDateTime;                                   // 数据上报时间 UTC时间
            public UInt32 nDetectorID;                                 // 检测车道编号
            public UInt32 nVolume;                                     // 交通量（辆/单位时间）小车当量
            public UInt32 nFlowRate;                                   // 流率（辆/小时）小车当量
            public float nAverageSpeed;                                // 平均速度（公里/小时）
            public float fTimeOccupancy;                               // 平均时间占有率（%）
            public float nTimeHeadway;                                 // 车头时距（秒）
            public float nLength;                                      // 平均车长（米）
            public float nBackOfQueue;                                 // 排队长度（米）
            public float nTravelTime;                                  // 旅行时间（秒）
            public float nDelay;                                       // 延误（秒），##
            public UInt32 nMotoVehicles;                               // 微型车交通量（辆/单位时间）
            public UInt32 nSmallVehicles;                              // 小车交通量（辆/单位时间）
            public UInt32 nMediumVehicles;                             // 中车交通量（辆/单位时间）
            public UInt32 nLargeVehicles;                              // 大车交通量（辆/单位时间）
            public UInt32 nLongVehicles;                               // 特大车交通量（辆/单位时间）
            public UInt32 nState;                                      // 状态值：1-流量过大,2-流量过大恢复,3-正常,4-流量过小,5-流量过小恢复
            public UInt32 nFlow;                                       // 流量值，单位：辆
            public UInt32 nPeriod;                                     // 流量值对应的统计时间
            public UInt32 nDrivingDir;                                 // 行驶方向，上行下行。上行，即车辆离设备部署点越来越近 /下行，即车辆离设备部署点越来越远
            public Int32 nDirection;                                   // 车道方向
            public Int32 nPeriodBySeconds;                             // 以秒为单位的周期
        }


        // 设备交通流量上报详细信息数据结构
        [StructLayout(LayoutKind.Sequential)]
        public struct DevTrafficFlow_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szChannelId;          // 通道编号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
            public string szDirect;             // 方向编号，见字典表
            public UInt64 nTrafficLaneNum;                             // 交通流量数量
            public TrafficFlow_Lane_State[] LaneState;             // 车道交通流量状态,最多支持8个车道
        }


        //订阅流量上报请求信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Subscribe_Traffic_Flow_Info_t
        {
            public Int32 nChnlCount;                                  // 订阅通道的数量
            public Int32 nInterval;                                   // 订阅通道上报间隔，单位秒
                                                                      //Enc_Channel_Info_t*                 pEncChannelnfo;                                // 通道信息
            public IntPtr pEncChannelnfo;                              // 通道信息
            public Int32 nSubscribeFlag;                              // 订阅标记。0:取消订阅，1：订阅
        }

        //订阅卡口过车(或区间测速)信息请求信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Subscribe_Bay_Car_Info_t
        {
            public int nChnlCount;                  // 订阅通道的数量 nChnlCount =0 pEncChannelnfo = null表示订阅/取消订阅全部
            public IntPtr pEncChannelnfo;           // 通道信息
            public int nSubscribeFlag;              // 订阅标记。0:取消订阅，1：订阅
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Pic_Url_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string szPicUrl;                                 // 图片URL
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct arryDevID
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string devID;                                 // 图片URL
        }

        //违章报警信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Traffic_Alarm_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;                               // 通道ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string nPtsIp;                                   // pts内网
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string nPtsIpy;                                  // pts外网
            public int nPicPort;                                    // pic内网port
            public int nPicPorty;                                   // pic外网port
            public dpsdk_alarm_type_e type;                         // 违章类型
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szCarNum;                                 // 车牌
            public int nLicentype;                                  // 车牌颜色类型
            public int nCarColor;					                // 车身颜色
            public int nCarLogo;					                // 车标类型
            public int nWay;						                // 车道号
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 6)]
            public Pic_Url_t[] szPicUrl;                            // 图片URL
            public UInt32 nPicGroupStoreID;                         // 图片组存储ID
            public int bNeedStore;					                // 是否需存盘 0：不需存盘 1：需存盘
            public int bStored;					                    // 是否已存盘 0：未存盘 1：已存盘int	
            public int nAlarmLevel;				                    // 报警级别
            public int nAlarmTime;                                  // 报警发生时间,精度为秒，值为time(NULL)值

            //新增字段
            public int nChannel;                                    // 通道
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szDeviceId;                               // 设备ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDeviceName;                             // 设备名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDeviceChnName;                          // 通道名称
            public int nCarType;                                    // 车类型
            public int nCarSpeed;                                   // 车速
            public int nCarLen;                                     // 车身长度单位
            public int nCardirect;                                  // 行车方向
            public int nMaxSpeed;                                   // 限制速度
            public int nMinSpeed;                                   // 最低限制速度
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public int[] nRtPlate;                                  // 车牌坐标
        }

        public enum dpsdk_netalarmhost_report_type_e
        {
            AHOST_TYPE_NONE = 0,
            AHOST_TYPE_STAY = 1,                // 留守布防
            AHOST_TYPE_BYPASS,							// 防区旁路
            AHOST_TYPE_ALARMOUT_CLOSE,					// 报警输出通道关闭
            AHOST_TYPE_ALARMOUT_OPEN,					// 报警输出通道打开
            AHOST_TYPE_ISOLATE,							// 防区隔离
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct dpsdk_phone_subscribe_alarm_t
        {
            public int iUserId;				            //用户ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szPhoneId;                            //手机ID,手机推送的标志
            public int iAppId;					            //应用程序编号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szPush_type;                      //手机类型
            public int iIs_subscribe;			            //是否订阅; 1-订阅, 0-取消订阅
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szLanguage;			                //语言
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szTimefmt;                            //时间格式化
            public int iDbOper;				            //db操作类型. 1-插入, 2-更新, 3-查询
        }

        //卡口业务类型
        public enum dpsdk_bay_monitor_e
        {
            BAY_MONITOR_CAR = 1,        //车辆
            BAY_MONITOR_FACE = 2,		//人脸
        }

        public enum dpsdk_area_type_e
        {
            AREATYPE_UNKOWN = 0x00,     //未知
            AREATYPE_SPEEDLIMIT = 0x01,     //限速区
            AREATYPE_DRIVERALLOW = 0x02,        //行驶区
            AREATYPE_FORBIDDRIVE = 0x04,        //禁行区
            AREATYPE_LOADGOODS = 0x08,      //出发区
            AREATYPE_UNLOADGOODS = 0x10,		//达到区
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Area_Point_t
        {
            public double dwLongitude;              //经度
            public double dwLatidude;				//纬度
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Area_Time_Period_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szBeginTime;				// 起始时间
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szEndTime;					// 结束时间
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Area_Time_Weekdays_t
        {
            public int days;
            //Area_Time_Period_t* arryAlarmtimePeriod;
            public IntPtr arryAlarmtimePeriod;
            public int nAlarmtimePeriodCount;
        }

        // 电子围栏启用时间段
        [StructLayout(LayoutKind.Sequential)]
        public struct Area_Time_t
        {
            public int enable;												// 时间端启用使用,1表示启用，0表示不启用
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public Area_Time_Weekdays_t[] areatimeWeekdays;	                    // 星期中一天的时间段
        }

        //电子围栏信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Area_Info_t
        {
            public dpsdk_area_type_e nAreaType;						//围栏类型
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string strAreaAttr;			                    //围栏属性
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string strAreaName;                              //围栏名称
            public int nSpeedLimit;                                 //限速
            public int nNumMax;                                     //最大车辆数
                                                                    //public Area_Point_t* arryAreaPoints;
            public IntPtr arryAreaPoints;
            public int nAreaPointsCount;

            public Area_Time_t stuAlarmTime;
        }

        //卡口过车信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Bay_Car_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szDeviceId;                                   //设备ID           
            public int nDevChnId;                                       //通道号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szChannelId;                                  //通道ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDeviceName;                                 //设备名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDeviceChnName;                              //通道名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szCarNum;                                     //车牌号
            public int nCarNumType;                                     //车牌类型
            public int nCarNumColor;                                    //车牌颜色
            public int nCarSpeed;                                       //车速
            public int nCarType;                                        //车类型
            public int nCarColor;                                       //车颜色
            public int nCarLen;                                         //车长
            public int nCarDirect;                                      //行车方向
            public int nWayId;                                          //车道号
            public UInt64 lCaptureTime;                                 //抓图时间，精确到毫秒级
            public UInt32 lPicGroupStoreID;                             //图片组存盘ID 
            public int nIsNeedStore;                                    //图片组是否需要存盘 0：不需要 1：需要
            public int nIsStoraged;                                     //图片组是否已经存盘 0：未完成存盘 1：已成功存盘，保留，目前未使用
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szCaptureOrg;                                 //通缉机构
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szOptOrg;                                     //布控机构
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szOptUser;                                    //布控人员
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szOptNote;                                    //备注信息
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szImg0Path;                                   //图片路径
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szImg1Path;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szImg2Path;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szImg3Path;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szImg4Path;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szImg5Path;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szImgPlatePath;		                        //车牌小图片坐标
            public int icarLog;			                                //车标类型
            public int iPlateLeft;
            public int iPlateRight;
            public int iPlateTop;
            public int iPlateBottom;
        }

        //卡口布控告警信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Bay_WantedAlarm_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;                                //通道ID
            public Int32 nChannel;                                  //通道号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szDeviceId; 	                            //设备ID（DVR+通道）
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szPtsIp;	                                //pts内网
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szPtsIpy;	                                //pts外网
            public Int32 ulPicPort;                                  //pic内网port
            public Int32 ulPicPorty;                                 //pic外网port

            //布控信息
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szSurveyId;	                                //布控ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szSurveryOrg;                               //布控机构
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szArrestOrg;                                //通缉机构
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szUserName;                                 //当前布控的操作员
            public Int32 nAlart;								        //报警类型
            public Int32 nSurlevel;						            //布控等级
            public Int32 nState;								        //布控状态
            public Int32 nCartype;						            //车辆类型
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCartypeName;                              //车辆类型名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCarNum;		                            //车牌
            public Int32 nLicentype;							        //车牌颜色类型
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCarlicenName;                             //车牌颜色名称
            public Int32 nCarnotype;							        //车牌类型
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCarnotypeName;                            //车牌类型名称

            public Int32 nCarcolor;								    //车身颜色
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCarpower;		                            //发动机号
            public Int32 nCarlen;								    //车长：厘米
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szFrameno;		                            //车架号
            public Int32 nBckcolor;								    //车牌底色

            public Int32 nSurType;								    //布控类型
            public Int32 nBegindate;
            public Int32 nEnddate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szIdentify;		                            //车主身份证
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szOwnName;		                            //车主姓名
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szOwnAddr;		                            //车主地址
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szOwnPhone;		                            //车主电话
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szDescirbe;		                            //描述
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szOrderlyPhone;	                            //布控单位值班电话
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public string szLinkMan;			                        //布控联系人
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szLinkManPhone;                             //布控联系人电话
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
            public string szOriUrl;                                 //车辆原始图片

            //报警信息
            //char			szPicUrl[DPSDK_CORE_BAY_IMG_NUM][DPSDK_CORE_IMG_URL];//图片URL
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 6)]
            public Pic_Url_t[] szPicUrl;                                   // 图片URL
            public UInt32 ulPicGroupStoreID;					        //图片组存储ID
            public Int32 bNeedStore;						            //是否需存盘 0：不需存盘 1：需存盘
            public Int32 bStored;							        //是否已存盘 0：未存盘 1：已存盘
            public Int32 nCarLogo;							        //车标类型

            public Int32 nAlarmLevel;						        //布控报警级别
            public UInt32 ulAlarmTime;							    //报警发生时间,精度为秒，值为time(NULL)值
            public Int32 nDeviceType;						        //设备类型。1编码器，2报警主机
            public Int32 nLightColor;                                //红绿灯状态，0 未知,1 绿灯,2 红灯,3 黄灯
        }

        //区间测速上报信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Area_Detect_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string szAreaId;                // 区间ID 50
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szAreaName;            // 区间名称 256
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szStartDevId;             // 起始设备ID
            public UInt32 nStartChnNum;                                    // 起始点通道号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szStartChnId;             // 起始点通道ID 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szStartDevName;   // 起始点设备名,UTF8编码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szStartDevChnName;// 起始点通道名,UTF8编码
            public UInt64 nStartCapTime;                                   // 起始点通过时间
            public UInt32 nStartCarSpeed;                                  // 起始点通过速度
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string szStartPosId;             // 起始点卡点ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szStartPosName;		 // 起始点卡点名
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szEndDevId;               // 终止点设备ID
            public UInt32 nEndChnNum;                                      // 终止点通道号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szEndChnId;               // 终止点通道ID 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szEndDevName;     // 终止点设备名,UTF8编码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szEndDevChnName;  // 终止点通道名,UTF8编码
            public UInt64 nEndCapTime;                                     // 终止点通过时间
            public UInt32 nEndCarSpeed;                                    // 终止点通过速度
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string szEndPosId;               // 终止点卡点ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szEndPosName;           // 终止点卡点名
            public UInt32 nAreaRange;                                      // 区间距离
            public UInt32 nMinSpeed;                                       // 路段限速下限 
            public UInt32 nMaxSpeed;                                       // 路段限速上限 
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szCarNum;             // 车牌号码，UTF8编码
            public UInt32 nCarNumType;                                     // 车牌类型
            public UInt32 nCarNumColor;                                    // 车牌颜色
            public UInt32 nCarColor;                                       // 车身颜色
            public UInt32 nCarType;                                        // 车类型
            public UInt32 nCarLogo;                                        // 车标类型
            public UInt32 nCarAvgSpeed;                                    // 车辆平均速度
            public UInt32 nIsIllegalSpeed;                                 // 是否超速或低速
            public UInt32 nPicNum;                                         // 图片张数，最大支持6张
            //char                                szPicName[DPSDK_CORE_BAY_IMG_NUM][DPSDK_CORE_FILEPATH_LEN]; // 图片文件命名，最大支持6张。
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 6)]
            public Pic_Path_t[] szPicName;             // 图片文件命名，最大支持6张。如果为空，则由PTS生成。
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt32 nRtPlate;             // 车牌坐标,left,top, right, bottom,不能超过4位
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szCarPlatePicURL;                           // 车牌小图片URL
        }

        //预置点操作
        [StructLayout(LayoutKind.Sequential)]
        public struct Ptz_Prepoint_Operation_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;                                               // 通道ID
            public dpsdk_ptz_prepoint_cmd_e nCmd;									// 预置点操作类型
            public Ptz_Single_Prepoint_Info_t pPoint;								// 预置点信息
        }

        //预置点操作类型
        public enum dpsdk_ptz_prepoint_cmd_e
        {
            DPSDK_CORE_PTZ_PRESET_LOCATION = 1,			        // 预置点定位
            DPSDK_CORE_PTZ_PRESET_ADD = 2,					    // 预置点增加
            DPSDK_CORE_PTZ_PRESET_DEL = 3,					    // 预置点删除
        }

        // 云台方向控制信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Ptz_Direct_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;           // 通道ID
            public dpsdk_ptz_direct_e nDirect;          // 云台方向控制命令
            public UInt32 nStep;                // 步长	
            public bool bStop;				// 控制命令：停止：true；控制：false
        }

        // 云台命令操作信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Ptz_Operation_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;           // 通道ID
            public dpsdk_camera_operation_e nOperation;         // 云台命令控制操作
            public UInt32 nStep;                // 步长	
            public bool bStop;				// 控制命令：停止：true；控制：false
        }

        // 云台三维定位操作信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Ptz_Sit_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;           // 通道ID
            public Int32 pointX;                // 坐标值X
            public Int32 pointY;                // 坐标值Y
            public Int32 pointZ;                // 坐标值Z	
            public Int32 type;               // 1--快速定位, 2--精确定位
        }

        // 云台锁定操作信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Ptz_Lock_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;           // 通道ID
            public dpsdk_ptz_locktype_e nLock;				// 锁定/解锁类型
        }

        // 云台打开命令信息 目前包括灯光 雨刷 红外
        [StructLayout(LayoutKind.Sequential)]
        public struct Ptz_Open_Command_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;                   // 通道ID
            public bool bOpen;						// 开启
        }


        // 云台方向控制命令			
        public enum dpsdk_ptz_direct_e
        {
            DPSDK_CORE_PTZ_GO_UP = 1,                    // 上
            DPSDK_CORE_PTZ_GO_DOWN = 2,                  // 下
            DPSDK_CORE_PTZ_GO_LEFT = 3,                  // 左
            DPSDK_CORE_PTZ_GO_RIGHT = 4,                     // 右
            DPSDK_CORE_PTZ_GO_LEFTUP = 5,                    // 左上
            DPSDK_CORE_PTZ_GO_LEFTDOWN = 6,                  // 左下
            DPSDK_CORE_PTZ_GO_RIGHTUP = 7,                   // 右上
            DPSDK_CORE_PTZ_GO_RIGHTDOWN = 8,					 // 右下
        }

        // 云台图像控制命令
        public enum dpsdk_camera_operation_e
        {
            DPSDK_CORE_PTZ_ADD_ZOOM = 0,                     // 变倍+ 
            DPSDK_CORE_PTZ_ADD_FOCUS = 1,                    // 变焦+
            DPSDK_CORE_PTZ_ADD_APERTURE = 2,                     // 光圈+
            DPSDK_CORE_PTZ_REDUCE_ZOOM = 3,                  // 变倍-
            DPSDK_CORE_PTZ_REDUCE_FOCUS = 4,                     // 变焦-
            DPSDK_CORE_PTZ_REDUCE_APERTURE = 5,					 // 光圈-
        }

        // 云台锁类型
        public enum dpsdk_ptz_locktype_e
        {
            DPSDK_CORE_PTZ_CMD_LOCK = 1,					 // 锁定当前摄像头
            DPSDK_CORE_PTZ_CMD_UNLOCK_ONE = 2,				 // 解锁当前摄像头
            DPSDK_CORE_PTZ_CMD_UNLOCK_ALL = 3,				 // 解锁被该用户锁定的所有摄像头
        }

        // 视频图像属性
        [StructLayout(LayoutKind.Sequential)]
        public struct Video_Color_Info_t
        {
            public int nBrightness;								// 亮度,默认64;范围0-128
            public int nContrast;									// 对比度,默认64;范围0-128
            public int nSaturation;								// 饱和度,默认64;范围0-128
            public int nHue;										// 色调,默认64;范围0-128
        }

        public enum IvsInfoVisible
        {
            IVS_RULE_VISIBLE = 1,           // 规则
            IVS_OBJ_VISIBLE = 2,            // 目标框
            IVS_LOCUS_VISIBLE = 3,			// 轨迹
        }


        // 日志等级
        public enum dpsdk_log_level_e
        {
            DPSDK_LOG_LEVEL_DEBUG = 2,					// 调试
            DPSDK_LOG_LEVEL_INFO = 4,					// 信息
            DPSDK_LOG_LEVEL_ERROR = 6,					// 错误
        }

        // 实时码流类型
        public enum dpsdk_encdev_stream_e
        {
            DPSDK_CORE_STREAM_ALL = 0,      // 0-全部
            DPSDK_CORE_STREAM_MAIN = 1,     // 1-主码流
            DPSDK_CORE_STREAM_SUB,                  // 2-辅码流
            DPSDK_CORE_STREAM_THIRD,                // 3-三码流
            DPSDK_CORE_STREAM_SIGNAL = 5,		// 5-M60本地信号
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Get_RecordStream_Time_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;                           // 通道ID
            public dpsdk_check_right_e nRight;					// 是否检测权限
            public dpsdk_pb_mode_e nMode;						// 录像流请求类型

            public dpsdk_recsource_type_e nSource;				// 录像源类型
            public UInt64 uBeginTime;							// 播放起始
            public UInt64 uEndTime;							    // 播放结束
            public int nTrackID;                                // 拉流TrackID，默认0
        }

        // 按文件获取录像码流信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Get_RecordStream_File_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;           // 通道ID
            public dpsdk_check_right_e nRight;              // 是否检测权限
            public dpsdk_pb_mode_e nMode;               // 录像流请求类型

            public UInt32 nFileIndex;           // 文件索引
            public UInt64 uBeginTime;           // 播放起始
            public UInt64 uEndTime;			// 播放结束
        }

        public enum dpsdk_pb_mode_e
        {
            DPSDK_CORE_PB_MODE_NORMAL = 1,					     // 回放
            DPSDK_CORE_PB_MODE_DOWNLOAD = 2,					 // 下载
        }

        //回放类型
        public enum playback_type_e
        {
            PLAYBACK_BY_FILE = 0,                   // 按文件回放
            PLAYBACK_BY_TIME = 1,					// 按时间回放
        }

        public enum dpsdk_recsource_type_e
        {
            DPSDK_CORE_PB_RECSOURCE_DEVICE = 2,	    			     // 设备录像
            DPSDK_CORE_PB_RECSOURCE_PLATFORM = 3,					 // 平台录像
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Get_RealStream_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;                               // 通道ID
            public dpsdk_check_right_e nRight;						// 是否检测权限
            public dpsdk_stream_type_e nStreamType;					// 码流类型 参考dpsdk_stream_type_e；预览接口，此参数为分割数，参考dpsdk_stream_real_video_slite_e
            public dpsdk_media_type_e nMediaType;					// 媒体类型
            public dpsdk_trans_type_e nTransType;				    // 传输类型
            public int nTrackID;                                    // 拉流TrackID，默认0
        }

        public enum dpsdk_check_right_e
        {
            DPSDK_CORE_CHECK_RIGHT = 0,					    // 检查
            DPSDK_CORE_NOT_CHECK_RIGHT = 1,					// 不检查
        }

        // 码流类型
        public enum dpsdk_stream_type_e
        {
            DPSDK_CORE_STREAMTYPE_MAIN = 1,			// 主码流
            DPSDK_CORE_STREAMTYPE_SUB = 2,			// 辅码流
            DPSDK_CORE_STREAMTYPE_THIRD = 3,			// 三码流
            DPSDK_CORE_STREAMTYPE_SIGNAL = 5,			// 本地信号，只能用于上墙
        }

        // 媒体类型
        public enum dpsdk_media_type_e
        {
            DPSDK_CORE_MEDIATYPE_VIDEO = 1,					 // 视频
            DPSDK_CORE_MEDIATYPE_AUDI = 2,					 // 音频
            DPSDK_CORE_MEDIATYPE_ALL = 3,					 // 音频 + 视频
        }

        // 传输类型
        public enum dpsdk_trans_type_e
        {
            DPSDK_CORE_TRANSTYPE_UDP = 0,					 // UDP
            DPSDK_CORE_TRANSTYPE_TCP = 1,					 // TCP
        }

        /// <summary>
        /// 电视墙列表
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TvWall_List_Info_t
        {
            /// <summary>
            /// 个数
            /// </summary>
            public UInt32 nCount;					// 电视墙数量
            /// <summary>
            /// 电视墙信息列表
            /// </summary>
            /// /*OUT	TvWall_Info_t*	 */                        
            public IntPtr pTvWallInfo;            // 电视墙信息
        }

        /// <summary>
        /// 电视墙信息
        /// </summary>
        public struct TvWall_Info_t
        {
            /// <summary>
            /// 电视墙ID
            /// </summary>
            public uint nTvWallId;
            /// <summary>
            /// 状态
            /// </summary>
            public uint nState;

            /// <summary>
            /// 名字
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szName;
        }


        /// <summary>
        /// 电视墙列表
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TvWall_Layout_Info_t
        {
            /// <summary>
            /// 电视墙ID
            /// </summary>
            public uint nTvWallId;

            /// <summary>
            /// 屏数量
            /// </summary>
            public uint nCount;

            /// <summary>
            /// 电视墙信息列表
            /// </summary>
            ///  /*OUT	TvWall_Screen_Info_t* */
            public IntPtr pScreenInfo;
        }

        /// <summary>
        ///  // 电视墙屏信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TvWall_Screen_Info_t
        {
            /// <summary>
            /// 屏ID
            /// </summary>
            public uint nScreenId;

            /// <summary>
            /// 屏的名称
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szName;
            // public byte[] szName;

            /// <summary>
            /// 屏幕绑定的解码器ID
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szDecoderId;

            /// <summary>
            /// 左边距
            /// </summary>
            public Single fLeft;

            /// <summary>
            /// 左边距
            /// </summary>
            public Single fTop;

            /// <summary>
            /// 左边距
            /// </summary>
            public Single fWidth;

            /// <summary>
            /// 左边距
            /// </summary>
            public Single fHeight;

            /// <summary>
            /// 是否绑定解码器
            /// </summary>
            public byte bBind;

            /// <summary>
            /// 是否是融合屏
            /// </summary>
            public byte bCombine;
        }


        /// <summary>
        /// 电视墙屏分割（分割后窗口ID从0开始，从左到右，从上到下依次递增，使用者自己维护）
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct TvWall_Screen_Split_t
        {
            /// <summary>
            /// 电视墙ID
            /// </summary>
            public uint nTvWallId;
            /// <summary>
            /// 屏ID
            /// </summary>
            public uint nScreenId;
            /// <summary>
            /// 分割数量
            /// </summary>
            public tvwall_screen_split_caps enSplitNum;
        }

        public enum tvwall_screen_split_caps
        {
            Screen_Split_1 = 1,
            Screen_Split_4 = 4,
            Screen_Split_9 = 9,
            Screen_Split_16 = 16,
        }

        //上墙模式,安徽三联项目
        public enum dpsdk_tvwall_pip_e
        {
            DPSDK_CORE_TVWALL_GENERAL = 0,                  //普通模式
            DPSDK_CORE_TVWALL_PIP,													//画中画模式
        }

        //电视墙任务信息
        [StructLayout(LayoutKind.Sequential)]
        public struct TVWall_Task_Info_t
        {
            public UInt32 nTaskId;                                                  //任务ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szName;                                               //任务名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szDes;						                        //任务描述
        }

        // 电视墙任务信息列表
        [StructLayout(LayoutKind.Sequential)]
        public struct TvWall_Task_Info_List_t
        {
            public UInt32 nCount;                               // 电视墙任务数量
                                                                /*OUT	TVWall_Task_Info_t* 			pTvWallTaskInfo;					// 电视墙任务信息 */
            public IntPtr pTvWallTaskInfo;					// 电视墙任务信息
        }

        // 电视墙屏开窗信息
        [StructLayout(LayoutKind.Sequential)]
        public struct TvWall_Screen_Open_Window_t
        {
            public uint nTvWallId;									// 电视墙ID
            public uint nScreenId;									// 屏ID
            public Single fLeft;									// 窗口左边距
            public Single fTop;										// 窗口上边距
            public Single fWidth;									// 窗口宽度
            public Single fHeight;									// 窗口高度
            //下面两个是输出结果
            public uint nWindowId;									// 窗口ID
            public uint nZorder;									// 窗口Z序
        }

        // 电视墙屏窗口移动
        [StructLayout(LayoutKind.Sequential)]
        public struct TvWall_Screen_Move_Window_t
        {
            public UInt32 nTvWallId;                                    // 电视墙ID
            public UInt32 nScreenId;                                    // 屏ID
            public UInt32 nWindowId;                                    // 窗口ID
            public float fLeft;                                     // 窗口左边距
            public float fTop;                                      // 窗口上边距
            public float fWidth;                                        // 窗口宽度
            public float fHeight;									// 窗口高度
        }

        // 电视墙屏窗口置顶
        [StructLayout(LayoutKind.Sequential)]
        public struct TvWall_Screen_Set_Top_Window_t
        {
            public UInt32 nTvWallId;                                    // 电视墙ID
            public UInt32 nScreenId;                                    // 屏ID
            public UInt32 nWindowId;									// 窗口ID
        }

        //安徽三联项目单个屏是否为画中画屏
        [StructLayout(LayoutKind.Sequential)]
        public struct Pic_In_Pic_Info_t
        {
            public Int32 nScreenID;								    //屏ID
            public float fLeftPercent;								//左坐标 0~1之间的值
            public float fTopPercent;								//上坐标 0~1之间的值
            public float fRightPercent;								//右坐标 0~1之间的值 右>左
            public float fBottomPercent;								//下坐标 0~1之间的值 下>上
            public Int32 nBigChnNum;                                    //大窗口号 此值有取值范围为：屏1：0~15；屏2：16~19；屏3：20~23；屏4：24~27；
            public Int32 nSmallChnNum;								//小窗口号 此值有取值范围为：屏1：0~15；屏2：16~19；屏3：20~23；屏4：24~27；
            public dpsdk_tvwall_pip_e enPIP;										//是否采用画中画功能               
        }

        //安徽三联项目画中画上墙控制操作
        [StructLayout(LayoutKind.Sequential)]
        public struct Pip_TvWall_Screen_Info_t
        {
            public UInt32 nTvWallId;									// 电视墙ID

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public Pic_In_Pic_Info_t[] picInPicInfo;			//画中画上墙信息
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TvWall_SubTv_Info_t
        {
            public UInt32 nTvWallId;
            public UInt32 nScreenId;
        }


        /// <summary>
        /// 预置点信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Ptz_Prepoint_Info_t
        {
            /// <summary>
            /// 通道ID
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;

            /// <summary>
            /// 预置点数量
            /// </summary>
            public int nCount;

            /// <summary>
            /// 预置点信息
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 300)]
            public Ptz_Single_Prepoint_Info_t[] pPoints;
        }

        /// <summary>
        /// 预置点信息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct Ptz_Single_Prepoint_Info_t
        {
            /// <summary>
            /// 预置点编号
            /// </summary>
            public uint nCode;

            /// <summary>
            /// 名字
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szName;
        }

        // 报警方案
        [StructLayout(LayoutKind.Sequential)]
        public struct Alarm_Enable_Info_t
        {
            public int nCount;							// 报警布控个数
            //[MarshalAs(UnmanagedType.ByValArray)]
            public IntPtr pSources;				        // 报警内容
        }

        // 单个报警方案
        [StructLayout(LayoutKind.Sequential)]
        public struct Alarm_Single_Enable_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szAlarmDevId;		            // 报警设备ID
            public int nVideoNo;						// 视频通道 视频相关的报警 -1接收所有通道
            public int nAlarmInput;					    // 报警输入通道 报警输入相关的报警 -1接收所有通道
            public dpsdk_alarm_type_e nAlarmType;		// 报警类型
        }

        // 单个报警方案(针对整个部门所有设备设置报警)
        [StructLayout(LayoutKind.Sequential)]
        public struct Alarm_Single_Enable_By_Dep_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szAlarmDepartmentCode;                    // 报警设备所属部门
            public int nVideoNo;                                // 视频通道 视频相关的报警 -1接收所有通道
            public int nAlarmInput;                         // 报警输入通道 报警输入相关的报警 -1接收所有通道
            public dpsdk_alarm_type_e nAlarmType;								// 报警类型
        }

        // 报警方案(针对整个部门所有设备设置报警)
        [StructLayout(LayoutKind.Sequential)]
        public struct Alarm_Enable_By_Dep_Info_t
        {
            public UInt32 nCount;                                   // 报警布控个数
                                                                    //Alarm_Single_Enable_By_Dep_Info_t
            public IntPtr pSources;							    // 报警内容
        }

        // 查询报警信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Alarm_Query_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;           // 通道ID
            public UInt64 uStartTime;           // 开始时间
            public UInt64 uEndTime;         // 结束时间
            public dpsdk_alarm_type_e nAlarmType;			// 报警类型
            public byte bLinkInfo;			// 返回的报警信息是否有联动录像信息，用于兼容部分老平台
            public byte bPositionInfo;		// 返回的报警信息是否有点位信息，用于兼容新版本ADS新增点位信息
        }

        // 单个报警信息
        public struct Single_Alarm_Info_t
        {
            public dpsdk_alarm_type_e nAlarmType;               // 报警类型
            public dpsdk_event_type_e nEventType;				// 事件状态
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szDevId;              // 报警设备ID
            public UInt32 uChannel;             // 报警通道
            public UInt64 uAlarmTime;               // 报警时间
            public dpsdk_alarm_dealwith_e nDealWith;				// 处理意见
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szPicUrl;			    // 报警图片url
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szSwLabel;			    // 超声波探测标签
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szElecLabel;		    // 车用电子标签
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (4 * 1024))]
            public string szMessage;		        // 附加信息
        }

        // 报警信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Alarm_Info_t
        {
            public UInt32 nCount;                                       // 请求录像数
            public UInt32 nRetCount;                                    // 实际返回个数
                                                                        //public Single_Alarm_Info_t[]			pAlarmInfo;									// 报警信息
            public IntPtr pAlarmInfo;									                        // 报警信息
        }

        // 客户端报警上报信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Client_Alarm_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;                       // 报警通道ID
            public dpsdk_alarm_type_e enAlarmType;                  // 报警类型
            public dpsdk_event_type_e enStatus;                     // 事件状态
            public UInt64 uAlarmTime;						// 报警时间
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = (4 * 1024))]
            public string szMsg;			                                    // 报警信息
        }
        // 报警发生类型
        public enum dpsdk_event_type_e
        {
            ALARM_EVENT_OCCUR = 1,               // 产生
            ALARM_EVENT_DISAPPEAR,												 // 消失
        }

        // ftp操作类型
        public enum dpsdk_operator_ftp_type_e
        {
            OP_FTP_TYPE_UNKNOW = 0,
            OP_FTP_TYPE_DOWN = 0x01,           //下载
            OP_FTP_TYPE_UP = 0x02,           //上传
            OP_FTP_TYPE_DELETE = 0x03,           //删除
        }

        // 报警处理类型
        public enum dpsdk_alarm_dealwith_e
        {
            ALARM_DEALWITH_PENDING = 1,               // 处理中
            ALARM_DEALWITH_RESOLVE = 2,               // 已解决
            ALARM_DEALWITH_SUGGESTTED = 4,                // 误报
            ALARM_DEALWITH_IGNORED = 8,               // 忽略	
            ALARM_DEALWITH_UNPROCESSED = 16				  // 未解决
        }

        public enum dpsdk_alarm_type_e
        {
            DPSDK_CORE_ALARM_TYPE_Unknown = 0,				// 未知
            DPSDK_CORE_ALARM_TYPE_VIDEO_LOST = 1,				// 视频丢失
            DPSDK_CORE_ALARM_TYPE_EXTERNAL_ALARM = 2,				// 外部报警
            DPSDK_CORE_ALARM_TYPE_MOTION_DETECT = 3,				// 移动侦测
            DPSDK_CORE_ALARM_TYPE_VIDEO_SHELTER = 4,				// 视频遮挡
            DPSDK_CORE_ALARM_TYPE_DISK_FULL = 5,				// 硬盘满
            DPSDK_CORE_ALARM_TYPE_DISK_FAULT = 6,				// 硬盘故障
            DPSDK_CORE_ALARM_TYPE_FIBER = 7,				// 光纤报警
            DPSDK_CORE_ALARM_TYPE_GPS = 8,				// GPS信息
            DPSDK_CORE_ALARM_TYPE_3G = 9,				// 3G
            DPSDK_CORE_ALARM_TYPE_STATUS_RECORD = 10,				// 设备录像状态
            DPSDK_CORE_ALARM_TYPE_STATUS_DEVNAME = 11,				// 设备名
            DPSDK_CORE_ALARM_TYPE_STATUS_DISKINFO = 12,				// 硬盘信息
            DPSDK_CORE_ALARM_TYPE_IPC_OFF = 13,				// 前端IPC断线
            DPSDK_CORE_ALARM_TYPE_DEV_DISCONNECT = 16,				// 设备断线报警

            //景德镇华润燃气项目 
            DPSDK_CORE_ALARM_POWER_INTERRUPT = 17,				// 市电中断报警 
            DPSDK_CORE_ALARM_POWER_ENABLED = 18,				// 市电启用报警 
            DPSDK_CORE_ALARM_INFRARED_DETECT = 19,				// 红外探测报警 
            DPSDK_CORE_ALARM_GAS_OVER_SECTION = 20,				// 燃气浓度超过上限 
            DPSDK_CORE_ALARM_FLOW_OVER_SECTION = 21,				// 瞬时流量超过上限 
            DPSDK_CORE_ALARM_TEMPERATURE_OVER_SECTION = 22,				// 温度超过上限 
            DPSDK_CORE_ALARM_TEMPERATURE_UNDER_SECTION = 23,				// 温度低于下限 
            DPSDK_CORE_ALARM_PRESSURE_OVER_SECTION = 24,				// 压力超过上限 
            DPSDK_CORE_ALARM_PRESSURE_UNDER_SECTION = 25,				// 压力低于下限

            DPSDK_CORE_ALARM_STATIC_DETECTION = 26,				// 静态检测
            DPSDK_CORE_ALARM_REGULAR = 27,				// 定时报警
            DPSDK_CORE_ALARM_REMOTE_EXTERNAL_ALARM = 28,				// 远程外部报警
            DPSDK_CORE_ALARM_BUTTON_EXTERNAL_ALARM = 29,				// 报警按钮外部报警
            DPSDK_CORE_ALARM_POWER_INTERRUPT_EXTERNAL_ALARM = 30,				// 断电信号外部报警
            DPSDK_CORE_ALARM_RECORD_LOSS = 31,		        // 录像丢失事件，指硬盘完好的情况下，发生误删等原因引起
            DPSDK_CORE_ALARM_VIDEO_FRAME_LOSS = 32,		        // 视频丢帧事件，比如网络不好或编码能力不足引起的丢帧
            DPSDK_CORE_ALARM_RECORD_VOLUME_FAILURE = 33,               // 由保存录像的磁盘卷发生异常，引起的录像异常

            //门禁
            DPSDK_CORE_ALARM_DOOR_BEGIN = 40,		    	 // 门禁设备报警起始
            DPSDK_CORE_ALARM_FORCE_CARD_OPENDOOR = 41,				 // 胁迫刷卡开门
            DPSDK_CORE_ALARM_VALID_PASSWORD_OPENDOOR = 42,				 // 合法密码开门
            DPSDK_CORE_ALARM_INVALID_PASSWORD_OPENDOOR = 43,				 // 非法密码开门
            DPSDK_CORE_ALARM_FORCE_PASSWORD_OPENDOOR = 44,				 // 胁迫密码开门
            DPSDK_CORE_ALARM_VALID_FINGERPRINT_OPENDOOR = 45,			     // 合法指纹开门
            DPSDK_CORE_ALARM_INVALID_FINGERPRINT_OPENDOOR = 46,				 // 非法指纹开门
            DPSDK_CORE_ALARM_FORCE_FINGERPRINT_OPENDOOR = 47,				 // 胁迫指纹开门

            DPSDK_CORE_ALARM_TYPE_VALID_CARD_READ = 51,				 // 合法刷卡/开门
            DPSDK_CORE_ALARM_TYPE_INVALID_CARD_READ = 52,				 // 非法刷卡/开门
            DPSDK_CORE_ALARM_TYPE_DOOR_MAGNETIC_ERROR = 53,				 // 门磁报警
            DPSDK_CORE_ALARM_TYPE_DOOR_BREAK = 54,				 // 破门报警和开门超时报警
            DPSDK_CORE_ALARM_TYPE_DOOR_ABNORMAL_CLOSED = 55,				 // 门非正常关闭
            DPSDK_CORE_ALARM_TYPE_DOOR_NORMAL_CLOSED = 56,				 // 门正常关闭
            DPSDK_CORE_ALARM_TYPE_DOOR_OPEN = 57,				 // 门打开
            DPSDK_CORE_ALARM_TALK_REQUEST = 59,				 // 门禁对讲请求报警

            DPSDK_CORE_ALARM_DOOR_OPEN_TIME_OUT_BEG = 60,
            DPSDK_CORE_ALARM_VALID_FACE_OPENDOOR = 61,				 // 合法人脸开门
            DPSDK_CORE_ALARM_INVALID_FACE_OPENDOOR = 62,				 // 非法人脸开门
            DPSDK_CORE_ALARM_DOOR_OPEN_TIME_OUT_THIRD = 63,                // 超时等级三
            DPSDK_CORE_ALARM_DOOR_OPEN_TIME_OUT_FOUR = 64,                // 超时等级四
            DPSDK_CORE_ALARM_DOOR_OPEN_TIME_OUT_FIVE = 65,                // 超时等级五
            DPSDK_CORE_ALARM_DOOR_OPEN_TIME_OUT_ONE = 66,                // 超时等级一
            DPSDK_CORE_ALARM_DOOR_OPEN_TIME_OUT_SECOND = 67,                // 超时等级二

            DPSDK_CORE_ALARM_DOOR_OPEN_FORCE = 68,				 // 门强制打开 
            DPSDK_CORE_ALARM_DOOR_OPEN_TIME_OUT_END = 70,

            //报警主机
            ALARM_TYPE_ALARMHOST_BEGIN = 80,
            DPSDK_CORE_ALARM_TYPE_ALARM_CONTROL_ALERT = 81,				 // 报警主机报警
            DPSDK_CORE_ALARM_TYPE_FIRE_ALARM = 82,				 // 火警
            DPSDK_CORE_ALARM_TYPE_ZONE_DISABLED = 83,				 // 防区失效
            DPSDK_CORE_ALARM_TYPE_BATTERY_EMPTY = 84,				 // 电池没电
            ALARM_TYPE_AC_OFF = 85,				 // 市电断开-设备报警
            //大力高速报警平台报警（上行和下行）
            ALARM_DALI_UP = 86,				 // 上行报警 
            //ALARM_DALI_DOWN									= 87,				 // 下行报警
            ALARM_TYPE_ALARMHOST_WIRE_SHOCK = 87,				 // 报警主机电网触电报警，协议中87为下行报警，这里复用这个值作为报警主机电网触电报警

            //新增“晨鹰”厂商哨位机报警
            ALARM_PRISONERS_ESCAPE = 88,				 // 犯人逃跑
            ALARM_PRISONERS_RIOT = 89,				 // 犯人暴狱
            ALARM_TYPE_ALARMHOST_END = 90,
            DPSDK_CORE_ALARM_NATURAL_DISASTER = 91,				 // 自然灾害
            DPSDK_CORE_ALARM_ONEKEY_ALARM = 92,				 // 一键报警
            DPSDK_CORE_ALARM_EMERGENCY_BUTTON = 93,                // 紧急按钮
            DPSDK_CORE_ALARM_BREAKIN_ALARM = 94,				 // 两个防区同时入侵
            DPSDK_CORE_ALARM_HOST_CHANNEL_OFFLINE = 95,				 // 报警主机通道离线报警
            DPSDK_CORE_ALARM_FLASH_LAMP_OFF = 96,				 // 闪光灯离线报警

            DPSDK_CORE_ALARM_DISABLE_ARM_ABNORMAL = 97,				 // 撤防异常
            DPSDK_CORE_ALARM_BYPASS_ABNORMAL = 98,				 // 旁路异常
            DPSDK_CORE_ALARM_ALARMHOST_EXTERNAL_ALARM = 99,				 // 报警主机外部报警

            DPSDK_CORE_ALARM_FILESYSTEM = 100,				 // 文件系统
            DPSDK_CORE_ALARM_RAID_FAULT = 101,				 // raid故障
            DPSDK_CORE_ALARM_RECORDCHANNELFUNCTION_ABNORMAL = 102,				 // 录像通道功能异常
            DPSDK_CORE_SVR_HARDDISK_STATUS = 103,				 // 硬盘状态
            DPSDK_CORE_ALARM_RECORD_REPAIR = 104,				 // 录像补全 -P3.0

            //begin电网报警类型
            ELECTRIC_WIRE_SHOCK = 109,				 // 电网触电
            ELECTRIC_WIRE_INTERRUPT = 110,				 // 电网断电
            ELECTRIC_WIRE_SHORT_CIRCUIT = 111,				 // 电网短路
            ELECTRIC_WIRE_BREAKDOWN = 112,				 // 电网故障
            ELECTRIC_WIRE_VOLTAGE_LOW = 113,				 // 电网电压低
            //end
            ALARM_TYPE_RECORD_WRITE_FAIL = 114,				 // 录像写入失败

            //电网报警类型新增begin add by hu_wenjuan
            ELECTRIC_ALARM_BEGIN_EX = 115,
            ELECTRIC_BREAK_CIRCUIT = 115,               // 电网开路 
            ELECTRIC_SENSE_ALARM = 116,               // 电网传感报警 
            //重庆声光电项目
            ALARM_GAS_CHROMA = 120,               // 气体浓度报警
            ALARM_GAS_CHROMA_CHANGE = 121,               // 气体浓度变化异常报警
            ALARM_GAS_DETECTOR_LEVEL1 = 122,               // 气体探测器一级报警
            ALARM_GAS_DETECTOR_LEVEL2 = 123,               // 气体探测器二级报警
            //江苏油罐车车载设备电子铅封相关报警类型
            ALARM_LOCK_OPEN = 130,               // 异常开锁
            ALARM_LOCK_CLOSE = 131,               // 异常关锁
            ALARM_LID_OPEN = 132,               // 异常开盖
            ALARM_POWER_ON = 133,               // 异常上电
            ELECTRIC_ALARM_END_EX = 150,
            //电网报警类型新增end 
            ALARM_FIRE_WARNING = 151,               // 火灾报警
            ALARM_WATER_GAUGE = 152,				 // 水尺报警
            ALARM_SMOKE_DETECTION = 153,				 // 烟感报警

            ALARM_VTT_URGENCY = 160,               // VTT设备紧急按钮报警
            ALARM_APPROVE_LEAVE = 165,				 // 请销假批准外出报警
            ALARM_DISAPPROVE_LEAVE = 166, 				 // 请销假未批准外出报警
            ALARM_NORMAL_BACK = 167,				 // 请销假正常归队报警
            ALARM_ABNORMAL_BACK = 168,  			 // 请销假异常归队报警
            //政企河南气象局定制项目报警类型begin
            ALARM_LIGTHON_ALARM = 169,          	 // 机房开灯
            ALARM_HUMIDITY_ALARM = 170,          	 // 湿度异常
            ALARM_COMMUNICATION_ALARM = 171,          	 // 通信状态告警
            ALARM_DOOROPEN_ALARM = 172,          	 // 机房门开
            ALARM_WATEROUT_ALARM = 173,          	 // 水浸告警  
            ALARM_THEFT_ALARM = 174,          	 // 防盗告警
            ALARM_THALPOSISWARNING_ALARM = 175,          	 // 温感告警
            ALARM_THALPOSISFAULT_ALARM = 176,          	 // 温感故障  
            ALARM_SMOKEWARNING_ALARM = 177,           	 // 烟感告警
            ALARM_SMOKEFAULT_ALARM = 178,          	 // 烟感故障
            ALARM_BUZZERWARNING_ALARM = 179,           	 // 讯响器告警
            ALARM_INFRARED_ALARM = 180,           	 // 红外告警
            ALARM_HUMIDITYLOW_ALARM = 181,           	 // 湿度过低
            ALARM_RUNNINGSTATUS_ALARM = 182,          	 // 运行状态提示
            ALARM_TEMPERATURELOW_ALARM = 183,          	 // 温度过低
            ALARM_TEMPERATUREHIGH_ALARM = 184,           	 // 温度过高
            ALARM_FOG_ALARM = 185,               // 烟雾告警
            ALARM_FIREINHOUSE_ALARM = 186,           	 // 机房火警
            ALARM_OUTDOORTHEFT_ALARM = 187,           	 // 室外机被盗告警
            ALARM_BUZZERFAULT_ALARM = 188,           	 // 讯响器故障
            ALARM_COMMUNICATEDSTATUS_ALARM = 189,           	 // 通讯状况
            ALARM_BUTTONFAULT_ALARM = 190,           	 // 手动按钮故障
            ALARM_BUTTONWARNING_ALARM = 191,           	 // 手动按钮告警
            ALARM_FIREDAMPERFAULT_ALARM = 192,           	 // 防火阀故障
            ALARM_FIREDAMPERWARNING_ALARM = 193,           	 // 防火阀告警
            ALARM_SMOKEDAMPERFAULT_ALARM = 194,           	 // 排烟阀故障
            ALARM_SMOKEDAMPERWARNING_ALARM = 195,           	 // 排烟阀告警
            //政企河南气象局定制项目报警类型end
            ALARM_VEHICLE_INVASION = 196,				 // 车辆入侵报警
            ALARM_CROSSLINE_DETECTION = 198,     			 // 绊线入侵报警 
            ALARM_CROSSREGION_DETECTION = 199,     			 // 区域入侵报警

            //-M的相关报警在这里添加
            DPSDK_CORE_ALARM_MOTOR_BEGIN = 200,
            DPSDK_CORE_ALARM_OVERSPEED_OCCURE = 201, 			     // 超速报警产生
            DPSDK_CORE_ALARM_OVERSPEED_DISAPPEAR = 202,  			 // 超速报警消失
            DPSDK_CORE_ALARM_DRIVEROUT_DRIVERALLOW = 203,				 // 驶出行区
            DPSDK_CORE_ALARM_DRIVERIN_DRIVERALLOW = 204,				 // 驶入行区
            DPSDK_CORE_ALARM_DRIVEROUT_FORBIDDRIVE = 205,				 // 驶出禁入区
            DPSDK_CORE_ALARM_DRIVERIN_FORBIDDRIVE = 206,				 // 驶入禁入区
            DPSDK_CORE_ALARM_DRIVEROUT_LOADGOODS = 207,				 // 驶出装货区
            DPSDK_CORE_ALARM_DRIVERIN_LOADGOODS = 208,				 // 驶入装货区
            DPSDK_CORE_ALARM_DRIVEROUT_UNLOADGOODS = 209,				 // 驶出卸货区
            DPSDK_CORE_ALARM_DRIVERIN_UNLOADGOODS = 210,				 // 驶入卸货区
            DPSDK_CORE_ALARM_CAR_OVER_LOAD = 211,				 // 超载
            DPSDK_CORE_ALARM_SPEED_SOON_ZERO = 212,				 // 急刹车
            DPSDK_CORE_ALARM_3GFLOW = 213,				 // 3G流量
            DPSDK_CORE_ALARM_AAC_POWEROFF = 214,				 // ACC断电报警
            DPSDK_CORE_ALARM_SPEEDLIMIT_LOWERSPEED = 215,				 // 限速报警 LowerSpeed
            DPSDK_CORE_ALARM_SPEEDLIMIT_UPPERSPEED = 216,				 // 限速报警 UpperSpeed 
            DPSDK_CORE_ALARM_VEHICLEINFOUPLOAD_CHECKIN = 217,				 // 车载自定义信息上传 CheckIn
            DPSDK_CORE_ALARM_VEHICLEINFOUPLOAD_CHECKOUT = 218,				 // 车载自定义信息上传 CheckOut
            ALARM_CAR_OPEN_DOOR = 219,               // 开门报警
            ALARM_URGENCY = 220,				 // 紧急报警
            ALARM_MOTOR_TURNOVER = 221,				 // 侧翻报警
            ALARM_MOTOR_COLLISION = 222,				 // 撞车报警
            ALARM_VEHICLE_CONFIRM = 223,				 // 车辆上传信息事件(OSD下发 设备的回复通知)
            ALARM_VEHICLE_LARGE_ANGLE = 224,				 // 车载摄像头大角度扭转事件
            ALARM_BATTERYLOWPOWER = 225,				 // 电池电量低报警
            ALARM_TEMPERATURE = 226,				 // 温度过高报警     
            ALARM_TALKING_INVITE = 227,				 // 设备请求对方发起对讲事件 
            ALARM_ALARM_EX2 = 228,				 // 扩展的本地报警事件 
            ALARM_DEV_VOICE_EX = 229,    			 // 设备语音请求报警
            ALARM_POWER_OFF_EX = 230,    			 // 断电报警
            ALARM_ROUTE_OFFSET_EX = 231,    			 // 线路偏移报警
            ALARM_TYRE_PRESSURE_EX = 232,    			 // 轮胎气压检测报警
            ALARM_FATIGUE_DRIVING = 233,				 // 疲劳驾驶报警
            ALARM_DRIVER_CHECKIN = 234,				 // 司机签入
            ALARM_DRIVER_CHECHOUT = 235,				 // 司机签出
            DPSDK_CORE_ALARM_GAS_LOWLEVEL = 236,				 // 油耗报警
            ALARM_GAS_INFO = 237,				 // 油耗信息
            ALARM_GETIN_STATION = 238,				 // 进站报警
            ALARM_GETOUT_STATION = 239,				 // 出站报警
            ALARM_STATION_BEGIN_IN = 240,				 // 始发站进站报警
            ALARM_STATION_BEGIN_OUT = 241,				 // 始发站出站报警
            ALARM_STATION_END_IN = 242,				 // 终点站进站报警
            ALARM_STATION_END_OUT = 243,				 // 终点站出站报警 <进出站类报警放在一起了>
            ALARM_STAY_STATION_OVERTIME = 244,				 // 停车超时报警
            ALARM_RECOVER_RUNNING = 245,				 // 恢复营运报警
            ALARM_MEAL = 246,				 // 吃饭报警
            ALARM_BLOCK = 247,				 // 路堵报警
            ALARM_CALL = 248,				 // 通话报警
            ALARM_CAR_BREAKDOWN = 249, 				 // 车坏报警
            ALARM_STOP_RUNNING = 250,				 // 停止营运报警
            ALARM_ROBING = 251, 				 // 盗抢报警
            ALARM_DISPUTE = 252, 				 // 纠纷报警
            ALARM_ACCIDENT = 253, 				 // 事故报警
            ALARM_OVER_SPEED = 254, 				 // 超速报警
            ALARM_RENTAL = 255, 				 // 包车报警
            ALARM_MAINTENANCE = 256, 				 // 车辆保养报警
            ALARM_CLOSURE = 257, 				 // 脱保停运报警
            ALARM_OPEN_OR_CLOSE_DOOR = 258,				 // 开关门报警
            ALARM_ILLEGALIN_OVERSPEED = 259,				 // 非法进入限速区报警
            ALARM_ILLEGALOUT_OVERSPEED = 260,				 // 非法驶出限速区报警
            ALARM_ILLEGALIN_DRIVERALLOW = 261,				 // 非法进入行使区报警
            ALARM_ILLEGALOUT_DRIVERALLOW = 262,				 // 非法驶出行使区报警
            ALARM_ILLEGALIN_FORBIDDRIVE = 263,				 // 非法进入禁入区报警
            ALARM_ILLEGALOUT_FORBIDDRIVE = 264,				 // 非法驶出禁入区报警
            ALARM_ILLEGALIN_LOADGOODS = 265,				 // 非法进入装货区报警
            ALARM_ILLEGALOUT_LOADGOODS = 266,				 // 非法驶出装货区报警
            ALARM_ILLEGALIN_UNLOADGOODS = 267,				 // 非法进入卸货区报警
            ALARM_ILLEGALOUT_UNLOADGOODS = 268,				 // 非法驶出卸货区报警
            ALARM_ILLEGALIN_GETIN_STATION = 269,				 // 非法进站报警
            ALARM_ILLEGALIN_GETOUT_STATION = 270,				 // 非法出站报警
            ALARM_REONLINE_INFO = 271,				 // 设备重新登录报警
            ALARM_DETAINED = 272,               // 车辆滞留报警
            ALARM_DELAY = 273,               // 托班报警，车辆班次拖延
            ALARM_SHAP_TURNING = 274,				 // 急转报警
            ALARM_SHAP_SPEEDUP = 275,				 // 急加速
            ALARM_SHAP_SLOWDOWN = 276,				 // 急减速
            ALARM_STOP_OVERTIME = 277,				 // 停车超时报警（辽宁油罐车项目）
            ALARM_RUN_NONWOKINGTIME = 278,				 // 非工作时间驾驶报警（辽宁油罐车项目）
            ALARM_PASSENGER_CHECK_CARD = 279,				 // 乘客刷卡事件上报（黑龙江校车项目）

            //江苏油罐车设备添加
            ALARM_ABNORMAL_PARK = 280,
            ALARM_BUS_LOW_OIL = 281,				 // 低油量报警事件
            ALARM_BUS_CUR_OIL = 282,				 // 当前油耗情况事件

            ALARM_SWIPE_OVERTIME = 283,               // 司机没有刷卡（泰国Usupply项目）
            ALARM_DRIVING_WITHOUTCARD = 284,               // 司机无卡驾驶（泰国Usupply项目）
            ALARM_NONPOWEROFF_CHECKOUT = 285,               // 司机签出没有关闭引擎（泰国Usupply项目)

            ALARM_VEHICLE_TAMPER_ALARM = 286,				 // 车载防拆报警

            ALARM_CAR_UNMOVING = 287,				 // 未清车报警
            ALARM_CAR_OUTRULE_DRIVE = 288,				 // 非规定时间行车报警
            ALARM_SOS = 289,				 // SOS求救报警
            DPSDK_CORE_ALARM_MOTOR_END = 300,

            //智能报警
            DPSDK_CORE_ALARM_IVS_ALARM_BEGIN = 300,				 // 智能设备报警类型在dhnetsdk.h基础上+300（DMS服务中添加）
            DPSDK_CORE_ALARM_IVS_ALARM = 301,				 // 智能设备报警
            DPSDK_CORE_ALARM_CROSSLINEDETECTION = 302,				 // 警戒线事件
            DPSDK_CORE_ALARM_CROSSREGIONDETECTION = 303,				 // 警戒区事件
            DPSDK_CORE_ALARM_PASTEDETECTION = 304,				 // 贴条事件
            DPSDK_CORE_ALARM_LEFTDETECTION = 305,				 // 物品遗留事件
            DPSDK_CORE_ALARM_STAYDETECTION = 306,				 // 停留事件
            DPSDK_CORE_ALARM_WANDERDETECTION = 307,				 // 徘徊事件
            DPSDK_CORE_ALARM_PRESERVATION = 308,				 // 物品保全事件
            DPSDK_CORE_ALARM_MOVEDETECTION = 309,				 // 移动事件
            DPSDK_CORE_ALARM_TAILDETECTION = 310,				 // 尾随事件
            DPSDK_CORE_ALARM_RIOTERDETECTION = 311,				 // 聚众事件
            DPSDK_CORE_ALARM_FIREDETECTION = 312,				 // 火警事件
            DPSDK_CORE_ALARM_SMOKEDETECTION = 313,				 // 烟雾报警事件
            DPSDK_CORE_ALARM_FIGHTDETECTION = 314,				 // 斗殴事件
            DPSDK_CORE_ALARM_FLOWSTAT = 315,				 // 流量统计事件
            DPSDK_CORE_ALARM_NUMBERSTAT = 316,				 // 数量统计事件
            DPSDK_CORE_ALARM_CAMERACOVERDDETECTION = 317,				 // 摄像头覆盖事件
            DPSDK_CORE_ALARM_CAMERAMOVEDDETECTION = 318,				 // 摄像头移动事件
            DPSDK_CORE_ALARM_VIDEOABNORMALDETECTION = 319,				 // 视频异常事件
            DPSDK_CORE_ALARM_VIDEOBADDETECTION = 320,				 // 视频损坏事件
            DPSDK_CORE_ALARM_TRAFFICCONTROL = 321,				 // 交通管制事件
            DPSDK_CORE_ALARM_TRAFFICACCIDENT = 322,				 // 交通事故事件
            DPSDK_CORE_ALARM_TRAFFICJUNCTION = 323,				 // 交通路口事件
            DPSDK_CORE_ALARM_TRAFFICGATE = 324,				 // 交通卡口事件
            DPSDK_CORE_ALARM_TRAFFICSNAPSHOT = 325,				 // 交通抓拍事件
            DPSDK_CORE_ALARM_FACEDETECT = 326,				 // 人脸检测事件 
            DPSDK_CORE_ALARM_TRAFFICJAM = 327,				 // 交通拥堵事件
            DPSDK_CORE_ALARM_STRANGE_FACEDETECT = 328,				 // 陌生人脸事件

            DPSDK_CORE_ALARM_TRAFFIC_RUNREDLIGHT = 556,				 // 交通违章-闯红灯事件
            DPSDK_CORE_ALARM_TRAFFIC_OVERLINE = 557,				 // 交通违章-压车道线事件
            DPSDK_CORE_ALARM_TRAFFIC_RETROGRADE = 558,				 // 交通违章-逆行事件
            DPSDK_CORE_ALARM_TRAFFIC_TURNLEFT = 559,				 // 交通违章-违章左转
            DPSDK_CORE_ALARM_TRAFFIC_TURNRIGHT = 560,				 // 交通违章-违章右转
            DPSDK_CORE_ALARM_TRAFFIC_UTURN = 561,				 // 交通违章-违章掉头
            DPSDK_CORE_ALARM_TRAFFIC_OVERSPEED = 562,				 // 交通违章-超速
            DPSDK_CORE_ALARM_TRAFFIC_UNDERSPEED = 563,				 // 交通违章-低速
            DPSDK_CORE_ALARM_TRAFFIC_PARKING = 564,				 // 交通违章-违章停车
            DPSDK_CORE_ALARM_TRAFFIC_WRONGROUTE = 565,				 // 交通违章-不按车道行驶
            DPSDK_CORE_ALARM_TRAFFIC_CROSSLANE = 566,				 // 交通违章-违章变道
            DPSDK_CORE_ALARM_TRAFFIC_OVERYELLOWLINE = 567,				 // 交通违章-压黄线
            DPSDK_CORE_ALARM_TRAFFIC_DRIVINGONSHOULDER = 568,				 // 交通违章-路肩行驶事件  
            DPSDK_CORE_ALARM_TRAFFIC_YELLOWPLATEINLANE = 570,				 // 交通违章-黄牌车占道事件
            DPSDK_CORE_ALARM_CROSSFENCEDETECTION = 587,				 // 翻越围栏事件
            DPSDK_CORE_ALARM_ELECTROSPARKDETECTION = 572,				 // 电火花事件
            DPSDK_CORE_ALARM_TRAFFIC_NOPASSING = 573,				 // 交通违章-禁止通行事件
            DPSDK_CORE_ALARM_ABNORMALRUNDETECTION = 574,				 // 异常奔跑事件
            DPSDK_CORE_ALARM_RETROGRADEDETECTION = 575,				 // 人员逆行事件
            DPSDK_CORE_ALARM_INREGIONDETECTION = 576,				 // 区域内检测事件
            DPSDK_CORE_ALARM_TAKENAWAYDETECTION = 577,				 // 物品搬移事件
            DPSDK_CORE_ALARM_PARKINGDETECTION = 578,				 // 非法停车事件
            DPSDK_CORE_ALARM_FACERECOGNITION = 579,				 // 人脸识别事件
            DPSDK_CORE_ALARM_TRAFFIC_MANUALSNAP = 580,				 // 交通手动抓拍事件
            DPSDK_CORE_ALARM_TRAFFIC_FLOWSTATE = 581,				 // 交通流量统计事件
            DPSDK_CORE_ALARM_TRAFFIC_STAY = 582,				 // 交通滞留事件
            DPSDK_CORE_ALARM_TRAFFIC_VEHICLEINROUTE = 583,				 // 有车占道事件
            DPSDK_CORE_ALARM_MOTIONDETECT = 584,				 // 视频移动侦测事件
            DPSDK_CORE_ALARM_LOCALALARM = 585,				 // 外部报警事件
            DPSDK_CORE_ALARM_PRISONERRISEDETECTION = 586,				 // 看守所囚犯起身事件
            DPSDK_CORE_ALARM_CORSSFENCE = 587,				 // 穿越围栏
            DPSDK_CORE_ALARM_IVS_B_ALARM_END,									 // 以上报警都为IVS_B服务的报警类型，与SDK配合
            DPSDK_CORE_ALARM_VIDEODIAGNOSIS = 588,				 // 视频诊断结果事件
            DPSDK_CORE_ALARM_IVS_V_ALARM = DPSDK_CORE_ALARM_VIDEODIAGNOSIS,	// 
            DPSDK_CORE_ALARM_IVS_DENSITYDETECTION = 589,    			 // 人员密集度检测事件 	

            DPSDK_CORE_ALARM_IVS_QUEUEDETECTION = 591,				 // 排队检测报警事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_VEHICLEINBUSROUTE = 592,				 // 占用公交车道事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_BACKING = 593,				 // 违章倒车事件
            //新增智能报警start
            DPSDK_CORE_ALARM_IVS_AUDIO_ABNORMALDETECTION = 594,				 // 声音异常检测
            DPSDK_CORE_ALARM_IVS_TRAFFIC_RUNYELLOWLIGHT = 595,				 // 交通违章-闯黄灯事件
            DPSDK_CORE_ALARM_CLIMB_UP = 596,				 // 攀高检测 
            DPSDK_CORE_ALARM_LEAVE_POST = 597,				 // 离岗检测
            //新增智能报警End
            DPSDK_CORE_ALARM_IVS_TRAFFIC_PARKINGONYELLOWBOX = 598,				 // 黄网格线抓拍事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_PARKINGSPACEPARKING = 599,				 // 车位有车事件(对应 DEV_EVENT_TRAFFIC_PARKINGSPACEPARKING_INFO)
            DPSDK_CORE_ALARM_IVS_TRAFFIC_PARKINGSPACENOPARKING = 600,			 // 车位无车事件(对应 DEV_EVENT_TRAFFIC_PARKINGSPACENOPARKING_INFO)    
            DPSDK_CORE_ALARM_IVS_TRAFFIC_PEDESTRAIN = 601,				 // 交通行人事件(对应 DEV_EVENT_TRAFFIC_PEDESTRAIN_INFO)
            DPSDK_CORE_ALARM_IVS_TRAFFIC_THROW = 602,				 // 交通抛洒物品事件(对应 DEV_EVENT_TRAFFIC_THROW_INFO)
            DPSDK_CORE_ALARM_IVS_TRAFFIC_IDLE = 603,				 // 交通空闲事件
            DPSDK_CORE_ALARM_VEHICLEACC = 604,				 // 车载ACC断电报警事件 
            DPSDK_CORE_ALARM_VEHICLE_TURNOVER = 605,				 // 车辆侧翻报警事件
            DPSDK_CORE_ALARM_VEHICLE_COLLISION = 606,				 // 车辆撞车报警事件

            DPSDK_CORE_ALARM_ALARM_VEHICLE_LARGE_ANGLE = 607,				 // 车载摄像头大角度扭转事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_PARKINGSPACEOVERLINE = 608,				 // 车位压线事件(对应 DEV_EVENT_TRAFFIC_PARKINGSPACEOVERLINE_INFO)
            DPSDK_CORE_ALARM_IVS_MULTISCENESWITCH = 609,				 // 多场景切换事件(对应 DEV_EVENT_IVS_MULTI_SCENE_SWICH_INFO)
            DPSDK_CORE_ALARM_IVS_TRAFFIC_RESTRICTED_PLATE = 610,				 // 受限车牌事件(对应 DEV_EVENT_TRAFFIC_RESTRICTED_PLATE)
            DPSDK_CORE_ALARM_IVS_TRAFFIC_OVERSTOPLINE = 611,				 // 压停止线事件(对应 DEV_EVENT_TRAFFIC_OVERSTOPLINE)

            DPSDK_CORE_ALARM_IVS_TRAFFIC_WITHOUT_SAFEBELT = 612,				 // 交通未系安全带事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_DRIVER_SMOKING = 613,				 // 驾驶员抽烟事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_DRIVER_CALLING = 614,				 // 驾驶员打电话事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_PEDESTRAINRUNREDLIGHT = 615,			 // 行人闯红灯事件(对应 DEV_EVENT_TRAFFIC_PEDESTRAINRUNREDLIGHT_INFO)
            DPSDK_CORE_ALARM_IVS_TRAFFIC_PASSNOTINORDER = 616,				 // 未按规定依次通行(对应DEV_EVENT_TRAFFIC_PASSNOTINORDER_INFO)
            DPSDK_CORE_ALARM_IVS_OBJECT_DETECTION = 621,				 // 物体特征检测事件

            DPSDK_CORE_ALARM_ALARM_ANALOGALARM = 636,				 // 模拟量报警通道的报警事件(对应DEV_EVENT_ALARM_ANALOGALRM_INFO)
            DPSDK_CORE_ALARM_IVS_CROSSLINEDETECTION_EX = 637,				 // 警戒线扩展事件
            DPSDK_CORE_ALARM_ALARM_COMMON = 638,				 // 普通录像
            DPSDK_CORE_ALARM_VIDEOBLIND = 639,				 // 视频遮挡事件(对应 DEV_EVENT_ALARM_VIDEOBLIND)
            DPSDK_CORE_ALARM_ALARM_VIDEOLOSS = 640,				 // 视频丢失事件
            DPSDK_CORE_ALARM_IVS_GETOUTBEDDETECTION = 641,				 // 看守所下床事件(对应 DEV_EVENT_GETOUTBED_INFO)
            DPSDK_CORE_ALARM_IVS_PATROLDETECTION = 642,				 // 巡逻检测事件(对应 DEV_EVENT_PATROL_INFO)
            DPSDK_CORE_ALARM_IVS_ONDUTYDETECTION = 643,				 // 站岗检测事件(对应 DEV_EVENT_ONDUTY_INFO)
            DPSDK_CORE_ALARM_IVS_NOANSWERCALL = 644,				 // 门口机呼叫未响应事件
            DPSDK_CORE_ALARM_IVS_STORAGENOTEXIST = 645,				 // 存储组不存在事件
            DPSDK_CORE_ALARM_IVS_STORAGELOWSPACE = 646,				 // 硬盘空间低报警事件
            DPSDK_CORE_ALARM_IVS_STORAGEFAILURE = 647,				 // 存储错误事件
            DPSDK_CORE_ALARM_IVS_PROFILEALARMTRANSMIT = 648,				 // 报警传输事件
            DPSDK_CORE_ALARM_IVS_VIDEOSTATIC = 649,				 // 视频静态检测事件(对应 DEV_EVENT_ALARM_VIDEOSTATIC_INFO)
            DPSDK_CORE_ALARM_IVS_VIDEOTIMING = 650,				 // 视频定时检测事件(对应 DEV_EVENT_ALARM_VIDEOTIMING_INFO)
            DPSDK_CORE_ALARM_IVS_HEATMAP = 651,				 // 热度图(对应 CFG_IVS_HEATMAP_INFO)
            DPSDK_CORE_ALARM_IVS_CITIZENIDCARD = 652,				 // 身份证信息读取事件(对应 DEV_EVENT_ALARM_CITIZENIDCARD_INFO)
            DPSDK_CORE_ALARM_IVS_PICINFO = 653,				 // 图片信息事件(对应 DEV_EVENT_ALARM_PIC_INFO)
            DPSDK_CORE_ALARM_IVS_NETPLAYCHECK = 654,				 // 上网登记事件(对应 DEV_EVENT_ALARM_NETPLAYCHECK_INFO)
            DPSDK_CORE_ALARM_IVS_TRAFFIC_JAM_FORBID_INTO = 655,				 // 车辆拥堵禁入事件(对应DEV_EVENT_ALARM_JAMFORBIDINTO_INFO)
            DPSDK_CORE_ALARM_IVS_SNAPBYTIME = 656,				 // 定时抓图事件
            DPSDK_CORE_ALARM_IVS_PTZ_PRESET = 657,				 // 云台转动到预置点事件
            DPSDK_CORE_ALARM_IVS_RFID_INFO = 658,				 // 红外线检测信息事件
            DPSDK_CORE_ALARM_IVS_STANDUPDETECTION = 659,				 // 人起立检测事件 
            DPSDK_CORE_ALARM_IVS_QSYTRAFFICCARWEIGHT = 660,				 // 交通卡口称重事件(对应 DEV_EVENT_QSYTRAFFICCARWEIGHT_INFO)
            DPSDK_CORE_ALARM_IVS_SCENE_CHANGE = 665,				 // 场景变更事件(对应 DEV_ALRAM_SCENECHANGE_INFO,CFG_VIDEOABNORMALDETECTION_INFO)

            DPSDK_CORE_ALARM_IVS_NEAR_DISTANCE_DETECTION = 672,				 // 近距离接触事件
            DPSDK_CORE_ALARM_IVS_OBJECTSTRUCTLIZE_PERSON = 673,				 // 行人特征检测事件
            DPSDK_CORE_ALARM_IVS_OBJECTSTRUCTLIZE_NONMOTOR = 674,				 // 非机动车特征检测事件
            DPSDK_CORE_ALARM_IVS_TUMBLE_DETECTION = 675,				 // 倒地报警事件

            DPSDK_CORE_ALARM_IVS_ALIEN_INVASION = 677,				 // 外来人员入侵报警
            DPSDK_CORE_ALARM_IVS_BLACKLIST = 678,				 // 黑名单报警

            // 新增违章报警类型
            DPSDK_CORE_ALARM_VEHICLE_INBUSROUTE = 700,				 // 占用公交车道事件 41
            DPSDK_CORE_ALARM_BACKING = 701,				 // 违章倒车事件     42
            DPSDK_CORE_ALARM_RUN_YELLOWLIGHT = 702,				 // 闯黄灯事件       43
            DPSDK_CORE_ALARM_PARKINGSPACE_PARKING = 703,				 // 车位有车事件     44
            DPSDK_CORE_ALARM_PARKINGSPACE_NOPARKING = 704,				 // 车位无车事件     45
            DPSDK_CORE_ALARM_COVERINGPLATE = 705,
            DPSDK_CORE_ALARM_PARKINGONYELLOWBOX = 706,
            DPSDK_CORE_ALARM_THROW = 707,				 // 交通抛洒物事件	71
            DPSDK_CORE_ALARM_PEDESTRAIN = 708,				 // 交通行人事件		72

            DPSDK_CORE_ALARM_IVS_LINKSD = 813,				 // 813:  球机轮训报警
            DPSDK_CORE_ALARM_IVS_TRAFFIC_TIREDPHYSIOLOGICAL = 819,				 // 生理疲劳驾驶事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_BUSSHARPTURN = 820,				 // 车辆急转报警事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_TIREDLOWERHEAD = 822,				 // 开车低头报警事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_DRIVERLOOKAROUND = 823,				 // 开车左顾右盼事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_DRIVERLEAVEPOST = 824,				 // 开车离岗事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_MAN_NUM_DETECTION = 826,				 // 立体视觉区域内人数统计事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_DRIVERYAWN = 828,				 // 开车打哈欠事件
            DPSDK_CORE_ALARM_IVS_HUMANTRAIT = 833,				 // 人体特征事件
            DPSDK_CORE_ALARM_IVS_INSTALL_CARDREADER = 844,				 // 安装读卡器事件
            DPSDK_CORE_ALARM_IVS_XRAY_DETECTION = 847,				 // X光检测事件 
            DPSDK_CORE_ALARM_IVS_HIGHSPEED = 855,				 // 车辆超速报警事件
            DPSDK_CORE_ALARM_IVS_CROWDDETECTION = 856,				 // 人群密度检测事件
            DPSDK_CORE_ALARM_IVS_TRAFFIC_WAITINGAREA = 864,				 // 违章进入待行区事件						

            DPSDK_CORE_ALARM_IVS_STEREO_FIGHTDETECTION = 867,				 // ATM舱内打架事件
            DPSDK_CORE_ALARM_IVS_STEREO_TAILDETECTION = 868,				 // ATM舱内尾随事件
            DPSDK_CORE_ALARM_IVS_STEREO_STEREOFALLDETECTION = 869,				 // ATM舱内跌倒事件
            DPSDK_CORE_ALARM_IVS_STEREO_STAYDETECTION = 870,				 // ATM舱内滞留事件
            DPSDK_CORE_ALARM_IVS_BANNER_DETECTION = 871,				 // 拉横幅事件
            DPSDK_CORE_ALARM_IVS_ELEVATOR_ABNORMAL = 873,				 // 电动扶梯运行异常事件
            DPSDK_CORE_ALARM_IVS_NONMOTORDETECT = 874,				 // 非机动车检测
            DPSDK_CORE_ALARM_IVS_FIREWARNING = 881,				 // 火警事件
            DPSDK_CORE_ALARM_IVS_SHOPPRESENCE = 882,				 // 商铺占道经营事件
            DPSDK_CORE_ALARM_IVS_WASTEDUMPED = 883,				 // 垃圾违章倾倒事件
            DPSDK_CORE_ALARM_IVS_DISTANCE_DETECTION = 886,				 // 距离异常事件
            DPSDK_CORE_ALARM_IVS_FLOWBUSINESS = 887,				 // 游摊小贩

            DPSDK_CORE_ALARM_IVS_GARBAGE_EXPOSURE = 890,				 // 暴露垃圾-智能城管报警
            DPSDK_CORE_ALARM_IVS_HOLD_UMBRELLA = 891,				 // 违规撑伞-智能城管报警
            DPSDK_CORE_ALARM_IVS_DOOR_FRONT_DIRTY = 892,				 // 门前脏乱-智能城管报警
            DPSDK_CORE_ALARM_IVS_CITYPARKING_MOTOR = 893,				 // 机动车违章停车-智能城管报警
            DPSDK_CORE_ALARM_IVS_CITYPARKING_NOMOTOR = 894,				 // 非机动车违章停车-智能城管报警
            DPSDK_CORE_ALARM_IVS_DUSTBIN_OVER_FLOW = 895,				 // 垃圾桶满溢-智能城管报警
            DPSDK_CORE_ALARM_IVS_LINKSD_CROSS_REGION = 896,				 // NVR枪球联动报警

            DPSDK_CORE_ALARM_IVS_ALARM_CAPTURPIC = 897,				 // 报警抓图
            DPSDK_CORE_ALARM_IVS_TIMING_CAPTURPIC = 898,				 // 定时抓图
            DPSDK_CORE_ALARM_IVS_CLIENT_CAPTURPIC = 899,				 // 客户端抓图
            DPSDK_CORE_ALARM_IVS_M_END = 900,				 // _M3.0特殊的IVS报警结束

            DPSDK_CORE_ALARM_IVS_ABNORMAL_FACEDETECT = 901,				 // 人脸检测事件--智能报警，异常人脸检测，放在区间300~1000内
            DPSDK_CORE_ALARM_IVS_SIMILAR_FACEDETECT = 902,				 // 人脸检测事件--相邻人脸检测报警
            DPSDK_CORE_ALARM_IVS_HIDENOSE_FACEDETECT = 903,				 // 鼻子遮挡报警
            DPSDK_CORE_ALARM_IVS_HIDEMOUTH_FACEDETECT = 904,				 // 嘴部遮挡报警
            DPSDK_CORE_ALARM_IVS_HIDEEYE_FACEDETECT = 905,				 // 眼部遮挡报警

            DPSDK_CORE_ALARM_DETECTIONAREA_PASTEDETECTION = 920,				 // 检测区贴条检测
            DPSDK_CORE_ALARM_KEYBOARDAREA_PASTEDETECTION = 921,				 // 键盘区贴条检测
            DPSDK_CORE_ALARM_SPIGOTAREA_PASTEDETECTION = 922,				 // 插卡区贴条检测
            DPSDK_CORE_ALARM_AUDIO_MUTATION_ALARM = 923,				 // 声强突变报警

            DPSDK_CORE_ALARM_AUDIO_DETECT_ALARM = 924,				 // 音频检测报警
            DPSDK_CORE_ALARM_AUDIO_ANOMALY_ALARM = 925,				 // 音频异常报警
            DPSDK_CORE_ALARM_TRAFFICJUNCTION_NON_MOTOR = 926,				 // 非机动车报警
            DPSDK_CORE_ALARM_CROSSREGION_ENTRY = 927,				 // 进入区域
            DPSDK_CORE_ALARM_CROSSREGION_EXIT = 928,				 // 离开区域
            DPSDK_CORE_ALARM_BLACKLIST_ALARM = 929,				 // 黑名单报警
            DPSDK_CORE_ALARM_STRANGER_ALARM = 930,				 // 陌生人报警
            DPSDK_CORE_ALARM_VIPCUSTOMER_ALARM = 931,				 // VIP客户报警
            DPSDK_CORE_ALARM_WHITELIST_ALARM = 932,				 // 白名单报警
            DPSDK_CORE_ALARM_EMPLOYEE_ALARM = 933,				 // 员工库报警
            DPSDK_CORE_ALARM_LEADER_ALARM = 934,				 // 领导库报警

            // ---ALARM_VIDEOABNORMALDETECTION 报警子类型起始
            DPSDK_CORE_ALARM_IVS_VIDEOABNORMAL_SUBBEGIN = 950,
            DPSDK_CORE_ALARM_IVS_VIDEOABNORMAL_LOST = 950,				 // 视频异常事件:视频丢失
            DPSDK_CORE_ALARM_IVS_VIDEOABNORMAL_FREEZE = 951,				 // 视频异常事件:视频冻结
            DPSDK_CORE_ALARM_IVS_VIDEOABNORMAL_SHELTER = 952,				 // 视频异常事件:摄像头遮挡
            DPSDK_CORE_ALARM_IVS_VIDEOABNORMAL_MOTION = 953,				 // 视频异常事件:摄像头移动
            DPSDK_CORE_ALARM_IVS_VIDEOABNORMAL_HIGHDARK = 954,				 // 视频异常事件:过暗
            DPSDK_CORE_ALARM_IVS_VIDEOABNORMAL_HIGHBRIGHT = 955,				 // 视频异常事件:过亮
            DPSDK_CORE_ALARM_IVS_VIDEOABNORMAL_COLORCAST = 956,				 // 视频异常事件:图像偏色
            DPSDK_CORE_ALARM_IVS_VIDEOABNORMAL_NOISE = 957,				 // 视频异常事件:噪声干扰
            DPSDK_CORE_ALARM_IVS_VIDEOABNORMAL_SCENE_CHANGE = 958,				 // 视频异常事件:场景变更
            DPSDK_CORE_ALARM_IVS_VIDEOABNORMAL_SUBEND = 960,
            DPSDK_CORE_ALARM_IVS_BLACKLIST_FACE = 961,				 // 人脸检测事件--黑名单告警
            DPSDK_CORE_ALARM_CROSSLINEDETECTION_HUMAN = 962,				 // 人穿越警戒线
            DPSDK_CORE_ALARM_CROSSLINEDETECTION_VEHICLE = 963,				 // 机动车穿越警戒线
            DPSDK_CORE_ALARM_CROSSREGIONDETECTION_HUMAN = 964,				 // 人穿越区域
            DPSDK_CORE_ALARM_CROSSREGIONDETECTION_VEHICLE = 965,				 // 机动车穿越区域
            DPSDK_CORE_ALARM_IVS_HUMANTRAIT_WITH_SAFEHAT = 966,				 // 带帽子报警
            DPSDK_CORE_ALARM_IVS_HUMANTRAIT_WITHOUT_SAFEHAT = 967,				 // 不带帽子报警
            DPSDK_CORE_ALARM_TRAFFICJUNCTION_DISAPPEAR = 968,				 // 交通路口事件：车辆离开
            DPSDK_CORE_ALARM_CROSSLINEDETECTION_NonMotor = 969,				 // 非机动车穿越警戒线
            DPSDK_CORE_ALARM_CROSSREGIONDETECTION_NonMotor = 970,				 // 非机动车穿越区域
            DPSDK_CORE_ALARM_DRESS_ABNORMALITIES = 971,				 // 着装异常
            DPSDK_CORE_ALARM_WORKACTION_STATE_NO_WORKER = 972,				 // 无人作业
            DPSDK_CORE_ALARM_WORKACTION_STATE_SINGLE_WORKER = 973,				 // 单人作业
            DPSDK_CORE_ALARM_WORKACTION_STATE_NORED_VEST = 974,				 // 没穿红马甲
            DPSDK_CORE_ALARM_CROSSLINEDETECTION_ENGINEERING_VEHICLE = 975,		 // 工程车穿越警戒线
            DPSDK_CORE_ALARM_CROSSREGIONDETECTION_ENGINEERING_VEHICLE = 976,	 // 工程车穿越区域
            DPSDK_CORE_ALARM_STAYDETECTION_HUMAN = 977,				 // 人停留事件
            DPSDK_CORE_ALARM_STAYDETECTION_VEHICLE = 978,				 // 机动车停留事件
            DPSDK_CORE_ALARM_STAYDETECTION_ENGINEERING_VEHICLE = 979,			 // 工程车停留事件
            DPSDK_CORE_ALARM_ELECTRIC_NO_LADDER = 990,				 // 无梯子报警
            DPSDK_CORE_ALARM_ELECTRIC_NONINSULATED_LADDER = 991,				 // 非绝缘梯子报警 
            DPSDK_CORE_ALARM_ELECTRIC_NO_GLOVE = 992, 				 // 没带手套报警 
            DPSDK_CORE_ALARM_ELECTRIC_NONINSULATED_GLOVE = 993,				 // 非绝缘手套报警
            DPSDK_CORE_ALARM_ELECTRIC_NO_FENCE = 994,				 // 无围栏报警
            DPSDK_CORE_ALARM_ELECTRIC_NO_SIGNBOARD = 995,				 // 无标识牌报警
            DPSDK_CORE_ALARM_ELECTRIC_NO_CURTAIN = 996, 				 // 无红布幔报警
            DPSDK_CORE_ALARM_PERSON_FREQUENCY = 997,			     // 人员频次报警
            DPSDK_CORE_AlAEM_SMILE_FREQUENCY = 998,				 // 微笑频次报警

            DPSDK_CORE_ALARM_IVS_ALARM_END = 1000,				 // 智能设备报警类型的范围为300-1000
            DPSDK_CORE_ALARM_OSD = 1001,				 // osd信息
            DPSDK_CORE_ALARM_CROSS_INFO = 1002,				 // 十字路口
            DPSDK_CORE_ALARM_VTM_DISCONNETED = 1003,				 // vtm离线报警
            DPSDK_CORE_ALARM_PTS_FORTIFY_ILLEGALTIMEPASS = 1004,              // 布控在白名单内，非法时间段通过车辆
            DPSDK_CORE_ALARM_PTS_FORTIFY_NOTINWHITELIST = 1005,              // 布控未在白名单内车辆
            DPSDK_CORE_ALARM_PTS_FORTIFY_PLATERECOGNISEFAIL = 1006,              // 布控车牌无法识别车辆 
            DPSDK_CORE_ALARM_DEVICE_LATEST_SHUTDOWN = 1007,				 // mtp300 设备新增最后一次关机后再开机报警
            DPSDK_CORE_ALARM_TF_CARD_STORAGE_LOW_SPACE = 1008,				 // mtp300 TF卡容量过低报警
            DPSDK_CORE_ALARM_DEV_UNPLUG_DISK = 1009,				 // 拔掉硬盘报警

            DPSDK_CORE_ALARM_CLIENT_ALARM_BEGIN = 1100,				 // 客户端平台报警开始
            DPSDK_CORE_ALARM_CLIENT_DERELICTION = 1101,				 // 遗留检测[交通事件-抛洒物]
            DPSDK_CORE_ALARM_CLIENT_RETROGRADATION = 1102,				 // 逆行 [交通事件]
            DPSDK_CORE_ALARM_CLIENT_OVERSPEED = 1103,				 // 超速  [交通事件]
            DPSDK_CORE_ALARM_CLIENT_LACK_ALARM = 1104,				 // 欠速  [交通事件]
            DPSDK_CORE_ALARM_CLIENT_FLUX_COUNT = 1105,				 // 流量统计[交通事件]
            DPSDK_CORE_ALARM_CLIENT_PARKING = 1106,				 // 停车检测[交通事件]
            DPSDK_CORE_ALARM_CLIENT_PASSERBY = 1107,				 // 行人检测[交通事件]
            DPSDK_CORE_ALARM_CLIENT_JAM = 1108,				 // 拥堵检测[交通事件]
            DPSDK_CORE_ALARM_CLIENT_AREA_INBREAK = 1109,				 // 特殊区域入侵
            DPSDK_CORE_ALARM_USER = 1122,				 // 用户报警
            DPSDK_CORE_ALARM_USER_OVERSPEED = 1123,              //来自用户的超速报警
            DPSDK_CORE_ALARM_USER_VTT_EMERGENCY = 1124,           	 // 用户VTT紧急报警，由客户端上报
            DPSDK_CORE_ALARM_USER_AH_EMERGENCY = 1125,            	 // 用户报警主机紧急报警，由客户端上报 

            //人脸检测事件细化事件
            ALARM_TYPE_ALARM_FACEDETECT_NORMAL = 1151,				 // 人脸检测事件中－正常人脸
            ALARM_TYPE_ALARM_FACEDETECT_UNNORMAL = 1152,				 // 人脸检测事件中－异常人脸

            DPSDK_CORE_ALARM_CLIENT_ALARM_END = 1200,				 // 客户端平台报警结束

            ALARM_SYSTEM_BEGIN = 1200,					// 来自系统的报警
            ALARM_HOST_TEMPRATURER = 1201,					// 主机温度过高
            ALARM_RAID_LOAD = 1202,					// raid降级
            ALARM_SERVER_AUTO_MIGRATE = 1203,					// 服务器自动迁移
            ALARM_SERVER_MANUAL_MIGRATE = 1204,					// 服务器手动迁移
            ALARM_SERVER_STATUS_CHANGE = 1205,					// 服务器状态变更 
            ALARM_MASTER_TO_BACKUP = 1206,					// 双机热备主机切备机
            ALARM_BACKUP_TO_MASTER = 1207,					// 双机热备备机切主机
            ALARM_BACKUP_ABNORMAL = 1208,					// 双机热备备机故障
            ALARM_BACKUP_NORMAL = 1209,					// 双机热备备机故障恢复
            ALARM_STORAGE_WARNING = 1210,					//存储到达（70，89）这个区间时的报警（单位：天）(confict!)
            ALARM_STORAGE_STOP = 1211,					//存储达到90天时，停止录像的报警10:42(confict!)
            ALARM_POWER_ABNORMITY = 1213,					//电源异常报警
            ALARM_SYSTEM_POWER_OFF = 1214,					//系统断电报警【市电断开】
            ALARM_SYSTEM_POWER_ON = 1215,					//系统电源恢复报警【市电恢复】
            ALARM_DLT_TRANSFER_FAULT = 1216,                 //DSS平台与DLT平台通信故障（泰国Usupply项目)
            ALARM_SSD_LIFETIME_ABNORMAL = 1217,					// SSD寿命异常
            ALARM_SSD_STATUS_ABNORMAL = 1218,					// SSD状态异常
            //哈牡客专平台管理满站铁科院检测需求
            ALARM_CS_OFFLINE = 1219,				//111当检测到系统中的cs服务状态由正常变为离线时触发
            ALARM_MDS_OFFLINE = 1220,				//108当检测到系统中的mds服务由正常变为离线时触发
            ALARM_DATA_NODE_OFFLINE = 1221,				//103当存储节点的状态由正常变为离线时触发
            ALARM_DISK_OFFLINE = 1222,				//104当节点上的某块磁盘状态由非离线变为离线状态时触发
            ALARM_DISK_SLOW = 1223,				//105当磁盘状态由正常变为慢盘时触发
            ALARM_DISK_BROKEN = 1224,				//106当磁盘的状态由正常变为损坏时触发
            ALARM_DISK_ERR = 1225, 				//107当磁盘状态由正常变为错误盘时触发
            ALARM_SYSTEM_DISK_FULL = 1226,				//服务器磁盘满报警 
            ALARM_SYSTEM_END = 1300,
            // -F 3.0 报警类型新增
            ALARM_DOOR_VTO_AUTH = 1394,				//VTO鉴权
            ALARM_DOOR_PLATFORM_AUTH = 1395,				//平台鉴权
            ALARM_DOOR_LADDERCONTROL_AUTH = 1396,				//梯控主机鉴权
            ALARM_DOOR_LOCKED = 1397,				// 门锁锁死     
            ALARM_DOOR_UNLOCKED = 1398,				// 门锁解除
            ALARM_OUT_STAT_OPEN = 1399,				// 报警输出打开状态    

            ALARM_DOOR_SNAP_PIC = 1400,					// 门禁报警抓图
            ALARM_DEV_OFFLINE = 1401,					// 将设备断线也当做报警，CMS->ADS
            ALARM_IPSAN_OFFLINE = 1402,					// 将IPSAN存储掉线(或卸载)都当做报警，SS->CMS->ADS
            ALARM_ALARM_HOST_OFFLINE = 1403,					// 报警主机断线当做报警，PES->CMS->ADS
            ALARM_IP_DEV_IGNORE_TALK = 1404,					// IP对讲网点取消对讲请求
            ALARM_DEV_NEW_FILE = 1405,					// 设备NewFile事件通知
            ALARM_DEV_SNAP_UPLOAD = 1406,					// 图片上传成功事件
            //ALARM_DOOR_LOCKED                 = 1404,                 // 门锁锁死
            //ALARM_DOOR_UNLOCKED               = 1405,                 // 门锁解除
            //ALARM_OUT_STAT_OPEN               = 1406,                 // 报警输出打开状态
            ALARM_OUT_STAT_CLOSED = 1407,                 // 报警输出关闭状态
            ALARM_DOOR_OFFLINE = 1408,					// 报警门禁离线当做报警
            ALARM_IP_DEV_OFFLINE = 1409,					// IP对讲设备离线当做报警
            ALARM_MULTI_DEV_OFFLINE = 1410,					// 批量设备离线报警
            //-F 门禁设备报警新增区间（40-70不够用了）
            ALARM_DOOR_NEW_BEGIN = 1411,
            ALARM_DOOR_FORCE_LOCKED = 1411,					// 门禁强锁报警
            ALARM_DOOR_FORCE_OPEN = 1412,					// 门禁强开报警
            ALARM_DOOR_URGENCY_OPEN_ALL = 1413,					// 门禁紧急全开报警
            ALARM_DOOR_URGENCY_LOCKED_ALL = 1414,					// 门禁紧急全关报警
            ALARM_DOOR_KEY_OPEN_DOOR = 1415,					// 门禁钥匙开门报警
            ALARM_DOOR_LOCK_SHAKE = 1416,					// 门禁锁体震动报警
            ALARM_DOOR_SMOKE = 1417,					// 烟雾报警（防撬门外接的探测器）
            ALARM_DOOR_WALL_SHAKE = 1418,					// 墙体震动报警（防撬门外接的探测器）
            ALARM_DOOR_ATM_SHAKE = 1419,					// 取款机震动报警（防撬门外接的探测器）
            ALARM_DOOR_UNLOCK_OVERTIME = 1420,					// 门禁超时未关门报警
            ALARM_DOOR_RFID_ACTIVE = 1421,					// 有源RFID门禁刷卡报警
            ALARM_DOOR_RFID_PASSIVE = 1422,					// 无源RFID门禁刷卡报警
            ALARM_DOOR_MODE_ARMING = 1423,					// 门禁布防设置
            ALARM_DOOR_MODE_DISARMING = 1424,					// 门禁撤防设置
            ALARM_DOOR_CHASSIS_INTRUDED = 1425,					// 门禁防拆报警
            ALARM_DOOR_AUTH_REQUEST = 1426,					// 门禁请求授权
            ALARM_DOOR_FINGERPRINT = 1427,					// 考勤系统指纹事件
            ALARM_DOOR_VALID_CARD_OPENDOOR_IN = 1428,					//对接海康设备 进门刷卡成功
            ALARM_DOOR_VALID_CARD_OPENDOOR_OUT = 1429,  				//对接海康设备 出门刷卡成功
            ALARM_DOOR_BREAK_IN = 1430,  				// 门禁闯入事件
            ALARM_DOOR_ERR_NODOORRIGHT = 1431,					// 该门没有权限
            ALARM_DOOR_ERR_CARDRIGHT_PWDERR = 1432,					// 卡号正确但是密码错误
            ALARM_DOOR_BLACK_USER = 1433,					//黑名单用户
            ALARM_VALID_VRCODE_OPENDOOR = 1434,					//合法二维码开门
            ALARM_INVALID_VRCODE_OPENDOOR = 1435,					//非法二维码开门
            ALARM_VALID_IDCARD = 1436,					//人证合法开门	
            ALARM_INVALID_IDCARD = 1437,					//人证非法开门
            ALARM_INVALID_IDCARD_AND_IC = 1438,					//人证和身份证非法开门
            ALARM_VALID_IDCARD_AND_IC = 1439,					//人证和身份证合法开门
            ALARM_PATROL_STATUS = 1440,					//巡更状态报警
            ALARM_VALID_BT_OPENDOOR = 1441,					//蓝牙合法开门
            ALARM_INVALID_BT_OPENDOOR = 1442,					//蓝牙非法开门
            ALARM_DOOR_LOCAL_ALARM = 1443,					//门禁外部报警
            ALARM_DOOR_CHANL_MODEL = 1444,					//通道模式
            ALARM_DOOR_CHANL_AWAYS_STATUS = 1445,					//通道常开，常关状态
            ALARM_DOOR_MALICIOUT = 1446,					//二代门禁非法卡超次报警
            ALARM_DOOR_HEIGHTLIMIT = 1447,                 // 门禁限高报警
            ALARM_DOOR_RFID = 1448,					// RFID感应报警
            ALARM_DOOR_RFID_INVALID = 1449,					// RFID非法感应报警
            ALARM_DOOR_RFID_LOCAL = 1450,					// RFID外部报警(按键报警)
            ALARM_RFID_PEOPLE_UPPER_LIMIT = 1451,					// RFID人数上限报警
            ALARM_DOOR_FACEINFO_COLLECT = 1452,					// 人脸信息录入事件
            ALARM_DOOR_FAST_OPER_SCHEDULE = 1453,					//快速下发(复核、下发)
            ALARM_DOOR_RFID_POSITION_INVALID = 1454,					//RFID非法位置报警
            ALARM_VALID_METHOD_CARD_FIRST = 1455,					//先刷卡后密码合法开门
            ALARM_INVALID_METHOD_CARD_FIRST = 1456,					//先刷卡后密码非法开门
            ALARM_VALID_PWD_CARD_FINGERPRINT = 1457,					//密码+刷卡+指纹组合合法开门
            ALARM_INVALID_PWD_CARD_FINGERPRINT = 1458,					//密码+刷卡+指纹组合非法开门
            ALARM_VALID_PWD_FINGERPRINT = 1459,					//密码+指纹组合合法开门
            ALARM_INVALID_PWD_FINGERPRINT = 1460,					//密码+指纹组合非法开门
            ALARM_VALID_CARD_FINGERPRINT = 1461,					//刷卡+指纹组合合法开门
            ALARM_INVALID_CARD_FINGERPRINT = 1462,					//刷卡+指纹组合非法开门
            ALARM_VALID_PERSONS = 1463,					//多人合法开门
            ALARM_INVALID_PERSONS = 1464,					//多人非法开门
            ALARM_VALID_KEY = 1465,					//钥匙合法开门
            ALARM_INVALID_KEY = 1466,					//钥匙非法开门
            ALARM_VALID_USERID_AND_PWD = 1467,					//UserID+密码合法开门
            ALARM_INVALID_USERID_AND_PWD = 1468,					//UserID+密码非法开门
            ALARM_VALID_FACE_AND_PWD = 1469,					//人脸+密码合法开门
            ALARM_INVALID_FACE_AND_PWD = 1470,					//人脸+密码非法开门
            ALARM_VALID_FINGERPRINT_AND_PWD = 1471,					//指纹+密码合法开门
            ALARM_INVALID_FINGERPRINT_AND_PWD = 1472,					//指纹+密码非法开门
            ALARM_VALID_FINGERPRINT_AND_FACE = 1473,					//指纹+人脸合法开门
            ALARM_INVALID_FINGERPRINT_AND_FACE = 1474,					//指纹+人脸非法开门
            ALARM_VALID_CARD_AND_FACE = 1475,					//刷卡+人脸合法开门
            ALARM_INVALID_CARD_AND_FACE = 1476,					//刷卡+人脸非法开门
            ALARM_VALID_FACE_OR_PWD = 1477,					//人脸或密码合法开门
            ALARM_INVALID_FACE_OR_PWD = 1478,					//人脸或密码非法开门
            ALARM_VALID_FINGERPRINT_OR_PWD = 1479,					//指纹或密码合法开门
            ALARM_INVALID_FINGERPRINT_OR_PWD = 1480,					//指纹或密码非法开门
            ALARM_VALID_FINGERPRINT_OR_FACE = 1481,					//指纹或人脸合法开门
            ALARM_INVALID_FINGERPRINT_OR_FACE = 1482,					//指纹或人脸非法开门
            ALARM_VALID_CARD_OR_FACE = 1483,					//刷卡或人脸合法开门
            ALARM_INVALID_CARD_OR_FACE = 1484,					//刷卡或人脸非法开门
            ALARM_VALID_CARD_OR_FINGERPRINT = 1485,					//刷卡或指纹合法开门
            ALARM_INVALID_CARD_OR_FINGERPRINT = 1486,					//刷卡或指纹非法开门
            ALARM_VALID_FINGERPRINT_AND_FACE_AND_PWD = 1487,					//指纹+人脸+密码合法开门
            ALARM_INVALID_FINGERPRINT_AND_FACE_AND_PWD = 1488,					//指纹+人脸+密码非法开门
            ALARM_VALID_CARD_AND_FACE_AND_PWD = 1489,					//刷卡+人脸+密码合法开门
            ALARM_INVALID_CARD_AND_FACE_AND_PWD = 1490,					//刷卡+人脸+密码非法开门
            ALARM_VALID_CARD_AND_FINGERPRINT_AND_PWD = 1491,					//刷卡+指纹+密码合法开门
            ALARM_INVALID_CARD_AND_FINGERPRINT_AND_PWD = 1492,					//刷卡+指纹+密码非法开门
            ALARM_VALID_CARD_AND_PWD_AND_FACE = 1493,					//卡+指纹+人脸组合合法开门
            ALARM_INVALID_CARD_AND_PWD_AND_FACE = 1494,					//卡+指纹+人脸组合非法开门
            ALARM_VALID_FINGERPRINT_OR_FACE_OR_PWD = 1495,					//指纹或人脸或密码合法开门
            ALARM_INVALID_FINGERPRINT_OR_FACE_OR_PWD = 1496,					//指纹或人脸或密码非法开门
            ALARM_VALID_CARD_OR_FACE_OR_PWD = 1497,					//卡或人脸或密码合法开门
            ALARM_INVALID_CARD_OR_FACE_OR_PWD = 1498,					//卡或人脸或密码非法开门

            ALARM_DOOR_NEW_END = 1499,

            // -E 视频质量诊断 新增12种报警类型
            ALARM_VQDS_VIDEO_LOST = 1500,					// 视频质量诊断-视频丢失
            ALARM_VQDS_HIGHBRIGHT = 1501,					// 高亮度警告
            ALARM_VQDS_HIGHBRIGHT_RED = 1502,					// 高亮度红色报警
            ALARM_VQDS_LOWBRIGHT = 1503,					// 低亮度警告
            ALARM_VQDS_LOWBRIGHT_RED = 1504,					// 低亮度红色报警
            ALARM_VQDS_CONTRAST = 1505,					// 对比度警告
            ALARM_VQDS_CONTRAST_RED = 1506,					// 对比度红色报警
            ALARM_VQDS_CLARITY = 1507,					// 清晰度警告
            ALARM_VQDS_CLARITY_RED = 1508,					// 清晰度红色报警
            ALARM_VQDS_COLOR_OFFSET = 1509,					// 色彩偏差警告
            ALARM_VQDS_COLOR_OFFSET_RED = 1510,					// 偏色红色报警
            ALARM_VQDS_DIAGNOSE_FAIL = 1511,					// 视频质量诊断失败
            // 报警运营平台新增
            ALARM_ALARMHOST_INBREAK = 1598,					// 入侵报警
            ALARM_ALARMHOST_FAULT = 1599,					// 故障报警 
            ALARM_ALARMHOST_EnableArm = 1600,					// 报警主机布防 
            ALARM_ALARMHOST_DisableArm = 1601,					// 报警主机撤防 
            ALARM_ALARMHOST_BYPASS_BYPASS = 1602,					// 防区旁路 
            ALARM_ALARMHOST_BYPASS_NORMAL = 1603,					// 防区取消旁路

            ALARM_ALARMHOST_MEDICAL = 1604,				 // 医疗报警
            ALARM_ALARMHOST_URGENCY = 1605,				 // 报警主机紧急报警
            ALARM_ALARMHOST_CATCH = 1606,				 // 挟持报警
            ALARM_ALARMHOST_MENACE_SLIENCE = 1607,				 // 无声威胁
            ALARM_ALARMHOST_PERIMETER = 1608,				 // 周界报警
            ALARM_ALARMHOST_DEFENCEAREA_24H = 1609,				 // 24小时防区报警
            ALARM_ALARMHOST_DEFENCEAREA_DELAY = 1610,				 // 延时防区报警
            ALARM_ALARMHOST_DEFENCEAREA_INITIME = 1611,				 // 及时防区报警
            ALARM_ALARMHOST_BREAK = 1612,				 // 防拆
            ALARM_ALARMHOST_AUX_OVERLOAD = 1613,                 // AUX过流
            ALARM_ALARMHOST_AC_POWDOWN = 1614,                 // 交流电掉电
            ALARM_ALARMHOST_BAT_DOWN = 1615,                 // 电池欠压
            ALARM_ALARMHOST_SYS_RESET = 1616,                 // 系统复位
            ALARM_ALARMHOST_PROGRAM_CHG = 1617,                 // 电池掉线
            ALARM_ALARMHOST_BELL_CUT = 1618,                 // 警号被切断或短路
            ALARM_ALARMHOST_PHONE_ILL = 1619,                 // 电话切断或失效
            ALARM_ALARMHOST_MESS_FAIL = 1620,				 // 通讯失败
            ALARM_ALARMHOST_WIRELESS_PWDOWN = 1621,				 // 无线探测器欠压
            ALARM_ALARMHOST_SIGNIN_FAIL = 1622,				 // 登录失败
            ALARM_ALARMHOST_ERR_CODE = 1623,				 // 错误密码登陆
            ALARM_ALARMHOST_MANAUL_TEST = 1624,				 // 手动测试
            ALARM_ALARMHOST_CYCLE_TEST = 1625,				 // 定期测试
            ALARM_ALARMHOST_SVR_REQ = 1626,				 // 服务请求
            ALARM_ALARMHOST_BUF_RST = 1627,				 // 报警缓冲复位
            ALARM_ALARMHOST_CLR_LOG = 1628,				 // 清除日志
            ALARM_ALARMHOST_TIME_RST = 1629,				 // 日期时间复位
            ALARM_ALARMHOST_NET_FAIL = 1630,				 // 网络错误
            ALARM_ALARMHOST_IP_CONFLICT = 1631,				 // IP冲突
            ALARM_ALARMHOST_KB_BREAK = 1632,				 // 键盘防拆
            ALARM_ALARMHOST_KB_ILL = 1633,				 // 键盘问题
            ALARM_ALARMHOST_SENSOR_O = 1634,				 // 探测器开路
            ALARM_ALARMHOST_SENSOR_C = 1635,				 // 探测器短路
            ALARM_ALARMHOST_SENSOR_BREAK = 1636,				 // 探测器防拆
            ALARM_FIRE_ALARM = 1637,				 // 报警主机火警

            //接警主机报警end
            ALARM_POWER_MAJORTOSPARE = 1640,					// 主电源切备用电源
            ALARM_POWER_SPARETOMAJOR = 1641,					// 备用电源切主电源
            ALARM_ENCODER_ALARM = 1642,					// 编码器故障报警
            ALARM_DEVICE_REBOOT = 1643,					// 设备重启报警
            ALARM_DISK = 1644,					// 硬盘报警
            ALARM_NET_ABORT_WIRE = 1645,					// 有线网络故障报警
            ALARM_NET_ABORT_WIRELESS = 1646,					// 无线网络故障报警
            ALARM_NET_ABORT_3G = 1647,					// 3G网络故障报警
            ALARM_MAC_CONFLICT = 1648,					// MAC冲突
            ALARM_POWER_OFF_BACKUP = 1649,					// 备用电源掉电
            ALARM_CHASSISINTRUDED = 1650,					// 机箱入侵	
            ALARM_PSTN_BREAK_LINE = 1651,					// PSTN掉线报警
            ALARM_CALL_ALARM_HOST = 1652,					// 电话报警主机设备报警
            ALARM_CALL_ALARM_HOST_CHN = 1653,					// 电话报警主机通道报警
            ALARM_RCEMERGENCY_CALL = 1654,					// 紧急救助未知事件
            ALARM_RCEMERGENCY_CALL_KEYBOARD_FIRE = 1655,				// 紧急救助键盘区火警
            ALARM_RCEMERGENCY_CALL_KEYBOARD_DURESS = 1656,				// 紧急救助键盘区胁迫
            ALARM_RCEMERGENCY_CALL_KEYBOARD_ROBBER = 1657,				// 紧急救助键盘区匪警
            ALARM_RCEMERGENCY_CALL_KEYBOARD_MEDICAL = 1658,				// 紧急救助键盘区医疗
            ALARM_RCEMERGENCY_CALL_KEYBOARD_EMERGENCY = 1659,			// 紧急救助键盘区紧急
            ALARM_RCEMERGENCY_CALL_WIRELESS_EMERGENCY = 1660,			// 紧急救助遥控器紧急
            ALARM_LOCK_BREAK = 1661,					// 撬锁
            ALARM_ACCESS_CTL_OPEN = 1662, 				// 视频报警主机异常开门
            // 报警运营平台新增end
            //DSS-H富士康项目报警主机报警类型
            ALARM_ALARMHOST_SMOKE = 1670,					// 报警主机烟感报警
            ALARM_SAND_PICKING_BOATS = 1671,					// 采砂船报警
            ALARM_ARTIFICIAL_REPORT = 1672,					// 人工上报 

            //动环(PE)报警-(SCS_ALARM_SWITCH_START 取名直接来自SCS动环文档)
            //系统工程动环增加报警类型ALARM_SCS_BEGIN
            //开关量，不可控
            ALARM_SCS_SWITCH_START = 1800,
            ALARM_SCS_INFRARED = 1801,					// 红外对射告警
            ALARM_SCS_SMOKE = 1802,					// 烟感告警
            ALARM_SCS_WATER = 1803,                	// 水浸告警
            ALARM_SCS_COMPRESSOR = 1804,           		// 压缩机故障告警
            ALARM_SCS_OVERLOAD = 1805,             	// 过载告警
            ALARM_SCS_BUS_ANOMALY = 1806,          		// 母线异常
            ALARM_SCS_LIFE = 1807,                 // 寿命告警
            ALARM_SCS_SOUND = 1808,					// 声音告警
            ALARM_SCS_TIME = 1809,                 // 时钟告警
            ALARM_SCS_FLOW_LOSS = 1810,            		// 气流丢失告警
            ALARM_SCS_FUSING = 1811,               	// 熔断告警
            ALARM_SCS_BROWN_OUT = 1812,            		// 掉电告警
            ALARM_SCS_LEAKING = 1813,              	// 漏水告警
            ALARM_SCS_JAM_UP = 1814,               	// 堵塞告警
            ALARM_SCS_TIME_OUT = 1815,             	// 超时告警
            ALARM_SCS_REVERSE_ORDER = 1816,        			// 反序告警
            ALARM_SCS_NETWROK_FAILURE = 1817,      			// 组网失败告警
            ALARM_SCS_UNIT_CODE_LOSE = 1818,       			// 机组码丢失告警
            ALARM_SCS_UNIT_CODE_DISMATCH = 1819,   				// 机组码不匹配告警
            ALARM_SCS_FAULT = 1820,					// 故障告警
            ALARM_SCS_UNKNOWN = 1821,              	// 未知告警
            ALARM_SCS_CUSTOM = 1822,               	// 自定义告警
            ALARM_SCS_NOPERMISSION = 1823,         		// 无权限告警
            ALARM_SCS_INFRARED_DOUBLE = 1824,      			// 红外双鉴告警
            ALARM_SCS_ELECTRONIC_FENCE = 1825,     			// 电子围栏告警
            ALARM_SCS_UPS_MAINS = 1826,            		// 市电正常市电异常
            ALARM_SCS_UPS_BATTERY = 1827,          		// 电池正常电池异常
            ALARM_SCS_UPS_POWER_SUPPLY = 1828,     			// UPS正常输出旁路供电
            ALARM_SCS_UPS_RUN_STATE = 1829,        			// UPS正常UPS故障
            ALARM_SCS_UPS_LINE_STYLE = 1830,       			// UPS类型为在线式UPS类  型为后备式
            ALARM_SCS_XC = 1831,                 // 小车
            ALARM_SCS_DRQ = 1832,                 // 断路器
            ALARM_SCS_GLDZ = 1833,                 // 隔离刀闸
            ALARM_SCS_JDDZ = 1834,                	// 接地刀闸
            //ALARM_SCS_IN_END					= 1835,					// 请注意这个值，不用把他作为判断值；只标记说“开关量，不可控”结束；
            //因为接下来的“开关量，可控”没有开始标记如ALARM_SCS_DOOR_START

            ALARM_SCS_COOL_WATER = 1840,					// 冷水机组通道 
            ALARM_SCS_ADD_HUMID = 1841,					// 加湿器通道 
            ALARM_SCS_HUMITURE = 1842,					// 温湿度通道 
            ALARM_SCS_PROSSURE = 1843, 				// 压差通道 
            ALARM_SCS_STS = 1844,					// STS通道 
            ALARM_SCS_ATS = 1845,					// ATS通道 
            ALARM_SCS_RPP = 1846,					// RPP通道 
            ALARM_SCS_KT = 1847,					// KT通道 
            ALARM_SCS_IN_END,

            //开关量，可控，请注意接下来的ALARM_SCS_DOOR_SWITCH这个不能作为BEGIN用
            ALARM_SCS_DOOR_SWITCH = 1850,					// 门禁控制器开关告警
            ALARM_SCS_UPS_SWITCH = 1851,					// UPS开关告警,
            ALARM_SCS_DBCB_SWITCH = 1852,          		// 配电柜开关告警
            ALARM_SCS_ACDT_SWITCH = 1853,          		// 空调开关告警
            ALARM_SCS_DTPW_SWITCH = 1854,          		// 直流电源开关告警
            ALARM_SCS_LIGHT_SWITCH = 1855,         		// 灯光控制器开关告警
            ALARM_SCS_FAN_SWITCH = 1856,           		// 风扇控制器开关告警
            ALARM_SCS_PUMP_SWITCH = 1857,          		// 水泵开关告警
            ALARM_SCS_BREAKER_SWITCH = 1858,       			// 刀闸开关告警
            ALARM_SCS_RELAY_SWITCH = 1859,         		// 继电器开关告警
            ALARM_SCS_METER_SWITCH = 1860,        			// 电表开关告警
            ALARM_SCS_TRANSFORMER_SWITCH = 1861,   				// 变压器开关告警
            ALARM_SCS_SENSOR_SWITCH = 1862,        			// 传感器开关告警
            ALARM_SCS_RECTIFIER_SWITCH = 1863,     			// 整流器告警
            ALARM_SCS_INVERTER_SWITCH = 1864,      			// 逆变器告警
            ALARM_SCS_PRESSURE_SWITCH = 1865,      			// 压力开关告警
            ALARM_SCS_SHUTDOWN_SWITCH = 1866,      			// 关机告警
            ALARM_SCS_WHISTLE_SWITCH = 1867,	   				// 警笛告警
            ALARM_SCS_SWITCH_END,
            //模拟量
            ALARM_SCS_ANALOG_START = 1880,
            ALARM_SCS_TEMPERATURE = 1881,					// 温度告警
            ALARM_SCS_HUMIDITY = 1882,             	// 湿度告警
            ALARM_SCS_CONCENTRATION = 1883,        			// 浓度告警
            ALARM_SCS_WIND = 1884,                 // 风速告警
            ALARM_SCS_VOLUME = 1885,               	// 容量告警
            ALARM_SCS_VOLTAGE = 1886,              	// 电压告警
            ALARM_SCS_ELECTRICITY = 1887,          		// 电流告警
            ALARM_SCS_CAPACITANCE = 1888,          		// 电容告警
            ALARM_SCS_RESISTANCE = 1889,           		// 电阻告警
            ALARM_SCS_CONDUCTANCE = 1890,          		// 电导告警
            ALARM_SCS_INDUCTANCE = 1891,           		// 电感告警
            ALARM_SCS_CHARGE = 1892,               	// 电荷量告警
            ALARM_SCS_FREQUENCY = 1893,            		// 频率告警
            ALARM_SCS_LIGHT_INTENSITY = 1894,      			// 发光强度告警(坎)
            ALARM_SCS_PRESS = 1895,                	// 力告警（如牛顿，千克力）
            ALARM_SCS_PRESSURE = 1896,             	// 压强告警（帕，大气压）
            ALARM_SCS_HEAT_TRANSFER = 1897,        			// 导热告警（瓦每平米）
            ALARM_SCS_THERMAL_CONDUCTIVITY = 1898, 				// 热导告警（kcal/(m*h*℃)）
            ALARM_SCS_VOLUME_HEAT = 1899,          		// 比容热告（kcal/(kg*℃)）
            ALARM_SCS_HOT_WORK = 1900,             	// 热功告警（焦耳）
            ALARM_SCS_POWER = 1901,                	// 功率告警（瓦）
            ALARM_SCS_PERMEABILITY = 1902,         		// 渗透率告警（达西）
            ALARM_SCS_PROPERTION = 1903,					// 比例（包括电压电流变比，功率因素，负载单位为%） 
            ALARM_SCS_ENERGY = 1904,					// 电能（单位为J）
            ALARM_SCS_TIME_EX = 1905,					// 时间
            ALARM_SCS_ANALOG_END,
            //ALARM_SCS_END,

            ALARM_IP_DEV_TALK = 1907,					// IP设备对讲报警

            ALARM_TYPE_UNIFY_BEGIN = 1908,					// 报警类型统一管理，不需要在EnumCenterRecType增加
            ALARM_VOICE_EXCEPTION = 1909,					// 音频异常报警	
            //ALARM_TYPE_UNIFY_END,										// 报警类型统一管理，不需要在EnumCenterRecType增加
            ALARM_RECORD_EXCEPTION = 1910,					// 录像异常报警
            ALARM_VOICE_LOSE = 1911,					// 音频丢失报警
            ALARM_WIFITERM_FIND = 1912,					// WIFI终端发现报警
            ALARM_WIFITERM_SURVEY = 1913,					// WIFI终端布控报警
            ALARM_PTZ_DIAGNOSES = 1914,					// 云台诊断信息
            ALARM_SNAP_ALARM = 1915,					// 通用抓图报警

            //EVS新增报警类型
            ALARM_NO_DISK = 1916,					// 无硬盘报警 
            ALARM_DOUBLE_DEV_VERSION_ABNORMAL = 1917,					// 双控设备主板与备板之间版本信息不一致异常事件 
            ALARM_DCSSWITCH = 1918,					// 主备切换事件/集群切换报警 
            ALARM_DEV_RAID_FAILED = 1919,					// 设备RAID错误报警 
            ALARM_DEV_RAID_DEGRADED = 1920,					// 设备RAID降级报警 
            ALARM_BUF_DROP_FRAME = 1921,					// 录像缓冲区丢帧报警
            ALARM_WIFI_VIRTUALINFO_SEARCH = 1922,					// WIFI终端MAC采集虚拟身份报警
            ALARM_PATIENTDETECTION = 1923,					// 监控病人活动状态报警事件
            ALARM_STORAGE_ERROR_PATITION = 1924,                 // 存储分区错误
            ALARM_RAID_STATE_EX = 1925,                 // RAID异常报警
            ALARM_SERVER_ABNORMAL = 1926,					// 设备本身服务异常报警 设备中使用iscsi，ftp，samba，nfs功能时，服务异常会给出报警

            ALARM_WANDERDETECTION_EVENT = 1994,					// 徘徊报警
            ALARM_RIOTERDETECTION_EVENT = 1995,					// 人员聚集报警
            ALARM_SCENNE_CHANGE_ALARM = 1996,					// 场景变更报警
            ALARM_VIDEO_UNFOCUS = 1997,					// 视频虚焦报警
            ALARM_DEV_AUDIO_MUTATION = 1998,					// 声强突变报警
            ALARM_HEATIMG_TEMPER = 1999,                 // 热成像测温点温度异常报警事件 

            AE_ALARM_TYPE_BEGIN = 2000,
            ALARM_RFID_BATTERY_EMPTY = 2010,						//射频设备低电量报警
            ALARM_RFID_BUTTON = 2011,						//射频设备按键报警
            ALARM_RFID_DATA_EXCEPTION = 2012,						//射频设备数据异常报警
            ALARM_RFID_ENTER_RECEIVER = 2013,						//射频设备接收器感应到手环报警
            ALARM_RFID_ILLEGAL_ENTER = 2014,						//非法进入
            ALARM_RFID_ILLEGAL_LEAVE = 2015,						//非法离开
            ALARM_RFID_ILLEGAL_GATHER = 2016,						//非法聚集
            ALARM_RFID_WITHOUT_TUTELAGE = 2017,						//无监护报警
            ALARM_RFID_STAY = 2018,						//滞留报警
            ALARM_RFID_EXCEPTION = 2019,						//异常报警
            ALARM_RFID_CUTOFF_LABEL = 2021,						//人员标签剪断
            ALARM_RFID_GPS = 2022,						//射频设备GPS上报
            ALARM_RFID_APPROACH = 2024,						//接近边界管理器
            ALARM_RFID_LEAVEAWAY = 2025,						//远离边界管理器
            ALARM_RFID_OFFLINE = 2026,						//离线超时报警
            ALARM_RFID_SingleInterrogation = 2027,                     //单人审讯报警
            ALARM_RFID_WaitingRoomTimeOut = 2028,                     //候问室超时报警
            ALARM_RFID_Unattended = 2029,                     //无人看管
            ALARM_RFID_InterrogationTimeout = 2030,                     //审讯超时
            ALARM_RFID_Broken = 2031,						//断开报警
            ALARM_RFID_HeartBeat = 2032,						//心率信息
            ALARM_RFID_HeartBeatException = 2033,						//心率异常报警
            ALARM_RFID_VEHICLE_NOT_ARRIVE_TIMEOUT = 2035,					//车辆超时未达报警
            ALARM_RFID_NEAR_DISTANCE_DETECTION = 2036,                 //近距离接触定位报警
            ALARM_RFID_RIOTERDETECTION = 2037,                 //人员聚集定位报警
            ALARM_RFID_CO_CASE_CONTACTS = 2038,					//同案接触定位报警
            ALARM_RFID_REVERSE = 2100,						//逆向报警
            ALARM_RFID_InterrogationBegin = 2101,                     //开始审讯
            ALARM_RFID_InterrogationEnd = 2102,                     //结束审讯
            ALARM_RFID_END = 2150,						//射频报警结束	

            ALARM_DOOR_MAGNETISM = 2200,					// 门磁
            ALARM_PASSIVE_INFRARED = 2201,					// 被动红外
            ALARM_GAS = 2202,					// 气感
            ALARM_INITIATIVE_INFRARED = 2203,					// 主动红外
            ALARM_GLASS_CRASH = 2204,					// 玻璃破碎
            ALARM_EXIGENCY_SWITCH = 2205,					// 紧急开关
            ALARM_SHAKE = 2206,					// 震动
            ALARM_BOTH_JUDGE = 2207,					// 双鉴（红外+微波）
            ALARM_THREE_TECHNIC = 2208,					// 三技术
            ALARM_CALL_BUTTON = 2209,					// 呼叫按钮
            ALARM_SENSE_OTHER = 2210,					// 其他
            //模拟室内机报警类型
            ALARM_SENSE_OTHER_ANALOG = 2211,					// 模拟室内机报警类型“其他”
            AE_ALARM_TYPE_END = 2400,
            ALARM_ID_CARD_COMPARE_OK = 2401,					//人证对比成功结果上报
            ALARM_ID_CARD_COMPARE_FAILED = 2402,					//人证对比失败结果上报
            ALARM_IVSS_STRANGER_ALARM = 2403,					//IVSS陌生人报警事件（不同于陌生人脸报警）
            ALARM_VTO_QRCODE_CHECK = 2404,					//二维码上报事件
            ALARM_FACE_BLACK_LIST = 2405,					//人脸黑名单报警
            ALARM_IVSS_VIP_ALARM = 2406,					//招行项目-VIP客户报警
            ALARM_ID_CARD_COMPARE_OK_TWO = 2407,					//双人人证对比成功结果上报

            //防护舱报警类型 有人有锁，有人无锁，无人有锁，无人无锁，关锁超时
            ALARM_PRC_TYPE_BEGIN = 2500,
            ALARM_PRC_MAN_AND_LOCK = 2501,					// 有人有锁
            ALARM_PRC_MAN_AND_NOLOCK = 2502,					// 有人无锁
            ALARM_PRC_NOMAN_AND_LOCK = 2503,					// 无人有锁
            ALARM_PRC_NOMAN_AND_NOLOCK = 2504,					// 无人无锁
            ALARM_PRC_LOCK_TIMEOUT = 2505,					// 关锁超时
            ALARM_PRC_EMERGENCY_CALL = 2506,					// 紧急呼叫
            ALARM_PRC_TYPE_END = 2600,

            //begin震动光纤报警类型
            ALARM_TYPE_VIBRATIONFIBER_BEGIN = 2601,					// 震动光纤1
            ALARM_VIBRATIONFIBER_SNLALARM = 2602,                 // 开关量报警 
            ALARM_VIBRATIONFIBER_BOXALARM = 2603,                 // 开关盒报警 
            ALARM_VIBRATIONFIBER_INVALIDZONE = 2604,                 // 防区失效1106 
            ALARM_VIBRATIONFIBER_SIGNAL_OFF = 2605,                 // 光纤信号源停止 
            ALARM_VIBRATIONFIBER_FIBRE_BREAK = 2606,                 // 光纤断开
            ALARM_TALK_ALARM_IN = 2699,					// 对讲报警输入通道报警
            ALARM_TYPE_VIBRATIONFIBER_END = 2700,					// 震动光纤5
            //end
            //巡更报警
            ALARM_PATROL_BEGIN = 2701,
            ALARM_PATROL_EXCEPTION = 2702,				// 巡更异常报警 
            ALARM_PATROL_ROUTINE_REQUEST = 2703,				// 请求路线报警,巡更轨迹通知，GPS通知
            ALARM_PATROL_LOCATION_REQUEST = 2704,				// 请求定位报警
            ALARM_PATROL_PROMPTING = 2705,				// 巡更提醒
            ALARM_PATROL_ROUTE_RESULT_NTF = 2706,				// 线路巡更结果通知
            ALARM_PATROL_ROUTE_PROMPTING = 2707,				// 线路巡更提醒
            ALARM_PATROL_REMIND_START_TASK = 2708,				// 巡更任务开始前提醒
            ALARM_PATROL_REMIND_END_TASK = 2709,				// 巡更任务结束前提醒
            ALARM_PATROL_END = 2800,
            ALARM_ZKFINGER_BEGIN = 2801,
            ALARM_VALID_IDENTIFY = 2802,					// 中控指纹 有效验证
            ALARM_INVALID_IDENTIFY = 2803,					// 中控指纹 无效验证
            ALARM_ZKFINGER_END = 2900,

            // 其他第三方设备报警类型
            ALARM_OTHER_DEVICE_BEGIN = 2901,
            ALARM_GOODSWEIGHT = 2902,					// 货重信息报警
            ALARM_FACE_GOODS = 2910,					// 大江东人脸物品信息
            ALARM_FACE_PROOF = 2911,					// 富士康 认证比对
            ALARM_FACE_PROOF_FAILED = 2912,					// 富士康 认证比对失败
            ALARM_FLOOD_DOOR_OPEN = 2913,					// 防汛门开门事件
            ALARM_FINGER_PRINT = 2914,					//指纹采集成功
            ALARM_FINGER_PRINT_FAILED = 2915,					//指纹采集失败
            ALARM_OTHER_DEVICE_END = 3000,

            // 哨位箱报警类型
            ALARM_SENTRY_BOX_BEGIN = 3001,
            ALARM_BREAK_PRISON = 3002,					// 越狱报警
            ALARM_FORCE_BREAK_PRISON = 3003,					// 暴力越狱报警
            ALARM_ATTACK = 3004,					// 袭击报警
            ALARM_DISASTER = 3005,					// 灾害报警
            ALARM_BULLET_BOX = 3006,					// 子弹箱报警
            ALARM_OTHERS = 3007,					// 其他报警
            ALARM_HIJACKED = 3008,					// 劫持事件报警
            ALARM_SENTRY_BOX_END = 3100,
            // -F预留报警类型，自定义报警
            ALARM_TYPE_USERDEFINE_BEGIN = 3101,
            ALARM_TYPE_USERDEFINE_END = 3130,

            // 弘视智能设备报警类型begin
            ALARM_HSIVS_ALARM_BEGIN = 3131,
            ALARM_VIRTUAL_WALL = 3131,					// 虚拟墙
            ALARM_ASSETS_PROTECT = 3132,					// 财产保护
            ALARM_VIDEO_QUALITY_CHECK = 3133,					// 视质检测
            ALARM_REGION_PROTECT = 3134,					// 区域看防
            ALARM_ONDUTY_CHECK = 3135,					// 值岗检测
            ALARM_CARNUM_RECOGNIZE = 3136,					// 车牌识别
            ALARM_ROUGH_MOTION_CHECK = 3137,					// 剧烈运动检测
            ALARM_DOUBLE_PERSON_ONDUTY = 3138,					// 双人值岗
            ALARM_PERSON_CAR_CLASSIFY = 3139,					// 人车分类
            ALARM_PERSON_NUM_COUNT = 3140,					// 人数统计
            ALARM_TURNVEDIO_DIAGNOSIS = 3141,					// 轮视视频诊断
            ALARM_EARTHWORKCAR_RECOGNIZE = 3142,					// 土方车识别
            ALARM_NONMOTORIZEDOBJECT_CHECK = 3143,					// 非机动目标检测
            ALARM_EPOLICE = 3144,					// 电子警察
            ALARM_IN_CROSSREGIONDETECTION = 3145,					// 报警输入通道-警戒区事件
            ALARM_IN_FACEDETECT = 3146,					// 报警输入通道-人脸检测和识别
            ALARM_IN_PRISONERRISEDETECTION = 3147,					// 报警输入通道-起身检测
            ALARM_IN_CROSSLINEDETECTION = 3148,					// 报警输入通道-警戒线事件
            ALARM_IN_WANDERDETECTION = 3149,					// 报警输入通道-徘徊事件
            ALARM_IN_TRAFFIC_OVERYELLOWLINE = 3150,					// 报警输入通道-压黄线检测
            ALARM_IN_RETROGRADEDETECTION = 3151,					// 报警输入通道-逆行检测
            ALARM_IN_TRAFFIC_RUNREDLIGHT = 3152,					// 报警输入通道-闯红灯检测
            ALRAM_IN_PATROL_OVER_TIME = 3153,					// 报警输入通道-巡更检测
            ALARM_IN_TRAFFICGATE = 3154,					// 报警输入通道-卡口检测
            ALARM_HSIVS_ALARM_END = 3200,
            // 弘视智能设备报警类型end

            // 报警运营平台，扩展自定义报警类型
            ALARM_TYPE_USERDEFINEEX_BEGIN = 3201,
            ALARM_TYPE_USERDEFINEEX_END = 4200,

            ALARM_NODE_ACTIVE = 4201,					// 主从切换报警
            ALARM_ISCSI_STATUS = 4202,					// ISCSI存储状态变更报警
            ALARM_OUTDOOR_STATIC = 4203,

            ALARM_FALLING = 4204,					// 跌落事件报警 
            ALARM_ITC_OUTSIDE_CARNUM = 4205,					// 出入口外部车报警
            ALARM_POS_TRANING_MODE = 4206,					//POS机训练模式报警
            ALARM_REFUND_OVER_QUOTA = 4207,					//退货限额报警
            ALARM_SWING_CARD_FREQUENTLY = 4208,					//会员卡频繁出现报警
            ALARM_SIGNLE_COST_OVER_QUOTA = 4209,					//销售单笔超额报警

            //DSS-H可视对讲设备室内机新增传感器报警类型
            ALARM_SENSE_BEGIN = 4299,
            ALARM_SENSE_DOOR = 4300,                 //门磁
            ALARM_SENSE_PASSIVEINFRA = 4301,                 //被动红外
            ALARM_SENSE_GAS = 4302,                 //气感
            ALARM_SENSE_SMOKING = 4303,                 //烟感
            ALARM_SENSE_WATER = 4304,                 //水感
            ALARM_SENSE_ACTIVEFRA = 4305,                 //主动红外
            ALARM_SENSE_GLASS = 4306,                 //玻璃破碎
            ALARM_SENSE_EMERGENCYSWITCH = 4307,                 //紧急开关
            ALARM_SENSE_SHOCK = 4308,                 //震动
            ALARM_SENSE_DOUBLEMETHOD = 4309,                 //双鉴(红外+微波)
            ALARM_SENSE_THREEMETHOD = 4310,                 //三技术
            ALARM_SENSE_TEMP = 4311,                 //温度
            ALARM_SENSE_HUMIDITY = 4312,                 //湿度
            ALARM_SENSE_WIND = 4313,                 //风速
            ALARM_SENSE_CALLBUTTON = 4314,                 //呼叫按钮
            ALARM_SENSE_GASPRESSURE = 4315,                 //气体压力
            ALARM_SENSE_GASCONCENTRATION = 4316,                 //燃气浓度
            ALARM_SENSE_GASFLOW = 4317,                 //气体流量
            ALARM_SENSE_OIL = 4319,                 //油量检测，汽油、柴油等车辆用油检测
            ALARM_SENSE_MILEAGE = 4320,                 //里程数检测
            ALARM_SENSE_URGENCYBUTTON = 4321,                 //紧急按钮
            ALARM_SENSE_STEAL = 4322,                 //盗窃
            ALARM_SENSE_PERIMETER = 4323,                 //周界
            ALARM_SENSE_PREVENTREMOVE = 4324,                 //防拆
            ALARM_SENSE_DOORBELL = 4325,                 //门铃
            ALARM_SENSE_LOCK_LOCKKEY = 4326,                 //门锁钥匙报警
            ALARM_SENSE_LOCK_LOWPOWER = 4327,                 //门锁低电压报警
            ALARM_SENSE_LOCK_PREVENTREMOVE = 4328,                 //门锁防拆
            ALARM_SENSE_LOCK_FORCE = 4329,                 //门锁胁迫报警
            ALARM_SENSE_LOCK_OFFLINE = 4330,					//门锁离线报警
            ALARM_SENSE_FIRE = 4331,					//火警
            ALARM_SENSE_SIGNIN = 4332,					//室内机签到报警
            ALARM_SENSE_INFRARED = 4333,					//红外报警
            ALARM_SENSE_GATE_PREVENTREMOVE = 4334,					//网关防拆
            ALARM_SENSE_DELAY_ARMING = 4335,					//延时布放
            ALARM_SENSE_MODE_ARMING = 4336,					//布防设置
            ALARM_SENSE_MODE_DISARMING = 4337,					//撤防设置
            ALARM_SENSE_END = 4399,

            ALARM_STORAGE_BEGIN = 4400,
            ALARM_IO_QUEUE_FULL = 4401,					// 磁盘读写高负荷
            ALARM_DISK_DESTROY = 4402,					// 磁盘异常
            ALARM_IPSAN_OFF_LINE = 4403,					// IPSan掉线
            ALARM_NO_DISK_STORAGE = 4404,					// 没有磁盘			
            ALARM_GET_STREAM_ERROR = 4405,					// 取码流错误
            ALARM_STORAGE_END = 4499,

            //DSSH出入口卡口黑名单报警类型新增
            ALARM_TRAFFIC_SUSPICIOUSCAR = 4501,

            //大华出入口控制机报警类型
            ALARM_SLUICE_BEGIN = 4502,
            ALARM_SLUICE_IC_CARD_STATUS_LOWCARD = 4503,					//卡箱少卡报警
            ALARM_SLUICE_IC_CARD_STATUS_NOCARD = 4504,					//卡箱无卡报警
            ALARM_SLUICE_IC_CARD_STATUS_FULLCARDS = 4505,					//卡箱卡满报警
            ALARM_SLUICE_CAR_DETECTOR_STATE_OFFLINE = 4506,					//车检器掉线报警
            ALARM_SLUICE_CAR_DETECTOR_STATE_LOOPOFFLINE = 4507,					//地感线圈掉线报警
            ALARM_SLUICE_LED_DEV_STATE_OFFLINE = 4508,					//LED掉线报警
            ALARM_SLUICE_SWIPING_CARD_DEV_STATE_OFFLINE = 4509,					//面板刷卡板掉线报警
            ALARM_SLUICE_DELIVE_CARD_DEV_OFFLINE = 4510,					//发卡刷卡板掉线报警
            ALARM_SLUICE_SPEAK_DEV_STATUS = 4511,					//对讲事件报警
            ALARM_SLUICE_END = 4550,

            //DSSH自助缴费机报警类型
            ALARM_SELFPAY_BEGIN = 4551,
            ALARM_SELFPAY_NOPAPER = 4552,//缺纸
            ALARM_SELFPAY_NOCASH50 = 4553,
            ALARM_SELFPAY_NOCASH20 = 4554,
            ALARM_SELFPAY_NOCASH10 = 4555,
            ALARM_SELFPAY_NOCASH1 = 4556,
            ALARM_SELFPAY_NOCOIN = 4557,
            ALARM_SELFPAY_LOCKMONEY = 4558,//卡币
            ALARM_SELFPAY_DISMANTLE = 4559,//防拆
            ALARM_SELFPAY_UNPACK = 4560,//开箱
            ALARM_SELFPAY_UNKONWN = 4561,//纸币不识别
            ALARM_SELFPAY_CASHBOXOTHER = 4562,					//钱箱识别器其他错误
            ALARM_SELFPAY_PRINTERERR = 4563,					//热敏打印机械故障
            ALARM_SELFPAY_RECOGNITIONSELFCHECKERR = 4564,					//硬币识别器自检错误
            ALARM_SELFPAY_RECOGNITIONPOLLONLINE = 4565,					//硬币识别器轮询在线
            ALARM_SELFPAY_CHANGEONLINE = 4566,					//硬币找零器是否在线
            ALARM_SELFPAY_END = 4580,
            ALARM_ITC_BLACKLIST_CARNUM = 4581,					//PES停车场模块黑名单车辆
            ALARM_ITC_RESERVE_OCCUPY = 4582,					//停车场手机预定车位被占用 
            //门禁设备扩展报警
            ALARM_DOOREX_BEGIN = 4600,
            ALARM_VALID_CARD_OR_FINGERPRINT_OR_FACE = 4601,				//卡或指纹或人脸合法开门
            ALARM_INVALID_CARD_OR_FINGERPRINT_OR_FACE = 4602,				//卡或指纹或人脸非法开门
            ALARM_VALID_CARD_AND_FINGERPRINT_AND_FACE_AND_PWD = 4603,		//卡+指纹+人脸+密码组合合法开门
            ALARM_INVALID_CARD_AND_FINGERPRINT_AND_FACE_AND_PWD = 4604,		//卡+指纹+人脸+密码组合非法开门
            ALARM_VALID_CARD_OR_FINGERPRINT_OR_FACE_OR_PWD = 4605,				//卡或指纹或人脸或密码合法开门
            ALARM_INVALID_CARD_OR_FINGERPRINT_OR_FACE_OR_PWD = 4606,			//卡或指纹或人脸或密码非法开门
            ALARM_VALID_FACEIPCARDANDIDCARD_OR_CARD_OR_FACE = 4607,			//(身份证+人证比对)或 刷卡 或 人脸合法开门
            ALARM_INVALID_FACEIPCARDANDIDCARD_OR_CARD_OR_FACE = 4608,			//(身份证+人证比对)或 刷卡 或 人脸非法开门
            ALARM_VALID_FACEIDCARD_OR_CARD_OR_FACE = 4609,				//人证比对 或 刷卡(二维码) 或 人脸合法开门
            ALARM_INVALID_FACEIDCARD_OR_CARD_OR_FACE = 4610,				//人证比对 或 刷卡(二维码) 或 人脸非法开门
            ALARM_VALID_REMOTE_QRCODE = 4611,				//远程二维码合法开门
            ALARM_INVALID_REMOTE_QRCODE = 4612,				//远程二维码非法开门
            ALARM_VALID_REMOTE_FACE = 4613,				//远程人脸合法开门
            ALARM_INVALID_REMOTE_FACE = 4614,				//远程人脸非法开门
            ALARM_VALID_CITIZEN_FINGERPRINT = 4615,				//人证比对(指纹)合法开门
            ALARM_INVALID_CITIZEN_FINGERPRINT = 4616,				//人证比对(指纹)非法开门
            ALARM_RFID_PET_ABNORMAL_THROUGH = 4617,				//宠物异常通行
            ALARM_RFID_ELECTROMBILE_UNIT_ENTER = 4618,				//电动车进出单元报警
            ALARM_RFID_ELECTROMBILE_AREA_FORBID = 4619,				//电动车区域禁停报警
            ALARM_RFID_ABNORMAL_IN_AND_OUT = 4620,				//人员异常出入预警
            ALARM_VALID_PWD_FIRST = 4621,				//先密码后刷卡合法开门
            ALARM_INVALID_PWD_FIRST = 4622,				//先密码后刷卡非法开门
            ALARM_DOOROPEN_MALICE = 4623,				//恶意开门事件
            ALARM_RFID_NOT_IN_AND_OUT = 4624,				//人员未出入预警
            ALARM_ACTIVE_LOW_POWER = 4625,				//有源RFID低电量报警
            ALARM_VALID_HELMET_OPEN_DOOR = 4626,				//人脸安全帽合法开门
            ALARM_INVALID_HELMET_OPEN_DOOR = 4627,				//人脸安全帽非法开门
            ALARM_DISCONN_TIMEOUT = 4628,				//离线超时报警
            ALARM_TYPE_FREEZE = 4629,				//冻结卡刷卡事件
            ALARM_DOOREX_END = 4699,

            //客户端IP对讲报警
            ALARM_IP_DEV_BEGIN = 4700,
            ALARM_IP_DEV_CALLIN = 4701,				//分机呼叫
            ALARM_IP_DEV_CALLOUT = 4702,				//拨打
            ALARM_IP_DEV_END = 4800,

            // -F,ATM防护舱报警类型
            ALARM_DEFENCE_DISMANTLE_DESTORY = 4801,					// ATM防护舱防拆防破坏报警
            ALARM_URGENT_BUTTON_TRIGGER = 4802,					// ATM防护舱紧急按钮触发报警
            ALARM_CABIN_TWO_PERSONS = 4803,					// ATM防护舱舱内两人报警
            ALARM_CABIN_OUTSIDE = 4804,					// ATM防护舱外部报警
            ALARM_GATELOCK_ABNORMAL = 4805,					// ATM防护舱门锁异常报警
            ALARM_CABIN_INSIDE_SOMEONE_FALLDOWN = 4806,
            ALARM_INFRARED_INSPECTED = 4807,					// 红外对射
            ALARM_CLOSE_LOCK_IN_BUTTON = 4808,					// 闭锁进门按钮
            ALARM_CLOSE_LOCK_OUT_BUTTON = 4809,					// 闭锁出门按钮
            ALARM_IN_DOOR_BUTTON = 4810,					// 进门按钮
            ALARM_OUT_DOOR_BUTTON = 4811,					// 出门按钮
            ALARM_OPEN_LOCK_BREAKDOWN = 4812,					// 开锁故障
            ALARM_CLOSE_LOCK_BREAKDOWN = 4813,					// 闭锁故障	
            ALARM_CABIN_FACEDETECT_UNNORMAL = 4814,                 // 人脸检测事件中－异常人脸
            ALARM_CABIN_FIGHTDETECTION = 4815,                 // 斗殴事件
            ALARM_CABIN_VIDEO_SHELTER = 4816,                 // 视频遮挡
            ALARM_CABIN_DOOR_ATM_SHAKE = 4817,                 // 取款机震动报警（防撬门外接的探测器）
            ALARM_CABIN_EXCEPTION_STAY = 4818,                 //异常滞留
            ALARM_ATM_MSG_TYPE_USER_TIMESLOT_USEDUP = 4819,					// ATM未按出门按钮人员感应失效
            ALARM_ATM_MSG_TYPE_CABIN_IN = 4820,					// 进ATM防护舱
            ALARM_ATM_MSG_TYPE_CABIN_OUT = 4821,					// 出ATM防护舱
            ALARM_ATM_MSG_TYPE_CABIN_LOCK = 4822,					// 闭锁ATM防护舱
            ALARM_ATM_MSG_TYPE_CABIN_UNLOCK = 4823,					// 解锁ATM防护舱
            ALARM_ATM_MSG_TYPE_DOOR_CLOSE = 4824,					// ATM防护舱门关
            ALARM_ATM_MSG_TYPE_DOOR_OPEN = 4825,					// ATM防护舱门开

            //-F报警
            ALARM_OUT_STAT_OPENEX = 4851,                 // 报警输出打开状态
            ALARM_OUT_STAT_CLOSEDEX = 4852,                 // 报警输出关闭状态重新定义
            ALARM_ROBOT_GENERAL = 4853,					// 机器人通用报警
            ALARM_ENABLE_ARM_PROMPT = 4854,					// 布防提示报警
            ALARM_DISABLE_ARM_PROMPT = 4855,					// 撤防提示报警
            AlARM_DEV_CFG_ABNORMAL = 4856,					// 设备参数异常报警（DMS自造）
            ALARM_DEV_TIME_ABNORMAL = 4857,					// 设备时间异常报警（DMS自造）

            //手机APP报警类型
            ALARM_MOBILEAPP_BEGIN = 4900,
            ALARM_MOBILEAPP_GPS = 4901,		//手机APP上传GPS
            ALARM_MOBILEAPP_ONE_CLICK = 4902,		//手机APP一键报警
            ALARM_MOBILEAPP_MANUAL_ADD = 4903,		//手机APP手动添加报警
            ALARM_MOBILEAPP_END = 5000,

            //APS人数统计报警
            ALAMR_PEOPLE_COUNTING_BEGIN = 5001,
            ALARM_PEOPLE_UPPER_LIMIT = 5002,		//人数上限
            ALARM_PEOPLE_LOWER_LIMIT = 5003,		//人数下限
            ALARM_INFLUX_UPPER_LIMIT = 5004,		//人流量超标（进）
            ALARM_OUTFLUX_UPPER_LIMIT = 5005,		//人流量超标（出）
            ALARM_DENSITY_UPPER_LIMIT = 5006,		//密度报警
            ALARM_SCENE_EXCEPTION = 5007,		//场景异常报警
            ALARM_EXCEPTION_STAY = 5008,		//异常滞留
            ALARM_GUARD_LINE = 5009,		//警卫线报警
            ALARM_STAY_TIMEOUT = 5010,		//超时滞留报警
            ALAMR_PEOPLE_COUNTING_END = 5100,

            ALARM_THIRD_ACCESS = 5101,      //第三方接入设备报警
            ALARM_PC_REPORT = 5102,		 //智能设备上报人数统计报警
            ALARM_THREE_IN_ONE = 5103,		 //三台合一报警
            ALARM_HUMAM_NUMBER_STATISTIC = 5104,		 //人流量统计相机客流量超过阀值报警事件
            ALARM_PERSON_COUNT_REPORT = 5105,      //人流量统计（以报警方式上报人流量统计信息）
            ALARM_MAN_NUM_DETECTION = 5106,      //立体视觉区域内人数统计报警
            ALARM_NUMBERSTAT_GROUP_SUMMARY = 5107,      //按分组查询的人流量统计	
            ALARM_DISTANCE_DETECTION = 5108,		//立体行为分析间距异常/人员靠近检测报警
            ALARM_TUMBLE_DETECTION = 5109,		//立体行为分析跌倒检测报警

            // 热成像报警
            ALARM_RADIOMETRY_HEATIMG_TEMPER = 5120,          //热成像测温点温度异常报警
            ALARM_RADIOMETRY_FIRE_WARNING = 5121,          //热成像着火点报警
            ALARM_RADIOMETRY_FIREWARNING_INFO = 5122,          //热成像火情报警信息上报
            ALARM_RADIOMETRY_HOTSPOT_WARNING = 5123,          //热成像热点异常报警（高于温度阀值报警）
            ALARM_RADIOMETRY_COLDSPOT_WARNING = 5124,          //热成像冷点异常报警（低于温度阀值报警）
            ALARM_RADIOMETRY_BETWEENRULE_TEMP_DIFF = 5125,          //热成像规则间温差异常报警
            ALARM_RADIOMETRY_SMOKE_DETECTION = 5126,          //热成像烟雾报警
            ALARM_RADIOMETRY_FACE_OVERHEATING = 5127,          //热成像人体发烧预警

            // 长春地铁报警
            ALARM_SUB_WAY_DOOR_STATE = 5170,		// 地铁车厢门报警
            ALARM_SUB_WAY_PECE_SWITCH = 5171,		// 地铁PECE柜门报警
            ALARM_SUB_WAY_FIRE_ALARM = 5172,		// 地铁火警报警
            ALARM_SUB_WAY_EMER_HANDLE = 5173,		// 地铁乘客紧急手柄动作报警
            ALARM_SUB_WAY_CAB_COVER = 5174,		// 地铁司机室盖板报警
            ALARM_SUB_WAY_DERA_OBST = 5175,		// 地铁检测到障碍物或脱轨报警
            ALARM_SUB_WAY_PECU_CALL = 5176,		// 地铁客室报警器报警


            //客户端机顶盒设备定制报警
            ALARM_STB_BEGIN = 5200,
            ALARM_STB_FIRE = 5201,		//火警
            ALARM_STB_CRIME = 5202,		//匪警
            ALARM_STB_EMERGENCY = 5203,		//急救中心
            ALARM_STB_OTHER = 5204,		//其他报警
            ALARM_STB_END = 5250,

            //-C/-P新增报警预留
            ALARM_DSSC_BEGIN = 5300,
            ALARM_PATIENTDETECTION_TYPE_CROSS_REGION = ALARM_DSSC_BEGIN + 1,	// 警戒区域报警，可能是病人离开或者有其他靠近病人
            ALARM_PATIENTDETECTION_TYPE_LIGHT_OFF = ALARM_DSSC_BEGIN + 2,	// 病房电灯被熄灭
            ALARM_PATIENTDETECTION_TYPE_STOP_DETECTION = ALARM_DSSC_BEGIN + 3,	// 撤防，不再监控病人
            ALARM_PATIENTDETECTION_TYPE_START_DETECTION = ALARM_DSSC_BEGIN + 4,	// 开始布防
            ALARM_PATIENTDETECTION_TYPE_ESCAPE = ALARM_DSSC_BEGIN + 5,	// 病人在押解过程中逃跑
            ALARM_PATIENTDETECTION_TYPE_SMOKE = ALARM_DSSC_BEGIN + 6,	// 烟感报警
            ALARM_DSSC_END = 5400,

            ALARM_U700_BEGIN = 5401,
            ALARM_VTA_INSPECTION = ALARM_U700_BEGIN + 1, // VTA报警柱巡检报警
            ALARM_VTA_OVERSPEED = ALARM_U700_BEGIN + 2, // VTA报警柱超速报警
            ALARM_VTA_INSPECTION_SWING_CARD = ALARM_U700_BEGIN + 3, //VTA巡检刷卡
            ALARM_VTA_PATROL_SWING_CARD = ALARM_U700_BEGIN + 4, //VTA巡更刷卡
            ALARM_U700_END = 5500,

            ALARM_REMOTE_CAMERA_STATE = 5501,				//卡口设备相机状态上报报警
            ALARM_SHANGHAI_JIHENG = 5502,				//上海迹恒上报报警
            ALARM_PATROL_REMIND = 5503,				//巡更提醒报警
            ALARM_VTO_ACCESSIDENTIFY = 5504,				//门口机人脸认证
            ALARM_CAR_SURVEY = 5505,				//卡口布控报警
            ALARM_CHANNEL_TALK = 5506,				//通道对讲报警
            ALARM_HEARTRATE_DETECT = 5507,				//心率侦测

            //人行道闸报警定义 5640- 5680
            ALARM_ROADGATE_BEGIN = 5640,

            ALARM_ROADGATE_VALID_PASSWORD_OPENDOOR = 5642,
            ALARM_ROADGATE_INVALID_PASSWORD_OPENDOOR = 5643,
            ALARM_ROADGATE_REMOTE_OPENDOOR = 5648,
            ALARM_ROADGATE_VALID_CARD_OPENDOOR = 5651,
            ALARM_ROADGATE_INVALID_CARD_OPENDOOR = 5652,
            ALARM_ROADGATE_NORMAL_CLOSED = 5656,
            ALARM_ROADGATE_OPEN = 5657,
            ALARM_ROADGATE_OPEN_TIME_OUT_BEG = 5660,
            ALARM_ROADGATE_OPEN_TIME_OUT_END = 5670,

            ALARM_ROADGATE_END = 5680,


            //-P 行业线 对接海康设备增加报警
            ALARM_AUDIO_ABNORMALDETECTION = 5700,
            ALARM_CLIMB_UP_DETECTION = 5701,
            ALARM_CROSSRE_DETECTION = 5702,
            ALARM_FIGHT_DETECTION = 5703,
            //倍特卫视分析设备
            ALARM_RAISE_UP_DETECTION = 5705,
            ALARM_WC_TIMEOUT_DETECTION = 5706,
            ALARM_DUTY_DETECTION = 5707,
            ALARM_OUTSIDE_STRANDED_DETECTION = 5708,
            // 科大讯飞语音报警
            ALARM_KVOICE_ALARM = 5709,

            //老动环报警扩展定义报警区间段
            ALARM_SCS_EXT_BEGIN = 6000,
            ALARM_SCS_EXT_NOISE_INTENSITY = 6001,						//噪声告警
            ALARM_SCS_EXT_END = 6999,

            //雷达信息报警区间
            AlARM_RADAR_BEGIN = 7000,
            AlARM_RADAR_TARGETINFO = 7001,		// 雷达上传目标信息
            AlARM_RADAR_ALARM = 7002,		// 雷达报警上传
            AlARM_RADAR_END = 7100,

            ALARM_TYPE_MAINTENANCE_OUT = 7200, 		//钥匙回转
            ALARM_TYPE_MAINTENANCE_IN = 7201,		//钥匙打开
            ALARM_TYPE_BATTERY_FAILURE = 7202,		//电池失效
            ALARM_TYPE_POWER_FAILURE = 7203,		//POWER_FAILURE
            ALARM_TYPE_SPEAKER_FAILURE = 7204,		//SPEAKER_FAILURE


            ALARM_TYPE_FAN_SPEED = 7300,		//风扇异常


            //新动环报警定义报警区间段
            ALARM_NEW_SCS_BEGIN = 8000,
            ALARM_NEW_SCS_PREVENTREMOVE = 9997,				//动环防拆
            ALARM_NEW_SCS_PREVENT_SHORTCIRCUIT = 9998,				//动环防短
            ALARM_NEW_SCS_END = 9999,
            //平台业务报警区间段
            ALARM_BUSINESS_BEGIN = 10001,
            ALARM_BUSINESS_POLICE_PATROL = 10002,			//民警巡视业务报警		
            ALARM_BUSINESS_WAITING_ROOM_UNATTENDED = 10003,			//候问室无人看管
            ALARM_BUSINESS_WASHROOM_UNATTENDED = 10004,			//卫生间无人跟随
            ALARM_BUSINESS_MAN_NUM_DETECTION = 10005,			//审讯室人数报警
            ALARM_BUSINESS_NOCAP_DETECTION = 10006,			//未带安全帽报警
            ALARM_BUSINESS_END = 10500,

            ALARM_VEHICLE_SURVEY_EW = 10501,			//车辆布控预警报警
            ALARM_FACE_BLOCK = 10502,			//人脸卡口报警
            ALARM_VEHICLE_SCORE_EW = 10503,			//车辆积分预警报警
            //CMS平台报警
            ALARM_DISTRIBUTE_SWITCHOVER = 10600,			//N+M备份切换报警

            ALARM_HBSZZ_APP_BUTTON = 10601,			//河北省综治项目APP一键报警
            ALARM_WIDE_VIEW_REGION_ALARM = 10602,			// 全景区域报警，浙江二监定制
            ALARM_HIGH_DECIBEL = 10603,			// 声音高分贝检测报警
            ALARM_SHAKE_DETECTION = 10604,			// 摇晃检测报警
            ALARM_BATTERY_LOW_POWER = 10605,			// 电池电量低报警
            ALARM_GZGDXL_TVAPP = 10606,			//贵州广电雪亮TV APP报警

            ALARM_LOWER_DOMAIN_DISCONNECT = 10700,			//下级平台断开报警
            //PTS新增报警
            ALARM_PTS_BEGIN = 11000,
            //布控报警 begin
            ALARM_FORTIFY_OVERSPEED = 11001,	// 布控超速车辆
            ALARM_FORTIFY_STOLEN = 11002,	// 布控盗抢车辆
            ALARM_FORTIFY_ACCIDENT = 11003,	// 布控肇事车辆
            ALARM_FORTIFY_SUSPICE = 11004,	// 布控嫌疑车辆
            ALARM_FORTIFY_HEADOFF = 11005,	// 布控拦截车辆
            ALARM_FORTIFY_CHECKED = 11006,	// 布控检查盘查
            ALARM_FORTIFY_FOLLOWED = 11007,	// 布控观察跟踪
            ALARM_FORTIFY_DANGER = 11008,	// 布控高危车辆
            ALARM_FORTIFY_STRANDING = 11009,	// 布控滞留车辆
            ALARM_FORTIFY_SPECIALEXCEPTION = 11010,	// 特殊异常车辆
            ALARM_FORTIFY_EXHAUST = 11011,	// 布控黄标车
            ALARM_FORTIFY_WHITELIST = 11012,	// 布控白名单
            ALARM_FORTIFY_BLACKLIST = 11013,	// 布控黑名单
            ALARM_FORTIFY_LASTNUMBER = 11014,	// 布控尾号限行
            ALARM_FORTIFY_GRIDLINE = 11015,	// 网格布控（车辆经过网内任意两个卡点）
            ALARM_FORTIFY_TIMEOUT = 11016,	// 布控滞留超时车辆
            ALARM_FORTIFY_ILLEGALTIMEPASS = 11017,	// 布控在白名单内，非法时间段通过车辆(暂定上海浦东垃圾场定制)
            ALARM_FORTIFY_NOTINWHITELIST = 11018,	// 布控未在白名单内车辆(暂定上海浦东垃圾场定制)
            ALARM_FORTIFY_RECOGNISEFAIL = 11019,	// 布控车牌无法识别车辆(暂定上海浦东垃圾场定制)
            //布控报警 end

            ALARM_NO_DRIVERROAD = 11101,	//非机动车道
            ALARM_OFFEND_INTERDICTORYSIGN = 11102,	//机动车违反禁令标志指示	
            ALARM_COVERING_PLATE = 11103,	//遮挡号牌
            ALARM_ROUND_ITS = 11104,	//绕行卡口
            ALARM_RESTRICT_DRIVING = 11105,	//限行
            ALARM_PEDESTRAIN_PRIORITY = 11106,	//斑马线行人优先
            ALARM_MNVR_PEC = 11107,	//车辆黑名单事件
            ALARM_COMPARE_PLATE = 11108,	//车牌前后对比	
            ALARM_TRAFFIC_CARWEIGHT = 11109,	//超重
            ALARM_TRANSFINITE_PECCANCY = 11110,	//超限违章
            ALARM_CHASSIS_CHECK = 11111,	//底盘检查			
            ALARM_PREILLEGALLY_PARKED = 11112,	//预违停
            ALARM_CAR_DETECTOR_FAULT = 11113,	//线圈/车检器故障报警
            ALARM_REMOTE_HOST = 11114,	//远程主机报警
            ALARM_TRAFFICLIGHTS_FAULT = 11115,	//灯绿灯
            ALARM_TRAFFIC_INTERRUPT = 11116,	//交通中断
            ALARM_DATABASE_FAULT = 11117,	//数据库错误
            ALARM_PTS_END = 11500,

            // 报警事件类型 DH_EVENT_VIDEOABNORMALDETECTION (视频异常事件)对应的数据描述信息
            ALARM_VIDEOABNORMALDETECTION_VIDEO_LOST = 11510,//视频丢失
            ALARM_VIDEOABNORMALDETECTION_VIDEO_SHELTER = 11511,//视频遮挡
            ALARM_VIDEOABNORMALDETECTION_PICTURE_FREEZE = 11512,//画面冻结
            ALARM_VIDEOABNORMALDETECTION_LIGHT = 11513,//过亮
            ALARM_VIDEOABNORMALDETECTION_DARK = 11514,//过暗
            ALARM_VIDEOABNORMALDETECTION_SCENE_CHANGE = 11515,//场景变化
            ALARM_VIDEOABNORMALDETECTION_STRIPE = 11516,//条纹检测
            ALARM_VIDEOABNORMALDETECTION_NOISE = 11517,//噪声检测
            ALARM_VIDEOABNORMALDETECTION_COLOUR_CAST = 11518,//偏色检测
            ALARM_VIDEOABNORMALDETECTION_VIDEO_BLUR = 11519,//视频模糊
            ALARM_VIDEOABNORMALDETECTION_CONTRAST_ABNORMAL = 11520,//对比度异常检测
            ALARM_VIDEOABNORMALDETECTION_VIDEO_MOVE = 11521,//视频运动
            ALARM_VIDEOABNORMALDETECTION_VIDEO_TWINKLE = 11522,//视频闪烁
            ALARM_VIDEOABNORMALDETECTION_VIDEO_COLOUR = 11523,//视频颜色
            ALARM_VIDEOABNORMALDETECTION_FOCAL = 11524,//虚焦检测
            ALARM_VIDEOABNORMALDETECTION_OVEREXPOSURE = 11525,//过曝检测
            ALARM_VIDEOABNORMALDETECTION_END = 11530,

            // 微云报警
            ALARM_MCS_GENERAL_CAPACITY_LOW = 11600,// 微云常规容量事件
            ALARM_MCS_DATA_NODE_OFFLINE = 11601,// 微云存储节点下线事件
            ALARM_MCS_DISK_OFFLINE = 11602,// 微云磁盘下线事件
            ALARM_MCS_DISK_SLOW = 11603,// 微云磁盘变慢事件
            ALARM_MCS_DISK_BROKEN = 11604,// 微云磁盘损坏事件
            ALARM_MCS_DISK_UNKNOW_ERROR = 11605,// 微云磁盘未知错误事件
            ALARM_MCS_METADATA_SERVER_ABNORMAL = 11606,// 微云元数据服务器异常事件
            ALARM_MCS_CATALOG_SERVER_ABNORMAL = 11607,// 微云目录服务器异常事件
            ALARM_MCS_GENERAL_CAPACITY_RESUME = 11608,// 微云常规容量恢复事件
            ALARM_MCS_DATA_NODE_ONLINE = 11609,// 微云存储节点上线事件
            ALARM_MCS_DISK_ONLINE = 11610,// 微云磁盘上线事件
            ALARM_MCS_METADATA_SLAVE_ONLINE = 11611,// 微云元数据备机上线事件
            ALARM_MCS_CATALOG_SERVER_ONLINE = 11612,// 微云目录服务器上线事件

            ALARM_VITAL_SIGNS_ABNORMAL = 11700,// 生命体征异常报警

            //新增围栏报警 占用范围：12000-12200
            ALARM_DRIVERIN_FLYAREA = 12000,                 //驶入飞行区
            ALARM_DRIVEROUT_FLYAREA,                                     //驶出飞行区
            ALARM_DRIVERIN_MANUALBANFLYAREA,                             //驶入禁飞区（手动配置）
            ALARM_DRIVEROUT_MANUALBANFLYAREA,                            //驶出禁飞区（手动配置）
            ALARM_DRIVERIN_FIXEDBANFLYAREA,                              //驶入禁飞区（不可配置）
            ALARM_DRIVEROUT_FIXEDBANFLYAREA,                             //驶出禁飞区（不可配置）
            ALARM_DRIVERIN_FiXEDLIMITFLY,                                //驶入限制飞行（不可配置）
            ALARM_DRIVEROUT_FiXEDLIMITFLY,                               //驶出限制飞行（不可配置）

            ALARM_ILLEGALIN_FLYAREA,					                 // 非法进入飞行区报警
            ALARM_ILLEGALOUT_FLYAREA,					                 // 非法驶出飞行区报警
            ALARM_ILLEGALIN_MANUALBANFLYAREA,					         // 非法进入禁飞区（手动配置）
            ALARM_ILLEGALOUT_MANUALBANFLYAREA,					         // 非法驶出禁飞区（手动配置）
            ALARM_ILLEGALIN_FIXEDBANFLYAREA,					         // 非法进入禁飞区（不可配置）
            ALARM_ILLEGALOUT_FIXEDBANFLYAREA,					         // 非法驶出禁飞区（不可配置）
            ALARM_ILLEGALIN_FiXEDLIMITFLY, 					             // 非法进入限制飞行
            ALARM_ILLEGALOUT_FiXEDLIMITFLY = 12015, 			     // 非法出驶出限制飞行

            //新增消防主机报警 占用范围：12300-12400
            ALARM_FIREENGINE_BEGIN = 12300,
            ALARM_FIREENGINE_FIRE,                                     	//火警报警
            ALARM_FIREENGINE_EQUIPMENT_FAILURE,                        	//设备故障报警
            ALARM_FIREENGINE_HOST_FAILURE,                         		//主电故障
            ALARM_FIREENGINE_BACKUP_FAILURE,                          	//备电故障
            ALARM_FIREENGINE_HOST_UNDERVOLTAGE,                         //主电欠压
            ALARM_FIREENGINE_BACKUP_UNDERVOLTAGE,                       //备电欠压
            ALARM_FIREENGINE_BUS_FAILURE,                            	//总线故障
            ALARM_FIREENGINE_HOST_OFFLINE,                          	//主机离线
            ALARM_FIREENGINE_MANUAL,                          			//手动报警
            ALARM_FIREENGINE_TEMPERATUAL,                            	//温度报警
            ALARM_FIREENGINE_LOW_WATERPRESSURE,                         //水压过低
            ALARM_FIREENGINE_HIGH_WATERPRESSURE,                        //水压过高
            ALARM_FIREENGINE_DETECTOR_FAULT,                            //探测器故障

            ALARM_FIREENGINE_END = 12400,

            //新增消控报警 占用范围：12401-12500
            ALARM_FIRECONTROL_BEGIN = 12401,
            ALARM_FIRECONTROL_NOBODY = 12402,             //无人报警
            ALARM_FIRECONTROL_UPPER_LIMIT = 12403,             //超过上限报警
            ALARM_FIRECONTROL_LOWER_LIMIT = 12404,             //少于下限报警
            ALARM_FIRECONTROL_NOT_MATCH = 12405,             //计划不符报警
            ALARM_FIRECONTROL_FIRE = 12406,             //火警
            ALARM_FIRECONTROL_HIGH_CURRENT = 12407,             //电流过高
            ALARM_FIRECONTROL_LOW_CURRENT = 12408,             //电流过低
            ALARM_FIRECONTROL_VALVE_OPEN = 12409,             //阀门被打开
            ALARM_FIRECONTROL_FALL = 12410,             //倾倒
            ALARM_FIRECONTROL_HIGH_TEMPERATURE = 12411,             //温度过高
            ALARM_FIRECONTROL_LOW_TEMPERATURE = 12412,             //温度过低
            ALARM_FIRECONTROL_HIGH_VOLTAGE = 12413,             //电压过高
            ALARM_FIRECONTROL_LOW_VOLTAGE = 12414,             //电压过低
            ALARM_FIRECONTROL_HIGH_WATERPRESSURE = 12415,             //水压过高
            ALARM_FIRECONTROL_LOW_WATERPRESSURE = 12416,             //水压过低	
            ALARM_FIRECONTROL_NORMAL = 12417,             //正常
            ALARM_FIRECONTROL_HIGH_LIQUID = 12418,             //液位过高
            ALARM_FIRECONTROL_LOW_LIQUID = 12419,             //液位过低
            ALARM_FIRECONTROL_GAS = 12420,             //燃气
            ALARM_FIRECONTROL_START = 12421,             //启动
            ALARM_FIRECONTROL_FEEDBACK = 12422,             //反馈
            ALARM_FIRECONTROL_MANUAL = 12423,             //手动
            ALARM_FIRECONTROL_STEAL = 12424,             //智能充电桩防盗报警
            ALARM_FIRECONTROL_SMOKING = 12425,             //智能充电桩烟雾报警
            ALARM_FIRECONTROL_CHARGINGWIRE_PULLOUT = 12426,             //智能充电桩充电线被拔报警
            ALARM_FIRECONTROL_DEV_FAULT = 12427,             //智能充电桩桩点故障
            ALARM_FIRECONTROL_CHRGINGPILE_FAULT = 12428,             //智能充电桩充电桩故障		
            ALARM_FIRECONTROL_POWER_OVERLOAED = 12429,             //智能充电桩功率过载
            ALARM_FIRECONTROL_TEMPERATURE_UNNORMAL = 12430,             //智能充电桩温度报警
            ALARM_FIRECONTROL_OFFLINE_CARD = 12431,             //智能充电桩离线卡异常
            ALARM_FIRECONTROL_REFUND = 12432,             //智能充电桩充电订单退款	
            ALARM_FIRECONTROL_CHARGER_FAULT = 12433,             //智能充电桩充电器故障
            ALARM_FIRECONTROL_BATTERY_FAULT = 12434,             //智能充电桩电池故障
            ALARM_FIRECONTROL_DISMANTLE = 12435,             //设备被拆除
            ALARM_FIRECONTROL_HAPPEN = 12436,             //告警	

            ALARM_FIRECONTROL_END = 12500,

            //新增java层通过MQ发给ADS报警 占用范围：12700-12999
            ALARM_WEBALARM_BEGIN = 12700,
            ALARM_WEBALARM_PERSON_CHANGED = 12701,			//人员信息变动报警
            ALARM_WEBALARM_INVALID_CARD = 12702,			//刷卡无效报警
            ALARM_WEBALARM_DOOR_STAYOPEN = 12703,			//门保持常开报警
            ALARM_DOOR_NOTCLOSED_LONGTIME = 12704,			//门长时间未关报警
            ALARM_LORA_DOOR_NOTCLOSED = 12705,			//单元门未关
            ALARM_LORA_LOWPOWER = 12706,			//低电压
            ALARM_LORA_WELLCOVER_MOVED = 12707,			//井盖移动
            ALARM_LORA_RUBBISH_FULL = 12708,			//垃圾满
            ALARM_LORA_DEVICE_FAULT = 12709,			//设备故障
            ALARM_LORA_SEWAGE_OVERLIMIT = 12710,			//污水超限
            ALARM_LORA_HIGH_TEMP_HUMIDITY = 12711,			//温湿度过高
            ALARM_LORA_LOW_TEMP_HUMIDITY = 12712,			//温湿度过低
            ALARM_LORA_VALUE_FAULT = 12713,			//阀门故障
            ALARM_PERSON_IDENTIFICATION_NOTSAME = 12714,			//人证不一致报警
            ALARM_WEBALARM_END = 12999,

            ALARM_TYRE_PRESSURE_ABNORMAL = 13000,				 //胎压异常
            ALARM_BATTERY_POWER_EVENT = 13001,				 //电池电量定时通知事件
            ALARM_VEHICLE_ACC = 13002,				 //车辆ACC报警事件
            ALARM_RETROGRADATION_DETECT = 13003,				 //逆行检测
            ALARM_TARGET_REMOVE_DETECT = 13004,				 //目标移除检测
            ALARM_GPS_MODULE_LOST = 13005,				 //GPS异常
            ALARM_WIFI_MODULE_LOST = 13006,				 //WIFI异常
            ALARM_3G4G_MODULE_LOST = 13007,				 //3G/4G异常
            ALARM_POLICE_CHECK = 13008,				 //单兵设备警员签到报警
            ALARM_WIFI_MODULE_OFFLINE = 13009,				 //WIFI模块离线
            ALARM_CHASSIS_INTRUSION = 13010,				 //报警柱防拆报警
            ALARM_WIFI_MODULE_ONLINE = 13011,				 //WIFI模块在线
            ALARM_DOOR_CONTROL = 13012,				 //报警输出联动开门事件
            ALARM_DOOR_NOTCLOSED_FORLONGTIME = 13013,				 //门超长时间未关报警事件
            ALARM_DOOR_ACCESS_CTL_STATUS = 13014,				 //门禁状态事件
            ALARM_RECHARGE_BUSINESS_QUERY = 13015,				 //充值机查询事件
            ALARM_RECHARGE_BUSINESS_RECHARGE = 13016,				 //充值机充值事件
            ALARM_CUSTOMER_BLACKLIST = 13017,				 //访客黑名单事件
            ALARM_VEHICLE_BRAKE_EX = 13018,               //刹车 走外部报警

            //海外门禁报警新增:13100-13500
            ALARM_DOOR_UNAUTHORIZE = 13100,				//未授权(无效刷卡)
            ALARM_DOOR_LOST = 13101,               //卡挂失或注销(无效刷卡)
            ALARM_DOOR_NO_PERMISSION = 13102,               //没有该门权限(无效刷卡)	
            ALARM_DOOR_ERR_MODE = 13103,               //开门模式错误(无效刷卡)
            ALARM_DOOR_ERR_VALIDITY = 13104,               //有效期错误(无效刷卡)
            ALARM_DOOR_ERR_REPEATENTERROUTE = 13105,               //防反潜模式(无效刷卡)
            ALARM_DOOR_FORCE_NOTOPEN = 13106,               //胁迫报警未打开(无效刷卡)
            ALARM_DOOR_ALWAYS_CLOSED = 13107,               //门常闭状态(无效刷卡)
            ALARM_DOOR_AB_CLOKED = 13108,               //AB互锁状态(无效刷卡)
            ALARM_DOOR_PATROL_CARD = 13109,               //巡逻卡(无效刷卡)
            ALARM_DOOR_IN_BROKEN = 13110,               //设备处于闯入报警状态(无效刷卡)
            ALARM_DOOR_ERR_TIMESECTION = 13111,               //时间段错误(无效刷卡)
            ALARM_DOOR_ERR_HOLIDAYTIMESECTION = 13112,               //假期内开门时间段错误(无效刷卡)
            ALARM_DOOR_NEED_FIRSTCARD_PERMISSION = 13113,               //需要先验证有首卡权限的卡片(无效刷卡)
            ALARM_DOOR_ERR_CARD_PASSWORD = 13114,               //卡片正确,输入密码错误(无效刷卡)
            ALARM_DOOR_INPUTCARDPWD_TIMEOUT = 13115,               //卡片正确,输入密码超时(无效刷卡)
            ALARM_DOOR_ERR_CARD_FINGERPRINT = 13116,               //卡片正确,输入指纹错误(无效刷卡)
            ALARM_DOOR_INPUTCARDFINGERPRINT_TIMEOUT = 13117,            //卡片正确,输入指纹超时(无效刷卡)
            ALARM_DOOR_ERR_FINGERPRINT_PASSWORD = 13118,              //指纹正确,输入密码错误(无效刷卡)
            ALARM_DOOR_INPUTFINGERPRINTPWD_TIMEOUT = 13119,              //指纹正确,输入密码超时(无效刷卡)
            ALARM_DOOR_ERR_GROUP = 13120,              //组合开门顺序错误(无效刷卡)
            ALARM_DOOR_GROUPN_NEED_VERIFY = 13121,              //组合开门需要继续验证(无效刷卡)
            ALARM_DOOR_CONSOLE_UNAUTHORIZE = 13122,              //验证通过,控制台未授权(无效刷卡)
            ALARM_DOOR_CARD_PWD_OPENDOOR = 13123,				//卡加密码开门
            ALARM_DOOR_CARD_FINGERPRINT_OPENDOOR = 13124,				//卡加指纹开门
            ALARM_DOOR_REMOTE_CONFIRM = 13125,				//远程验证
            ALARM_DOOR_GROUP_OPENDOOR_CONFIRM = 13126,				//组合开门验证通过
            ALARM_DOOR_GROUP_MANY_DOOR_CONFIRM = 13127,             //多卡组合

            ALARM_ATTENDANCESTATE_SIGNIN = 13130,
            ALARM_ATTENDANCESTATE_SIGNOUT = 13131,
            ALARM_ATTENDANCESTATE_WORK_OVERTIME_SIGNIN = 13132,
            ALARM_ATTENDANCESTATE_WORK_OVERTIME_SIGNOUT = 13133,
            ALARM_ATTENDANCESTATE_GOOUT = 13134,
            ALARM_ATTENDANCESTATE_GOOUT_AND_RETRUN = 13135,

            ALARM_WORK_CHECK_IN = 13200,
            ALARM_WORK_CHECK_OUT = 13201,
            ALARM_OVERTIME_CHECK_IN = 13202,
            ALARM_OVERTIME_CHECK_OUT = 13203,
            ALARM_GO_OUT = 13204,
            ALARM_GO_BACK = 13205,
            ALARM_CUSTOM_PASSWORD_OPEN = 13300,			// 个性化密码开门
            ALARM_RECORD_DOWNLOADORPLAYBACK_END = 13501,			// 录像文件下载、回放结束
            ALARM_TRAFFIC_JAM = 13502,         // 交通拥堵事件

            ALARM_CUSTOMER_STATISTICIAN_OVERFLOW = 13600,			// 客流统计报警 - 流量超限报警
            ALARM_CUSTOMER_STATISTICIAN_END = 13700,			// 客流统计报警 - 结束
            // 综合能源产品线 报警 开始
            // 14000 - 15000
            // 综合能源产品线 报警 结束

            //新增的车载报警类型  预留500个报警 15001 - 15500  其它请勿占用
            ALARM_BUS_LANE_DEPARTURE_WARNNING = 15001,			//车道偏离
            ALARM_BUS_FORWARD_COLLISION_WARNNING = 15002,			//前向碰撞预警
            ALARM_VEHICLE_STATE_START = 15003,            //车辆状态上报 开始运动
            ALARM_VEHICLE_STATE_BEYOND_10 = 15004,            //车辆状态上报 车速大于10km/h
            ALARM_VEHICLE_STATE_STOP = 15005,            //车辆状态上报 停止
            ALARM_SERVICE_DISTANCE_BELOW_2000 = 15006,            // 车辆保养距离小于2000km报警

            ALARM_NATIONAL_GRID_ACCIDENT = 15345,			//-电网事故  
            ALARM_RUN_ABNORMAL = 15346,			//-运行异常  
            ALARM_DEVICE_ACTION = 15347,			//-设备动作
            ALARM_REMOTE_LETTER_POSITIONING = 15348,			//-遥信定位  									
            ALARM_FAR_LETTER_CROSSLINE = 15349,			//-遥测越限  
            ALARM_NOTIFY_INFORMATION = 15350,			//-信息告知

            DPSDK_CORE_ALARM_IVS_CITY_MOTORPARKING = 0X0000024F + 15000, // 城市机动车违停事件 15591
            DPSDK_CORE_ALARM_IVS_MATERIALSSTAY = 0X00000253 + 15000, // 物料堆放事件 15595
            ALARM_CORE_ALARM_IVS_FLOATINGOBJECT_DETECTION = 0X00000257 + 15000, // 漂浮物检测事件 15599
            DPSDK_CORE_ALARM_IVS_HOTSPOT_WARNING = 0X00000268 + 15000, // 热点异常智能报警 15616 国网甘肃省电力公司智慧消防项目定制


            // 交通事件检测类型									//对应EnumCarRule中违章类型
            ALARM_TRAFFIC_ACTION_BENGIN = 50000,
            ALARM_TRAFFIC_ACTION_PARKING = 50001,			// 交通事件检测停车
            ALARM_TRAFFIC_ACTION_PEDESTRAIN = 50002,			// 交通事件检测行人
            ALARM_TRAFFIC_ACTION_CONVERSE_RUN = 50003,			// 交通事件检测逆行
            ALARM_TRAFFIC_ACTION_JAM = 50004,			// 交通事件检测拥堵
            ALARM_TRAFFIC_ACTION_OMISSION = 50005,			// 交通事件检测遗落物
            ALARM_TRAFFIC_ACTION_FOG = 50006,			// 交通事件检测烟雾
            ALARM_TRAFFIC_ACTION_BLAZE = 50007,			// 交通事件检测火焰
            ALARM_TRAFFIC_ACTION_SPEED = 50008,			// 交通事件检测超速
            ALARM_TRAFFIC_ACTION_LOWSPEED = 50009,			// 交通事件检测低速
            ALARM_TRAFFIC_ACTION_ONLINE = 50010,			// 交通事件检测压线
            ALARM_TRAFFIC_ACTION_SUDDEN_DECELE_RATION = 50011,			// 交通事件检测突然减速
            ALARM_TRAFFIC_ACTION_PASSERBY = 50012,			// 交通事件检测行人穿越
            ALARM_TRAFFIC_ACTION_BACK = 50013,			// 交通事件检测倒车
            ALARM_TRAFFIC_ACTION_RUN_FORBIDDEN_AREA = 50014,			// 交通事件检测禁行区行驶
            ALARM_TRAFFIC_ACTION_TRAIL_ANOMALY = 50015,			// 交通事件检测轨迹异常
            ALARM_TRAFFIC_ACTION_END = 59999,
            ALARM_VEHICLE_IMPORT_SITE = 600000,			//车载进站报警
            ALARM_VEHICLE_EXPORT_SITE = 600001,			//车载离站报警
            ALARM_VEHICLE_CIRCUIT_SHIFT = 600002,			//车载路线偏移报警
            ALARM_VEHICLE_ROUTE_OVERTIME = 600003,			//车载路线超时报警
            ALARM_BULLDOZE_FORCE = 600004,			//强拆报警
            ALARM_VAILID_FACE_OPEN = 600005,			//合法人脸开门报警
            ALARM_INVAILID_FACE_OPEN = 600006,			//非法人脸开门报警
            ALARM_CARD_AND_FACE_TIMEOUT = 600011,			//卡和人脸超时报警
            ALARM_CARD_AND_FACE_ERROR = 600012,			//卡和人脸错误报警
            ALARM_CARD_AND_FACE_OPEN = 600013,			//卡和人脸正确开门报警

            ALARM_DOOR_FINGERPRINT_AND_PWD_OPENDOOR = 700000,				//指纹+密码开锁
            ALARM_DOOR_FINGERPRINT_AND_FACE_OPENDOOR = 700001,				//指纹+人脸开锁
            ALARM_DOOR_FACE_AND_PWD_OPENDOOR = 700002,				//人脸+密码开锁
            ALARM_DOOR_CARD_AND_FINGERPRINT_AND_PWD_OPENDOOR = 700003,				//刷卡+指纹+密码开锁
            ALARM_DOOR_CARD_AND_FINGERPRINT_AND_FACE_OPENDOOR = 700004,				//刷卡+指纹+人脸开锁
            ALARM_DOOR_FINGERPRINT_AND_FACE_AND_PWD_OPENDOOR = 700005,				//指纹+人脸+密码
            ALARM_DOOR_CARD_AND_FACE_AND_PWD_OPENDOOR = 700006,				//刷卡+人脸+密码开锁
            ALARM_DOOR_CARD_AND_FINGERPRINT_AND_FACE_AND_PWD_OPENDOOR = 700007,				//卡+指纹+人脸+密码开锁
            //---预留到701000
            // 智能电源报警
            ALARM_POWERSRC_SHORT_CIRCUIT = 701001, //通道短路故障
            ALARM_POWERSRC_MAIN_POWER_OUTPUT = 701002, //主电源输出故障报警
            ALARM_POWERSRC_MAINS_INPUT = 701003, //市电输入故障报警
            ALARM_POWERSRC_FAN = 701004, //风扇故障报警	
            ALARM_POWERSRC_TEMPERATURE = 701005, // 温度告警
            ALARM_POWERSRC_VOLTAGE = 701006, // 电压告警
            ALARM_POWERSRC_OFFLINE = 701007, // 电源设备离线告警 

            //新接带图片门禁
            //701500-702000
            ALARM_FACE_DOOR_TYPE_ENABLEUSERCARD = 701500, //合法卡
            ALARM_FACE_DOOR_TYPE_ENABLEUSERCARD_VALID_FACE_OPEN = 701501, //合法人脸
            ALARM_FACE_DOOR_TYPE_VALID_FINGERPRINT = 701502, //合法指纹
            ALARM_FACE_DOOR_TYPE_VALID_PASSWORD = 701503, //合法密码

            ALARM_FACE_DOOR_TYPE_NOCARD = 701504, //非法卡
            ALARM_FACE_DOOR_TYPE_ENABLEUSERCARD_INVALID_FACE = 701505, //非法人脸
            ALARM_FACE_DOOR_TYPE_INVALID_FINGERPRINT = 701506, //非法指纹
            ALARM_FACE_DOOR_TYPE_INVALID_PASSWORD = 701507, //非法密码

            ALARM_FACE_DOOR_REMOTE_CONFIRM_OPEN = 701508, //远程验证		
        }

        public enum dpsdk_sdk_type_e
        {
            /// <summary>
            /// 服务模式使用
            /// </summary>
            DPSDK_CORE_SDK_SERVER = 1
        }
        /// <summary>
        /// 初始化参数
        /// </summary>
        public class DPSDK_CreateParam_t
        {
            /// <summary>
            /// 用户id标识符
            /// </summary>
            dpsdk_ipproto_type_e eSipProto;
            /// <summary>
            /// SCAgent设置，默认为DSSCClient，可设置为APPClient
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
            public string szSCAgent;
        }

        public enum dpsdk_ipproto_type_e
        {
            DPSDK_IPPROTO_UDP = 1,	    //UDP
            DPSDK_IPPROTO_TCP = 2,		//TCP
        }

        /// <summary>
        /// 登录信息
        /// </summary>
        public struct Login_Info_t
        {
            /// <summary>
            /// 服务IP
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 46)]
            public string szIp;
            /// <summary>
            /// 服务端口
            /// </summary>
            public uint nPort;
            /// <summary>
            /// 用户名
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szUsername;
            /// <summary>
            /// 密码
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szPassword;
            /// <summary>
            /// 协议库类型
            /// </summary>
            public dpsdk_protocol_version_e nProtocol;
            /// <summary>
            /// 登陆类型，1为PC客户端, 2为手机客户端
            /// </summary>
            public uint iType;
        }

        public enum dpsdk_protocol_version_e
        {
            /// <summary>
            /// 一代协议
            /// </summary>
            DPSDK_PROTOCOL_VERSION_I = 1,
            /// <summary>
            /// 二代协议
            /// </summary>
            DPSDK_PROTOCOL_VERSION_II = 2,
        }

        public enum dpsdk_door_status_e
        {
            Door_Close = 0,                                //门关闭
            Door_Open = 1,                                //门打开
            Door_DisConn = 2,                                //门离线
        }

        public enum Core_EnumSetDoorCmd
        {
            CORE_DOOR_CMD_PROGARM,
            CORE_DOOR_CMD_OPEN = 5,				                        // 开门
            CORE_DOOR_CMD_CLOSE = 6,				                    // 关门
            CORE_DOOR_CMD_ALWAYS_OPEN,						            // 门常开
            CORE_DOOR_CMD_ALWAYS_CLOSE,						            // 门常关
            CORE_DOOR_CMD_HOLIDAY_MAG_OPEN,					            // 假日管理门常开
            CORE_DOOR_CMD_HOLIDAY_MAG_CLOSE,				            // 假日管理门常关
            CORE_DOOR_CMD_RESET,							            // 复位
            CORE_DOOR_CMD_HOST_ALWAYS_OPEN,					            // 报警主机下的门禁通道，常开
            CORE_DOOR_CMD_HOST_ALWAYS_CLOSE,				            // 报警主机下的门禁通道，常关
        }

        // 门控制请求
        [StructLayout(LayoutKind.Sequential)]
        public struct SetDoorCmd_Request_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;			                            //通道ID
            public Core_EnumSetDoorCmd cmd;									//控制命令
            public Int64 start;										        //开始时间
            public Int64 end;										        //结束时间
        }

        public enum dpsdk_call_type_e
        {
            CALL_TYPE_SINGLE_CALL,          // 单呼
            CALL_TYPE_GROUP_CALL,           // 组呼
            CALL_TYPE_VT_CALL,				// 可视对讲
        }

        // 呼叫业务状态
        public enum dpsdk_call_status_e
        {
            CALL_STATUS_PREPARED,           //准备
            CALL_STATUS_REQTOSCS,           //向SCS请求
            CALL_STATUS_CALLING,            //呼叫中
            CALL_STATUS_RECVING,            //接收中
            CALL_STATUS_CEASED,				//呼叫释放
        }

        // 音频类型
        public enum dpsdk_audio_type_e
        {
            Talk_Coding_Default = 0,                     //default
            Talk_Coding_PCM = 1,                     //PCM
            Talk_Coding_G711a,                                                   //G711a
            Talk_Coding_AMR,                                                     //AMR
            Talk_Coding_G711u,                                                   //G711u
            Talk_Coding_G726,                                                    //G726
            Talk_Coding_AAC = 8,                     //add by fengjian 2012.8.8
            Talk_Coding_G722 = 101,                  //G722 海康设备使用
            Talk_Coding_G711_MU,		                                         //G711 海康使用
        }

        //位数
        public enum dpsdk_talk_bits_e
        {
            Talk_Audio_Bits_8 = 8,
            Talk_Audio_Bits_16 = 16,
        }

        //采样率
        public enum Talk_Sample_Rate_e
        {
            Talk_Audio_Sam_8K = 8000,
            Talk_Audio_Sam_16K = 16000,
            Talk_Audio_Sam_8192 = 8192,
            Talk_Audio_Sam_48k = 48000,
        }

        ///
        public enum dpsdk_talk_type_e
        {
            Talk_Type_Device = 1,                    //设备
            Talk_Type_Channel		                                             //通道
        }

        // 获取语音信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Get_TalkStream_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;           // 设备ID或通道ID
            public dpsdk_audio_type_e nAudioType;                                 // 音频类型		  
            public dpsdk_talk_type_e nTalkType;                                  // 设备类型（通道或设备）
            public dpsdk_talk_bits_e nBitsType;                                 // 位数
            public Talk_Sample_Rate_e nSampleType;                              // 采样率类型
            public dpsdk_trans_type_e nTransType;									// 传输类型
        }


        // 呼叫邀请信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Get_InviteCall_Info_t
        {
            public dpsdk_call_type_e nCallType;									//呼叫类型
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szSendChnlId;		//发送通道
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szRecvChnlId;		//接收通道
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string szRtpServIP;              //TRP服务IP
            public int nRtpPort;                                    //TRP服务端口
            public int nCallID;                                 //呼叫ID
            public int nDlgID;                                      //会话ID
            public int nTid;										//T ID
        }

        // 
        [StructLayout(LayoutKind.Sequential)]
        public struct AudioUserParam_t
        {
            public IntPtr objectPoint;								// 音频录音获取用户参数
        }

        //集群对讲结构体定义

        //StartCall返回的对讲参数
        [StructLayout(LayoutKind.Sequential)]
        public struct StartCallParam_t
        {
            public int returnValue;                         // 平台返回的错误码，0代表成功，其它失败
            public int seq;                                 // 用于关闭呼叫的seq
            public UInt32 sessionId;								// 语音对讲会话Id
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szGroupId;		// 设备ID或通道ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string strSendChnlID;	// 发送通道ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string strRecvChnlID;	// 接收通道ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string rtpServIP;            // 远端RTP IP
            public int rtpPort;                             // 远端RTP端口
            public int talkMode;                                // 对讲模式 0对讲 1广播（喊话）
            public int audioType;                               // 1 PCM，2 G.711，3 G.723，4 G.726，5 G.729
            public int audioBit;                                // 用实际的值表示，如8位 则填值为8
            public UInt32 sampleRate;                               // 采样率，如16k 则填值为16000	
            public int transMode;                               // 1 tcp,2 udp;

            //SCS返回，报错时候使用
            public int cmsaudioType;                            // 1 PCM，2 G.711，3 G.723，4 G.726，5 G.729
            public int cmsaudioBit;                         // 用实际的值表示，如8位 则填值为8
            public UInt32 cmssampleRate;                            // 采样率，如16k 则填值为16000
            public int callId;
            public int dlgId;
        }

        //InviteCall返回的对讲参数
        [StructLayout(LayoutKind.Sequential)]
        public struct InviteCallParam_t
        {
            public int audioType;                           // 1 PCM，2 G.711，3 G.723，4 G.726，5 G.729
            public int audioBit;                            // 用实际的值表示，如8位 则填值为8
            public UInt32 sampleRate;							// 采样率，如16k 则填值为16000	
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string rtpServIP;
            public int rtpPort;
            public dpsdk_call_type_e nCallType;							//呼叫类型，单呼还是组呼
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string groupID;		//呼叫者ID，组呼时为组ID，单呼时为ID#IP
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string callerID;	//讲话者ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string sendChnlID;	//发送通道，单呼时才会用到
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string recvChnlID;   //接收通道，穿网包需要带上
            public int callId;
            public int dlgId;
            public int tid;
        }

        //音频采集参数
        [StructLayout(LayoutKind.Sequential)]
        public struct AudioRecrodParam_t
        {
            public dpsdk_audio_type_e audioType;                                // 1 PCM，2 G.711，3 G.723，4 G.726，5 G.729
            public dpsdk_talk_bits_e audioBit;                              // 用实际的值表示，如8位 则填值为8
            public Talk_Sample_Rate_e sampleRate;								// 采样率，如16k 则填值为16000
        }

        //可视对讲结构体定义 begin

        //邀请对讲参数
        [StructLayout(LayoutKind.Sequential)]
        public struct InviteVtCallParam_t
        {
            public int audioType;                       // 1 PCM，G.711，G.723，G.726，G.729
            public int audioBit;                        // 用实际的值表示，如位则填值为
            public UInt32 sampleRate;						    // 采样率，如k 则填值为
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string rtpServIP;                        // 远端RTP IP
            public int rtpAPort;                        // 音频端口
            public int rtpVPort;                        // 视频端口
            public dpsdk_call_type_e nCallType;						// 呼叫类型，单呼还是组呼
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szUserId;                     // 呼叫者ID
            public int callId;
            public int dlgId;
            public int tid;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;                     // 视频通道ID
        }

        //响铃通知参数
        [StructLayout(LayoutKind.Sequential)]
        public struct RingInfo_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szUserId;                             // 设备ID或通道ID
            public int callId;
            public int dlgId;
            public int tid;
        }

        //可视对讲状态繁忙通知参数
        [StructLayout(LayoutKind.Sequential)]
        public struct BusyVtCallInfo_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szUserId;                             // 设备ID或通道ID
            public int callId;
            public int dlgId;
        }

        //可视对讲应答参数
        [StructLayout(LayoutKind.Sequential)]
        public struct StartVtCallParam_t
        {
            public int videoPort;                       // 本地视频端口
            public int audioPort;						// 本地音频端口
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 48)]
            public string rtpServIP;                        // 远端RTP IP
            public int rtpAPort;                        // 远端RTP音频端口
            public int rtpVPort;                        // 远端RTP视频端口
            public int talkMode;                        // 对讲模式0对讲1广播（喊话）
            public int audioType;                       // 1 PCM，G.711，G.723，G.726，G.729
            public int audioBit;                        // 用实际的值表示，如位则填值为
            public UInt32 sampleRate;                       // 采样率，如k 则填值为	
            public int callId;
            public int dlgId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCameraId;                     // 视频通道ID
        }

        //集群对讲结构体定义 end

        [StructLayout(LayoutKind.Sequential)]
        public struct Person_Count_Info_t
        {
            public int nChannelID;									// 统计通道号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string szRuleName;                               // 规则名称
            public UInt32 nStartTime;									// 开始时间
            public UInt32 nEndTime;									// 结束时间
            public UInt32 nEnteredSubTotal;							// 进入人数小计
            public UInt32 nExitedSubtotal;							// 出去人数小计
            public UInt32 nAvgInside;									// 平均保有人数(除去零值)
            public UInt32 nMaxInside;									// 最大保有人数
        }

        public enum dpsdk_mdl_type_e
        {
            DPSDK_MDL_UNKNOW = -1, //未知模块
            DPSDK_MDL_APP,
            DPSDK_MDL_CMS,
            DPSDK_MDL_PCS,
            DPSDK_MDL_DMS,
            DPSDK_MDL_ADS,
            DPSDK_MDL_TRAN, //中转模块
            DPSDK_MDL_RTSP,
            DPSDK_MDL_FTP,
            DPSDK_MDL_PEC,
            DPSDK_MDL_MGR,
            DPSDK_MDL_AREA,
            DPSDK_MDL_CMS_FORSNVD,
            DPSDK_MDL_SCS,
            DPSDK_MDL_COUNT,   //模块总数
        }	//默认族类

        // JSON 传输类型
        public enum generaljson_trantype_e
        {
            GENERALJSON_TRAN_UNKNOW = -1,
            GENERALJSON_TRAN_REQUEST,
            GENERALJSON_TRAN_RESPONSE,
            GENERALJSON_TRAN_NOTIFY,
            GENERALJSON_TRAN_COUNT,
        }

        //Prison模块结构体定义 begin

        // 刻录实时状态信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Dev_Burner_CDState_Info_t
        {
            //char								szName[DPSDK_CORE_TVWALL_NAME_LEN];
            public UInt32 m_burnerId;                                           // 刻录机ID, ID从0开始
            public UInt32 m_burnerState;                                        // 刻录机状态 0：可以刻录 1：刻录机类型不对，是一个非光盘设备 
                                                                                // 2：未找到刻录机 3：有其它光盘在刻录 4：刻录机处于非空闲状态，即在备份、刻录或回放中
            public UInt32 m_romType;                                            // 盘片类型 0：大华文件系统 1：移动硬盘或U盘 2：光盘
            public UInt32 m_operateType;                                        // 操作类型 0：空闲 1：正在备份中 2：正在刻录中 3：正在进行光盘回放
            public UInt32 m_processState;                                       // 进度状态 0：停止或结束 1：开始 2：出错 3：满 4：正在初始化
            public UInt32 m_startTime;                                      // 开始时间
            public UInt32 m_elapseTime;                                     // 已刻录时间
            public UInt32 m_totalSpace;                                     // 光盘总容量
            public UInt32 m_remainSpace;                                        // 光盘剩余容量
            public UInt32 m_burned;                                         // 已刻录容量
            public UInt32 m_channelMask;                                        // 刻录的通道掩码
            public UInt32 m_emMode;                                         // 刻录模式0-BURN_MODE_SYNC,1-BURN_MODE_TURN,2-BURN_MODE_CYCLE
            public UInt32 m_emPack;											// 刻录流格式0-DHAV,1-BURN_PACK_PS...

        }

        // 获取刻录实时状态信息请求
        [StructLayout(LayoutKind.Sequential)]
        public struct Dev_Burner_CDState_Request_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string deviceId;         // 设备ID
            public UInt32 burnerId;			// 光盘刻录机ID (id从0开始计数)

        }

        // 获取刻录实时状态信息回复
        [StructLayout(LayoutKind.Sequential)]
        public struct Dev_Burner_CDState_Reponse_t
        {
            public Dev_Burner_CDState_Info_t stuDevBurnerStateInfo;

        }

        //刻录控制命令
        public enum Core_EnumControlBurnerCmd
        {
            Cmd_StartBurn = 1,                                                          // 开始刻录
            Cmd_PauseBurn,                                                              // 暂停刻录
            Cmd_ContinueBurn,                                                           // 继续刻录
            Cmd_StopBurn,                                                               // 停止刻录
            Cmd_ChangeCD,																// 手动换盘
        }

        // 获取刻录实时状态信息请求
        [StructLayout(LayoutKind.Sequential)]
        public struct Control_Dev_Burner_Request_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string deviceId;                             // 设备ID
            public Core_EnumControlBurnerCmd cmd;                                   // 控制命令
            public int channelMask;                         // 通道掩码    第1个通道为1；第2个通道是：1<<1;第3个是1<<2
            public int burnerMask;                              // 刻录机掩码  1表示光驱1；2光驱2；3双光驱
                                                                //庭审相关
            public int emMode;                                  // 刻录模式    0-BURN_MODE_SYNC,1-BURN_MODE_TURN,2-BURN_MODE_CYCLE
            public int emPack;									// 刻录流格式  0-DHAV,1-BURN_PACK_PS...
        }

        //刻录片头
        [StructLayout(LayoutKind.Sequential)]
        public struct DevBurnerInfoHeader_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_deviceId;					                 // 设备ID
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string m_password;					                 //  叠加密码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string m_caseId;					                 // 1.案件编号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string m_trialSeq;				                     // 2.案件序号/审讯序号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string m_caseUnderTaker;			                 // 3.办案人员
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string m_caseDep;					                 // 4.办案单位	
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string m_caseReason;				                 // 5.涉嫌名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string m_caseReferPerson;			                 // 6.涉案人员
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string m_caseRemark;				                 // 7.案卷备注
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string m_caseRecordName;			                 // 8.录像名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string m_RecordNum;				                 // 9.光盘编号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string m_recordPerson;                                // 10.刻录人
            public bool m_dataCheckOsdEn;                            // 11.光盘刻录数据校验配置/叠加使能
            public bool m_AttachFileEn;                              // 12.附加文件使能
            public bool m_multiBurnerDataCheck;                      // 13.多光盘一致性校验使能
            public UInt32 m_multiBurnerDataCheckSpeed;				 // 14.校验速度校验速度 0 高速（头尾数据校验）,1 正常（随机数据校验）,2 低速 （全盘数据校验）,默认0
        }

        //审讯表单属性名
        [StructLayout(LayoutKind.Sequential)]
        public struct TrialFormAttrName_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_caseIdAttr;				// 案件编号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_trialSeqAttr;			    // 案件序号/审讯序号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_caseUnderTakerAttr;		// 办案人员
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_caseDepAttr;				// 办案单位
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_caseReasonAttr;			// 涉嫌名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_caseReferPersonAttr;		// 涉案人员
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_caseRemarkAttr;			// 案卷备注
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_caseRecordNameAttr;		// 录像名称
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_trialObjNameAttr;			// 被审讯人姓名
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_trialObjSexAttr;			// 被审讯人性别
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_trialObjIDAttr;			// 被审讯人身份证号码
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_trialObjNationAttr;		// 民族
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_trialObjBirthdayAttr;		// 出生年月
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_trialObjHomeAddrAttr;		// 住址
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_trialObjWorkUnitsAttr;	// 工作单位
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_trialObjTelephoneAttr;	// 联系电话
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_CDIDAttr;					// 光盘编号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_recordPersonAttr;			// 刻录人
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_caseAssistantAttr;		// 协助办案人员--卷宗信息新增，非必填项
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string m_trialObjAgeAttr;		    	// 年龄--被询问人信息-新增，非必填项
        }

        //单个磁盘信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Single_Disk_Info_t
        {
            public int nDiskId;                 // 硬盘ID（从0开始）
            public UInt32 uVolume;                  // 硬盘容量
            public UInt32 uFreeSpace;                   // 剩余容量
            public byte diskState;                  // 高四位的值表示硬盘类型，具体为：0 读写驱动器 1 只读驱动器 2 备份驱动器或媒体驱动器 3 冗余驱动器 4 快照驱动器；低四位的值表示硬盘的状态，0-休眠,1-活动,2-故障
            public byte diskNum;                    // 硬盘号
            public byte subareaNum;					// 分区号
            public byte signal;						// 标识， 0本地 1 远程
        }

        //设备磁盘信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Device_Disk_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public char szDevId;                        // 设备ID
            public int nDiskInfoSize;                   // 磁盘信息数量
                                                        // Single_Disk_Info_t*		pDiskInfoList;					// 磁盘信息列表
            public IntPtr pDiskInfoList;							// 磁盘信息列表

            //tagDiskInfo()
            //{
            //    nDiskInfoSize = 0;
            //    pDiskInfoList = NULL;
            //}

            //~tagDiskInfo()
            //{
            //    nDiskInfoSize = 0;
            //    if(pDiskInfoList)
            //    {
            //        delete [] pDiskInfoList;
            //        pDiskInfoList = NULL;
            //    }
            //}
        }

        //Prison模块结构体定义 end

        //单张图片信息
        [StructLayout(LayoutKind.Sequential)]
        public struct One_Ftp_Pic_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szDevId;                                  // 摄像头ID
            public Int32 nChlNo;									// 通道号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szCapTime;			                    // 抓图时间
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szFtpPath;		                        // Ftp保存图片路径
        }

        //图片信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Ftp_Pic_Info_t
        {
            //[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 256)]
            //public One_Ftp_Pic_Info_t[] oneFtpPicInfo;                                      // 图片信息
            // One_Ftp_Pic_Info_t oneFtpPicInfo[DPSDK_CORE_QUERY_PIC_MAXCOUT];			    // 图片信息
            public IntPtr oneFtpPicInfo;                                                    // 图片信息 
            public Int32 nSize;																// 图片张数
        }

        // 报警输入通道信息
        [StructLayout(LayoutKind.Sequential)]
        public struct AlarmIn_Channel_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szId;				        // 通道ID:设备ID+通道号
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string szName;	                                    // 名称
            public UInt64 nRight;                                     // 权限信息
            public Int32 nChnlType;                                  // 通道类型
            public Int32 nStatus;
        }

        // 获取报警输入通道请求信息
        [StructLayout(LayoutKind.Sequential)]
        public struct Get_AlarmInChannel_Info_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szDeviceId;                                   // 设备ID
            public UInt32 nAlarmInChannelCount;                     // 通道个数
                                                                    //OUT AlarmIn_Channel_Info_t*			pAlarmInChannelnfo;							// 通道信息
            public IntPtr pAlarmInChannelnfo;							// 通道信息
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct dpsdk_AHostDefenceStatus_t
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szNodeID;                     // 防区节点ID
            public Int32 nAlarm;                                // 0表示未报警 1104表示报警 1105表示火警 1106表示防
            public Int32 nUndefendAlarm;                        // 0表示没有未布防报警 83表示未布防报警（由于24小时防区会出现布防报警和未布防报警并发的情况 所以需要区分一下）
            public bool bByPass;                                // true=旁路, false=未旁路
            public bool bDefend;								// true=布防, false=撤防

        }


        private System.Windows.Forms.Button LoadOrganization;
        private System.Windows.Forms.TreeView treeView;

        /// <summary>
        /// /////////////////////////////////////////olddesigner
        /// </summary>
        /// 
        [StructLayout(LayoutKind.Sequential)]
        public struct Item_Infor_t
        {
            public int nType;      // =1 组织 =2 设备 =3 通道
            public string strCode;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OrgNode
        {
            public string parent;       // 父节点coding
            public List<string> child;  // 子节点coding
            public string code;         // 本级节点编码
            public string name;         // 本级节点名称

            public List<string> strDev;    //当前节点的设备ID
            public List<string> strChnl;   //当前节点的通道ID

        }
        Dictionary<string, OrgNode> OrgInfo = new Dictionary<string, OrgNode>();

        //[StructLayout(LayoutKind.Sequential)]
        //public struct NodeInfo
        //{
        //    public List<string> strDev;    //当前节点的设备ID
        //    public List<string> strChnl;   //当前节点的通道ID
        //}

        //Dictionary<HTREEITEM, Item_Infor_t> TreeItemMap;
        //Dictionary<string, HTREEITEM> CodeTreeItemMap; // 不同的节点不同的ID 组织：code 设备：设备id 通道：通道id
        public IntPtr nPDLLHandle = (IntPtr)0;
        public IntPtr nGroupLen = IntPtr.Zero;
        //public Int32 nGroupLen = 0;
        public IntPtr realseq = default(IntPtr);
        public int TvWallId = -1;
        public int ScreenId = -1;
        public int WindowId = -1;
        public IntPtr playbackseq = default(IntPtr);
        public int downloadSeq = 0;
        public IntPtr m_pAudioDataFun;
        public IntPtr m_pAudioDataUserParam;
        public static InviteVtCallParam_t m_stuInviteVtCallParam = new InviteVtCallParam_t();
        public static StartVtCallParam_t m_stuStartVtCallParam = new StartVtCallParam_t();
        public static bool bVtCallInvate = false;
    }
}
