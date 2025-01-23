using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class DataTableManager
{
    private static readonly Dictionary<string, DataTable> tables = new Dictionary<string, DataTable>();

    static DataTableManager()
    {
        var towerTable = new TowerTable();
        towerTable.Load(DataTableIds.Tower);
        tables.Add(DataTableIds.Tower, towerTable);
    }

    public static StringTable StringTable
    {
        get
        {
            return Get<StringTable>(DataTableIds.String[(int)Variables.currentLang]);
        }
    }

    public static TowerTable TowerTable
    {
        get
        {
            return Get<TowerTable>(DataTableIds.Tower);
        }
    }


    public static T Get<T>(string id) where T : DataTable
    {
        if (!tables.ContainsKey(id))
        {
            Debug.LogError("테이블 없음");
            return null;
        }
        return tables[id] as T;
    }
}
