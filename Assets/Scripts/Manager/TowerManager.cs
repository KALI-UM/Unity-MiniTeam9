using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject defaultTowerPrefab;
    private readonly Dictionary<eTower, GameObject> towerPrefabs = new();

    [Range(0.1f, 5f)]
    public float rangeFactor = 1f;

    public static float RangeFactor
    {
        get;
    }

    private void Awake()
    {
        InitializeTowerPrefabs();
    }

    private void InitializeTowerPrefabs()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/Tower");
        foreach (var tower in prefabs)
        {
            eTower id = Enum.Parse<eTower>(tower.name);
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

    public GameObject GetTower(eTower id)
    {
        var go = Instantiate(towerPrefabs[id]);
        var origin = towerPrefabs[id].GetComponent<Tower>();
        go.GetComponent<Tower>().InitializeData(origin.TowerId, origin.Data);
        return go;
    }
}
