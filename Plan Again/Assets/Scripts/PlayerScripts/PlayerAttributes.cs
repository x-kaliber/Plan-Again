using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttributes : MonoBehaviour
{
    public static float hunger;
    public  float MaxHunger;

    void Update()
    {
        if( hunger < MaxHunger)
        {

            hunger += 0.5f * Time.deltaTime;
        }

        if(hunger >= MaxHunger)
        {
            Die();
        }
    }

    void OntriggerEnter2d(Collider2D other)
    {
        if(other.tag == "Food")
        {
            hunger = hunger - 25;
        }
    }

    void OntriggerExit2D(Collider2D other)
    {

    }

    void Die()
    {
        this.gameObject.SetActive(false);
        hunger = MaxHunger;
        SceneManager.LoadScene("EndGame");
    }
}
