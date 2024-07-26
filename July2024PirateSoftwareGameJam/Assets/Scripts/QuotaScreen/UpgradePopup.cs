using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePopup : MonoBehaviour
{
    public Animator controller;
    
    /*
        Slots:
        0 = C
        1 = L1
        2 = R1
        3 = L2
        4 = R2
        5 = L3
        6 = R3
        7 = Hull
        8 = Shield
        9 = Collector
    */
    private int openSlot = -1;
    private bool replaceOpen = false;
    
    public void OpenUpgradePopup(int slot)
    {
        openSlot = slot;
        controller.SetTrigger("OpenPopup");
    }
    
    public void CloseUpgradePopup()
    {
        if (replaceOpen)
        {
            controller.SetTrigger("ForceCloseReplace");
        }

        controller.SetTrigger("ClosePopup");
        replaceOpen = false;
    }
    
    public void OpenReplace()
    {
        if(!replaceOpen)
        {
            replaceOpen = true;
            controller.SetTrigger("OpenReplace");
        }
    }
    
    public void CloseReplace()
    {
        replaceOpen = false;
        controller.SetTrigger("CloseReplace");
    }
}
