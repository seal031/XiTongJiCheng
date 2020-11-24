using Newtonsoft.Json;

/// <summary>
/// 报警消息
/// </summary>
public class AlarmEntity
{
    public AlarmEntity()
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
        //public string alarmCode { get; set; }
        public string alarmName { get; set; }
        public string alarmClassCode { get; set; }
        public string alarmClassName { get; set; }
        //public string alarmTypeCode { get; set; }
        //public string alarmTypeName { get; set; }
        //public string alarmLevelCode { get; set; }
        //public string alarmLevelName { get; set; }
        public string alarmTime { get; set; }
        //public string areaName { get; set; }
        //public string alarmAddress { get; set; }
        //public string alarmDescibe { get; set; }
        //public string alarmStateCode { get; set; }
        //public string alarmStateName { get; set; }
        public string alarmEquCode { get; set; }
        //public string alarmEquName { get; set; }
        //public string equClassCode { get; set; }
        //public string equClassName { get; set; }
        //public string alarmSource { get; set; }
        //public string gisX { get; set; }
        //public string gisY { get; set; }
        //public string gisZ { get; set; }
        //public string floorCode { get; set; }
        //public string floorName { get; set; }
        public string alarmStateCode { get; set; }
        public string alarmStateName { get; set; }
        public string airportIata { get; set; }
        public string airportName { get; set; }
        public string alarmNameCode { get; set; }
    }
    public string toJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}

/// <summary>
/// 设备状态信息
/// </summary>
public class DeviceStateEntity
{
    public DeviceStateEntity()
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
        public string equCode { get; set; }
        public string timeStateId { get; set; }
        public string operateTime { get; set; }
        public string timeStateName { get; set; }
    }
    public string toJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}

/// <summary>
/// 刷卡信息
/// </summary>
public class AccessEntity
{
    public AccessEntity()
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
        /// 卡号
        /// </summary>
        public string cardNumber { get; set; }
        /// <summary>
        /// 卡状态
        /// </summary>
        public string cardStatus { get; set; }
        /// <summary>
        /// 卡类型
        /// </summary>
        public string cardType { get; set; }
        /// <summary>
        /// 卡类型名称
        /// </summary>
        public string cardTypeName { get; set; }
        /// <summary>
        /// 通道编码
        /// </summary>
        public string channelCode { get; set; }
        /// <summary>
        /// 通道名称
        /// </summary>
        public string channelName { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string deptName { get; set; }
        /// <summary>
        /// 设备编码
        /// </summary>
        public string deviceCode { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string deviceName { get; set; }
        /// <summary>
        /// 进出标志 1：进2：出3：进/出
        /// </summary>
        public string enterOrExit { get; set; }
        public string id { get; set; }
        /// <summary>
        /// 开门结果1：成功0：失败
        /// </summary>
        public string openResult { get; set; }
        /// <summary>
        /// 开门类型
        /// </summary>
        public string openType { get; set; }
        /// <summary>
        /// 开门类型名称
        /// </summary>
        public string openTypeName { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string paperNumber { get; set; }
        /// <summary>
        /// 人员编号
        /// </summary>
        public string personCode { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>
        public string personId { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        public string personName { get; set; }
        /// <summary>
        /// 刷卡时间
        /// </summary>
        public string swingTime { get; set; }
    }
    public string toJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}

/// <summary>
/// 指令信息
/// </summary>
public class CommandEntity
{
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
        public string equId { get; set; }
    }

    public static CommandEntity fromJson(string json)
    {
        return JsonConvert.DeserializeObject<CommandEntity>(json);
    }
}
