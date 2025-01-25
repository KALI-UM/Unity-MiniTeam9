using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TowerTable;

public class EnemyTable : DataTable
{
    public class EnemyRawData
    {
        public int Monster_ID { get; set; }
        public string String_Key { get; set; }
        public int Monster_Grade { get; set; }
        public int Monster_HP { get; set; }
        public float Monster_MoveSpeed { get; set; }
        public int DropGold { get; set; }
        public int DropGem { get; set; }
        public string Monster_Resource { get; set; }

    }

    private readonly Dictionary<int, EnemyRawData> dictionary = new();

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<EnemyRawData>(textAsset.text);
        dictionary.Clear();

        foreach (var raw in list)
        {
            if (!dictionary.ContainsKey(raw.Monster_ID))
            {
                dictionary.Add(raw.Monster_ID, raw);
            }
            else
            {
                Debug.LogError($"Å° Áßº¹: {raw.Monster_ID}");
            }
        }
    }
}
