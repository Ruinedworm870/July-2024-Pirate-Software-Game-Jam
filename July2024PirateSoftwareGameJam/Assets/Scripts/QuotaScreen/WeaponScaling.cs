using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Also includes non weapon ship upgrades
public static class WeaponScaling
{
    private static int baseLaserDamage = 1;
    private static int laserDamageIncrease = 1;
    
    public static int GetLaserDamage(int lvl)
    {
        lvl -= 1;
        return baseLaserDamage + (lvl * laserDamageIncrease);
    }
    
    private static int baseLaserFireRate = 4;
    private static float laserFireRateIncrease = 0.5f;

    public static int GetLaserFireRate(int lvl)
    {
        lvl -= 1;
        return baseLaserFireRate + (int)(lvl * laserFireRateIncrease);
    }

    private static int baseLaserMagSize = 100;
    private static int laserMagSizeIncrease = 25;
    
    public static int GetLaserMagSize(int lvl)
    {
        lvl -= 1;
        return baseLaserMagSize + (lvl * laserMagSizeIncrease);
    }

    private static float baseLaserReloadSpeed = 3f;
    private static float laserReloadSpeedIncrease = -0.05f;

    public static float GetLaserReloadSpeed(int lvl)
    {
        lvl -= 1;
        return baseLaserReloadSpeed + (lvl * laserReloadSpeedIncrease);
    }

    private static int baseMissileDamage = 50;
    private static int missileDamageIncrease = 10;

    public static int GetMissileDamage(int lvl)
    {
        lvl -= 1;
        return baseMissileDamage + (lvl * missileDamageIncrease);
    }

    private static int baseMissileFireRate = 1;
    private static float missileFireRateIncrease = 0.1f;

    public static int GetMissileFireRate(int lvl)
    {
        lvl -= 1;
        return baseMissileFireRate + (int)(lvl * missileFireRateIncrease);
    }

    private static int baseMissileMagSize = 1;
    private static float missileMagSizeIncrease = 0.25f;

    public static int GetMissileMagSize(int lvl)
    {
        lvl -= 1;
        return baseMissileMagSize + (int)(lvl * missileMagSizeIncrease);
    }
    
    private static float baseMissileReloadSpeed = 10f;
    private static float missileReloadSpeedIncrease = -0.25f;
    
    public static float GetMissileReloadSpeed(int lvl)
    {
        lvl -= 1;
        return baseMissileReloadSpeed + (lvl * missileReloadSpeedIncrease);
    }

    private static int baseHullStrength = 500;
    private static int hullStrengthIncrease = 50;

    public static int GetHullStrength(int lvl)
    {
        lvl -= 1;
        return baseHullStrength + (lvl * hullStrengthIncrease);
    }

    private static int baseShieldStrength = 250;
    private static int shieldStrengthIncrease = 25;
    
    public static int GetShieldStrength(int lvl)
    {
        lvl -= 1;
        return baseShieldStrength + (lvl * shieldStrengthIncrease);
    }
    
    private static float baseShieldRegen = 0.1f;
    private static float shieldRegenIncrease = 0.03f;
    
    public static float GetShieldRegen(int lvl)
    {
        lvl -= 1;
        return baseShieldRegen + (lvl * shieldRegenIncrease);
    }

    private static float baseRechargeDropChance = 0.05f;
    private static float rechargeDropChanceIncrease = 0.005f;

    public static float GetRechargeDropChance(int lvl)
    {
        lvl -= 1;
        return baseRechargeDropChance + (lvl * rechargeDropChanceIncrease);
    }
    
    private static float baseRechargeDropHeal = 0.2f;
    private static float rechargeDropHealIncrease = 0.01f;

    public static float GetRechargeDropHeal(int lvl)
    {
        lvl -= 1;
        return baseRechargeDropHeal + (lvl * rechargeDropHealIncrease);
    }
    
    private static int totalImpureLevels = 10;
    private static int startImpureCost = 25;
    private static int impureIncrease = 25;

    private static int totalChunkLevels = 10;
    private static int startChunkCost = 25;
    private static int chunkIncrease = 25;
    
    private static int totalPureLevels = 10;
    private static int startPureCost = 25;
    private static int pureIncrease = 25;

    public static (int id, int amount) GetUpgradeCost(int currentLvl)
    {
        int lvl = currentLvl - 1;

        int id;
        int amount;
        if(lvl < totalImpureLevels)
        {
            id = 1;
            amount = startImpureCost + (lvl * impureIncrease);
        }
        else if(lvl < totalImpureLevels + totalChunkLevels)
        {
            id = 2;
            amount = startChunkCost + (lvl * chunkIncrease);
        }
        else
        {
            id = 3;
            amount = startPureCost + (lvl * pureIncrease);
        }

        return (id, amount);
    }

    public static bool IsMaxLevel(int lvl)
    {
        return lvl >= totalImpureLevels + totalChunkLevels + totalPureLevels;
    }

    private static int replaceAnythingCost = 50;
    private static int replaceEmptyStartCost = 100;
    private static int replaceEmptyCostIncrease = 100;
    private static int replacesBeforeNextMat = 2;

    public static int GetReplaceCostOfAnything()
    {
        return replaceAnythingCost;
    }

    public static (int id, int amount) GetReplaceCostOfEmpty(int emptiesReplaced)
    {
        int id = (emptiesReplaced / replacesBeforeNextMat) + 1;
        int amount = replaceEmptyStartCost + (replaceEmptyCostIncrease * (emptiesReplaced % replacesBeforeNextMat));

        return (id, amount);
    }
}
