using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FSI.DeviceSDK.FiberDefenderDevice;
using WeiJieBaoJing.Entity;
using FSI.DeviceSDK;
using System.Net;
using System.IO;
using FSI.DeviceSDK.Config;
using FSI.DeviceSDK.Input;
using WeiJieBaoJing;

namespace IntegrationClient.Model
{
    public class DeviceSDKAPUModel
    {
        #region Properties

        /// <summary>
        /// Gets the current list of APUs
        /// </summary>
        public IList<IFiberDefenderAPUDevice> APUDevices
        {
            get
            {
                return m_Devices;
            }
        }


        /// <summary>
        /// Gets whether this object is started or not
        /// </summary>
        public bool IsStarted { get; private set; }

        /// <summary>
        /// Gets or sets whether the system time is used for timestamps or not.
        /// </summary>
        public bool UseSystemTimeInsteadOfAlarmTime { get; set; }
        #endregion

        #region Fields
        public const string UCRM = "User controlled relay mode";
        public const string ALARMA = "Alarm A Relay";
        public const string ALARMB = "Alarm B Relay";
        public const string FAULT = "Fault Relay";
        public const string ENABLED_PROPERTY = "Enabled";
        public const string DISABLED_PROPERTY = "Disabled";

        private IList<IFiberDefenderAPUDevice> m_Devices;
        private FiberDefenderAPUDeviceFactory m_FDFactory;
        private Dictionary<System.Net.IPAddress, bool> m_IsAPUsConnectionCurrentlyDown;
        private object m_IsStartedLockObject;
        private Mutex m_ScanMutex;
        private DateTime startTime = DateTime.Now;
        /// <summary>
        /// 设备状态列表（防区设备）
        /// </summary>
        public List<DeviceAlarmState> deviceAlarmStates = new List<DeviceAlarmState>();
        #endregion
        
        public DeviceSDKAPUModel()
        {
            // Fields
            m_FDFactory = new FiberDefenderAPUDeviceFactory();
            m_IsAPUsConnectionCurrentlyDown = new Dictionary<System.Net.IPAddress, bool>();
            m_IsStartedLockObject = new object();
            m_ScanMutex = new Mutex();//用于扫描设备的锁
            // Properties
            IsStarted = false;
        }

        #region Methods
        /// <summary>
        /// Removes a device based on a description string of the device
        /// </summary>
        /// <param name="DescriptionString">The description string of the APU</param>
        /// <returns>A IList of devices that are connected after the removal</returns>
        /// <remarks>
        /// The description strings are in the format of Name : Description : IPAddress
        /// </remarks>
        public IList<string> RemoveDevice(string descriptionString)
        {
            return RemoveDevice((fd) => fd.ToString() == descriptionString);
        }

        /// <summary>
        /// Removes a device based on a description string of the device
        /// </summary>
        /// <param name="ipAddress">The IP address of the APU to remove</param>
        /// <returns>A IList of devices that are connected after the removal</returns>
        public IList<string> RemoveDevice(System.Net.IPAddress ipAddress)
        {
            return RemoveDevice((fd) => fd.IPAddress == ipAddress);
        }

        /// <summary>
        /// Removes a device based on a predicate
        /// </summary>
        /// <param name="compare">A predicate that will compare IFiberDefenderAPUDevices</param>
        /// <returns>The list of devices after the removal</returns>
        private IList<string> RemoveDevice(Predicate<IFiberDefenderAPUDevice> compare)
        {
            List<string> names = new List<string>();
            try
            {
                // We are locking this mutex so that scanning will wait until after this removal is finished.
                // Otherwise m_Devices could be modified while we are looping through it.
                m_ScanMutex.WaitOne();

                IFiberDefenderAPUDevice toRemove = null;
                lock (m_Devices)
                {
                    foreach (var fd in m_Devices)
                    {
                        if (compare(fd))
                        {
                            toRemove = fd;
                            m_IsAPUsConnectionCurrentlyDown.Remove(fd.IPAddress);
                            fd.NetDisconnect();
                            continue;
                        }
                        else
                        {
                            names.Add(fd.ToString());
                        }
                    }
                    m_Devices.Remove(toRemove);
                }
            }
            finally
            {
                m_ScanMutex.ReleaseMutex();
            }
            return names;
        }

