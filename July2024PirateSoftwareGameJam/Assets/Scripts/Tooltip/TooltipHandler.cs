using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHandler : MonoBehaviour
{
    public static TooltipHandler Instance;

    public GameObject tooltipObject;

    private Vector2 tooltipSize;
    private Vector2 tooltipOffset;
    private float staticOffset = 10f;
    
    private bool active;
    private float time = 0f;
    private float toolTipOpenDelay = 0.5f;

    private void Awake()
    {
        Instance = this;
    }
    
    private void Update()
    {
        if(active)
        {
            time += Time.deltaTime;

            if(time >= toolTipOpenDelay)
            {
                tooltipObject.SetActive(true);
                active = false;
            }
        }
        else
        {
            time = 0f;
        }
    }
    
    public void SetTooltipText(string t)
    {
        TextMeshProUGUI tooltipText = tooltipObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        tooltipText.text = t;
        
        tooltipSize = new Vector2(tooltipText.preferredWidth, tooltipText.preferredHeight);
        tooltipOffset = new Vector2(tooltipSize.x / 2f + staticOffset, tooltipSize.y / 3f);
        
        tooltipObject.GetComponent<RectTransform>().sizeDelta = tooltipSize;
    }
    
    public void SetTooltipActive(bool a, PointerEventData eventData)
    {
        active = a;

        if(!a)
        {
            tooltipObject.SetActive(a);
        }
    }

    public void MoveTooltip(PointerEventData eventData)
    {
        SetTooltipPosition(eventData);
    }

    private void SetTooltipPosition(PointerEventData eventData)
    {
        Vector2 targetPosition = eventData.position;

        if (targetPosition.x + tooltipSize.x + tooltipOffset.x > Screen.width)
        {
            targetPosition.x -= tooltipOffset.x;
        }
        else
        {
            targetPosition.x += tooltipOffset.x;
        }

        if (targetPosition.y + tooltipSize.y + tooltipOffset.y > Screen.height)
        {
            targetPosition.y -= tooltipOffset.y;
        }
        else
        {
            targetPosition.y += tooltipOffset.y;
        }

        tooltipObject.transform.position = targetPosition;
    }
}
