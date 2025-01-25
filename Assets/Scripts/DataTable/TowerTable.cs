using CsvHelper.Configuration.Attributes;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using static StringTable;
using static UnityEngine.GraphicsBuffer;


public class TowerTable : DataTable
{
    public class TowerRawData
    {
        public int Tower_ID { get; set; }
        public string String_Key { get; set; }
        public int Tower_Grade { get; set; }
        public int Tower_AttackType { get; set; }
        public int Tower_AttackPower { get; set; }
        public int Tower_AttackSpeed { get; set; }
        public float Tower_AttackRange { get; set; }
        public int Tower_SaleGold { get; set; }
        public int Tower_SaleGem { get; set; }
        public string Tower_Resource { get; set; }
    }


    //public class TowerData
    //{
    //    public int Id { get; set; }
    //    public string Key { get; set; }
    //    public int Grade { get; set; }
    //    public int AttackType { get; set; }
    //    public int AttackPower { get; set; }
    //    public int AttackSpeed { get; set; }
    //    public float AttackRange { get; set; }

    //    public TowerRawData RawData { get; set; }
    //}

    //private readonly Dictionary<eTower, TowerData> dictionary = new();
    //private readonly List<List<eTower>> towerGrade = new();

    private readonly Dictionary<eTower, TowerRawData> dictionary = new();
    private readonly List<List<eTower>> towerGrade = new();

    public int MaxGrade
    {
        get;
        private set;
    }

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<TowerRawData>(textAsset.text);
        dictionary.Clear();

        MaxGrade = list.Max(data => data.Tower_Grade);
        for (int i = 0; i <= MaxGrade; i++)
        {
            towerGrade.Add(new List<eTower>());
        }

        foreach (var raw in list)
        {
            if (!dictionary.ContainsKey((eTower)raw.Tower_ID))
            {
                dictionary.Add((eTower)raw.Tower_ID, raw);
                towerGrade[raw.Tower_Grade].Add((eTower)raw.Tower_ID);
            }
            else
            {
                Debug.LogError($"키 중복: {raw.Tower_ID}");
            }
        }

        //var path = string.Format(FormatPath, filename);
        //var textAsset = Resources.Load<TextAsset>(path);
        //var list = LoadCSV<TowerRawData>(textAsset.text);
        //dictionary.Clear();

        //MaxGrade = list.Max(data=>data.Tower_Grade);
        //for(int i=0; i<=MaxGrade; i++)
        //{
        //    towerGrade.Add(new List<eTower>());
        //}

        //foreach (var raw in list)
        //{
        //    TowerData data = ConvertToTowerData(raw);

        //    eTower key = (eTower)data.Id;
        //    if (!dictionary.ContainsKey(key))
        //    {
        //        dictionary.Add(key, data);
        //        towerGrade[data.Grade].Add(key);
        //    }
        //    else
        //    {
        //        Debug.LogError($"키 중복: {key}");
        //    }
        //}
    }
    public TowerRawData Get(eTower key)
    {
        if (!dictionary.ContainsKey(key))
        {
            KALLogger.Log("None Key", this);
            return null;
        }
        return dictionary[key];
    }

    public TowerRawData Get(int key)
    {
        return Get((eTower)key);
    }


    public List<eTower> GetTowerList(int grade)
    {
        return towerGrade[grade];
    }

    //public TowerData Get(eTower key)
    //{
    //    if (!dictionary.ContainsKey(key))
    //    {
    //        KALLogger.Log("None Key", this);
    //        return null;
    //    }
    //    return dictionary[key];
    //}

    //public List<eTower> GetTowerList(int grade)
    //{
    //    return towerGrade[grade];
    //}

    //private TowerData ConvertToTowerData(TowerRawData data)
    //{
    //    TowerData newData = new();
    //    newData.Id = data.Tower_ID;
    //    newData.Key = data.String_Key;
    //    newData.Grade = data.Tower_Grade;
    //    newData.AttackType = data.Tower_AttackType;
    //    newData.AttackPower = data.Tower_AttackPower;
    //    newData.AttackSpeed = data.Tower_AttackSpeed;
    //    newData.AttackRange = data.Tower_AttackRange;
    //    newData.RawData = data;
    //    return newData;
    //}
}
