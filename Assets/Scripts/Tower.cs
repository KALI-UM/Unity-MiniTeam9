using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TowerTable;


public class Tower : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private Enemy target;

    public int TowerId
    {
        get;
        private set;
    }

    private TowerRawData data;
    public TowerRawData Data
    {
        get=>data;
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
            return Data.Tower_AttackRange*TowerManager.RangeFactor;
        }
    }
    public int AttackPower
    {
        get
        {
            return Data.Tower_AttackPower ;
        }
    }
    public int AttackSpeed
    {
        get
        {
            return Data.Tower_AttackSpeed;
        }
    }

    public void InitializeData(int id, TowerRawData data)
    {
        TowerId = id;
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