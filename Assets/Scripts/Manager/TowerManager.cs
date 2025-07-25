using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerUpgradeTable;

public class TowerManager : InGameManager
{
    private readonly Dictionary<eTower, GameObject> towerPrefabs = new();
    private readonly Dictionary<eTower, int> towerCounts = new();

    public GlobalFactorData factorData;

    public MaxFusionSystem MaxFusionSystem
    {
        get;
        private set;
    }

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

    public bool IsMaxTowrCount
    {
        get => TowerCount >= MaxTowerCount;
    }

    public int TowerCount
    {
        get;
        private set;
    }

    private void Awake()
    {
        InitializeTowerPrefabs();
        InitializeTowerDatas();

        TowerCountChange(0);
    }

    public override void Initialize()
    {
        base.Initialize();
        MaxFusionSystem = new MaxFusionSystem(this);
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

    private void InitializeTowerDatas()
    {
        foreach (eTower id in Enum.GetValues(typeof(eTower)))
        {
            towerCounts.Add(id, 0);
        }

        MaxGrade = DataTableManager.TowerTable.MaxGrade;
    }

    public GameObject GetRandomTower(int grade)
    {
        var list = DataTableManager.TowerTable.GetTowerGradeList(grade);
        int index = UnityEngine.Random.Range(0, list.Count);

        return GetTower(list[index]);
    }

    public GameObject GetTower(eTower id)
    {
        var go = Instantiate(towerPrefabs[id]);
        go.GetComponent<Tower>().Initialize(this);
        towerCounts[id]++;

        MaxFusionSystem.UpdateRecipeProgress();
        return go;
    }

    public void DestoryTower(Tower tower)
    {
        towerCounts[tower.TowerId]--;
        GameObject.Destroy(tower.gameObject);

        MaxFusionSystem.UpdateRecipeProgress();
        TowerCountChange(-1);
    }

    public int GetTowerCount(eTower id)
    {
        return towerCounts[id];
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
