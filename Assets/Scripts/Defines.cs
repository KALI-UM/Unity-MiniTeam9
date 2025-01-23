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
        "StringTableKr",
        "StringTableEn",
    };

    public static readonly string Tower = "TowerTable";
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
