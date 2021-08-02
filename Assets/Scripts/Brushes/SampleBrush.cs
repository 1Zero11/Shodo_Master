using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SampleBrush : MonoBehaviour {

    public Button buttonComponent;
    public Text nameLabel;

    private BrushScrollList scrollList;

    private int number;
    //private ShopScrollList scrollList;

    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
    }

    public void Setup(Image brush, string name, BrushScrollList currentScrollList, int Number)
    {
        nameLabel.text = name;
        scrollList = currentScrollList;
        number = Number;

    }

    public void HandleClick()
    {
        scrollList.ChooseItem(number);
    }
}
