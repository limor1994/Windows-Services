namespace ImageService
{
    partial class ProjectInstaller
    {

        #region Component Designer generated code

        /// <summary>Method Designer Support</summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.ImageService = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller
            // 
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;
            // 
            // ImageService
            // 
            this.ImageService.Description = "ImageService ";
            this.ImageService.DisplayName = "ImageService";
            this.ImageService.ServiceName = "ImageService";
            this.ImageService.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.ImageService});

        }

        #endregion

        /// <summary>designer component</summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>Dispose resource</summary>
        /// <param name="disposing">true - dispose, false - don't</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller ImageService;
    }
}