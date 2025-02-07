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

    public SpumAnimationHandler animationHandler;
    public GameObject character;

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

        animationHandler.Move(true);
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
        animationHandler.Death();
    }

    public void SetDirection(Vector3 dir)
    {
        SetDefaultDirection();
        if (dir.y > 0 || dir.x > 0)
        {
            var flip = character.transform.localScale;
            flip.x *= -1;
            character.transform.localScale = flip;
        }
    }

    public void SetDefaultDirection()
    {
        character.transform.localScale = Vector3.one;
    }
}
