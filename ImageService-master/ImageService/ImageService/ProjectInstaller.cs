using System.ComponentModel;
using System.Collections;

namespace ImageService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }



        protected override void OnBeforeInstall(IDictionary savedState)
        {
            string parameter = "MySource\" \"MyLogFile";
            Context.Parameters["assemblypath"] = "\"" + Context.Parameters["assemblypath"] + "\" \"" + parameter + "\"";
            base.OnBeforeInstall(savedState);
        }
    }
   
}
