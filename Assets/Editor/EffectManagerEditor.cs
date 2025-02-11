using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EffectManager))]

public class EffectManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Update eEffect Enum"))
        {
            UpdateEffectsEnum();
        }
        base.OnInspectorGUI();
    }

    private void UpdateEffectsEnum()
    {
        var sb = new StringBuilder();
        sb.AppendLine(@"public enum eEffects");
        sb.AppendLine(@"{");
        var effectManager = (EffectManager)target;
        for (int i = 0; i < effectManager.effectPrefabs.Count; i++)
        {
            sb.AppendLine($"\t{effectManager.effectPrefabs[i].name},");
        }
        sb.AppendLine(@"}");

        var path = EditorUtility.SaveFilePanel("Save", "Assets/Scripts/Enums", "eEffects.cs", "cs");
        using (var fs = new FileStream(path, FileMode.Create))
        {
            using (var writer = new StreamWriter(fs))
            {
                writer.Write(sb.ToString());
            }
        }

        AssetDatabase.Refresh();
    }


}
