﻿using System;
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
            this.eventType = "HAND_ALARM";
            this.sendTime = DateTime.Now.ToString("yyyyMMddhhmmss");
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
            alarmClassName = "手动报警";
            alarmLevelCode = "AL01";
            alarmLevelName = "一级";
        }
        //public string alarmCode { get; set; }
        //public string alarmName { get; set; }
        public string alarmClassCode { get; set; }
        public string alarmClassName { get; set; }
        //public string alarmTypeCode { get; set; }
        //public string alarmTypeName { get; set; }
        public string alarmLevelCode { get; set; }
        public string alarmLevelName { get; set; }
        public string alarmTime { get; set; }
        //public string areaName { get; set; }
        public string alarmAddress { get; set; }
        public string alarmDescibe { get; set; }
        //public string alarmStateCode { get; set; }
        //public string alarmStateName { get; set; }
        public string alarmEquCode { get; set; }
        public string alarmEquName { get; set; }
        //public string equClassCode { get; set; }
        //public string equClassName { get; set; }
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
