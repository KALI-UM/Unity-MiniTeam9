using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WayPointData : MonoBehaviour
{
    public Transform spawnPoint;
    public List<Transform> wayPoints;
    public Vector3 initialDirection;
    public List<Vector3> directions;

    public Vector3 MinPoint
    {
        get;
        private set;
    }
    public Vector3 MaxPoint
    {
        get;
        private set;
    }
    public Vector3 SpawnMinPoint
    {
        get;
        private set;
    }
    public Vector3 SpawnMaxPoint
    {
        get;
        private set;
    }

    private void Awake()
    {
        Vector3 min = Vector3.zero;
        Vector3 max = Vector3.zero;

        min.x = wayPoints.Min(t => t.position.x);
        min.y = wayPoints.Min(t => t.position.y);
        max.x = wayPoints.Max(t => t.position.x);
        max.y = wayPoints.Max(t => t.position.y);

        MinPoint = min;
        MaxPoint = max;

        min.x = Mathf.Min(min.x, spawnPoint.transform.position.x);
        min.y = Mathf.Min(min.y, spawnPoint.transform.position.y);
        max.x = Mathf.Max(max.x, spawnPoint.transform.position.x);
        max.y = Mathf.Max(max.y, spawnPoint.transform.position.y);

        SpawnMinPoint = min;
        SpawnMaxPoint = max;

        initialDirection = (wayPoints[0].position - spawnPoint.position).normalized;
        Vector3 prev = wayPoints[wayPoints.Count - 1].position;
        for (int i = 0; i < wayPoints.Count; i++)
        {
            directions.Add((wayPoints[i].position - prev).normalized);
            prev = wayPoints[i].position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 prev = spawnPoint.position;
        foreach (Transform t in wayPoints)
        {
            Gizmos.DrawLine(prev, t.position);
            prev = t.position;
        }
        Gizmos.DrawLine(prev, wayPoints[0].position);
    }
}
