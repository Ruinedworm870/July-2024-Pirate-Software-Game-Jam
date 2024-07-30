using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleSettings : MonoBehaviour
{
    public Animator controller;
    public AudioSource backgroundSource;

    public Slider background;
    public Slider ui;
    public Slider weapons;

    private bool open;

    private float defaultBackgroundVolume;

    private void Start()
    {
        defaultBackgroundVolume = backgroundSource.volume;

        background.value = DataHandler.Instance.backgroundAudio;
        ui.value = DataHandler.Instance.uiAudio;
        weapons.value = DataHandler.Instance.weaponAudio;
    }

    public void Open()
    {
        if(!open)
        {
            controller.SetTrigger("Open");
            open = true;
        }
    }
    
    public void Close()
    {
        if(open)
        {
            controller.SetTrigger("Close");
            open = false;
        }
    }

    public void ChangeBackgroundSlider(float v)
    {
        DataHandler.Instance.backgroundAudio = v;
        backgroundSource.volume = defaultBackgroundVolume * v;
    }

    public void ChangeUISlider(float v)
    {
        DataHandler.Instance.uiAudio = v;
    }
    
    public void ChangeWeaponSlider(float v)
    {
        DataHandler.Instance.weaponAudio = v;
    }
}
