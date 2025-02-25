using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static TowerTable;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(TowerEditor))]
public class TowerPrefabEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.Label("If Tower Added...");
        if (GUILayout.Button("Update Tower Enum"))
        {
            var path = string.Format(DataTable.FormatPath, DataTableIds.Tower);
            var textAsset = Resources.Load<UnityEngine.TextAsset>(path);
            var list = DataTable.LoadCSV<TowerRawData>(textAsset.text);
            UpdateTowerEnum(list);
        }

        GUILayout.Label("If Tower Data Changed...");
        if (GUILayout.Button("Update Tower Prefabs"))
        {
            var path = string.Format(DataTable.FormatPath, DataTableIds.Tower);
            var textAsset = Resources.Load<UnityEngine.TextAsset>(path);
            var list = DataTable.LoadCSV<TowerRawData>(textAsset.text);

            var towerEditor = (TowerEditor)target;
            UpdateTowerPrefabs(towerEditor.defaultTowerPrefab, list, towerEditor.shadowColors, towerEditor.spumToTexture);

            AssetDatabase.Refresh();
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

        var path = EditorUtility.SaveFilePanel("Save", "Assets/Scripts/Enums", "eTower.cs", "cs");
        using (var fs = new FileStream(path, FileMode.Create))
        {
            using (var writer = new StreamWriter(fs))
            {
                writer.Write(sb.ToString());
            }
        }
    }

    private void UpdateTowerDatas(List<TowerRawData> list)
    {
        string dataPath = "Assets/Resources/Datas/Tower/{0}.asset";
        foreach (var data in list)
        {
            //이미 있으면 삭제
            AssetDatabase.DeleteAsset(string.Format(dataPath, data.String_Key));

            //해당 데이터 ScriptableObject생성
            TowerData scriptableData = ScriptableObject.CreateInstance<TowerData>();
            scriptableData.SetData(data);

            AssetDatabase.CreateAsset(scriptableData, string.Format(dataPath, data.String_Key));
            AssetDatabase.SaveAssets();
        }
    }


    private void UpdateTowerPrefabs(GameObject defaultPrefab, List<TowerRawData> list, List<Color> colors, SpumToTexture spumToTexture)
    {
        string prefabPath = "Assets/Resources/Prefabs/Tower/{0}.prefab";
        string dataPath = "Assets/Resources/Datas/Tower/{0}.asset";
        string spumPath = "Assets/Resources/Prefabs/Units/{0}.prefab";

        int y = 0;
        UpdateTowerDatas(list);

        foreach (var data in list)
        {
            //이미 있으면 삭제
            AssetDatabase.DeleteAsset(string.Format(prefabPath, data.String_Key));
            GameObject newPrefab = Instantiate(defaultPrefab);
            var tower = newPrefab.GetComponent<Tower>();

            //해당 데이터 ScriptableObject
            TowerData scriptableData = AssetDatabase.LoadAssetAtPath<TowerData>(string.Format(dataPath, data.String_Key));
            tower.InitializeData(scriptableData);


            if (!string.IsNullOrEmpty(data.Tower_Resource))
            {
                KALLogger.Log(string.Format(spumPath, data.Tower_Resource));
                //GameObject spumprefab = AssetDatabase.LoadAssetAtPath<GameObject>(string.Format(spumPath, data.Tower_Resource));
                GameObject spumprefab = tower.Data.towerSpum;

                GameObject.DestroyImmediate(tower.character.transform.GetChild(0).gameObject);

                GameObject spum = Instantiate(spumprefab, tower.character.transform);

                var handler = spum.AddComponent<SpumAnimationHandler>();
                tower.animationHandler = handler;
                tower.shadowRenderer = spum.transform.GetChild(0).Find("Shadow").transform.Find("Shadow").GetComponent<SpriteRenderer>();
            }

            tower.shadowRenderer.color = colors[tower.Data.grade];

            newPrefab.name = data.Tower_Resource;
            spumToTexture.PushToSpumToTextureQueue(newPrefab);
            PrefabUtility.SaveAsPrefabAsset(newPrefab, string.Format(prefabPath, data.String_Key));
            var lineup = new Vector3(0, ++y * 10, 0);
            newPrefab.transform.position = lineup;

            //GameObject.DestroyImmediate(newPrefab);
        }
    }
}
