using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static EnemyTable;
using static TowerTable;
[CustomEditor(typeof(TowerScriptableData))]
public class TowerScriptableDataEditor : Editor
{
  
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var dataEditor = (TowerScriptableData)target;
        if (dataEditor.currentTowerData != null)
        {
            Editor editor = Editor.CreateEditor(dataEditor.currentTowerData);

            if (Application.isPlaying)
            {
                var gameManager = dataEditor.gameManager;

                GUILayout.BeginHorizontal();
                GUILayout.Label("Spawn "+ dataEditor.currentTowerData.name);
                if (GUILayout.Button("Spawn")&& gameManager.SlotManager.IsPossibleToSpawnTower())
                {
                    var tower = gameManager.TowerManager.GetTower(dataEditor.currentTowerData.Id);
                    gameManager.SlotManager.AddTower(tower.GetComponent<Tower>());
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Reset to CSV data");
            //데이터 테이블 기존 값으로 
            if (GUILayout.Button("Revert"))
            {
                dataEditor.currentTowerData.SetData(DataTableManager.TowerTable.Get(dataEditor.currentTowerData.Id));
            }
            GUILayout.EndHorizontal();

            if (!Application.isPlaying)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Import to CSV data");
                if (GUILayout.Button("Save"))
                {
                    SaveCsv();
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.BeginHorizontal();
            GUILayout.Label("Reload Csv data"); 
            if (GUILayout.Button("Reload"))
            {
                DataTableManager.TowerTable.Load(DataTableIds.Tower);
            }
            GUILayout.EndHorizontal();

            editor.OnInspectorGUI();
        }
    }

    public void SaveCsv()
    {
        List<TowerRawData> towerDatas = new List<TowerRawData>();
        string dataPath = "Assets/Resources/Datas/Tower/{0}.asset";

        foreach (string key in Enum.GetNames(typeof(eTower)))
        {
            var data = AssetDatabase.LoadAssetAtPath<TowerData>(string.Format(dataPath, key));
            towerDatas.Add(data.ConvertToRawData());
        }

        var path = string.Format("Assets/Resources/" + DataTable.FormatPath, DataTableIds.Tower);
        var tempPath = string.Format("Assets/Resources/" + DataTable.FormatTempPath, DataTableIds.Tower + "_" + ++DataTableManager.TowerTable.currentVersion);
        DataTable.SaveCSV<TowerRawData>(towerDatas, path);
        DataTable.SaveCSV<TowerRawData>(towerDatas, tempPath);

        AssetDatabase.Refresh();
        DataTableManager.TowerTable.Load(DataTableIds.Tower);
    }
}
