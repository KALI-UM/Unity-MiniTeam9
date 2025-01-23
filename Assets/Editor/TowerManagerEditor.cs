using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JetBrains.Annotations;
using static TowerTable;
using System.IO;
using System.Text;
using UnityEngine.TextCore.Text;
using DG.DemiEditor;
using System;
using UnityEditor.TerrainTools;

[ExecuteInEditMode]
[CustomEditor(typeof(TowerManager))]
public class TowerManagerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Update Tower Enum"))
        {
            var path = string.Format(DataTable.FormatPath, DataTableIds.Tower);
            var textAsset = Resources.Load<UnityEngine.TextAsset>(path);
            var list = DataTable.LoadCSV<TowerRawData>(textAsset.text);
            UpdateTowerEnum(list);
        }

        
        if (GUILayout.Button("Update Tower Prefabs"))
        {
            var path = string.Format(DataTable.FormatPath, DataTableIds.Tower);
            var textAsset = Resources.Load<UnityEngine.TextAsset>(path);
            var list = DataTable.LoadCSV<TowerRawData>(textAsset.text);

            var towerManager = (TowerManager)target;
            UpdateTowerPrefabs(towerManager.defaultTowerPrefab, list);
        }

        EditorGUILayout.LabelField("RangeFactor");
        float currRangeFactor = EditorGUILayout.Slider(TowerManager.RangeFactor, 0.1f, 5f);
        if(TowerManager.RangeFactor!= currRangeFactor)
        {
            TowerManager.RangeFactor = currRangeFactor;
        }

        EditorGUILayout.LabelField("SpeedFactor");
        float currSpeedFactor = EditorGUILayout.Slider(TowerManager.SpeedFactor, 0.1f, 5f);
        if (TowerManager.SpeedFactor != currSpeedFactor)
        {
            TowerManager.SpeedFactor = currSpeedFactor;
        }

        base.OnInspectorGUI();
    }



    private void UpdateTowerEnum(List<TowerRawData> list)
    {
        var sb = new StringBuilder();
        sb.AppendLine(@"public enum eTower");
        sb.AppendLine(@"{");

        foreach (var data in list)
        {
            sb.AppendLine($"\t{data.String_Key} = {data.Tower_ID},");
        }
        sb.AppendLine(@"}");

        var path = EditorUtility.SaveFilePanel("Save", "Assets/Scripts/DataTable/Data", "eTower.cs", "cs");
        using (var fs = new FileStream(path, FileMode.Create))
        {
            using (var writer = new StreamWriter(fs))
            {
                writer.Write(sb.ToString());
            }
        }
    }

    private void UpdateTowerPrefabs(GameObject defaultPrefab, List<TowerRawData> list)
    {
        string prefabPath = "Assets/Resources/Prefabs/Tower/{0}.prefab";
        foreach (var data in list)
        {
            //이미 있으면 삭제
            AssetDatabase.DeleteAsset(string.Format(prefabPath, data.String_Key));
            GameObject newPrefab = Instantiate(defaultPrefab);
            var tower = newPrefab.GetComponent<Tower>();
            if (!data.Tower_Resource.IsNullOrEmpty())
            {
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(data.Tower_Resource);
                tower.spriteRenderer.sprite = sprite;
            }
            //newPrefab.GetComponent<Tower>().InitializeData((eTower)data.Tower_ID, DataTableManager.TowerTable.Get((eTower)data.Tower_ID));
            PrefabUtility.SaveAsPrefabAsset(newPrefab, string.Format(prefabPath, data.String_Key));
            GameObject.DestroyImmediate(newPrefab);
        }
    }
}
