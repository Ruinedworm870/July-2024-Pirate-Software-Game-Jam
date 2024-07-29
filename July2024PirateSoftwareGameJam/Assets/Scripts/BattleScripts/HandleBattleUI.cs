using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HandleBattleUI : MonoBehaviour
{
    public Animator controller;
    public SetupPlayer setupPlayer;

    public Slider healthSlider;
    public Slider shieldSlider;

    public TextMeshProUGUI eowTitle;
    public TextMeshProUGUI impureRewardText;
    public TextMeshProUGUI chunkRewardText;
    public TextMeshProUGUI pureRewardText;
    public TextMeshProUGUI goldRewardText;
    public GameObject newWaveButton;
    public GameObject leaveButton;
    public GameObject leaveOnDeathButton;

    public TextMeshProUGUI waveText;
    public TextMeshProUGUI impureText;
    public TextMeshProUGUI chunkText;
    public TextMeshProUGUI pureText;

    public TextMeshProUGUI laserAmmo;
    public TextMeshProUGUI missileAmmo;

    private int impureReward = 0;
    private int chunkReward = 0;
    private int pureReward = 0;
    
    private void Start()
    {
        waveText.text = "Wave: 1";
        impureText.text = "0";
        chunkText.text = "0";
        pureText.text = "0";
    }

    public void UpdateHealthSliders(float health, float shield)
    {
        healthSlider.value = health;
        shieldSlider.value = shield;
    }

    public void UpdateShieldSlider(float shield)
    {
        shieldSlider.value = shield;
    }
    
    public void OnDeath()
    {
        setupPlayer.OnDeath();

        UISoundController.Instance.PlayClickSound();

        EnemySpawner.Instance.DisableEnemies();
        ProjectilePool.Instance.ResetProjectiles();

        eowTitle.text = "You Have DIED!";
        
        //Removed this feature:
        //Set reward to 1/4 of value
        //impureReward = (int)(impureReward * 0.25f);
        //chunkReward = (int)(chunkReward * 0.25f);
        //pureReward = (int)(pureReward * 0.25f);
        HandleReward();
        
        newWaveButton.SetActive(false);
        leaveButton.SetActive(false);
        leaveOnDeathButton.SetActive(true);
        
        controller.SetTrigger("Open");
    }

    private void HandleReward(bool updateData = false)
    {
        int goldReward = 0;
        
        if(updateData)
        {
            DataHandler.Instance.resourceInfo.SetAmount(1, DataHandler.Instance.resourceInfo.GetAmount(1) + impureReward);
            DataHandler.Instance.resourceInfo.SetAmount(2, DataHandler.Instance.resourceInfo.GetAmount(2) + chunkReward);
            DataHandler.Instance.resourceInfo.SetAmount(3, DataHandler.Instance.resourceInfo.GetAmount(3) + pureReward);
        }
        
        goldReward += impureReward / DataHandler.Instance.resourceInfo.GetConversionRate(1);
        goldReward += chunkReward / DataHandler.Instance.resourceInfo.GetConversionRate(2);
        goldReward += pureReward / DataHandler.Instance.resourceInfo.GetConversionRate(3);

        impureRewardText.text = NumberHandler.GetDisplay(impureReward, 1);
        chunkRewardText.text = NumberHandler.GetDisplay(chunkReward, 1);
        pureRewardText.text = NumberHandler.GetDisplay(pureReward, 1);
        goldRewardText.text = NumberHandler.GetDisplay(goldReward, 1);
    }

    public void HandleAmmoText(int lasers, int totalLasers, int missiles, int totalMissiles, float laserReloadTime, float missileReloadTime)
    {
        if(totalLasers > 0)
        {
            if (lasers > 0)
            {
                laserAmmo.text = "Ammo: " + NumberHandler.GetDisplay(lasers, 1) + " / " + NumberHandler.GetDisplay(totalLasers, 1);
            }
            else
            {
                laserAmmo.text = "Reloading: " + NumberHandler.GetDisplay(laserReloadTime, 1) + "s";
            }
        }   
        else
        {
            laserAmmo.text = "Ammo: 0 / 0";
        }
        
        if(totalMissiles > 0)
        {
            if (missiles > 0)
            {
                missileAmmo.text = "Ammo: " +NumberHandler.GetDisplay(missiles, 1) + " / " + NumberHandler.GetDisplay(totalMissiles, 1);
            }
            else
            {
                missileAmmo.text = "Reloading: " + NumberHandler.GetDisplay(missileReloadTime, 1) + "s";
            }
        }
        else
        {
            missileAmmo.text = "Ammo: 0 / 0";
        }
    }

    public void HandleEnemyDeath()
    {
        int wave = EnemySpawner.Instance.GetWave();

        impureReward += WaveData.GetImpureRewardPerKill(wave);
        chunkReward += WaveData.GetChunkRewardPerKill(wave);
        pureReward += WaveData.GetPureRewardPerKill(wave);

        impureText.text = NumberHandler.GetDisplay(impureReward, 1);
        chunkText.text = NumberHandler.GetDisplay(chunkReward, 1);
        pureText.text = NumberHandler.GetDisplay(pureReward, 1);
    }

    public void HandleEndOfWave()
    {
        UISoundController.Instance.PlayClickSound();

        ProjectilePool.Instance.ResetProjectiles();
        
        eowTitle.text = "Wave " + EnemySpawner.Instance.GetWave() + " Survived";
        
        HandleReward();

        controller.SetTrigger("Open");
    }

    public void NextWave()
    {
        controller.SetTrigger("Close");
        setupPlayer.OnNewWave();
        ShieldPowerupPool.Instance.ResetPositions();
        PlayerPosition.pos = Vector3.zero;
        EnemySpawner.Instance.NewWave();
        waveText.text = "Wave: " + EnemySpawner.Instance.GetWave();
    }

    public void Leave()
    {
        HandleReward(true);
        SceneManager.LoadSceneAsync(0);
    }
}
