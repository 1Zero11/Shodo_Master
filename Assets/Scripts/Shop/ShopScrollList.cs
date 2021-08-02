using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class ShopScrollList : MonoBehaviour
{

    public List<Objective> itemList;
    public Transform contentPanel;
    public SimpleObjectPool buttonObjectPool;



    // Use this for initialization
    void Start()
    {
        itemList = new List<Objective>();
        foreach(Objective obj in GameControl.control.currentObjectives)
        {
            //Debug.Log("Current objective " + obj.shortIntroduction);
            //Debug.Log(MainFightingScript.MFS.objectives[0].shortIntroduction);
            itemList.Add(obj);
        }
        //RefreshDisplay();
        
    }

    public void RefreshDisplay()
    {
        itemList = new List<Objective>();
        foreach (Objective obj in GameControl.control.currentObjectives)
        {
            Debug.Log("Current objective " + obj.shortIntroduction);
            //Debug.Log(MainFightingScript.MFS.objectives[0].shortIntroduction);
            itemList.Add(obj);
        }

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
        for (int i = 0; i < itemList.Count; i++)
        {
            Objective item = itemList[i];
            //Debug.Log("2Current objective " + item.shortIntroduction + " isOn " + item.isOn);
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);
            
            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(item, this, i);
        }
    }


    void AddItem(Objective itemToAdd, ShopScrollList shopList)
    {
        shopList.itemList.Add(itemToAdd);
    }

    private void RemoveItem(Objective itemToRemove, ShopScrollList shopList)
    {
        for (int i = shopList.itemList.Count - 1; i >= 0; i--)
        {
            if (shopList.itemList[i] == itemToRemove)
            {
                shopList.itemList.RemoveAt(i);
            }
        }
    }

    public void Delete(Objective item)
    {
        RemoveItem(item, this);
        RefreshDisplay();
    }
    
    public void ChooseItem(int number, bool value)
    {
        
        MainFightingScript.MFS.ChangeObjective(number, value);
        
    }

    public void SlotClick(int id)
    {
        
            //GameControl.control.selectedSets[id] = chosenItem.id;
    }

}