using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Loads everything that is immediately displayed (Not in a popup)
//Can be used to refresh info (ex. used some resource, call LoadResourceInfo, and it will refresh that part of the display)
public class LoadMainScreen : MonoBehaviour
{
    public TextMeshProUGUI battlesRemaining;
    public TextMeshProUGUI goldProgressText;
    public Slider quotaSlider;

    public TextMeshProUGUI impureIronAmount;
    public TextMeshProUGUI ironChunkAmount;
    public TextMeshProUGUI pureIronPlateAmount;
    
    //These preserve the already established ship slot indexes (visible in ShipInfo)
    public List<Transform> shipSections = new List<Transform>();
    
    private void Start()
    {
        LoadQuotaInfo();
        LoadResourceInfo();
        LoadShipInfo();
    }
    
    public void LoadQuotaInfo()
    {
        QuotaInfo quotaInfo = DataHandler.Instance.quotaInfo;
        ResourceInfo resourceInfo = DataHandler.Instance.resourceInfo;

        int quotaSize = QuotaScaling.GetQuotaSize(quotaInfo.GetQuotaTier());

        battlesRemaining.text = "Battles Remaining: " + quotaInfo.GetBattlesRemaining();

        quotaSlider.maxValue = quotaSize;
        quotaSlider.value = resourceInfo.GetAmount(0);

        goldProgressText.text = NumberHandler.GetDisplay(resourceInfo.GetAmount(0), 1) + " / " + NumberHandler.GetDisplay(quotaSize, 1);
    } 

    public void LoadResourceInfo()
    {
        ResourceInfo resourceInfo = DataHandler.Instance.resourceInfo;

        impureIronAmount.text = NumberHandler.GetDisplay(resourceInfo.GetAmount(1), 1);
        ironChunkAmount.text = NumberHandler.GetDisplay(resourceInfo.GetAmount(2), 1);
        pureIronPlateAmount.text = NumberHandler.GetDisplay(resourceInfo.GetAmount(3), 1);
    }
    
    public void LoadShipInfo()
    {
        ShipInfo shipInfo = DataHandler.Instance.shipInfo;

        for(int i = 0; i < shipSections.Count; i++)
        {
            Transform section = shipSections[i];

            section.GetChild(0).GetComponent<TextMeshProUGUI>().text = GetSectionName(i) + GetWeaponName(shipInfo.GetWeaponId(i));
            section.GetChild(1).GetComponent<TextMeshProUGUI>().text = NumberHandler.GetDisplay(shipInfo.GetLvl(i), 0);
        }
    }
    
    public string GetWeaponName(int weaponId)
    {
        if(weaponId < 0)
        {
            return "";
        }

        string s = " - ";
        
        if(weaponId == 0)
        {
            return s + "EMPTY";
        }
        else if(weaponId == 1)
        {
            return s + "Laser";
        }
        else
        {
            return s + "Missile";
        }
    }

    public string GetSectionName(int slot)
    {
        switch (slot)
        {
            case 0:
                return "C";

            case 1:
                return "L1";

            case 2:
                return "R1";

            case 3:
                return "L2";

            case 4:
                return "R2";

            case 5:
                return "L3";

            case 6:
                return "R3";

            case 7:
                return "Hull";

            case 8:
                return "Shield";

            case 9:
                return "Collector";
        }
        
        return "";
    }
}
