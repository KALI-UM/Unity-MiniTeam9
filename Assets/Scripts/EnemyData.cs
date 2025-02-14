using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyTable;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    static private readonly string spumPath = "Prefabs/Units/{0}";

    [ReadOnly] public eEnemy Id;
    [ReadOnly] public string key;
    [ReadOnly] public int grade;
    public int maxHp;
    public float moveSpeed;
    public int dropGold;
    public int dropGem;
    public GameObject enemySpum;

    public void SetData(EnemyRawData raw)
    {
        Id = (eEnemy)raw.Monster_ID;
        key = raw.String_Key;
        grade = raw.Monster_Grade;
        maxHp = raw.Monster_HP;
        moveSpeed = raw.Monster_MoveSpeed;
        dropGold = raw.DropGold;
        dropGem = raw.DropGem;
        enemySpum = Resources.Load<GameObject>(string.Format(spumPath, raw.Monster_Resource));
    }

    public EnemyRawData ConvertToRawData()
    {
        EnemyRawData raw = new EnemyRawData();
        raw.Monster_ID = (int)Id;
        raw.String_Key = key;
        raw.Monster_Grade = grade;
        raw.Monster_HP = maxHp;
        raw.Monster_MoveSpeed = moveSpeed;
        raw.DropGold = dropGold;
        raw.DropGem = dropGem;
        raw.Monster_Resource = DataTableManager.EnemyTable.Get(Id).Monster_Resource;
        return raw;
    }
}
