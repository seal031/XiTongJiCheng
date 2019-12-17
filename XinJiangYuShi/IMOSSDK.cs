using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public enum IMOS_PICTURE_FORMAT_E
{
    IMOS_PF_PAL = 0,                            /* PAL 制式 */
    IMOS_PF_NTSC,                               /* NTSC 制式 */
    IMOS_PF_720P24HZ,
    IMOS_PF_720P25HZ,
    IMOS_PF_720P30HZ,
    IMOS_PF_720P50HZ,
    IMOS_PF_720P60HZ,
    IMOS_PF_1080I48HZ,
    IMOS_PF_1080I50HZ,
    IMOS_PF_1080I60HZ,
    IMOS_PF_1080P24HZ,
    IMOS_PF_1080P25HZ,
    IMOS_PF_1080P30HZ,
    IMOS_PF_1080P50HZ,
    IMOS_PF_1080P60HZ,
    IMOS_PF_AUTO,
    IMOS_PF_INVALID
};

/* 前端设备类型 */
public enum IMOS_DEVICE_TYPE_E
{
    IMOS_DT_EC1001_HF = 0,
    IMOS_DT_EC1002_HD,                          /* 软型号 由 EC1004_HC 配置而成，对应 EC1004-HC[2CH] */
    IMOS_DT_EC1004_HC,
    IMOS_DT_EC2004_HF,
    IMOS_DT_ER3304_HF,
    IMOS_DT_ER3308_HD = 5,
    IMOS_DT_ER3316_HC,
    IMOS_DT_DC1001_FF = 7,
    IMOS_DT_EC3016_HF,
    IMOS_DT_ISC3100_ER,
    IMOS_DT_EC1001_EF = 10,
    IMOS_DT_EC3004_HF_ER,
    IMOS_DT_EC3008_HD_ER,

    /* 枚举值 200 -- 500 */
    IMOS_DT_EC2016_HC = 200,
    IMOS_DT_EC2016_HC_8CH,
    IMOS_DT_EC2016_HC_4CH,
    IMOS_DT_EC1501_HF,                          /* 6437真双流 */
    IMOS_DT_EC1004_MF,                          /* 多路EC */
    IMOS_DT_ECR2216_HC,                         /* ECR预研，1U */
    IMOS_DT_EC1304_HC,                          /* 带PCMCIA */

    /* 枚举值 500 -- 800 */
    IMOS_DT_DC3016_FC = 500,                    /* 目前V1R5，2U，和ISC3000-M共用设计 */
    IMOS_DT_DC2016_FC,                          /* DC3016-FC降成本 */
    IMOS_DT_DC1016_MF,                          /* 多路DC */
    IMOS_DT_DC2004_FF,                          /* 带数字矩阵的多路DC */

    IMOS_DT_EC1001 = 1000,                      /* R1 编码板，为了方便扩展，从1000开始计数 */
    IMOS_DT_DC1001 = 1001,                      /* R1 解码板 */
    IMOS_DT_EC1101_HF = 1002,
    IMOS_DT_EC1102_HF = 1003,
    IMOS_DT_EC1801_HH = 1004,
    IMOS_DT_DC1801_FH = 1005,

    /* OEM产品型号 */
    IMOS_DT_VR2004 = 10003,
    IMOS_DT_R1000 = 10203,
    IMOS_DT_VL2004 = 10503,
    IMOS_DT_VR1102 = 11003,
    /* IPC产品型号 */
    IMOS_DT_HIC5201 = 12001,
    IMOS_DT_HIC5221 = 12002,
    IMOS_DT_BUTT
};

/**
* @enum tagMediaFileFormat
* @brief 媒体文件格式枚举定义
* @attention 无
*/
public enum XP_MEDIA_FILE_FORMAT_E
{
    XP_MEDIA_FILE_TS = 0,              /**< TS格式的媒体文件 */
    XP_MEDIA_FILE_FLV = 1               /**< FLV格式的媒体文件 */
};

public enum IMOS_STREAM_RELATION_SET_E
{
    IMOS_SR_MPEG4_MPEG4 = 0,                    /* MPEG4[主码流] + MPEG4[辅码流] */
    IMOS_SR_H264_SHARE = 1,                     /* H.264[主码流] */
    IMOS_SR_MPEG2_MPEG4 = 2,                    /* MPEG2[主码流] + MPEG4[辅码流] */
    IMOS_SR_H264_MJPEG = 3,                     /* H.264[主码流] + MJPEG[辅码流] */
    IMOS_SR_MPEG4_SHARE = 4,                    /* MPEG4[主码流] */
    IMOS_SR_MPEG2_SHARE = 5,                    /* MPEG2[主码流] */
    IMOS_SR_STREAM_MPEG4_8D1 = 8,          /* MPEG4[主码流_D1] 8D1 套餐*/
    IMOS_SR_MPEG2_MPEG2 = 9,                    /* MPEG2[主码流] + MPEG2[辅码流] */
    IMOS_SR_H264_H264 = 11,                     /* H.264[主码流] + H.264[辅码流] */

    IMOS_SR_BUTT
};


/**
* @enum tagIMOSFavoriteStream
* @brief  流配置策略类型
* @attention 无
*/
public enum IMOS_FAVORITE_STREAM_E
{
    IMOS_FAVORITE_STREAM_ANY = 0,        /**< 不指定 */
    IMOS_FAVORITE_STREAM_PRIMARY = 1,        /**< 指定主流 */
    IMOS_FAVORITE_STREAM_SECONDERY = 2,        /**< 指定辅流 */
    IMOS_FAVORITE_STREAM_THIRD = 3,        /**< 指定三流 */
    IMOS_FAVORITE_STREAM_FOURTH = 4,        /**< 指定四流 */
    IMOS_FAVORITE_STREAM_FIFTH = 5,        /**< 指定五流 */
    IMOS_FAVORITE_STREAM_BI_AUDIO = 6,        /**< 指定语音对讲 */
    IMOS_FAVORITE_STREAM_VOICE_BR = 7,        /**< 指定语音广播 */
    IMOS_FAVORITE_STREAM_BUTT,
    IMOS_FAVORITE_STREAM_INVALID = 0xFFFF    /**< 无效值 */
}

public enum XP_PROTOCOL_E
{
    XP_PROTOCOL_UDP = 0,                   /**< UDP协议 */
    XP_PROTOCOL_TCP = 1,                   /**< TCP协议Client端*/
    XP_PROTOCOL_TCP_SERVER = 2             /**< TCP协议Server端*/
}
/// <summary>
/// 查询项类型
/// @attention 300~500 视讯使用
/// </summary>
public enum QUERY_TYPE_E
{
    CODE_TYPE = 0,    /**< 编码类型(除用户编码之外) */
    NAME_TYPE = 1,    /**< 名称类型(除用户名称之外) */
    USER_CODE_TYPE = 2,    /**< 用户编码类型 */
    USER_NAME_TYPE = 3,    /**< 用户名称类型 */
    TIME_TYPE = 4,    /**< 时间类型 */

    EVENT_TYPE = 5,    /**< 告警事件类型,取值为#AlARM_TYPE_E */
    EVENT_SECURITY = 6,    /**< 告警事件级别,取值为#ALARM_SEVERITY_LEVEL_E */
    EVENT_STATUS_TYPE = 7,  /**< 告警状态,取值为#ALARM_STATUS_E */
    EVENT_TIME = 8,    /**< 告警时间 */

    DEV_SUB_TYPE = 9,    /**< 设备子类型 */

    INDEX_TYPE = 10,   /**< 索引类型(如视频输入通道索引,视频输出通道索引,串口索引,开关量索引) */

    RES_SUB_TYPE = 11,   /**< 资源子类型 */

    /* Begin: Add by x06948 for VVD38087, 20100115 */
    SRV_TYPE = 12,       /**< 业务类型 */
    MONITOR_TYPE = 13,       /**< 监视器类型 */
    MONITOR_NAME = 14,       /**< 监视器名称 */
    MONITOR_DOMAIN = 15,       /**< 监视器所在域 */
    CAMERA_NAME = 16,       /**< 摄像机名称 */
    USER_LOGIN_IP = 17,       /**< 用户登录IP */
    MON_SRV_STATUS = 18,       /**< 实况状态 */
    VOCBRD_SRV_STATUS = 19,       /**< 广播状态 */
    /* End: Add by x06948 for VVD38087, 20100115 */
    //RES_TYPE            = 20,       /**< 资源类型 */

    CASE_DESC = 20,       /**< 案例描述 */
    TASK_STATUS = 21,       /**< 任务状态 */
    TASK_SUB_TIME = 22,       /**< 任务提交时间 */
    TASK_END_TIME = 23,       /**< 任务结束时间 */
    BAK_START_TIME = 24,       /**< 备份开始时间 */
    BAK_END_TIME = 25,       /**< 备份结束时间 */
    FILE_CREATE_TIME = 26,       /**< 文件创建时间 */
    FILE_CAPACITY = 27,       /**< 文件容量 */
    FILE_TYPE = 28,       /**< 文件类型 */
    FILE_LOCK_FLAG = 29,       /**< 文件锁定标识 */
    LAYOUT_TYPE = 30,       /**< 布局类型，取值为#LAYOUT_TYPE_E */

    PHY_DEV_IP = 31,

    ASSET_TYPE = 32,       /**< 资产类型，取值为#ASSET_TYPE_E */
    ASSET_MODEL = 33,       /**< 资产型号 */
    ASSET_MANUFACTURE = 34,       /**< 资产厂商 */
    ASSET_STATUS = 35,       /**< 资产状态，取值为#ASSET_STATUS_E */
    ASSET_PURCHASE_TIME = 36,       /**< 资产采购时间 */
    ASSET_WARRANT_TIME = 37,       /**< 资产保修时间 */
    ASSET_INSTALL_TIME = 38,       /**< 资产安装时间 */
    ASSET_INSTALL_LOCATION = 39,       /**< 资产安装地点 */
    ASSET_PRODEALER = 40,       /**< 资产工程商 */
    FAULT_STAT_TIME = 41,       /**< 故障统计时间 */
    FAULT_TYPE = 42,       /**< 故障类型 */
    FAULT_RECO_TIME = 43,       /**< 故障恢复时间 */
    FAULT_INTERVAL = 44,       /**< 故障时长 */
    FAULT_COUNT = 45,       /**< 故障次数 */

    PLATE_CODE = 46,       /**< 车牌号码 */
    STUFF_TYPE = 47,       /**< 涉案物品类型 */
    STUFF_PROPERTY = 48,       /**< 涉案物品性质 */
    SERIESCASE_TYPE = 49,       /**< 串并案类型 */
    CASEINSE_CODE = 50,       /**< 串并案中案件编号 */
    CASESHARE_TYPE = 51,       /**< 案件共享类型 */
    CASE_PROPERTY = 52,       /**< 案件性质 */
    CASE_TYPE = 53,       /**< 案件类型 */
    CASE_STATUS = 54,       /**< 案件状态 */
    POLICE_INTSRC = 55,       /**< 案件警情来源 */
    CASE_BEGIN_TIME = 56,       /**< 案件开始时间 */
    CASE_END_TIME = 57,       /**< 案件结束时间 */
    STAT_START_TIME = 58,       /**< 统计开始时间 */
    STAT_END_TIME = 59,       /**< 统计结束时间 */
    EBULLETIN_EXPIRE_FLAG = 60,       /**< 电子公告过期标志 */
    CASE_DATA_TYPE = 61,       /**< 案件资料类型 */
    CASE_SOLVED_TIME = 63,       /**< 案件破案时间 */
    CASE_CLOSED_TIME = 64,       /**< 案件结案时间 */
    CASE_UPLOAD_TIME = 65,       /**< 案件资料上传时间 */
    CASE_CREATE_TIME = 66,       /**< 案件创建时间 */
    DEPT_CODE = 67,       /**< 部门编码 */

    MSGSTATUS = 69,/**< 消息处理编码 */

    DOMAIN_TYPE = 100,       /**< 域类型,现只支持本外域 */

    EXT_DOMAIN_TPYE = 200,      /**< 外域类型:上/下/平级域 */
    EXT_DOMAIN_IP = 201,      /**< 外域IP */
    EXT_DOMAIN_PORT = 202,      /**< 外域PORT */
    EXT_DOMAIN_TRUNK_NUM = 203,      /**< 外域干线数量 */
    EXT_DOMAIN_MULTICAST = 204,      /**< 域间组播策略 */
    EXT_DOMAIN_SESSION = 205,      /**< 外域引流标志 */
    EXT_DOMAIN_SUBTYPE = 206,      /**< 外域子类型 */

    JOB_STATUS = 210,          /**< 升级任务状态 */
    JOB_CREATOR = 211,          /**< 升级任务创建者 */
    JOB_EXEC_TIME = 212,          /**< 升级任务调度时间 */
    JOB_ESTB_TIME = 213,          /**< 升级任务制定时间 */
    JOB_END_TIME = 214,          /**< 升级任务完成时间 */
    JOB_RESULT = 215,          /**< 升级结果 */

    OPER_TIME = 220,      /**< 操作时间 */
    OPER_IP = 221,      /**< 操作者IP地址 */
    OPER_TYPE = 222,      /**< 操作类型 */
    OPER_RESULT = 223,      /**< 操作结果 */
    OPER_SERVICE_TYPE = 224,      /**< 操作的业务类型 */
    OPER_OBJ = 225,      /**< 操作对象 */

    LABEL_TIME = 230,          /**< 标签时间点 */
    REC_START_TIME = 231,          /**< 标签录像开始时间，格式为"hh:mm:ss" */
    REC_END_TIME = 232,          /**< 标签录像结束时间，格式为"hh:mm:ss" */

    USER_FULL_NAME = 240,      /**< 用户全名 */
    USER_TEL_PHONE = 241,      /**< 用户座机电话 */
    USER_MOBILE_PHONE = 242,      /**< 用户移动电话 */
    USER_EMAIL = 243,      /**< 用户电子邮件 */
    USER_IS_LOCKED = 244,      /**< 用户是否被锁定 */

    USER_DESC = 245,      /**< 用户描述 */

    ROLE_PRIORITY = 250,           /**< 角色优先级 */

    DEV_TYPE = 255,          /**< 设备类型 */
    RES_TYPE = 256,          /**< 资源类型 */
    IS_QUERY_SUB = 257,          /**< 是否查下级(需要调AS_CON_Parse进行解析) */
    RES_ID = 258,          /**< 资源ID */
    SUPPORT_LINK = 259,          /**< 是否支持联动 */
    SUPPORT_GUARD = 260,          /**< 是否支持布防 */
    RES_ATTRIBUTE = 261,          /**< 资源属性 */
    IS_QUERY_ENCODESET = 262,          /**< 是否查询摄像机或者监视器所在设备的流套餐，对应szQueryData为0-不查询，1-查询 */

    TYPE_ACTION_PLAN = 263,          /**< 预案类型 */
    IS_PLAN_ALARM = 264,          /**< 是否预案告警 */
    IS_ONCE_PLAN_ALARM = 265,          /**< 是否启动过预案 */
    IS_MISINFORM = 266,          /**< 是否误报 */
    CHK_USER_NAME_TYPE = 267,          /**< 核警用户名 */
    TRIGGER_ID = 268,          /**< 触发器ID */

    IS_HIDE_EMPTY_ORG = 269,          /**< 是否隐藏空组织 */
    IS_QUERY_SHARED = 270,          /**< 是查询已共享资源=1还是查未共享资源=0 */

