using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class KanjiMouseDown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler  {
    public bool Inside = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Inside = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Inside = false;
    }

    
}
