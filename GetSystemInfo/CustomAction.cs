using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;

namespace GetSystemInfo
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult GetSystemInfo(Session session)
        {
            session.Log("Begin Collecting System information");

            // Get OS information and store in properties
            string osPlatform = System.Environment.OSVersion.Platform.ToString();
            session["OS_Platform"]= osPlatform;
            string arch=System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE", EnvironmentVariableTarget.Machine).ToString();
            session["OS_Arch"] = arch;

            return ActionResult.Success;
        }
    }
}