    IS_LEACH_SHARED_BELONG = 271,      /**< 是否过滤掉因为资源共享给外域而在资源表中生成的划归记录(需要调AS_CON_Parse进行解析) */
    RESULT_CODE = 272,      /**< 结果码 */
    RES_ORDER_NUMBER = 273,      /**< 资源排序序号 */

    AUTOID = 273,      /**< 主键 */

    EVENT_TYPE_NAME = 273,      /**< 事件类型名称 */

    IS_QUERY_SYSTEM_EVENT_TYPE = 274,   /**< 0-查询全部;1-查询系统预定义;2-查询用户自定义 */

    IS_SUPPORT_BISTORE_CAM = 275,   /**< 是否增加支持双直存的外域摄像机过滤条件 */

    RES_STATUS_CONDITON = 276,      /**< 资源状态查询条件 */

    CONF_START_TIME = 300,      /**< 会议开始时间，1970年以来的秒数 */
    CONF_END_TIME = 301,      /**< 会议结束时间，1970年以来的秒数 */

    STORE_DEV_NAME = 302,          /**< 存储设备名称 */
    DM_NAME = 303,          /**< 数据管理服务器名称 */
    RES_BELONGIN = 304,          /**< 资源归属,0代表原始加入#MM_RESOURCE_BELONGIN_ORIGINAL，1代表划归过来#MM_RESOURCE_BELONGIN_TRANSFER */
    IS_CASE_SERVER = 310,      /**< 是否案件备份管理服务器 */
    BAK_SERVICE_TYPE = 311,      /**< 备份业务类型 */
    BAK_RES_SERVICE_TYPE = 312,      /**< 备份资源业务类型 */
    DOMAIN_CODE = 350,      /**< 域编码 */
    TOLLGATE_CODE = 351,      /**< 卡口编号 */
    TOLLGATE_NAME = 352,      /**< 卡口名称 */
    TOLLGATE_CAMERA_CODE = 353,      /**< 卡口相机编码 */
    LANE_NUMBER = 354,      /**< 车道编号 */
    LANE_DIRECTION = 355,      /**< 车道方向编号 */
    PASS_TIME = 356,      /**< 通过时间 */
    VEHICLE_LICENSE_PLATE = 357,      /**< 号牌号码 */
    VEHICLE_LICENSE_PLATE_TYPE = 358,      /**< 号牌种类 */
    VEHICLE_LICENSE_PLATE_COLOR = 359,      /**< 号牌颜色 */
    SPEED_TYPE = 360,      /**< 速度 */
    LIMIT_SPEED = 361,      /**< 限速 */
    VEHICLE_LOGO = 362,      /**< 车辆品牌 */
    VEHICLE_TYPE = 363,      /**< 车辆类型 */
    VEHICLE_COLOR_DEPTH = 364,      /**< 车身颜色深浅 */
    VEHICLE_COLOR = 365,      /**< 车身颜色 */
    VEHICLE_IDENTIFY_STATUS = 366,      /**< 识别状态 */
    VEHICLE_STATUS = 367,      /**< 行驶状态 */
    VEHICLE_DEAL_STATUS = 368,      /**< 车辆处理标记 */
    DISPOSITION_CODE = 369,      /**< 布控编号 */
    DISPOSITION_TYPE = 370,      /**< 布控类别 */
    DISPOSITION_DEPT = 371,      /**< 布控单位 */
    DISPOSITION_USER = 372,      /**< 布控人员 */
    DISPOSITION_STATUS = 373,      /**< 布控状态 */
    DISPOSITION_UNDO_DEPT = 374,      /**< 撤控单位 */
    DISPOSITION_UNDO_USER = 375,      /**< 撤控人员 */
    PRIORITY_TYPE = 376,      /**< 优先级 */
    ALARM_TYPE = 377,      /**< 告警类型 */
    PLATE_ANALYSE_STATUS = 378,      /**< 套牌分析状态 */
    SECTION_CODE = 379,      /**< 测速区间编号 */
    SECTION_NAME = 380,      /**< 测速区间名称 */
    TOLLGATE_CODE_IN = 381,      /**< 驶入卡口 */
    TOLLGATE_CODE_OUT = 382,      /**< 驶出卡口 */
    VEHICLE_DATA_TYPE = 383,      /**< 过车数据类型 */
    PASS_TIME2 = 384,      /**< 通过时间2 */
    DISPOSITION_RESULT = 385,      /**< 布控处理结果 */
    EXT_DOMAIN_PROTOCOL_TYPE = 386,      /**< 域间通讯协议类型 */
    DISTANCE_RANGE = 387,      /**< 距离范围 */
    DEFAULT_WEBGIS_MAP = 388,      /**< 默认WebGis地图 */
    DEFAULT_WEBGIS_POINT = 389,      /**< 默认WebGis地图关注点 */
    STAT_TIME = 390,      /**< 统计时间 */

    PLACE_CODE = 501,      /**< 违章地点编码 */
    EQUIPMENT_TYPE = 502,      /**< 采集类型 */
    ABNORMAL_ANALYSE_STATUS = 503,      /**< 异常行为分析状态 */
    COUNT_TYPE = 504,      /**< 次数 */
    RESTRICT_TYPE = 505,      /**< 限行方式 */
    VEHICLE_DATA_CODE = 506,      /**< 车辆信息编号 */
    COMBINE_FLAG = 507,      /**< 图片合成标识 */

    VEHICLE_NEW_LOGO = 519,  /**< 车标 */
    VEHICLE_LINE = 517,  /**< 车系 */
    VEHICLE_YEAR = 518,  /**< 年份 */

    DRIVER_SUN_VISOR_STATUS = 570,  /**<正驾驶遮阳板>*/
    CODRIVER_SUN_VISOR_STATUS = 571, /**<副驾驶遮阳板>*/
    DRIVER_SEAT_BELT_STATUS = 572, /**<正驾驶安全带>*/
    CODRIVER_SEAT_BELT_STATUS = 573, /**<副驾驶安全带>*/
    DRIVER_MOBIL_STATUS = 574, /**<正驾驶开车打电话>*/

    DEVICE_CODE = 580,    /**<设备编码(rfid编码)>*/
    MAC_ADDR = 581,    /**<MAC地址>*/
    DISPOSITION_MACRFID_TYPE = 582,    /**<MAC&RFID布控类型>*/
    DISPOSITION_MACRFID_STATUS = 583,    /**<MAC&RFID布控状态>*/
    MAC_DISPOSE_REASON = 584,    /**<MAC布控原因>*/
    RFID_DISPOSE_REASON = 585,    /**<RFID布控原因>*/
    START_TIME = 586,             /**< 开始时间 */
    END_TIME = 587,             /**< 结束时间 */
    IDENTIFY_INFO = 579,   /**<身份信息>*/


    MAC_PLACE_CODE = 600,
    MAC_PLACE_NAME = 601,
    MAC_BOSS_NAME = 602,
    MAC_PLACE_FULL_NAME = 603,

    MAC_MANU_NAME = 604,
    MAC_MANU_ORG_CODE = 605,
    MAC_CONTACT_PEOPLE = 606,

    QUERY_IOT_TYPE = 607,

    QUERY_DANGEROUS_GOODS = 608,   /**<危险品>*/
    QUERY_YELLOW_CAR = 609,        /**<黄标车>*/
    QEURY_ANNUAL_SURVEY = 610,     /**<年检贴>*/
    QUERY_PENDENT = 611,           /**<挂坠>*/
    IS_DISTINCT = 612,           /**<去重>*/

    NAPKIN_BOX_STATUS = 634,    /**<纸巾盒>*/

    QUERY_TYPE_MAX          /**< 无效值 */
}

/// <summary>
/// 查询逻辑条件
/// </summary>
public enum LOGIC_FLAG_E
{
    EQUAL_FLAG,     //等于  
    GREAT_FLAG,     //大于  
    LITTLE_FLAG,    //小于  
    GEQUAL_FLAG,    //大于等于  
    LEQUAL_FLAG,    //小于等于  
    LIKE_FLAG,      //模糊查询  
    ASCEND_FLAG,    //升序  
    DESCEND_FLAG,   //降序  

    /* added by kangshunli for MPPD17836 */
    /// <summary>
    /// /*不等于 */
    /// </summary>
    NEQUAL_FLAG = 8,
    /// <summary>
    /// /*NOT LIKE */
    /// </summary>
    NLIKE_FLAG = 13,
    /// <summary>
    /// in 99
    /// </summary>
    IN_FLAG = 99,
    /* added by kangshunli for MPPD17836 */
    LOGIC_FLAG_MAX  //无效值  
}
/**
* @enum tagDownMediaSpeed
* @brief 媒体数据下载速度的枚举定义
* @attention 无
*/
public enum XP_DOWN_MEDIA_SPEED_E
{
    XP_DOWN_MEDIA_SPEED_ONE = 0,            /**< 一倍速下载媒体文件 */
    XP_DOWN_MEDIA_SPEED_TWO = 1,            /**< 二倍速下载媒体文件 */
    XP_DOWN_MEDIA_SPEED_FOUR = 2,           /**< 四倍速下载媒体文件 */
    XP_DOWN_MEDIA_SPEED_EIGHT = 3           /**< 八倍速下载媒体文件 */
}


/// <summary>
/// 通用的云台操作枚举值
/// </summary>
public enum PTZ_CMD_E
{
    PTZ_UP,
    PTZ_UP_STOP,
    PTZ_DOWN,
    PTZ_DOWN_STOP,
    PTZ_LEFT,
    PTZ_LEFT_STOP,
    PTZ_RIGHT,
    PTZ_RIGHT_STOP,
    PTZ_LEFT_UP,
    PTZ_LEFT_UP_STOP,
    PTZ_LEFT_DOWN,
    PTZ_LEFT_DOWN_STOP,
    PTZ_RIGHT_UP,
    PTZ_RIGHT_UP_STOP,
    PTZ_RIGHT_DOWN,
    PTZ_RIGHT_DOWN_STOP,

    PTZ_ALL_STOP,           /**< 全停 */

    PTZ_IRIS_OPEN,          /**< 光圈开 */
    PTZ_IRIS_OPEN_STOP,
    PTZ_IRIS_CLOSE,         /**< 光圈关 */
    PTZ_IRIS_CLOSE_STOP,

    PTZ_FOCUS_FAR,          /**< 聚焦远 */
    PTZ_FOCUS_FAR_STOP,
    PTZ_FOCUS_NEAR,         /**< 聚焦近 */
    PTZ_FOCUS_NEAR_STOP,

    PTZ_ZOOM_TELE,          /**< 放大 */
    PTZ_ZOOM_TELE_STOP,
    PTZ_ZOOM_WIDE,          /**< 缩小 */
    PTZ_ZOOM_WIDE_STOP,

    PTZ_PRE_SAVE,           /**< 保存预置位 */
    PTZ_PRE_CALL,           /**< 调用预置位 */
    PTZ_PRE_DELETE,         /**< 删除预置位 */

    PTZ_BRUSH_ON,           /**< 雨刷开 */
    PTZ_BRUSH_OFF,
    PTZ_LIGHT_ON,           /**< 灯光开 */
    PTZ_LIGHT_OFF,
    PTZ_HEAT_ON,            /**< 加热开 */
    PTZ_HEAT_OFF,

    PTZ_CRUISE_START,       /**< 启动巡航 */
    PTZ_CRUISE_STOP,        /**< 停止巡航 */
}

/// <summary>
/// 告警推送类型
/// </summary>
public enum SUBSCRIBE_PUSH_TYPE_E
{
    SUBSCRIBE_PUSH_TYPE_ALL,         //接受告警推送和设备状态推送  
    SUBSCRIBE_PUSH_TYPE_ALARM,       //只接收告警推送  
    SUBSCRIBE_PUSH_TYPE_ALARM_STATUS,//只接收设备状态推送  
    SUBSCRIBE_PUSH_TYPE_MAX,         //   
    SUBSCRIBE_PUSH_TYPE_INVALID
}

/** 回调函数处理信息类型 */
public enum CALL_BACK_PROC_TYPE_E : uint
{
    PROC_TYPE_DEV_STATUS = 0,            /**< 设备状态，对应结构 : AS_STAPUSH_UI_S */
    PROC_TYPE_ALARM = 1,            /**< 告警，对应结构 : AS_ALARMPUSH_UI_S */
    PROC_TYPE_DEV_CAM_SHEAR = 2,            /**< 共享摄像机，对应结构 : AS_DEVPUSH_UI_S */
    PROC_TYPE_MONITOR_BE_REAVED = 3,            /**< 实况被抢占，对应结构 : CS_MONITOR_REAVE_NOTIFY_S */
    PROC_TYPE_SWITCH_BE_REAVED = 4,            /**< 轮切被抢占，对应结构 : CS_SWITCH_REAVE_NOTIFY_S */
    PROC_TYPE_MONITOR_BE_STOPPED = 5,            /**< 实况被停止，对应结构 : CS_MONITOR_REAVE_NOTIFY_S */
    PROC_TYPE_SWITCH_BE_STOPPED = 6,            /**< 轮切被停止，对应结构 : CS_SWITCH_REAVE_NOTIFY_S */
    PROC_TYPE_VOCSRV_BE_REAVED = 7,            /**< 语音被抢占，对应结构 : CS_VOCSRV_REAVE_NOTIFY_S */
    PROC_TYPE_PTZ_EVENT = 8,            /**< 云台事件通知，对应结构 : CS_PTZ_REAVE_NOTIFY_S */
    PROC_TYPE_TRB_PROC = 9,            /**< 故障处理通知，对应结构 : CS_NOTIFY_UI_TRB_EVENT_PROC_S */
    PROC_TYPE_SRV_SETUP = 10,           /**< 故障恢复业务建立通知，对应结构 : CS_NOTIFY_UI_SRV_SETUP_S */
    PROC_TYPE_XP_ALARM_SETUP = 11,           /**< 告警联动到XP窗格通知，对应结构 : CS_NOTIFY_UI_SRV_SETUP_S */

    PROC_TYPE_LOGOUT = 12,           /**< 退出登陆，对应结构 :无 */

    PROC_TYPE_MEDIA_PARAM_CHANGE = 13,           /**< 媒体参数改变，对应结构 : CS_NOTIFY_UI_MEDIA_PARAM_CHANGE_PROC_S */

    PROC_TYPE_EXDOMAIN_STATUS = 14,           /**< 外域状态，对应结构 : AS_EXDOMAIN_STAPUSH_UI_S */

    PROC_TYPE_BACKUP_DATA_FINISH = 15,           /**< 信息备份完成通知, 对应结构 : CS_BACKUP_FINISH_INFO_S */

    PROC_TYPE_TL_EVENT = 16,           /**< 透明通道事件通知，对应结构 : CS_TL_REAVE_NOTIFY_S */

    PROC_TYPE_SALVO_UNIT_EVENT = 17,           /**< 组显示单元事件通知, 对应结构 : CS_NOTIFY_SALVO_UNIT_EVENT_S */
    PROC_TYPE_SALVO_EVENT = 18,           /**< 组切业务事件通知, 对应结构 : CS_NOTIFY_UI_SALVO_EVENT_S */
    PROC_TYPE_START_XP_SALVO = 19,           /**< 通知UI启动组轮巡的组显示, 对应结构: CS_NOTIFY_START_XP_SALVO_S */

    PROC_TYPE_VODWALL_BE_REAVED = 20,           /**< 通知回放上墙被抢占，对应结构：CS_VODWALL_REAVE_NOTIFY_S */
    PROC_TYPE_VODWALL_BE_STOPPED = 21,           /**< 通知回放上墙被停止，对应结构：CS_VODWALL_REAVE_NOTIFY_S */

    PROC_TYPE_VOD_BE_REAVED = 22,           /**< 回放被抢占，对应结构 : CS_VOD_REAVE_NOTIFY_S */

