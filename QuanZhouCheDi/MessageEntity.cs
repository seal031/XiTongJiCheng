using Newtonsoft.Json;
/// <summary>
/// D00
/// </summary>
public class MessCommand
{
    public MessCommand()
    {
        meta = new Head();
        body = new Body();
    }
    public Head meta { get; set; }
    public class Head
    {
        public string sender { get; set; }
        public string msgType { get; set; }
        public string eventType { get; set; }
        public string receiver { get; set; }
        public string sequence { get; set; }
        public string recvSequence { get; set; }
        public string sendTime { get; set; }
        public string recvTime { get; set; }
    }
    public Body body { get; set; }
    public class Body
    {
        /// <summary>
        /// 道口编号
        /// </summary>
        public string parkCode { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>
        public string plateNo { get; set; }
        /// <summary>
        /// 入场取票号/无牌车入场的卡号
        /// </summary>
        public string cardNo { get; set; }
        /// <summary>
        /// 卡片类型编码
        /// </summary>
        public string carTypeCode { get; set; }
        /// <summary>
        /// 卡片类型
        /// </summary>
        public string carType { get; set; }
        /// <summary>
        /// 车底检查时间,进出时间
        /// </summary>
        public string capTime { get; set; }
        /// <summary>
        /// 进出地点
        /// </summary>
        public string capPlace { get; set; }
        /// <summary>
        /// 进出标致,车行方向,0-进 1-出需要进行转义
        /// </summary>
        public string capFlag { get; set; }//车行方向 0表示"出",1表示"进"
        /// <summary>
        /// 抓拍图片
        /// </summary>
        public string imgName { get; set; }
        /// <summary>
        /// 放行备注
        /// </summary>
        public string capRemark { get; set; }
        /// <summary>
        /// 车型编码
        /// </summary>
        public string vechicleShape { get; set; }
        /// <summary>
        /// 车型名称
        /// </summary>
        public string vechicleShapeName { get; set; }
        /// <summary>
        /// 车辆颜色编码
        /// </summary>
        public string vechicleColour { get; set; }
        /// <summary>
        /// 车辆颜色名称
        /// </summary>
        public string vechicleColourName { get; set; }
        /// <summary>
        /// 车底图片
        /// </summary>
        public string vechicleInUvssImg { get; set; }
        /// <summary>
        /// 车底图片路径
        /// </summary>
        public string vechicleInUvssPicpath { get; set; }
        /// <summary>
        /// 车牌图片
        /// </summary>
        public string vechicleInAnprImg { get; set; }
        /// <summary>
        /// 车牌图片路径
        /// </summary>
        public string vechicleInAnprPicpath { get; set; }
        /// <summary>
        /// 车顶图片
        /// </summary>
        public string vechicleInTopImg { get; set; }
        /// <summary>
        /// 车顶图片路径
        /// </summary>
        public string vechicleInTopPicpath { get; set; }
        /// <summary>
        /// 离开时间
        /// </summary>
        public string vechicleOutTime { get; set; }
        /// <summary>
        /// 是否放行
        /// </summary>
        public string vechicleInState { get; set; }
        /// <summary>
        /// 是否放行名称
        /// </summary>
        public string vechicleInStateName { get; set; }
        /// <summary>
        /// 驾驶员名称
        /// </summary>
        public string driverName { get; set; }
        /// <summary>
        /// 驾驶员身份证号
        /// </summary>
        public string driverCard { get; set; }
        /// <summary>
        /// 驾驶员驾驶证号
        /// </summary>
        public string driverLicense { get; set; }
        /// <summary>
        /// 驾驶员工号,驾驶员ID
        /// </summary>
        public string driverJobNo { get; set; }
        /// <summary>
        /// 车辆归口公司
        /// </summary>
        public string vechicleDept { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createDate { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string updateDate { get; set; }
        /// <summary>
        /// 机场三字码
        /// </summary>
        public string airportIata { get; set; }
        /// <summary>
        /// 机场名称
        /// </summary>
        public string airportIame { get; set; }
    }

    public string toJson()
    {
        return JsonConvert.SerializeObject(this, JsonSerializer.IgnoreSerializerSetting);
    }
}
/// <summary>
/// D01
/// </summary>
public class WorkingState
{
    public string carVidiconState { get; set; }//车底摄像机状态
    public float carVidiconTemp { get; set; }//车底摄像机温度
    public string conConnStateVSIO { get; set; }//VSIO控制器连接状态
    public string roadNumber { get; set; }//道口号
    public string inputState { get; set; }//输入状态
    public string toJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}
public class MessageOrder
{
    public string roadNumber { get; set; }//道口编号
    public int maxJoinNumber { get; set; }//最大接入客户端数量
}
static class JsonSerializer
{
    static JsonSerializerSettings jssIgnore = new JsonSerializerSettings();

    public static JsonSerializerSettings IgnoreSerializerSetting
    {
        get
        {
            jssIgnore.NullValueHandling = NullValueHandling.Ignore;
            return jssIgnore;
        }
    }
}
