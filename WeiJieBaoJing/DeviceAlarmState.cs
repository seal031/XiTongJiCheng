using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiJieBaoJing
{
    public class DeviceAlarmState
    {
        public DeviceAlarmState()
        {
            if (int.TryParse(ConfigWorker.GetConfigValue("alarmTimespan"), out alarmTimespan))
            { }
            else
            {
                alarmTimespan = 30;
            }
            hadSetUserControl = false;
        }
        private int alarmTimespan = 0;
        /// <summary>
        /// 设备名称(防区设备）
        /// </summary>
        public string deviceName { get; set; }
        private DateTime lastAlarmTime { get; set; }
        /// <summary>
        /// 是否可向web端发送消息。
        /// </summary>
        public bool canOutputAlarm
        {
            get
            {
                if (lastAlarmTime == null)
                {
                    lastAlarmTime = DateTime.Now;
                    return true;
                }
                else
                {
                    if ((DateTime.Now - lastAlarmTime).TotalSeconds >= alarmTimespan)
                    {
                        lastAlarmTime = DateTime.Now;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// 是否已经设置了“用户可控”
        /// </summary>
        public bool hadSetUserControl { get; set; }

        private bool isOnline = true;
        private object isOnlineLock = new object();
        /// <summary>
        /// 设备是否在线
        /// </summary>
        public bool IsOnline
        {
            get
            {
                lock (isOnlineLock)
                {
                    return isOnline;
                }
            }
            set
            {
                lock (isOnlineLock)
                {
                    isOnline = value;
                }
            }
        }

        private bool isSending = false;
        private object isSendingLock = new object();
        /// <summary>
        /// 是否正在向继电器输出“开”
        /// </summary>
        public bool IsSending
        {
            get
            {
                lock (isSendingLock)
                {
                    return isSending;
                }
            }
            set
            {
                lock (isSendingLock)
                {
                    isSending = value;
                }
            }
        }
        
    }
}
