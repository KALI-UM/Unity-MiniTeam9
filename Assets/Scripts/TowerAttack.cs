using System;
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

    private Action AttackTarget;

    public void Awake()
    {
        if (tower.Data.attackType == 0)
        {
            AttackTarget = AttackMelee;
        }
        else
        {
            AttackTarget = AttackProjectile;
        }
    }

    public void OnEnable()
    {
        StartCoroutine(CoAttack());
    }

    public void AttackMelee()
    {
        tower.animationHandler.Attack(tower.AttackSpeed);
        tower.SetDirection(target.transform.position);
        StartCoroutine(CoAttackMeleeDamageApply(0.5f));
    }

    public void AttackProjectile()
    {
        tower.animationHandler.Attack(tower.AttackSpeed);
        tower.SetDirection(target.transform.position);
        StartCoroutine(CoAttackProjectileDamageApply(1f));
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

    private IEnumerator CoAttackProjectileDamageApply(float ratio)
    {
        yield return new WaitForSeconds(tower.animationHandler.GetCurrentAnimationClipLength() * ratio);
        if (!IsValidTarget)
        {
            FindTarget();
        }

        if (IsValidTarget)
        {
            target.OnDamaged(tower.AttackPower);
            var fire = tower.towerManager.EffectManager.Get<Projectile>(eEffects.Fire);
            fire.SetDestination(target.transform.position);
            fire.Play(tower.transform.position + new Vector3(0.25f, 0.25f, 0));
        }
    }
    private IEnumerator CoAttackMeleeDamageApply(float ratio)
    {
        yield return new WaitForSeconds(tower.animationHandler.GetCurrentAnimationClipLength() * ratio);
        if (!IsValidTarget)
        {
            FindTarget();
        }

        if (IsValidTarget)
        {
            target.OnDamaged(tower.AttackPower);
        }
        //var fire = tower.towerManager.EffectManager.Get<Projectile>(eEffects.Fire);
        //fire.SetDestination(target.transform.position);
        //fire.Play(transform.position);
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
