﻿using System;
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
    static string deviceTopicName = ConfigWorker.GetConfigValue("topicDevice");
    static string deviceStateTopicName = ConfigWorker.GetConfigValue("topicDeviceState");
    static string commandTopicName = ConfigWorker.GetConfigValue("topicCommand");
    static string consumerGroupId = ConfigWorker.GetConfigValue("consumerGroupId");
    static IProducer<Null, string> producerAlarm=null;
    static IProducer<Null, string> producerDevice = null;
    static IProducer<Null, string> producerDeviceState = null;
    static IConsumer<Ignore, string> consumerCommand = null;
    static ProducerConfig configAlarm = null;
    static ProducerConfig configDevice = null;
    static ProducerConfig configDeviceState = null;
    static ConsumerConfig configCommand = null;
    //static Action<DeliveryReport<Null, string>> handler = r =>
    //   FileWorker.WriteLog(!r.Error.IsError
    //       ? $"Delivered message to {r.TopicPartitionOffset}"
    //       : $"Delivery Error: {r.Error.Reason}");
    static Action<DeliveryReport<Null, string>> handler = r =>
       FileWorker.LogHelper.WriteLog(!r.Error.IsError
           ? $"Delivered message to{r.Topic} at {r.TopicPartitionOffset}"
           : $"Delivery Error: {r.Error.Reason} on {r.Topic}");

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
            producerAlarm.Produce(messageTopicName, new Message<Null, string> { Value = message }, handler);
            producerAlarm.Flush(TimeSpan.FromSeconds(5));
        }
        catch (Exception e)
        {
            FileWorker.LogHelper.WriteLog("alarm error  " + e.Message);
        }
    }
    public static void sendDeviceMessage(string message)
    {
        if (configDevice == null) { configDevice = new ProducerConfig { BootstrapServers = brokerList }; }
        FileWorker.LogHelper.WriteLog("正在向kafka发送device消息" + message);
        try
        {
            if (producerDevice == null) { producerDevice = new ProducerBuilder<Null, string>(configDevice).Build(); }
            {
                producerDevice.Produce(deviceTopicName, new Message<Null, string> { Value = message }, handler);
                producerDevice.Flush(TimeSpan.FromSeconds(5));
            }
        }
        catch (Exception e)
        {
            FileWorker.LogHelper.WriteLog("device error  " + e.Message);
        }
    }

    public static void sendDeviceStateMessage(string message)
    {
        if (configDeviceState == null) { configDeviceState = new ProducerConfig { BootstrapServers = brokerList }; }
        FileWorker.LogHelper.WriteLog("正在向kafka发送deviceState消息" + message);
        try
        {
            if (producerDeviceState == null) { producerDeviceState = new ProducerBuilder<Null, string>(configDeviceState).Build(); }
            {
                producerDeviceState.Produce(deviceStateTopicName, new Message<Null, string> { Value = message }, handler);
                producerDeviceState.Flush(TimeSpan.FromSeconds(5));
            }
        }
        catch (Exception e)
        {
            FileWorker.LogHelper.WriteLog("deviceState error  " + e.Message);
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
                        //Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                    }
                    catch (ConsumeException e)
                    {
                        FileWorker.LogHelper.WriteLog($"Error occured: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException e)
            {
                // Ensure the consumer leaves the group cleanly and final offsets are committed.
                FileWorker.LogHelper.WriteLog($"Error occured1: {e.Message}");
                consumerCommand.Close();
            }
        }
    }
}
