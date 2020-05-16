using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button,
    item
}

public class Door : Interactable
{
    [Header("Door Variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerInRange && thisDoorType == DoorType.key)
            {
                // Does the player has a key?
                if(playerInventory.numberOfKeys > 0)
                {
                    //remove a player key
                    playerInventory.numberOfKeys--;
                    //If, yes. open the door
                    Open();
                }
            }
        }
    }

    public void Open()
    {
        //turn off the door Sprite Renderer
        doorSprite.enabled = false;
        //set open to true
        open = true;
        //turn off the door's box collider
        physicsCollider.enabled = false; 
    }

    public void Close()
    {
        //turn off the door Sprite Renderer
        doorSprite.enabled = true;
        //set open to true
        open = false;
        //turn off the door's box collider
        physicsCollider.enabled = true;
    }

}
