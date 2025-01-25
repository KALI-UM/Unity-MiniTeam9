using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyTable;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public eEnemy Id;
    public string key;
    public int grade;
    public int maxHp;
    public float moveSpeed;
    public int dropGold;
    public int dropGem;

    public void SetData(EnemyRawData raw)
    {
        Id = (eEnemy)raw.Monster_ID;
        key = raw.String_Key;
        grade = raw.Monster_Grade;
        maxHp = raw.Monster_HP;
        moveSpeed = raw.Monster_MoveSpeed;
        dropGold = raw.DropGold;
        dropGem = raw.DropGem;
    }
}
