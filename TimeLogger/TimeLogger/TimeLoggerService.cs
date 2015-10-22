using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Configuration.Install;
using System.Messaging;

namespace TimeLogger
{
    public partial class TimeLoggerService : ServiceBase
    {
        public enum commands
        {
            LogIt = 255
        }

        private Timer _timer = null;

        public TimeLoggerService()
        {
            InitializeComponent();
            _timer = new Timer(10000);
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed); 
        }

        private void WriteToLog(string msg)
        {
            EventLog evt = new EventLog("ArcaneTimeLogger");
            string message = msg + ": " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            evt.Source = "ArcaneTimeLoggerService";
            evt.WriteEntry(message, EventLogEntryType.Information);
        }

        protected override void OnStart(string[] args)
        {
            _timer.Start();
            WriteToLog("Arcane Start");
        }

        protected override void OnStop()
        {
            _timer.Stop();
            WriteToLog("Arcane Stop");
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            _timer.Start();
        }

        protected override void OnPause()
        {
            base.OnPause();
            _timer.Stop();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
            _timer.Stop();
        }

        protected void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WriteToLog("Arcane Timer");
        }

        protected override void OnCustomCommand(int command)
        {
            base.OnCustomCommand(command);
            if (command == (int)commands.LogIt)
            {
                WriteToLog("Arcane LogIt");
            }
        }

        public void OnDebug()
        {
            OnStart(null);
        }
    }
}
