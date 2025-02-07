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
using static EnemyTable;
using static UnityEngine.EventSystems.EventTrigger;
using System.Drawing.Drawing2D;
using Unity.VisualScripting;

//[ExecuteInEditMode]
[CustomEditor(typeof(TowerManager))]
public class TowerManagerEditor : Editor
{
    //public static float GlobalRangeFactor = 1f;
    //public static float GlobalAttackSpeedFactor = 1f;
    //public static float GlobalTowerMoveSpeed = 10f;
    public override void OnInspectorGUI()
    {
   

        base.OnInspectorGUI();
    }


    private void UpdateTowerEnum(List<TowerRawData> list)
    {
        var sb = new StringBuilder();
        sb.AppendLine(@"public enum eTower");
        sb.AppendLine(@"{");

        foreach (var data in list)
        {
            sb.AppendLine($"\t{data.Strnig_Key} = {data.Tower_ID},");
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
            AssetDatabase.DeleteAsset(string.Format(dataPath, data.Strnig_Key));

            //해당 데이터 ScriptableObject생성
            TowerData scriptableData = ScriptableObject.CreateInstance<TowerData>();
            scriptableData.SetData(data);

            AssetDatabase.CreateAsset(scriptableData, string.Format(dataPath, data.Strnig_Key));
            AssetDatabase.SaveAssets();
        }
    }


    private void UpdateTowerPrefabs(GameObject defaultPrefab, List<TowerRawData> list)
    {
        string prefabPath = "Assets/Resources/Prefabs/Tower/{0}.prefab";
        string dataPath = "Assets/Resources/Datas/Tower/{0}.asset";
        string spumpath = "Assets/Resources/Prefabs/Units/{0}.prefab";

        UpdateTowerDatas(list);

        //임시 컬러
        var colorList = new List<Color>();
        colorList.Add(Color.gray);
        colorList.Add(Color.black);
        colorList.Add(Color.green);
        colorList.Add(Color.cyan);
        colorList.Add(Color.red);
        colorList.Add(Color.yellow);

        for (int i = 0; i < colorList.Count; i++)
        {
            var color = colorList[i];
            color.a = 0.5f;
            colorList[i] = color;
        }

        foreach (var data in list)
        {
            //이미 있으면 삭제
            AssetDatabase.DeleteAsset(string.Format(prefabPath, data.Strnig_Key));
            GameObject newPrefab = Instantiate(defaultPrefab);
            var tower = newPrefab.GetComponent<Tower>();

            //해당 데이터 ScriptableObject
            TowerData scriptableData = AssetDatabase.LoadAssetAtPath<TowerData>(string.Format(dataPath, data.Strnig_Key));
            tower.InitializeData(scriptableData);


            if (!data.Tower_Resource.IsNullOrEmpty())
            {
                KALLogger.Log(string.Format(spumpath, data.Tower_Resource));
                GameObject spumprefab = AssetDatabase.LoadAssetAtPath<GameObject>(string.Format(spumpath, data.Tower_Resource));
                KALLogger.Log(spumprefab);

                GameObject.DestroyImmediate(tower.character.transform.GetChild(0).gameObject);

                GameObject spum = Instantiate(spumprefab);
                spum.transform.position = Vector3.zero;
                spum.transform.SetParent(tower.character.transform);

                spum.transform.GetChild(0).AddComponent<SpumAnimationHandler>();
                tower.animationHandler = spum.transform.GetChild(0).GetComponent<SpumAnimationHandler>();

                tower.shadowRenderer = spum.transform.GetChild(0).Find("Shadow").transform.Find("Shadow").GetComponent<SpriteRenderer>();
            }

            tower.shadowRenderer.color = colorList[tower.Data.grade];

            PrefabUtility.SaveAsPrefabAsset(newPrefab, string.Format(prefabPath, data.Strnig_Key));
            GameObject.DestroyImmediate(newPrefab);
        }
    }
}
