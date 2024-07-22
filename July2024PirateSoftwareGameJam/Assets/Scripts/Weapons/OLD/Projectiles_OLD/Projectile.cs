using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public TrailRenderer trail;

    private float damage;
    private Vector3 direction;
    private float range;
    private Vector3 startPos;
    private Rigidbody2D rb;
    private Collider2D c;
    private float speed;
    
    public void Init(Transform pos, float damage, float range, float speed, Vector3 characterVelocity, LayerMask targetLayer, LayerMask sender)
    {
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();

        c.includeLayers = targetLayer;
        c.excludeLayers = sender;
        this.damage = damage;
        transform.position = pos.position + characterVelocity * 0.015f;
        transform.rotation = pos.rotation;
        direction = transform.up;
        this.range = range;
        this.speed = speed;
        startPos = transform.position;
        gameObject.SetActive(true);
        trail.Clear();
        trail.emitting = true;
    }

    private void FixedUpdate()
    {
        if(gameObject.activeSelf)
        {
            rb.velocity = direction * speed;

            if(Vector3.Distance(startPos, transform.position) > range)
            {
                ReturnToPool();
            }
        }
    }

    private void ReturnToPool()
    {
        trail.emitting = false;
        gameObject.SetActive(false);

        ProjectilePool.Instance.ReturnProjectile(gameObject);
        /*
            Type:
                0 = Player Bullet
                1 = Player Laser
                2 = Player Missile
                3 = Enemy Bullet
                4 = Enemy Laser
                5 = Enemy Missile
        */
        
        /*switch(type)
        {
            case 0:
                ProjectilePool.Instance.ReturnPlayerBullet(gameObject);
                break;
            
            case 1:
                
                break;
            
            case 2:
                
                break;
            
            case 3:
                
                break;
            
            case 4:
                
                break;
            
            case 5:
                
                break;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if(damageable != null)
        {
            damageable.TakeDamage(damage);
        }
        
        ReturnToPool();
    }
}
