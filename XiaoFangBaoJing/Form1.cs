namespace XiaoFangBaoJing
{
    using Modbus.Device;
    using Quartz;
    using Quartz.Impl;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private IScheduler scheduler;
        private IJobDetail job;
        private List<Device> deviceList = new List<Device>();
        private List<int> alarmList = new List<int>;
        private string ip;
        byte slaveAddress;
        ushort startAddress;
        ushort numInputs;
        int maxPoint;
        string functionType;
        int scanTimeSpan;

        public Form1()
        {
            this.InitializeComponent();
            //var a = MathTransfer.Ten2Tow(720);
            functionType = ConfigWorker.GetConfigValue("functionType");
            this.scanTimeSpan = int.Parse(ConfigWorker.GetConfigValue("scanTimeSpan"));
            var lines = FileWorker.readTxt(System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "device.txt");
            LogHelper.WriteLog("从文件" + System.AppDomain.CurrentDomain.BaseDirectory.ToString() + "device.txt" + "中加载设备名称");
            foreach (string line in lines)
            {
                string[] deviceInfo = line.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
                if (deviceInfo.Length == 2)
                {
                    Device device = new Device() { index = deviceInfo[0], name = deviceInfo[1] };
                    deviceList.Add(device);
                }
            }
            LogHelper.WriteLog("共加载设备" + deviceList.Count + "个");
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

        private void btn_readHodingRegister_Click(object sender, EventArgs e)
        {
            //this.getData(this.txt_ip.Text.Trim(), (byte)this.num_slaveId.Value, (ushort)this.num_startAddress.Value, (ushort)this.num_pointNumber.Value, "HodingRegister");
        }
        private void btn_readInputRegister_Click(object sender, EventArgs e)
        {
            //this.getData(this.txt_ip.Text.Trim(), (byte)this.num_slaveId.Value, (ushort)this.num_startAddress.Value, (ushort)this.num_pointNumber.Value, "InputRegister");
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
                                LogHelper.WriteLog("收到数据" + string.Join(",", data));
                                this.dealData(data, (int)numInputs);
                            }
                        }
                        else
                        {
                            ushort[] data = modbusIpMaster.ReadHoldingRegisters(slaveAddress, startAddress, numInputs);
                            LogHelper.WriteLog("收到数据" + string.Join(",", data));
                            this.dealData(data, (int)numInputs);
                        }
                        startAddress += numInputs;
                        //richTextBox1.Text += Environment.NewLine;
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
                //richTextBox1.Text += Environment.NewLine;
                //richTextBox1.Text += "-------------------------------------------------------------";
                //richTextBox1.Text += Environment.NewLine;
            }
        }

        private int deviceIndex = 0;
        private void dealData(ushort[] data, int numInputs)
        {
            try
            {
                foreach (ushort uData in data)
                {
                    //richTextBox1.Text += uData.ToString() + ",";
                    if (uData == 0)
                    {
                        deviceIndex += 16;
                    }
                    else
                    {
                        char[] charArray = MathTransfer.Ten2Tow(uData);
                        foreach (char c in charArray)
                        {
                            deviceIndex++;
                            if (c.ToString() == "1")
                            {
                                var device = deviceList.FirstOrDefault(d => d.index == deviceIndex.ToString());
                                if (device != null)
                                {
                                    //richTextBox1.Text += "第" + deviceIndex + "个设备故障" + "设备名:" + device.name;
                                    //richTextBox1.Text += Environment.NewLine;
                                    LogHelper.WriteLog("第" + deviceIndex + "个设备故障" + "设备名:" + device.name);

                                    if (alarmList.Count > 0 && alarmList.Contains(deviceIndex))
                                    {
                                        return;
                                    }
                                    alarmList.Add(deviceIndex);
                                    MessageEntity message = new MessageEntity();
                                    message.meta.sequence= Guid.NewGuid().ToString("N");
                                    message.meta.sendTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    message.meta.recvTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    message.meta.forward = DateTime.Now.ToString("yyyyMMddHHmmss");
                                    message.body.alarmClassCode = "AC04";
                                    message.body.alarmClassName = "消防报警";
                                    message.body.alarmLevelCode = "AL01";
                                    message.body.alarmLevelName = "一级";
                                    message.body.alarmTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                    message.body.alarmEquCode = device.name;
                                    if (deviceIndex <= 200)//前200设备是红线门，之后的是烟感报警
                                    {
                                        message.body.alarmTypeCode = "AC0401";
                                        message.body.alarmTypeName = "红线门报警";
                                    }
                                    else
                                    {
                                        message.body.alarmTypeCode = "AC0402";
                                        message.body.alarmTypeName = "烟感报警";
                                    }

                                    KafkaWorker.sendMessage(message.toJson());
                                }
                                else
                                {
                                    //richTextBox1.Text += "第" + deviceIndex + "个设备故障。设备名未找到";
                                    //richTextBox1.Text += Environment.NewLine;
                                    LogHelper.WriteLog("第" + deviceIndex + "个设备故障。设备名未找到");
                                }
                            }
                            else
                            {
                                if (alarmList.Contains(deviceIndex))
                                {
                                    alarmList.Remove(deviceIndex);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex.Message);
            }
        }

        private void btn_startJob_Click(object sender, EventArgs e)
        {
            if (txt_ip.Text.Trim() == "")
            {
                MessageBox.Show("请填写IP");
                return;
            }
            try
            {
                ip = txt_ip.Text.Trim();
                slaveAddress = (byte)this.num_slaveId.Value;
                startAddress = (ushort)this.num_startAddress.Value;
                numInputs = (ushort)this.num_pointNumber.Value;
                startJob();
            }
            catch (Exception ex)
            {
                //richTextBox1.Text += ex.Message;
                //richTextBox1.Text += Environment.NewLine;
                LogHelper.WriteLog(ex.Message);
            }
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
            Thread.Sleep(2000);
            startJob();
        }
    }
}


