using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TowerTable;


public class Tower : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private Enemy target;

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

    public bool IsValidTarget
    {
        get
        {
            return target != null && !target.IsDead && Vector3.Distance(target.transform.position, transform.position) <= AttackRange;
        }
    }

    public float AttackRange
    {
        get
        {
            return Data.attackRange*TowerManager.RangeFactor;
        }
    }
    public int AttackPower
    {
        get
        {
            return Data.attackPower ;
        }
    }
    public float AttackSpeed
    {
        get
        {
            return Data.attackSpeed;
        }
    }

    private void Awake()
    {
        TowerId = Data.Id;
    }

    public void InitializeData(TowerData data)
    {
        TowerId = data.Id;
        this.data = data;
    }

    public void AttackTarget()  
    {
        target.OnDamaged(AttackPower);
    }

    public void SetTarget(Enemy target)
    {
        this.target = target;
    }

    private void OnDrawGizmos()
    {
        if (!IsValidTarget)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target.transform.position);
    }
}