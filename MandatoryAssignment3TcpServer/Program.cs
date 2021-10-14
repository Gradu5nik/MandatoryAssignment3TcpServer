using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using MandatoryAssignment2RestService;
using MandatoryAssigment1Library;
using System.Threading.Tasks;

namespace MandatoryAssignment3TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 4646);
            listener.Start();
            while (true)
            {
                Console.WriteLine("Server ready");
                TcpClient socket = listener.AcceptTcpClient();
                Console.WriteLine("Incoming client");
                Task.Run(() =>
                {
                    DoClient(socket);
                });
            }
        }
        private static void DoClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);
            RestClient slave = new RestClient();
            string rmessage;
            while (true)
            {
                string message1 = reader.ReadLine().ToLower();
                string message2 = reader.ReadLine().ToLower();
                switch (message1)
                {
                    case "get":
                        rmessage = slave.Get(message2).Result;
                        writer.WriteLine(rmessage);
                        writer.Flush();
                        break;
                    case "getall":
                        rmessage = slave.GetAll().Result;
                        writer.WriteLine(rmessage);
                        writer.Flush();
                        break;
                    case "post":
                        _ = slave.Post(message2);
                        break;
                    case "put":
                        _ = slave.Put(message2);
                        break;
                    case "delete":
                        slave.Remove(message2);
                        break;
                    case "help":
                        break;
                    case "close":
                        writer.Write("Server closed connection");
                        writer.Flush();
                        socket.Close();
                        break;
                    default:
                        break;
                }
                if (message1 == "close") break;
            }

        }
    }
}
