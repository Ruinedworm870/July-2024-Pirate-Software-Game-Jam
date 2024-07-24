using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
    private float distanceTraveled = 0;
    private Vector3 lastPos;
    private float rotationSpeed = 2f;
    
    public override void OnInit()
    {
        lastPos = transform.position;
        distanceTraveled = 0;
        rb.mass = 1;
        rotationSpeed = 12f;
    }

    private void FixedUpdate()
    {
        if(gameObject.activeSelf)
        {
            if(distanceTraveled == 0)
            {
                rb.AddForce(characterVelocity / 2f, ForceMode2D.Impulse);
            }

            if (rb.mass < speed)
            {
                rb.mass += 0.1f;
            }

            if (isPlayer && rotationSpeed > 2f)
            {
                rotationSpeed -= 0.1f;
            }
            else if(rotationSpeed < 90f)
            {
                rotationSpeed += 0.25f;
            }

            Vector3 direction;

            if (isPlayer)
            {
                //Go towards cursor
                
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                direction = (mousePos - transform.position).normalized;
                
            }
            else
            {
                //Go towards player
                direction = (PlayerPosition.pos - transform.position).normalized;

            }

            direction.z = 0;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            rb.AddForce(transform.up, ForceMode2D.Impulse);

            distanceTraveled += Vector3.Distance(lastPos, transform.position);

            if (distanceTraveled > range)
            {
                ReturnToPool();
            }

            lastPos = transform.position;
        }
    }

    public override void OnReturnToPool()
    {
        ExplosionPool.Instance.PlayExplosion(transform.position, 0.1f);
    }
}
