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
    private EnemyData data;
    public EnemyData Data
    {
        get => data;
    }

    public float Speed
    {
        get => Data.moveSpeed;
    }

    private Vector3 direction;

    private int currentIndex = 0;
    public int CurrentInex
    {
        get => currentIndex;
        private set
        {
            currentIndex = value % EnemyManager.WayPointData.wayPoints.Count();
        }
    }

    public bool IsMovingToWaypoint
    {
        get;
        private set;
    } = false;

    private void Update()
    {
        if (Vector3.Distance(transform.position, EnemyManager.WayPointData.wayPoints[CurrentInex].position) < 0.1f)
        {
            transform.position = EnemyManager.WayPointData.wayPoints[CurrentInex].position;
            CurrentInex++;
            direction = (EnemyManager.WayPointData.wayPoints[CurrentInex].position - transform.position).normalized;

            if (IsMovingToWaypoint)
            {
                IsMovingToWaypoint = false;
            }
        }

        if (!IsMovingToWaypoint)
        {
            if (Vector3.Distance(transform.position, EnemyManager.WayPointData.wayPoints[CurrentInex].position) < 0.05f)
            {
                transform.position = EnemyManager.WayPointData.wayPoints[CurrentInex].position;
                CurrentInex++;
                direction = (EnemyManager.WayPointData.wayPoints[CurrentInex].position - transform.position).normalized;
            }
            MoveTo();
        }
        else
        {
            if (Vector3.Distance(transform.position, EnemyManager.WayPointData.wayPoints[CurrentInex].position) < 0.05f)
            {
                transform.position = EnemyManager.WayPointData.wayPoints[CurrentInex].position;
                CurrentInex++;
                direction = (EnemyManager.WayPointData.wayPoints[CurrentInex].position - transform.position).normalized;

                IsMovingToWaypoint = false;
                MoveTo();
            }
            else
            {
                MoveToWayPoints();
            }
        }
    }

    public void Spawn()
    {
        IsMovingToWaypoint = true;
        transform.position = EnemyManager.WayPointData.spawnPoint.position;
        direction = EnemyManager.WayPointData.initialDirection;
    }

    private void MoveTo()
    {
        Vector3 newPosition = transform.position +direction * Speed * Time.deltaTime;

        //if()

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, EnemyManager.WayPointData.MinPoint.x, EnemyManager.WayPointData.MaxPoint.x),
                                        Mathf.Clamp(transform.position.y, EnemyManager.WayPointData.MinPoint.y, EnemyManager.WayPointData.MaxPoint.y), 0);

    }

    private void MoveToWayPoints()
    {
        transform.position += direction * Speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, EnemyManager.WayPointData.SpawnMinPoint.x, EnemyManager.WayPointData.SpawnMaxPoint.x),
                                        Mathf.Clamp(transform.position.y, EnemyManager.WayPointData.SpawnMinPoint.y, EnemyManager.WayPointData.SpawnMaxPoint.y), 0);

    }

    public void InitializeData(EnemyData data)
    {
        this.data = data;
    }

    //private bool MovePassed()
    //{
    //    Vector3 min;
    //    Vector3 max;

    //    if(transform.position>)
    //}
}
