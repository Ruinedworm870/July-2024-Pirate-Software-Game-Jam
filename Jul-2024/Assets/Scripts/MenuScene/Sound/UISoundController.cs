using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundController : MonoBehaviour
{
    public static UISoundController Instance;

    public AudioSource source;
    public AudioClip clickSound;
    public AudioClip pickupSound;

    private void Awake()
    {
        Instance = this;
    }
    
    public void PlayClickSound()
    {
        source.PlayOneShot(clickSound, 2.25f * DataHandler.Instance.uiAudio);
    }

    public void PlayPickupSound()
    {
        source.PlayOneShot(pickupSound, 0.75f * DataHandler.Instance.uiAudio);
    }
}