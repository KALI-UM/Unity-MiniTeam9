using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;
using static EnemyTable;

[CustomEditor(typeof(EnemyScriptableData))]

public class EnemyScriptableDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var dataEditor = (EnemyScriptableData)target;
        if (dataEditor.currentEnemyData != null)
        {
            // `EnemyData`의 필드를 수정할 수 있도록 인스펙터를 렌더링
            Editor editor = Editor.CreateEditor(dataEditor.currentEnemyData);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Reset to CSV data");
            //데이터 테이블 기존 값으로 
            if (GUILayout.Button("Revert"))
            {
                dataEditor.currentEnemyData.SetData(DataTableManager.EnemyTable.Get(dataEditor.currentEnemyData.Id));
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
                DataTableManager.EnemyTable.Load(DataTableIds.Enemy);
            }
            GUILayout.EndHorizontal();

            editor.OnInspectorGUI();
        }
    }

    public void SaveCsv()
    {
        List<EnemyRawData> enemyDatas = new List<EnemyRawData>();
        string dataPath = "Assets/Resources/Datas/Enemy/{0}.asset";

        foreach (string key in Enum.GetNames(typeof(eEnemy)))
        {
            var data = AssetDatabase.LoadAssetAtPath<EnemyData>(string.Format(dataPath, key));
            enemyDatas.Add(data.ConvertToRawData());
        }

        var path = string.Format("Assets/Resources/" + DataTable.FormatPath, DataTableIds.Enemy);
        var tempPath = string.Format("Assets/Resources/" + DataTable.FormatTempPath, DataTableIds.Enemy + "_" + ++DataTableManager.EnemyTable.currentVersion);
        DataTable.SaveCSV<EnemyRawData>(enemyDatas, path);
        DataTable.SaveCSV<EnemyRawData>(enemyDatas, tempPath);

        AssetDatabase.Refresh();
        DataTableManager.EnemyTable.Load(DataTableIds.Enemy);
    }
}
