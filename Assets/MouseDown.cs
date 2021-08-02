using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MouseDown : MonoBehaviour, IPointerDownHandler
{
    public NavigationMainScript script;

    public void OnPointerDown(PointerEventData eventData)
    {
        script.ButtonPressed(0);
    }
}
