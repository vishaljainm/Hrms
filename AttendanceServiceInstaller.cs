﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace HyderabadService
{
    [RunInstaller(true)]
    public class AttendanceServiceInstaller : Installer
    {/// <summary>
     /// Public Constructor for WindowsServiceInstaller.
     /// - Put all of your Initialization code here.
     /// </summary>
        public AttendanceServiceInstaller()
        {
            ServiceProcessInstaller serviceProcessInstaller =
                               new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            //# Service Account Information
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            //# Service Information
            serviceInstaller.DisplayName = "Attendance Hyderabad Service";
            serviceInstaller.StartType = ServiceStartMode.Automatic;

            //# This must be identical to the WindowsService.ServiceBase name
            //# set in the constructor of WindowsService.cs
            serviceInstaller.ServiceName = " Hyderabad  Service";

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);

        }
    }
}