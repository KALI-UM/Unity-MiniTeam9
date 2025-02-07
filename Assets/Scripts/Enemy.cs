using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static EnemyTable;
using static TowerTable;

public class Enemy : MonoBehaviour
{
    public EnemyMovement movement;
    public EnemyHpBar hpBar;

    public Animator animator;

    public Action onDie;

    public eEnemy EnemyId
    {
        get;
        private set;
    }

    [ReadOnly, SerializeField]
    private EnemyData data;

    public EnemyData Data
    {
        get => data;
    }

    [ReadOnly, SerializeField]
    private int hp;
    public int Hp
    {
        get => hp;
        private set => hp = value;
    }

    public bool IsDead
    {
        get;
        private set;
    }

    private void Awake()
    {
        EnemyId = Data.Id;
        movement.InitializeData(data);
    }

    public void InitializeData(EnemyData data)
    {
        EnemyId = data.Id;
        this.data = data;
    }

    public virtual void Spawn()
    {
        movement.enabled = true;
        movement.Spawn();
        animator.SetBool("1_Move", true);
    }

    public void OnReset()
    {
        IsDead = false;

        Hp = Data.maxHp;
        hpBar.OnHpChanged(Hp, Data.maxHp);
    }

    public void OnDamaged(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            OnDie();
        }
        hpBar.OnHpChanged(Hp, Data.maxHp);
    }

    public virtual void OnDie()
    {
        IsDead = true;
        Hp = 0;
        movement.enabled = false;

        onDie?.Invoke();
        animator.SetTrigger("4_Death");
    }

    public void SetDirection(Vector3 targetPos)
    {
        if (transform.position.x < targetPos.x)
        {
            var one = Vector3.one;
            one.x *= -1;
            transform.localScale = one;
        }
        else
        {
            SetDefaultDirection();
        }
    }

    public void SetDefaultDirection()
    {
        transform.localScale = Vector3.one;
    }
}
