namespace JRahmatiNL.Unity3D.CustomEditorTools.BuildPipelines
{
    internal class GitService
    {
        public static string GetDescription()
        {
            var gitDescription = default(string);
            using(var gitProcess = new System.Diagnostics.Process())
            {
                gitProcess.StartInfo.FileName = "git";
                gitProcess.StartInfo.Arguments = "describe --dirty";
                gitProcess.StartInfo.UseShellExecute = false;
                gitProcess.StartInfo.RedirectStandardOutput = true;
                gitProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                gitProcess.StartInfo.CreateNoWindow = true;
                gitProcess.Start();
                gitDescription = gitProcess.StandardOutput.ReadToEnd().Trim();
                gitProcess.WaitForExit();
            }
            return gitDescription;
        }
    }
}