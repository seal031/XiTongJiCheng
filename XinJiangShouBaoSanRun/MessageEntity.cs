using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


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
/// <summary>
/// 发送至kafka的消息
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
        public Head()
        {
            this.eventType = "HAND_ALARM";
            this.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.msgType = "ALARM";
            this.sequence = Guid.NewGuid().ToString("N");
        }
        //public string sender { get; set; }
        public string msgType { get; set; }
        public string eventType { get; set; }
        //public string receiver { get; set; }
        public string sequence { get; set; }
        //public string recvSequence { get; set; }
        public string sendTime { get; set; }
        //public string recvTime { get; set; }
    }
    public Body body { get; set; }

    public class Body
    {
        public Body()
        {
            alarmClassCode = "AC03";
            alarmClassName = "手动报警事件";
        }
        public string alarmName { get; set; }
        public string alarmClassCode { get; set; }
        public string alarmClassName { get; set; }
        public string alarmTime { get; set; }
        public string alarmEquCode { get; set; }
        public string alarmStateCode { get; set; }
        public string alarmStateName { get; set; }
        public string airportIata { get; set; }
        public string airportName { get; set; }
        public string alarmNameCode { get; set; }
    }
    public string toJson()
    {
        return JsonConvert.SerializeObject(this, JsonSerializer.IgnoreSerializerSetting);
    }
}

public class DeviceEntity
{
    public Head meta { get; set; }
    public Body body { get; set; }
    public DeviceEntity()
    {
        meta = new Head();
        body = new Body();
    }
    public class Head
    {
        public Head()
        {
            this.eventType = "";
            this.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.msgType = "";
            this.sequence = Guid.NewGuid().ToString("N");
        }
        public string sender { get; set; }
        public string msgType { get; set; }
        public string eventType { get; set; }
        public string receiver { get; set; }
        public string sequence { get; set; }
        public string recvSequence { get; set; }
        public string sendTime { get; set; }
    }

    public class Body
    {
        public Body()
        {

        }
        public string equCode { get; set; }         //设备编码
        public string equName { get; set; }         //设备名称
        public string equClassCode { get; set; }    //设备分类代码
        public string equClassName { get; set; }    //设备分类名称
        public string equTypeCode { get; set; }     //设备类型
        public string equTypeName { get; set; }     //设备类型名称
        public string equIp { get; set; }           //设备ip
        public string equSystemCode { get; set; }   //归属系统
        public string equDetail { get; set; }       //设备描述
        public string equWarranty { get; set; }     //质保期
        public string equFacturer { get; set; }     //设备厂家
        public string gisX { get; set; }            //经度
        public string gisY { get; set; }            //纬度
        public string floorCode { get; set; }       //所在楼层
        public string floorName { get; set; }       //所在楼层名称
        public string timeStateId { get; set; }     //实时状态ID
        public string timeStateName { get; set; }   //实时状态名称
        public string gateFlag { get; set; }        //进出标识
        public string gateFlagName { get; set; }    //进出标识
        public string parentId { get; set; }        //上级id
    }
    public string toJson()
    {
        return JsonConvert.SerializeObject(this, JsonSerializer.IgnoreSerializerSetting);
    }
}

/// <summary>
/// 发送至kafka的设备状态变化消息
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
        public Head()
        {
            this.eventType = "HAND_STATUS";
            this.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.sender = "HAND_ALARM";
            this.msgType = "EQU_STATUS";
            this.sequence = Guid.NewGuid().ToString("N");
        }
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
        public string createDate { get; set; }
        public string timeStateName { get; set; }
    }
    public string toJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}
