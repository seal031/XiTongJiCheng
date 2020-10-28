using Modbus.Device;
using ShaoGuanoXiaoFangBaoJing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShaoGuanXiaoFangBaoJing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void getData()
        {
            LogHelper.WriteLog("一次扫描开始");
        //    startAddress = (ushort)(int.Parse(ConfigWorker.GetConfigValue("StartAddress")));//每次扫描开始，将startAddress置为配置文件中的数
        //    deviceIndex = 0; //每次扫描开始，将deviceIndex置为0
        //    try
        //    {
        //        List<string> list = ip.Split(new string[]
        //            {
        //        "."
        //            }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
        //        byte[] array = new byte[list.Count];
        //        for (int i = 0; i < list.Count; i++)
        //        {
        //            array[i] = Convert.ToByte(list[i]);
        //        }
        //        IPAddress iPAddress = new IPAddress(array);//502
        //        using (TcpClient tcpClient = new TcpClient(iPAddress.ToString(), 502))
        //        {
        //            //tcpClient.SendTimeout = 1;
        //            ModbusIpMaster modbusIpMaster = ModbusIpMaster.CreateIp(tcpClient);
        //            while (startAddress <= maxPoint)//循环读取，每次numInputs个，startAddress递增
        //            {
        //                if (!(functionType == "Hold"))
        //                {
        //                    if (functionType == "Input")
        //                    {
        //                        ushort[] data = modbusIpMaster.ReadInputRegisters(slaveAddress, startAddress, numInputs);
        //                        LogHelper.WriteLog("收到数据" + string.Join(",", data));
        //                        this.dealData(data, (int)numInputs);
        //                    }
        //                }
        //                else
        //                {
        //                    ushort[] data = modbusIpMaster.ReadHoldingRegisters(slaveAddress, startAddress, numInputs);
        //                    LogHelper.WriteLog("收到数据" + string.Join(",", data));
        //                    this.dealData(data, (int)numInputs);
        //                }
        //                startAddress += numInputs;
        //                //richTextBox1.Text += Environment.NewLine;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLog(ex.Message);
        //    }
        //    finally
        //    {
        //        LogHelper.WriteLog("一次扫描完毕");
        //    }
        }
    }
}
