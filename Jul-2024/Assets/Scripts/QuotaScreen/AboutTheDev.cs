using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutTheDev : MonoBehaviour
{
    public Animator controller;

    public void Open()
    {
        controller.SetTrigger("Open");
    }
    
    public void Close()
    {
        controller.SetTrigger("Close");
    }

    public void GitHub()
    {
        Application.OpenURL("https://github.com/Ruinedworm870");
    }

    public void Website()
    {
        Application.OpenURL("https://williamsmolinskijr.com");
    }

    public void PCVersion()
    {
        Application.OpenURL("");
    }
}
