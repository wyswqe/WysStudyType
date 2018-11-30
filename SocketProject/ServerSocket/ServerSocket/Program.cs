using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ServerSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServerASync();
            Console.ReadKey();
        }
        static void StartServerASync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //127.0.0.1 代表本机
            //IpAddress xxx.xx.xx.xx IpEndPoint xxx.xx.xx.xx:port
            IPAddress ipAddress = IPAddress.Parse("192.168.1.105");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 88);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);//开始监听

            //Socket clientSocket = serverSocket.Accept();//接收连接
            serverSocket.BeginAccept(AcceptCallBack, serverSocket);
        }
        static Message msg = new Message();
        static void AcceptCallBack(IAsyncResult ar)
        {
            Socket serverSocket = ar.AsyncState as Socket;
            Socket clientSocket = serverSocket.EndAccept(ar);

            string msgStr = "服务器: Connect";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(msgStr);
            clientSocket.Send(data);

            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);

            serverSocket.BeginAccept(AcceptCallBack, serverSocket);
        }
        static byte[] dataBuffer = new byte[1024];
        static void ReceiveCallBack(IAsyncResult ar)
        {
            Socket clientSocket = null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    clientSocket.Close();
                    return;
                }
                msg.AddCount(count);
                //string msgStr = Encoding.UTF8.GetString(dataBuffer, 0, count);
                //Console.WriteLine("从客户端接收到数据: " + msgStr + " 长度:" + count);
                msg.ReadMessage();
                //clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);
                clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (clientSocket != null)
                {
                    clientSocket.Close();
                }
            }
        }

        void StartServerSync()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //127.0.0.1 代表本机
            //IpAddress xxx.xx.xx.xx IpEndPoint xxx.xx.xx.xx:port
            IPAddress ipAddress = IPAddress.Parse("192.168.1.105");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 88);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);//开始监听
            Socket clientSocket = serverSocket.Accept();//接收连接

            string msg = "服务器: *(!@&#!@#*(HDJADS";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);
            clientSocket.Send(data);

            byte[] dataBuffer = new byte[1024];
            int count = clientSocket.Receive(dataBuffer);
            string msgReceive = System.Text.Encoding.UTF8.GetString(dataBuffer, 0, count);
            Console.WriteLine(msgReceive);

            clientSocket.Close();
            serverSocket.Close();
        }
    }
}
