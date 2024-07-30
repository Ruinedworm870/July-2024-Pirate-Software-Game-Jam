using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollisionHandler : MonoBehaviour
{
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
    private float damage;
    
    private void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    public void SetDamage(float d)
    {
        damage = d;
    }

    private void OnParticleCollision(GameObject other)
    {
        //int numCollisions = part.GetCollisionEvents(other, collisionEvents);

        IDamageable damageable = other.GetComponent<IDamageable>();

        if(damageable != null)
        {
            damageable.TakeDamage(damage, 0);
        }
    }
}
