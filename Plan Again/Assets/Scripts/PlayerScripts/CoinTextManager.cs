using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI coinDisplay;

    public void UpdateCoinCount()
    {
        if (playerInventory.Coins != null || playerInventory.Coins != 0)
        {

            if (playerInventory.Coins >= 1000)
            {
                if (playerInventory.Coins >= 1000000)
                {
                    coinDisplay.text = "" + Math.Round((playerInventory.Coins / 1000000), 2) + " M";
                }
                else
                {
                    coinDisplay.text = "" + Math.Round((playerInventory.Coins / 1000), 2) + " K";
                }
            }
            else
            {
                coinDisplay.text = "" + playerInventory.Coins;
            }
        }
        else
        {
            coinDisplay.text = "0" + playerInventory.Coins;
        }
    }
}
