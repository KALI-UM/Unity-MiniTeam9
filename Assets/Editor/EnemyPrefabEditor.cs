using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static EnemyTable;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(EnemyEditor))]

public class EnemyPrefabEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Label("If Enemy Added...");
        if (GUILayout.Button("Update Enemy Enum"))
        {
            var path = string.Format(DataTable.FormatPath, DataTableIds.Enemy);
            var textAsset = Resources.Load<UnityEngine.TextAsset>(path);
            var list = DataTable.LoadCSV<EnemyRawData>(textAsset.text);
            UpdateEnemyEnum(list);
        }

        GUILayout.Label("If Enemy Data Changed...");
        if (GUILayout.Button("Update Enemy Prefabs"))
        {
            var path = string.Format(DataTable.FormatPath, DataTableIds.Enemy);
            var textAsset = Resources.Load<UnityEngine.TextAsset>(path);
            var list = DataTable.LoadCSV<EnemyRawData>(textAsset.text);

            var enemyEditor = (EnemyEditor)target;
            UpdateEnemyPrefabs(enemyEditor.defaultEnemyPrefab, enemyEditor.bossEnemyPrefab, list);
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

    private void UpdateEnemyPrefabs(GameObject defaultPrefab, GameObject bossPrefab, List<EnemyRawData> list)
    {
        string prefabPath = "Assets/Resources/Prefabs/Enemy/{0}.prefab";
        string dataPath = "Assets/Resources/Datas/Enemy/{0}.asset";
        string spumpath = "Assets/Resources/Prefabs/Units/{0}.prefab";


        UpdateEnemyDatas(list);
        
        foreach (var data in list)
        {
            //이미 있으면 삭제
            AssetDatabase.DeleteAsset(string.Format(prefabPath, data.String_Key));
            GameObject newPrefab = data.Monster_Grade == 2 ? Instantiate(bossPrefab) : Instantiate(defaultPrefab);
            var enemy = newPrefab.GetComponent<Enemy>();

            //해당 데이터 ScriptableObject
            EnemyData scriptableData = AssetDatabase.LoadAssetAtPath<EnemyData>(string.Format(dataPath, data.String_Key));
            enemy.InitializeData(scriptableData);

            if (!string.IsNullOrEmpty(data.Monster_Resource))
            {
                KALLogger.Log(string.Format(spumpath, data.Monster_Resource));
                GameObject spumprefab = AssetDatabase.LoadAssetAtPath<GameObject>(string.Format(spumpath, data.Monster_Resource));
                KALLogger.Log(spumprefab);


                Vector3 scale = enemy.character.transform.GetChild(0).transform.localScale;
                GameObject.DestroyImmediate(enemy.character.transform.GetChild(0).gameObject);

                GameObject spum = Instantiate(spumprefab);
                spum.transform.localScale = scale;
                spum.transform.position = Vector3.zero;
                spum.transform.SetParent(enemy.character.transform);

                //spum.GetComponent<SPUM_Prefabs>().OverrideControllerInit();
                var handler = spum.AddComponent<SpumAnimationHandler>();
                enemy.animationHandler = handler;
            }

            PrefabUtility.SaveAsPrefabAsset(newPrefab, string.Format(prefabPath, data.String_Key));
            GameObject.DestroyImmediate(newPrefab);
        }
    }
}
