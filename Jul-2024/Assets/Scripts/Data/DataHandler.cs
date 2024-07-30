using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Saves data 
//There is no saving between game sessions for the game jam, so this is just for between scenes
public class DataHandler : MonoBehaviour
{
    public static DataHandler Instance;
    
    public ShipInfo shipInfo = new ShipInfo();
    public ResourceInfo resourceInfo = new ResourceInfo();
    public QuotaInfo quotaInfo = new QuotaInfo();
    public float backgroundAudio = 1f;
    public float uiAudio = 1f;
    public float weaponAudio = 1f;
    
    public bool firstLoad = true; //Set to false in IntroPopup

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
