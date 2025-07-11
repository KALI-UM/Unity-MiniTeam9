using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Languages
{
    Korean,
    English,
    Japanese,
}

public static class DataTableIds
{
    public static readonly string[] String =
    {
        "StringTableKR",
        "StringTableEN",
    };

    public static readonly string Tower = "TowerTable";
    public static readonly string Enemy= "MonsterTable";
    public static readonly string Wave= "WaveTable";
    public static readonly string TowerUpgrade= "TowerUpgradeTable";
    public static readonly string TowerRecipe= "TowerRecipeTable";
}

public static class Variables
{
    public static Languages currentLang = Languages.Korean;
}

public static class Tags
{
    public static readonly string Player = "Player";
}

public static class SortingLayers
{
    public static readonly string Default = "Default";
}

public static class Layers
{
    public static readonly string Default = "Default";
    public static readonly string UI = "UI";
}
