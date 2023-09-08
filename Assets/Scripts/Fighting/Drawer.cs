using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour {

    public GameObject brush;
    public GameObject teachingBrush;
    public bool isDeletingLines = true;
    private List<GameObject> lastHelpLines = new List<GameObject>();
    MLine[] drawLines;
    List<GameObject> instantiatedStrokes = new List<GameObject>();
 
    // Use this for initialization
    void Start () {
		
	}

    private Vector3 to23(Vector2 point, int numberOfLine, int p, bool single, bool fromEnemy)
    {
        Vector3 send = Camera.main.ScreenToWorldPoint(new Vector2(drawLines[numberOfLine].points[p].x * Screen.width, drawLines[numberOfLine].points[p].y * Screen.height));
        if(single)
            send.z = 1;
        else
            send.z = 2;

        if (fromEnemy)
            send.y += 5;

        return send;
    }

    public void DrawKanji(List<MLine> lines, bool fromEnemy)
    {
        drawLines = new MLine[lines.Count];
        int i = 0;
        foreach (MLine l in lines)
        {
            drawLines[i] = l;
            i++;
        }


        StartCoroutine(DrawFull(fromEnemy));
    }

    public void DrawPath(myOwnPath path, Color color, bool help = false)
    {
        //drawLines = new MLine[lines.Count];
        List<Vector2> pt = path.vectors;
        /*
        for(int i = 0; i < pt.Count; i++)
        {
            pt[i] = Camera.main.ScreenToWorldPoint(pt[i]);
        }*/

        if (help)
            lastHelpLines.Add(DrawSinglePath(pt, color, help));
        else
        {
            instantiatedStrokes.Add(DrawSinglePath(pt, color));
            if (lastHelpLines.Count > 0 && isDeletingLines)
            {
                Destroy(lastHelpLines[0]);
                lastHelpLines.RemoveAt(0);
            }
        }


        //StartCoroutine(DrawSingle(numberOfLine));
        //GameControl.control.DrawAllPaths(lines);
    }

    public GameObject DrawSinglePath(List<Vector2> vectors, Color color, bool help = false)
    {
        GameObject stroke = Instantiate(teachingBrush);
        if (help)
            stroke.transform.Translate(Vector3.forward * 0.2f);
        stroke.GetComponent<Renderer>().material.SetColor("_EmisColor", color);
        stroke.GetComponent<PathCreator>().path = new Path(vectors);
        stroke.GetComponent<RoadCreator>().UpdateRoad();
        stroke.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0f, Random.Range(0f, 0.5f));
        return stroke;
    }

    public void DeleteAllLines()
    {
        foreach(GameObject gm in instantiatedStrokes)
        {
            Destroy(gm);
        }
        instantiatedStrokes.Clear();
    }

    public void TurnLines(bool isActive)
    {
        foreach (GameObject gm in instantiatedStrokes)
        {
            gm.SetActive(isActive);
        }

        foreach (GameObject gm in lastHelpLines)
        {
            gm.SetActive(isActive);
        }
    }




    /*private void DrawStroke()
    {

        //drawLines[numberOfLine].points[p]
        if (single)
            speed = 20;
        else
            speed = 5;
        Vector3 pos = Vector3.MoveTowards(newBrush.transform.position, to23(drawLines[numberOfLine].points[p]), speed * Time.deltaTime);
        newBrush.transform.position = pos;
        if (newBrush.transform.position == to23(drawLines[numberOfLine].points[p]))
        {
            p++;
            if (p >= 5&&!single)
            {
                numberOfLine++;
                if (numberOfLine + 1 <= drawLines.Length)
                {
                    p = 0;
                    if(single)
                        newBrush = Instantiate(brush, to23(drawLines[numberOfLine].points[p]), Quaternion.identity);
                    else
                        newBrush = Instantiate(teachingBrush, to23(drawLines[numberOfLine].points[p]), Quaternion.identity);

                }
                else
                {
                    p = 0;
                    numberOfLine = 0;
                    targeted = false;
                }
            }
            else if(p >= 5)
            {
                if(numberOfLine == drawLines.Length-1)
                {
                    MainFightingScript.MFS.WritingEnded(true);
                    GameObject[] trails = GameObject.FindGameObjectsWithTag("Trail");
                    foreach (GameObject t in trails)
                    {
                        Destroy(t);
                    }
                }
                p = 0;
                numberOfLine = 0;
                targeted = false;
                
            }
        }
    }*/

    private IEnumerator DrawSingle(int numberOfLine)
    {
        int speed = 15;
        int p = 0;
        GameObject newBrush = Instantiate(brush, to23(drawLines[numberOfLine].points[p], numberOfLine, p, true, false), Quaternion.identity);
        while (true)
        {
            Vector3 pos = Vector3.MoveTowards(newBrush.transform.position, to23(drawLines[numberOfLine].points[p],numberOfLine,p,true, false), speed * Time.deltaTime);
            newBrush.transform.position = pos;
            if (newBrush.transform.position == to23(drawLines[numberOfLine].points[p], numberOfLine, p, true, false))
            {
                p++;
                if (p >= 5)
                {
                    if (numberOfLine == drawLines.Length - 1)
                    {
                        //MainFightingScript.MFS.WritingSingleLineEnded();
                        GameObject[] trails = GameObject.FindGameObjectsWithTag("Trail");
                        foreach (GameObject t in trails)
                        {
                            Destroy(t);
                        }
                        
                    }
                    break;
                }
            }

            yield return null;

        }
    }



    private IEnumerator DrawFull(bool fromEnemy)
    {
        int numberOfLine = 0;
        int speed = 5;
        int p = 0;
        GameObject newBrush = Instantiate(teachingBrush, to23(drawLines[numberOfLine].points[p], numberOfLine, p, false, fromEnemy), Quaternion.identity);

        while (true)
        {
            Vector3 pos = Vector3.MoveTowards(newBrush.transform.position, to23(drawLines[numberOfLine].points[p], numberOfLine, p, false, fromEnemy), speed * Time.deltaTime);
            newBrush.transform.position = pos;
            if (newBrush.transform.position == to23(drawLines[numberOfLine].points[p], numberOfLine, p, false, fromEnemy))
            {
                p++;
                if (p >= 5)
                {
                    numberOfLine++;
                    if (numberOfLine + 1 <= drawLines.Length)
                    {
                        p = 0;
                        newBrush = Instantiate(teachingBrush, to23(drawLines[numberOfLine].points[p], numberOfLine, p, false, fromEnemy), Quaternion.identity);

                    }
                    else
                    {
                        p = 0;
                        numberOfLine = 0;
                        //MainFightingScript.MFS.ableToDraw = true;
                        break;
                    }
                }
            }

            yield return null;

        }
    }


}
