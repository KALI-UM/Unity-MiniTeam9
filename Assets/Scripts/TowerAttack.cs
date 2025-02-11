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
        tower.animationHandler.Attack();

        tower.SetDirection(target.transform.position);
        var fire = tower.towerManager.EffectManager.Get<Projectile>(eEffects.Fire);
        fire.SetDestination(target.transform.position);
        fire.duration = tower.animationHandler.GetCurrentAnimationClipLength();
        fire.Play(transform.position);
        StartCoroutine(CoAttackDamageApply(0.5f));
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

            yield return new WaitForSeconds(tower.AttackInterval);
        }
    }

    private IEnumerator CoAttackDamageApply(float ratio)
    {
        yield return new WaitForSeconds(tower.animationHandler.GetCurrentAnimationClipLength()*ratio);
        target.OnDamaged(tower.AttackPower);
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
