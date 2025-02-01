using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TowerTable;


public class Tower : MonoBehaviour
{
    public TowerAttack towerAttack;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer shadowRenderer;

    public TowerGroup TowerGroup
    {
        get;
        private set;
    }

    #region TowerData
    public eTower TowerId
    {
        get;
        private set;
    }

    [ReadOnly, SerializeField]
    private TowerData data;

    public TowerData Data
    {
        get => data;
    }

    public float AttackRange
    {
        get => Data.attackRange * TowerManager.GlobalRangeFactor;
    }

    public int AttackPower
    {
        get => Data.attackPower;
    }

    public float AttackSpeed
    {
        get => Data.attackSpeed * TowerManager.GlobalAttackSpeedFactor;
    }

    public float AttackInterval
    {
        get => 1f / AttackSpeed;
    }

    #endregion

    private Coroutine coAttack;

    private void Awake()
    {
        TowerId = Data.Id;
    }

    public void InitializeData(TowerData data)
    {
        TowerId = data.Id;
        this.data = data;
    }

    public void OnAddTowerGroup(TowerGroup group)
    {
        TowerGroup = group;
        transform.SetParent(TowerGroup.transform);
        towerAttack.enabled = true;
    }
   
}