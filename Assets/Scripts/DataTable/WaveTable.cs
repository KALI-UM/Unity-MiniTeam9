using System;
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
        public string Wave_Text_Key { get; set; }
    }

    private readonly List<WaveRawData> list = new List<WaveRawData>();


    [Serializable]
    public class WaveData
    {
        public int waveNumber;
        public eEnemy enemyId;
        public int enemyCount;
        public float spawnInterval;
        public float waveDuration;

        public string waveTextFormat;
    }

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<WaveRawData>(textAsset.text);

        foreach (var raw in list)
        {
            this.list.Add(raw);
        }
    }

    public List<WaveData> GetWaveDatas()
    {
        List<WaveData> datas = new List<WaveData>();
        datas.Add(new WaveData());
        foreach (var raw in list)
        {
            datas.Add(ConvertToWaveData(raw));
        }
        return datas;
    }

    private WaveData ConvertToWaveData(WaveRawData raw)
    {
        WaveData data = new WaveData();
        data.waveNumber = raw.Wave_Number;
        data.enemyId = (eEnemy)raw.Monster_ID;
        data.enemyCount = raw.Monster_Count;
        data.spawnInterval = raw.Spawn_Interval;
        data.waveDuration = raw.Spawn_Duration;
        data.waveTextFormat = raw.Wave_Text_Key;
        
        return data;
    }
}
