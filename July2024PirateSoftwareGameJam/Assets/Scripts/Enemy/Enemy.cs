using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public float health = 100f;
    public List<Weapon> weapons = new List<Weapon>();
    public ParticleSystem deathExplosion;
    public GameObject sprite;
    public float inaccuracyDegree = 3.0f;

    private Rigidbody2D rb;

    private float rotationSpeed = 8f;

    private Vector3 targetPosRelToPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosRelToPlayer = new Vector3(Random.Range(-8f, 8f), Random.Range(-8f, 8f), 0);
    }
    
    private void FixedUpdate()
    {
        if(gameObject.activeSelf)
        {
            Vector3 predictedPos = PredictPlayerPos.GetPredictedPos(PlayerPosition.playerRb, transform.position, Random.Range(15f, 35f));//PlayerPosition.pos - transform.position;
            Vector3 direction = predictedPos.normalized;
            
            //Rotating to look at player
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) <= 15f && Vector3.Distance(transform.position, PlayerPosition.pos) < 100f)
            {
                Shoot();
            }

            Vector3 targetPos = PlayerPosition.pos + targetPosRelToPlayer;
            Vector3 directionToTargetPos = targetPos - transform.position;

            if (Vector3.Distance(transform.position, targetPos) > Vector3.Distance(Vector3.zero, targetPosRelToPlayer))
            {
                rb.AddForce(directionToTargetPos.normalized, ForceMode2D.Impulse);
            }
            else
            {
                //Random Force
                rb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized, ForceMode2D.Impulse);
            }
        }
    }
    
    private Vector3 ApplyRandomInaccuracy(Vector3 direction)
    {   
        float randomAngle = Random.Range(-inaccuracyDegree, inaccuracyDegree);

        Quaternion rotation = Quaternion.Euler(0, 0, randomAngle);

        return rotation * direction;
    }

    private void Shoot()
    {
        foreach(var i in weapons)
        {
            i.Shoot(rb.velocity);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0)
        {
            ExplosionPool.Instance.PlayExplosion(transform.position);
            gameObject.SetActive(false);
        }
    }
}
