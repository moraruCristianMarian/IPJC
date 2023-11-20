using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorProjectileTrajectory : MonoBehaviour
{
    public LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
    }

    public void RenderLine(Vector3 startPoint, Vector3 endPoint)
    {
        lineRenderer.positionCount = 2;

        float currentDistance = Vector3.Distance(startPoint, endPoint);
        float maxDistance = 3.0f;

        endPoint = Vector3.Lerp(startPoint, endPoint, maxDistance / currentDistance);

        Vector3[] points = new Vector3[2];
        points[0] = startPoint;
        points[1] = endPoint;

        lineRenderer.SetPositions(points);
    }

    public void EndLine()
    {
        lineRenderer.positionCount = 0;
    }
}
