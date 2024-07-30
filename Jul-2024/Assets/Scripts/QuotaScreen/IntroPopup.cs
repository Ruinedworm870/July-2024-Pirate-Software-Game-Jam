using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPopup : MonoBehaviour
{
    public Animator controller;
    public Animator mainMenuConroller;

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
        mainMenuConroller.SetTrigger("Open");
        controller.SetTrigger("OpenPopup");
    }

    public void ClosePopup()
    {
        controller.SetTrigger("ClosePopup");
    }

    public void CloseMainMenu()
    {
        mainMenuConroller.SetTrigger("Close");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
