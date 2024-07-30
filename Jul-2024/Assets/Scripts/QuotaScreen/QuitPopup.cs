using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitPopup : MonoBehaviour
{
    public Animator controller;

    bool isOpen = false;

    private void Update()
    {
        if(Input.GetKeyDown(Keybinds.GetKeyCode("ESC")))
        {
            Open();
        }
    }

    private void Open()
    {
        if(!isOpen)
        {
            isOpen = true;
            controller.SetTrigger("Open");
        }
        
    }

    public void Close()
    {
        if(isOpen)
        {
            isOpen = false;
            controller.SetTrigger("Close");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
