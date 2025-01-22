using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public enum AttackType
    {
        Near,   //근거리
        Far,    //원거리
    }

    public float attackRadius = 2f;
    public float attackInterval = 1f;
    public int damage = 10;
    public string TowerId;
    private Enemy target;

    public bool IsValidTarget
    {
        get
        {
            return target != null && !target.IsDead && Vector3.Distance(target.transform.position, transform.position) <= attackRadius;
        }
    }

    public void AttackTarget()
    {
        target.OnDamaged(damage);
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