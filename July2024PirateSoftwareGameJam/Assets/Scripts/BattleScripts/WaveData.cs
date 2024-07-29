using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WaveData
{
    private static int startWaveShips = 1;
    private static int waveShipsIncrease = 1;
    
    public static int GetShipsThisWave(int wave)
    {
        wave -= 1;
        return startWaveShips + (waveShipsIncrease * wave);
    }

    private static int mostAliveShips = 16;

    public static int GetMostAliveShips()
    {
        return mostAliveShips;
    }
    
    private static int startMostShipsAlive = 1;
    private static float aliveShipsIncrease = 0.5f;

    public static int GetMostAliveShipsInWave(int wave)
    {
        wave -= 1;
        return Mathf.Clamp(startMostShipsAlive + (int)(aliveShipsIncrease * wave), 1, GetMostAliveShips());
    }
    
    private static int impureRewardPerKill = 8;
    private static float impureRewardIncrease = 2f;

    public static int GetImpureRewardPerKill(int wave)
    {
        wave -= 1;
        return impureRewardPerKill + (int)(impureRewardIncrease * wave);
    }

    private static int chunkRewardPerKill = 0;
    private static float chunkRewardIncrease = 0.5f;

    public static int GetChunkRewardPerKill(int wave)
    {
        wave -= 1;
        return chunkRewardPerKill + (int)(chunkRewardIncrease * wave);
    }

    private static int pureRewardPerKill = 0;
    private static float pureRewardIncrease = 0.2f;
    
    public static int GetPureRewardPerKill(int wave)
    {
        wave -= 1;
        return pureRewardPerKill + (int)(pureRewardIncrease * wave);
    }
    
    private static int enemyWeaponsPerLevel = 1;
    private static float weaponsPerLevelIncrease = 0.25f;

    public static int GetEnemyWeaponsPerLevel(int lvl)
    {
        lvl -= 1;
        return enemyWeaponsPerLevel + (int)(weaponsPerLevelIncrease * lvl);
    }

    private static float missileChance = 0.25f;

    public static float GetMissileChance()
    {
        return missileChance;
    }
}
