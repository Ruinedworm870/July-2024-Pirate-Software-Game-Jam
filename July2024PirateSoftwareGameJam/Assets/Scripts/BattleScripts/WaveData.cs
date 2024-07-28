using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WaveData
{
    private static int startWaveShips = 2;
    private static int waveShipsIncrease = 2;
    
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
    
    private static int impureRewardPerKill = 1;
    private static int impureRewardIncrease = 1;

    public static int GetImpureRewardPerKill(int wave)
    {
        wave -= 1;
        return impureRewardPerKill + (impureRewardIncrease * wave);
    }

    private static int chunkRewardPerKill = 0;
    private static float chunkRewardIncrease = 0.5f;

    public static int GetChunkRewardPerKill(int wave)
    {
        wave -= 1;
        return chunkRewardPerKill + (int)(chunkRewardIncrease * wave);
    }

    private static int pureRewardPerKill = 0;
    private static float pureRewardIncrease = 0.25f;

    public static int GetPureRewardPerKill(int wave)
    {
        wave -= 1;
        return pureRewardPerKill + (int)(pureRewardIncrease * wave);
    }
}
