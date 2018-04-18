using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;


namespace EbixAdvantageTranLog
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void tLogServiceProcessInstaller_AfterInstall(object sender, InstallEventArgs e)
        {

        }

       
    }
}
