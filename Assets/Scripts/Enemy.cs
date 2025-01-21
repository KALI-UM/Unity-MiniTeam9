using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyMovement movement;
    public Action onDie;

    public enum EnemyType
    {
        SoldierA,
        SoldierB,
    }

    public struct EnemyData
    {
        public EnemyType type;
        public int maxHp;
    }

    public EnemyData data;

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
        movement = GetComponent<EnemyMovement>();
        data.maxHp = 100;
        data.type = EnemyType.SoldierA;

        OnReset();
    }
    public void Spawn()
    {
        movement.enabled = true;
        movement.Spawn();
    }

    public void OnReset()
    {
        IsDead = false;
        Hp = data.maxHp;
    }

    public void OnDamaged(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            OnDie();
        }
    }

    public void OnDie()
    {
        IsDead = true;
        Hp = 0;
        movement.enabled = false;

        KALLogger.Log("enemy - die", this);

        onDie?.Invoke();
    }
}
