namespace TimeLoggerManager
{
    partial class TLManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnLogIt = new System.Windows.Forms.Button();
            this.txtEventLog = new System.Windows.Forms.TextBox();
            this.TimerEventLog = new System.Diagnostics.EventLog();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TimerEventLog)).BeginInit();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AccessibleName = "lblStatus";
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(29, 28);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(45, 16);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "label1";
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // btnStart
            // 
            this.btnStart.AccessibleName = "btnStart";
            this.btnStart.Location = new System.Drawing.Point(32, 80);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.AccessibleName = "btnStop";
            this.btnStop.Location = new System.Drawing.Point(155, 80);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnLogIt
            // 
            this.btnLogIt.Location = new System.Drawing.Point(282, 80);
            this.btnLogIt.Name = "btnLogIt";
            this.btnLogIt.Size = new System.Drawing.Size(75, 23);
            this.btnLogIt.TabIndex = 3;
            this.btnLogIt.Text = "LogIt";
            this.btnLogIt.UseVisualStyleBackColor = true;
            this.btnLogIt.Click += new System.EventHandler(this.btnLogIt_Click);
            // 
            // txtEventLog
            // 
            this.txtEventLog.Location = new System.Drawing.Point(32, 181);
            this.txtEventLog.Multiline = true;
            this.txtEventLog.Name = "txtEventLog";
            this.txtEventLog.Size = new System.Drawing.Size(325, 20);
            this.txtEventLog.TabIndex = 4;
            this.txtEventLog.TextChanged += new System.EventHandler(this.txtEventLog_TextChanged);
            // 
            // TimerEventLog
            // 
            this.TimerEventLog.EnableRaisingEvents = true;
            this.TimerEventLog.Log = "ArcaneTimeLogger";
            this.TimerEventLog.Source = "ArcaneTimeLoggerService";
            this.TimerEventLog.SynchronizingObject = this;
            this.TimerEventLog.EntryWritten += new System.Diagnostics.EntryWrittenEventHandler(this.TimerEventLog_EntryWritten);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Enabled = true;
            this.tmrRefresh.Interval = 10000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(155, 127);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TLManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 236);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtEventLog);
            this.Controls.Add(this.btnLogIt);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblStatus);
            this.Name = "TLManager";
            this.Text = "TLManager";
            this.Load += new System.EventHandler(this.TLManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TimerEventLog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnLogIt;
        private System.Windows.Forms.TextBox txtEventLog;
        private System.Diagnostics.EventLog TimerEventLog;
        public System.Windows.Forms.Timer tmrRefresh;
        private System.Windows.Forms.Button button1;
    }
}

