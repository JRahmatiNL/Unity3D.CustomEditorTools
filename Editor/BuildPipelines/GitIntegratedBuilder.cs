using System;
using UnityEditor;

namespace JRahmatiNL.Unity3D.CustomEditorTools.BuildPipelines
{

    public class GitIntegratedBuilder<TScene> where TScene : Enum
    {
        private ModificationMode _modificationMode;
        public GitIntegratedBuilder(ModificationMode modificationMode)
        {
            _modificationMode = modificationMode;
        }

        private bool ConfirmBuildSideEffect()
        {
            if(_modificationMode == ModificationMode.WithoutUserConfirmation)
            {
                return true;
            }
            
            return EditorUtility.DisplayDialog(
                ("Temporary side effect"), 
                (
                    "This will temporarily change settings during build based on git description.\n\n" +
                    "Would you like to continue & revert the changes after completion?"
                ),
                ("Continue & revert changes on completion"),
                ("Abort")
            );
        }

        public void PerformBuild(CustomBuildTarget buildTarget)
        {
            switch (buildTarget)
            {
                case CustomBuildTarget.iOS:
                    var buildPath = PerformBuild(BuildTarget.iOS);
                    if(!string.IsNullOrWhiteSpace(buildPath))
                    {
                        XCodeService.Open(buildPath);
                    }
                break;
                case CustomBuildTarget.Android:
                    PerformBuild(BuildTarget.Android);
                break;
            }
        }

        private string PerformBuild(BuildTarget buildTarget)
        {
            if (!ConfirmBuildSideEffect())
            {
                return string.Empty;
            }

            var originalPlayerSettings = PlayerSettingsFactory.Create();
            var gitDescription = GitService.GetDescription();
            var gitBasedSettings = PlayerSettingsFactory.CreateByGitDescription(gitDescription);
            
            PlayerSettingsService.UpdateSettings(gitBasedSettings);
            var buildPath = BuildService<TScene>.PerformBuild(
                buildTarget, gitBasedSettings.PreferredBuildOptions
            );
            PlayerSettingsService.UpdateSettings(originalPlayerSettings);

            AssetDatabase.SaveAssets();
            
            return buildPath;
        }
    }
}