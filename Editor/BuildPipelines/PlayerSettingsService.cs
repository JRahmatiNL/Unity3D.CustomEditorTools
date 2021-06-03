using UnityEditor;

namespace JRahmatiNL.Unity3D.CustomEditorTools.BuildPipelines
{
    internal class PlayerSettingsService
    {
        internal static void UpdateSettings(PlayerSettingsModel playerSettings)
        {
            PlayerSettings.productName = playerSettings.ProductName;
            PlayerSettings.bundleVersion = playerSettings.BundleVersion;
            PlayerSettings.iOS.buildNumber = playerSettings.IOSBuildNumber;
            PlayerSettings.Android.bundleVersionCode = playerSettings.AndroidVersionCode;
        }
    }
}