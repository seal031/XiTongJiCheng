using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


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
            this.eventType = "ACS_ALARM";
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
            alarmClassCode = "AC02";
            alarmClassName = "门禁报警";
            alarmLevelCode = "AL01";
            alarmLevelName = "一级";
        }
        //public string alarmCode { get; set; }
        public string alarmName { get; set; }
        public string alarmClassCode { get; set; }
        public string alarmClassName { get; set; }
        public string alarmTypeCode { get; set; }
        public string alarmTypeName { get; set; }
        public string alarmLevelCode { get; set; }
        public string alarmLevelName { get; set; }
        public string alarmTime { get; set; }
        //public string areaName { get; set; }
        //public string areaId { get; set; }
        //public string alarmAddress { get; set; }
        //public string alarmDescibe { get; set; }
        //public string alarmStateCode { get; set; }
        //public string alarmStateName { get; set; }
        public string alarmEquCode { get; set; }
        public string alarmEquName { get; set; }
        public string equClassCode { get; set; }
        public string equClassName { get; set; }
        //public string alarmSource { get; set; }
        //public string gisX { get; set; }
        //public string gisY { get; set; }
        //public string gisZ { get; set; }
        //public string floorCode { get; set; }
        //public string floorName { get; set; }

    }
    public string toJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class SwipeEntity
{

    public SwipeEntity()
    {
        meta = new Head();
        body = new Body();
    }

    public Head meta { get; set; }
    public class Head
    {
        public Head()
        {
            this.eventType = "ACS_RECORD_CARD";
            this.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.msgType = "RECORD_CARD";
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
            
        }
        //public string areaControllerCode { get; set; }
        //public string doorControllerCode { get; set; }
        public string gateGuardCode { get; set; }
        //public string acsEventCode { get; set; }
        //public string acsIoCode { get; set; }
        public string recordTime { get; set; }
        //public string areaControllerName { get; set; }
        //public string doorControllerName { get; set; }
        public string gateGuardName { get; set; }
        //public string acsEventName { get; set; }
        //public string acsIoEvent { get; set; }
        public string cardCode { get; set; }
        public string empCode { get; set; }
        public string empName { get; set; }
    }

    public string toJson()
    { 
        return JsonConvert.SerializeObject(this);
    }
}

public class PersonEntity
{
    public PersonEntity()
    {
        meta = new Head();
        body = new Body();
    }

    public Head meta { get; set; }
    public class Head
    {
        public Head()
        {
            this.eventType = "FIRST_CARD_DATA_UE";
            this.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            this.msgType = "CARD_DATA";
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
            
        }
        /// <summary>
        /// 部门编号
        /// </summary>
        public string stcStaffCode { get; set; }//
        /// <summary>
        /// 中文姓名
        /// </summary>
        public string stcStaffName { get; set; }//
        /// <summary>
        /// 证件号
        /// </summary>
        public string stcIdNumber { get; set; }//
        /// <summary>
        /// 通行区域
        /// </summary>
        public string stcArea { get; set; }//
        /// <summary>
        /// 通行证号
        /// </summary>
        public string stcStaffCardId { get; set; }//
        /// <summary>
        /// 卡号
        /// </summary>
        public string stcStaffIcId { get; set; }//
        /// <summary>
        /// 照片地址
        /// </summary>
        public string stcPhotoPath { get; set; }
    }

    public string toJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}
