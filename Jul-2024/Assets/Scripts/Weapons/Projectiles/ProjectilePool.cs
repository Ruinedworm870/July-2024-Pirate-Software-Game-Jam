using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;

    public Transform holder;
    
    public GameObject playerLaser;
    public GameObject playerMissile;
    
    public GameObject enemyLaser;
    public GameObject enemyMissile;
    
    private int playerLasers = 250;
    private int playerMissiles = 50;
    
    private int enemyLasers = 1000;
    private int enemyMissiles = 250;
    
    private Stack<GameObject> unusedPlayerLasers = new Stack<GameObject>();
    private Stack<GameObject> unusedPlayerMissiles = new Stack<GameObject>();
    
    private Stack<GameObject> unusedEnemyLasers = new Stack<GameObject>();
    private Stack<GameObject> unusedEnemyMissiles = new Stack<GameObject>();

    private List<GameObject> allProjectiles = new List<GameObject>();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for(int i = 0; i < playerLasers; i++)
        {
            GameObject c = CreateObject(playerLaser);
            unusedPlayerLasers.Push(c);
            allProjectiles.Add(c);
        }

        for(int i = 0; i < enemyLasers; i++)
        {
            GameObject c = CreateObject(enemyLaser);
            unusedEnemyLasers.Push(c);
            allProjectiles.Add(c);
        }

        for(int i = 0; i < playerMissiles; i++)
        {
            GameObject c = CreateObject(playerMissile);
            unusedPlayerMissiles.Push(c);
            allProjectiles.Add(c);
        }

        for(int i = 0; i < enemyMissiles; i++)
        {
            GameObject c = CreateObject(enemyMissile);
            unusedEnemyMissiles.Push(c);
            allProjectiles.Add(c);
        }
    }   

    private GameObject CreateObject(GameObject prefab)
    {
        GameObject created = Instantiate(prefab, holder);
        created.SetActive(false);
        return created;
    }
    
    public void ShootProjectile(Transform pos, float damage, float range, float speed, Vector2 characterVelocity, bool isPlayer, WeaponTypes weaponType)
    {
        Projectile projectile = GetProjectile(weaponType, isPlayer).GetComponent<Projectile>();
        
        LayerMask sender;
        LayerMask target;

        if(isPlayer)
        {
            sender = LayerMask.GetMask("Player");
            target = LayerMask.GetMask("Enemy");
        }
        else
        {
            sender = LayerMask.GetMask("Enemy");
            target = LayerMask.GetMask("Player");
        }
        
        projectile.Init(pos, damage, range, speed, characterVelocity, target, sender, isPlayer, weaponType);
    }

    private GameObject GetProjectile(WeaponTypes weaponType, bool isPlayer)
    {
        GameObject projectile = null;
        
        if (weaponType == WeaponTypes.Laser)
        {
            if (isPlayer)
            {
                if(unusedPlayerLasers.TryPop(out projectile))
                {
                    return projectile;
                }
                else
                {
                    return CreateObject(playerLaser);
                }
            }
            else
            {
                if(unusedEnemyLasers.TryPop(out projectile))
                {
                    return projectile;
                }
                else
                {
                    return CreateObject(enemyLaser);
                }
            }
        }
        else if (weaponType == WeaponTypes.Missile)
        {
            if (isPlayer)
            {
                if(unusedPlayerMissiles.TryPop(out projectile))
                {
                    return projectile;
                }
                else
                {
                    return CreateObject(playerMissile);
                }
            }
            else
            {
                if(unusedEnemyMissiles.TryPop(out projectile))
                {
                    return projectile;
                }
                else
                {
                    return CreateObject(enemyMissile);
                }
            }
        }

        return projectile;
    }

    public void ReturnProjectile(GameObject p, WeaponTypes weaponType, bool isPlayer)
    {
        if(weaponType == WeaponTypes.Laser)
        {
            if(isPlayer)
            {
                unusedPlayerLasers.Push(p);
            }
            else
            {
                unusedEnemyLasers.Push(p);
            }
        }
        else if(weaponType == WeaponTypes.Missile)
        {
            if(isPlayer)
            {
                unusedPlayerMissiles.Push(p);
            }
            else
            {
                unusedEnemyMissiles.Push(p);
            }
        }
    }

    public void ResetProjectiles()
    {
        foreach(var i in allProjectiles)
        {
            i.GetComponent<Projectile>().ReturnToPool();
        }
    }
}
