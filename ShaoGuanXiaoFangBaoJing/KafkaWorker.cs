using Confluent.Kafka;
using ShaoGuanoXiaoFangBaoJing;
using System;

namespace ShaoGuanXiaoFangBaoJing
{
    public class KafkaWorker
    {
        static string brokerList = ConfigWorker.GetConfigValue("kafkaUrl");
        static string messageTopicName = ConfigWorker.GetConfigValue("topicAlarm");
        static IProducer<Null, string> producerAlarm = null;
        static ProducerConfig configAlarm = null;

        static Action<DeliveryReport<Null, string>> handler = r =>
           LogHelper.WriteLog(!r.Error.IsError
               ? $"Delivered message to {r.TopicPartitionOffset}"
               : $"Delivery Error: {r.Error.Reason}");

        public static async void sendMessage(string message)
        {
            if (configAlarm == null) { configAlarm = new ProducerConfig { BootstrapServers = brokerList }; }
            LogHelper.WriteLog("正在向kafka发送alarm消息" + message);
            try
            {
                if (producerAlarm == null) { producerAlarm = new ProducerBuilder<Null, string>(configAlarm).Build(); }
                {
                    producerAlarm.Produce(messageTopicName, new Message<Null, string> { Value = message }, handler);
                    producerAlarm.Flush(TimeSpan.FromSeconds(5));
                }
            }
            catch (Exception e)
            {
                LogHelper.WriteLog("kafka error  " + e.Message);
            }
        }
    }
}