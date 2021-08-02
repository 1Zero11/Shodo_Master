using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class CurvedLineRenderer : MonoBehaviour
{
    //PUBLIC
    public float lineSegmentSize = 0.15f;
    public float lineWidth = 0.1f;
    [Header("Gizmos")]
    public bool showGizmos = true;
    public float gizmoSize = 0.1f;
    public Color gizmoColor = new Color(1, 0, 0, 0.5f);
    public Vector3[] linePositions = new Vector3[0];
    public GameObject whiteCircle;
    //PRIVATE
    private CurvedLinePoint[] linePoints = new CurvedLinePoint[0];
    private Vector3[] linePositionsOld = new Vector3[0];
    private List<GameObject> circles;

    // Update is called once per frame
    public void Start()
    {
        GetPoints();
        SetPointsToLine();
    }

    public void Update()
    {
        GetPoints();
        SetPointsToLine();
    }

    void GetPoints()
    {
        //find curved points in children
        linePoints = this.GetComponentsInChildren<CurvedLinePoint>();

        //add positions
        linePositions = new Vector3[linePoints.Length];
        for (int i = 0; i < linePoints.Length; i++)
        {
            linePositions[i] = linePoints[i].transform.position;
        }
    }

    void SetPointsToLine()
    {
        //create old positions if they dont match
        if (linePositionsOld.Length != linePositions.Length)
        {
            linePositionsOld = new Vector3[linePositions.Length];
        }

        //check if line points have moved
        bool moved = false;
        for (int i = 0; i < linePositions.Length; i++)
        {
            //compare
            if (linePositions[i] != linePositionsOld[i])
            {
                moved = true;
            }
        }

        //update if moved
        if (moved == true)
        {
            //LineRenderer line = this.GetComponent<LineRenderer>();

            //get smoothed values
            Vector3[] smoothedPoints = LineSmoother.SmoothLine(linePositions, lineSegmentSize);
          //  StartCoroutine(Draw(smoothedPoints));

            foreach(GameObject gm in circles)
            {
                DestroyImmediate(gm);
            }

            circles.Clear();

            foreach (Vector3 sp in smoothedPoints)
            {
                circles.Add(Instantiate(whiteCircle, sp, Quaternion.identity, transform));
            }

            //set line settings
            /*line.positionCount = smoothedPoints.Length;
            line.SetPositions(smoothedPoints);
            line.startWidth = lineWidth;
            line.endWidth = lineWidth;
            */
        }
    }

    private IEnumerator Draw(Vector3[] points)
    {
        foreach (Vector3 sp in points)
        {
            Instantiate(whiteCircle, sp, Quaternion.identity, transform);
            yield return new WaitForSeconds(0.05f);
        }
    }


    void OnDrawGizmosSelected()
    {
        Update();
    }

    void OnDrawGizmos()
    {
        if (linePoints.Length == 0)
        {
            GetPoints();
        }

        //settings for gizmos
        foreach (CurvedLinePoint linePoint in linePoints)
        {
            linePoint.showGizmo = showGizmos;
            linePoint.gizmoSize = gizmoSize;
            linePoint.gizmoColor = gizmoColor;
        }
    }
}
