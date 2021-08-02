using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureBuilder : MonoBehaviour {

	// Use this for initialization
	public void Deb () {
        float nativeY = 5f;
        float nativeX = 2.8125f;
        float invX = 1 / (2*nativeX);
        float invY = 1 / (2*nativeY);

        //CurvedLineRenderer[] objects = gameObject.GetComponentsInChildren<CurvedLineRenderer>();
        TwoDCurvedLine[] objectsN = gameObject.GetComponentsInChildren<TwoDCurvedLine>();
        string temp = "allKanji.Add(new Kanji(new List<MLine>{";
        /*foreach (CurvedLineRenderer line in objects)
        {
            temp += ("\r\n new MLine(" +
                "new Vector2(" + (line.linePositions[0].x + nativeX)*invX + "f, " + (line.linePositions[0].y + nativeY)*invY + "f), " +
                "new Vector2(" + (line.linePositions[1].x + nativeX)*invX + "f, " + (line.linePositions[1].y + nativeY)*invY + "f), " +
                "new Vector2(" + (line.linePositions[2].x + nativeX) * invX + "f, " + (line.linePositions[2].y + nativeY) * invY + "f), " +
                "new Vector2(" + (line.linePositions[3].x + nativeX) * invX + "f, " + (line.linePositions[3].y + nativeY) * invY + "f), " +
                "new Vector2(" + (line.linePositions[4].x + nativeX)*invX + "f, " + (line.linePositions[4].y + nativeY)*invY + "f)),");
        }*/
        foreach (TwoDCurvedLine line in objectsN)
        {
            line.GetPoints();
            line.SetPointsToLine();
            temp += "\r\n new MLine(";
            foreach(Vector3 p in line.linePositions)
            {
                temp += "new Vector2(" + (p.x + nativeX) * invX + "f, " + (p.y + nativeY) * invY + "f), ";
            }

            for(int i = line.linePositions.Length; i < 5; i++)
            {
                if(i!=4)
                    temp += "new Vector2(" + (line.linePositions[line.linePositions.Length-1].x + nativeX) * invX + "f, " + (line.linePositions[line.linePositions.Length - 1].y + nativeY) * invY + "f), ";
                else
                    temp += "new Vector2(" + (line.linePositions[line.linePositions.Length - 1].x + nativeX) * invX + "f, " + (line.linePositions[line.linePositions.Length - 1].y + nativeY) * invY + "f)";
            }
            temp += "),";
        }

        temp += "},\r\n\"someting\",\"\"));";
        Debug.Log(temp);
        UniClipboard.SetText(temp);
        
    }

    private void Start()
    {
        //Invoke("Deb", 1f);
    }

}
