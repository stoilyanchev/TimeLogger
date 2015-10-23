using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Diagnostics;
using System.Transactions;
using System.Messaging;
using System.Runtime.Serialization;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading;
using Eneter.Messaging.EndPoints.TypedMessages;
using Eneter.Messaging.MessagingSystems.MessagingSystemBase;
using Eneter.Messaging.MessagingSystems.SharedMemoryMessagingSystem;


namespace TimeLoggerManager
{
    public partial class TLManager : Form
    {
        //public class RequestData
        //{
        //    public int Number1 { get; set; }
        //    public int Number2 { get; set; }
        //}

        //private static IDuplexTypedMessageSender<int, RequestData> mySender;

        //public TLManager()
        //{
        //    InitializeComponent();

        //    // Create messaging using Shared Memory.
        //    IMessagingSystemFactory aMessaging = new SharedMemoryMessagingSystemFactory();
        //    IDuplexOutputChannel anOutputChannel =
        //        aMessaging.CreateDuplexOutputChannel("TimeLoggerSerice");

        //    // Create the sender, that sends 'RequestData' and receives 'int'.
        //    IDuplexTypedMessagesFactory aTypedMessagesFactory = new DuplexTypedMessagesFactory();
        //    mySender = aTypedMessagesFactory.CreateDuplexTypedMessageSender<int, RequestData>();

        //    // Register the handler receiving the result from the service.
        //    mySender.ResponseReceived += OnResponseReceived;

        //    // Attach the Named duplex output channel and be able to send messages
        //    // and receive response messages.
        //    mySender.AttachDuplexOutputChannel(anOutputChannel);


        //}

        //private void OnResponseReceived(object sender, TypedResponseReceivedEventArgs<int> e)
        //{
        //    if (e.ReceivingError == null)
        //    {
        //        InvokeInUIThread(() => txtEventLog.Text = e.ResponseMessage.ToString());
        //    }
        //}

        //private void InvokeInUIThread(Action action)
        //{
        //    if (InvokeRequired)
        //    {
        //        Invoke(action);
        //    }
        //    else
        //    {
        //        action();
        //    }
        //}


        public TLManager()
        {
            InitializeComponent();
        }

        private void TimerEventLog_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {
            txtEventLog.Text = e.Entry.Message + System.Environment.NewLine + txtEventLog.Text;
        }

        private void SetDisplay(ServiceController sc)
        {
            sc.Refresh();
            if (sc.Status == ServiceControllerStatus.Stopped)
            {
                btnStop.Enabled = false;
                btnStart.Enabled = true;
                btnLogIt.Enabled = false;
                lblStatus.Text = "Stopped";
            }
            if (sc.Status == ServiceControllerStatus.Running)
            {
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnLogIt.Enabled = true;
                lblStatus.Text = "Running";
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("ArcaneTimeLoggerServiceInstaller2");
            sc.Start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblStatus.Text = "Running";
            sc.Refresh();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("ArcaneTimeLoggerServiceInstaller2");
            sc.Stop();
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            lblStatus.Text = "Stopped";
            sc.Refresh();
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
        }

        private void TLManager_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            EventLog atl = new EventLog("ArcaneTimeLogger");
            for (int i = atl.Entries.Count - 1; i > -1; i--)
            {
                sb.AppendLine(atl.Entries[i].Message);
            }
            txtEventLog.Text = sb.ToString();
        }
        
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("ArcaneTimeLoggerServiceInstaller2");
            SetDisplay(sc);
        }

        private void btnLogIt_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("ArcaneTimeLoggerServiceInstaller2");
            sc.ExecuteCommand(255);
        }

        private void tmrRefresh_Tick_1(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("ArcaneTimeLoggerServiceInstaller2");
            SetDisplay(sc);
        }

        private void txtEventLog_TextChanged(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            EventLog atl = new EventLog("ArcaneTimeLogger");
            for (int i = atl.Entries.Count - 1; i > -1; i--)
            {
                sb.AppendLine(atl.Entries[i].Message);
            }
            txtEventLog.Text = sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
