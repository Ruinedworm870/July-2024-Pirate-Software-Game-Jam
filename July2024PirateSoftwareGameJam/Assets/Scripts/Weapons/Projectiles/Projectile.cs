using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float damage;
    protected Vector3 direction;
    protected float range;
    protected Vector3 startPos;
    protected Rigidbody2D rb;
    protected Collider2D c;
    protected float speed; //Mass for missile
    protected Vector3 characterVelocity;

    protected bool isPlayer;
    protected WeaponTypes weaponType;
    
    public void Init(Transform pos, float damage, float range, float speed, Vector3 characterVelocity, LayerMask targetLayer, LayerMask sender, bool isPlayer, WeaponTypes weaponType)
    {
        rb = GetComponent<Rigidbody2D>();
        c = GetComponent<Collider2D>();

        c.includeLayers = targetLayer;
        c.excludeLayers = sender;

        this.characterVelocity = characterVelocity;
        this.damage = damage;
        transform.position = pos.position + characterVelocity * 0.015f;
        transform.rotation = pos.rotation;
        direction = transform.up;
        this.range = range;
        this.speed = speed;
        startPos = transform.position;

        this.isPlayer = isPlayer;
        this.weaponType = weaponType;
        
        gameObject.SetActive(true);
        OnInit();
    }
    
    //For doing extra initialization stuff (starting trail / particle system)
    public abstract void OnInit();
    
    public void ReturnToPool()
    {
        OnReturnToPool();
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);

        ProjectilePool.Instance.ReturnProjectile(gameObject, weaponType, isPlayer);
    }
    
    //For doing extra disabling stuff (explosions, disabling trail)
    public abstract void OnReturnToPool();

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if(damageable != null)
        {
            damageable.TakeDamage(damage, weaponType);
        }
        
        ReturnToPool();
    }
}
