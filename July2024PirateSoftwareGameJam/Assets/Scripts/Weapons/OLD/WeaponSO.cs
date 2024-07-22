using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    Unspecified,
    Bullet,
    Laser,
    Missile
}

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/WeaponScriptableObject")]
public class WeaponSO : ScriptableObject
{
    public Sprite sprite;
    public WeaponTypes weaponType;
    public float damage;
    public float timeBetweenShots;
    public TrailRenderer projectileTrail;
    public Sprite projectileSprite;
    
    public string weaponName()
    {
        return name;
    }
}
