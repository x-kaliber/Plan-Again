using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureChest : Interactable
{
    [Header("Contents")]
    public Item Contents;
    public Inventory playerInventory;
    public bool isOpen;
    public BoolValue Looted;

    [Header("Signals and Dialog")]
    public Signal raiseItem;
    public GameObject dialogBox;
    public Text dialogText;

    [Header("Animation")]
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOpen = Looted.RunTimeValue;
        //Check if we have opened this container on the past
        if (isOpen)
        {
            //activate the animation
            anim.SetBool("Opened", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && PlayerInRange )
        {
            if (!isOpen)
            {
                //Open Chest
                OpenChest();
            }
            else
            {
                //Chest is Open
                ChestIsOpen();
            }
        }
    }

    public void OpenChest()
    {
        //Dialog Window on
        dialogBox.SetActive(true);
        //Dialog text = contents text
        dialogText.text = Contents.itemDescription;
        //Add contents to the inventory
        playerInventory.AddItem(Contents);
        playerInventory.currentItem = Contents;
        //Rasie the signal to animate correctly
        raiseItem.Raise();
        //set the chest to opened
        isOpen = true;
        //raise the context clue
        context.Raise();
        //activate the animation
        anim.SetBool("Opened", true);
        Looted.RunTimeValue = isOpen; 
    }

    public void ChestIsOpen()
    {
            //dialog off
            dialogBox.SetActive(false);
            //raise the signal to player to stop animate
            raiseItem.Raise();

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
        {
            context.Raise();
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger && !isOpen)
            context.Raise();
        PlayerInRange = false;
    }
}
