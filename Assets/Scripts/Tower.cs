using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TowerTable;


public class Tower : MonoBehaviour
{
    public TowerAttack towerAttack;

    public SpumAnimationHandler animationHandler;
    public GameObject character;

    public SpriteRenderer shadowRenderer;
   

    [ReadOnly]
    public TowerManager towerManager;

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
        get => Data.attackRange * towerManager.factorData.attackRange;
    }

    public int AttackIndexRange
    {
        get => Mathf.CeilToInt(AttackRange/CellIndexer.CellSize);
    }

    public int AttackPower
    {
        get => Mathf.CeilToInt((Data.attackPower + (Data.attackPower * towerManager.AttackPowerUpgradeRate))*towerManager.factorData.attackPower);
    }

    public float AttackSpeed
    {
        get => (Data.attackSpeed + Data.attackSpeed * towerManager.AttackSpeedUpgradeRate) * towerManager.factorData.attackSpeed;
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
        SetDefaultDirection();
        if (transform.position.x < targetPos.x)
        {
            var flip = character.transform.localScale;
            flip.x *= -1;
            character.transform.localScale = flip;
        }
    }

    public void SetDefaultDirection()
    {
        character.transform.localScale = Vector3.one;
    }
}