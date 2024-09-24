using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    private Vector3[] splinePoint;
    private int splineCount;

    public bool debug_drawspline = true;
    public float gizmoRadius = 0.1f; // Radius of the sphere to draw for each point
    public Color gizmoColor = Color.red; // Color of the gizmos

    private void Start()
    {
        UpdateSplinePoints();
    }

    private void Update()
    {
        if (splineCount > 1)
        {
            for (int i = 0; i < splineCount - 1; i++)
            {
                Debug.DrawLine(splinePoint[i], splinePoint[i + 1], Color.green);
            }
        }
    }

    private void OnDrawGizmos()
    {
        UpdateSplinePoints();

        if (splinePoint == null || splinePoint.Length == 0)
        {
            Debug.Log("Spline points array is null or empty");
            return;
        }

        Gizmos.color = gizmoColor;

        for (int i = 0; i < splineCount; i++)
        {
            Vector3 pointPosition = transform.GetChild(i).position;
            Gizmos.DrawSphere(pointPosition, gizmoRadius);
            //Debug.Log($"Drawing gizmo sphere at {pointPosition}");
        }

        for (int i = 0; i < splineCount - 1; i++)
        {
            Vector3 startPoint = transform.GetChild(i).position;
            Vector3 endPoint = transform.GetChild(i + 1).position;
            Gizmos.DrawLine(startPoint, endPoint);
            //Debug.Log($"Drawing gizmo line from {startPoint} to {endPoint}");
        }
    }

    private void UpdateSplinePoints()
    {
        splineCount = transform.childCount;
        splinePoint = new Vector3[splineCount];

        for (int i = 0; i < splineCount; i++)
        {
            splinePoint[i] = transform.GetChild(i).position;
        }
    }

    public Vector3 WhereOnSpline(Vector3 pos)
    {
        int closestSplinePoint = GetClosestSplinePoint(pos);

        if (closestSplinePoint == 0)
        {
            return splineSegment(splinePoint[0], splinePoint[1], pos);
        }
        else if (closestSplinePoint == splineCount - 1)
        {
            return splineSegment(splinePoint[splineCount - 1], splinePoint[splineCount - 2], pos);
        }
        else
        {
            Vector3 leftSeg = splineSegment(splinePoint[closestSplinePoint - 1], splinePoint[closestSplinePoint], pos);
            Vector3 rightSeg = splineSegment(splinePoint[closestSplinePoint + 1], splinePoint[closestSplinePoint], pos);

            if ((pos - leftSeg).sqrMagnitude <= (pos - rightSeg).sqrMagnitude)
            {
                return leftSeg;
            }
            else
            {
                return rightSeg;
            }
        }
    }

    private int GetClosestSplinePoint(Vector3 pos)
    {
        int closestPoint = -1;
        float shortestDistance = 0.0f;

        for (int i = 0; i < splineCount; i++)
        {
            float sqrDistance = (splinePoint[i] - pos).sqrMagnitude;
            if (shortestDistance == 0.0f || sqrDistance < shortestDistance)
            {
                shortestDistance = sqrDistance;
                closestPoint = i;
            }
        }

        return closestPoint;
    }

    public Vector3 splineSegment(Vector3 v1, Vector3 v2, Vector3 pos)
    {
        Vector3 v1ToPos = pos - v1;
        Vector3 seqDirection = (v2 - v1).normalized;

        float distanceFromV1 = Vector3.Dot(seqDirection, v1ToPos);

        if (distanceFromV1 < 0.0f)
        {
            return v1;
        }
        else if (distanceFromV1 * distanceFromV1 > (v2 - v1).sqrMagnitude)
        {
            return v2;
        }
        else
        {
            Vector3 fromV1 = seqDirection * distanceFromV1;
            return v1 + fromV1;
        }
    }

    // Public method to check if splinePoint is initialized
    public bool IsSplinePointInitialized()
    {
        return splinePoint != null && splinePoint.Length > 0;
    }
}
