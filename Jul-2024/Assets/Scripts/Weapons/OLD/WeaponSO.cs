using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponScriptableObject")]
public class WeaponSO : ScriptableObject
{
    public float damage;
    public float fireRate; //Shots per second
    public float range;
    public float speed;
    public bool isPlayerWeapon;
    public WeaponTypes weaponType;
    
    public string weaponName()
    {
        return name;
    }
}
