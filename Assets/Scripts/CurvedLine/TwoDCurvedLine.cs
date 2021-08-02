using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TwoDCurvedLine : MonoBehaviour {
   
    public float lineSegmentSize = 0.15f;
    public float lineWidth = 0.1f;
    public Vector3[] linePositions = new Vector3[0];
    public GameObject whiteCircle;
    public bool allowChange = true;

    private Vector3[] linePositionsOld = new Vector3[0];
    private NullScriot[] linePoints;
    private List<GameObject> circles = new List<GameObject>();
    // Use this for initialization


    void Start () {
        GetPoints();
        SetPointsToLine();
    }

    public void Update()
    {
        if (allowChange)
        {
            GetPoints();
            SetPointsToLine();
        }
    }

    public void GetPoints()
    {
        //find curved points in children
        linePoints =  GetComponentsInChildren<NullScriot>();

        //add positions
        linePositions = new Vector3[linePoints.Length];
        for (int i = 0; i < linePoints.Length; i++)
        {
            linePositions[i] = linePoints[i].transform.position;
        }
    }

    // Update is called once per frame
    

    public void SetPointsToLine()
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

            foreach (GameObject gm in circles)
            {
                Destroy(gm);
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


}
