using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TowerTable;


public class Tower : MonoBehaviour
{
    public TowerAttack towerAttack;

    public Animator animator;
    public SpriteRenderer shadowRenderer;

    private TowerManager towerManager;

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
        get => Data.attackPower + (int)(Data.attackPower * towerManager.AttackPowerUpgradeRate);
    }

    public float AttackSpeed
    {
        get => (Data.attackSpeed + Data.attackSpeed * towerManager.AttackSpeedUpgradeRate) * TowerManager.GlobalAttackSpeedFactor;
    }

    public float AttackInterval
    {
        get => 1f / AttackSpeed;
    }

    #endregion


    private void Awake()
    {
        TowerId = Data.Id;
    }

    public void Initialize(TowerManager towerManager)
    {
        this.towerManager = towerManager;
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

    public void SetDirection(Vector3 targetPos)
    {
        if (transform.position.x < targetPos.x)
        {
            var one = Vector3.one;
            one.x *= -1;
            transform.localScale = one;
        }
        else
        {
            SetDefaultDirection();
        }
    }

    public void SetDefaultDirection()
    {
        transform.localScale = Vector3.one;
    }
}