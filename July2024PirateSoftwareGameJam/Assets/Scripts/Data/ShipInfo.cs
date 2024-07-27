using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInfo
{
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

        WeaponIds:
        -1 = Hull
        -2 = Shield
        -3 = Collector
        0 = EMPTY
        1 = Laser
        2 = Missile
    */
    private (int weaponId, int lvl)[] weaponInfo = new (int weaponId, int lvl)[10];
    
    public ShipInfo()
    {
        weaponInfo[0].weaponId = 1;
        weaponInfo[7].weaponId = -1;
        weaponInfo[8].weaponId = -2;
        weaponInfo[9].weaponId = -3;

        weaponInfo[0].lvl = 1;
        weaponInfo[7].lvl = 1;
        weaponInfo[8].lvl = 1;
        weaponInfo[9].lvl = 1;
    }

    public void SetWeaponId(int slot, int id)
    {
        if(id >= 0)
        {
            weaponInfo[slot].weaponId = id;
        }
    }

    public void SetWeaponLvl(int slot, int lvl)
    {
        weaponInfo[slot].lvl = lvl;
    }

    public int GetWeaponId(int slot)
    {
        return weaponInfo[slot].weaponId;
    }

    public int GetLvl(int slot)
    {
        return weaponInfo[slot].lvl;
    }
}
