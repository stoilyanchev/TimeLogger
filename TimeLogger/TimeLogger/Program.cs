using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Eneter.Messaging.EndPoints.TypedMessages;
using Eneter.Messaging.MessagingSystems.MessagingSystemBase;
using Eneter.Messaging.MessagingSystems.SharedMemoryMessagingSystem;
using Eneter.Messaging.MessagingSystems.TcpMessagingSystem;
using Eneter.Messaging.MessagingSystems.WebSocketMessagingSystem;
using Eneter.Messaging.MessagingSystems.SynchronousMessagingSystem;
using Eneter.Messaging.Nodes.Dispatcher;
using System.IO.MemoryMappedFiles;

namespace TimeLogger
{
    public class RequestData
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }
   

    static class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int size;
                MemoryMappedFile file = MemoryMappedFile.OpenExisting("MemoryFile");
                MemoryMappedViewAccessor reader = file.CreateViewAccessor(0, 1);
                byte[] bytes = new byte[1];
                reader.ReadArray(0, bytes, 0, bytes.Length);

                size = (int)bytes[0] + 1;

                reader = file.CreateViewAccessor(0, size);
                bytes = new byte[size];
                reader.ReadArray(0, bytes, 0, bytes.Length);

                char[] c = new char[size];


                for (int i = 0; i < bytes.Length; i++)
                {
                    if (i > 0)
                        c[i - 1] = (char)bytes[i];
                }

                string temp = new string(c);
                Console.WriteLine("Memory sharing success!");
                Console.WriteLine("String received: {0}", temp);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
            }

            Console.WriteLine("Press any key to exit.");


            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            {
                new TimeLoggerService()
            };
            ServiceBase.Run(ServicesToRun);


            TcpListener serverSocket = new TcpListener(8888);
            int requestCount = 0;
            TcpClient clientSocket = default(TcpClient);
            serverSocket.Start();
            Console.WriteLine(" >> Server Started");
            clientSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine(" >> Accept connection from client");
            requestCount = 0;

            while (true)
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    byte[] bytesFrom = new byte[10025];
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    string dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> Data from client - " + dataFromClient);
                    string serverResponse = "Last Message from client" + dataFromClient;
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + serverResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            clientSocket.Close();
            serverSocket.Stop();
            Console.WriteLine(" >> exit");
            Console.ReadLine();
        }
    }
}
