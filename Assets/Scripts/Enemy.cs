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
    public SpriteRenderer spriteRenderer;

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

    public int Hp
    {
        get;
        private set;
    }

    public bool IsDead
    {
        get;
        private set;
    }

    private void Awake()
    {
        EnemyId=Data.Id;
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
    }

    public void OnReset()
    {
        IsDead = false;
        
        Hp = Data.maxHp;
        hpBar.OnHpChanged(Hp);
    }

    public void OnDamaged(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            OnDie();
        }
        hpBar.OnHpChanged((float)Hp / Data.maxHp);
    }

    public virtual void OnDie()
    {
        IsDead = true;
        Hp = 0;
        movement.enabled = false;

        KALLogger.Log("enemy - die", this);
        onDie?.Invoke();
    }
}
