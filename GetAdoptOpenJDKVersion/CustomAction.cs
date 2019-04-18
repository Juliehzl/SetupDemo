using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;
using Microsoft.Win32;
namespace GetAdoptOpenJDKVersion
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult GetAdoptOpenJDKVersion(Session session)
        {
            session.Log("Begin Check AdoptOpenJDK installed and get version number");
            //RegistryKey HKLM = Registry.LocalMachine;
            RegistryKey HKLM = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64); ;
            if (session["OS_Arch"] == "x86")
            {
                HKLM = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            }
            
            RegistryKey OurKey = HKLM.OpenSubKey(@"SOFTWARE\AdoptOpenJDK\JDK");
            if (OurKey != null)
            {

                
                foreach (string Keyname in OurKey.GetSubKeyNames())
                {
                    //session.Log(Keyname);
                    session["JDKVERSION"] = Keyname;
                    RegistryKey key = OurKey.OpenSubKey(Keyname).OpenSubKey("MSI");
                    string JavaHome = key.GetValue("Path").ToString();
                    //session.Log(JavaHome);
                    session["JAVAHOME"] = JavaHome;
                }
            }
            else
                session.Log("AdoptOpenJDK NOT Installed!");

            return ActionResult.Success;
        }
    }
}
