using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumberHandler
{
    public static string GetDisplay(double num, int decimals)
    {
        double temp = num;

        int total = 0;

        string zeroInFront = "";

        if (num == 0)
        {
            return num.ToString();
        }

        if (num > -1 && num < 1)
        {
            zeroInFront = "0";
        }

        if (temp < 0)
        {
            temp *= -1;
        }

        while (temp / 1000 > 1)
        {
            temp /= 1000;

            total += 1;
        }

        string end = "";

        switch (total)
        {
            case 1:
                end = "K";
                break;

            case 2:
                end = "M";
                break;

            case 3:
                end = "B";
                break;

            case 4:
                end = "T";
                break;

            case 5:
                end = "q";
                break;

            case 6:
                end = "Q";
                break;

            case 7:
                end = "s";
                break;

            case 8:
                end = "S";
                break;

            case 9:
                end = "o";
                break;

            case 10:
                end = "n";
                break;

            case 11:
                end = "d";
                break;

            case 12:
                end = "U";
                break;

            case 13:
                end = "D";
                break;

            case 14:
                end = "Td";
                break;

            case 15:
                end = "qd";
                break;

            case 16:
                end = "Qd"; //Quindecillion
                break;
        }

        string commas = "";

        if (total > 0)
        {
            for (int i = 0; i < total; i++)
            {
                commas += ",";
            }
        }

        if (decimals == 2)
        {
            return zeroInFront + num.ToString("#" + commas + ".#.#" + commas + end);
        }
        else if (decimals == 1)
        {
            return zeroInFront + num.ToString("#" + commas + ".#" + commas + end);
        }
        else
        {
            return num.ToString("#" + commas + end);
        }
    }
}
