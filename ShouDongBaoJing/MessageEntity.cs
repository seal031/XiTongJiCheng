using AxIPModuleLib;
using System;
namespace IPMALARM
{
    public class MessageEntity
    {
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
                this.sender = "IPMALARM";
                this.msgType = "ALARM";
                this.eventType = "HAND_ALARM";
            }
        }
        public class MessageBody
        {
            public string alarmTime
            {
                get;
                set;
            }
            public string acct
            {
                get;
                set;
            }
            public string cID
            {
                get;
                set;
            }
            public int isNewEvent
            {
                get;
                set;
            }
            public string isZone
            {
                get;
                set;
            }
            public int lPlayback
            {
                get;
                set;
            }
            public string strCode
            {
                get;
                set;
            }
            public string strMac
            {
                get;
                set;
            }
            public string subSystemID
            {
                get;
                set;
            }
            public void buildFromEvent(_ICooMonitorEvents_VistaCIDReportEvent e)
            {
                this.acct = e.acct;
                this.cID = e.cID;
                this.isNewEvent = e.isNewEvent;
                this.isZone = e.isZone.ToString().Length == 1 ? "0" + e.isZone.ToString() : e.isZone.ToString();
                this.lPlayback = e.lPlayback;
                this.strCode = e.strCode;
                this.strMac = e.strMac;
                this.subSystemID = e.subSystemID;
            }
            public void buildFromEvent(_ICooMonitorEvents_NewAlarmEvent e)
            {
                this.isZone = e.lZone.ToString().Length == 1 ? "0" + e.lZone.ToString() : e.lZone.ToString(); ;
                this.strMac = e.strMac;
                this.lPlayback = e.lPlayback;
            }
            public void buildFromEvent(_ICooMonitorEvents_PanelStatusExEvent e)
            {
                this.lPlayback = e.lPlayback;
                this.strMac = e.strMac;  
            }
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
}
