using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This isn't going to be too crazy, its just a game jam, so most people probably won't even play past the first quota
public static class QuotaScaling
{
    private static int startQuotaSize = 10;
    private static int quotaIncrement = 10;
    private static int startBattlesRemaining = 3;
    private static float battlesRemainingIncrement = 1f;
    
    public static int GetQuotaSize(int quotaTier)
    {
        return startQuotaSize + (quotaIncrement * quotaTier);
    }

    public static int GetQuotaBattlesRemaining(int quotaTier)
    {
        return startBattlesRemaining + (int)(battlesRemainingIncrement * quotaTier);
    }
}
