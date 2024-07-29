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
    public int startAmmo;
    public float reloadTime;
    public AudioClip shotSound;
    public float volume;
    private AudioSource source;

    private float fireRateCounter = 0;

    public Transform firingPoint;
    
    private bool reloading;
    private float reloadTimeRemaining;
    
    public void Reset()
    {
        reloadTimeRemaining = 0;
        reloading = false;
        fireRateCounter = 0;
        ammo = startAmmo;
    }

    //Returns true if shot (used to only shoot 1 missile at a time)
    public bool Shoot(Vector3 characterVelocity, WeaponTypes weaponType, bool playSound = true, bool varyShootDelay = true)
    {
        if (!reloading && (this.weaponType == weaponType || weaponType == WeaponTypes.AllTypes) && Time.time >= fireRateCounter)
        {
            if(shotSound != null && playSound)
            {
                if(source == null)
                {
                    source = SoundSourcePool.Instance.GetAudioSource();
                }
                
                SoundManager.Instance.PlayOneShotSound(source, shotSound, transform.position, 0, volume);
            }

            ProjectilePool.Instance.ShootProjectile(firingPoint, damage, range, speed, characterVelocity, isPlayerWeapon, this.weaponType);

            float t = 1f / fireRate;

            if(varyShootDelay)
            {
                t *= Random.Range(1f, 1.075f);
            }

            fireRateCounter = Time.time + t;//(1f / fireRate) * Random.Range(1f, 1.05f);

            ammo -= 1;
            
            if(ammo <= 0)
            {
                reloading = true;
                StartCoroutine(HandleReloading());
            }

            return true;
        }

        return false;
    }
    
    public float GetReloadTimeRemaining()
    {
        return reloadTimeRemaining;
    }
    
    private IEnumerator HandleReloading()
    {
        reloadTimeRemaining = reloadTime;
        
        while(reloadTimeRemaining > 0)
        {
            yield return new WaitForSeconds(0.02f);
            reloadTimeRemaining -= 0.02f;
        }
        
        ammo = startAmmo;
        reloading = false;
    }
}
