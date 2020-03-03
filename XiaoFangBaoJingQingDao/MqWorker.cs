using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBMMQ_LIB;

namespace XiaoFangBaoJingQingDao
{
    public class MqWorker
    {
        private MQ_SDK mqHelper;
        private string mqChannel;
        private string mqQueue;
        private string mqUser;
        private string mqPwd;
        List<ConnectorConfig> list = new List<ConnectorConfig>();

        public MqWorker()
        {
            try
            {
                mqChannel = ConfigWorker.GetConfigValue("mqChannel");
                mqQueue = ConfigWorker.GetConfigValue("mqQueue");
                mqUser = ConfigWorker.GetConfigValue("mqUser");
                mqPwd = ConfigWorker.GetConfigValue("mqPwd");
                mqHelper = MQ_SDK.createInstance();
                list.Add(new ConnectorConfig(mqChannel, mqQueue));
                mqHelper.initial(list);
                mqHelper.login(mqUser, mqPwd, mqQueue);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("MQ初始化错误：" + ex.Message);
            }
        }

        public void sendMsg(string msg)
        {
            LogHelper.WriteLog("正在发送消息" + msg);
            try
            {
                var result = mqHelper.sendMSG(msg, mqChannel, mqQueue);
                if (result != "13")
                {
                    LogHelper.WriteLog("MQ发送消息失败：返回值为" + result);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("MQ发送消息错误：" + ex.Message);
            }
        }
    }
}
