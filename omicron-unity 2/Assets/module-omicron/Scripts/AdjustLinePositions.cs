using System;
using System.Collections;
using System.Collections.Generic;

using System.IO;

using System.Text;
using UnityEngine;

public class AdjustLinePositions : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public Vector3 velocity;
    List<List<Vector3>> constellationsVectorData1 = new List<List<Vector3>>();
    List<List<Vector3>> constellationsVectorDataVelocity = new List<List<Vector3>>();

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // Example: Moving the first point along the x-axis
        Vector3 firstPointPos = lineRenderer.GetPosition(0);
        firstPointPos.x += Time.deltaTime;
        lineRenderer.SetPosition(0, firstPointPos);

        // If you need to adjust multiple points, consider modifying the positions array directly
        Vector3[] positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);

        for (int i = 0; i < positions.Length; i++)
        {
            // Example modification: raising all points along the y-axis
            positions[i].y += Time.deltaTime * 0.5f;
        }

        // Apply the modified positions back to the LineRenderer
        lineRenderer.SetPositions(positions);
    }
}
