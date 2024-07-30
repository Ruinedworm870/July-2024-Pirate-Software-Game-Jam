using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePopup : MonoBehaviour
{
    public Animator controller;

    public LoadMainScreen loadMainScreen;

    public Sprite impureIron;
    public Sprite ironChunk;
    public Sprite pureIronPlate;

    public GameObject emptySelectButton;
    public GameObject normalSectionStuff;
    public GameObject weaponSectionButtons;
    public GameObject nonWeaponSectionButtons;
    
    public TextMeshProUGUI title;
    public GameObject missileInfoText;
    public TextMeshProUGUI lvl;
    public TextMeshProUGUI damage;
    public TextMeshProUGUI fireRate;
    public TextMeshProUGUI magSize;
    public TextMeshProUGUI reloadSpeed;

    public Image weaponUpgradeMaterialImage;
    public TextMeshProUGUI weaponUpgradeAmount;

    public Image nonWeaponUpgradeMaterialImage;
    public TextMeshProUGUI nonWeaponUpgradeAmount;

    public Image laserReplaceMaterial;
    public TextMeshProUGUI laserReplaceAmount;

    public Image missileReplaceMaterial;
    public TextMeshProUGUI missileReplaceAmount;
    
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
    private int openSlot;
    private bool replaceOpen = false;
    private bool isOpen = false;
    
    public void OpenUpgradePopup(int slot)
    {
        if(!isOpen)
        {
            isOpen = true;
            openSlot = slot;
            SetupMenu();
            controller.SetTrigger("OpenPopup");
        }
    }
    
    public void CloseUpgradePopup()
    {
        if(isOpen)
        {
            isOpen = false;

            if (replaceOpen)
            {
                controller.SetTrigger("ForceCloseReplace");
            }

            controller.SetTrigger("ClosePopup");
            replaceOpen = false;
        }
    }
    
    public void OpenReplace()
    {
        if(!replaceOpen)
        {
            replaceOpen = true;
            SetupReplace();
            controller.SetTrigger("OpenReplace");
        }
    }
    
    public void CloseReplace()
    {
        replaceOpen = false;
        controller.SetTrigger("CloseReplace");
    }
    
    private void SetupReplace()
    {
        if(DataHandler.Instance.shipInfo.GetWeaponId(openSlot) == 0)
        {
            //Empty, scaled cost increase

            (int id, int amount) = WeaponScaling.GetReplaceCostOfEmpty(DataHandler.Instance.shipInfo.GetEmptySlotsFilled());

            laserReplaceMaterial.sprite = GetMaterialSprite(id);
            laserReplaceMaterial.GetComponent<Tooltip>().SetText(GetMaterialName(id));
            laserReplaceAmount.text = "-" + amount;

            missileReplaceMaterial.sprite = GetMaterialSprite(id);
            missileReplaceMaterial.GetComponent<Tooltip>().SetText(GetMaterialName(id));
            missileReplaceAmount.text = "-" + amount;
        }
        else
        {
            //Not empty, base replace cost
            
            int amount = WeaponScaling.GetReplaceCostOfAnything();
            
            laserReplaceMaterial.sprite = impureIron;
            laserReplaceMaterial.GetComponent<Tooltip>().SetText(GetMaterialName(1));
            laserReplaceAmount.text = "-" + amount;
            
            missileReplaceMaterial.sprite = impureIron;
            missileReplaceMaterial.GetComponent<Tooltip>().SetText(GetMaterialName(1));
            missileReplaceAmount.text = "-" + amount;
        }
    }
    
    //For on initial setup / replace
    public void SetupMenu()
    {
        ShipInfo shipInfo = DataHandler.Instance.shipInfo;

        int weaponId = shipInfo.GetWeaponId(openSlot);
        title.text = loadMainScreen.GetSectionName(openSlot) + loadMainScreen.GetWeaponName(weaponId);

        emptySelectButton.SetActive(false);
        normalSectionStuff.SetActive(false);
        weaponSectionButtons.SetActive(false);
        nonWeaponSectionButtons.SetActive(false);

        missileInfoText.SetActive(weaponId == 2);
        
        if (weaponId == 0)
        {
            //empty
            
            emptySelectButton.SetActive(true);
        }
        else if(weaponId < 0)
        {
            //non weapon

            normalSectionStuff.SetActive(true);
            nonWeaponSectionButtons.SetActive(true);

            int l = shipInfo.GetLvl(openSlot);
            lvl.text = "Lvl. " + l;
            
            (int id, int amount) = WeaponScaling.GetUpgradeCost(l);
            nonWeaponUpgradeMaterialImage.sprite = GetMaterialSprite(id);
            nonWeaponUpgradeMaterialImage.GetComponent<Tooltip>().SetText(GetMaterialName(id));
            nonWeaponUpgradeAmount.text = "-" + NumberHandler.GetDisplay(amount, 1);

            if (weaponId == -1)
            {
                //Hull
                damage.text = "Hull Strength: " + NumberHandler.GetDisplay(WeaponScaling.GetHullStrength(l), 1);
                fireRate.text = "";
                magSize.text = "";
                reloadSpeed.text = "";
            }
            else if(weaponId == -2)
            {
                //Shield
                damage.text = "Shield Strength: " + NumberHandler.GetDisplay(WeaponScaling.GetShieldStrength(l), 1);
                fireRate.text = "Shield Regen: " + (WeaponScaling.GetShieldRegen(l) * 100f) + "% / min";
                magSize.text = "";
                reloadSpeed.text = "";
            }
            else
            {
                //Collector
                damage.text = "Recharge Drop Chance: " + (WeaponScaling.GetRechargeDropChance(l) * 100f) + "%";
                fireRate.text = "Recharge Heal Amount: " + (WeaponScaling.GetRechargeDropHeal(l) * 100f) + "%";
                magSize.text = "";
                reloadSpeed.text = "";
            }
        }
        else
        {
            //weapon
            
            normalSectionStuff.SetActive(true);
            weaponSectionButtons.SetActive(true);

            int l = shipInfo.GetLvl(openSlot);
            lvl.text = "Lvl. " + l;
            
            (int id, int amount) = WeaponScaling.GetUpgradeCost(l);
            weaponUpgradeMaterialImage.sprite = GetMaterialSprite(id);
            weaponUpgradeMaterialImage.GetComponent<Tooltip>().SetText(GetMaterialName(id));
            weaponUpgradeAmount.text = "-" + NumberHandler.GetDisplay(amount, 1);
            
            if (weaponId == 1)
            {
                //Laser

                damage.text = "Damage: " + WeaponScaling.GetLaserDamage(l);
                fireRate.text = "Fire Rate: " + WeaponScaling.GetLaserFireRate(l);
                magSize.text = "Mag Size: " + WeaponScaling.GetLaserMagSize(l);
                reloadSpeed.text = "Reload Speed: " + WeaponScaling.GetLaserReloadSpeed(l) + "s";
            }
            else
            {
                //Missile

                damage.text = "Damage: " + WeaponScaling.GetMissileDamage(l);
                fireRate.text = "Fire Rate: " + WeaponScaling.GetMissileFireRate(l);
                magSize.text = "Mag Size: " + WeaponScaling.GetMissileMagSize(l);
                reloadSpeed.text = "Reload Speed: " + WeaponScaling.GetMissileReloadSpeed(l) + "s";
            }
        }

        lvl.color = Color.white;
        damage.color = Color.white;
        fireRate.color = Color.white;
        magSize.color = Color.white;
        reloadSpeed.color = Color.white;
    }
    
    //Wehn hovering the upgrade button
    public void HoverUpgrade()
    {
        int weaponId = DataHandler.Instance.shipInfo.GetWeaponId(openSlot);

        int currentLvl = DataHandler.Instance.shipInfo.GetLvl(openSlot);
        
        if(WeaponScaling.IsMaxLevel(currentLvl))
        {
            return;
        }

        lvl.text = "Lvl. " + (currentLvl + 1);
        lvl.color = Color.green;
        
        if(weaponId < 0)
        {
            (int id, int amount) = WeaponScaling.GetUpgradeCost(currentLvl);
            nonWeaponUpgradeMaterialImage.sprite = GetMaterialSprite(id);
            nonWeaponUpgradeMaterialImage.GetComponent<Tooltip>().SetText(GetMaterialName(id));
            nonWeaponUpgradeAmount.text = "-" + NumberHandler.GetDisplay(amount, 1);

            if (weaponId == -1)
            {
                //Hull
                int cStrength = WeaponScaling.GetHullStrength(currentLvl);
                int uStrength = WeaponScaling.GetHullStrength(currentLvl + 1);

                damage.text = "Hull Strength: " + NumberHandler.GetDisplay(uStrength, 1);
                fireRate.text = "";
                magSize.text = "";
                reloadSpeed.text = "";

                HandleTextColor(damage, cStrength, uStrength);
            }
            else if(weaponId == -2)
            {
                //Shield
                int cStrength = WeaponScaling.GetShieldStrength(currentLvl);
                int uStrength = WeaponScaling.GetShieldStrength(currentLvl + 1);

                float cRegen = WeaponScaling.GetShieldRegen(currentLvl);
                float uRegen = WeaponScaling.GetShieldRegen(currentLvl + 1);

                damage.text = "Shield Strength: " + NumberHandler.GetDisplay(uStrength, 1);
                fireRate.text = "Shield Regen: " + (uRegen * 100f) + "% / min";
                magSize.text = "";
                reloadSpeed.text = "";

                HandleTextColor(damage, cStrength, uStrength);
                HandleTextColor(fireRate, cRegen, uRegen);
            }
            else
            {
                //Collector
                float cDrop = WeaponScaling.GetRechargeDropChance(currentLvl);
                float uDrop = WeaponScaling.GetRechargeDropChance(currentLvl + 1);

                float cHeal = WeaponScaling.GetRechargeDropHeal(currentLvl);
                float uHeal = WeaponScaling.GetRechargeDropHeal(currentLvl + 1);

                damage.text = "Recharge Drop Chance: " + (uDrop * 100f) + "%";
                fireRate.text = "Recharge Heal Amount: " + (uHeal * 100f) + "%";
                magSize.text = "";
                reloadSpeed.text = "";

                HandleTextColor(damage, cDrop, uDrop);
                HandleTextColor(fireRate, cHeal, uHeal);
            }
        }
        else
        {
            (int id, int amount) = WeaponScaling.GetUpgradeCost(currentLvl);
            weaponUpgradeMaterialImage.sprite = GetMaterialSprite(id);
            weaponUpgradeMaterialImage.GetComponent<Tooltip>().SetText(GetMaterialName(id));
            weaponUpgradeAmount.text = "-" + NumberHandler.GetDisplay(amount, 1);

            if (weaponId == 1)
            {
                //Laser
                int cDamage = WeaponScaling.GetLaserDamage(currentLvl);
                int uDamage = WeaponScaling.GetLaserDamage(currentLvl + 1);

                int cFireRate = WeaponScaling.GetLaserFireRate(currentLvl);
                int uFireRate = WeaponScaling.GetLaserFireRate(currentLvl + 1);

                int cMagSize = WeaponScaling.GetLaserMagSize(currentLvl);
                int uMagSize = WeaponScaling.GetLaserMagSize(currentLvl + 1);

                float cReloadSpeed = WeaponScaling.GetLaserReloadSpeed(currentLvl);
                float uReloadSpeed = WeaponScaling.GetLaserReloadSpeed(currentLvl + 1);

                damage.text = "Damage: " + uDamage;
                fireRate.text = "Fire Rate: " + uFireRate;
                magSize.text = "Mag Size: " + uMagSize;
                reloadSpeed.text = "Reload Speed: " + uReloadSpeed + "s";

                HandleTextColor(damage, cDamage, uDamage);
                HandleTextColor(fireRate, cFireRate, uFireRate);
                HandleTextColor(magSize, cMagSize, uMagSize);
                HandleTextColor(reloadSpeed, cReloadSpeed, uReloadSpeed);
            }
            else
            {
                //Missile
                int cDamage = WeaponScaling.GetMissileDamage(currentLvl);
                int uDamage = WeaponScaling.GetMissileDamage(currentLvl + 1);

                int cFireRate = WeaponScaling.GetMissileFireRate(currentLvl);
                int uFireRate = WeaponScaling.GetMissileFireRate(currentLvl + 1);

                int cMagSize = WeaponScaling.GetMissileMagSize(currentLvl);
                int uMagSize = WeaponScaling.GetMissileMagSize(currentLvl + 1);

                float cReloadSpeed = WeaponScaling.GetMissileReloadSpeed(currentLvl);
                float uReloadSpeed = WeaponScaling.GetMissileReloadSpeed(currentLvl + 1);

                damage.text = "Damage: " + uDamage;
                fireRate.text = "Fire Rate: " + uFireRate;
                magSize.text = "Mag Size: " + uMagSize;
                reloadSpeed.text = "Reload Speed: " + uReloadSpeed + "s";

                HandleTextColor(damage, cDamage, uDamage);
                HandleTextColor(fireRate, cFireRate, uFireRate);
                HandleTextColor(magSize, cMagSize, uMagSize);
                HandleTextColor(reloadSpeed, cReloadSpeed, uReloadSpeed);
            }
        }
    }
    
    private void HandleTextColor(TextMeshProUGUI text, float currentValue, float newValue)
    {
        if(newValue != currentValue)
        {
            text.color = Color.green;
        }
        else
        {
            text.color = Color.white;
        }
    }
    
    //When cursor moves off the upgrade button, or when upgrade is clicked (will then re-call HoverUpgrade())
    public void EndHoverUpgrade()
    {
        EndHoverUpgrade(false);
    }

    public void EndHoverUpgrade(bool fromUpgrade)
    {
        lvl.color = Color.white;
        damage.color = Color.white;
        fireRate.color = Color.white;
        magSize.color = Color.white;
        reloadSpeed.color = Color.white;

        if (fromUpgrade)
        {
            HoverUpgrade();
        }
        else
        {
            int weaponId = DataHandler.Instance.shipInfo.GetWeaponId(openSlot);
            int l = DataHandler.Instance.shipInfo.GetLvl(openSlot);
            lvl.text = "Lvl. " + l;

            if (weaponId < 0)
            {
                //non weapon

                (int id, int amount) = WeaponScaling.GetUpgradeCost(l);
                nonWeaponUpgradeMaterialImage.sprite = GetMaterialSprite(id);
                nonWeaponUpgradeMaterialImage.GetComponent<Tooltip>().SetText(GetMaterialName(id));
                nonWeaponUpgradeAmount.text = "-" + NumberHandler.GetDisplay(amount, 1);

                if (weaponId == -1)
                {
                    //Hull
                    damage.text = "Hull Strength: " + NumberHandler.GetDisplay(WeaponScaling.GetHullStrength(l), 1);
                    fireRate.text = "";
                    magSize.text = "";
                    reloadSpeed.text = "";
                }
                else if (weaponId == -2)
                {
                    //Shield
                    damage.text = "Shield Strength: " + NumberHandler.GetDisplay(WeaponScaling.GetShieldStrength(l), 1);
                    fireRate.text = "Shield Regen: " + (WeaponScaling.GetShieldRegen(l) * 100f) + "% / min";
                    magSize.text = "";
                    reloadSpeed.text = "";
                }
                else
                {
                    //Collector
                    damage.text = "Recharge Drop Chance: " + (WeaponScaling.GetRechargeDropChance(l) * 100f) + "%";
                    fireRate.text = "Recharge Heal Amount: " + (WeaponScaling.GetRechargeDropHeal(l) * 100f) + "%";
                }
            }
            else
            {
                //weapon
                
                (int id, int amount) = WeaponScaling.GetUpgradeCost(l);
                weaponUpgradeMaterialImage.sprite = GetMaterialSprite(id);
                weaponUpgradeMaterialImage.GetComponent<Tooltip>().SetText(GetMaterialName(id));
                weaponUpgradeAmount.text = "-" + NumberHandler.GetDisplay(amount, 1);

                if (weaponId == 1)
                {
                    //Laser

                    damage.text = "Damage: " + WeaponScaling.GetLaserDamage(l);
                    fireRate.text = "Fire Rate: " + WeaponScaling.GetLaserFireRate(l);
                    magSize.text = "Mag Size: " + WeaponScaling.GetLaserMagSize(l);
                    reloadSpeed.text = "Reload Speed: " + WeaponScaling.GetLaserReloadSpeed(l) + "s";
                }
                else
                {
                    //Missile

                    damage.text = "Damage: " + WeaponScaling.GetMissileDamage(l);
                    fireRate.text = "Fire Rate: " + WeaponScaling.GetMissileFireRate(l);
                    magSize.text = "Mag Size: " + WeaponScaling.GetMissileMagSize(l);
                    reloadSpeed.text = "Reload Speed: " + WeaponScaling.GetMissileReloadSpeed(l) + "s";
                }
            }
        }
    }

    public void Upgrade()
    {
        int l = DataHandler.Instance.shipInfo.GetLvl(openSlot);
        (int id, int amount) = WeaponScaling.GetUpgradeCost(l);

        if(DataHandler.Instance.resourceInfo.GetAmount(id) >= amount)
        {
            int newAmount = DataHandler.Instance.resourceInfo.GetAmount(id) - amount;
            DataHandler.Instance.resourceInfo.SetAmount(id, newAmount);
            DataHandler.Instance.shipInfo.SetWeaponLvl(openSlot, l + 1);
            loadMainScreen.LoadResourceInfo();
            loadMainScreen.LoadShipInfo();
            EndHoverUpgrade(true);
        }
    }
    
    public void SelectReplacement(int id)
    {
        if(id == DataHandler.Instance.shipInfo.GetWeaponId(openSlot))
        {
            CloseReplace();
            return;
        }

        if(DataHandler.Instance.shipInfo.GetWeaponId(openSlot) == 0)
        {
            //From empty

            (int i, int amount) = WeaponScaling.GetReplaceCostOfEmpty(DataHandler.Instance.shipInfo.GetEmptySlotsFilled());

            int newAmount = DataHandler.Instance.resourceInfo.GetAmount(i) - amount;

            if(newAmount >= 0)
            {
                DataHandler.Instance.resourceInfo.SetAmount(i, newAmount);
            }
            else
            {
                return;
            }
        }
        else
        {
            //From other weapon

            int newAmount = DataHandler.Instance.resourceInfo.GetAmount(1) - WeaponScaling.GetReplaceCostOfAnything();

            if (newAmount >= 0)
            {
                DataHandler.Instance.resourceInfo.SetAmount(1, newAmount);
            }
            else
            {
                return;
            }
        }

        DataHandler.Instance.shipInfo.SetWeaponId(openSlot, id);
        
        if(DataHandler.Instance.shipInfo.GetLvl(openSlot) == 0)
        {
            DataHandler.Instance.shipInfo.SetWeaponLvl(openSlot, 1);
        }

        CloseReplace();
        loadMainScreen.LoadResourceInfo();
        loadMainScreen.LoadShipInfo();
        SetupMenu();
    }

    private Sprite GetMaterialSprite(int option)
    {
        switch (option)
        {
            case 1:
                return impureIron;

            case 2:
                return ironChunk;

            case 3:
                return pureIronPlate;
        }

        return null;
    }

    private string GetMaterialName(int option)
    {
        switch (option)
        {
            case 1:
                return "Impure Iron";

            case 2:
                return "Iron Chunk";

            case 3:
                return "Pure Iron Plate";
        }

        return null;
    }
}
