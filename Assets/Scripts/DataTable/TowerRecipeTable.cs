using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnemyTable;

public class TowerRecipeTable : DataTable
{
    public class TowerRecipeRawData
    {
        public int RecipeID { get; set; }
        public int ResultTowerID { get; set; }
        public int SourceTower1ID { get; set; }
        public int SourceTower1Amount { get; set; }
        public int SourceTower2ID { get; set; }
        public int SourceTower2Amount2 { get; set; }
        public int SourceTower3ID { get; set; }
        public int SourceTower3Amount { get; set; }
        public int SourceTower4ID { get; set; }
        public int SourceTower4Amount { get; set; }

    }

    public class TowerRecipeData
    {
        public eTower Id;
        public List<(eTower Id, int count)> Recipes = new();
        public int RecipeSum;
    }

    private readonly Dictionary<eTower, TowerRecipeData> dictionary = new();
    public List<TowerRecipeData> RecipeDatas
    {
        get => dictionary.Values.ToList<TowerRecipeData>();
    }

    public override void Load(string filename)
    {
        var path = string.Format(FormatPath, filename);
        var textAsset = Resources.Load<TextAsset>(path);
        var list = LoadCSV<TowerRecipeRawData>(textAsset.text);
        dictionary.Clear();

        foreach (var raw in list)
        {
            if (!dictionary.ContainsKey((eTower)raw.ResultTowerID))
            {
                dictionary.Add((eTower)raw.ResultTowerID, ConvertToTowerRecipeData(raw));
            }
            else
            {
                Debug.LogError($"Å° Áßº¹: {raw.ResultTowerID}");
            }
        }
    }

    public TowerRecipeData Get(eTower key)
    {
        if (!dictionary.ContainsKey(key))
        {
            KALLogger.Log("None Key", this);
            return null;
        }
        return dictionary[key];
    }

    private TowerRecipeData ConvertToTowerRecipeData(TowerRecipeRawData raw)
    {
        TowerRecipeData data = new TowerRecipeData();

        data.Id = (eTower)raw.ResultTowerID;

        if (raw.SourceTower1Amount > 0)
            data.Recipes.Add(((eTower)raw.SourceTower1ID, raw.SourceTower1Amount));

        if (raw.SourceTower2Amount2 > 0)
            data.Recipes.Add(((eTower)raw.SourceTower2ID, raw.SourceTower2Amount2));

        if (raw.SourceTower3Amount > 0)
            data.Recipes.Add(((eTower)raw.SourceTower3ID, raw.SourceTower3Amount));

        if (raw.SourceTower4Amount > 0)
            data.Recipes.Add(((eTower)raw.SourceTower4ID, raw.SourceTower4Amount));

        data.RecipeSum = raw.SourceTower1Amount + raw.SourceTower2Amount2 + raw.SourceTower3Amount + raw.SourceTower4Amount;

        return data;
    }
}
