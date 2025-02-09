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
        public int Tower_ID { get; set; }
        public int Recipe_1 { get; set; }
        public int RecipeCount_1 { get; set; }
        public int Recipe_2 { get; set; }
        public int RecipeCount_2 { get; set; }
        public int Recipe_3 { get; set; }
        public int RecipeCount_3 { get; set; }
        public int Recipe_4 { get; set; }
        public int RecipeCount_4 { get; set; }

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
            if (!dictionary.ContainsKey((eTower)raw.Tower_ID))
            {
                dictionary.Add((eTower)raw.Tower_ID, ConvertToTowerRecipeData(raw));
            }
            else
            {
                Debug.LogError($"Å° Áßº¹: {raw.Tower_ID}");
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

        data.Id = (eTower)raw.Tower_ID;

        if (raw.RecipeCount_1 > 0)
            data.Recipes.Add(((eTower)raw.Recipe_1, raw.RecipeCount_1));

        if (raw.RecipeCount_2 > 0)
            data.Recipes.Add(((eTower)raw.Recipe_2, raw.RecipeCount_2));

        if (raw.RecipeCount_3 > 0)
            data.Recipes.Add(((eTower)raw.Recipe_3, raw.RecipeCount_3));

        if (raw.RecipeCount_4 > 0)
            data.Recipes.Add(((eTower)raw.Recipe_4, raw.RecipeCount_4));

        data.RecipeSum = raw.RecipeCount_1 + raw.RecipeCount_2 + raw.RecipeCount_3 + raw.RecipeCount_4;

        return data;
    }
}
