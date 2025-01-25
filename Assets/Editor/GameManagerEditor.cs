using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
using static TowerTable;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //if (GUILayout.Button("Update Managers Enum"))
        //{
        //    UpdateManagersEnum();
        //}

        base.OnInspectorGUI();
    }

    //private void UpdateManagersEnum()
    //{
    //    var sb = new StringBuilder();
    //    sb.AppendLine(@"public enum InGameManagers");
    //    sb.AppendLine(@"{");
    //    var gameManager = (GameManager)target;
    //    for (int i = 0; i < gameManager.managers.Length; i++)
    //    {
    //        sb.AppendLine($"\t{gameManager.managers[i].name},");
    //    }
    //    sb.AppendLine(@"}");

    //    var path = EditorUtility.SaveFilePanel("Save", "Assets/Scripts/Enums", "InGameManagers.cs", "cs");
    //    using (var fs = new FileStream(path, FileMode.Create))
    //    {
    //        using (var writer = new StreamWriter(fs))
    //        {
    //            writer.Write(sb.ToString());
    //        }
    //    }

    //    AssetDatabase.Refresh();
    //}
}
