using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TowerAttack : MonoBehaviour
{
    [SerializeField]
    private Tower tower;

    [ReadOnly, SerializeField]
    private Enemy target;

    public bool IsValidTarget
    {
        get
        {
            return target != null && !target.IsDead && Vector3.Distance(target.transform.position, tower.TowerGroup.transform.position) <= tower.AttackRange;
        }
    }

    private Coroutine coAttack;

    public void OnEnable()
    {
        coAttack = StartCoroutine(CoAttack());
    }

    public void AttackTarget()
    {
        tower.animator.SetTrigger("2_Attack");

        tower.SetDirection(target.transform.position);
        target.OnDamaged(tower.AttackPower);
    }

    private IEnumerator CoAttack()
    {
        while (true)
        {
            if (!IsValidTarget)
            {
                tower.SetDefaultDirection();
                FindTarget();
            }

            if (IsValidTarget)
            {
                AttackTarget();
            }

            yield return new WaitForSeconds(tower.AttackInterval);
        }
    }

    private bool FindTarget()
    {
        var closestEnemy = tower.TowerGroup.enemyManager.ValidEnemies.
            OrderBy(e => Vector3.Distance(e.transform.position, tower.TowerGroup.transform.position)).
            FirstOrDefault();

        if (closestEnemy != null && Vector3.Distance(closestEnemy.transform.position, tower.TowerGroup.transform.position) <= tower.AttackRange)
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