    PROC_TYPE_DOMAIN_SYN_RESULT = 23,           /**< 域间同步的结果，对应结构 : AS_DOMAIN_SYN_PUSHUI_S */

    PROC_TYPE_VOC_REQ = 24,           /**< 客户端语音请求，对应结构 : CS_VOC_REQ_NOTIFY_S */
    PROC_TYPE_VOC_STATE_NOTIFY = 25,           /**< 语音业务状态通知，对应结构 : CS_VOC_STATE_NOTIFY_S */

    /*******************************************************************************
    SS新增定义 新增回调函数
    *******************************************************************************/
    PROC_TYPE_ACCEPT_SPEAK_YESORNO = 100,           /**< 发言申请， 对应结构 ：CONF_SITE_INFO_EX_S */
    PROC_TYPE_CONF_STATUS_CHANGE = 101,           /**< 会议状态改变， 对应结构 ：CONF_STATUS_INFO_EX_S 如果是周期会议且非最后一个周期，上报会议未开始/其它上报会议已经结束 */
    PROC_TYPE_DEVICE_CODE_CHANGE = 102,           /**< 设备编码改变， 对应结构 ：DEVICE_CODE_CHANGE_INFO_EX_S */
    PROC_TYPE_DEVICE_CHANGE = 103,           /**< 终端设备更新信息， 当存在设备新增、删除时， 上报更新消息， 对应的结构 ：DEVICE_CHANGE_INFO_EX_S */
    PROC_TYPE_MODIFY_TERM = 104,           /**< 修改终端消息， 对应的结构 ：MODIFY_TERM_REP_EX_S */
    PROC_TYPE_CHAIR_CHANGE = 105,           /**< 当前主席发生改变， 主席会场释放则会场编码为空。对应的结构 ：CONF_SITE_INFO_EX_S */
    PROC_TYPE_SPEAKER_CHANGE = 106,           /**< 当前发言人发生改变， 对应的结构 ：CONF_SITE_INFO_EX_S */
    PROC_TYPE_TERM_STATUS_CHANGE = 107,           /**< 会场终端状态改变， 对应的结构 ：TERM_STATUS_CHANGE_EX_S */
    PROC_TYPE_DELAY_CONF = 108,           /**< 延迟会议， 对应结构 ：DELAY_CONF_INFO_EX_S */
    PROC_TYPE_SYNCHRONIZE_WITH_WEB = 109,           /**< 上报广播会场， 主席、主场观看会场 对应的结构 ： MC_SYNCHRONIZE_WITH_WEB_EX_S  */
    PROC_TYPE_MCU_BACKUP_CHANGE_TO_MASTER = 110,    /**< MCU备份通知， 对应结构 ：BACKUP_MCU_REPORT_S  */
    PROC_TYPE_SEND_ROLE_SITE_CHANGE = 111,           /**< 当前辅流发送者变化通知， 对应结构 ：CONF_SEND_ROLE_SITE_CHANGE_S  */
    PROC_TYPE_AUTOMULTIPIC_CHANGE = 112,           /**< 多画面自动切换值改变通知， 对应结构 ：CONF_AUTOMULTIPIC_CHANGE_S  */
    PROC_TYPE_SET_TURN_BROADCAST_CHANGE = 113,       /**< 设置画面轮询模式改变通知， 对应结构 ：CONF_SET_TURN_BROADCAST_CHANGE_S */
    PROC_TYPE_SET_PIC_MODE_CHANGE = 114,           /**< 设置画面模式改变通知， 对应结构 ：CONF_SET_PIC_MODE_CHANGE_S */
    PROC_TYPE_MCU_SYNC_STATUS_CHANGE = 115,          /**< MCU同步状态改变通知， 对应结构 ：MCU_SYNC_STATUS_CHANGE_S */
    PROC_TYPE_CUR_BROADCAST_CHANGE = 116,          /**< 当前实际广播会场改变通知，对应结构：CUR_BROADCAST_INFO_EX_S */
    PROC_TYPE_CUR_CHAIR_BROWSE_CHANGE = 117,     /**< 当前主席或主场实际观看的会场改变通知，对应结构：CUR_CHAIR_BROWSE_INFO_EX_S */
    PROC_TYPE_CONF_FECC_CHANGE = 118,          /**< 当前FECC控制者或被控者变化通知，对应结构：CONF_FECC_CHANGE_S */
    PROC_TYPE_CONF_MCU_BACKUP_CHANGE = 119,     /**< 当前会议中MCU备份变化通知，对应结构：CONF_MCU_BACKUP_CHANGE_S */
    PROC_TYPE_CALL_SITE_RESULT = 120,          /**< 呼叫会场结果通知，对应结构：CALL_SITE_INFO_EX_S */
    PROC_TYPE_GK_REG_STATE = 121,          /**< GK注册结果通知，对应结构：GK_REG_STATE_INFO_EX_S */
    PROC_TYPE_MG_SESSION_STATUS_CHANGE = 122,     /**< 终端会话状态，对应结构：MG_SESSION_STATUS_EX_S */
    PROC_TYPE_MAX,                                   /**< 回调函数处理信息类型最大值 */
    PROC_TYPE_INVALID = 0xFFFFFFFE    /**< 无效值 */
}




/**
* @enum tagRunInfoType
* @brief 上报消息类型的枚举定义
* @attention 无
*/
public enum XP_RUN_INFO_TYPE_E
{
    XP_RUN_INFO_SERIES_SNATCH = 0,        /**< 连续抓拍过程中上报运行信息 */
    XP_RUN_INFO_RECORD_VIDEO = 1,         /**< 本地录像过程中上报运行信息 */
    XP_RUN_INFO_MEDIA_PROCESS = 2,        /**< 视频媒体处理过程中的上报运行信息 */
    XP_RUN_INFO_DOWN_MEDIA_PROCESS = 3,   /**< 媒体流下载过程中上报运行信息 */
    XP_RUN_INFO_VOICE_MEDIA_PROCESS = 4,  /**< 语音媒体处理过程中的上报运行信息 */
    XP_RUN_INFO_RTSP_PROTOCOL = 5,        /**< RTSP协议组件运行的错误信息 */
    XP_RUN_INFO_DOWN_RTSP_PROTOCOL = 6,   /**< 下载录像过程中RTSP协议的错误信息 */
    XP_RUN_INFO_SIP_LIVE_TIMEOUT = 7,     /**< SIP注册保活超时 */
    XP_RUN_INFO_PASSIVE_MONITOR = 8,      /**< 被动实况停止操作信息 */
    XP_RUN_INFO_PASSIVE_START_MONITOR = 9,/**< 被动实况启动操作信息 */
    XP_RUN_INFO_MEDIA_NOT_IDENTIFY = 10,  /**< 码流无法识别 */
    XP_RUN_INFO_RECV_PACKET_NUM = 11,     /**< 周期内接收到的包数 */
    XP_RUN_INFO_RECV_BYTE_NUM = 12,       /**< 周期内接收到的字节数 */
    XP_RUN_INFO_VIDEO_FRAME_NUM = 13,     /**< 周期内解析的视频帧数 */
    XP_RUN_INFO_AUDIO_FRAME_NUM = 14,     /**< 周期内解析的音频帧数 */
    XP_RUN_INFO_LOST_PACKET_RATIO = 15,   /**< 周期内丢包率统计信息（单位为0.01%） */
    XP_RUN_INFO_MEDIA_PLAY_PROGRESS = 16, /**< 媒体中携带的进度信息 */
    XP_RUN_INFO_MEDIA_PLAY_END = 17,      /**< 媒体中携带的播放结束 */
    XP_RUN_INFO_MEDIA_ABNORMAL = 18       /**< 媒体处理异常 */
}

public enum MW_PTZ_CMD_E
{
    MW_PTZ_IRISCLOSESTOP = 0x0101, /**< 光圈关停止 */
    MW_PTZ_IRISCLOSE = 0x0102,         /**< 光圈关 */
    MW_PTZ_IRISOPENSTOP = 0x0103,   /**< 光圈开停止 */
    MW_PTZ_IRISOPEN = 0x0104,   /**< 光圈开 */

    MW_PTZ_FOCUSNEARSTOP = 0x0201, /**< 近聚集停止 */
    MW_PTZ_FOCUSNEAR = 0x0202,    /**< 近聚集 */
    MW_PTZ_FOCUSFARSTOP = 0x0203,/**< 远聚集 停止*/
    MW_PTZ_FOCUSFAR = 0x0204,        /**< 远聚集 */

    MW_PTZ_ZOOMTELESTOP = 0x0301,/**< 放大停止 */
    MW_PTZ_ZOOMTELE = 0x0302,/**< 放大 */
    MW_PTZ_ZOOMWIDESTOP = 0x0303,/**< 缩小停止 */
    MW_PTZ_ZOOMWIDE = 0x0304,/**< 缩小 */

    MW_PTZ_TILTUPSTOP = 0x0401,/**< 向上停止 */
    MW_PTZ_TILTUP = 0x0402,/**< 向上 */
    MW_PTZ_TILTDOWNSTOP = 0x0403,/**< 向下停止 */
    MW_PTZ_TILTDOWN = 0x0404,/**< 向下 */

    MW_PTZ_PANRIGHTSTOP = 0x0501,/**< 向右停止 */
    MW_PTZ_PANRIGHT = 0x0502,/**< 向右 */
    MW_PTZ_PANLEFTSTOP = 0x0503,/**< 向左停止 */
    MW_PTZ_PANLEFT = 0x0504,/**< 向左 */

    MW_PTZ_PRESAVE = 0x0601,/**< 预置位保存 */
    MW_PTZ_PRECALL = 0x0602,/**< 预置位调用 */
    MW_PTZ_PREDEL = 0x0603,/**< 预置位删除 */

    MW_PTZ_LEFTUPSTOP = 0x0701,/**< 左上停止 */
    MW_PTZ_LEFTUP = 0x0702,/**< 左上 */
    MW_PTZ_LEFTDOWNSTOP = 0x0703,/**< 左下停止 */
    MW_PTZ_LEFTDOWN = 0x0704,/**< 左下 */

    MW_PTZ_RIGHTUPSTOP = 0x0801,/**< 右上停止 */
    MW_PTZ_RIGHTUP = 0x0802,/**< 右上 */
    MW_PTZ_RIGHTDOWNSTOP = 0x0803,/**< 右下停止 */
    MW_PTZ_RIGHTDOWN = 0x0804,/**< 右下 */

    MW_PTZ_ALLSTOP = 0x0901,/**< 全停命令字 */

    MW_PTZ_BRUSHON = 0x0A01,/**< 雨刷开 */
    MW_PTZ_BRUSHOFF = 0x0A02,/**< 雨刷关 */

    MW_PTZ_LIGHTON = 0x0B01,/**< 灯开 */
    MW_PTZ_LIGHTOFF = 0x0B02,/**< 灯关 */

    MW_PTZ_HEATON = 0x0C01,/**< 加热开 */
    MW_PTZ_HEATOFF = 0x0C02,/**< 加热关 */

    MW_PTZ_INFRAREDON = 0x0D01,/**< 红外开 */
    MW_PTZ_INFRAREDOFF = 0x0D02,/**< 红外关 */

    MW_PTZ_SCANCRUISE = 0x0E01,/**< 云台线性扫猫 */
    MW_PTZ_SCANCRUISESTOP = 0x0E02,/**< 云台线性扫猫 */

    MW_PTZ_TRACKCRUISE = 0x0F01,/**<  云台轨迹巡航 */
    MW_PTZ_TRACKCRUISESTOP = 0x0F02,/**<  云台轨迹巡航 */

    MW_PTZ_PRESETCRUISE = 0x1001,/**<  云台按预置位巡航 ，该命令字不在云台模板体现 */
    MW_PTZ_PRESETCRUISESTOP = 0x1002,/**<  云台按预置位巡航 停止，该命令字不在云台模板体现 */

    PTZ_RELEASE,            /**< 释放云台 */
    PTZ_LOCK,               /**< 锁定云台 */
    PTZ_UNLOCK,             /**< 解锁云台 */
    MW_PTZ_CMD_BUTT

}


public enum IMOS_TYPE_E
{
    IMOS_TYPE_ORG = 1,                     /**< 组织域 */
    IMOS_TYPE_OUTER_DOMAIN = 2,            /**< 外域 */
    IMOS_TYPE_LOCAL_DOMAIN = 3,            /**< 本域 */

    IMOS_TYPE_DM = 11,                     /**< DM */
    IMOS_TYPE_MS = 12,                     /**< MS */
    IMOS_TYPE_VX500 = 13,                  /**< VX500 */
    IMOS_TYPE_MONITOR = 14,                /**< 监视器 */

    IMOS_TYPE_EC = 15,                     /**< EC */
    IMOS_TYPE_DC = 16,                     /**< DC */

    IMOS_TYPE_GENERAL = 17,                /**< 通用设备 */

    IMOS_TYPE_MCU = 201,                   /**< MCU */
    IMOS_TYPE_MG = 202,                    /**< MG */

    IMOS_TYPE_CAMERA = 1001,               /**< 摄像机 */
    IMOS_TYPE_ALARM_SOURCE = 1003,         /**< 告警源 */

    IMOS_TYPE_STORAGE_DEV = 1004,          /**< 存储设备 */
    IMOS_TYPE_TRANS_CHANNEL = 1005,        /**< 透明通道 */

    IMOS_TYPE_ALARM_OUTPUT = 1200,         /**< 告警输出 */

    IMOS_TYPE_GUARD_TOUR_RESOURCE = 2001,  /**< 轮切资源 */
    IMOS_TYPE_GUARD_TOUR_PLAN = 2002,      /**< 轮切计划 */
    IMOS_TYPE_MAP = 2003,                  /**< 地图 */

    IMOS_TYPE_XP = 2005,                   /**< XP */
    IMOS_TYPE_XP_WIN = 2006,               /**< XP窗格 */
    IMOS_TYPE_GUARD_PLAN = 2007,           /**< 布防计划 */

    IMOS_TYPE_DEV_ALL = 2008,              /**< 所有的设备类型(EC/DC/MS/DM/VX500/摄像头/监视器) */
    IMOS_TYPE_TV_WALL = 3001,              /**< 电视墙 */

    IMOS_TYPE_CONFERENCE = 4001,           /**< 会议资源 */

    IMOS_TYPE_MAX
}


/**
  * @struct tagUserLoginIDInfo
  * @brief 用户登录ID信息结构
  * @attention
  */
[StructLayout(LayoutKind.Sequential)]
public struct USER_LOGIN_ID_INFO_S
{
    /** 用户编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_USER_CODE_LEN)]
    public byte[] szUserCode;

    /** 用户登录ID，是用户登录后服务器分配的，它是标记一次用户登录的唯一标识 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szUserLoginCode;

    /** 用户登录的客户端IP地址 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szUserIpAddress;
}

/// <summary>
/// 获取云录像URL的请求结构
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct UNITED_GET_URL_INFO_S
{
    /** 摄像机编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
    public byte[] szCamCode;

    /** 录像文件名 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_FILE_NAME_LEN_V2)]
    public byte[] szFileName;

    /** 录像的起始/结束时间, 其中的时间格式为"YYYY-MM-DD hh:mm:ss" */
    public TIME_SLICE_S stRecTimeSlice;

    /** 客户端IP地址 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szClientIp;

    /** 回放中继策略#IMOS_VOD_STREAM_SERVER_MODE_E */
    public uint ulPlaybackAutofit;

    /** 域级别计数: 0xFFFF为无效的域级别计数 */
    public uint ulDomainLevel;

