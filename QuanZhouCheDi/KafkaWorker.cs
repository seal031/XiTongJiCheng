using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using QuanZhouCheDi;

public class KafkaWorker
{
    static string brokerList = ConfigWorker.GetConfigValue("kafkaUrl");
    static string messageTopicName = ConfigWorker.GetConfigValue("topicMessCommand");
    static string commandTopicName = ConfigWorker.GetConfigValue("topicCommand");
    static string consumerGroupId = ConfigWorker.GetConfigValue("consumerGroupId");
    static IProducer<Null, string> producerMess=null;
    static IConsumer<Ignore, string> consumerCommand = null;
    static ProducerConfig configMess = null;
    static ConsumerConfig configCommand = null;
    //static Action<DeliveryReport<Null, string>> handler = r =>
    //   FileWorker.WriteLog(!r.Error.IsError
    //       ? $"Delivered message to {r.TopicPartitionOffset}"
    //       : $"Delivery Error: {r.Error.Reason}");
    static Action<DeliveryReport<Null, string>> handler = r =>
       FileWorker.LogHelper.WriteLog(!r.Error.IsError
           ? $"Delivered message to {r.TopicPartitionOffset}"
           : $"Delivery Error: {r.Error.Reason}");

    public static void sendCarRecordMessage(string message)
    {
        int len = message.Length;
        //if (configMess == null) { configMess = new ProducerConfig { BootstrapServers = brokerList}; }
        if (configMess == null)
        {
            ClientConfig clientfig = new ClientConfig();
            clientfig.BootstrapServers = brokerList;
            //clientfig.MessageMaxBytes = 92914560;
            clientfig.MessageMaxBytes = 100000000;
            //clientfig.MessageCopyMaxBytes = 1000000000;
            //clientfig.ReceiveMessageMaxBytes = 800000000;
            //clientfig.SocketSendBufferBytes = 100000000;
            //clientfig.MetadataRequestTimeoutMs = 1000 * 60;
            configMess = new ProducerConfig(clientfig);
            //configMess.
            //configMess.MessageTimeoutMs = 300000;
        }
        FileWorker.LogHelper.WriteLog("正在向kafka发送MessComm消息:" + message);
        try
        {
            if (producerMess == null)
            {
                producerMess = new ProducerBuilder<Null, string>(configMess).Build();
            }
            producerMess.Produce(messageTopicName, new Message<Null, string> { Value = message }, handler);
            producerMess.Flush(TimeSpan.FromSeconds(5));
        }
        catch (Exception e)
        {
            FileWorker.LogHelper.WriteLog("alarm error  " + e.Message);
        }
    }

    public delegate void GetMessage(string message);
    public static event GetMessage OnGetMessage;

    public static void startGetMessage()
    {
        configCommand = new ConsumerConfig
        {
            GroupId = consumerGroupId,
            BootstrapServers = brokerList,
            AutoOffsetReset = AutoOffsetReset.Latest
        };
        using (consumerCommand = new ConsumerBuilder<Ignore, string>(configCommand).Build())
        {
            consumerCommand.Subscribe(commandTopicName);
            CancellationTokenSource cts = new CancellationTokenSource();
            //Console.CancelKeyPress += (_, e) => {
            //    e.Cancel = true; // prevent the process from terminating.
            //    cts.Cancel();
            //};
            try
            {
                while (true)
                {
                    try
                    {
                        var cr = consumerCommand.Consume(cts.Token);
                        OnGetMessage(cr.Value);
                    }
                    catch (ConsumeException e)
                    {
                        FileWorker.LogHelper.WriteLog($"Error occured: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException e)
            {
                FileWorker.LogHelper.WriteLog($"Error occured1: {e.Message}");
                consumerCommand.Close();
            }
        }
    }
}
public class KafkaTest
{
    public static void SendMessCommand(string message)
    {
        FileWorker.LogHelper.WriteLog("正在向kafka发送MessComm:" + message);
    }
}
