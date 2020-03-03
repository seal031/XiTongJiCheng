using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

public class KafkaWorker
{
    static string brokerList = ConfigWorker.GetConfigValue("kafkaUrl");
    static string messageTopicName = ConfigWorker.GetConfigValue("topicAlarm");
    //static string deviceTopicName= ConfigWorker.GetConfigValue("topicDevice");
    //static string commandTopicName = ConfigWorker.GetConfigValue("topicCommand");
    //static string consumerGroupId = ConfigWorker.GetConfigValue("consumerGroupId");
    static IProducer<Null, string> producerAlarm=null;
    //static IProducer<Null, string> producerDevice= null;
    //static IConsumer<Ignore, string> consumerCommand = null;
    static ProducerConfig configAlarm = null;
    //static ProducerConfig configDevice = null;
    //static ConsumerConfig configCommand = null;
    //static Action<DeliveryReport<Null, string>> handler = r =>
    //   FileWorker.WriteLog(!r.Error.IsError
    //       ? $"Delivered message to {r.TopicPartitionOffset}"
    //       : $"Delivery Error: {r.Error.Reason}");
    static Action<DeliveryReport<Null, string>> handler = r =>
       FileWorker.LogHelper.WriteLog(!r.Error.IsError
           ? $"Delivered message to {r.TopicPartitionOffset}"
           : $"Delivery Error: {r.Error.Reason}");

    public static void sendAlarmMessage(string message)
    {
        if (configAlarm == null) { configAlarm = new ProducerConfig { BootstrapServers = brokerList}; }
        FileWorker.LogHelper.WriteLog("正在向kafka发送alarm消息" + message);
        try
        {
            if (producerAlarm == null)
            {
                producerAlarm = new ProducerBuilder<Null, string>(configAlarm).Build();
            }
            //var dr = await producerAlarm.ProduceAsync(deviceTopicName, new Message<Null, string> { Value = message });
            //FileWorker.WriteLog("消息" + message + "的发送状态为：" + dr.Status);
            producerAlarm.Produce(messageTopicName, new Message<Null, string> { Value = message }, handler);
            producerAlarm.Flush(TimeSpan.FromSeconds(5));
        }
        catch (Exception e)
        {
            FileWorker.LogHelper.WriteLog("alarm error  " + e.Message);
        }
    }
    //public static void sendDeviceMessage(string message)
    //{
    //    if (configDevice == null) { configDevice = new ProducerConfig { BootstrapServers = brokerList }; }
    //    FileWorker.WriteLog("正在向kafka发送device消息" + message);
    //    try
    //    {
    //        if (producerDevice == null) { producerDevice = new ProducerBuilder<Null, string>(configDevice).Build(); }
    //        {
    //            //var dr = await producerDevice.ProduceAsync(deviceTopicName, new Message<Null, string> { Value = message });
    //            producerDevice.Produce(deviceTopicName, new Message<Null, string> { Value = message }, handler);
    //            producerAlarm.Flush(TimeSpan.FromSeconds(5));
    //        }
    //    }
    //    catch (Exception e)
    //    {
    //        FileWorker.PrintLog("device error  " + e.Message);
    //        FileWorker.WriteLog("device error  " + e.Message);
    //    }
    //}

    //public delegate void GetMessage(string message);
    //public static event GetMessage OnGetMessage;

    //public static void startGetMessage()
    //{
    //    configCommand = new ConsumerConfig
    //    {
    //        GroupId = consumerGroupId,
    //        BootstrapServers = brokerList,
    //        AutoOffsetReset = AutoOffsetReset.Latest
    //    };
    //    using (consumerCommand = new ConsumerBuilder<Ignore, string>(configCommand).Build())
    //    {
    //        consumerCommand.Subscribe(commandTopicName);
    //        CancellationTokenSource cts = new CancellationTokenSource();
    //        //Console.CancelKeyPress += (_, e) => {
    //        //    e.Cancel = true; // prevent the process from terminating.
    //        //    cts.Cancel();
    //        //};

    //        try
    //        {
    //            while (true)
    //            {
    //                try
    //                {
    //                    var cr = consumerCommand.Consume(cts.Token);
    //                    OnGetMessage(cr.Value);
    //                    //Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
    //                }
    //                catch (ConsumeException e)
    //                {
    //                    FileWorker.PrintLog($"Error occured: {e.Error.Reason}");
    //                    FileWorker.WriteLog($"Error occured: {e.Error.Reason}");
    //                }
    //            }
    //        }
    //        catch (OperationCanceledException e)
    //        {
    //            // Ensure the consumer leaves the group cleanly and final offsets are committed.
    //            FileWorker.PrintLog($"Error occured1: {e.Message}");
    //            FileWorker.WriteLog($"Error occured1: {e.Message}");
    //            consumerCommand.Close();
    //        }
    //    }
    //}
}
