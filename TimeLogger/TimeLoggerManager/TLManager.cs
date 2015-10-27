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
using System.IO.MemoryMappedFiles;
using System.Net.Configuration;
using System.Net.Sockets;


namespace TimeLoggerManager
{
    public partial class TLManager : Form
    {
        TcpClient clientSocket = new TcpClient();

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
            Msg("Client Started");
            //clientSocket.Connect("127.0.0.1", 8888);
            lblStatus.Text = "Client Socket Program - Server Connected ...";
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
            for (int i = atl.Entries.Count - 1; i >= 0; i--)
            {
                sb.AppendLine(atl.Entries[i].Message);
            }
            txtEventLog.Text = sb.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(
                @"G:\work\cloud\development\Duplicati\Duplicati\CommandLine\bin\Debug\Duplicati.CommandLine.exe", "backup & pause");
            //using (System.Diagnostics.Process process = new System.Diagnostics.Process())
            //{
            //    process.StartInfo = new System.Diagnostics.ProcessStartInfo(@"G:\work\cloud\development\Duplicati\Duplicati\CommandLine\bin\Debug\Duplicati.CommandLine.exe");
            //    process.StartInfo.CreateNoWindow = true;
            //    process.StartInfo.ErrorDialog = false;
            //    process.StartInfo.RedirectStandardError = true;
            //    process.StartInfo.RedirectStandardInput = true;
            //    process.StartInfo.RedirectStandardOutput = true;
            //    process.StartInfo.UseShellExecute = false;
            //    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //    process.Start();

            //    System.Threading.Thread.Sleep(2000);
            //    process.StandardInput.WriteLine("exit");
            //    if (!process.HasExited)
            //    {
            //        process.WaitForExit(2000);
            //        if (!process.HasExited)
            //        {
            //            process.Kill();
            //        }
            //    }
            //}
        }

        public void MakeMemoryMappedFile()
        {
            Task t = Task.Factory.StartNew(() =>
                {
                    MemoryMappedFile file = MemoryMappedFile.CreateOrOpen("MemoryFile", 6);
                    string sharingTemp = "Test";
                    char[] c = new char[sharingTemp.Length];
                    byte[] bytes = new byte[sharingTemp.Length + 1];
                    for (int i = 0; i < bytes.Length - 1; i++)
                    {
                        c[i] = sharingTemp[i];
                        bytes[i + 1] = (byte)c[i];
                    }
                    MemoryMappedViewAccessor writer = file.CreateViewAccessor(0, bytes.Length);
                    writer.WriteArray(0, bytes, 0, bytes.Length);
                });
        }

        public void ReadMemoryMappedFile()
        {
            Task tsk = Task.Factory.StartNew(() =>
                {
                    MemoryMappedFile file = MemoryMappedFile.OpenExisting("MemoryFile");
                    MemoryMappedViewAccessor reader = file.CreateViewAccessor(0, 1);
                    byte[] bytes = new byte[1];
                    reader.ReadArray(0, bytes, 0, bytes.Length);
                    txtEventLog.Text = "";
                    for (int i = 0; i < bytes.Length - 1; i++)
                    {
                        txtEventLog.AppendText(bytes[i].ToString() + " ");
                    }
                });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("ArcaneTimeLoggerServiceInstaller2");
            MakeMemoryMappedFile();
            lblStatus.Text = "Request";

            EventLog evt = new EventLog("ArcaneTimeLogger");
            string message = "Request" + ": " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            evt.Source = "ArcaneTimeLoggerService";
            evt.WriteEntry(message, EventLogEntryType.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ServiceController sc = new ServiceController("ArcaneTimeLoggerServiceInstaller2");
            ReadMemoryMappedFile();
            lblStatus.Text = "Response";

            EventLog evt = new EventLog("ArcaneTimeLogger");
            string message = "Response" + ": " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            evt.Source = "ArcaneTimeLoggerService";
            evt.WriteEntry(message, EventLogEntryType.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes(textBox1.Text + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, (int)clientSocket.ReceiveBufferSize);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            Msg(returndata);
            textBox1.Text = "";
            textBox1.Focus();
        }

        public void Msg(string mesg)
        {
            txtEventLog.Text = txtEventLog.Text + Environment.NewLine + " >> " + mesg;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
    }
}
