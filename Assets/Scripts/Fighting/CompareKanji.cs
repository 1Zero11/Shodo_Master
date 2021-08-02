using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MLine
{
    public Vector2[] points = new Vector2[2];
    public MLine(Vector2 start, Vector2 end)
    {
        points[0] = start;
        points[1] = end;
    }
}



public static class CompareKanji{

	

    public static bool CompareLine(MLine line, Kanji target, int numberOfLine)
    {
        float mistakeMargin = (target.lines[numberOfLine].points[1] - target.lines[numberOfLine].points[0]).magnitude*0.3f + 150f;
        Vector2 difStart = target.lines[numberOfLine].points[0] - line.points[0];
        Vector2 difEnd = target.lines[numberOfLine].points[1] - line.points[1];
        //Debug.Log("Comparing " + target.paths[numberOfLine].vectors[0] + " and " + line.points[0]);
        //Debug.Log("Diff are " + difStart + " and " + difEnd);
        if (Mathf.Abs(difStart.x) > mistakeMargin||
            Mathf.Abs(difStart.y) > mistakeMargin||
            Mathf.Abs(difEnd.x) > mistakeMargin||
            Mathf.Abs(difEnd.y) > mistakeMargin)
        {
            //Debug.Log("Returned false");
            return false;
        }
        //Debug.Log("Returned true");
        return true;
    }
    /*
	public static bool compare(Kanji writings, Kanji target){
		if (target.lines.Count != writings.lines.Count) {
			Debug.Log (target.lines.Count + " not equals " + writings.lines.Count);

			return false;
		}
		bool real = true;
		int ml = 0;
		foreach (MLine line in writings.lines) {
            if (Mathf.Abs(line.points[0].x - target.lines[ml].points[0].x ) > mistakeMargin*2||
			Mathf.Abs(line.points[0].y - target.lines[ml].points[0].y ) > mistakeMargin||
			Mathf.Abs(line.points[4].x - target.lines[ml].points[4].x ) > mistakeMargin*2||
			Mathf.Abs(line.points[4].y - target.lines[ml].points[4].y ) > mistakeMargin) {
				Debug.Log ("False is " + ml);
				real = false;
				break;
			}
			ml++;
		}
		return real;
	}
    */
}

public class Word{
	public List<int> kanjiIndexes = new List<int>();
    public string english;
	public Word(List<int> ls,string e){
		kanjiIndexes = ls;
        english = e;
	}
	public Word(int ls, string e){
		kanjiIndexes.Add (ls);
        english = e;
    }
}

public class Kanji{
	public List<MLine> lines;
    public myOwnPath[] paths;

    public Kanji(myOwnPath[] Paths)
    {
        lines = new List<MLine>();
        foreach(myOwnPath p in Paths)
        {
            lines.Add(new MLine(Camera.main.WorldToScreenPoint(p.vectors[0]), Camera.main.WorldToScreenPoint(p.vectors[p.vectors.Count-1])));
        }
        paths = Paths;
        
    }

}