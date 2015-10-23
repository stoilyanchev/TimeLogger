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

namespace TimeLogger
{
    public class RequestData
    {
        public int Number1 { get; set; }
        public int Number2 { get; set; }
    }
    
    static class Program
    {
        private static IDuplexTypedMessageReceiver<int, RequestData> myReceiver;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
#if DEBUG
            TimeLoggerService myService = new TimeLoggerService();
            myService.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            {
                new TimeLoggerService() 
            };
            ServiceBase.Run(ServicesToRun);
#endif

        //    // Create local messaging connecting the dispatcher with the receiver.
        //    IMessagingSystemFactory aLocalMessaging = new SynchronousMessagingSystemFactory();
        //    IDuplexInputChannel aLocalInputChannel =
        //        aLocalMessaging.CreateDuplexInputChannel("MyLocalAddress");

        //    IDuplexTypedMessagesFactory aTypedMessagesFactory = new DuplexTypedMessagesFactory();
        //    myReceiver = aTypedMessagesFactory.CreateDuplexTypedMessageReceiver<int, RequestData>();
        //    myReceiver.MessageReceived += OnMessageReceived;

        //    // Attach the local input channel to the receiver and start to receive messages.
        //    myReceiver.AttachDuplexInputChannel(aLocalInputChannel);

        //    IMessagingSystemFactory aSharedMemoryMessaging = new SharedMemoryMessagingSystemFactory();
        //    IDuplexInputChannel aSharedMemoryInputChannel = aSharedMemoryMessaging.CreateDuplexInputChannel("TimeLoggerService");

        //    // Create dispatcher that will receive messages via WebSockets, TCP and Shared Memory
        //    // and forward them to the local address "MyLocalAddress" -> i.e. to our receiver.
        //    IDuplexDispatcherFactory aDispatcherFactory = new DuplexDispatcherFactory(aLocalMessaging);
        //    IDuplexDispatcher aDispatcher = aDispatcherFactory.CreateDuplexDispatcher();
        //    aDispatcher.AddDuplexOutputChannel("MyLocalAddress");

        //    aDispatcher.AttachDuplexInputChannel(aSharedMemoryInputChannel);
        //    Console.WriteLine("Listening to SharedMemory");

        //    aDispatcher.DetachDuplexInputChannel();
        //}

        //// The handler called when a message is received.
        //static void OnMessageReceived(object sender, TypedRequestReceivedEventArgs<RequestData> e)
        //{
        //    if (e.ReceivingError == null)
        //    {
        //        int aResult = e.RequestMessage.Number1 + e.RequestMessage.Number2;
        //        Console.WriteLine("{0} + {1} = {2}", e.RequestMessage.Number1, e.RequestMessage.Number2, aResult);

        //        // Send the result back to the client.
        //        myReceiver.SendResponseMessage(e.ResponseReceiverId, aResult);
        //    }
        //}
    }
}
