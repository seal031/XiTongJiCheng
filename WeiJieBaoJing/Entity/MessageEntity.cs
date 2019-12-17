using FSI.DeviceSDK;
using FSI.DeviceSDK.FiberDefenderDevice;
using FSI.DeviceSDK.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiJieBaoJing.Entity
{
    public class MessageBase
    {
        protected const string sender = "FiberDefender";
        protected const string receiver = "AMS";
        public Head meta { get; set; }
        public MessageBase()
        {
            meta = new Entity.MessageBase.Head();
        }
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
    }

    public class MessageDevice : MessageBase
    {
        const string equClassCode = "EC06";
        const string equClassName = "围界";
        public MessageDevice(IFiberDefenderAPUDevice device)
        {
            meta.sender = sender;
            meta.msgType = "EQU";
            meta.eventType = "EQU_INFO_UE";
            meta.receiver = receiver;
            meta.sequence = "";
            meta.recvSequence = "";
            meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            meta.recvTime = "";
            body = new Entity.MessageDevice.Body();
            body.areaId = "";
            body.areaName = "";
            body.attachDeptId="";
            body.attachDeptName = "";
            body.equClassCode = equClassCode;
            body.equClassName = equClassName;
            body.equCode = "";
            body.equDetail = device.Description;
            body.equIp = device.IPAddress.ToString();
            body.equName = device.Name;
            body.equSystemCode = "";
            body.equSystemName = "";
            body.equTypeCode = "";
            body.equTypeName = "";
            body.floorCode = "";
            body.floorName = "";
            body.gisX = "";
            body.gisY = "";
            body.gisZ = "";
            body.managerDeptId = "";
            body.managerDeptName = "";
            body.operation = "";
            body.timeStateId = "";
            body.timeStateName = "";
        }
        public MessageDevice(IGeneralDevice subDevice)
        {
            meta.sender = sender;
            meta.msgType = "Device";
            meta.eventType = "Device";
            body = new Entity.MessageDevice.Body();
            body.areaId = "";
            body.areaName = "";
            body.attachDeptId = "";
            body.attachDeptName = "";
            body.equClassCode = "";
            body.equClassName = "";
            body.equCode = subDevice.Name;
            body.equDetail = subDevice.Description;
            body.equIp = "";
            body.equName = subDevice.Name;
            body.equSystemCode = "";
            body.equSystemName = "";
            body.equTypeCode = "";
            body.equTypeName = "";
            body.floorCode = "";
            body.floorName = "";
            body.gisX = "";
            body.gisY = "";
            body.gisZ = "";
            body.managerDeptId = "";
            body.managerDeptName = "";
            body.operation = "";
            body.timeStateId = "";
            body.timeStateName = "";
        }

        public Body body { get; set; }

        public class Body
        {
            public string equCode { get; set; }
            public string equName { get; set; }
            public string equClassCode { get; set; }
            public string equClassName { get; set; }
            public string equTypeCode { get; set; }
            public string equTypeName { get; set; }
            public string equIp { get; set; }
            public string equSystemCode { get; set; }
            public string equSystemName { get; set; }
            public string equDetail { get; set; }
            public string attachDeptId { get; set; }
            public string attachDeptName { get; set; }
            public string managerDeptId { get; set; }
            public string managerDeptName { get; set; }
            public string areaId { get; set; }
            public string areaName { get; set; }
            public string gisX { get; set; }
            public string gisY { get; set; }
            public string gisZ { get; set; }
            public string floorCode { get; set; }
            public string floorName { get; set; }
            public string timeStateId { get; set; }
            public string timeStateName { get; set; }
            public string operation { get; set; }
        }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class MessageAlarm : MessageBase
    {
        const string alarmClassCode = "AC05";
        const string alarmClassName = "围界报警";
        public MessageAlarm(string device,string eventType,string alarmTypeCode,string alarmTypeName)
        {
            meta.sender = sender;
            meta.msgType = "ALARM";
            meta.eventType = eventType;
            meta.receiver = receiver;
            meta.sequence = "";
            meta.recvSequence = "";
            meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            meta.recvTime = "";
            body = new Entity.MessageAlarm.Body();
            body.alarmAddress = "";
            body.alarmClassCode = alarmClassCode;
            body.alarmClassName = alarmClassName;
            body.alarmCode = "";
            body.alarmDescibe = "";
            body.alarmEquId = device;
            body.alarmEquName = device;
            body.alarmLevelCode = "";
            body.alarmLevelName = "";
            body.alarmName = "";
            body.alarmSource = "";
            body.alarmStateCode = "";
            body.alarmStateName = "";
            body.alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");// alarm.Time.ToString("yyyy-MM-dd HH:mm:ss");
            body.alarmTypeCode = alarmTypeCode;
            body.alarmTypeName = alarmTypeName;
            body.areaName = "";
            body.equClassCode = "";
            body.equClassName = "";
            body.floorCode = "";
            body.floorName = "";
            body.gisX = "";
            body.gisY = "";
            body.gisZ = "";
        }
        public Body body { get; set; }

        public class Body
        {
            public string alarmCode { get; set; }
            public string alarmName { get; set; }
            public string alarmClassCode { get; set; }
            public string alarmClassName { get; set; }
            public string alarmTypeCode { get; set; }
            public string alarmTypeName { get; set; }
            public string alarmLevelCode { get; set; }
            public string alarmLevelName { get; set; }
            public string alarmTime { get; set; }
            public string areaName { get; set; }
            public string alarmAddress { get; set; }
            public string alarmDescibe { get; set; }
            public string alarmStateCode { get; set; }
            public string alarmStateName { get; set; }
            public string alarmEquId { get; set; }
            public string alarmEquName { get; set; }
            public string equClassCode { get; set; }
            public string equClassName { get; set; }
            public string alarmSource { get; set; }
            public string gisX { get; set; }
            public string gisY { get; set; }
            public string gisZ { get; set; }
            public string floorCode { get; set; }
            public string floorName { get; set; }

        }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class MessageCommand
    {
        public Body body { get; set; }
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

        public class Body
        {
            public string alarmCode { get; set; }
            public string alarmName { get; set; }
            public string alarmClassCode { get; set; }
            public string alarmClassName { get; set; }
            public string alarmTypeCode { get; set; }
            public string alarmTypeName { get; set; }
            public string alarmLevelCode { get; set; }
            public string alarmLevelName { get; set; }
            public string alarmTime { get; set; }
            public string areaName { get; set; }
            public string alarmAddress { get; set; }
            public string alarmDescibe { get; set; }
            public string alarmStateCode { get; set; }
            public string alarmStateName { get; set; }
            public string alarmEquId { get; set; }
            public string alarmEquName { get; set; }
            public string equClassCode { get; set; }
            public string equClassName { get; set; }
            public string alarmSource { get; set; }
            public string gisX { get; set; }
            public string gisY { get; set; }
            public string gisZ { get; set; }
            public string floorCode { get; set; }
            public string floorName { get; set; }
            //关闭：0；打开：1
            public string openFlag { get; set; }

        }

        public static MessageCommand fromJson(string json)
        {
            return JsonConvert.DeserializeObject<MessageCommand>(json);
        }
    }

    public class PlanMessage
    {
        public Plan plan { get; set; }
        public List<Equ> equs { get; set; }
        public PlanMessage()
        {
            plan = new Plan();
            equs = new List<Equ>();
        }

        public class Plan
        {
            public string startTM { get; set; }
            public string endTM { get; set; }
            public List<string> Mon { get; set; }
            public List<string> Tue { get; set; }
            public List<string> Wed { get; set; }
            public List<string> Thu { get; set; }
            public List<string> Fri { get; set; }
            public List<string> Sat { get; set; }
            public List<string> Sun { get; set; }
        }

        public class Equ
        {
            public string equName { get; set; }
            public string equStatus { get; set; }
        }

        internal static PlanMessage fromJson(string json)
        {
            return JsonConvert.DeserializeObject<PlanMessage>(json);
        }
    }

    public class MessageDeviceChange : MessageBase
    {
        const string msgType = "EQU_STATUS";
        const string eventType = "EQU_STATUS_UE";
        public MessageDeviceChange(string timeStateId, string timeStateName,string equIp)
        {
            meta.sender = sender;
            meta.msgType = msgType;
            meta.eventType = eventType;
            meta.receiver = receiver;
            meta.sequence = "";
            meta.recvSequence = "";
            meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            meta.recvTime = "";
            body = new Body();
            body.createDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            body.equCode = "";
            body.timeStateId = timeStateId;
            body.timeStateName = timeStateName;
            body.equIp = equIp;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="timeStateId"></param>
        /// <param name="timeStateName"></param>
        /// <param name="equIp">参数无意义，只是为了跟上一个重载方法区别开</param>
        public MessageDeviceChange(string device,string timeStateId,string timeStateName,string equIp)
        {
            meta.sender = sender;
            meta.msgType = msgType;
            meta.eventType = eventType;
            meta.receiver = receiver;
            meta.sequence = "";
            meta.recvSequence = "";
            meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            meta.recvTime = "";
            body = new Body();
            body.createDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            body.equCode = device;
            body.timeStateId = timeStateId;
            body.timeStateName = timeStateName;
            body.equIp = "";
        }

        public Body body { get; set; }

        public class Body
        {
            public string equCode { get; set; }
            public string createDate { get; set; }
            public string timeStateId { get; set; }
            public string timeStateName { get; set; }

            public string equIp { get; set; }
        }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
