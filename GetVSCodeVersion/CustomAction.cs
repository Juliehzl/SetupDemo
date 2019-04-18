using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Deployment.WindowsInstaller;
using System.Diagnostics;

namespace GetVSCodeVersion
{
    public class CustomActions
    {
        [CustomAction]
        public static ActionResult GetVSCodeVersion(Session session)
        {
            session.Log("Begin Get VS Code Version");
            
            string VSCode_path = session["VSCODEPATH"] + "bin";
            //session.Log(VSCode_path);
            var pathvar = Environment.GetEnvironmentVariable("PATH");
            //session.Log(pathvar);
            if(!pathvar.Contains(VSCode_path))
            {
                // Set path and need administrator. Not work now
                session.Log("Start Set Path for VS Code");
                /*
                var value = pathvar + VSCode_path;
                var target = EnvironmentVariableTarget.User;
                System.Environment.SetEnvironmentVariable("PATH", value, target);*/

            }
       
            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c code -v";
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;

            p.Start();

            string error = p.StandardError.ReadToEnd();
            session.Log(error);

            //p.Start();

            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            
            string output = p.StandardOutput.ReadLine();
            //session.Log(output);

            session["VSCODEVERSION"] = output;
            p.WaitForExit();
            return ActionResult.Success;
        }
    }
}
