using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TowerTable;

public class TowerData : ScriptableObject
{
    static private readonly string spritePath = "Textures/Tower/{0}";
    static private readonly string spumPath = "Prefabs/Units/{0}";

    [ReadOnly] public eTower Id;
    [ReadOnly] public string key;
    [ReadOnly] public int grade;
    [ReadOnly] public int attackType;
    public int attackPower;
    public float attackSpeed;
    public float attackRange;
    public int saleGold;
    public int saleGem;
    public Sprite towerSprite;
    public GameObject towerSpum;


    public void SetData(TowerRawData raw)
    {
        Id = (eTower)raw.Tower_ID;
        key = raw.String_Key;
        grade = raw.Tower_Grade;
        attackType = raw.Tower_AttackType;
        attackPower = raw.Tower_AttackPower;
        attackSpeed = raw.Tower_AttackSpeed;
        attackRange = raw.Tower_AttackRange;
        saleGold = raw.Tower_SaleGold;
        saleGem = raw.Tower_SaleGem;
        towerSprite = Resources.Load<Sprite>(string.Format(spritePath, raw.Tower_Resource));
        towerSpum = Resources.Load<GameObject>(string.Format(spumPath, raw.Tower_Resource));
    }

    public TowerRawData ConvertToRawData()
    {
        TowerRawData raw = new TowerRawData();
        raw.Tower_ID = (int)Id;
        raw.String_Key = key;
        raw.Tower_Grade = grade;
        raw.Tower_AttackType = attackType;
        raw.Tower_AttackPower = attackPower;
        raw.Tower_AttackSpeed = attackSpeed;
        raw.Tower_AttackRange = attackRange;
        raw.Tower_SaleGold = saleGold;
        raw.Tower_SaleGem = saleGem;
        raw.Tower_Resource = DataTableManager.TowerTable.Get(Id).Tower_Resource;
        return raw;
    }
}
