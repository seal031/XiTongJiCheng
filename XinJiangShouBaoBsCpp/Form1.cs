using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XinJiangShouBaoBsCpp
{
    public partial class Form1 : Form
    {
        private const string queryZoneCommandStr= "QUERY_ ZONE_STATUS";

        private static IntPtr handle;
        private static string localIp;
        private static string remoteIp;

        BoShi.TRANDATAPROC trandataprocDelegate = new BoShi.TRANDATAPROC(trandata);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (loadLocalConfig())
            {
                handle = BoShi.New_Object();
                BoShi.ArrangeRcvAddress(handle, localIp, localIp);
                var openReceiverResult = openReceiver();
                if (openReceiverResult == (uint)0)
                {
                    sendCommand(queryZoneCommandStr, "0");
                }
                else
                {
                    FileWorker.LogHelper.WriteLog("打开接收事件/发送控制功能失败，返回值为" + openReceiverResult);
                }
            }
        }

        private bool loadLocalConfig()
        {
            try
            {
                localIp = ConfigWorker.GetConfigValue("localIp");
                remoteIp = ConfigWorker.GetConfigValue("remoteIp");
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("本地配置项不正确");
                return false;
            }
        }

        /// <summary>
        /// 打开接收事件/发送控制功能
        /// </summary>
        /// <returns></returns>
        private uint openReceiver()
        {
            uint pPara = 0;
            return BoShi.OpenReceiver(handle, trandataprocDelegate, pPara);
            //返回值说明
            //0-- 成功，接收事件的IP地址以及发送控制命令的IP地址都成功打开
            //7-- 部分成功，接收事件的IP地址成功打开，但发送控制命令的IP地址没有设置或者无效或者打开失败，故当前能且仅能接收事件，不能发送控制命令；
            //11-- 部分成功，发送控制命令的IP地址成功打开，但接收事件的IP地址没有设置或者无效或者打开失败，故当前能且仅能发送控制命令，不能接收事件；
            //其它值-- 失败，接收事件的IP地址以及发送控制的IP地址都没有设置或者无效或者打开失败，故当前即不能发送控制命令，也不能接收事件。详细定义如下：
            //15-- 参数pObject为空;
            //40-- 加载CDS7400EXP.dll失败;
            //41-- 调用CDS7400EXP.dll函数失败;
            //1--  工作线程1未能启动;
            //2--  工作线程2未能启动;
            //3--  通讯线程未能启动;
            //4--  接收事件的IP地址是空或者无法辩识;
            //5--  打开接收事件的套接字失败;
            //6--  接收事件工作的套接字未准备好或者无法正常工作;
            //8--  发送控制命令的IP地址是空或者无法辨识, 同时也不能接收事件;
            //10-- 打开发送控制命令的套接字失败;
        }

        /// <summary>
        /// 让指定的IP地址的主机执行控制命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        private static int sendCommand(string command, string param)
        {
            return BoShi.Execute(handle, remoteIp, command, param);
            //返回值：
            //0--  已成功发送控制命令
            //- 2 –  控制命令为空或者对方的IP地址为空
            //- 3--  控制命令参数错误
            //- 4 –  对方的IP地址为空
            //- 5 –  第一个参数pObject为空;
            //-6 –  调用CDS7400EXP.dll函数失败;
            //-7 –  加载CDS7400EXP.dll失败;
            //-8 –  初始化CDS7400EXP.dll失败;
            //-9 –  不能辩识的IP地址;
            //-10 – 命令是空值;
            //-11 – 目前指定的主机暂时不能进行远程控制，请稍后再试（可能需要等待三分钟）;
        }

        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        /// <param name="pObject"></param>
        /// <param name="sRcvData"></param>
        /// <param name="iDataLen"></param>
        private static void trandata(uint pObject, string sRcvData, int iDataLen)
        {
            //防区状态含义，即下面的<04005002>的含义
            //0 – 相应防区为正常状态
            //1 – 相应防区为报警状态（短路）
            //2 – 相应防区为报警状态（开路）
            //3 – 相应防区为拆动状态
            //4 – 相应防区为旁路状态
            //5 – 相应防区为故障状态
            //6 – 相应防区为遗失状态
            //7 –（无线防区）电池电压低
            //8 –（无线防区）干扰
            //F – 防区未设置（无效）
            if (sRcvData.Contains("ZONES_STATE"))//含ZONES_STATE的为防区状态信息,格式为<192.168.1.31|4000><10:30:15><ZONES_STATE><0><04005002>
            {
                var items = sRcvData.Split(new string[] { "><" },StringSplitOptions.RemoveEmptyEntries);
                if (items.Length > 4)
                {
                    int zoneGroupNumber = 0;
                    string zoneGroupNumberStr = items[3];
                    if (int.TryParse(zoneGroupNumberStr, out zoneGroupNumber))
                    {
                        char[] zoneStatus = items[4].Replace(">","").ToCharArray();
                        for (int i = 0; i < zoneStatus.Length; i++)
                        {
                            if (zoneStatus[i] == '2')
                            {
                                FileWorker.LogHelper.WriteLog("第" + (zoneGroupNumber * 8 + i + 1) + "个防区块报警");
                                //todo
                            }
                        }
                    }
                    else
                    {
                        FileWorker.LogHelper.WriteLog("数据中的防区块编号不是数字，完整数据为" + sRcvData);
                    }
                }
            }
        }

        /// <summary>
        /// 设置用于监控连接是否保持的连接测试间隔时间(秒)
        /// </summary>
        /// <param name="intval"></param>
        private void setLnkIntval(int intval)
        {
            BoShi.SetLnkIntval(handle, intval);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            BoShi.CloseReciever(handle);
            BoShi.Delete_Object(handle);
        }
    }
}
