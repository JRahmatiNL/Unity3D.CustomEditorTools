using UnityEditor;

namespace JRahmatiNL.Unity3D.CustomEditorTools.Input
{
    public class CustomToolToggler
    {
        private static Tool _previousToolMode;
        [MenuItem("Edit/Toggle on or off custom tool %T", false, 5)]
        // Note: this only works after selecting the custom tool first in the Unity Editor!
        private static void ToggleCustomTool()
        {
            if (Tools.current != Tool.Custom)
            {
                _previousToolMode = Tools.current;
                Tools.current = Tool.Custom;
            }
            else
            {
                Tools.current = _previousToolMode;
            }
        }
    }
}