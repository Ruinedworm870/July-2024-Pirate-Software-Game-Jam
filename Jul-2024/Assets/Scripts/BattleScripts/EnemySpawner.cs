using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public HandleBattleUI handleBattleUI;

    public GameObject prefab;
    public Transform holder;

    public AudioClip laserSound;
    public AudioClip missileShootSound;

    private int total = WaveData.GetMostAliveShips();

    private Queue<GameObject> enemies = new Queue<GameObject>();
    private List<GameObject> allEnemies = new List<GameObject>();
    
    private int enemyLevel;
    private int mostAliveEnemies;
    private int totalEnemiesLeft;
    private int aliveEnemies;
    private int wave = 0;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerPosition.pos = Vector3.zero;
        GetEnemyLevel();
        
        for(int i = 0; i < total; i++)
        {
            GameObject e = CreateEnemy();
            enemies.Enqueue(e);
            allEnemies.Add(e);
        }

        NewWave();
    }

    private GameObject CreateEnemy()
    {
        GameObject created = Instantiate(prefab, holder);
        created.SetActive(false);
        return created;
    }

    private void GetEnemyLevel()
    {
        ShipInfo shipInfo = DataHandler.Instance.shipInfo;
        int combined = 0;

        foreach(var i in shipInfo.GetWeaponInfo())
        {
            combined += i.lvl;
        }
        
        //Average
        enemyLevel = combined / shipInfo.GetWeaponInfo().Length;

        if(enemyLevel == 0)
        {
            enemyLevel = 1;
        }
    }

    public int GetWave()
    {
        return wave;
    }
    
    public void NewWave()
    {
        wave += 1;
        mostAliveEnemies = WaveData.GetMostAliveShipsInWave(wave);
        totalEnemiesLeft = WaveData.GetShipsThisWave(wave);
        HandleSpawnLoop();
    }

    private void HandleSpawnLoop()
    {
        float minDelay = 1f;
        float maxDelay = 5f;

        while(aliveEnemies < mostAliveEnemies && aliveEnemies < totalEnemiesLeft)
        {
            StartCoroutine(SpawnDelay(minDelay, maxDelay));
            aliveEnemies += 1;
        }
    }
    
    private IEnumerator SpawnDelay(float min, float max)
    {
        if(aliveEnemies == 0)
        {
            SpawnEnemy();
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(min, max));
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        float missileChance = WaveData.GetMissileChance();
        
        //no missiles on first 3 waves
        if(wave < 4)
        {
            missileChance = -1;
        }
        
        //Adds variation to the level of the enemies which would make the earlier waves easier as the player gets a higher and higher level (or at least a chance to since its random)
        int lvl = enemyLevel;
        int halfLvl = Mathf.Clamp(lvl / 2, 1, lvl);
        lvl = Random.Range(halfLvl + wave, lvl + 1);
        lvl = Mathf.Clamp(lvl, 1, enemyLevel);
        
        int weapons = Mathf.Clamp(WaveData.GetEnemyWeaponsPerLevel(lvl), 1, 7);
        
        GameObject e = enemies.Dequeue();
        Enemy enemy = e.GetComponent<Enemy>();
        enemy.weapons.Clear();
        Transform weaponHolder = e.transform.Find("Weapons");

        float modifier = WeaponScaling.GetEnemyHealthModifier();
        float hull = WeaponScaling.GetHullStrength(lvl) * modifier;
        float shield = WeaponScaling.GetShieldStrength(lvl) * modifier;
        float shieldRegen = WeaponScaling.GetShieldRegen(lvl);
        
        enemy.health = hull;
        enemy.shield = shield;
        enemy.regenPerMin = shieldRegen;
        enemy.Reset();

        for (int i = 0; i < weapons; i++)
        {
            bool isMissile = Random.Range(0f, 1f) < missileChance;
            Weapon weapon = weaponHolder.GetChild(i).GetComponent<Weapon>();

            float damage;
            float fireRate;
            float range; //laser range is 50, missile range is 100 (100 on enemy, 50 on player)
            float speed; //laser speed is 30, and missile is 7, they don't change based on level
            int ammo;
            float reloadSpeed;
            
            if(isMissile)
            {
                damage = WeaponScaling.GetMissileDamage(lvl);
                fireRate = WeaponScaling.GetMissileFireRate(lvl);
                range = 100f;
                speed = 7f;
                ammo = WeaponScaling.GetMissileMagSize(lvl);
                reloadSpeed = WeaponScaling.GetMissileReloadSpeed(lvl);

                weapon.weaponType = WeaponTypes.Missile;
                weapon.shotSound = missileShootSound;
            }
            else
            {
                damage = WeaponScaling.GetLaserDamage(lvl);
                fireRate = WeaponScaling.GetLaserFireRate(lvl);
                range = 50f;
                speed = 30f;
                ammo = WeaponScaling.GetLaserMagSize(lvl);
                reloadSpeed = WeaponScaling.GetLaserReloadSpeed(lvl);

                weapon.weaponType = WeaponTypes.Laser;
                weapon.shotSound = laserSound;
            }

            weapon.damage = damage;
            weapon.fireRate = fireRate;
            weapon.range = range;
            weapon.speed = speed;
            weapon.ammo = ammo;
            weapon.startAmmo = ammo;
            weapon.reloadTime = reloadSpeed;
            weapon.Reset();

            enemy.weapons.Add(weapon);
        }
        
        e.transform.position = GetSpawnPos(PlayerPosition.pos, 35f);
        e.SetActive(true);
    }

    private Vector3 GetSpawnPos(Vector3 pos, float distance)
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2);
        
        float newX = pos.x + distance * Mathf.Cos(randomAngle);
        float newY = pos.y + distance * Mathf.Sin(randomAngle);
        
        return new Vector3(newX, newY, 0);
    }
    
    public void EnemyDied(GameObject enemy)
    {
        if(!enemies.Contains(enemy))
        {
            totalEnemiesLeft -= 1;
            aliveEnemies -= 1;
            handleBattleUI.HandleEnemyDeath();
            enemies.Enqueue(enemy);

            if (totalEnemiesLeft <= 0)
            {
                handleBattleUI.HandleEndOfWave();
            }
            else
            {
                HandleSpawnLoop();
            }
        }        
    }
    
    public void DisableEnemies()
    {
        foreach(var i in allEnemies)
        {
            i.GetComponent<Enemy>().Deactivate();
        }
    }
}