    /** 备份业务类型，取值#BAK_TASK_SERVICE_TYPE_E */
    public uint ulBakSrvType;

    /** 案件录像编码，备份业务类型为案件备份时有效 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
    public byte[] szCaseRecCode;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 76)]
    public byte[] szReserve;

    public static UNITED_GET_URL_INFO_S GetInstance()
    {
        UNITED_GET_URL_INFO_S tmp = new UNITED_GET_URL_INFO_S();
        tmp.szCamCode = new byte[IMOSSDK.IMOS_CODE_LEN];
        tmp.szFileName = new byte[IMOSSDK.IMOS_FILE_NAME_LEN_V2];
        tmp.stRecTimeSlice = TIME_SLICE_S.GetInstance();

        tmp.szClientIp = new byte[IMOSSDK.IMOS_IPADDR_LEN];
        tmp.szCaseRecCode = new byte[IMOSSDK.IMOS_CODE_LEN];
        tmp.szReserve = new byte[76];

        return tmp;
    }
}

/**
* @struct tagLoginInfo
* @brief 用户登录信息结构体
* @attention 无
*/
[StructLayout(LayoutKind.Sequential)]
public struct LOGIN_INFO_S
{
    /** 用户登录ID信息 */
    public USER_LOGIN_ID_INFO_S stUserLoginIDInfo;

    /** 用户所属组织编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
    public byte[] szOrgCode;

    /** 用户所属域名称 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szDomainName;

    /** 用户所属域类型, 取值为#MM_DOMAIN_SUBTYPE_LOCAL_PHYSICAL和#MM_DOMAIN_SUBTYPE_LOCAL_VIRTUAL */
    public UInt32 ulDomainType;
}

/**
* @struct tagXpInfo
* @brief XP信息结构体
* @attention 无
*/
[StructLayout(LayoutKind.Sequential)]
public struct XP_INFO_S
{
    /** XP编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] szXpCode;

    /** 屏号 */
    public UInt32 ulScreenIndex;

    /** XP第一窗格编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szXpFirstWndCode;

    /** 语音对讲编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szVoiceTalkCode;

    /** 语音广播编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szVoiceBroadcastCode;

    /** SIP通信地址类型，#IMOS_IPADDR_TYPE_IPV4为IPv4类型; #IMOS_IPADDR_TYPE_IPV6为IPv6类型 */
    public UInt32 ulSipAddrType;

    /** SIP服务器通信IP地址，仅在使用XP的时候有效 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szSipIpAddress;

    /** SIP服务器通信端口号 */
    public UInt32 ulSipPort;

    /** 本域服务器编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] szServerCode;

}

/**
 * @struct tagQueryConditionItem
 * @brief 查询条件项
 * @attention
 */
[StructLayout(LayoutKind.Sequential)]
public struct QUERY_CONDITION_ITEM_S
{
    /** 查询条件类型: #QUERY_TYPE_E */
    public UInt32 ulQueryType;

    /** 查询条件逻辑关系类型: #LOGIC_FLAG_E */
    public UInt32 ulLogicFlag;

    /** 查询条件 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_QUERY_DATA_MAX_LEN)]
    public byte[] szQueryData;
}


/**
 * @struct tagCommonQueryCondition
 * @brief 通用查询条件
 * @attention
*/
[StructLayout(LayoutKind.Sequential)]
public struct COMMON_QUERY_CONDITION_S
{
    /** 查询条件数组中查询条件的实际个数, 最大取值为#IMOS_QUERY_ITEM_MAX_NUM */
    public UInt32 ulItemNum;

    /** 查询条件数组 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_QUERY_ITEM_MAX_NUM)]
    public QUERY_CONDITION_ITEM_S[] astQueryConditionList;
}

/**
 * @struct tagQueryPageInfo
 * @brief 分页请求信息
 * @brief 待查询数据的每条数据项对应一个序号。序号从1开始，连续增加。
 * - 查询到的结果以页的形式返回，每次查询只能返回一页。页包含的行数由ulPageRowNum设定，范围为1~200。
 * - 每次查询，可设置从待查询数据中特定序号（ulPageFirstRowNumber）开始
 * @attention
 */
[StructLayout(LayoutKind.Sequential)]
public struct QUERY_PAGE_INFO_S
{
    /** 分页查询中每页的最大条目数, 不能为0, 也不能大于#IMOS_PAGE_QUERY_ROW_MAX_NUM */
    public UInt32 ulPageRowNum;

    /** 分页查询中第一条数据的序号(即查询从第ulPageFirstRowNumber条数据开始的符合条件的数据), 取值符合ULONG类型的范围即可 */
    public UInt32 ulPageFirstRowNumber;

    /** 是否查询条目总数, BOOL_TRUE时查询; BOOL_FALSE时不查询 */
    public UInt32 bQueryCount;
}

/**
 * @struct tagRspPageInfo
 * @brief 分页响应信息
 * @attention
 */
[StructLayout(LayoutKind.Sequential)]
public struct RSP_PAGE_INFO_S
{
    /** 实际返回的条目数 */
    public UInt32 ulRowNum;

    /** 符合条件的总条目数 */
    public UInt32 ulTotalRowNum;
}

/**
 * @struct tagOrgResQueryItem
 * @brief 组织节点下资源信息项(查询资源列表时返回)
 * @attention
 */
[StructLayout(LayoutKind.Sequential)]
public struct ORG_RES_QUERY_ITEM_S
{
    /** 资源编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szResCode;

    /** 资源名称 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szResName;

    /** 资源类型，取值范围为#IMOS_TYPE_E */
    public UInt32 ulResType;

    /** 资源子类型,目前资源子类型只对摄像机和组织有效，对摄像机而言为云台/非云台;
        对组织而言为:1-本物理域，2-本域的虚拟域，3-外域的虚拟域. 4-上级外物理域.
        5-下级外物理域.6-平级外物理域. */
    public UInt32 ulResSubType;

    /** 资源状态，目前只针对物理设备和外域，对外域来说, 该字段代表接收注册状态，取值为
        #IMOS_DEV_STATUS_ONLINE或#IMOS_DEV_STATUS_OFFLINE */
    public UInt32 ulResStatus;

    /** 资源额外状态，对物理设备来说，枚举为#DEV_EXT_STATUS_E; 对外域来说, 该字段代表主动注册状态:
        取值为#IMOS_DEV_STATUS_ONLINE或#IMOS_DEV_STATUS_OFFLINE */
    public UInt32 ulResExtStatus;

    /** 该资源是否是被划归的资源, 1为被划归的资源; 0为非划归的资源 */
    public UInt32 ulResIsBeShare;

    /** 资源所属组织编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
    public byte[] szOrgCode;

    /** 支持的流数目，仅当资源类型为摄像机时有效，0:无效值，1:单流，2:双流 */
    public UInt32 ulStreamNum;

    /** 是否为外域资源，1为外域资源; 0为非外域资源 */
    public UInt32 ulResIsForeign;

}

public struct PRESET_INFO_S
{
    /** 预置位值, 取值范围为#PTZ_PRESET_MINVALUE~服务器配置文件里配置的预置位最大值 */
    public UInt32 ulPresetValue;

    /** 预置位描述, 需要填写 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
    public byte[] szPresetDesc;
}

/**
 * @struct tagPTZCtrlCommand
 * @brief 云台控制指令
 * @attention
 */
[StructLayout(LayoutKind.Sequential)]
public struct PTZ_CTRL_COMMAND_S
{
    /** 云台控制命令类型, 取值为#MW_PTZ_CMD_E */
    public UInt32 ulPTZCmdID;

    /** 云台横向转速 */
    public UInt32 ulPTZCmdPara1;

    /** 云台纵向变速 */
    public UInt32 ulPTZCmdPara2;

    /** 控制命令的参数值,保留字段 */
    public UInt32 ulPTZCmdPara3;

}

[StructLayout(LayoutKind.Sequential)]
public struct PLAY_WND_INFO_S
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szPlayWndCode;
}

[StructLayout(LayoutKind.Sequential)]
public struct TIME_SLICE_S
{
    /** 开始时间 格式为"hh:mm:ss"或"YYYY-MM-DD hh:mm:ss", 视使用情况而定 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
    public byte[] szBeginTime;

    /** 结束时间 格式为"hh:mm:ss"或"YYYY-MM-DD hh:mm:ss", 视使用情况而定 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
    public byte[] szEndTime;

    public static TIME_SLICE_S GetInstance()
    {
        TIME_SLICE_S stTimeSlice = new TIME_SLICE_S();
        stTimeSlice.szBeginTime = new byte[IMOSSDK.IMOS_TIME_LEN];
        stTimeSlice.szEndTime = new byte[IMOSSDK.IMOS_TIME_LEN];

        return stTimeSlice;
    }
}

/// 备份任务业务类型
/// </summary>
public enum BAK_TASK_SERVICE_TYPE_E
{
    BAK_TASK_SERVICE_TYPE_GENERAL = 0,    /**< 一般业务 */
    BAK_TASK_SERVICE_TYPE_CASE = 1,    /**< 案件备份业务 */

    BAK_TASK_SERVICE_TYPE_MAX
}

[StructLayout(LayoutKind.Sequential)]
public struct REC_QUERY_INFO_S
{
    /** 摄像头编码*/
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szCamCode;

    /** 检索的起始/结束时间 */
    public TIME_SLICE_S stQueryTimeSlice;

    /* 录像的域级别计数: 用于国标协议跨域回放 */
    public UInt32 uiDomainLevel;

    /* Begin add by zhengyibing(01306), 2014-04-19 for 新国标修订*/
    /* 新增模糊查询类型  #INDISTINCT_QUERY_TYPE_E */
    public UInt32 uiIndistinctQuery;

    /* 新增录像检索类型  #RECORD_QUERY_TYPE_E */
    public UInt32 uiType;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public byte[] szReserve;
}

[StructLayout(LayoutKind.Sequential)]
public struct RECORD_FILE_INFO_S
{
    /** 文件名 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_FILE_NAME_LEN)]
    public byte[] szFileName;

    /** 文件起始时间, 满足"%Y-%m-%d %H:%M:%S"格式, 长度限定为24字符 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
    public byte[] szStartTime;

    /** 文件结束时间, 满足"%Y-%m-%d %H:%M:%S"格式, 长度限定为24字符 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
    public byte[] szEndTime;

    /** 文件大小, 目前暂不使用 */
    public UInt32 ulSize;

    /** 描述信息, 可不填 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
    public byte[] szSpec;
}

[StructLayout(LayoutKind.Sequential)]
public struct GET_URL_INFO_S
{
    /** 摄像机编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szCamCode;

    /** 录像文件名 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_FILE_NAME_LEN)]
    public byte[] szFileName;

    /** 录像的起始/结束时间, 其中的时间格式为"YYYY-MM-DD hh:mm:ss" */
    public TIME_SLICE_S stRecTimeSlice;

    /** 客户端IP地址 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szClientIp;

    public static GET_URL_INFO_S GetInstance()
    {
        GET_URL_INFO_S tmp = new GET_URL_INFO_S();
        tmp.stRecTimeSlice = TIME_SLICE_S.GetInstance();
        tmp.szCamCode = new byte[IMOSSDK.IMOS_RES_CODE_LEN];
        tmp.szClientIp = new byte[IMOSSDK.IMOS_IPADDR_LEN];
        tmp.szFileName = new byte[IMOSSDK.IMOS_FILE_NAME_LEN];
        return tmp;
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct VOD_SEVER_IPADDR_S
{
    /** RTSP服务器IP地址 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szServerIp;

    /** RTSP服务器端口 */
    public UInt16 usServerPort;

    /** 补齐位, 用于字节对齐, 无实际含义 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public byte[] acReserved;
}

[StructLayout(LayoutKind.Sequential)]
public struct URL_INFO_S
{
    /** URL地址*/
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IE_URL_LEN)]
    public byte[] szURL;

    /** 点播服务器的IP地址和端口 */
    public VOD_SEVER_IPADDR_S stVodSeverIP;

    /** 解码插件类型 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
    public byte[] szDecoderTag;
}

[StructLayout(LayoutKind.Sequential)]
public struct EC_INFO_S
{
    /** EC编码, EC的唯一标识 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] szECCode;

    /** EC名称 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szECName;

    /** EC类型，取值为#IMOS_DEVICE_TYPE_E, 合法取值参见#ulChannum参数的说明 */
    public UInt32 ulECType;

    /** EC通道数量:
        几种常见EC类型对应的通道个数如下:
        EC1101(#IMOS_DT_EC1101_HF)/EC1001(#IMOS_DT_EC1001)/EC1001-HF(#IMOS_DT_EC1001_HF): 1
        EC1501(#IMOS_DT_EC1501_HF)/R1000 (#IMOS_DT_R1000) : 1
        EC2004(#IMOS_DT_EC2004_HF)/VR2004(#IMOS_DT_VR2004): 4
        EC1102(#IMOS_DT_EC1102_HF)/VR1102(#IMOS_DT_VR1102): 2
        EC1801(#IMOS_DT_EC1801_HH): 1
        EC2016(#IMOS_DT_EC2016_HC): 16
        EC2016[8CH](#IMOS_DT_EC2016_HC_8CH): 8
        EC2016[4CH](#IMOS_DT_EC2016_HC_4CH): 4
        HIC5201-H(#IMOS_DT_HIC5201)/HIC5221-H(#IMOS_DT_HIC5221): 1
    */
    public UInt32 ulChannum;

    /** 是否支持组播, 1为支持; 0为不支持 */
    public UInt32 ulIsMulticast;

    /** 低温告警温度上限, 取值为-100~49 */
    public Int32 lTemperatureMax;

    /** 高温告警温度下限, 取值为50~100 */
    public Int32 lTemperatureMin;

    /** 告警使能, 1为使能; 0为不使能 */
    public UInt32 ulEnableAlarm;

    /** EC所属组织编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
    public byte[] szOrgCode;

    /** 时间同步方式，默认为1，表示使用H3C的私有同步方式；2表示NTP的同步方式 */
    public UInt32 ulTimeSyncMode;

    /** 时区, 取值为-12~12 */
    public Int32 lTimeZone;

    /** 语言设置，由中心服务器来配置，取值为:#TD_LANGUAGE_E */
    public UInt32 ulLanguage;

    /** 是否启用本地缓存，1表示启用; 0表示不启动，默认值为0 */
    public UInt32 ulEnableLocalCache;

    /** 流套餐, 取值为:#IMOS_STREAM_RELATION_SET_E
        0：MPEG4+MPEG4(#IMOS_SR_MPEG4_MPEG4)
        1：H264主码流(#IMOS_SR_H264_SHARE)
        2：MPEG2+MPEG4(#IMOS_SR_MPEG2_MPEG4)
        3：H264+MJPEG(#IMOS_SR_H264_MJPEG)
        4：MPEG4主码流(#IMOS_SR_MPEG4_SHARE)
        5：MPEG2主码流(#IMOS_SR_MPEG2_SHARE)
        8: MPEG4主码流_D1(#IMOS_SR_STREAM_MPEG4_8D1)
        9：MPEG2+MPEG2(#IMOS_SR_MPEG2_MPEG2)
        11：H264+H264(#IMOS_SR_H264_H264)
    */
    public UInt32 ulEncodeSet;

    /** 制式, 取值为#IMOS_PICTURE_FORMAT_E */
    public UInt32 ulStandard;

    /** 音频输入源，取值为#IMOS_AUDIO_INPUT_SOURCE_E */
    public UInt32 ulAudioinSource;

