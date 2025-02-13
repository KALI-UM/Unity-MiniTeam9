using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpumAnimationHandler : MonoBehaviour
{
    [ReadOnly]
    public SPUM_Prefabs spumAnimator;

    public static int moveHash = Animator.StringToHash("1_Move");
    public static int attackHash = Animator.StringToHash("2_Attack");
    public static int attackSpeedHash = Animator.StringToHash("AttackSpeed");
    public static int deathHash = Animator.StringToHash("4_Death");

    public Action onDeathExit;

    private void Awake()
    {
        spumAnimator = GetComponent<SPUM_Prefabs>();
        spumAnimator.OverrideControllerInit();
        
    }

    private void Start()
    {
    }

    public void Move(bool value)
    {
        spumAnimator.PlayAnimation(PlayerState.IDLE, 0);

        if (value)
        {
            spumAnimator.PlayAnimation(PlayerState.MOVE, 0);
        }

        //spumAnimator._anim.SetBool(moveHash, value);
    }

    public void Attack(float speed)
    {
        //if (spumAnimator._anim.GetCurrentAnimatorStateInfo(0).GetHashCode() == attackHash)
        //{

        //}
        //else
        //{
        spumAnimator._anim.SetFloat(attackSpeedHash, speed);
        spumAnimator.PlayAnimation(PlayerState.ATTACK, 0);
        //}
    }

    public float GetCurrentAnimationClipLength()
    {
        return spumAnimator._anim.GetCurrentAnimatorStateInfo(0).length;
    }

    public void Death()
    {
        spumAnimator._anim.SetTrigger(deathHash);
    }

    public void OnDeathAnimationExit()
    {
        onDeathExit?.Invoke();
    }
}
