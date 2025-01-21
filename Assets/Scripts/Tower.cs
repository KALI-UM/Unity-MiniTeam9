using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum AttackType
    {
        Near,   //근거리
        Far,    //원거리
    }

    public float attackRadius;
    public string TowerId;
    public Enemy Target;

    
}
