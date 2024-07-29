using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IDamageable
{
    public float health = 100f;
    public float shield = 100f;
    public float regenPerMin = 0.1f; //Percentage
    public List<Weapon> weapons = new List<Weapon>();
    public float inaccuracyDegree = 3.0f;

    private Rigidbody2D rb;

    private float rotationSpeed = 8f;

    private Vector3 targetPosRelToPlayer;

    private GameObject healthSlider;
    
    private float startShield;
    private float startHealth;

    //private float startMass;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //startMass = rb.mass;

        targetPosRelToPlayer = new Vector3(Random.Range(-8f, 8f), Random.Range(-8f, 8f), 0);

        startHealth = health;
        startShield = shield;
    }

    public void Reset()
    {
        if(healthSlider == null)
        {
            healthSlider = EnemyHealthSliders.Instance.GetSliderObject();
        }
        
        startHealth = health;
        startShield = shield;
        healthSlider.transform.GetChild(0).GetComponent<Slider>().value = 1;
        healthSlider.transform.GetChild(1).GetComponent<Slider>().value = 1;
    }
    
    private void FixedUpdate()
    {
        if(gameObject.activeSelf)
        {
            /*if(Vector3.Distance(transform.position, PlayerPosition.pos) >= 25f)
            {
                rb.mass = startMass - 1;
            }
            else
            {
                rb.mass = startMass;
            }*/
            
            Vector3 predictedPos = PredictPlayerPos.GetPredictedPos(PlayerPosition.playerRb, transform.position, rb.velocity, Random.Range(15f, 35f));//PlayerPosition.pos - transform.position;
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

            shield = Mathf.Clamp(shield + (regenPerMin * (Time.fixedDeltaTime / 60f)), 0, startShield);
            healthSlider.transform.GetChild(0).GetComponent<Slider>().value = shield / startShield;
        }
    }
    
    private void Update()
    {
        if(gameObject.activeSelf)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            if (Vector3.Distance(mousePos, transform.position) < 2f)
            {
                if (!healthSlider.activeSelf)
                {
                    healthSlider.transform.position = new Vector3(transform.position.x, transform.position.y + 0.6f, 0);
                    healthSlider.SetActive(true);
                }
                else
                {
                    healthSlider.transform.position = Vector3.Lerp(healthSlider.transform.position, new Vector3(transform.position.x, transform.position.y + 0.6f, 0), 0.25f);
                }
            }
            else
            {
                healthSlider.SetActive(false);
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
        bool playedSound = false; //Prevents multiple laser sounds from playing at once

        foreach (var i in weapons)
        {
            if(i.Shoot(rb.velocity, WeaponTypes.AllTypes, !playedSound) && i.weaponType == WeaponTypes.Laser)
            {
                playedSound = true;
            }
        }
    }
    
    public void TakeDamage(float amount, WeaponTypes type)
    {
        if(type == WeaponTypes.Missile)
        {
            health -= amount;
        }
        else
        {
            if(shield > 0)
            {
                shield -= amount;

                if (shield < 0)
                {
                    health += shield;
                }
            }
            else
            {
                health -= amount;
            }    
        }
        
        healthSlider.transform.GetChild(0).GetComponent<Slider>().value = shield / startShield;
        healthSlider.transform.GetChild(1).GetComponent<Slider>().value = health / startHealth;

        if(health <= 0)
        {
            bool dropPowerup = Random.Range(0f, 1f) < WeaponScaling.GetRechargeDropChance(DataHandler.Instance.shipInfo.GetLvl(9));
            
            if(dropPowerup)
            {
                ShieldPowerupPool.Instance.SpawnShield(transform.position);
            }

            ExplosionPool.Instance.PlayExplosion(transform.position);
            gameObject.SetActive(false);
            //EnemyHealthSliders.Instance.ReturnSliderObject(healthSlider);
            EnemySpawner.Instance.EnemyDied(gameObject);
            healthSlider.SetActive(false);
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);

        if(healthSlider != null)
        {
            healthSlider.SetActive(false);
        }   
    }
}
