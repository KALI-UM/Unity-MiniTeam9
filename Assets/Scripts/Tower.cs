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


    public TowerData Data
    {
        get;
        private set;
    }

    public bool IsValidTarget
    {
        get
        {
            return target != null && !target.IsDead && Vector3.Distance(target.transform.position, transform.position) <= Data.AttackRange;
        }
    }

    public float AttackRange
    {
        get
        {
            return Data.AttackRange*TowerManager.RangeFactor;
        }
    }

    public void InitializeData(eTower id, TowerData data)
    {
        TowerId = id;
        Data = data;
    }

    public void AttackTarget()
    {
        target.OnDamaged(Data.AttackPower);
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