        /// <summary>
        /// Scans for APUs and adds any found to m_APUs
        /// </summary>
        public IList<IFiberDefenderAPUDevice> LocateAPUs(string filename)
        {
            try
            {
                var apus = new List<IFiberDefenderAPUDevice>();
                
                var ipStrings = File.ReadAllLines(filename);

                // Loop through each string loaded
                foreach (var ipString in ipStrings)
                {
                    IPAddress ip;
                    if (!IPAddress.TryParse(ipString, out ip))
                    {
                        if (!string.IsNullOrWhiteSpace(ipString))
                        {
                            FileWorker.PrintLog("围界设备的IP地址格式设置不正确" + ipString);
                            FileWorker.WriteLog("围界设备的IP地址格式设置不正确" + ipString);
                        }
                        continue;
                    }
                    
                    const int numberOfAttempts = 10;
                    bool found = false;
                    for (int i = 0; i < numberOfAttempts && !found; i++)
                    {
                        var device = m_FDFactory.DeviceAt(ip) as IFiberDefenderAPUDevice;
                        
                        if (device != null)
                        {
                            FileWorker.WriteLog("Located " + device.Name + " at " + ip);
                            apus.Add(device);
                            found = true;
                        }
                        else
                        {
                            FileWorker.PrintLog("Could not locate APU at " + ip);
                            FileWorker.WriteLog("Could not locate APU at " + ip);
                        }
                    }
                    if (found == false)//如果没找到设备
                    {
                        MessageDeviceChange messageAlarm = new MessageDeviceChange("ES02", "离线", ipString);
                        string message = messageAlarm.toJson();
                        KafkaWorker.sendDeviceMessage(message);
                    }
                    Thread.Sleep(100*2);//搜索每个设备之间间隔0.2秒
                }
                return apus;
            }
            catch (Exception)
            {
                FileWorker.PrintLog("Unable to open  " + filename);
                FileWorker.WriteLog("Unable to open  " + filename);
            }
            return null;
        }


        /// <summary>
        /// Scans and connects to APUs on the network
        /// </summary>
        /// <returns>
        /// An IList of <see cref="IFiberDefenderAPUDevice"/>s of the devices that were found and connected to
        /// </returns>
        public IList<IFiberDefenderAPUDevice> ScanForAPUs()
        {
            List<IFiberDefenderAPUDevice> apus = new List<IFiberDefenderAPUDevice>(m_Devices.Count);

            //var newList = m_FDFactory.ScanForDevices().OfType<IFiberDefenderAPUDevice>();
            var newList = LocateAPUs("devices.txt");
            try
            {
                // Locking so that only one scan is run at a time
                m_ScanMutex.WaitOne();
                foreach (var fd in newList)
                {
                    if (m_IsAPUsConnectionCurrentlyDown.ContainsKey(fd.IPAddress))
                        continue;

                    fd.NetConnectionDroppedEvent += new EventHandler<EventArgs>(OnFdNetConnectionDroppedEvent);
                    fd.AlarmEvent += new EventHandler<FSI.DeviceSDK.Input.AlarmEventArgs>(OnFdAlarmEvent);
                    try
                    {
                        fd.NetConnect();
                        // The IFiberDefenderAPUDevice ChildDevices property will never be null after a successful NetConnect.
                        AttachChildren(fd.ChildDevices);
                        apus.Add(fd);
                        SendDevices(fd);
                        lock (m_Devices)
                        {
                            m_Devices.Add(fd);
                        }
                        m_IsAPUsConnectionCurrentlyDown.Add(fd.IPAddress, false);
                    }
                    catch (TimeoutException)
                    {
                        FileWorker.PrintLog("IntegrationClient: Failed to connect,time out");
                        FileWorker.WriteLog("IntegrationClient: Failed to connect,time out");
                    }
                    catch (Exception e)
                    {
                        FileWorker.PrintLog("IntegrationClient: " + e);
                        FileWorker.WriteLog("IntegrationClient: " + e);
                    }
                }
            }
            finally
            {
                m_ScanMutex.ReleaseMutex();
            }
            return apus;
        }

