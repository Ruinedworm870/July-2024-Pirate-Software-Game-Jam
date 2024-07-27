using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Includs the amounts and the conversion rate
public class ResourceInfo
{
    /*
        0 = Gold
        1 = Impure Iron
        2 = Iron Chunk
        3 = Pure Iron Plate
    */
    private int[] amounts = new int[4];
    private int[] conversionRates = new int[3]; //Amount of resource for 1 gold (is amount index - 1, since gold is excluded)

    public ResourceInfo()
    {
        conversionRates[0] = 8;
        conversionRates[1] = 4;
        conversionRates[2] = 2;

        amounts[1] = 80;
    }
    
    public int GetAmount(int id)
    {
        return amounts[id];
    }
    
    public void SetAmount(int id, int amount)
    {
        amounts[id] = amount;
    }
    
    //Pass the normal amount index
    public int GetConversionRate(int id)
    {
        id -= 1;
        return conversionRates[id];
    }
}