    /** 语音对讲资源编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szAudioCommCode;

    /** 语音广播资源编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szAudioBroadcastCode;

    /** 设备访问密码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_PASSWD_ENCRYPT_LEN)]
    public byte[] szDevPasswd;

    /** 设备描述, 目前该字段未使用 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
    public byte[] szDevDesc;

    /** EC的IP地址, 添加及修改EC不需填写该参数, 查询EC信息时返回该字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szECIPAddr;

    /** EC的在线状态,添加及修改EC不需填写该参数, 查询EC信息时返回该字段, 1为在线; 0为离线 */
    public UInt32 ulIsECOnline;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)]
    public byte[] szReserve;
}

[StructLayout(LayoutKind.Sequential)]
public struct EC_QUERY_ITEM_S
{
    /** EC编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] szECCode;

    /** EC名称 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szECName;

    /** EC类型，取值为#IMOS_DEVICE_TYPE_E */
    public UInt32 ulECType;

    /** 设备地址类型，1-IPv4 2-IPv6 */
    public UInt32 ulDevaddrtype;

    /** 设备地址 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szDevAddr;

    /** 所属组织编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
    public byte[] szOrgCode;

    /** 设备是否在线取值为#IMOS_DEV_STATUS_ONLINE或#IMOS_DEV_STATUS_OFFLINE，在imos_def.h中定义 */
    public UInt32 ulIsOnline;

    /** 设备扩展状态，取值为#DEV_EXT_STATUS_E */
    public UInt32 ulDevExtStatus;

    /** 是否支持组播, 1为支持组播; 0为不支持组播 */
    public UInt32 ulIsMulticast;

    /** 告警使能, 1为使能告警; 0为不使能告警 */
    public UInt32 ulEnableAlarm;

    /** 流套餐类型，取值为#IMOS_STREAM_RELATION_SET_E */
    public UInt32 ulEncodeType;

    /** 制式，取值为#IMOS_PICTURE_FORMAT_E */
    public UInt32 ulStandard;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szReserve;
}

[StructLayout(LayoutKind.Sequential)]
public struct DC_INFO_S
{
    /** DC编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] szDCCode;

    /** DC名称 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szDCName;

    /** DC类型, 取值为#IMOS_DEVICE_TYPE_E, 合法取值参见#ulChannum参数的说明 */
    public UInt32 ulDCType;

    /** DC通道数量:
        几种常见DC类型对应的通道个数如下:
        DC1001(#IMOS_DT_DC1001): 1
        DC2004(#IMOS_DT_DC2004_FF)/VL2004(#IMOS_DT_VL2004): 4
        DC1801(#IMOS_DT_DC1801_FH): 1
    */
    public UInt32 ulChannum;

    /** 是否支持组播, 1为支持组播; 0为不支持组播 */
    public UInt32 ulIsMulticast;

    /** 低温告警温度上限, 取值为-100~49 */
    public Int32 lTemperatureMax;

    /** 高温告警温度下限, 取值为50~100 */
    public Int32 lTemperatureMin;

    /** 告警使能, 1为使能告警; 0为不使能告警 */
    public UInt32 ulEnableAlarm;

    /** 所属组织编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
    public byte[] szOrgCode;

    /** 时间同步方式，默认为1，表示使用H3C的私有同步方式；2表示NTP的同步方式 */
    public UInt32 ulTimeSyncMode;

    /** 时区, 取值为-12~12 */
    public Int32 lTimeZone;

    /** 语言设置，由中心服务器来配置，取值为:#TD_LANGUAGE_E */
    public UInt32 ulLanguage;

    /** 制式, 取值为#IMOS_PICTURE_FORMAT_E */
    public UInt32 ulStandard;

    /** 流套餐，取值为#IMOS_STREAM_RELATION_SET_E
        以下为解码器流套餐值：
        1：H264(#IMOS_SR_H264_SHARE)
        3: MJPEG(#IMOS_SR_H264_MJPEG)
        4：MEPG4(#IMOS_SR_MPEG4_SHARE)
        5：MEPG2(#IMOS_SR_MPEG2_SHARE)
    */
    public UInt32 ulEncodeSet;

    /** 设备访问密码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_PASSWD_ENCRYPT_LEN)]
    public byte[] szDevPasswd;

    /** 设备描述, 目前该字段未使用 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
    public byte[] szDevDesc;

    /** DC的IP地址,添加及修改DC不需填写该参数,查询DC信息时会返回该字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szDCIPAddr;

    /** EC的在线状态,添加及修改EC不需填写该参数, 查询EC信息时返回该字段, 1为在线; 0为离线 */
    public UInt32 ulIsDCOnline;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 96)]
    public byte[] szReserve;
}

[StructLayout(LayoutKind.Sequential)]
public struct DC_QUERY_ITEM_S
{
    /** DC编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] szDCCode;

    /** DC名称 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szDCName;

    /** DC类型，取值为#IMOS_DEVICE_TYPE_E */
    public UInt32 ulDCType;

    /** DC设备地址类型，1-IPv4 2-IPv6 */
    public UInt32 ulDevaddrtype;

    /** DC设备地址 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szDevAddr;

    /** DC所属组织编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DOMAIN_CODE_LEN)]
    public byte[] szOrgCode;

    /** 设备是否在线, 取值为#IMOS_DEV_STATUS_ONLINE或#IMOS_DEV_STATUS_OFFLINE，在imos_def.h中定义 */
    public UInt32 ulIsOnline;

    /** 设备扩展状态，枚举值为#DEV_EXT_STATUS_E */
    public UInt32 ulDevExtStatus;

    /** 是否支持组播, 1为支持组播; 0为不支持组播 */
    public UInt32 ulIsMulticast;

    /** 告警使能, 1为使能告警; 0为不使能告警 */
    public UInt32 ulEnableAlarm;

    /** 流套餐类型，取值为#IMOS_STREAM_RELATION_SET_E */
    public UInt32 ulEncodeType;

    /** 制式, 取值为#IMOS_PICTURE_FORMAT_E */
    public UInt32 ulStandard;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szReserve;
}

[StructLayout(LayoutKind.Sequential)]
public struct CAMERA_INFO_S
{
    /** 摄像机编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szCameraCode;

    /** 摄像机名称 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szCameraName;

    /** 摄像机类型, 取值为#CAMERA_TYPE_E */
    public UInt32 ulCameraType;

    /** 摄像机描述, 可不填 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
    public byte[] szCameraDesc;

    /** 云台控制协议, 目前支持的包括:PELCO-D, PELCO-P, ALEC, VISCA, ALEC_PELCO-D, ALEC_PELCO-P, MINKING_PELCO-D, MINKING_PELCO-P */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szPtzProtocol;

    /** 云台地址码, 取值为0~255, 具体取值视云台摄像机的实际地址码而定 */
    public UInt32 ulPtzAddrCode;

    /** 云台协议翻译模式,目前只能填写为#PTZ_TRANSLATE_EP(终端翻译模式) */
    public UInt32 ulPtzTranslateMode;

    /** 经度 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szLongitude;

    /** 纬度 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szLatitude;

    /** 看守位，与设定的预置位的编号相对应 */
    public UInt32 ulGuardPosition;

    /** 自动看守时间, 单位为秒, 最大不超过3600秒, 0表示不看守 */
    public UInt32 ulAutoGuard;

    /** 设备描述, 可不填 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
    public byte[] szDevDesc;

    /** EC编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] szECCode;

    /** EC的IP地址,在绑定及修改Camera时,不需填写,查询Camera信息时会返回该字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szECIPAddr;

    /** 所在EC通道索引号, 视具体情况而定 */
    public UInt32 ulChannelIndex;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szReserve;
}

/**
* @struct tagXpStreamInfo
* @brief XP实时监控流信息结构
* @attention
*/
[StructLayout(LayoutKind.Sequential)]
public struct XP_STREAM_INFO_S
{
    /** 支持的单组播类型，0为仅支持单播，1为既支持单播也支持组播 */
    public UInt32 ulStreamType;

    /** 支持的流传输协议 参见#IMOS_TRANS_TYPE_E。目前XP只支持自适应和TCP */
    public UInt32 ulStreamTransProtocol;

    /** 支持的流传输方式 参见#IMOS_STREAM_SERVER_MODE_E。目前XP只支持自适应和直连优先 */
    public UInt32 ulStreamServerMode;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szReserve;
}

[StructLayout(LayoutKind.Sequential)]
public struct VIN_CHANNEL_S
{
    /** 视频输入通道描述 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
    public byte[] szVinChannelDesc;

    /** 组播地址 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szMulticastAddr;

    /** 组播端口,范围为：10002-65534，且必须为偶数 */
    public UInt32 ulMulticastPort;

    /** MS选择的适应策略, 1为自适应; 0为非自适应 */
    public UInt32 ulIsAutofit;

    /** 使用MS数目, 视实际情况而定, 当适应策略#ulIsAutofit为自适应, 该值为0;
        当适应策略#ulIsAutofit为非自适应(即指定), 该值为1 */
    public UInt32 ulUseMSNum;

    /** MS编码列表 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = (IMOSSDK.IMOS_MS_MAX_NUM_PER_CHANNEL * IMOSSDK.IMOS_DEVICE_CODE_LEN))]
    public byte[,] aszMSCode;

    /** 是否启动图像遮挡检测告警, 1为启动; 0为不启动 */
    public UInt32 ulEnableKeepout;

    /** 是否启动运动检测告警, 1为启动运动检测告警; 0为不启动运动检测告警 */
    public UInt32 ulEnableMotionDetect;

    /** 是否启动视频丢失告警, 1为启动视频丢失告警; 0为不启动视频丢失告警 */
    public UInt32 ulEnableVideoLost;

    /** 绑定的串口编号，如无则填写0 */
    public UInt32 ulSerialIndex;

    /** 亮度，取值为（0~255） */
    public UInt32 ulBrightness;

    /** 对比度，取值为（0~255） */
    public UInt32 ulContrast;

    /** 饱和度，取值为（0~255） */
    public UInt32 ulSaturation;

    /** 色调，取值为（0~255） */
    public UInt32 ulTone;

    /** 是否启动静音, 1为启动静音; 0为不启动静音 */
    public UInt32 ulAudioEnabled;

    /** 音频编码, 取值为#IMOS_AUDIO_FORMAT_E */
    public UInt32 ulAudioCoding;

    /** 音频声道, 取值为#IMOS_AUDIO_CHANNEL_TYPE_E */
    public UInt32 ulAudioTrack;

    /** 音频采样率, 取值为#IMOS_AUDIO_SAMPLING_E */
    public UInt32 ulSamplingRate;

    /** 音频码率, 不可配置 */
    public UInt32 ulAudioCodeRate;

    /** 音频增益值，取值为（0~255） */
    public UInt32 ulIncrement;
}

[StructLayout(LayoutKind.Sequential)]
public struct VIDEO_STREAM_S
{
    /** 码流类型, 取值为#IMOS_STREAM_TYPE_E, 目前仅支持#IMOS_ST_TS */
    public UInt32 ulStreamType;

    /** 流索引，1为主流，2为副流 */
    public UInt32 ulStreamIndex;

    /** 流使能标识, 1为使能; 0为非使能 */
    public UInt32 ulEnabledFlag;

    /** 流传输方式, 取值为#IMOS_TRANS_TYPE_E */
    public UInt32 ulTranType;

    /** 编码格式, 取决于具体的流套餐值, 取值为#IMOS_VIDEO_FORMAT_E */
    public UInt32 ulEncodeFormat;

    /** 分辨率, 取值为#IMOS_PICTURE_SIZE_E */
    public UInt32 ulResolution;

    /** 码率 */
    public UInt32 ulBitRate;

    /** 帧率,可取的值有1, 3, 5, 8, 10, 15, 20, 25, 30 */
    public UInt32 ulFrameRate;

    /** GOP模式, 取值为#IMOS_GOP_TYPE_E */
    public UInt32 ulGopMode;

    /** I帧间隔, 取决于GOP模式值, 当GOP模式为#IMOS_GT_I时, I帧间隔为1; 当GOP模式为#IMOS_GT_IP时, I帧间隔为10~50 */
    public UInt32 ulIFrameInterval;

    /** 图像质量, 取值为#IMOS_VIDEO_QUALITY_E */
    public UInt32 ulImageQuality;

    /** 流编码模式, 取值为#IMOS_ENC_MODE_E */
    public UInt32 ulEncodeMode;

    /** 优先级, 仅在编码模式为#IMOS_EM_CBR时可设置该值, 取值为#IMOS_CBR_ENC_MODE_E */
    public UInt32 ulPriority;

    /** 码流平滑，取值为#IMOS_STREAM_SMOOTH_E */
    public UInt32 ulSmoothValue;
}

[StructLayout(LayoutKind.Sequential)]
public struct AREA_SCOPE_S
{
    /** 左上角x坐标, 取值为0~100 */
    public UInt32 ulTopLeftX;

    /** 左上角y坐标, 取值为0~100 */
    public UInt32 ulTopLeftY;

    /** 右下角x坐标, 取值为0~100 */
    public UInt32 ulBottomRightX;

    /** 右下角y坐标, 取值为0~100 */
    public UInt32 ulBottomRightY;

}

[StructLayout(LayoutKind.Sequential)]
public struct VIDEO_AREA_S
{
    /** 区域索引, 取值为1~4 */
    public UInt32 ulAreaIndex;

    /** 是否使能, 1为使能; 0为非使能 */
    public UInt32 ulEnabledFlag;

    /** 灵敏度, 1～5级，1级灵敏度最高。该值仅对运动检测区域有效 */
    public UInt32 ulSensitivity;

    /** 区域坐标 */
    public AREA_SCOPE_S stAreaScope;
}

[StructLayout(LayoutKind.Sequential)]
public struct DETECT_AREA_S
{
    /** 遮挡检测区域 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DETECT_AREA_MAXNUM)]
    public VIDEO_AREA_S[] astCoverDetecArea;

    /** 运动检测区域 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DETECT_AREA_MAXNUM)]
    public VIDEO_AREA_S[] astMotionDetecArea;
}

[StructLayout(LayoutKind.Sequential)]
public struct DEV_CHANNEL_INDEX_S
{
    /** 设备编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] szDevCode;

    /** 设备类型，当设备为编码器时, 取值为#IMOS_TYPE_EC; 当设备为解码器时, 取值为#IMOS_TYPE_DC */
    public UInt32 ulDevType;

    /** 通道索引号，分为:音频视频通道、串口通道、开关量通道(输入/输出), 取值视具体情况定 */
    public UInt32 ulChannelIndex;
}

[StructLayout(LayoutKind.Sequential)]
public struct SCREEN_INFO_S
{
    /** 监视器编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szScreenCode;

    /** 监视器名称 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szScreenName;

    /** 监视器描述, 可不填 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
    public byte[] szDevDesc;

    /**  DC的IP地址, 在绑定及修改Screen时, 不需填写; 查询Screen信息时会返回该字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szDCIPAddr;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szReserve;
}

[StructLayout(LayoutKind.Sequential)]
public struct VOUT_CHANNEL_S
{
    /** 逻辑输出通道索引, 取值为1~#IMOS_DC_LOGIC_CHANNEL_MAXNUM */
    public UInt32 ulVoutChannelindex;

    /** 逻辑输出通道描述, 可不填 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
    public byte[] szVoutChannelDesc;

    /** 是否使能, 1为使能; 0为不使能 */
    public UInt32 ulEnable;

    /** 码流类型, 取值为#IMOS_STREAM_TYPE_E, 目前仅支持#IMOS_ST_TS */
    public UInt32 ulStreamType;

    /** 流传输模式, 取值为#IMOS_TRANS_TYPE_E */
    public UInt32 ulTranType;

