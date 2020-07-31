using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WhGuanlang
{
    public class TcpClientTest
    {
        //Socket tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private TcpClient client=new TcpClient();

        public TcpClientTest()
        {
            client.Connect(IPAddress.Parse("172.28.161.253"), 20080);
        }

        public void receive()
        {
            //client.Connect(IPAddress.Parse("172.28.161.253"), 20080);
            //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket clientSocket = client.Client;
            //服务端链接ip与端口号
            //clientSocket.Connect(new IPEndPoint(IPAddress.Parse("172.28.161.253"), 20080));
            while (true)
            {
                byte[] data = new byte[1024];
                int count = clientSocket.Receive(data);
                string msg = Encoding.UTF8.GetString(data, 0, count);
                System.Diagnostics.Debug.WriteLine(msg);
            }
        }

        public void sendHeart()
        {
            using (NetworkStream strem = client.GetStream())
            {
                string str = "$HBT$"+Environment.NewLine;
                byte[] b = Encoding.UTF8.GetBytes(str);
                strem.Write(b, 0, b.Length);
                strem.Close();
            }
        }
    }
}
