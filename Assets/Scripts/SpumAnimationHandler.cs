using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpumAnimationHandler : MonoBehaviour
{
    [ReadOnly]
    public Animator animator;

    public static int moveHash = Animator.StringToHash("1_Move");
    public static int attackHash = Animator.StringToHash("2_Attack");
    public static int deathHash = Animator.StringToHash("4_Death");

    public Action onDeathExit;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Move(bool value)
    {
        animator.SetBool(moveHash, value);
    }

    public void Attack()
    {
        animator.SetTrigger(attackHash);
    }

    public void Death()
    {
        animator.SetTrigger(deathHash);
    }

    public void OnDeathAnimationExit()
    {
        onDeathExit?.Invoke();
    }
}