    /** 是否启动防抖动, 1为启动; 0为不启动 */
    public UInt32 ulEnableJitterBuff;
}

[StructLayout(LayoutKind.Sequential)]
public struct OSD_TIME_S
{
    /** 时间OSD索引, 固定为1 */
    public UInt32 ulOsdTimeIndex;

    /** 时间OSD使能, 1为使能; 0为非使能 */
    public UInt32 ulEnableFlag;

    /** 时间OSD时间格式 */
    public UInt32 ulOsdTimeFormat;

    /** 时间OSD日期格式 */
    public UInt32 ulOsdDateFormat;

    /** 时间OSD颜色, 取值为#IMOS_OSD_COLOR_E */
    public UInt32 ulOsdColor;

    /** 时间OSD透明度, 取值为#IMOS_OSD_ALPHA_E */
    public UInt32 ulTransparence;

    /** 时间OSD区域坐标 */
    public AREA_SCOPE_S stAreaScope;
}

[StructLayout(LayoutKind.Sequential)]
public struct OSD_NAME_S
{
    /** 是否使能场名OSD, 1为使能; 0为非使能 */
    public UInt32 ulEnabledFlag;

    /** 场名OSD索引, 固定为1 */
    public UInt32 ulOsdNameIndex;

    /** 场名OSD颜色, 取值为#IMOS_OSD_COLOR_E */
    public UInt32 ulOsdColor;

    /** 场名OSD透明度, 取值为#IMOS_OSD_ALPHA_E */
    public UInt32 ulTransparence;

    /** 场名OSD区域坐标 */
    public AREA_SCOPE_S stAreaScope;

    /** 第一个(主)场名OSD类型, 取值为#IMOS_INFO_OSD_TYPE_E */
    public UInt32 ulOsdType1;

    /** 第一个(主)场名OSD内容，对文字，该值为字符串，最长为20字符。对图片，该值为OSD图片编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szOsdString1;

    /** 第二个(辅)场名OSD类型, 取值为#IMOS_INFO_OSD_TYPE_E */
    public UInt32 ulOsdType2;

    /** 第二个(辅)场名OSD内容，对文字，该值为字符串，最长为20字符。对图片，该值为OSD图片编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szOsdString2;

    /** (第一个和第二个)场名OSD之间的切换时间, 单位为秒, 取值为0~300。取值为0, 表示只显示第一个(主)OSD */
    public UInt32 ulSwitchIntval;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public byte[] szReserve;
}
/// <summary>
/// 设备状态的信息结构体
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct AS_STAPUSH_UI_S
{
    /// <summary>
    ///  父设备类型 见IMOS_TYPE_E,在sdk_def.h中定义 
    /// </summary>
    public UInt32 ulParDevType;

    /// <summary>
    ///  父设备编码
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] szParDevCode;

    /// <summary>
    /// 设备状态 1-#IMOS_DEV_STATUS_ONLINE和2-#IMOS_DEV_STATUS_OFFLINE，在imos_def.h中定义
    ///         如果父设备类型为"摄像机",则设备状态取值为#AS_CAMERA_STATUS_E 
    /// </summary>
    public UInt32 ulDevSta;

    /// <summary>
    /// 是否有子设备
    /// </summary>
    public UInt32 bHasSubDev;

    /// <summary>
    /// 子设备类型
    /// </summary>
    public UInt32 ulSubDevType;

    /// <summary>
    /// 子设备编码
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] szSubDevCode;
}
/// <summary>
/// 告警的信息结构体
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct AS_ALARMPUSH_UI_S
{
    /// <summary>
    /// 告警事件编码
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] byAlarmEventCode;

    /// <summary>
    /// 告警源编码
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] byAlarmSrcCode;

    /// <summary>
    /// 告警源名称
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] byAlarmSrcName;

    /// <summary>
    /// 使能后名字
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] byActiveName;

    /// <summary>
    /// 告警类型 AlARM_TYPE_E 在sdk_def.h中定义
    /// </summary>
    public UInt32 ulAlarmType;

    /// <summary>
    /// 告警级别 ALARM_SEVERITY_LEVEL_E 在sdk_def.h中定义
    /// </summary>
    public UInt32 ulAlarmLevel;

    /// <summary>
    /// 告警触发时间
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
    public byte[] byAlarmTime;

    /// <summary>
    /// 告警描述信息
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
    public byte[] byAlarmDesc;

}
[StructLayout(LayoutKind.Sequential)]
public struct OSD_MASK_AREA_S
{
    /** 遮盖区域 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_MASK_AREA_MAXNUM)]
    public VIDEO_AREA_S[] astMaskArea;
}

[StructLayout(LayoutKind.Sequential)]
public struct OSD_INFO_S
{
    /** 时间OSD */
    public OSD_TIME_S stOSDTime;

    /** 场名OSD */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_OSD_NAME_MAXNUM)]
    public OSD_NAME_S[] astOSDName;

    /** 遮盖区域 */
    public OSD_MASK_AREA_S stOSDMaskArea;
}

[StructLayout(LayoutKind.Sequential)]
public struct PHYOUT_CHANNEL_S
{
    /** 工作模式，取值为1或4，代表BNC口的分屏数 */
    public UInt32 ulPhyoutMode;

    /** 视频解码格式，取值为#IMOS_VIDEO_FORMAT_E */
    public UInt32 ulDecodeFormat;

    /** 音频格式，取值为#IMOS_AUDIO_FORMAT_E */
    public UInt32 ulAudioFormat;

    /** 声道设置，取值为#IMOS_AUDIO_CHANNEL_TYPE_E */
    public UInt32 ulAudioTrack;

    /** 是否启用语音功能, 1为启用; 0为不启用 */
    public UInt32 ulAudioEnabled;

    /** 输出音量, 取值为1~7 */
    public UInt32 ulVolume;

    /** 音频输出选择, 视工作模式参数#ulPhyoutMode而定。如果工作模式取值为1, 则该值为1; 如果工作模式取值为4, 则该值取值为1~4 */
    public UInt32 ulOutputIndex;

    /** 最多监视器数量, 表示该物理通道最多可绑定的监视器数量, 目前固定为1 */
    public UInt32 ulMaxScreenNum;
}

[StructLayout(LayoutKind.Sequential)]
public struct VINCHNL_BIND_CAMERA_S
{
    /** 设备通道索引信息 */
    public DEV_CHANNEL_INDEX_S stECChannelIndex;

    /** 摄像机信息 */
    public CAMERA_INFO_S stCameraInfo;

    /** 视频输入通道信息 */
    public VIN_CHANNEL_S stVinChannel;

    /** OSD信息 */
    public OSD_INFO_S stOSDInfo;

    /** 视频流数组中视频流的实际数目, 最大取值为#IMOS_STREAM_MAXNUM, 视具体流套餐值而定 */
    public UInt32 ulVideoStreamNum;

    /** 视频流信息数组 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_STREAM_MAXNUM)]
    public VIDEO_STREAM_S[] astVideoStream;

    /** 检测区域，包含运动检测以及遮挡检测区域 */
    public DETECT_AREA_S stDetectArea;
}


[StructLayout(LayoutKind.Sequential)]
public struct VOUTCHNL_BIND_SCREEN_S
{
    /** 设备通道索引信息 */
    public DEV_CHANNEL_INDEX_S stDCChannelIndex;

    /** 监视器信息 */
    public SCREEN_INFO_S stScreenInfo;

    /** 逻辑输出通道信息 */
    public VOUT_CHANNEL_S stVoutChannel;

    /** OSD信息 */
    public OSD_INFO_S stOSDInfo;

    /** 物理输出通道信息 */
    public PHYOUT_CHANNEL_S stPhyoutChannel;
}

[StructLayout(LayoutKind.Sequential)]
public struct XP_RUN_INFO_EX_S
{
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public Byte[] szPortCode;     /**< 通道资源编码 */

    public UInt32 ulErrCode;
}

[StructLayout(LayoutKind.Sequential)]
public struct CAM_AND_CHANNEL_QUERY_ITEM_S
{
    /** 设备通道索引信息 */
    public DEV_CHANNEL_INDEX_S stECChannelIndex;

    /** 摄像机编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szCamCode;

    /** 摄像机名称 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szCamName;

    /** 摄像机类型, 取值为#CAMERA_TYPE_E */
    public UInt32 ulCamType;

    /** 云台控制协议 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szPtzProtocol;

    /** 云台地址码, 取值为0~255, 具体取值视云台摄像机的实际地址码而定 */
    public UInt32 ulPtzAddrCode;

    /** 组播地址 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_IPADDR_LEN)]
    public byte[] szMulticastAddr;

    /** 组播端口, 范围为：10002-65534 */
    public UInt32 ulMulticastPort;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szReserve;
}

[StructLayout(LayoutKind.Sequential)]
public struct SCR_AND_CHANNEL_QUERY_ITEM_S
{
    /** 设备通道索引信息 */
    public DEV_CHANNEL_INDEX_S stDCChannelIndex;

    /** 监视器编码 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szScrCode;

    /** 监视器名称 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szScrName;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szReserve;
}

[StructLayout(LayoutKind.Sequential)]
public struct SPLIT_SCR_INFO_S
{
    /** 分屏模式,取值为#SPLIT_SCR_MODE_E */
    public UInt32 ulSplitScrMode;

    /** 分屏编码(全屏时有效) */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
    public byte[] szSplitScrCode;

    /** 是否"自动切换主辅流"(#BOOL_TRUE 是,#BOOL_FALSE 否)  */
    public UInt32 bSwitchStream;

    /** 预留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
    public byte[] szReserve;
}
/// <summary>
///  录像段信息结构
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct UNITED_REC_FILE_INFO_S
{
    /** 文件名 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_FILE_NAME_LEN_V2)]
    public byte[] szFileName;

    /** 文件起始时间*/
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
    public byte[] szStartTime;

    /** 文件结束时间 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
    public byte[] szEndTime;

    /** 文件大小, 目前暂不使用 */
    public uint ulSize;

    /** 域级别计数，本域录像为0，低级别录像段依次累加 */
    public uint ulDomainLevel;

    /** 描述信息, 可不填 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DESC_LEN)]
    public byte[] szSpec;

    /* 预留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
    public byte[] szReserved;

    public static UNITED_REC_FILE_INFO_S GetInstance()
    {
        UNITED_REC_FILE_INFO_S instance = new UNITED_REC_FILE_INFO_S();

        instance.szFileName = new byte[IMOSSDK.IMOS_FILE_NAME_LEN_V2];
        instance.szStartTime = new byte[IMOSSDK.IMOS_TIME_LEN];
        instance.szEndTime = new byte[IMOSSDK.IMOS_TIME_LEN];
        instance.ulSize = 0;
        instance.ulDomainLevel = 0;
        instance.szSpec = new byte[IMOSSDK.IMOS_DESC_LEN];
        instance.szReserved = new byte[128];

        return instance;
    }
}


/// <summary>
/// 备份文件的信息项(查询摄像机备份文件时返回)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct CAM_BAK_FILE_QUERY_ITEM_S
{
    /// <summary>
    /// 备份文件编码
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_RES_CODE_LEN)]
    public byte[] szBakFileCode;

    /// <summary>
    ///  BM编码
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] szBMCode;

    /// <summary>
    /// BM名称
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szBMName;

    /// <summary>
    /// 摄像机编码
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_DEVICE_CODE_LEN)]
    public byte[] szCamCode;

    /// <summary>
    /// 摄像机名称
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szCamName;

    /// <summary>
    /// 摄像机类型, 取值为#CameraType
    /// </summary>
    public UInt32 ulCameraType;

    /// <summary>
    /// 备份文件存储路径
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_FILE_PATH_LEN)]
    public byte[] szBakFilePath;

    /// <summary>
    /// 文件生成时间
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
    public byte[] szFileCreateTime;

    /// <summary>
    /// 备份时间:开始、结束
    /// </summary>
    public TIME_SLICE_S stBakTime;

    /// <summary>
    /// 文件容量，以MB为单位
    /// </summary>
    public UInt32 ulFileCapacity;

    /// <summary>
    /// 备份资源编码
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_CODE_LEN)]
    public byte[] szBakResCode;

    /// <summary>
    /// 文件类型#BAK_TYPE_E
    /// </summary>
    public UInt32 ulFileType;

    /// <summary>
    /// 文件保存格式，0-TS、1-h3crd
    /// </summary>
    public UInt32 ulFileFormat;

    /// <summary>
    /// 备份文件状态标识，0-过程文件、1-最终文件
    /// </summary>
    public UInt32 ulFileStatus;

    /// <summary>
    /// 是否锁定标识，0-未锁定、1-锁定
    /// </summary>
    public UInt32 ulLockFlag;

    /// <summary>
    /// 案例描述
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_BAK_CASE_LEN)]
    public byte[] szCaseDesc;
}

/// <summary>
///  统一录像检索响应信息
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct REC_RSP_ROW_INFO_S
{
    /** 实际返回的录像段数目 */
    public uint ulRowNum;

    /** 是否还有数据 */
    public uint bHasMoreRow;

    /** 本轮录像的总结束时间 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_TIME_LEN)]
    public byte[] szEndTime;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
    public byte[] szReserve;

    public static REC_RSP_ROW_INFO_S GetInstance()
    {
        REC_RSP_ROW_INFO_S instance = new REC_RSP_ROW_INFO_S();
        instance.ulRowNum = 0;
        instance.bHasMoreRow = 0;
        instance.szEndTime = new byte[IMOSSDK.IMOS_TIME_LEN];
        instance.szReserve = new byte[32];
        return instance;
    }
}

[StructLayout(LayoutKind.Sequential)]
public struct RES_ITEM_V2_S
{
    /** V1资源信息项 */
    public ORG_RES_QUERY_ITEM_S stResItemV1;

    /** 资源所属组织的名称 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = IMOSSDK.IMOS_NAME_LEN)]
    public byte[] szOrgName;

    /** 资源属性信息，对于资源类型是摄像机时，取值为#CAMERA_ATTRIBUTE_E，其他资源类型该字段暂未使用 */
    public UInt32 ulResAttribute;

    /** 本域非ECR HF系列的摄像机或者监视器所在的设备的流套餐，
        其他资源类型,或者通用查询条件IS_QUERY_ENCODESET没有填写, 或者填了"不查询", 该字段均为无效值#IMOS_SR_INVALID
        取值为#IMOS_STREAM_RELATION_SET_E */
    public UInt32 ulDevEncodeSet;

    /** 保留字段 */
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 184)]
    public byte[] szReserve;

    public static RES_ITEM_V2_S GetInstance(ORG_RES_QUERY_ITEM_S stRes)
    {
        RES_ITEM_V2_S stRes_V2 = new RES_ITEM_V2_S();
        stRes_V2.stResItemV1 = stRes;
        stRes_V2.szOrgName = new byte[IMOSSDK.IMOS_NAME_LEN];
        stRes_V2.szReserve = new byte[184];
        stRes_V2.ulDevEncodeSet = 0;
        stRes_V2.ulResAttribute = 0;
        return stRes_V2;
    }
}

public class IMOSSDK
{

    public const int IMOS_NAME_LEN = 64;

    public const int IMOS_CODE_LEN = 48;

    public const int IMOS_IPADDR_LEN = 64;

    /*@brief 资源编码信息字符串长度*/
    public const int IMOS_RES_CODE_LEN = IMOS_CODE_LEN;

    /*@brief 设备编码信息字符串长度*/
    public const int IMOS_DEVICE_CODE_LEN = IMOS_CODE_LEN;

    /*@brief 用户编码信息字符串长度*/
    public const int IMOS_USER_CODE_LEN = IMOS_CODE_LEN;

