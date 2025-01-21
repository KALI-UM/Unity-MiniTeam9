using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private EnemyManager enemyManager;

    public enum AttackType
    {
        Near,   //근거리
        Far,    //원거리
    }

    public float attackRadius=2f;
    public float attackInterval = 1f;
    public int damage = 10;
    public string TowerId;
    public Enemy Target;

    public bool IsValidTarget
    {
        get
        {
            return Target != null && !Target.IsDead && Vector3.Distance(Target.transform.position, transform.position) <= attackRadius;
        }
    }

    private Coroutine coAttack;

    private void Awake()
    {
        enemyManager = GameObject.FindGameObjectsWithTag("GameController").First(obj => obj.name.Equals("EnemyManager")).GetComponent<EnemyManager>();
    }

    private void Update()
    {
        if (!IsValidTarget)
        {
            FindTarget();
        }

    }

    private void AttackTarget()
    {
        Target.OnDamaged(damage);
        KALLogger.Log("공격", this);
    }

    private IEnumerator CoAttack()
    {
        while (IsValidTarget)
        {
            AttackTarget();
            yield return new WaitForSeconds(attackInterval);
        }
    }

    private void FindTarget()
    {
        var closestEnemy = enemyManager.GetComponentsInChildren<Enemy>().Where(e => !e.IsDead).
            OrderBy(e => Vector3.Distance(e.transform.position, transform.position)).FirstOrDefault();

        if (closestEnemy != null && Vector3.Distance(closestEnemy.transform.position, transform.position) <= attackRadius)
        {
            Target = closestEnemy;
            coAttack = StartCoroutine(CoAttack());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);


        if (!IsValidTarget)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, Target.transform.position);
    }
}