using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : InGameManager
{
    public GameObject defaultTowerPrefab;
    private readonly Dictionary<int, GameObject> towerPrefabs = new();

    public static float RangeFactor = 1f;
    public static float SpeedFactor= 1f;

    private void Awake()
    {
        InitializeTowerPrefabs();
    }

    private void InitializeTowerPrefabs()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/Tower");
        foreach (var tower in prefabs)
        {
            int id = (int)Enum.Parse<eTower>(tower.name);
            tower.GetComponent<Tower>().InitializeData(id, DataTableManager.TowerTable.Get(id));

            towerPrefabs.Add(id, tower);
        }
    }

    public GameObject GetRandomTower(int grade)
    {
        var list = DataTableManager.TowerTable.GetTowerList(grade);
        int index = UnityEngine.Random.Range(0, list.Count);

        return GetTower(list[index]);
    }

    public GameObject GetTower(int id)
    {
        var go = Instantiate(towerPrefabs[id]);
        //var origin = towerPrefabs[id].GetComponent<Tower>();
        //go.GetComponent<Tower>().InitializeData(origin.TowerId, origin.Data);
        return go;
    }
}
