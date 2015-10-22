namespace TimeLogger
{
    partial class ProjectInstaller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ArcaneTimeLoggerServiceProcessInstaller2 = new System.ServiceProcess.ServiceProcessInstaller();
            this.ArcaneTimeLoggerServiceInstaller2 = new System.ServiceProcess.ServiceInstaller();
            // 
            // ArcaneTimeLoggerServiceProcessInstaller2
            // 
            this.ArcaneTimeLoggerServiceProcessInstaller2.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.ArcaneTimeLoggerServiceProcessInstaller2.Password = null;
            this.ArcaneTimeLoggerServiceProcessInstaller2.Username = null;
            // 
            // ArcaneTimeLoggerServiceInstaller2
            // 
            this.ArcaneTimeLoggerServiceInstaller2.Description = "ArcaneTimeLoggerServiceInstaller2";
            this.ArcaneTimeLoggerServiceInstaller2.DisplayName = "ArcaneTimeLoggerServiceInstaller2";
            this.ArcaneTimeLoggerServiceInstaller2.ServiceName = "ArcaneTimeLoggerServiceInstaller2";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ArcaneTimeLoggerServiceProcessInstaller2,
            this.ArcaneTimeLoggerServiceInstaller2});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller ArcaneTimeLoggerServiceProcessInstaller2;
        public System.ServiceProcess.ServiceInstaller ArcaneTimeLoggerServiceInstaller2;
    }
}