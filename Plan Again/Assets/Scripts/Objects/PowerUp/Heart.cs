using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : PowerUp
{

    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float amountToIncrease;



    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            playerHealth.RunTimeValue += amountToIncrease;
            if(playerHealth.initialValue > heartContainers.RunTimeValue * 2f)
            {
                playerHealth.initialValue = heartContainers.RunTimeValue * 2f;
            }
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }

}
