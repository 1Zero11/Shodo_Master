using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTile : MonoBehaviour {
    public Text kanji;
    public Text power;
    public TileManager tileManager;
    public int number;

	// Use this for initialization
	void Start () {
        
        gameObject.GetComponent<Button>().onClick.AddListener(Click);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
        tileManager.RecieveClick(number);
    }


}
