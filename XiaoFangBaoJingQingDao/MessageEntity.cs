using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoFangBaoJingQingDao
{
    public class Msg
    {
        public Msg()
        {
            Head = new Msg.msgHead();
            Body = new Msg.msgBody();
        }

        public class msgHead
        {
            public string Svc_ServiceCode
            {
                get;
                set;
            }
            public string Svc_Version
            {
                get;
                set;
            }
            public string Svc_Sender_Org
            {
                get;
                set;
            }
            public string Svc_Sender
            {
                get;
                set;
            }
            public string Svc_Receiver_Org
            {
                get;
                set;
            }
            public string Svc_Receiver
            {
                get;
                set;
            }
            public string Svc_SerialNumber
            {
                get;
                set;
            }
            public string Svc_SessionId
            {
                get;
                set;
            }
            public string Svc_SendTimeStamp
            {
                get;
                set;
            }
            public msgHead()
            {
                this.Svc_ServiceCode = "FIRE_ALARM_DATA";
                this.Svc_Version = "1.0";
                this.Svc_Sender_Org = "FIRE";
                this.Svc_Sender = "FIRE";
                this.Svc_Receiver_Org = string.Empty;
                this.Svc_Receiver = "";
                this.Svc_SerialNumber = "";
                this.Svc_SessionId = DateTime.Now.ToString("yyyyMMddHHmmss");
                this.Svc_SendTimeStamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            }
        }
        public class msgBody
        {
            public string Register
            {
                get;
                set;
            }
            public string Event
            {
                get;
                set;
            }
        }
        public Msg.msgHead Head
        {
            get;
            set;
        }
        public Msg.msgBody Body
        {
            get;
            set;
        }
        public string toXml()
        {
            return XmlParser.toXml(this);
        }
    }
}
