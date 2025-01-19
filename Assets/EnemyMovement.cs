using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;




public class EnemyMovement : MonoBehaviour
{

    public Transform spawnPoint;
    public List<Transform> wayPoints;
    private Vector3 minPoint;
    private Vector3 maxPoint;

    private int currentIndex = 0;
    public float speed;
    private Vector3 direction;

    public int CurrentIndex
    {
        get => currentIndex;
        private set
        {
            currentIndex = value % wayPoints.Count();
        }
    }

    private void Awake()
    {
        minPoint.x = wayPoints.Min(t => t.position.x);
        minPoint.y = wayPoints.Min(t => t.position.y);
        maxPoint.x = wayPoints.Max(t => t.position.x);
        maxPoint.y = wayPoints.Max(t => t.position.y);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, wayPoints[CurrentIndex].position) < 0.1f)
        {
            transform.position = wayPoints[CurrentIndex].position;
            CurrentIndex++;
            direction = (wayPoints[CurrentIndex].position - transform.position).normalized;
        }
        MoveTo();
    }

    private void OnEnable()
    {
        Spawn();
    }

    private void Spawn()
    {
        transform.position = spawnPoint.position;
        direction = (wayPoints[CurrentIndex].position - transform.position).normalized;
    }

    private void MoveTo()
    {
        transform.position += direction * speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minPoint.x, maxPoint.x), Mathf.Clamp(transform.position.y, minPoint.y, maxPoint.y), 0) ;
    }
}
