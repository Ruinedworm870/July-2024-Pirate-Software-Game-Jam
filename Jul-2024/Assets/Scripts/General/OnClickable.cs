using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

//Same as clickable, but will call methods when it is detected as clickable
public class OnClickable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEvent onHoverActions;
    public UnityEvent onEndHoverActions;

    public void OnPointerClick(PointerEventData eventData)
    {
        UISoundController.Instance.PlayClickSound();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HandleClickable.AddHoveredClickable();
        onHoverActions.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HandleClickable.RemoveHoveredClickable();
        onEndHoverActions.Invoke();
    }
}
