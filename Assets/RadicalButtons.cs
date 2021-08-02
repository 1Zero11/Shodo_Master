using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadicalButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void Push()
    {
        MainFightingScript.MFS.ShowRadicals();
        //MainFightingScript.MFS.loa GameControl.control.FindRadical(button, MainFightingScript.MFS.currentKanjiString);
    }


}
