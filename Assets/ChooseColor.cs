using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChooseColor : MonoBehaviour {

    public GameObject mainButton;

    public void ChooseColorNow(int button)
    {
        if (gameObject.activeSelf)
        {
            Color color = new Color();
            switch (button)
            {
                case 0:
                    color = Color.black;
                    break;
                case 1:
                    //Green
                    color = new Color(0f, 0.3921569f, 0f);
                    break;
                case 2:
                    //Red
                    color = new Color(0.7607843f, 0.09411765f, 0.02745098f);
                    break;
                case 3:
                    color = Color.blue;
                    break;
                case 4:
                    color = Color.cyan;
                    break;
                case 5:
                    //Swamp
                    color = new Color(0.5450981f, 0.8470588f, 0.7764706f);
                    break;
                case 6:
                    //Pink
                    color = new Color(1f, 0.6745098f, 0.7529412f);
                    break;
                case 7:
                    //Brown
                    color = new Color(0.5660378f, 0.2208692f,0f);
                    break;
                case 8:
                    //Gold
                    color = new Color(1f, 0.8431373f, 0f);
                    break;
                case 9:
                    //Grey
                    color = new Color(0.5f, 0.5f, 0.5f);
                    break;
            }

            if (color != Color.black)
            {
                if (GameControl.control.colors.Exists(x => x.kanji.Equals(MainFightingScript.MFS.currentKanjiString)))
                    GameControl.control.colors.Remove(GameControl.control.colors.Find(x => x.kanji.Equals(MainFightingScript.MFS.currentKanjiString)));
                GameControl.control.colors.Add(new ColoredKanji() { kanji = MainFightingScript.MFS.currentKanjiString, color = color });
            }else if (GameControl.control.colors.Exists(x => x.kanji.Equals(MainFightingScript.MFS.currentKanjiString)))
                GameControl.control.colors.Remove(GameControl.control.colors.Find(x => x.kanji.Equals(MainFightingScript.MFS.currentKanjiString)));
            ChangeButtonColor(color);
        }
       
        MinMaximise();
    }

    public void ChangeButtonColor(Color color)
    {
        mainButton.GetComponent<Image>().color = color;
    }

    public void MinMaximise()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
