using System.IO;

namespace JRahmatiNL.Unity3D.CustomEditorTools.BuildPipelines
{

    internal class XCodeService
    {
        public static void Open(string buildPath)
        {
            using (var xCodeProcess = new System.Diagnostics.Process())
            {
                xCodeProcess.StartInfo.FileName = "open";
                xCodeProcess.StartInfo.Arguments = Path.Combine(buildPath, "Unity-iPhone.xcodeproj");
                xCodeProcess.StartInfo.UseShellExecute = false;
                xCodeProcess.StartInfo.RedirectStandardOutput = true;
                xCodeProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                xCodeProcess.StartInfo.CreateNoWindow = true;
                xCodeProcess.Start();
            }
        }
    }
}