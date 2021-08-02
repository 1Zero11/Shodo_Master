using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushScrollList : MonoBehaviour {

    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;
    public Objective chosenItem;
    public List<BrushbuttonInfo> brushButtonList = new List<BrushbuttonInfo>();

    private bool chosen = false;
    
    public class BrushbuttonInfo
    {
        public string name = "";
        public Image image;
    }


    // Use this for initialization
    void Start()
    {
        //brushButtonList = new List<Brushbutton>();
        BrushbuttonInfo s = new BrushbuttonInfo();
        s.name = "Ultimate Brush";
        brushButtonList.Add(s);
        //RefreshDisplay();

    }

    public void RefreshDisplay()
    {

        RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    private void AddButtons()
    {
        for (int i = 0; i < brushButtonList.Count; i++)
        {
            BrushbuttonInfo item = brushButtonList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            SampleBrush sampleBrush = newButton.GetComponent<SampleBrush>();
            sampleBrush.Setup(item.image, item.name, this, i);
        }
    }


    void AddItem(BrushbuttonInfo itemToAdd, BrushScrollList shopList)
    {
        shopList.brushButtonList.Add(itemToAdd);
    }

    private void RemoveItem(BrushbuttonInfo itemToRemove, BrushScrollList shopList)
    {
        for (int i = shopList.brushButtonList.Count - 1; i >= 0; i--)
        {
            if (shopList.brushButtonList[i] == itemToRemove)
            {
                shopList.brushButtonList.RemoveAt(i);
            }
        }
    }

    public void Delete(BrushbuttonInfo item)
    {
        RemoveItem(item, this);
        RefreshDisplay();
    }

    public void ChooseItem(int number)
    {
        chosen = true;
        Debug.Log("Chosen " + number);

    }

    
}
