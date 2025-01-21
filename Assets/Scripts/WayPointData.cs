using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WayPointData : MonoBehaviour
{
    public Transform spawnPoint;
    public List<Transform> wayPoints;

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
