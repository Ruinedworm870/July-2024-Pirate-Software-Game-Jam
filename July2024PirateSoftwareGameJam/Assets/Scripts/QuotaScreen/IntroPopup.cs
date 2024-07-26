using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPopup : MonoBehaviour
{
    public Animator controller;
    
    private void Start()
    {
        if(DataHandler.Instance.firstLoad)
        {
            OpenPopup();
            DataHandler.Instance.firstLoad = false;
        }
    }

    public void OpenPopup()
    {
        controller.SetTrigger("OpenPopup");
    }

    public void ClosePopup()
    {
        controller.SetTrigger("ClosePopup");
    }
}
