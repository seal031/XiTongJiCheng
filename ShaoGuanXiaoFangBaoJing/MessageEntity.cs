using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoFangBaoJing
{
    public class MessageEntity
    {
        public MessageEntity()
        {
            meta = new XiaoFangBaoJing.MessageEntity.MessageHead();
            body = new XiaoFangBaoJing.MessageEntity.MessageBody();
        }

        public class MessageHead
        {
            public string sender
            {
                get;
                set;
            }
            public string receiver
            {
                get;
                set;
            }
            public string sequence
            {
                get;
                set;
            }
            public string recvSequence
            {
                get;
                set;
            }
            public string sendTime
            {
                get;
                set;
            }
            public string recvTime
            {
                get;
                set;
            }
            public string forward
            {
                get;
                set;
            }
            public string msgType
            {
                get;
                set;
            }
            public string eventType
            {
                get;
                set;
            }
            public MessageHead()
            {
                this.sender = "FIRE";
                this.msgType = "ALARM";
                this.eventType = "FIRE_ALARM";
            }
        }
        public class MessageBody
        {
            public string alarmTime
            {
                get;
                set;
            }
            public string alarmEquCode
            {
                get;
                set;
            }
            public string alarmTypeCode
            {
                get;
                set;
            }
            public string alarmTypeName
            {
                get;
                set;
            }
            public string alarmClassCode
            {
                get;
                set;
            }
            public string alarmClassName
            {
                get;
                set;
            }
            public string alarmLevelCode
            {
                get;
                set;
            }
            public string alarmLevelName
            {
                get;
                set;
            }

        }
        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }
        public MessageEntity.MessageHead meta
        {
            get;
            set;
        }
        public MessageEntity.MessageBody body
        {
            get;
            set;
        }
    }

    public class Device
    {
        public string index { get; set; }
        public string name { get; set; }
    }
}
