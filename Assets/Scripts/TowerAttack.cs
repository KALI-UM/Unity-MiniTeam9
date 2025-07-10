using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
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

    private void OnDisable()
    {
        uniCancelToken.Cancel();
    }

    private void OnDestroy()
    {
        uniCancelToken.Cancel();
        uniCancelToken.Dispose();
    }


    public async void OnEnable()
    {
        //StartCoroutine(CoAttack());

        await UniAttackAsync();
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
        InitializeClosestEnemyQuery();


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
        if (tower.TowerGroup.enemyManager.ValidEnemies.Count <= 0)
        {
            return false;
        }
        return FindClosestTarget();
    }

    private bool FindClosestTarget()
    {
        //return FindTargetRaw();
        return FindTargetByIndex();
    }

    private bool FindTargetRaw()
    {
        var closestEnemy = tower.TowerGroup.enemyManager.ValidEnemies
.OrderBy(e => Vector3.Distance(e.transform.position, tower.TowerGroup.transform.position)).
            FirstOrDefault();

        if (closestEnemy != null && Vector3.Distance(closestEnemy.transform.position, tower.TowerGroup.transform.position) <= tower.AttackRange)
        {
            target = closestEnemy;
            return true;
        }
        return false;
    }

    private System.Collections.Generic.IEnumerable<Enemy> closestEnemyQuery;
    private void InitializeClosestEnemyQuery()
    {
        closestEnemyQuery = tower.TowerGroup.enemyManager.ValidEnemies.
            Where(e =>
            Mathf.Abs(e.CellIndex.X -
            tower.TowerGroup.CellIndex.X)
            <= tower.AttackIndexRange &&
            Mathf.Abs(e.CellIndex.Y - tower.TowerGroup.CellIndex.Y)
            <= tower.AttackIndexRange);
    }

    private bool FindTargetByIndex()
    {
        var closestEnemyList = closestEnemyQuery;

        if (closestEnemyList.Count() <= 0)
        {
            return false;
        }
        var closestEnemy = closestEnemyList.OrderBy(e => Vector3.Distance(e.transform.position, tower.TowerGroup.transform.position)).
         FirstOrDefault();

        if (closestEnemyList != null && Vector3.Distance(closestEnemy.transform.position, tower.TowerGroup.transform.position) <= tower.AttackRange)
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

   private  CancellationTokenSource uniCancelToken = new CancellationTokenSource(); //비활성화시 취소처리

    private async UniTask UniAttackAsync()
    {
        uniCancelToken = new CancellationTokenSource();
        InitializeClosestEnemyQuery();
        while (true)
        {
            if (uniCancelToken.IsCancellationRequested)
            {
                return;
            }

            if (!IsValidTarget)
            {
                //공격 대상 탐색
                FindTarget();
            }

            if (IsValidTarget)
            {
                if (tower.Data.attackType == 0)
                {
                    //근접 공격
                    AttackMeleeAsync();
                }
                else
                {
                    //원거리 공격
                    AttackProjectileAsync();
                }
            }
            await UniTask.Delay(TimeSpan.FromSeconds(tower.AttackInterval));
        }
    }

    public async void AttackMeleeAsync()
    {
        tower.animationHandler.Attack(tower.AttackSpeed);
        tower.SetDirection(target.transform.position);
        await UniAttackMeleeDamageAsync(0.5f);
    }

    public async void AttackProjectileAsync()
    {
        tower.animationHandler.Attack(tower.AttackSpeed);
        tower.SetDirection(target.transform.position);
        await UniAttackProjectileDamageApplyAsync(1f);
    }


    private async UniTask UniAttackProjectileDamageApplyAsync(float ratio)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(tower.animationHandler.GetCurrentAnimationClipLength() * ratio));
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
    private async UniTask UniAttackMeleeDamageAsync(float ratio)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(tower.animationHandler.GetCurrentAnimationClipLength() * ratio));
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

}
