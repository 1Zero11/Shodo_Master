using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SupportKanjiDrawer : MonoBehaviour {

    public GameObject[] prefabs;
    //private CurvedLinePoint[] linePoints = new CurvedLinePoint[0];
    public GameObject structureBuilderObject;
    private GameObject lastLineRenderer;
    private List<GameObject> instantiatedRadicals = new List<GameObject>();

    public Slider verticalSlider;
    public Slider horizontalSlider;

    public GameObject test;

    // Use this for initialization
    void Start () {
        instantiatedRadicals.Add(Instantiate(prefabs[0], structureBuilderObject.transform));
        TwoDCurvedLine[] tls = instantiatedRadicals[instantiatedRadicals.Count - 1].GetComponentsInChildren<TwoDCurvedLine>();
        foreach(TwoDCurvedLine tl in tls)
        {
            tl.allowChange = false;
        }

        Vector3 startPoint = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 endPoint = new Vector3(-36.65f, -37.08f, 0.0f) + startPoint;
        Vector3 startTangent = new Vector3(-4.15f, -8.86f, 0.0f) + startPoint;
        Vector3 endTangent = new Vector3(-15.15f, -25.23f, 0.0f) + startPoint;
        Vector2[] point = new Vector2[11];
        for(int i = 0; i < 11; i++)
        {
            float t = i * 0.1f;
            point[i] = (1f - t) * (1f - t) * (1f - t) * startPoint + 3 * (1f - t) * (1f - t) * t * startTangent + 3 * (1f - t) * t * t * endTangent + t * t * t * endPoint;
            Instantiate(test, point[i], Quaternion.identity);
        }

    }

    public void NextLine()
    {
        instantiatedRadicals.Add(Instantiate(prefabs[0], structureBuilderObject.transform));
        TwoDCurvedLine[] tls = instantiatedRadicals[instantiatedRadicals.Count - 1].GetComponentsInChildren<TwoDCurvedLine>();
        foreach (TwoDCurvedLine tl in tls)
        {
            tl.allowChange = false;
        }

        
        //lastLineRenderer = liveRadical;
        //liveRadical = Instantiate(prefabs[1], structureBuilderObject.transform);
    }

    public void NextKanji()
    {
        
        structureBuilderObject.GetComponent<StructureBuilder>().Deb();
    }

	
	// Update is called once per frame
	void Update () {

        instantiatedRadicals[instantiatedRadicals.Count - 1].transform.localScale = new Vector3(horizontalSlider.value, verticalSlider.value, 1f);

        //linePoints = liveRadical.GetComponentsInChildren<CurvedLinePoint>();

        Plane objplane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;

        if (objplane.Raycast(mRay, out rayDistance))
        {
            this.transform.position = mRay.GetPoint(rayDistance);
        }

        if (Input.GetMouseButtonDown(0))
        {
           
        }

        if (Input.GetMouseButton(0)&& Input.mousePosition.y / Screen.height < 0.5f)
        {
            instantiatedRadicals[instantiatedRadicals.Count-1].transform.position = transform.position;

            /*if (numberOfPoint != 0)
            {
                if(Mathf.Abs(linePoints[numberOfPoint-1].transform.position.x - transform.position.x) < 0.1f)
                {
                    linePoints[numberOfPoint].transform.position =
                        new Vector3(linePoints[numberOfPoint-1].transform.position.x, linePoints[numberOfPoint].transform.position.y, linePoints[numberOfPoint].transform.position.z);
                }

                if (Mathf.Abs(linePoints[numberOfPoint - 1].transform.position.y - transform.position.y) < 0.1f)
                {
                    linePoints[numberOfPoint].transform.position =
                        new Vector3(linePoints[numberOfPoint].transform.position.x, linePoints[numberOfPoint-1].transform.position.y, linePoints[numberOfPoint].transform.position.z);
                }

               
            }


            for (int i = numberOfPoint+1; i < 5; i++)
            {
                linePoints[i].transform.position = linePoints[numberOfPoint].transform.position;
            }*/

        }

        if (Input.GetMouseButtonUp(0) && Input.mousePosition.y / Screen.height < 0.5f)
        {
            //numberOfPoint++;
        }


    }
}
