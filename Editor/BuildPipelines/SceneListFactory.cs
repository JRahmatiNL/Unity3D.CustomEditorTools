using System;
using System.Collections.Generic;
using System.Linq;

namespace JRahmatiNL.Unity3D.CustomEditorTools.BuildPipelines
{
    internal class SceneListFactory<TScene> where TScene : Enum
    {
        public static string[] Create()
        {
            var sceneValues = Enum.GetValues(typeof(TScene)).Cast<TScene>();

            var scenesToShowInAscendingOrder = sceneValues.Select(
                x => new KeyValuePair<TScene, int>(x, (int)(object) x)
            ).OrderBy(x => x.Value).Select(x => x.Key);

            return scenesToShowInAscendingOrder.Select(
                x => $"Assets/Scenes/{x}.unity"
            ).ToArray();
        }
    }
}