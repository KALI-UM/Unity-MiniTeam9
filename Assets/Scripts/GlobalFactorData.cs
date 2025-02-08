using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalFactorData", menuName = "ScriptableObjects/GlobalFactorData")]

public class GlobalFactorData : ScriptableObject
{
    public float attackPower;
    public float attackSpeed;
    public float attackRange;
    public float towerMoveSpeed;
    //public float enemyMoveSpeed;
}