    /*@brief 域编码信息字符串长度*/
    public const int IMOS_DOMAIN_CODE_LEN = IMOS_CODE_LEN;

    /*@brief 域名称信息字符串长度 */
    public const int IMOS_DOMAIN_NAME_LEN = IMOS_NAME_LEN;

    /*@brief 权限编码信息字符串长度*/
    public const int IMOS_AUTH_CODE_LEN = IMOS_CODE_LEN;

    //每次查询时返回的最大符合条件的结果的个数
    public const int QUERY_ITEM_MAX = 200;

    /*@brief imos_time 时间信息字符串长度 "2008-10-02 09:25:33.001 GMT" */
    public const int IMOS_TIME_LEN = 32;

    /// <summary>
    /// 文件绝对路径(路径+文件名)长度
    /// </summary>
    public const int IMOS_FILE_PATH_LEN = 256;
    /// <summary>
    /// 备份录像案例长度
    /// </summary>
    public const int IMOS_BAK_CASE_LEN = 128;
    /// <summary>
    /// 文件名长度(V2)
    /// </summary>
    public const int IMOS_FILE_NAME_LEN_V2 = 256;

    /*@brief 文件名长度 */
    public const int IMOS_FILE_NAME_LEN = 64;

    public const uint ERR_XP_FAIL_TO_SETUP_PROTOCOL = 0x0007B0;      /**< 建立流控协商失败 */
    public const uint ERR_XP_FAIL_TO_PLAY_PROTOCOL = 0x0007B1;      /**< 流控协商播放失败 */
    public const uint ERR_XP_FAIL_TO_PAUSE_PROTOCOL = 0x0007B2;      /**< 流控协商暂停失败 */
    public const uint ERR_XP_FAIL_TO_STOP_PROTOCOL = 0x0007B3;      /**< 停止流控协商失败 */
    public const uint ERR_XP_RTSP_COMPLETE = 0x0007B4;      /**< RTSP播放或下载完成 */
    public const uint ERR_XP_RTSP_ABNORMAL_TEATDOWN = 0x0007B5;      /**< RTSP异常下线，服务器读取文件错误或数据被覆写 */
    public const uint ERR_XP_RTSP_KEEP_LIVE_TIME_OUT = 0x0007B6;      /**< RTSP保活失败 */
    public const uint ERR_XP_RTSP_ENCODE_CHANGE = 0x0007B7;      /**< RTSP中码流格式切换 */
    public const uint ERR_XP_RTSP_DISCONNECT = 0x0007B8;      /**< RTSP连接断开，点播回放或下载已自动终止，请检查网络 */

    public const uint ERR_XP_DISK_CAPACITY_WARN = 0x00079B;      /**< 硬盘剩余空间低于阈值 */
    public const uint ERR_XP_DISK_CAPACITY_NOT_ENOUGH = 0x00079C;     /**< 硬盘剩余空间不足，无法继续业务 */
    public const uint ERR_XP_FAIL_TO_WRITE_FILE = 0x000723;     /**< 写文件操作失败 */
    public const uint ERR_XP_FAIL_TO_PROCESS_MEDIA_DATA = 0x0007A9;   /**< 媒体数据处理失败 */
    public const uint ERR_XP_NOT_SUPPORT_MEDIA_ENCODE_TYPE = 0x000735;/**< 播放通道的媒体编码格式不支持此操作 */
    public const uint ERR_XP_MEDIA_RESOLUTION_CHANGE = 0x000736;      /**< 播放通道的媒体流分辨率发生变化 */

    /*@brief imos_description 描述信息字符串长度 */
    public const int IMOS_DESC_LEN = (128 * 3);

    public const int IMOS_IE_URL_LEN = 512;

    public const int IMOS_PASSWD_ENCRYPT_LEN = 64;

    public const int IMOS_QUERY_ITEM_MAX_NUM = 16;

    public const int IMOS_QUERY_DATA_MAX_LEN = 64;

    public const int IMOS_DEV_STATUS_ONLINE = 1;

    public const int IMOS_DEV_STATUS_OFFLINE = 2;

    public const int IMOS_STREAM_MAXNUM = 2;

    public const int IMOS_MS_MAX_NUM_PER_CHANNEL = 1;

    public const int IMOS_DETECT_AREA_MAXNUM = 4;

    public const int IMOS_MASK_AREA_MAXNUM = 4;

    public const int IMOS_OSD_NAME_MAXNUM = 1;



    public static LOGIN_INFO_S stLoginInfo;
    public static XP_INFO_S stXpInfo;
    public static PLAY_WND_INFO_S[] astPlayWndInfo = new PLAY_WND_INFO_S[25];


    public static System.Timers.Timer timerKeepalive;

    public delegate void MethodInvoke1<T>(T Param);

    public static string UTF8ToUnicode(byte[] bufferIn)
    {

        byte[] buffer = Encoding.Convert(Encoding.UTF8, Encoding.Default, bufferIn, 0, bufferIn.Length);
        return Encoding.Default.GetString(buffer, 0, buffer.Length);
    }

    public static byte[] UnicodeToUTF8(string buffIn)
    {
        byte[] buffer = Encoding.Default.GetBytes(buffIn);
        return Encoding.Convert(Encoding.Default, Encoding.UTF8, buffer, 0, buffer.Length);
    }

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_Initiate(String strServerIP, UInt32 ulServerPort, UInt32 bUIFlag, UInt32 bXPFlag);

