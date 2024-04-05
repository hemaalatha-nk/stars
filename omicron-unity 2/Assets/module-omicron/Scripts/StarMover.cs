using System.Collections.Generic;
using UnityEngine;

public class StarMover : MonoBehaviour
{
    public Vector3 velocity;
    public Camera mainCamera;
    public List<LineRenderer> lr=new List<LineRenderer>();
    public List<List<LineRenderer>> llr = new List<List<LineRenderer>>();
    public float TimeScale = 1000f; // 1,000 years per real-time second
    private float elapsedTimeYears = 0f; // Track elapsed time in years
    Vector3 initial_pos;
    //public line
    private void Start()
    {
        initial_pos = transform.position;
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
           mainCamera.transform.rotation * Vector3.up);
    }

    void Update()
    {
        // Move the star based on velocity, adjusting for frame rate
        
        //LineRenderer[] allLineRenderers = FindObjectsOfType<LineRenderer>();
        //LineRenderer matchedLineRenderer = FindLineRendererByStartPosition(allLineRenderers, initial_pos, 0);

        //if (matchedLineRenderer != null)
        //{
        //    //Debug.Log("Found a matching LineRenderer.");
        //    matchedLineRenderer.transform.position += velocity * (Time.deltaTime / 100f);
        //    // Further processing here
        //}
        //else
        //{
        //    //Debug.Log("No matching LineRenderer found.");
        //}
        transform.position += velocity * (Time.deltaTime / 100f);
        //Debug.Log("lr count"+lr.Count);
    }

    public void stop()
    {
        Debug.Log("stop");
    }

    public void start()
    {
        Debug.Log("start");
    }
    public void reset()
    {
        Debug.Log("reset");
    }

    LineRenderer FindLineRendererByStartPosition(LineRenderer[] lineRenderers, Vector3 startPosition, float tolerance)
    {
        foreach (LineRenderer lr in lineRenderers)
        {
            if (lr.positionCount > 0)
            {
                Vector3 firstPosition = lr.GetPosition(0);
                if (Vector3.Distance(firstPosition, startPosition) <= tolerance)
                {
                    return lr;
                }
            }
        }

        return null; // No match found
    }


}
