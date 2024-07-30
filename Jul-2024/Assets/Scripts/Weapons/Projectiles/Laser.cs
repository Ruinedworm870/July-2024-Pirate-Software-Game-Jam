using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Projectile
{
    public TrailRenderer trail;
    
    public override void OnInit()
    {
        trail.Clear();
        trail.emitting = true;    
    }   

    private void FixedUpdate()
    {
        if (gameObject.activeSelf)
        {
            rb.velocity = direction * speed + characterVelocity;
            
            if (Vector3.Distance(startPos, transform.position) > range)
            {
                ReturnToPool();
            }
        }
    }

    public override void OnReturnToPool()
    {
        trail.emitting = false;
    }
}
