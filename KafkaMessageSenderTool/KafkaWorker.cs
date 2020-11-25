using Confluent.Kafka;
using System;
using System.Windows.Forms;

namespace KafkaMessageSenderTool
{
    public class KafkaWorker
    {
        public static string brokerList = "";
        public static string messageTopicName = "";
        //public static string commandTopicName = "";
        //public static string consumerGroupId ="";
        static IProducer<Null, string> producerAlarm = null;
        //static IConsumer<Ignore, string> consumerCommand = null;
        static ProducerConfig configAlarm = null;
        //static ConsumerConfig configCommand = null;
        //static Action<DeliveryReport<Null, string>> handler = r =>
        //   FileWorker.WriteLog(!r.Error.IsError
        //       ? $"Delivered message to {r.TopicPartitionOffset}"
        //       : $"Delivery Error: {r.Error.Reason}");
        static Action<DeliveryReport<Null, string>> handlerAlert = r =>
           MessageBox.Show(!r.Error.IsError
               ? $"发送成功，消息Offset是 {r.TopicPartitionOffset}"
               : $"发送失败，原因: {r.Error.Reason}");
        static Action<DeliveryReport<Null, string>> handler = r =>
           Form1.SetrichTextBox(!r.Error.IsError
               ? $"发送成功，消息Offset是 {r.TopicPartitionOffset}"
               : $"发送失败，原因: {r.Error.Reason}");

        public static bool init()
        {
            try
            {
                //if (configAlarm == null)
                {
                    ClientConfig clientconfig = new ClientConfig();
                    clientconfig.BootstrapServers = brokerList;
                    clientconfig.MessageMaxBytes = 92914560;
                    //clientfig.MessageCopyMaxBytes = 1000000000;
                    //clientfig.ReceiveMessageMaxBytes = 800000000;
                    //clientfig.SocketSendBufferBytes = 100000000;
                    //clientfig.MetadataRequestTimeoutMs = 1000 * 60;
                    //configAlarm = new ProducerConfig { BootstrapServers = brokerList,QueueBufferingMaxKbytes= 10485760 };
                    configAlarm = new ProducerConfig(clientconfig);
                }
                //if (producerAlarm == null)
                {
                    producerAlarm = new ProducerBuilder<Null, string>(configAlarm).Build();
                }
                MessageBox.Show("初始化成功");
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("初始化过程中出现异常  " + e.Message);
                return false;
            }
        }

        public static void sendAlarmMessage(string message, bool writeLog = false,bool alert=false)
        {
            if (writeLog)
            { Form1.SetrichTextBox("正在向kafka发送alarm消息：" + message); }
            //FileWorker.LogHelper.WriteLog("正在向kafka发送alarm消息" + message);
            try
            {
                if (configAlarm == null || producerAlarm == null)
                {
                    MessageBox.Show("请先点击初始化按钮");
                }
                else
                {
                    //var dr = await producerAlarm.ProduceAsync(deviceTopicName, new Message<Null, string> { Value = message });
                    //FileWorker.WriteLog("消息" + message + "的发送状态为：" + dr.Status);
                    if (alert)
                    {
                        producerAlarm.Produce(messageTopicName, new Message<Null, string> { Value = message }, handlerAlert);
                        producerAlarm.Flush(TimeSpan.FromSeconds(3));
                    }
                    else
                    {
                        producerAlarm.Produce(messageTopicName, new Message<Null, string> { Value = message }, handler);
                        producerAlarm.Flush(TimeSpan.FromSeconds(3));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("发送过程中出现异常  " + e.Message);
            }
        }
    }
}
