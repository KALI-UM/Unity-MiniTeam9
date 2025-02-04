using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerTable;
using static WaveTable;

public class TowerUpgradeTable : DataTable
{
    public class TowerUpgradeRawData
    {
        public int Level { get; set; }
        public int GoldCost { get; set; }
        public int GemCost { get; set; }
        public float PowerBonus { get; set; }
        public float SpeedBonus { get; set; }
    }

    private readonly List<TowerUpgradeRawData> list = new();

    public int MaxUpgradeLv
    {
        get;
        private set;
    }

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<TowerUpgradeRawData>(textAsset.text);

        MaxUpgradeLv = list.Count;
        foreach (var raw in list)
        {
            this.list.Add(raw);
        }
    }

    public List<TowerUpgradeRawData> GetTowerUpgradeDatas()
    {
        List<TowerUpgradeRawData> datas = new List<TowerUpgradeRawData>();
        datas.Add(new TowerUpgradeRawData());
        foreach (var raw in list)
        {
            datas.Add(raw);
        }
        return datas;
    }
}
