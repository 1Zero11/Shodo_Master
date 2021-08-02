using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SampleButton : MonoBehaviour
{

    public Button buttonComponent;
    public Toggle Accept;
    public Slider ExperienceSlider;
    public Text nameLabel;
    public Text Kanji;
    public Text FullText;
    public Text level;
    public Text KanjiEfficiency;

    bool isSetting = false;

    private bool expanded = false;
    private bool operating = false;
    
    private ShopScrollList scrollList;

    private Objective item;
    private int number;
    private int bestFitNumber = 300;
    //private ShopScrollList scrollList;

    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
        Accept.onValueChanged.AddListener(AcceptVoid);

    }

    public void Setup(Objective currentItem, ShopScrollList currentScrollList, int Number)
    {
        isSetting = true;
        item = currentItem;
        nameLabel.text = item.shortIntroduction;
        string s = "";
        foreach(int i in item.kanji)
        {
            s += GameControl.control.AllKanji[i] + " ";
        }
        Accept.isOn = currentItem.isOn;
        Kanji.text = s;
        FullText.text = currentItem.introduction;
        level.text = "Level " + currentItem.mastery;
        scrollList = currentScrollList;
        number = Number;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 300);
        expanded = false;

        int efficiency = 0; 
        int inexperience = 0;
        foreach(int i in currentItem.kanji)
        {
            if (GameControl.control.probabilities[i] != 0)
            {
                inexperience += GameControl.control.probabilities[i];
                if (GameControl.control.probabilities[i] - 1 == 0)
                    efficiency++;
            }
            else
                inexperience += 31;
        }
        KanjiEfficiency.text = efficiency + "/" + currentItem.kanji.Length;
        ExperienceSlider.maxValue = currentItem.kanji.Length * 31;
        ExperienceSlider.value = currentItem.kanji.Length * 31 - inexperience;
        isSetting = false;
    }

    private IEnumerator SmoothChange(int pixel)
    {

        float speed = 2f;
        operating = true;
        Vector2 old = gameObject.GetComponent<RectTransform>().sizeDelta;
        float time = 0f;
        while (time < 1f)
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(old.x, old.y + pixel*time);
            time += Time.deltaTime*speed;
            yield return null;
        }
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(old.x, old.y + pixel);
        operating = false;
    }

    public void HandleClick()
    {
        
        if (!operating)
        {
            if (!expanded)
            {
                StartCoroutine(SmoothChange(800));
                expanded = true;
            }
            else
            {
                StartCoroutine(SmoothChange(-800));
                expanded = false;
            }
        }

    }

    public void AcceptVoid(bool value)
    {
        //item.isOn = value;
        if(!isSetting)
        scrollList.ChooseItem(number, value);
    }


}