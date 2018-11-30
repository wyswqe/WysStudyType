using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TCP客户端
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("192.168.1.105"), 88));

            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);
            string msg = Encoding.UTF8.GetString(data, 0, count);
            Console.WriteLine(msg);

            while (true)
            {
                string s = Console.ReadLine();
                //Console.WriteLine(s);

                //if (s == "c")
                //{
                //    clientSocket.Close(); return;
                //}
                //clientSocket.Send(Encoding.UTF8.GetBytes(s));

                for (int i = 0; i < 100; i++)
                {
                    clientSocket.Send(Message.GetBytes(i.ToString()));
                }
                Console.ReadKey();
                clientSocket.Close();
            }
        }
    }
}
