using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum AttackType
    {
        Near,   //�ٰŸ�
        Far,    //���Ÿ�
    }

    public float attackRadius;
    public string TowerId;
    public Enemy Target;

    
}
