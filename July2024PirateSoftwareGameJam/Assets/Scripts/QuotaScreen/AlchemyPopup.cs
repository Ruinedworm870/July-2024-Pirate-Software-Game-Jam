using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyPopup : MonoBehaviour
{
    public Animator controller;
    
    /*
        Options:
        1 = Impure Iron
        2 = Iron Chunk
        3 = Pure Iron Plate
    */
    private int openOption;

    public void OpenPopup(int option)
    {
        openOption = option;
        controller.SetTrigger("OpenPopup");
    }

    public void ClosePopup()
    {
        controller.SetTrigger("ClosePopup");
    }
}
