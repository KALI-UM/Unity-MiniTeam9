using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIManager))]

public class UIManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Generate UIElement Enum"))
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"public enum UIElements");
            sb.AppendLine(@"{");
            var uiManager = (UIManager)target;
            //sb.AppendLine($"\tNone,");
            for (int i = 0; i < uiManager.uiElements.Length; i++)
            {
                sb.AppendLine($"\t{uiManager.uiElements[i].name},");
            }
            sb.AppendLine(@"}");

            var path = EditorUtility.SaveFilePanel("Save", "Assets/Scripts/UI", "UIElements.cs", "cs");
            using (var fs = new FileStream(path, FileMode.Create))
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.Write(sb.ToString());
                }
            }

            AssetDatabase.Refresh();
        }

        if (GUILayout.Button("Generate FocusWindow Enum"))
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"public enum FocusWindows");
            sb.AppendLine(@"{");
            var uiManager = (UIManager)target;
            //sb.AppendLine($"\tNone,");
            for (int i = 0; i < uiManager.focusWindows.Length; i++)
            {
                sb.AppendLine($"\t{uiManager.focusWindows[i].name},");
            }
            sb.AppendLine(@"}");

            var path = EditorUtility.SaveFilePanel("Save", "Assets/Scripts/UI", "FocusWindows.cs", "cs");
            using (var fs = new FileStream(path, FileMode.Create))
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.Write(sb.ToString());
                }
            }

            AssetDatabase.Refresh();
        }

        if (GUILayout.Button("Generate PopWindow Enum"))
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"public enum PopWindows");
            sb.AppendLine(@"{");
            var uiManager = (UIManager)target;
            for (int i = 0; i < uiManager.popWindows.Length; i++)
            {
                sb.AppendLine($"\t{uiManager.popWindows[i].name},");
            }
            sb.AppendLine(@"}");

            var path = EditorUtility.SaveFilePanel("Save", "Assets/Scripts/UI", "PopWindows.cs", "cs");
            using (var fs = new FileStream(path, FileMode.Create))
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.Write(sb.ToString());
                }
            }

            AssetDatabase.Refresh();
        }

        DrawDefaultInspector();

    }
}
