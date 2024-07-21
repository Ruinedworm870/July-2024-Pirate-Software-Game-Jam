using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponSO weaponData;
    private bool isPlayer = false;

    public void SetWeaponData(WeaponSO w)
    {
        weaponData = w;
    }

    public void SetIsPlayer(bool p)
    {
        isPlayer = p;
    }
    
    public void Shoot(int shootType, bool enemy)
    {
        //shootType =
        //0 = all
        //1 = bullets and lasers (Player left click)
        //2 = missiles (Player right click)

        if (weaponData != null)
        {
            if (shootType == 0 || 
            (shootType == 1 && weaponData.weaponType == WeaponTypes.Bullet) || (shootType == 1 && weaponData.weaponType == WeaponTypes.Laser) || 
            (shootType == 2 && weaponData.weaponType == WeaponTypes.Missile))
            {
                Shoot();
            }
        }
    }
    
    public void Shoot()
    {
        //Bypasses type and just shoots

        if(weaponData != null)
        {
            
        }
    }
}
