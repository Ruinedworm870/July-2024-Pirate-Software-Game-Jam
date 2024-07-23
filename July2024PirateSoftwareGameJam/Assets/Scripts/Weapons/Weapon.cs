using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    Laser,
    Missile
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

    //public float fireRate; //Shots per second
    private float fireRateCounter = 0;

    public Transform firingPoint;
    //public ParticleSystem projectile;
    
    private void Awake()
    {
        //projectile.GetComponent<ProjectileCollisionHandler>().SetDamage(damage);
    }
    
    public void Shoot(Vector3 characterVelocity)
    {
        if (Time.time >= fireRateCounter)
        {
            //projectile.transform.position = firingPoint.position + characterVelocity * 0.0015f;

            //projectile.Emit(1);

            ProjectilePool.Instance.ShootProjectile(firingPoint, damage, range, speed, characterVelocity, isPlayerWeapon, weaponType);

            fireRateCounter = Time.time + 1f / fireRate;
        }
    }
}
