using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool Instance;

    public Transform holder;
    
    /*public GameObject playerBullet;
    public GameObject playerLaser;
    public GameObject playerMissile;

    public GameObject enemyBullet;
    public GameObject enemyLaser;
    public GameObject enemyMissile;*/

    public GameObject defaultProjectile;

    private int projectiles = 2500;

    /*private int playerBullets = 250;
    private int playerLasers = 250;
    private int playerMissiles = 50;
    
    private int enemyBullets = 1000;
    private int enemyLasers = 1000;
    private int enemyMissiles = 250;

    private Stack<GameObject> unusedPlayerBullets = new Stack<GameObject>();
    private Stack<GameObject> unusedPlayerLasers = new Stack<GameObject>();
    private Stack<GameObject> unusedPlayerMissiles = new Stack<GameObject>();

    private Stack<GameObject> unusedEnemyBullets = new Stack<GameObject>();
    private Stack<GameObject> unusedEnemyLasers = new Stack<GameObject>();
    private Stack<GameObject> unusedEnemyMissiles = new Stack<GameObject>();*/

    private Stack<GameObject> unusedProjectiles = new Stack<GameObject>();
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        /*for(int i = 0; i < playerBullets; i++)
        {
            unusedPlayerBullets.Push(CreateObject(playerBullet));
        }*/

        for(int i = 0; i < projectiles; i++)
        {
            unusedProjectiles.Push(CreateObject(defaultProjectile));
        }
    }   

    private GameObject CreateObject(GameObject prefab)
    {
        GameObject created = Instantiate(prefab, holder);
        created.SetActive(false);
        return created;
    }

    /*public GameObject GetPlayerBullet()
    {
        GameObject bullet;
        
        if(unusedPlayerBullets.TryPop(out bullet))
        {
            return bullet;
        }
        else
        {
            return CreateObject(playerBullet);
        }
    }
    
    public void ReturnPlayerBullet(GameObject b)
    {
        unusedPlayerBullets.Push(b);
    }*/

    public GameObject GetProjectile()
    {
        GameObject projectile;
        
        if (unusedProjectiles.TryPop(out projectile))
        {
            return projectile;
        }
        else
        {
            return CreateObject(defaultProjectile);
        }
    }
    
    public void ReturnPlayerBullet(GameObject p)
    {
        unusedProjectiles.Push(p);
    }
}
