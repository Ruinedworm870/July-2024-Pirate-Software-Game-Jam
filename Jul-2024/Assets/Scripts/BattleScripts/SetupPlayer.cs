using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupPlayer : MonoBehaviour
{
    public GameObject player;
    public AudioClip laserSound;
    public AudioClip missileShootSound;

    private void Start()
    {
        HandleSetupPlayer();    
    }
    
    private void HandleSetupPlayer()
    {
        ShipInfo shipInfo = DataHandler.Instance.shipInfo;

        PlayerController p = player.GetComponent<PlayerController>();
        Transform weapons = player.transform.Find("Weapons");

        if(p.weapons == null)
        {
            p.weapons = new List<Weapon>();
        }
        else
        {
            p.weapons.Clear();
        }
        
        float hull = WeaponScaling.GetHullStrength(shipInfo.GetLvl(7));
        float shield = WeaponScaling.GetShieldStrength(shipInfo.GetLvl(8));
        float shieldRegen = WeaponScaling.GetShieldRegen(shipInfo.GetLvl(8));

        p.health = hull;
        p.shield = shield;
        p.regenPerMin = shieldRegen;

       for(int i = 0; i < shipInfo.GetWeaponInfo().Length; i++)
        {
            //Avoids non weapons
            if(i > 6)
            {
                continue;
            }

            int weaponId = shipInfo.GetWeaponId(i);
            int lvl = shipInfo.GetLvl(i);

            Weapon weapon = weapons.GetChild(i).GetComponent<Weapon>();
            
            if(weaponId == 1 || weaponId == 2)
            {
                float damage;
                float fireRate;
                float range; //laser range is 50, missile range is 50 (100 on enemy, 50 on player)
                float speed; //laser speed is 30, and missile is 7, they don't change based on level
                int ammo;
                float reloadSpeed;

                if (weaponId == 2)
                {
                    //Missile
                    
                    damage = WeaponScaling.GetMissileDamage(lvl);
                    fireRate = WeaponScaling.GetMissileFireRate(lvl);
                    range = 50f;
                    speed = 7f;
                    ammo = WeaponScaling.GetMissileMagSize(lvl);
                    reloadSpeed = WeaponScaling.GetMissileReloadSpeed(lvl);
                    
                    weapon.weaponType = WeaponTypes.Missile;
                    weapon.shotSound = missileShootSound;
                }
                else
                {
                    //Laser
                    
                    damage = WeaponScaling.GetLaserDamage(lvl);
                    fireRate = WeaponScaling.GetLaserFireRate(lvl);
                    range = 50f;
                    speed = 30f;
                    ammo = WeaponScaling.GetLaserMagSize(lvl);
                    reloadSpeed = WeaponScaling.GetLaserReloadSpeed(lvl);

                    weapon.weaponType = WeaponTypes.Laser;
                    weapon.shotSound = laserSound;
                }

                weapon.damage = damage;
                weapon.fireRate = fireRate;
                weapon.range = range;
                weapon.speed = speed;
                weapon.ammo = ammo;
                weapon.startAmmo = ammo;
                weapon.reloadTime = reloadSpeed;
                weapon.Reset();

                p.weapons.Add(weapon);
            }
        }
    }
    
    public void OnNewWave()
    {
        PlayerController p = player.GetComponent<PlayerController>();
        p.shield = p.startShield;
        player.transform.position = Vector3.zero;
        ForceReloadWeapons();
    }

    private void ForceReloadWeapons()
    {
        foreach(var i in player.GetComponent<PlayerController>().weapons)
        {
            i.Reset();
        }
    }

    public void OnDeath()
    {
        player.SetActive(false);
    }
}
