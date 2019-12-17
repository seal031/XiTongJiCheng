using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus;
using System.Net;
using Modbus.Device;
using System.Net.Sockets;

namespace JiDianQiKongZhi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void setData()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ushort[] data = new ushort[16] { 0, 1, 0, 5, 0, 0, 0, 0, FF, 15, 0, 0, 8, 12, 3, 10 };
            //string asString;
            //byte[] asBytes = new byte[data.Length * sizeof(ushort)];
            //Buffer.BlockCopy(data, 0, asBytes, 0, asBytes.Length);
            //asString = Encoding.Unicode.GetString(asBytes);
            open();
        }

        //01 05 00 00 FF 00 8C 3A 开1
        //01 05 00 00 00 00 CD CA 关1
        //01 05 00 01 FF 00 DD FA 开2
        //01 05 00 01 00 00 9C 0A 关2
        private void open()
        {
            List<string> list = "192.168.3.138".Split(new string[]
               {
                "."
               }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            byte[] array = new byte[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = Convert.ToByte(list[i]);
            }
            IPAddress iPAddress = new IPAddress(array);
            using (TcpClient tcpClient = new TcpClient(iPAddress.ToString(), 1030))
            {
                tcpClient.SendTimeout = 1;
                ModbusIpMaster modbusIpMaster = ModbusIpMaster.CreateIp(tcpClient);
                try
                {
                    //byte[] bytes = HexStrTobyte("01 05 00 00 FF 00 8C 3A");
                    //byte[] bytes = HexStrTobyte("01050000FF008C3A");
                    //ushort[] data = new ushort[8];
                    //Buffer.BlockCopy(bytes, 0, data, 0, 8);
                    ////ushort[] data1 = { (ushort)Convert.ToInt64("01", 16) };
                    ////ushort[] data2 = { (ushort)Convert.ToInt64("05", 16) };
                    ////ushort[] data3 = { (ushort)Convert.ToInt64("00", 16) };
                    ////ushort[] data4 = { (ushort)Convert.ToInt64("00", 16) };
                    ////ushort[] data5 = { (ushort)Convert.ToInt64("FF", 16) };
                    ////ushort[] data6 = { (ushort)Convert.ToInt64("00", 16) };
                    ////ushort[] data7 = { (ushort)Convert.ToInt64("8C", 16) };
                    ////ushort[] data8 = { (ushort)Convert.ToInt64("3A", 16) };
                    ////ushort[] data = new ushort[8];
                    ////data1.CopyTo(data, 0);
                    ////data2.CopyTo(data, 1);
                    ////data3.CopyTo(data, 2);
                    ////data4.CopyTo(data, 3);
                    ////data5.CopyTo(data, 4);
                    ////data6.CopyTo(data, 5);
                    ////data7.CopyTo(data, 6);
                    ////data8.CopyTo(data, 7);

                    //ushort[] data= { (ushort)Convert.ToInt64("01050000FF008C3A", 16) };
                    ushort data = (ushort)Convert.ToUInt64("01050000FF00", 16);

                    //ushort[] data = modbusIpMaster.ReadInputRegisters(slaveAddress, startAddress, numInputs);
                    //ushort[] data = new ushort[8] { 01, 05, 00, 00, 255, 00, 140, 58 };
                    //byte[] bytes = new byte[8] { 01, 05, 00, 00, 255, 00, 140, 58 };
                    //ushort data = BitConverter.ToUInt16(bytes, 0);
                    //byte[] bytes = new byte[8] { 00000001,00000101,00000000,00000000,11111111,00000000,1000010,00111010};
                    modbusIpMaster.WriteSingleRegister(0, data);
                    //modbusIpMaster.WriteMultipleRegisters(0, data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private byte[] HexStrTobyte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }
        //public ushort[] stringToUshort(String inString)
        //{
        //    if (inString.Length % 2 == 1) { inString += " "};
        //    char[] bufChar = inString.ToCharArray();
        //    byte[] oufByte = new byte[outChar.Length];
        //    byte[] bufByte = new byte[2];
        //    ushort[] outShort = new ushort[bufChar.Length / 2];
        //    for (int i = 0, j = 0; i < bufChar.Length; i += 2, j++)
        //    {
        //        bufByte[0] = outByte[i];
        //        bufByte[1] = outByte[i + 1];
        //        outShort[j] = BitConverter.ToUint16(bufByte, 0);
        //    }
        //    return outShort;
        //}
    }
}
