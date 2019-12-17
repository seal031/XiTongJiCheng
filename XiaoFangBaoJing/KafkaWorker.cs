using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace XiaoFangBaoJing
{
    public class KafkaWorker
    {
        static string brokerList = ConfigWorker.GetConfigValue("kafkaUrl");
        static string messageTopicName = ConfigWorker.GetConfigValue("topicAlarm");
        static IProducer<Null, string> producerAlarm = null;
        static ProducerConfig configAlarm = null;

        public static async void sendMessage(string message)
        {
            LogHelper.WriteLog(message);
            if (configAlarm == null) { configAlarm = new ProducerConfig { BootstrapServers = brokerList }; }
            //var config = new ProducerConfig { BootstrapServers = brokerList };
            try
            {
                if (producerAlarm == null) { producerAlarm = new ProducerBuilder<Null, string>(configAlarm).Build(); }
                {
                    var dr = await producerAlarm.ProduceAsync(messageTopicName, new Message<Null, string> { Value = message });
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("kafka error  " + e.Message);
            }
        }
    }
}