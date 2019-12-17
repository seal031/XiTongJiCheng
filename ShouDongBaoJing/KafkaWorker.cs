using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
namespace IPMALARM
{
    public class KafkaWorker
    {
        private string brokerList;
        private string topicMsg;
        private string topicLog;
        private Producer<Null, string> msgProducer;
        private Producer<Null, string> logProducer;
        public KafkaWorker()
        {
            this.brokerList = ConfigWorker.GetConfigValue("kafkaUrl");
            this.topicMsg = ConfigWorker.GetConfigValue("topicAlarm");
            this.topicLog = ConfigWorker.GetConfigValue("topicLog");
            Dictionary<string, object> config = new Dictionary<string, object>
            {

                {
                    "bootstrap.servers",
                    this.brokerList
                }
            };
            this.msgProducer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8));
            this.logProducer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8));
        }
        public void sendMsg(string msg)
        {
            try
            {
                this.msgProducer.ProduceAsync(this.topicMsg, null, msg);
                this.msgProducer.Flush(TimeSpan.FromSeconds(1.0));
            }
            catch (Exception var_0_33)
            {
                throw;
            }
        }
        public void sendLog(string log)
        {
            try
            {
                this.logProducer.ProduceAsync(this.topicLog, null, log);
                this.logProducer.Flush(TimeSpan.FromSeconds(1.0));
            }
            catch (Exception var_0_33)
            {
                throw;
            }
        }
    }
}