        /// <summary>
        /// Starts the model.
        /// </summary>
        /// <remarks>
        /// This method would be a good place to put any start-up code if necessary.
        /// </remarks>
        public void Start()
        {
            int sumDevice = 0;
            int sumConnected = 0;
            int maxConnectFaild = 5;
            // Double-Checked lock
            if (!IsStarted)
            {
                lock (m_IsAPUsConnectionCurrentlyDown)
                {
                    if (!IsStarted)
                    {
                        //m_Devices = m_FDFactory.ScanForDevices().OfType<IFiberDefenderAPUDevice>().ToList();
                        m_Devices = LocateAPUs("devices.txt");
                        sumDevice = m_Devices.Count;
                        Parallel.ForEach(m_Devices, (device) =>
                        {
                            int currentConnectCount = 0;//重连次数
                            try
                            {
                                FileWorker.PrintLog("");
                                var fd = (device as IFiberDefenderAPUDevice);
                                FileWorker.PrintLog("");
                                fd.NetConnectionDroppedEvent += new EventHandler<EventArgs>(OnFdNetConnectionDroppedEvent);
                                FileWorker.PrintLog("");
                                fd.AlarmEvent += new EventHandler<FSI.DeviceSDK.Input.AlarmEventArgs>(OnFdAlarmEvent);
                                FileWorker.PrintLog("");
                                if (m_IsAPUsConnectionCurrentlyDown == null)
                                {
                                    FileWorker.PrintLog("m_IsAPUsConnectionCurrentlyDown是null");
                                }
                                m_IsAPUsConnectionCurrentlyDown.Add(fd.IPAddress, false);
                                FileWorker.PrintLog("");
                                //FileWorker.PrintLog("添加了设备 " + fd.IPAddress.ToString());

                                while (!fd.IsConnected)
                                {
                                    currentConnectCount++;
                                    if (currentConnectCount == maxConnectFaild)
                                    {
                                        FileWorker.WriteLog(fd.IPAddress.ToString() + "重试了5次都没连上");
                                        MessageDeviceChange messageAlarm = new MessageDeviceChange("ES02", "离线", device.IPAddress.ToString());
                                        string message = messageAlarm.toJson();
                                        KafkaWorker.sendDeviceMessage(message);
                                    }
                                    FileWorker.WriteLog("IntegrationClient: Connecting to: " + fd.Name + "," + fd.IPAddress);
                                    fd.NetConnect();
                                    sumConnected++;
                                    FileWorker.PrintLog("共" + sumDevice.ToString() + "个设备，已成功连接" + sumConnected.ToString() + "个");
                                    FileWorker.WriteLog("共" + sumDevice.ToString() + "个设备，已成功连接" + sumConnected.ToString() + "个");
                                }
                                AttachChildren(fd.ChildDevices);
                                SendDevices(fd);
                            }
                            catch (TimeoutException)
                            {
                                Debug.WriteLine("IntegrationClient: Failed to connect");
                                FileWorker.PrintLog("IntegrationClient: Failed to connect");

                            }
                            catch (Exception e)
                            {
                                Debug.WriteLine("IntegrationClient: " + e);
                                FileWorker.PrintLog("IntegrationClient: " + e);
                            }
                        });
                        IsStarted = true;
                    }
                }
            }
        }

        private void SendDevices(IFiberDefenderAPUDevice APUdevice)
        {
            MessageDevice messageObj = new MessageDevice(APUdevice);
            string message = messageObj.toJson();
            KafkaWorker.sendDeviceMessage(message);
            foreach (IGeneralDevice device in APUdevice.ChildDevices)
            {
                MessageDevice messageSubObj = new MessageDevice(device);
                string messageSub = messageSubObj.toJson();
                KafkaWorker.sendDeviceMessage(messageSub);
            }
        }

