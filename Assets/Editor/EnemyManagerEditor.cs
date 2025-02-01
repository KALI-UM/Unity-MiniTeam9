using DG.DemiEditor;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static EnemyTable;
using static TowerTable;

[CustomEditor(typeof(EnemyManager))]
public class EnemyManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Update Enemy Enum"))
        {
            var path = string.Format(DataTable.FormatPath, DataTableIds.Enemy);
            var textAsset = Resources.Load<UnityEngine.TextAsset>(path);
            var list = DataTable.LoadCSV<EnemyRawData>(textAsset.text);
            UpdateEnemyEnum(list);
        }

        if (GUILayout.Button("Update Enemy Prefabs"))
        {
            var path = string.Format(DataTable.FormatPath, DataTableIds.Enemy);
            var textAsset = Resources.Load<UnityEngine.TextAsset>(path);
            var list = DataTable.LoadCSV<EnemyRawData>(textAsset.text);

            var enemyManager = (EnemyManager)target;
            UpdateEnemyPrefabs(enemyManager.defaultEnemyPrefab, list);
        }

        base.OnInspectorGUI();
    }

    private void UpdateEnemyEnum(List<EnemyRawData> list)
    {
        var sb = new StringBuilder();
        sb.AppendLine(@"public enum eEnemy");
        sb.AppendLine(@"{");

        foreach (var data in list)
        {
            sb.AppendLine($"\t{data.String_Key} = {data.Monster_ID},");
        }
        sb.AppendLine(@"}");

        var path = EditorUtility.SaveFilePanel("Save", "Assets/Scripts/Enums", "eEnemy.cs", "cs");
        using (var fs = new FileStream(path, FileMode.Create))
        {
            using (var writer = new StreamWriter(fs))
            {
                writer.Write(sb.ToString());
            }
        }
    }

    private void UpdateEnemyDatas(List<EnemyRawData> list)
    {
        string dataPath = "Assets/Resources/Datas/Enemy/{0}.asset";
        foreach (var data in list)
        {
            //이미 있으면 삭제
            AssetDatabase.DeleteAsset(string.Format(dataPath, data.String_Key));

            //해당 데이터 ScriptableObject생성
            EnemyData scriptableData = ScriptableObject.CreateInstance<EnemyData>();
            scriptableData.SetData(data);

            AssetDatabase.CreateAsset(scriptableData, string.Format(dataPath, data.String_Key));
            AssetDatabase.SaveAssets();
        }
    }

    private void UpdateEnemyPrefabs(GameObject defaultPrefab, List<EnemyRawData> list)
    {
        string prefabPath = "Assets/Resources/Prefabs/Enemy/{0}.prefab";
        string dataPath = "Assets/Resources/Datas/Enemy/{0}.asset";

        UpdateEnemyDatas(list);

        foreach (var data in list)
        {
            //이미 있으면 삭제
            AssetDatabase.DeleteAsset(string.Format(prefabPath, data.String_Key));
            GameObject newPrefab = Instantiate(defaultPrefab);
            var enemy = newPrefab.GetComponent<Enemy>();

            //해당 데이터 ScriptableObject
            EnemyData scriptableData = AssetDatabase.LoadAssetAtPath<EnemyData>(string.Format(dataPath, data.String_Key));
            enemy.InitializeData(scriptableData);

            if (!data.Monster_Resource.IsNullOrEmpty())
            {
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(data.Monster_Resource);
                enemy.spriteRenderer.sprite = sprite;
            }


            PrefabUtility.SaveAsPrefabAsset(newPrefab, string.Format(prefabPath, data.String_Key));
            GameObject.DestroyImmediate(newPrefab);
        }
    }
}
