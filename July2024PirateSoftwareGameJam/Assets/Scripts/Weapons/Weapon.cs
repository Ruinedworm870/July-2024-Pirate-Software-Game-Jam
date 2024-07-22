using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage;

    public float fireRate; //Shots per second
    private float fireRateCounter = 0;

    public Transform firingPoint;
    public ParticleSystem projectile;
    
    private void Awake()
    {
        projectile.GetComponent<ProjectileCollisionHandler>().SetDamage(damage);
    }

    public void Shoot(Vector3 characterVelocity)
    {
        if (Time.time >= fireRateCounter)
        {
            projectile.transform.position = firingPoint.position + characterVelocity * 0.0015f;
            
            projectile.Emit(1);

            fireRateCounter = Time.time + 1f / fireRate;
        }
    }
}
