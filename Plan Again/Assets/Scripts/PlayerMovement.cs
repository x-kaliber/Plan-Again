using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D MyRigidBody;
    private Vector3 Change;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        MyRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Change = Vector3.zero;
        Change.x = Input.GetAxisRaw("Horizontal");
        Change.y = Input.GetAxisRaw("Vertical");
        Debug.Log(Change);
        UpdateAnimationAndMove();
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
        MyRigidBody.MovePosition(
            transform.position + Change * speed * Time.deltaTime);
    }
}
