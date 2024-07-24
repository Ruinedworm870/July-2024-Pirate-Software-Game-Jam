using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UISoundController.Instance.PlayClickSound();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        HandleClickable.AddHoveredClickable();
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        HandleClickable.RemoveHoveredClickable();
    }
}
