using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTrail : MonoBehaviour {
    public GameObject trail;
    GameObject tr;
    Vector2 lastDownPoint;
    List<MLine> lines = new List<MLine>();
    List<GameObject> inst = new List<GameObject>();
    public GameObject Menu;
    public bool free = false;
    int frames = 0;
    [HideInInspector]
    public int failedLines = 0;
    [HideInInspector]
    public float color = 0.0f;
    [HideInInspector]
    public float water = 1f;
    public Material testMat;
    public KanjiMouseDown mouseDown;
    





    void Update() {
        Plane objplane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

        Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;

        if (objplane.Raycast(mRay, out rayDistance)) {
            this.transform.position = mRay.GetPoint(rayDistance);
        }
        if (MainFightingScript.MFS.ableToDraw)
        {
            if (Input.GetMouseButtonDown(0) && mouseDown.Inside)//Input.mousePosition.y / Screen.height < 0.6f
            {
                //MainFightingScript.MFS.kanjiText.text = "";
                tr = Instantiate(trail, transform);
                tr.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Random.Range(0f, 0.5f),0f);

                
                tr.GetComponent<LineRenderer>().startColor = MainFightingScript.MFS.colors[lines.Count];
                tr.GetComponent<LineRenderer>().endColor = MainFightingScript.MFS.colors[lines.Count];
                
                lastDownPoint.x = Input.mousePosition.x;
                lastDownPoint.y = Input.mousePosition.y;
                Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                temp.z = -1.01f;
                tr.GetComponent<LineRenderer>().SetPosition(0, temp);
                tr.GetComponent<LineRenderer>().SetPosition(1, temp);

                if (color <= 1.0f)
                {
                    //tr.GetComponent<LineRenderer>().startColor = new Color(color, color, color);
                }

                if (water >= 0f)
                {
                    water -= 0.1f;
                    tr.GetComponent<Renderer>().material = testMat;
                }



            }
            if (Input.GetMouseButton(0)&&mouseDown.Inside)
            {
                Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                temp.z = -1f;

                
                if (Mathf.Abs(tr.GetComponent<LineRenderer>().GetPosition(tr.GetComponent<LineRenderer>().positionCount - 1).sqrMagnitude - temp.sqrMagnitude) > 0.035f)
                {

                    tr.GetComponent<LineRenderer>().positionCount++;
                    tr.GetComponent<LineRenderer>().SetPosition(tr.GetComponent<LineRenderer>().positionCount - 1, temp);

                    if (color <= 1.0f)
                    {
                        color += 0.001f;
                    }
                }
            }

            if ((Input.GetMouseButtonUp(0)|| !mouseDown.Inside) && tr != null && lastDownPoint != new Vector2(0.0f, 0.0f))
            {
                

                tr.transform.parent = null;
                inst.Add(tr);
                lines.Add(new MLine(lastDownPoint, new Vector2(Input.mousePosition.x, Input.mousePosition.y)));


                ReadThisLine();
               
            }
        }

    }



    public void ReadThisLine() {
        Kanji targetKanji = MainFightingScript.MFS.kanjiAsPaths;

        if (free)
        {
            MainFightingScript.MFS.EndOfLine(lines.Count - 1, Color.white);
        }
        else
        {
            
            //bool result = CompareKanji.compare (new Kanji (lines), targetKanji);
            bool result = CompareKanji.CompareLine(lines[lines.Count - 1], targetKanji, lines.Count - 1);
            //Debug.Log(result);

            Destroy(tr);
            inst.Remove(tr);
            //MainFightingScript.MFS.EndOfLine(lines.Count - 1);

            if (result)
            {
                Color c = MainFightingScript.MFS.colors[lines.Count - 1];
                MainFightingScript.MFS.EndOfLine(lines.Count - 1, c);
            }
            else
            {
                failedLines++;
                if (failedLines == 3)
                    MainFightingScript.MFS.Failed(lines.Count - 2);
                lines.RemoveAt(lines.Count - 1);
            }
        }
        if (targetKanji.lines.Count == lines.Count)
        {
            lines.Clear();

            Invoke("Delayed", 1f);

        }


        lastDownPoint = new Vector2(0.0f, 0.0f);


    }

    public void ButtonFailed()
    {
        if (MainFightingScript.MFS.ableToDraw)
        {
            MainFightingScript.MFS.Failed(lines.Count - 1);
            failedLines = 3;
        }
        else if (MainFightingScript.MFS.isSeingRadical)
        {
            MainFightingScript.MFS.Escape();
        }else
        {
            MainFightingScript.MFS.KanjiComplete();
        }
    }

    public void Delayed()
    {
        foreach (GameObject gm in inst)
        {
            Destroy(gm);
        }
        inst.Clear();
    }

    


}



