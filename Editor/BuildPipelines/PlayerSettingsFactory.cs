using UnityEditor;

namespace JRahmatiNL.Unity3D.CustomEditorTools.BuildPipelines
{
    internal class PlayerSettingsFactory
    {
        public static PlayerSettingsModel Create()
        {
            return new PlayerSettingsModel
            {
                ProductName = PlayerSettings.productName,
                BundleVersion = PlayerSettings.bundleVersion,
                IOSBuildNumber = PlayerSettings.iOS.buildNumber,
                AndroidVersionCode = PlayerSettings.Android.bundleVersionCode,
                PreferredBuildOptions = BuildOptions.None
            };
        }

        public static PlayerSettingsModel CreateByGitDescription(string gitDescription)
        {
            var gitBasedSettings = PlayerSettingsFactory.Create();
            if(!string.IsNullOrWhiteSpace(gitDescription))
            {
                if(gitDescription.Contains("-"))
                {
                    gitBasedSettings.PreferredBuildOptions = BuildOptions.Development;
                    gitBasedSettings.ProductName = $"JTG-{gitDescription}";
                }
                else if(gitDescription.StartsWith("v"))
                {
                    var versionString = gitDescription.Substring(1).Trim();
                    var versionCode = GetVersionCodeFromVersionString(versionString);
                    gitBasedSettings.BundleVersion = versionString;
                    gitBasedSettings.AndroidVersionCode = versionCode;
                    gitBasedSettings.IOSBuildNumber = "0";
                }
            }
            return gitBasedSettings;
        }

        private static int GetVersionCodeFromVersionString(string versionString)
        {
            var numbersInVersionString = versionString.Split('.');
            var majorNumberInVersionString = int.Parse(numbersInVersionString[0]);
            var minorNumberInVersionString = int.Parse(numbersInVersionString[1]);
            var patchNumberInVersionString = int.Parse(numbersInVersionString[2]);
            return int.Parse(string.Format(
                "2{0:000}{1:000}{2:000}", 
                majorNumberInVersionString, 
                minorNumberInVersionString,
                patchNumberInVersionString
            ));
        }
    }
}