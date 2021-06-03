using UnityEditor;

namespace JRahmatiNL.Unity3D.CustomEditorTools.BuildPipelines
{
    internal class PlayerSettingsModel
    {
        internal string ProductName;
        internal string BundleVersion;
        internal string IOSBuildNumber;
        internal int AndroidVersionCode;
        internal BuildOptions PreferredBuildOptions;
    }
}