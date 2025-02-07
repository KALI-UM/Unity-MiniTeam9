using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerTable;

public class TowerData : ScriptableObject
{
    public eTower Id;
    public string key;
    public int grade;
    public int attackType;
    public int attackPower;
    public float attackSpeed;
    public float attackRange;
    public int saleGold;
    public int saleGem;

    public void SetData(TowerRawData raw)
    {
        Id = (eTower)raw.Tower_ID;
        key = raw.Strnig_Key;
        grade = raw.Tower_Grade;
        attackType = raw.Tower_AttackType;
        attackPower = raw.Tower_AttackPower;
        attackSpeed = raw.Tower_AttackSpeed;
        attackRange = raw.Tower_AttackRange;
        saleGold = raw.Tower_SaleGold;
        saleGem = raw.Tower_SaleGem;
    }
}
