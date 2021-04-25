using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MonsterPartLocator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isMouseOver = false;

    public void OnPointerEnter(PointerEventData pointerEventData) {
        isMouseOver = true;    
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        isMouseOver = false;
    }
}
