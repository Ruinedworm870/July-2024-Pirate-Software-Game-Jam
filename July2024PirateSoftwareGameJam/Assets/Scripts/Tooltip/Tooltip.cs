using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IPointerClickHandler
{
    [SerializeField][TextArea(3, 25)] private string text;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipHandler.Instance.SetTooltipText(text);
        TooltipHandler.Instance.SetTooltipActive(true, eventData);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipHandler.Instance.SetTooltipActive(false, eventData);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        TooltipHandler.Instance.MoveTooltip(eventData);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        TooltipHandler.Instance.SetTooltipActive(false, eventData);
    }

    public void SetText(string text)
    {
        this.text = text;
    }
}
