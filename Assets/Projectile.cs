using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Projectile : Effect
{
    private Vector3 destination;
    private Vector3 projectileDir = new Vector3(1, 0, 0);

    public override void Play(Vector3 position)
    {
        base.Play(position);
        transform.DOMove(destination, duration).SetEase(Ease.InOutCirc).OnComplete(() => ReturnToObjectPool());

        float angle = Vector2.SignedAngle(projectileDir, destination - transform.position);
        transform.rotation = Quaternion.Euler(0, 0, angle); ;
    }

    public void SetDestination(Vector3 position)
    {
        destination = position;
    }
}
