using Modbus.Device;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace XiaoFangBaoJingQingDao
{
    public partial class Form1 : Form
    {
        private IScheduler scheduler;
        private IJobDetail job;

        private MqWorker mqWorker;

        private string ip;
        byte slaveAddress;
        ushort startAddress;
        ushort numInputs;
        int maxPoint;
        string functionType;
        int scanTimeSpan;

        public Form1()
        {
            InitializeComponent();
            functionType = ConfigWorker.GetConfigValue("functionType");
            this.scanTimeSpan = int.Parse(ConfigWorker.GetConfigValue("scanTimeSpan"));
        }
        private void getConfigValue()
        {
            ip = ConfigWorker.GetConfigValue("IP");
            if (string.IsNullOrEmpty(ip))
            {
                MessageBox.Show("请填写正确格式的IP");
                return;
            }
            string slaveAddressStr = ConfigWorker.GetConfigValue("SlaveID");
            string startAddressStr = ConfigWorker.GetConfigValue("StartAddress");
            string numInputsStr = ConfigWorker.GetConfigValue("PointNumber");
            string maxPointStr = ConfigWorker.GetConfigValue("MaxPoint");
            try
            {
                slaveAddress = (byte)(int.Parse(slaveAddressStr));
                startAddress = (ushort)(int.Parse(startAddressStr));
                numInputs = (ushort)(int.Parse(numInputsStr));
                maxPoint = int.Parse(maxPointStr);
            }
            catch (Exception)
            {
                MessageBox.Show("配置文件参数设置不正确");
                return;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            getConfigValue();
            if (mqWorker == null)
            {
                mqWorker = new MqWorker();
            }
            Thread.Sleep(2000);
            startJob();
        }
        private void startJob()
        {
            if (scheduler == null && job == null)
            {
                scheduler = StdSchedulerFactory.GetDefaultScheduler();
                JobWorker.form = this;
                job = JobBuilder.Create<JobWorker>().WithIdentity("fireJob", "jobs").Build();
                ITrigger trigger = TriggerBuilder.Create().WithIdentity("fireTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(3))
                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(scanTimeSpan).RepeatForever()).Build();
                scheduler.ScheduleJob(job, trigger);//把作业，触发器加入调度器。  
            }
            scheduler.Start();
        }
        public void getData()
        {
            LogHelper.WriteLog("一次扫描开始");
            startAddress = (ushort)(int.Parse(ConfigWorker.GetConfigValue("StartAddress")));//每次扫描开始，将startAddress置为配置文件中的数
            deviceIndex = 0; //每次扫描开始，将deviceIndex置为0
            try
            {
                List<string> list = ip.Split(new string[]
                    {
                "."
                    }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                byte[] array = new byte[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    array[i] = Convert.ToByte(list[i]);
                }
                IPAddress iPAddress = new IPAddress(array);
                using (TcpClient tcpClient = new TcpClient(iPAddress.ToString(), 502))
                {
                    //tcpClient.SendTimeout = 1;
                    ModbusIpMaster modbusIpMaster = ModbusIpMaster.CreateIp(tcpClient);
                    while (startAddress <= maxPoint)//循环读取，每次numInputs个，startAddress递增
                    {
                        if (!(functionType == "Hold"))
                        {
                            if (functionType == "Input")
                            {
                                ushort[] data = modbusIpMaster.ReadInputRegisters(slaveAddress, startAddress, numInputs);
                                this.dealData(data, (int)numInputs);
                            }
                        }
                        else
                        {
                            ushort[] data = modbusIpMaster.ReadHoldingRegisters(slaveAddress, startAddress, numInputs);
                            this.dealData(data, (int)numInputs);
                        }
                        startAddress += numInputs;
                        richTextBox1.Text += Environment.NewLine;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
            }
            finally
            {
                LogHelper.WriteLog("一次扫描完毕");
                richTextBox1.Text += Environment.NewLine;
                richTextBox1.Text += "-------------------------------------------------------------";
                richTextBox1.Text += Environment.NewLine;
            }
        }

        private int deviceIndex = 0;
        private void dealData(ushort[] data, int startAddress)
        {
            try
            {
                if (mqWorker == null)
                {
                    mqWorker = new MqWorker();
                }
                int bitIndex = 0;
                foreach (ushort uData in data)
                {
                    richTextBox1.Text += uData.ToString() + ",";
                    if (uData == 0)
                    {
                        //deviceIndex += 16;
                    }
                    else
                    {
                        char[] charArray = MathTransfer.Ten2Tow(uData);
                        if (charArray.Contains('1'))
                        {
                            Msg message = new Msg();
                            message.Body.Register = (startAddress + bitIndex).ToString();
                            message.Body.Event = string.Join("", charArray);
                            //Debug.WriteLine(message.toXml());
                            mqWorker.sendMsg(message.toXml());
                        }
                    }
                    bitIndex++;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
            }
        }
    }
}
