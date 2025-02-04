using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerUpgradeTable;

public class TowerManager : InGameManager
{
    public GameObject defaultTowerPrefab;
    private readonly Dictionary<eTower, GameObject> towerPrefabs = new();

    public static float GlobalRangeFactor = 1f;
    public static float GlobalAttackSpeedFactor = 1f;
    public static float GlobalTowerMoveSpeed = 10f;

    public int MaxGrade
    {
        get;
        private set;
    }

    public float AttackPowerUpgradeRate
    {
        get;
        private set;
    }

    public float AttackSpeedUpgradeRate
    {
        get;
        private set;
    }

    public Action<int> onTowerCountChange;

    [SerializeField]
    private int maxTowerCount = 50;

    public int MaxTowerCount
    {
        get => maxTowerCount;
    }

    public int TowerCount
    {
        get;
        private set;
    }


    private void Awake()
    {
        InitializeTowerPrefabs();

        MaxGrade = DataTableManager.TowerTable.MaxGrade;
        TowerCountChange(0);
    }

    private void InitializeTowerPrefabs()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/Tower");
        foreach (var tower in prefabs)
        {
            var id = Enum.Parse<eTower>(tower.name);
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
        go.GetComponent<Tower>().Initialize(this);
        //var origin = towerPrefabs[id].GetComponent<Tower>();
        //go.GetComponent<Tower>().InitializeData(origin.TowerId, origin.Data);
        return go;
    }

    public void TowerCountChange(int amount)
    {
        TowerCount += amount;
        onTowerCountChange?.Invoke(TowerCount);
    }

    public void SetAttackPowerUpgradeRate(float data)
    {
        AttackPowerUpgradeRate = data;
    }

    public void SetAttackSpeedUpgradeRate(float data)
    {
        AttackSpeedUpgradeRate = data;
    }
}
