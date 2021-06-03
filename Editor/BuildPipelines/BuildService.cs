using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace JRahmatiNL.Unity3D.CustomEditorTools.BuildPipelines
{
    internal class BuildService<TScene> where TScene : Enum
    {
        public static string PerformBuild(BuildTarget buildTarget, BuildOptions buildOptions)
        {
            var buildPath = $"../Build/{buildTarget}";
            buildPath += buildTarget == BuildTarget.Android ? ".apk" : "";
            var directoryInfo = Directory.CreateDirectory(buildPath);
            Debug.Log($"Building {buildTarget} in {directoryInfo.FullName}");
            var buildReport = BuildPipeline.BuildPlayer(
                SceneListFactory<TScene>.Create(), buildPath, buildTarget,
                buildOptions
            );
            if (buildReport.summary.result != BuildResult.Succeeded)
            {
                return string.Empty;
            }
            return buildPath;
        }
    }
}