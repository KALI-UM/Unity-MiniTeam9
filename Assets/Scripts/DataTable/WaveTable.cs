using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyTable;

public class WaveTable : DataTable
{
    public class WaveRawData
    {
        public int Wave_ID { get; set; }
        public int Wave_Number { get; set; }
        public int Monster_ID { get; set; }
        public int Monster_Count { get; set; }
        public float Spawn_Interval { get; set; }
        public float Spawn_Duration { get; set; }
        public int Wave_Text_Key { get; set; }
    }

    private readonly List<WaveRawData> list = new List<WaveRawData>();

   public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<EnemyRawData>(textAsset.text);
      
        foreach (var raw in list)
        {
            list.Add(raw);
        }
    }

}
