using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D MyRigidBody;
    private Vector3 Change;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        MyRigidBody = GetComponent<Rigidbody2D>();
        animator.SetFloat("MoxeX", 0);
        animator.SetFloat("MoveY", -1);
    }

    // Update is called once per frame
    void Update()
    {
        Change = Vector3.zero;
        Change.x = Input.GetAxisRaw("Horizontal");
        Change.y = Input.GetAxisRaw("Vertical");
        if ( Input.GetButtonDown("attack") && currentState != PlayerState.attack) 
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.walk)
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
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove()
    {
        if (Change != Vector3.zero)
        {
            MoveCharacter();
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
        MyRigidBody.MovePosition(
            transform.position + Change * speed * Time.deltaTime);
    }
}
