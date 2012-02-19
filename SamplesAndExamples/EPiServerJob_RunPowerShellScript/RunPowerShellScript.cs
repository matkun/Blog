using System.Text;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace EPiServer.CodeSample.ScheduledJobs
{
    [EPiServer.PlugIn.ScheduledPlugIn(
        DisplayName = "Run a PowerShell script",
        Description = "A scheduled job showing how to run a PowerShell script on regular intervals.",
        SortIndex = 1
    )] 
    public class RunPowerShellScript
    {
        public static string Execute()
        {
            var message = string.Empty;
            using(var runspace = RunspaceFactory.CreateRunspace())
            {
                message = RunScript(runspace);
            }
            return message;
        }

        private static string RunScript(Runspace runspace)
        {
            runspace.Open();
            var pipeline = runspace.CreatePipeline();

            var runspaceInvoke = new RunspaceInvoke(runspace);
            runspaceInvoke.Invoke("Set-ExecutionPolicy -ExecutionPolicy Unrestricted -Scope Process -Force");

            pipeline.Commands.AddScript(@"D:\Projekt\EPiServer\SampleCode\CodeSample\ScheduledJobs\SampleScript.ps1");
            pipeline.Commands.Add("Out-String");

            var stringBuilder = new StringBuilder();
            foreach (var psObject in pipeline.Invoke())
            {
                stringBuilder.AppendLine(psObject.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}
