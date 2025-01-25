using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WindowManager))]

public class WindowManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Generate FocusWindow Enum"))
        {
            var sb = new StringBuilder();
            sb.AppendLine(@"public enum FocusWindows");
            sb.AppendLine(@"{");
            var windowManager = (WindowManager)target;
            //sb.AppendLine($"\tNone,");
            for (int i = 0; i < windowManager.focusWindows.Length; i++)
            {
                sb.AppendLine($"\t{windowManager.focusWindows[i].name},");
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
            var windowManager = (WindowManager)target;
            for (int i = 0; i < windowManager.popWindows.Length; i++)
            {
                sb.AppendLine($"\t{windowManager.popWindows[i].name},");
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
