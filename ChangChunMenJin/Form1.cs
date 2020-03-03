using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangChunMenJin
{
    public partial class Form1 : Form
    {
        AccessEngine ace = new AccessEngine();
        
        string user = string.Empty;
        string password = string.Empty;
        string hostname = string.Empty;
        string serverIp = string.Empty;
        string localIp = string.Empty;
        string port = string.Empty;
        Thread thread;
        Thread threadPerson;
        DateTime currentHearTime;
        IScheduler scheduler;
        IJobDetail job;

        CloseForm cf = new CloseForm();

        public Form1()
        {
            InitializeComponent();
            //string a = "/ lgnr = 92852 / cats = !0000000000000000040000000000000000000000000000000000000000000000 / ltim = 20191212173623W / levl = 0 / msid = 16777985 / ctim = 20191212173624W / sndr = \"MAC-1\" / macdevid = \"FF00003300000003\" / Devices.id = \"00135A4897EA11D2\" / MSGNR = 16777985 / MSGTEXT = \"access\" / Cards.cardid = \"00135A5BF00244D7\" / Areas.areaid = \"FF00000900000002\" / DeviceGroups.devgroupid = \"00135A4897E9A7F1\" 1 / Cards.cardid = \"00135A5BF00244D7\" / Cards.cardno = \"000677701472\" / Persons.persid = \"00135A5BA84741CE\" / Persons.lastname = \"测试\" / Persons.firstname = ! / Persons.dateofbirth = ! / Persons.persno = ! / Persons.persclass = 69 / Persons.department = ! / Companies.companyid = ! / Companies.companyno = ! / OsDaten.datevaliduntil = ! / Devices.id = \"00135A4897EA11D2\" / Devices.id02 = ! / Devices.locator = \"$/AC\" / Devices.type = \"WIE1\" / Devices.displaytext = \"WIE1 读卡器-1\" / DeviceGroups.devgroupid = \"00135A4897E9A7F1\" / DeviceGroups.displaytext = \"123\" / Areas.name = \"之外\" / host = \"BISSERVER\"";
            //SwipeEntity swipe = tcpToSwipe(a);
            //string s = swipe.toJson();
            //string b = "/ lgnr = 98284 / cats = !0000000000000000040000000000000000000000000000000000200000000000 / ltim = 20191213103301W / levl = 0 / msid = 16778245 / ctim = 20191213103301W / sndr = \"MAC-1\" / macdevid = \"FF00003300000003\" / Devices.id = \"00135A4897EA11D2\" / MSGNR = 16778245 / MSGTEXT = \"NOT authorized, card is unknown\" / CODENR = !000000006e255cc5 / DeviceGroups.devgroupid = \"00135A4897E9A7F1\" 1 / Devices.id = \"00135A4897EA11D2\" / Devices.id02 = ! / Devices.locator = \"$/AC\" / Devices.type = \"WIE1\" / Devices.displaytext = \"WIE1 读卡器-1\" / DeviceGroups.devgroupid = \"00135A4897E9A7F1\" / DeviceGroups.displaytext = \"123\" / host = \"BISSERVER\"ALIVE = \"20191213103331W\"";
            //AlarmEntity alarm = tcpToAlarm(b);
            //string ss = alarm.toJson();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            user = ConfigWorker.GetConfigValue("user");
            password = ConfigWorker.GetConfigValue("password");
            hostname = ConfigWorker.GetConfigValue("hostname");
            serverIp = ConfigWorker.GetConfigValue("serverIp");
            localIp = ConfigWorker.GetConfigValue("localIp");
            port = ConfigWorker.GetConfigValue("port");
            thread = new Thread(new ThreadStart(startListener));
            threadPerson = new Thread(new ThreadStart(KafkaWorker.startGetMessage));
            KafkaWorker.OnGetMessage += KafkaWorker_OnGetMessage;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            threadPerson.Start();
            if (login())
            {
                //startListener();
                thread.Start();
                startJob();
            }
        }
        private void startJob()
        {
            try
            {
                if (scheduler == null && job == null)
                {
                    scheduler = StdSchedulerFactory.GetDefaultScheduler();
                    JobWorker.form = this;
                    job = JobBuilder.Create<JobWorker>().WithIdentity("fireJob", "jobs").Build();
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("fireTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(1))
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(30).RepeatForever()).Build();
                    scheduler.ScheduleJob(job, trigger);//把作业，触发器加入调度器。  
                }
                scheduler.Start();
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("定时任务异常："+ex.Message);
            }
        }

        /// <summary>
        /// 登录
        /// </summary>
        private bool login()
        {
            try
            {
                API_RETURN_CODES_CS result = ace.Login(user, password, hostname);
                if (API_RETURN_CODES_CS.API_SUCCESS_CS != result)
                {
                    FileWorker.LogHelper.WriteLog("登录失败 " + result);
                    MessageBox.Show("登录失败 " + result);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("登录异常 " + ex.Message);
                MessageBox.Show("登录异常 "+ex.Message);
                return false;
            }
        }

        private void startReceiveData()
        {
            try
            {
                bool result = ace.StartEventMessageDistribution((uint)0, localIp, port, 0);
                if (result)
                {
                    FileWorker.LogHelper.WriteLog("开始接收方法执行成功 " + localIp + ":" + port);
                }
                else
                {
                    FileWorker.LogHelper.WriteLog("开始接收方法执行失败 " + localIp + ":" + port);
                    MessageBox.Show("开始接收方法执行失败 " + localIp + ":" + port);
                }
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("开始接收方法异常："+ex.Message);
                MessageBox.Show("开始接收方法异常 " + localIp + ":" + port);
            }

        }

        private void stopReceiveData()
        {
            try
            {
                bool result = ace.StopEventMessageDistribution();
                if (result)
                {
                    FileWorker.LogHelper.WriteLog("停止接收方法执行成功");
                }
                else
                {
                    FileWorker.LogHelper.WriteLog("停止接收方法执行失败");
                }
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("停止接收方法异常："+ex.Message);
            }
        }

        public void checkHeart()
        {
            if ((DateTime.Now - currentHearTime).Seconds > 90)//如果距离上次心跳间隔超过90秒
            {
                FileWorker.LogHelper.WriteLog("在规定时间内没有收到心跳，开始尝试重新向门禁服务器发送start命令");
                startReceiveData();
            }
        }

        private void startListener()
        {
            IPAddress address = IPAddress.Parse(localIp);
            IPEndPoint ep = new IPEndPoint(address, int.Parse(port));
            TcpListener listener = new TcpListener(ep);
            listener.Start();

            startReceiveData();

            byte[] buffer = new byte[2 * 8192];
            var sender = listener.AcceptTcpClient();
            sender.ReceiveBufferSize = 2 * 8192;
            // Set the timeout for synchronous receive methods to 
            // 40 second (1000 milliseconds.) because after 30 seconds without any new message an ALIVE message  
            // should be received
            sender.ReceiveTimeout = 60 * 1000;
            while (true)
            {
                // Accept connection again
                //
                if ((sender == null) || !sender.Connected)
                    sender = listener.AcceptTcpClient();    // blocking call ...
                // Try to read received message
                //
                int iReadNum = 0;
                iReadNum = sender.GetStream().Read(buffer, 0, sender.ReceiveBufferSize); //bytesize
                // Any message received? 
                if (iReadNum == 0)
                {
                    // Here simple retry again.. Timeout and reconnect handling should be implemented
                    continue;
                }
                // Message received .. it is always of type unicode!
                string tcpMessage = System.Text.Encoding.Unicode.GetString(buffer).Substring(0, iReadNum / 2);
                Debug.WriteLine(tcpMessage);
                // The format of the received message string is described in the next chapter “Event messsages”
                if (tcpMessage.Contains("ALIVE="))//如果是心跳
                {
                    string dtStr = tcpMessage.Substring(tcpMessage.IndexOf("ALIVE="), 20);

                }
                if (tcpMessage.Contains("START=") || tcpMessage.Contains("ALIVE="))
                { continue; }
                if (tcpMessage.Contains("MSGTEXT=\"door is closed\"") || tcpMessage.Contains("MSGTEXT=\"door is open\""))  //开门关门暂不处理
                {
                    continue;
                }
                else if (tcpMessage.Contains("MSGTEXT=\"access\"") || tcpMessage.Contains("MSGTEXT=\"access, via remote command\""))//合法卡刷卡、远程开门，发送至正常刷卡队列
                {
                    SwipeEntity swipte = tcpToSwipe(tcpMessage);
                    string message = swipte.toJson();
                    KafkaWorker.sendSwipeMessage(message);
                }
                else //报警信息发送至报警队列
                {
                    AlarmEntity alarm = tcpToAlarm(tcpMessage);
                    string message = alarm.toJson();
                    KafkaWorker.sendAlarmMessage(message);
                }
            }
        }

        //////事件名称列表
        // 门：
        //1.door is closed 门已关闭
        //2.door is open 门已打开
        //3.door is open too long 开门时间过长 （报警信息）
        //4.door opened without authorization 非法开门（报警信息）
        //5.door is open permanently 门解锁、或者翻译为门常开
        //读卡器：
        //1.access 合法卡刷卡进入
        //2.authorized, person did not enter 合法卡刷卡但未进入，或者翻译为超时（报警信息）
        //3.NOT authorized, no authorization 卡片未授权（报警信息）
        //4.NOT authorized, card is unknown 卡片未知（报警信息）
        //5.NOT authorized, 1 * wrong pincode 密码错误（报警信息）
        //6.NOT authorized, distress pincode used 胁迫报警（报警信息）
        //7.access, via remote command 远程开门

        public AlarmEntity tcpToAlarm(string tcpMsg)
        {
            AlarmEntity alarm = new AlarmEntity();
            var items = tcpMsg.Split(new string[] { @"/"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in items)
            {
                var kvPair=item.Split(new string[] { @"=" }, StringSplitOptions.RemoveEmptyEntries);
                {
                    switch (kvPair[0].Trim())
                    {
                        case "ltim"://时间
                            alarm.body.alarmTime = DateTime.ParseExact(kvPair[1].Replace("W", "").Trim(), "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case "Devices.type"://设备类型
                            if (kvPair[1].Replace("\"", "") == "NORMDOOR")
                            {
                                alarm.body.equClassCode = "01";
                                alarm.body.equClassName = "门";
                            }
                            if (kvPair[1].Replace("\"", "").Contains("WIE1")) //读卡器
                            {
                                alarm.body.equClassCode = "02";
                                alarm.body.equClassName = "读卡器";
                            }
                            break;
                        case "Devices.displaytext"://设备名称
                            alarm.body.alarmEquName = kvPair[1].Replace("\"", "").Trim();
                            alarm.body.alarmName= kvPair[1].Replace("\"", "").Trim();
                            break;
                        case "Devices.id"://设备编号
                            alarm.body.alarmEquCode = kvPair[1].Replace("\"", "").Trim();
                            break;
                        case "MSGTEXT"://事件名称
                            switch (kvPair[1].Replace("\"", "").Trim())
                            {
                                case "door is open too long":
                                    alarm.body.alarmTypeCode = "03";
                                    alarm.body.alarmTypeName = "开门时间过长";
                                    break;
                                case "door opened without authorization":
                                    alarm.body.alarmTypeCode = "04";
                                    alarm.body.alarmTypeName = "非法开门";
                                    break;
                                case "door is open permanently":
                                    alarm.body.alarmTypeCode = "05";
                                    alarm.body.alarmTypeName = "门常开";
                                    break;
                                case "authorized, person did not enter":
                                    alarm.body.alarmTypeCode = "06";
                                    alarm.body.alarmTypeName = "超时";
                                    break;
                                case "NOT authorized, no authorization":
                                    alarm.body.alarmTypeCode = "07";
                                    alarm.body.alarmTypeName = "卡片未授权";
                                    break;
                                case "NOT authorized, card is unknown":
                                    alarm.body.alarmTypeCode = "08";
                                    alarm.body.alarmTypeName = "卡片未知";
                                    break;
                                case "NOT authorized, 1* wrong pincode":
                                    alarm.body.alarmTypeCode = "09";
                                    alarm.body.alarmTypeName = "密码错误";
                                    break;
                                case "NOT authorized, distress pincode used":
                                    alarm.body.alarmTypeCode = "10";
                                    alarm.body.alarmTypeName = "胁迫报警";
                                    break;
                                default:
                                    break;
                            }
                            break;
                        //case "Areas.areaid"://区域id
                        //    alarm.body.areaId = "" + kvPair[1].Replace("\"", "").Trim();
                        //    break;
                        //case "Areas.name"://区域名称
                        //    alarm.body.areaName = "" + kvPair[1].Replace("\"", "").Trim();
                        //    break;
                        //case "Cards.cardid"://刷卡id

                        //    break;
                        //case "Cards.cardno"://刷卡no

                        //    break;
                        //case "Persons.persid"://刷卡人id

                        //    break;
                        //case "Persons.persno"://刷卡人no

                        //    break;
                        //case "Persons.firstname"://刷卡人姓

                        //    break;

                        //case "Persons.lastname"://刷卡人名

                        //    break;
                        default:
                            break;
                    }
                }
            }
            return alarm;
        }

        public SwipeEntity tcpToSwipe(string tcpMsg)
        {
            SwipeEntity swipte = new SwipeEntity(); ;
            var items = tcpMsg.Split(new string[] { @"/" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in items)
            {
                var kvPair = item.Split(new string[] { @"=" }, StringSplitOptions.RemoveEmptyEntries);
                {
                    switch (kvPair[0].Trim())
                    {
                        case "ltim"://时间
                            swipte.body.recordTime = DateTime.ParseExact(kvPair[1].Replace("W", "").Trim(),"yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                        case "Devices.displaytext"://设备名称
                            swipte.body.gateGuardName = kvPair[1].Replace("\"", "").Trim();
                            break;
                        case "Devices.id"://设备编号
                            swipte.body.gateGuardCode = kvPair[1].Replace("\"", "").Trim();
                            break;
                        case "Cards.cardid"://刷卡id

                            break;
                        case "Cards.cardno"://刷卡no
                            swipte.body.cardCode = kvPair[1].Replace("\"", "").Trim();
                            break;
                        case "Persons.persid"://刷卡人id

                            break;
                        case "Persons.persno"://刷卡人no
                            swipte.body.empCode = kvPair[1].Replace("\"", "").Trim();
                            break;
                        case "Persons.firstname"://刷卡人姓
                            swipte.body.empName += "" + kvPair[1].Replace("\"", "").Trim();
                            break;
                        case "Persons.lastname"://刷卡人名
                            swipte.body.empName += "" + kvPair[1].Replace("\"", "").Trim();
                            break;
                        default:
                            break;
                    }
                }
            }
            return swipte;
        }

        #region 防误关
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //判断是否选择的是最小化按钮
            if (WindowState == FormWindowState.Minimized)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                notifyIcon1.Visible = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cf.canClose == false)
            {
                //隐藏任务栏区图标
                this.ShowInTaskbar = false;
                //图标显示在托盘区
                notifyIcon1.Visible = true;
                e.Cancel = true;
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cf = new CloseForm();
            cf.Show();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                //还原窗体显示    
                WindowState = FormWindowState.Normal;
                //激活窗体并给予它焦点
                this.Activate();
                //任务栏区显示图标
                this.ShowInTaskbar = true;
                //托盘区图标隐藏
                notifyIcon1.Visible = false;
            }
        }
        #endregion
        

        /// <summary>
        /// 命令接收、执行
        /// </summary>
        /// <param name="message"></param>
        private void KafkaWorker_OnGetMessage(string message)
        {
            if (message.Contains("FIRST_CARD_DATA_REQ"))
            {
                var persons = SqlServerHelper.getPersons();
                foreach (PersonEntity person in persons)
                {
                    string personMsg = person.toJson().Replace(@"\\",@"\") ;
                    KafkaWorker.sendPersonMessage(personMsg);
                }
            }
        }
    }
}
