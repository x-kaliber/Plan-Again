using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{

    public GameObject contextClue;
    public bool contextActive = false;

    public void changeContext()
    {
        contextActive = !contextActive;
        if (contextActive)
        {
            contextClue.SetActive(true);
        }
        else
        {
            contextClue.SetActive(false);
        }
    }
}
