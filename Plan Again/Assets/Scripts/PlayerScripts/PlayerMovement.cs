﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 Change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public Signal playerHit;

    // Start is called before the first frame update
    void Start()
    {

        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("MoveX", 0);
        animator.SetFloat("MoveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void Update()
    {
        //is the player in interaction
        if(currentState == PlayerState.interact)
        {
            return;
        }
        Change = Vector3.zero;
        Change.x = Input.GetAxisRaw("Horizontal");
        Change.y = Input.GetAxisRaw("Vertical");
        if ( Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("Attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("Receive Item", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("Receive Item", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    void UpdateAnimationAndMove()
    {
        if (Change != Vector3.zero)
        {
            MoveCharacter();
            Change.x = Mathf.Round(Change.x);
            Change.y = Mathf.Round(Change.y);
            animator.SetFloat("MoveX", Change.x);
            animator.SetFloat("MoveY", Change.y);
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }

    void MoveCharacter()
    {
        Change.Normalize();
        myRigidbody.MovePosition(transform.position + Change * speed * Time.deltaTime);
    }

    public void Knock(float KnockTime, float damage)
    {
        currentHealth.RunTimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RunTimeValue > 0)
        {
            StartCoroutine(KnockCo(KnockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
            SceneManager.LoadScene("EndGame");
        }
    }

    private IEnumerator KnockCo(float KnockTime)
    {
        playerHit.Raise();
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(KnockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;

        }
    }
}