    /// <summary>
    /// xp信息回调，主要用于接收一些XP相关信息
    /// </summary>
    /// <param name="stUserLoginIDInfo"></param>
    /// <param name="ulRunInfoType"></param>
    /// <param name="ptrInfo"></param>
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void XP_RUN_INFO_CALLBACK_EX_PF(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, UInt32 ulRunInfoType, IntPtr pParam);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_Encrypt(String strInput, UInt32 ulInLen, IntPtr ptrOutput);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_Login(String strUserLoginName, String strPassword, String strIpAddr, IntPtr ptrSDKLoginInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_LoginEx(String strUserLoginName, String strPassword, String srvIpAddr, String cltIpAddr, IntPtr ptrSDKLoginInfo);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_PlaySound(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_CleanUp(IntPtr pstUserLogIDInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_UserKeepAlive(ref USER_LOGIN_ID_INFO_S stUserLoginInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StartMonitor(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCameraCode, byte[] szMonitorCode, UInt32 ulStreamType, UInt32 ulOperateCode);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StopMonitor(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szMonitorCode, UInt32 ulOperateCode);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryResourceList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szOrgCode, UInt32 ulResType, UInt32 ptrQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrResList);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryResourceListV2(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szOrgCode, ref COMMON_QUERY_CONDITION_S pstQueryCondition, ref QUERY_PAGE_INFO_S pstQueryPageInfo, ref RSP_PAGE_INFO_S pstRspPageInfo, IntPtr pstResList);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_Logout(ref USER_LOGIN_ID_INFO_S stUserLoginInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern void IMOS_LogoutEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern void IMOS_GetChannelCode(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, IntPtr pcChannelCode);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StartPtzCtrl(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ReleasePtzCtrl(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, UInt32 bReleaseSelf);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryPresetList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, ref QUERY_PAGE_INFO_S stQueryPageInfo, ref RSP_PAGE_INFO_S ptrRspPage, IntPtr pstPresetList);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetPreset(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szCamCode, ref PRESET_INFO_S pstPreset);

    /// <summary>
    /// 删除预置位
    /// </summary>
    /// <param name="pstUserLogIDInfo">用户登录ID信息标识</param>
    /// <param name="szCamCode">摄像机编码</param>
    /// <param name="ulPresetValue">预置位值</param>
    /// <returns></returns>
    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_DelPreset(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szCamCode, UInt32 ulPresetValue);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_UsePreset(ref USER_LOGIN_ID_INFO_S pstUserLogIDInfo, byte[] szCamCode, UInt32 ulPresetNum);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_PtzCtrlCommand(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, ref PTZ_CTRL_COMMAND_S stPTZCommand);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StartPlayer(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, UInt32 ulPlayWndNum, IntPtr ptrPlayWndInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StopPlayer(ref USER_LOGIN_ID_INFO_S stUserLoginInfo);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetPlayWnd(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr hWnd);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_RecordRetrieval(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref REC_QUERY_INFO_S stSDKRecQueryInfo, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrSDKRecordFileInfo);

    /// <summary>
    /// 注册回调函数
    /// </summary>
    /// <param name="pstUserLoginIDInfo">登入信息</param>
    /// <param name="ptrCallBack">回调函数</param>
    /// <returns></returns>
    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_RegCallBackPrcFunc(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, IntPtr pfnCallBackProc);

    /// <summary>
    /// 告警回调函数
    /// </summary>
    /// <param name="ulProcType">告警类型</param>
    /// <param name="ptrParam">返回的数据指针</param>
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void CALL_BACK_PROC_PF(UInt32 ulProcType, IntPtr ptrParam);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_GetRecordFileURL(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref GET_URL_INFO_S stSDKGetUrlInfo, ref URL_INFO_S stUrlInfo);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_FreeChannelCode(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, Byte[] pcChannelCode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_OpenVodStream(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szVodUrl, byte[] szServerIP, UInt16 usServerPort, UInt32 ulProtl);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StartPlay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_PausePlay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ResumePlay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StopPlay(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetPlaySpeed(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, UInt32 ulPlaySpeed);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetRunMsgCB(IntPtr ptrRunInfoFunc);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_GetDownloadTime(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, String pcDownloadID, byte[] pszTime);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_OneByOne(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetPlayedTime(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szTime);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetPlayedTimeEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, UInt32 ulTime);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_GetPlayedTime(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr ptrTime);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_GetPlayedTimeEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr ptrTime);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StopDownload(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcDownloadID);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SnatchOnce(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulPicFormat);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SnatchOnceEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulPicFormat);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StartSnatchSeries(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulPicFormat, UInt32 ulInterval);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StopSnatchSeries(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StartRecord(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulFileFormat);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StartRecordEx(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, byte[] szFileName, UInt32 ulFileFormat, IntPtr ptrFilePostfix);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StopRecord(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_GetVideoEncodeType(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, ref UInt32 ptrVideoEncodeType);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_GetLostPacketRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr ptrRecvPktNum, IntPtr ptrLostPktNum);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ResetLostPacketRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_GetLostFrameRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, IntPtr ptrAllFrameNum, IntPtr ptrLostFrameNum);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ResetLostFrameRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_OpenDownload(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcDownUrl, byte[] pcServerIP, ushort usServerPort, UInt32 ulProtl, UInt32 ulDownMediaSpeed, byte[] pcFileName, UInt32 ulFileFormat, byte[] pcChannelCode);


    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StartDownload(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode);



    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetDecoderTag(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode, byte[] pcDecorderTag);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_GetFrameRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, ref UInt32 ptrFrameRate);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_GetBitRate(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, ref UInt32 ptrBitRate);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_StopSound(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetVolume(UInt32 ulVolume);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_GetVolume(IntPtr ptrVolume);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetFieldMode(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szChannelCode, UInt32 ulFieldMode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_AdjustAllWaveAudio(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, UInt32 ulCoefficient);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_AdjustPktSeq(Boolean bAdjust);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetRenderMode(UInt32 ulRenderMode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetRealtimeFluency(UInt32 ulFluency);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_XP_SetDeinterlaceMode(UInt32 ulPort, UInt32 ulDeinterlaceMode);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetPixelFormat(UInt32 ulPixelFormat);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ConfigXpStreamInfo(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref XP_STREAM_INFO_S pstXpStreamInfo);



    //EC Camera 配置接口

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_AddEc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref EC_INFO_S stEcInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ModifyEc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref EC_INFO_S stEcInfo, UInt32 IsEncodeChange);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_DelEc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szEcCode);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryEcList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szOrgCode, IntPtr ptrQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrEcList);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryEcInfo(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szEcCode, IntPtr ptrEcInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_BindCameraToVideoInChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref VINCHNL_BIND_CAMERA_S stVinChnlAndCamInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryCameraAndChannelList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDevCode, IntPtr stQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrCamAndChannelList);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ModifyCamera(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref CAMERA_INFO_S stCamInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_UnBindCamera(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryCamera(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szCamCode, IntPtr ptrCameraInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ConfigVideoInChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, ref VIN_CHANNEL_S stVideoInChannelInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryECVideoInChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrVideoInChannelInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ConfigECVideoStream(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, ref VIDEO_STREAM_S stVideoStreamInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryECVideoStream(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrStreamNum, IntPtr ptrVideoStreamInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ConfigECMaskAreaOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, UInt32 ulMaskAreaNum, ref VIDEO_AREA_S stMaskArea);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryECMaskAreaOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrMaskAreaNum, IntPtr ptrMaskArea);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ConfigECMotionDetectArea(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, UInt32 ulMotionDetectAreaNum, ref VIDEO_AREA_S stMotionDetectArea);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryECMotionDetectArea(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrMotionDetectAreaNum, IntPtr ptrMotionDetectArea);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ConfigDeviceTimeOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, ref OSD_TIME_S stTimeOSD);

    /// <summary>
    /// 完成订阅推送功能
    /// </summary>
    /// <param name="stUserLoginIDInfo">登入信息</param>
    /// <param name="ulSubscribePushType">订阅类型</param>
    /// <returns></returns>
    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SubscribePushInfo(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, UInt32 ulSubscribePushType);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryDeviceTimeOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrTimeOSD);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ConfigDeviceNameOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, UInt32 ulNameOSDNum, ref OSD_NAME_S stNameOSD);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryDeviceNameOSD(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrNameOSDNum, IntPtr ptrNameOSD);



    //DC Screen 配置接口

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_AddDc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DC_INFO_S stDcInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ModifyDc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DC_INFO_S stDcInfo, UInt32 IsEncodeChange);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_DelDc(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDcCode);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryDcList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szOrgCode, IntPtr ptrQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrDcList);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryDcInfo(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDcCode, IntPtr ptrDcInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_BindScreenToVideoOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref VOUTCHNL_BIND_SCREEN_S stVOUTChnlAndScrInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryScreenAndChannelList(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szDevCode, IntPtr ptrQueryCondition, ref QUERY_PAGE_INFO_S stQueryPageInfo, IntPtr ptrRspPage, IntPtr ptrVOUTChnlAndScrList);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ModifyScreen(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref SCREEN_INFO_S stScrInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_UnBindScreen(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szScrCode);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryScreen(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szScrCode, IntPtr ptrScreenInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ConfigDCVideoOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, UInt32 ulVideoOutNum, ref VOUT_CHANNEL_S stVideoOutChannelInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryDCVideoOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrVideoOutNum, IntPtr ptrVideoOutChannelInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_ConfigDCPhyOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, ref PHYOUT_CHANNEL_S stPhyoutChannelInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QueryDCVideoOutChannel(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, ref DEV_CHANNEL_INDEX_S stChannelIndex, IntPtr ptrPhyoutChannelInfo);

    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_QuerySplitScrInfo(ref USER_LOGIN_ID_INFO_S stUserLoginInfo, byte[] szScrCode, ref SPLIT_SCR_INFO_S ptrSplitInfo);


    /**
    * 拼帧前媒体流数据回调函数的指针类型。\n
    * @param [IN] pstUserLoginIDInfo    用户登录ID信息标识。
    * @param [IN] pcChannelCode         播放通道编码。
    * @param [IN] pBuffer               存放拼帧前媒体流数据缓冲区指针。
    * @param [IN] ulBufSize             缓冲区大小。
    * @param [IN] ulMediaDataType       媒体流数据类型，对应#XP_MEDIA_DATA_FORMAT_E枚举中的值。
    * @param [IN] lUserParam            用户设置参数，用户在调用IMOS_SetSourceMediaDataCB函数时指定的用户参数。
    * @param [IN] lReserved             存放拼帧前媒体流扩展信息缓冲区指针，用户需要转换为#XP_SOURCE_DATA_EX_INFO_S
    *                                   结构体指针类型，内含解码器标签（DecoderTag），当用户使用播放库解码和显示回调
    *                                   的媒体流时，可根据解码器标签指定解码器。
    * @return 无。
    * @note
    * -     用户应及时处理输出的媒体流数据，确保函数尽快返回，否则会影响播放器内的媒体流处理。
    */
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void XP_SOURCE_MEDIA_DATA_CALLBACK_PF(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, string pcChannelCode, IntPtr pBuffer, UInt32 ulBufSize, UInt32 ulMediaDataType, long lUserParam, long lReserved);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetSourceMediaDataCB(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode, IntPtr ptrSplitInfo, bool bContinue, long lUserParam);




    /**
    * 设置拼帧后视频数据回调函数。\n
    * @param [IN] pstUserLoginIDInfo      用户登录ID信息标识。
    * @param [IN] pcChannelCode           播放通道编码。
    * @param [IN] pfnParseVideoDataCBFun  拼帧后视频数据回调函数的指针。
    * @param [IN] bContinue               是否继续进行视频解码和播放操作。
    * @param [IN] lUserParam              用户设置参数。
    * @return 返回如下错误码：
    * -         #ERR_COMMON_SUCCEED                      成功
    * -         #ERR_COMMON_INVALID_PARAM                输入参数非法
    * -         #ERR_XP_PORT_NOT_REGISTER                播放通道没有注册
    * -         #ERR_XP_FAIL_TO_GET_PORT_RES             获得播放通道资源失败
    * -         #ERR_XP_FAIL_TO_SET_PROCESS_DATA_CB      设置媒体流数据回调函数失败
    * @note
    * - 1、实时播放时，该函数在#IMOS_StartMonitor之前或者之后调用，在#IMOS_StopMonitor时自动失效，下次调用
    *      #IMOS_StartMonitor之前或者之后需要重新设置；点播回放时，该函数可在#IMOS_OpenVodStream之前调用，也可
    *      在#IMOS_OpenVodStream和#IMOS_StartPlay之间调用，还可以在#IMOS_StartPlay之后调用，在#IMOS_StopPlay时自
    *      动失效，下次启动点播回放时需要在相同位置重新设置；
    * - 2、回调函数要尽快返回，如果要停止回调，可以把回调函数指针#XP_PARSE_VIDEO_DATA_CALLBACK_PF设为NULL；
    * - 3、该接口函数支持Windows和Linux。
    */
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void XP_PARSE_VIDEO_DATA_CALLBACK_PF(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, string pcChannelCode, IntPtr pstParseVideoData, long lUserParam, long lReserved);


    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetParseVideoDataCB(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode, IntPtr ptrSplitInfo, bool bContinue, long lUserParam);

    /**
    * 拼帧后音频流数据回调函数的指针类型。\n
    * @param [IN] pstUserLoginIDInfo    用户登录ID信息标识。
    * @param [IN] pcChannelCode         播放通道编码。
    * @param [IN] pstParseAudioData     存放拼帧后音频流数据信息缓冲区指针
    * @param [IN] lUserParam            用户设置参数，用户在调用#IMOS_SetParseAudioDataCB函数时指定的用户参数
    * @param [IN] lReserved             保留参数
    * @return 无。
    * @note
    * -     用户应及时处理输出的数据，确保函数尽快返回，否则会影响播放器内的媒体流处理。
    */
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void XP_PARSE_AUDIO_DATA_CALLBACK_PF(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, string pcChannelCode, IntPtr pstParseVideoData, long lUserParam, long lReserved);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetParseAudioDataCB(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode, IntPtr ptrSplitInfo, bool bContinue, long lUserParam);

    /**
    * 解码后视频流数据回调函数的指针类型。\n
    * @param [IN] pstUserLoginIDInfo   用户登录ID信息标识。
    * @param [IN] pcChannelCode        播放通道编码。
    * @param [IN] pstPictureData       存放解码后视频流数据信息缓冲区指针。
    * @param [IN] lUserParam           用户设置参数，用户在调用#IMOS_SetDecodeVideoDataCB函数时指定的用户参数。
    * @param [IN] lReserved            保留参数。
    * @return 无。
    * @note
    * -     1、用户应及时处理输出的视频流数据，确保函数尽快返回，否则会影响播放器内的媒体流处理。
    * -     2、视频数据是yv12格式。排列顺序“Y0-Y1-......”，“U0-U1-......”，“V0-V1-......”。
    */
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void XP_DECODE_VIDEO_DATA_CALLBACK_PF(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, string pcChannelCode, IntPtr pstParseVideoData, long lUserParam, long lReserved);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetDecodeVideoDataCB(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode, IntPtr ptrSplitInfo, bool bContinue, long lUserParam);

    /**
    * 解码前语音数据回调函数的指针类型。\n
    * @param [IN] pstUserLoginIDInfo    用户登录ID信息标识。
    * @param [IN] pucVoiceDataBuffer    存放解码前语音数据信息缓冲区指针
    * @param [IN] ulBufSize             音频数据大小
    * @param [IN] ulAudioFlag           音频数据类型,需从#XP_AUDIO_FLAG_E枚举中取值
    * @param [IN] pUserParam            用户设置参数
    * @return 无。
    * @note
    * -     用户应及时处理输出的音频流数据，确保函数尽快返回，否则会影响播放器内的媒体流处理。
    */
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate void XP_DECODE_AUDIO_DATA_CALLBACK_PF(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, string pcChannelCode, XP_WAVE_DATA_S pstWaveData, long lUserParam, long lReserved);

    [DllImport("xp_frame.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SetDecodeAudioDataCB(ref USER_LOGIN_ID_INFO_S pstUserLoginIDInfo, byte[] pcChannelCode, IntPtr ptrSplitInfo, bool bContinue, long lUserParam);

    /**
      * 统一录像检索 \n
      * @param [IN]   pstUserLogIDInfo            用户登录ID信息标识
      * @param [IN]   pstRecQueryInfo             回放检索消息数据结构
      * @param [IN]   ulRowNum                    请求的记录数
      * @param [OUT]  pstRecRspRowInfo            返回记录信息
      * @param [OUT]  pstUnitedRecFileInfoList    录像文件信息数据结构体
      * @return 返回如下结果：
      * - 成功：
      * - 失败：
      * -     返回操作结果码，见错误码文件
      * @note 1、查询时间跨度不能大于24小时，且在录像检索流程中时间格式为："%Y-%m-%d %H:%M:%S"格式 ，信息长度限定为24字符.
      *       2、文件名字符串数组最大长度为IMOS_FILE_NAME_LEN_V2
      */
    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_UnitedRecordRetrieval(ref USER_LOGIN_ID_INFO_S stUserLogIDInfo, ref REC_QUERY_INFO_S stSDKRecQueryInfo,
        uint ulRowNum, ref REC_RSP_ROW_INFO_S stRecRspRowInfo, IntPtr pstSDKRecordFileInfo);

    /**
      * 备份录像检索 \n
      * @param [IN]   pstUserLogIDInfo        用户登录ID信息标识
      * @param [IN]   pstQueryCondition       通用查询条件
      * @param [IN]   pstQueryPageInfo        请求分页信息
      * @param [OUT]  pstRspPageInfo          响应分页信息
      * @param [OUT]  pstSDKRecordFileInfo    备份文件信息数据结构体
      * @return 返回如下结果：
      * - 成功：
      * - 失败：
      * -     返回操作结果码：见结果码文件sdk_err.h
      * @note
      * - 1、通用查询条件支持:摄像机编码[#CODE_TYPE]、摄像机名称[#NAME_TYPE]、文件生成时间[#FILE_CREATE_TIME]、
      * -    备份开始时间[#BAK_START_TIME]、备份结束时间[#BAK_END_TIME]、文件容量[#FILE_CAPACITY]、文件类型[#FILE_TYPE]、
      * -    文件锁定标识[#FILE_LOCK_FLAG]、案例描述[#CASE_DESC]的查询和排序;
      * - 2、通用查询条件为NULL,或无排序条件时,默认按"备份开始时间升序"排序;
      * - 3、通用查询条件中添加的备份开始时间与结束时间之间的时间跨度不能跨天，且时间格式为："%Y-%m-%d %H:%M:%S"格式 ，信息长度限定为24字符
      */
    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_BakRecordRetrieval(ref USER_LOGIN_ID_INFO_S stUserLoginIDInfo, ref COMMON_QUERY_CONDITION_S stQueryCondition,
                                                                                       ref QUERY_PAGE_INFO_S pageInfo, ref RSP_PAGE_INFO_S stRspPageInfo, IntPtr pstRecordFileInfo);


    /**
  * 摄像机的备用存储录像检索 \n
  * @param [IN]   pstUserLogIDInfo        用户登录ID信息标识
  * @param [IN]   bExtDomainDev           是否外域共享推送的摄像机
  * @param [IN]   pstSDKRecQueryInfo      回放检索消息数据结构，对于外域摄像机，szCamCode为摄像机共享编码
  * @param [IN]   pstQueryPageInfo        请求分页信息
  * @param [OUT]  pstRspPageInfo          返回分页信息
  * @param [OUT]  pstSDKRecordFileInfo    录像文件信息数据结构体
  * @return 返回如下结果：
  * - 成功：
  * - 失败：
  * -     返回操作结果码，见错误码文件
  * @note 查询时间跨度不能大于24小时，且在录像检索流程中时间格式为："%Y-%m-%d %H:%M:%S"格式 ，信息长度限定为24字符
  */
    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_SlaveRecordRetrieval(ref USER_LOGIN_ID_INFO_S stUserLogIDInfo, int bExtDomainDev,
        ref REC_QUERY_INFO_S stSDKRecQueryInfo, ref QUERY_PAGE_INFO_S stQueryPageInfo,
        ref RSP_PAGE_INFO_S pstRspPageInfo, IntPtr pstSDKRecordFileInfo);


    /**
   * 获取录像文件的URL信息 \n
   * @param[IN]    pstUserLogIDInfo            用户登录ID信息标识
   * @param[IN]    pstUnitedGetUrlInfo         获取云录像URL的请求结构
   * @param[OUT]   pstSDKURLInfo               URL信息
   * @return 返回如下结果：
   * - 成功：
   * - 失败：
   * -     返回操作结果码，见错误码文件
   */
    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_UnitedGetRecordFileURL(ref USER_LOGIN_ID_INFO_S stUserLogIDInfo,
        ref UNITED_GET_URL_INFO_S stUnitedGetUrlInfo, ref URL_INFO_S stSDKURLInfo);


    /**
  * 获取摄像机的备用存储录像的URL信息 \n
  * @param[IN]    pstUserLogIDInfo            用户登录ID信息标识
  * @param [IN]   bExtDomainDev               是否外域共享推送的摄像机
  * @param[IN]    pstSDKGetUrlInfo            获取URL请求消息数据结构，对于外域摄像机，szCamCode为摄像机共享编码
  * @param[OUT]    pstSDKURLInfo               URL信息
  * @return 返回如下结果：
  * - 成功：
  * - 失败：
  * -     返回操作结果码，见错误码文件
  */
    [DllImport("imos_sdk.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern UInt32 IMOS_GetSlaveRecordFileURL(ref USER_LOGIN_ID_INFO_S stUserLogIDInfo, int bExtDomainDev,
        ref GET_URL_INFO_S stSDKGetUrlInfo, ref URL_INFO_S stSDKURLInfo);
}



/// <summary>
/// 存放拼帧后视频数据的指针和长度等信息的结构体定义
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct XP_PARSE_VIDEO_DATA_S
{
    /// <summary>
    /// 视频数据指针 UCHAR* pucData;
    /// </summary>
    public IntPtr pucData;

    /// <summary>
    /// 视频数据长度
    /// </summary>

    public UInt32 ulDataLen;
    /// <summary>
    /// 视频帧类型，从#XP_VIDEO_FRAME_TYPE_E中取值
    /// </summary>
    public UInt32 ulVideoFrameType;

    /// <summary>
    /// 视频编码格式，从#XP_VIDEO_ENCODE_TYPE_E中取值
    /// </summary>
    public UInt32 ulVideoCodeFormat;
    /// <summary>
    /// 视频图像高度
    /// </summary>
    public UInt32 ulHeight;

    /// <summary>
    /// 视频图像宽度
    /// </summary>
    public UInt32 ulWidth;

    /// <summary>
    /// 时间戳（毫秒）
    /// </summary>
    public long dlTimeStamp;

    /// <summary>
    /// 保留参数
    /// </summary>
    public UInt32 ulReserved1;

    /// <summary>
    /// 保留参数
    /// </summary>
    public UInt32 ulReserved2;
}

/// <summary>
/// 存放拼帧后音频数据的指针和长度等信息的结构体定义
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct XP_PARSE_AUDIO_DATA_S
{
    /// <summary>
    /// 音频数据指针
    /// </summary>
    public IntPtr pucData;
    /// <summary>
    /// 音频数据长度
    /// </summary>
    public UInt32 ulDataLen;
    /// <summary>
    /// 音频编码格式，从#XP_AUDIO_ENCODE_TYPE_E中取值
    /// </summary>
    public UInt32 ulAudioCodeFormat;
    /// <summary>
    /// 音频数据解码后音频格式，对应#XP_WAVE_FORMAT_INFO_E枚举中的值
    /// </summary>
    public UInt32 ulWaveFormat;
    /// <summary>
    /// 时间戳（毫秒）
    /// </summary>
    public long dlTimeStamp;
    /// <summary>
    /// 保留参数
    /// </summary>
    public UInt32 ulReserved1;
    /// <summary>
    /// 保留参数
    /// </summary>
    public UInt32 ulReserved2;
}

/// <summary>
/// 存放解码后图像数据的指针和长度等信息的结构体定义
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct XP_PICTURE_DATA_S
{
    /// <summary>
    /// pucData[0]:Y 平面指针,pucData[1]:U 平面指针,pucData[2]:V 平面指针
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public IntPtr[] pucData;
    /// <summary>
    /// ulLineSize[0]:Y平面每行跨距, ulLineSize[1]:U平面每行跨距, ulLineSize[2]:V平面每行跨距
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public UInt32[] ulLineSize;
    /// <summary>
    /// 图片高度
    /// </summary>
    public UInt32 ulPicHeight;
    /// <summary>
    /// 图片宽度
    /// </summary>
    public UInt32 ulPicWidth;
    /// <summary>
    /// 用于渲染的时间数据类型，对应tagRenderTimeType枚举中的值
    /// </summary>
    public UInt32 ulRenderTimeType;
    /// <summary>
    /// 用于渲染的时间数据
    /// </summary>
    public long dlRenderTime;
}

/// <summary>
/// 存放解码后音频数据的指针和长度等信息的结构体定义
/// </summary>
public struct XP_WAVE_DATA_S
{
    /// <summary>
    /// 音频数据指针
    /// </summary>
    public IntPtr pcData;
    /// <summary>
    /// 音频数据长度
    /// </summary>
    public UInt32 ulDataLen;
    /// <summary>
    /// 解码后音频格式，对应tagWaveFormatInfo枚举中的值
    /// </summary>
    public UInt32 ulWaveFormat;
}
