using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float height;
    private float width;

    private Rect cameraViewRect;
    private Rect requiredViewRect;

    [SerializeField] private List<Transform> requiredViewPosition;

    void Start()
    {
        SetRequiredViewRectangle();
        SetCameraSize();
    }

    private void SetCameraSize()
    {
        float aspectRatio = (float)Screen.width / Screen.height;

        float size = Mathf.Max((requiredViewRect.height * 0.5f), (requiredViewRect.width * 0.5f / aspectRatio));

        if (Camera.main.orthographicSize < size)
        {
            Camera.main.orthographicSize = size;
        }
    }

    private void SetRequiredViewRectangle()
    {
        requiredViewRect.x = requiredViewPosition.Min(tr => tr.position.x);
        requiredViewRect.y = requiredViewPosition.Min(tr => tr.position.y);
        requiredViewRect.xMax = requiredViewPosition.Max(tr => tr.position.x);
        requiredViewRect.yMax = requiredViewPosition.Max(tr => tr.position.y);
    }

    private void OnDrawGizmos()
    {
        Vector3 topLeft = new Vector3(requiredViewRect.xMin, requiredViewRect.yMax, 0f);
        Vector3 topRight = new Vector3(requiredViewRect.xMax, requiredViewRect.yMax, 0f);
        Vector3 bottomRight = new Vector3(requiredViewRect.xMax, requiredViewRect.yMin, 0f);
        Vector3 bottomLeft = new Vector3(requiredViewRect.xMin, requiredViewRect.yMin, 0f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(topLeft, topRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomRight, bottomLeft);
        Gizmos.DrawLine(bottomLeft, topLeft);
    }
}
