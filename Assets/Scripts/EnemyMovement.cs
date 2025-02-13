using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static EnemyTable;


public class EnemyMovement : MonoBehaviour
{
    public Enemy enemy;

    public float Speed
    {
        get => enemy.Data.moveSpeed;
    }

    private Vector3 direction;

    private int nextIndex = 0;
    public int NextIndex
    {
        get => nextIndex;
        private set
        {
            nextIndex = value % EnemyManager.WayPointData.wayPoints.Count();
        }
    }

    public bool IsOnWaypoints
    {
        get;
        private set;
    } = false;

    private void Update()
    {
        MoveTo();
    }

    public void Spawn()
    {
        IsOnWaypoints = false;
        transform.position = EnemyManager.WayPointData.spawnPoint.position;
        NextIndex = 0;
        direction = EnemyManager.WayPointData.initialDirection;

        enemy.SetDirection(direction);
    }

    private void MoveTo()
    {
        Vector3 next;

        float sqrDelta = Mathf.Pow(Speed * Time.deltaTime, 2);
        float sqrMax = Vector3.SqrMagnitude(transform.position - EnemyManager.WayPointData.wayPoints[NextIndex].position);

        if (sqrDelta >= sqrMax)
        {
            enemy.SetDirection(direction);

            if (!IsOnWaypoints)
            {
                IsOnWaypoints = true;
            }

            NextIndex++;
            float delta = Mathf.Sqrt(sqrDelta);
            float max = Mathf.Sqrt(sqrMax);

            next = direction * max;
            direction = EnemyManager.WayPointData.directions[NextIndex];
            next += direction * (delta - max);
        }
        else
        {
            next = direction * Speed * Time.deltaTime;
        }

        transform.position = transform.position + next;
    }

}
