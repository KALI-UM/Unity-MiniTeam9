using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    public WayPointData wayPointData;

    private int currentIndex = 0;
    public float speed;
    private Vector3 direction;

    public int CurrentIndex
    {
        get => currentIndex;
        private set
        {
            currentIndex = value % wayPointData.wayPoints.Count();
        }
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, wayPointData.wayPoints[CurrentIndex].position) < 0.1f)
        {
            transform.position = wayPointData.wayPoints[CurrentIndex].position;
            CurrentIndex++;
            direction = (wayPointData.wayPoints[CurrentIndex].position - transform.position).normalized;
        }
        MoveTo();
    }

    public void Spawn()
    {
        transform.position = wayPointData.spawnPoint.position;
        direction = (wayPointData.wayPoints[CurrentIndex].position - transform.position).normalized;
    }

    private void MoveTo()
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, wayPointData.MinPoint.x, wayPointData.MaxPoint.x),
                                        Mathf.Clamp(transform.position.y, wayPointData.MinPoint.y, wayPointData.MaxPoint.y), 0);
    }
}
