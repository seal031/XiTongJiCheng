using LiteNetLib;
using LiteNetLib.Utils;
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

namespace UdpServerTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

            EventBasedNetListener listener = new EventBasedNetListener();
            NetManager server ;
        private void runServer()
        {
            server = new NetManager(listener);
            server.UnsyncedEvents = true;
            server.DisconnectTimeout = 20000;
            //var a = server.Start("172.173.121.45","",3000);
            var a = server.Start();
            listener.ConnectionRequestEvent += request =>
            {
                if (server.PeersCount < 10 /* max connections */)
                    request.AcceptIfKey("SomeConnectionKey");
                else
                    request.Reject();
            };

            listener.PeerConnectedEvent += peer =>
            {
                Debug.WriteLine("We got connection: {0}", peer.EndPoint.ToString()); // Show peer ip
                //NetDataWriter writer = new NetDataWriter();                 // Create writer class
                //writer.Put("Hello client!");                                // Put some string
                //peer.Send(writer, DeliveryMethod.ReliableOrdered);             // Send with reliability
            };

            listener.NetworkReceiveEvent += Listener_NetworkReceiveEvent;
            listener.NetworkReceiveUnconnectedEvent += Listener_NetworkReceiveUnconnectedEvent;

            //while (true)
            //{
            //    server.PollEvents();
            //    Thread.Sleep(15);
            //}
        }

        private void Listener_NetworkReceiveUnconnectedEvent(System.Net.IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {
            throw new NotImplementedException();
        }

        private void Listener_NetworkReceiveEvent(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            throw new NotImplementedException();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            runServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            server.PollEvents();
        }

        private UdpClient udpcRecv;
        Thread thrRecv;
        private void button3_Click(object sender, EventArgs e)
        {
            IPEndPoint localIpep = new IPEndPoint(IPAddress.Parse("192.168.74.183"), 3000); // 本机IP和监听端口号  
            udpcRecv = new UdpClient(localIpep);
            thrRecv = new Thread(ReceiveMessage);
            thrRecv.Start();

        }
        private void ReceiveMessage(object obj)
        {
            IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Parse("172.173.121.9"), 2000);
            while (true)
            {
                try
                {
                    byte[] bytRecv = udpcRecv.Receive(ref remoteIpep);
                    string str = Convert.ToBase64String(bytRecv);
                    var hex = BitConverter.ToString(bytRecv, 0).Replace("-", string.Empty).ToLower();
                    string message = Encoding.Unicode.GetString(bytRecv, 0, bytRecv.Length);
                    MessageBox.Show( message);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    break;
                }
            }
        }
    }
}