        /// <summary>
        /// Stops the model
        /// </summary>
        /// <remarks>
        /// This method would be a good place to put a tear down code if necessary.
        /// </remarks>
        public void Stop()
        {
            if (m_Devices != null)
            {
                //lock (m_Devices)
                //{
                //    Parallel.ForEach(m_Devices, (device) =>
                //    {
                //        try
                //        {
                //            var fd = (device as IFiberDefenderAPUDevice);
                //            FileWorker.PrintLog("程序即将关闭，SDK即将断开设备: Disconnecting from: " + fd.Name + "," + fd.IPAddress);
                //            FileWorker.WriteLog("程序即将关闭，SDK即将断开设备: Disconnecting from: " + fd.Name + "," + fd.IPAddress);
                //            fd.NetDisconnect();
                //        }
                //        catch(Exception ex)
                //        {
                //            FileWorker.PrintLog("程序即将关闭，SDK断开发时生异常: "+ex.Message);
                //            FileWorker.WriteLog("程序即将关闭，SDK断开时发生异常:"+ex.Message);
                //        }
                //    });
                //}
                foreach (var device in m_Devices)
                {
                    try
                    {
                        var fd = (device as IFiberDefenderAPUDevice);
                        FileWorker.PrintLog("程序即将关闭，SDK即将断开设备: Disconnecting from: " + fd.Name + "," + fd.IPAddress);
                        FileWorker.WriteLog("程序即将关闭，SDK即将断开设备: Disconnecting from: " + fd.Name + "," + fd.IPAddress);
                        fd.NetDisconnect();
                    }
                    catch (Exception ex)
                    {
                        FileWorker.PrintLog("程序即将关闭，SDK断开发时生异常: " + ex.Message);
                        FileWorker.WriteLog("程序即将关闭，SDK断开时发生异常:" + ex.Message);
                    }
                }
            }
            IsStarted = false;
        }

        #region Private Methods

        /// <summary>
        /// Attaches the AlarmEvent EventHandler for the entire tree structure of Channels
        /// </summary>
        /// <param name="iList">The list of channels of the root node to attach</param>
        /// <remarks>
        /// This method will recursively attach the AlarmEvent EventHandler for each channel. The
        /// reason for the recursion is that for 500 series APUs, there are hyperzones containing zones.
        /// </remarks>
        private void AttachChildren(System.Collections.Generic.IList<FSI.DeviceSDK.IGeneralDevice> iList)
        {
            // This is just in case an empty list is passed in.
            if (iList.Count <= 0)
                return;
            foreach (var child in iList)
            {
                var channel = child as IChannelDevice;
                if (channel == null)
                    continue;
                // Check if this is a leaf node of the tree.
                if (channel.NumberOfChildren > 0)
                {
                    AttachChildren(channel.ChildDevices);
                }
                else
                {
                    if (channel is IAlarmInputDevice)
                    {
                        (channel as IAlarmInputDevice).AlarmEvent += new EventHandler<AlarmEventArgs>(OnFdAlarmEvent);
                    }
                    if (channel is IAPUConfigDevice)
                    {
                        (channel as IAPUConfigDevice).APUSettingChangedEvent += new EventHandler<APUSettingChangedEventArgs>(OnAPUSettingChangedEvent);
                    }
                }
            }
        }

