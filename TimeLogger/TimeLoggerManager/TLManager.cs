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

namespace TimeLoggerManager
{
    public partial class TLManager : Form
    {
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
