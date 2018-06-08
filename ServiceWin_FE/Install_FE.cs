using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;

namespace ServiceWin_FE
{
    [RunInstaller(true)]
    public partial class Install_FE : System.Configuration.Install.Installer
    {
        public Install_FE()
        {
            InitializeComponent();
        }
    }
}
