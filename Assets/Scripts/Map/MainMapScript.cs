using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMapScript : MonoBehaviour {
    public static MainMapScript MMS;

    public Text statsText;

    private Vector2 startPosition;
    private Vector2 endPosition;

    public GameObject hideB;
    public GameObject showB;

    public GameObject list;

    public void ChooseTheLocation(int id)
    {
        SceneManager.LoadScene(id);
    }
    

    void Start () {
        MMS = this;

        statsText.text = GameControl.control.masteryLevel.ToString();

    }
	
	
	void Update () {
       

    }

    public void HideShow(bool show)
    {
        if (show) {
            list.SetActive(true);
            hideB.SetActive(true);
            showB.SetActive(false);
        }
        else
        {
            list.SetActive(false);
            hideB.SetActive(false);
            showB.SetActive(true);
        }
            
    }
}
