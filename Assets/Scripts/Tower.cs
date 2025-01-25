using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TowerTable;


public class Tower : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [ReadOnly, SerializeField]
    private Enemy target;

    public TowerGroup TowerGroup
    {
        get;
        private set;
    }

    public bool IsValidTarget
    {
        get
        {
            return target != null && !target.IsDead && Vector3.Distance(target.transform.position, TowerGroup.transform.position) <= AttackRange;
        }
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
        get => Data.attackRange * TowerManager.RangeFactor;
    }

    public int AttackPower
    {
        get => Data.attackPower;
    }

    public float AttackSpeed
    {
        get => Data.attackSpeed * TowerManager.SpeedFactor;
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

    public void OnSpawn(TowerGroup group)
    {
        TowerGroup = group;
        transform.SetParent(TowerGroup.transform);
        coAttack = StartCoroutine(CoAttack());
    }

    public void AttackTarget()
    {
        target.OnDamaged(AttackPower);
    }

    private IEnumerator CoAttack()
    {
        while (true)
        {
            if (!IsValidTarget)
            {
                FindTarget();
            }

            if (IsValidTarget)
            {
                AttackTarget();
            }

            yield return new WaitForSeconds(AttackInterval);
        }
    }

    private bool FindTarget()
    {
        var closestEnemy = TowerGroup.enemyManager.ValidEnemies.OrderBy(e => Vector3.Distance(e.transform.position, TowerGroup.transform.position)).FirstOrDefault();

        if (closestEnemy != null && Vector3.Distance(closestEnemy.transform.position, TowerGroup.transform.position) <= AttackRange)
        {
            target = closestEnemy;
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        if (!IsValidTarget)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, target.transform.position);
    }
}