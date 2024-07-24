using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    Laser,
    Missile,
    AllTypes
}

public class Weapon : MonoBehaviour
{
    //public float damage;
    //public WeaponSO weaponData;
    
    //Setting here instead of the ScriptableObject because these can change with upgrades / enemy level
    public float damage;
    public float fireRate; //Shots per second
    public float range;
    public float speed;
    public bool isPlayerWeapon;
    public WeaponTypes weaponType;
    public int ammo;
    public float reloadTime;
    public AudioClip shotSound;
    public float volume;
    private AudioSource source;

    private float fireRateCounter = 0;

    public Transform firingPoint;
    
    //Returns true if shot (used to only shoot 1 missile at a time)
    public bool Shoot(Vector3 characterVelocity, WeaponTypes weaponType)
    {
        if ((this.weaponType == weaponType || weaponType == WeaponTypes.AllTypes) && Time.time >= fireRateCounter)
        {
            if(shotSound != null)
            {
                if(source == null)
                {
                    source = SoundSourcePool.Instance.GetAudioSource();
                }
                
                SoundManager.Instance.PlayOneShotSound(source, shotSound, transform.position, 0, volume);
            }

            ProjectilePool.Instance.ShootProjectile(firingPoint, damage, range, speed, characterVelocity, isPlayerWeapon, this.weaponType);

            fireRateCounter = Time.time + (1f / fireRate) * Random.Range(1f, 1.05f);

            return true;
        }

        return false;
    }
}