        /// <summary>
        /// Event Handler for an AlarmEvent from a device
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">An EventArgs for the event</param>
        private void OnFdAlarmEvent(object sender, FSI.DeviceSDK.Input.AlarmEventArgs e)
        {
            //if (fd != null && m_IsAPUsConnectionCurrentlyDown.ContainsKey(fd.IPAddress))
            {
                string deviceFullName = string.IsNullOrEmpty(e.ChildDeviceName) ? e.Device.Name : e.ChildDeviceName;
                string deviceName = deviceFullName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[0];
                string channelName = deviceFullName.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries)[1];
                //FileWorker.PrintLog("设备是" + deviceFullName + " 类型是" + ((int)(e.Type)).ToString() + " 状态是" + ((int)(e.Status)).ToString());
                //报警产生后，从报警状态列表deviceAlarmStates中查找到该设备的状态对象。如果没有则新增一个。
                DeviceAlarmState deviceAlarmState = deviceAlarmStates.FirstOrDefault(d => d.deviceName == deviceFullName);
                if (deviceAlarmState == null)
                {
                    deviceAlarmState = new DeviceAlarmState();
                    deviceAlarmState.deviceName = deviceFullName;
                    deviceAlarmStates.Add(deviceAlarmState);
                }
                if (deviceAlarmState.canOutputAlarm)//如果超过设置的时间间隔，才可以触发
                {
                    bool isInPlan = PlanEntity.IsInPlan(deviceFullName);
                    FileWorker.WriteLog("设备" + deviceFullName + "开始进行判定，判定结果是(true布防false撤防)："+isInPlan.ToString());
                    if (isInPlan)
                    {
                        DateTime time = UseSystemTimeInsteadOfAlarmTime ? DateTime.Now : e.Time;
                        switch (e.Type)
                        {
                            case FSI.DeviceSDK.Input.AlarmType.Alarm:
                                {
                                    //下游设备一直响
                                    if (deviceFullName.Contains("."))
                                    {
                                        switch (channelName)
                                        {
                                            case "CHA":
                                                ToggleSetting(deviceName, DeviceSDKAPUModel.ALARMA, DeviceSDKAPUModel.ENABLED_PROPERTY);
                                                break;
                                            case "CHB":
                                                ToggleSetting(deviceName, DeviceSDKAPUModel.ALARMB, DeviceSDKAPUModel.ENABLED_PROPERTY);
                                                break;
                                        }
                                    }
                                    MessageAlarm messageAlarm = new MessageAlarm(e.Device.Name, "ALARM_INFO", "01", "入侵");
                                    string message = messageAlarm.toJson();
                                    KafkaWorker.sendAlarmMessage(message);
                                }
                                break;
                            case FSI.DeviceSDK.Input.AlarmType.Tamper:
                                {
                                    switch (e.Status)
                                    {
                                        //防拆产生的报警，拆分成2条消息，分别表示AB端
                                        case FSI.DeviceSDK.Input.AlarmStatus.AlarmOn:
                                            {
                                                MessageAlarm messageAlarmA = new MessageAlarm(e.Device.Name + ".CHA", "ALARM_INFO", "04", "防拆");
                                                string messageA = messageAlarmA.toJson();
                                                KafkaWorker.sendAlarmMessage(messageA);
                                                MessageAlarm messageAlarmB = new MessageAlarm(e.Device.Name + ".CHB", "ALARM_INFO", "04", "防拆");
                                                string messageB = messageAlarmB.toJson();
                                                KafkaWorker.sendAlarmMessage(messageB);
                                                //触发防拆后也触发下游声光报警
                                                ToggleSetting(e.Device.Name, DeviceSDKAPUModel.ALARMA, DeviceSDKAPUModel.ENABLED_PROPERTY);
                                                ToggleSetting(e.Device.Name, DeviceSDKAPUModel.ALARMB, DeviceSDKAPUModel.ENABLED_PROPERTY);
                                            }
                                            break;
                                        case FSI.DeviceSDK.Input.AlarmStatus.AlarmOff:
                                            {
                                                MessageAlarm messageAlarmA = new MessageAlarm(e.Device.Name + ".CHA", "ALARM_INFO", "05", "防拆解除");
                                                string messageA = messageAlarmA.toJson();
                                                KafkaWorker.sendAlarmMessage(messageA);
                                                MessageAlarm messageAlarmB = new MessageAlarm(e.Device.Name + ".CHB", "ALARM_INFO", "05", "防拆解除");
                                                string messageB = messageAlarmB.toJson();
                                                KafkaWorker.sendAlarmMessage(messageB);
                                            }
                                            break;
                                        case FSI.DeviceSDK.Input.AlarmStatus.AlarmInstant:
                                            return;
                                    }
                                }
                                break;
                            case FSI.DeviceSDK.Input.AlarmType.Fault:
                                {
                                    //防拆、故障产生的状态改变、报警
                                    //20190823修改，e.Device.Name是具体设备名，所以只发本设备的
                                    if (e.Status == FSI.DeviceSDK.Input.AlarmStatus.AlarmOn)
                                    {
                                        //发送设备状态信息
                                        MessageDeviceChange messageDeviceChangeA = new MessageDeviceChange(e.Device.Name , "ES03", "故障", "");
                                        string messageA = messageDeviceChangeA.toJson();
                                        KafkaWorker.sendDeviceMessage(messageA);
                                        //发送报警信息
                                        MessageAlarm messageAlarm = new MessageAlarm(e.Device.Name, "ALARM_INFO", "02", "故障");
                                        KafkaWorker.sendAlarmMessage(messageAlarm.toJson());
                                    }
                                    else if (e.Status == FSI.DeviceSDK.Input.AlarmStatus.AlarmOff)
                                    {
                                        if ((DateTime.Now - startTime).TotalMinutes > 3)//系统启动前3分钟不发故障解除信息
                                        {
                                            //发送设备状态信息
                                            MessageDeviceChange messageDeviceChange = new MessageDeviceChange(e.Device.Name, "ES01", "在线", "");
                                            KafkaWorker.sendDeviceMessage(messageDeviceChange.toJson());
                                            //发送报警信息
                                            MessageAlarm messageAlarm = new MessageAlarm(e.Device.Name, "ALARM_INFO", "03", "故障解除");
                                            KafkaWorker.sendAlarmMessage(messageAlarm.toJson());
                                        }
                                        else
                                        {
                                            FileWorker.WriteLog(e.Device.Name + "收到故障解除信号，但不发送信息");
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
        

        /// <summary>
        /// Event Handler for an unplanned disconnect from a device
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">An EventArgs for the event</param>
        private void OnFdNetConnectionDroppedEvent(object sender, EventArgs e)
        {
            var fd = sender as IFiberDefenderAPUDevice;

            if (fd != null)
            {
                // Check if we already know this connection has been dropped. Since this will run a continuous loop until it reconnects,we don't need to have multiple threads looping.
                lock (m_IsAPUsConnectionCurrentlyDown)
                {
                    if (!m_IsAPUsConnectionCurrentlyDown[fd.IPAddress])
                    {
                        m_IsAPUsConnectionCurrentlyDown[fd.IPAddress] = true;
                    }
                    else
                    {
                        return;
                    }
                }
                // At this point we can tell other software that the connection has be lost. SendMessageToOtherSoftware(fd.Name, DateTime.Now, IntegrationStrings.CommunicationsLost);
                FileWorker.PrintLog("设备" + fd.Name + "意外离线");
                FileWorker.WriteLog("设备" + fd.Name + "意外离线");
                MessageDeviceChange messageAlarmA = new MessageDeviceChange(fd.Name + ".CHA", "ES02", "离线", "");
                KafkaWorker.sendDeviceMessage(messageAlarmA.toJson());
                MessageDeviceChange messageAlarmB = new MessageDeviceChange(fd.Name + ".CHB", "ES02", "离线", "");
                KafkaWorker.sendDeviceMessage(messageAlarmB.toJson());

                //DeviceAlarmState das = deviceAlarmStates.FirstOrDefault(d => d.deviceName == fd.Name);
                DeviceAlarmState dasA = deviceAlarmStates.FirstOrDefault(d => d.deviceName == fd.Name + ".CHA");
                DeviceAlarmState dasB = deviceAlarmStates.FirstOrDefault(d => d.deviceName == fd.Name + ".CHB");
                //if (das != null)
                //{
                //    das.IsOnline = false;
                //}
                if (dasA != null) { dasA.IsOnline = false; }
                else { FileWorker.WriteLog("设备" + fd.Name + "意外离线，但.CHA不在设备状态列表中"); }
                if (dasB != null) { dasB.IsOnline = false; }
                else { FileWorker.WriteLog("设备" + fd.Name + "意外离线，但.CHB不在设备状态列表中"); }
                while (!fd.IsConnected)
                {
                    // Check if the device has been removed so we don't keep bothering to attempt a reconnection
                    lock (m_Devices)
                    {
                        if (!m_Devices.Contains(fd))
                            return;
                    }
                    try
                    {
                        fd.NetConnect();
                        lock (m_IsAPUsConnectionCurrentlyDown)
                        {
                            m_IsAPUsConnectionCurrentlyDown[fd.IPAddress] = false;
                        }
                        FileWorker.PrintLog("设备" + fd.Name + "恢复连接成功");
                        FileWorker.WriteLog("设备" + fd.Name + "恢复连接成功");
                        MessageDeviceChange messageAlarmOnLineA = new MessageDeviceChange(fd.Name + ".CHA", "ES01", "在线", "");
                        KafkaWorker.sendDeviceMessage(messageAlarmOnLineA.toJson());
                        MessageDeviceChange messageAlarmOnLineB = new MessageDeviceChange(fd.Name + ".CHB", "ES01", "在线", "");
                        KafkaWorker.sendDeviceMessage(messageAlarmOnLineB.toJson());
                        if (dasA != null) { dasA.IsOnline = true; }
                        if (dasB != null) { dasB.IsOnline = true; }
                    }
                    catch (System.Net.Sockets.SocketException ex)
                    {
                        // The NetConnect failed.
                        FileWorker.PrintLog("OnFdNetConnectionDroppedEvent异常：" + ex.Message);
                        FileWorker.WriteLog("OnFdNetConnectionDroppedEvent异常：" + ex.Message);
                        if (ex.SocketErrorCode == System.Net.Sockets.SocketError.ConnectionRefused)
                        {
                            FileWorker.PrintLog("IntegrationClient: Connection Refused");
                            FileWorker.WriteLog("IntegrationClient: Connection Refused");
                        }
                        FileWorker.PrintLog("IntegrationClient: Failed To Reconnect");
                        FileWorker.WriteLog("IntegrationClient: Failed To Reconnect");
                    }
                    // Lets try waiting longer and trying again. Will sleep for the ping interval since that should be an appropriate time.
                    Thread.Sleep((int)(fd.PingUpkeepInterval.TotalMilliseconds));
                };
            }
        }
        
        public void ToggleSetting(string  deviceName, string setting,string value)
        {
            IFiberDefenderAPUDevice device = m_Devices.FirstOrDefault(d => d.Name == deviceName);
            if (device != null)
            {
                {
                    try
                    {
                        IChannelDevice subDevice = setting == DeviceSDKAPUModel.ALARMA ? (IChannelDevice)device.ChildDevices[0] : (IChannelDevice)device.ChildDevices[1];
                        string channelName = setting == DeviceSDKAPUModel.ALARMA ? ".CHA" : ".CHB";
                        DeviceAlarmState das = deviceAlarmStates.FirstOrDefault(d => d.deviceName == deviceName + channelName);
                        if (das == null)
                        {
                            das = new DeviceAlarmState();
                            das.deviceName = deviceName + channelName;
                            deviceAlarmStates.Add(das);
                        }
                        if (das.hadSetUserControl == false)
                        {
                            ucrmSetting(subDevice, ENABLED_PROPERTY);//SDK用户获取控制权。对应每个防区，只需设置一次即可
                            das.hadSetUserControl = true;
                        }
                        if (das.IsOnline)
                        {
                            if (das.IsSending == false && value == ENABLED_PROPERTY)//如果现在为“关”状态，且用户要设为“开”，此时执行设置
                            {
                                FileWorker.PrintLog("**********设备" + deviceName + channelName + "进行设置，值为" + value);
                                FileWorker.WriteLog("**********设备" + deviceName + channelName + "进行设置，值为" + value);
                                das.IsSending = true;//设置为正在发送“开”状态
                                subDevice.SetSetting(setting, value);
                            }
                            else if (das.IsSending == true && value == DISABLED_PROPERTY)//如果现在为“开”状态，且用户要设置为“关”，此时执行设置
                            {
                                FileWorker.PrintLog("**********设备" + deviceName + channelName + "进行设置，值为" + value);
                                FileWorker.WriteLog("**********设备" + deviceName + channelName + "进行设置，值为" + value);
                                das.IsSending = false;//设置为正在发送“关”状态
                                subDevice.SetSetting(setting, value);
                            }
                            else//其他情况不做处理
                            {
                                FileWorker.PrintLog("@@@@@@@@@@设备" + deviceName + channelName + "想设置为" + value + "，但目前状态已经是" + das.IsSending + "，故不做处理");
                                FileWorker.WriteLog("@@@@@@@@@@设备" + deviceName + channelName + "想设置为" + value + "，但目前状态已经是" + das.IsSending + "，故不做处理");
                            }
                        }
                        else
                        {
                            FileWorker.PrintLog("#########未对设备" + deviceName + channelName + "设置为" + value + "，因为其断线");
                            FileWorker.WriteLog("#########未对设备" + deviceName + channelName + "设置为" + value + "，因为其断线");
                        }
                    }
                    catch (InvalidOperationException e)
                    {
                        System.Console.WriteLine("设置引发InvalidOperationException异常：" + e.Message);
                        FileWorker.PrintLog("设置引发InvalidOperationException异常：" + e.Message);
                        FileWorker.WriteLog("设置引发InvalidOperationException异常：" + e.Message);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("设置引发Exception异常：" + ex.Message);
                        FileWorker.PrintLog("设置引发Exception异常：" + ex.Message);
                        FileWorker.WriteLog("设置引发Exception异常：" + ex.Message);
                    }
                }
            }
            else
            {
                FileWorker.PrintLog("未找到设备" + deviceName);
                FileWorker.WriteLog("未找到设备" + deviceName);
            }
        }

        void ucrmSetting(IChannelDevice device, string value)
        {
            try
            {
                device.SetSetting(DeviceSDKAPUModel.UCRM, value);
            }
            catch (Exception ex)
            {
                FileWorker.WriteLog("设备" + device.Name + "设置ucrmSetting为" + value + "时出现异常：" + ex.Message);
            }
        }
        void OnAPUSettingChangedEvent(object sender, APUSettingChangedEventArgs e)
        {
            string deviceName = ((IChannelDevice)sender).Name;
            DeviceAlarmState deviceAlarmState = deviceAlarmStates.FirstOrDefault(d => d.deviceName == deviceName);
            if (deviceAlarmState != null)
            {
                //deviceAlarmState.IsSending = false;
                FileWorker.WriteLog("设备" + deviceName + "成功设置" + e.SettingName + "为" + e.NewValue);
            }
            else
            {
                FileWorker.WriteLog("设备" + deviceName + "成功设置" + e.SettingName + "为" + e.NewValue+"。但该设备未储存于设备状态列表中");
            }
        }

        public void closeAllDeviceUserControl()
        {
            if (m_Devices != null)
            {
                foreach (var device in m_Devices)
                {
                    foreach (IChannelDevice subDevice in device.ChildDevices)
                    {
                        ucrmSetting(subDevice, DISABLED_PROPERTY);//SDK用户放弃控制权
                    }
                }
            }
        }

        /// <summary>
        /// 关闭所有防区的控制开关（下游声光报警）
        /// </summary>
        public void closeAllDeviceSetting()
        {
            if (m_Devices != null)
            {
                foreach (var device in m_Devices)
                {
                    foreach (IChannelDevice subDevice in device.ChildDevices)
                    {
                        ToggleSetting(subDevice.Name, DeviceSDKAPUModel.ALARMA, DeviceSDKAPUModel.DISABLED_PROPERTY);
                        ToggleSetting(subDevice.Name, DeviceSDKAPUModel.ALARMB, DeviceSDKAPUModel.DISABLED_PROPERTY);
                    }
                }
            }
        }
        #endregion
        
        #endregion

        
    }
}
