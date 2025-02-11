using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Projectile : Effect
{
    private Vector3 destination;


    public override void Play(Vector3 position)
    {
        base.Play(position);
        transform.DODynamicLookAt(destination, duration);
        transform.DOMove(destination, duration).OnComplete(() => Release());
    }

    public void SetDestination(Vector3 position)
    {
        destination = position;
    }
}
