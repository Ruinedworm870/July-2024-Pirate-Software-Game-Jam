using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuotaInfo
{
    private int quotaTier;
    private int battlesRemaining;
    private int quotaProgress;
    
    public QuotaInfo()
    {
        battlesRemaining = QuotaScaling.GetQuotaBattlesRemaining(0);
    }

    public int GetQuotaTier()
    {
        return quotaTier;
    }

    public int GetBattlesRemaining()
    {
        return battlesRemaining;
    }

    public int GetQuotaProgress()
    {
        return quotaProgress;
    }

    public void SetQuotaTier(int tier)
    {
        quotaTier = tier;
    }

    public void SetBattlesRemaining(int br)
    {
        battlesRemaining = br;
    }
    
    public void SetQuotaProgress(int progress)
    {
        quotaProgress = progress;
    }
}
