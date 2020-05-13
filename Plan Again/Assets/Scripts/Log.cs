using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public Transform HomePosition;
    public float ChaseRadius;
    public float AttackRadius;
    public Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= ChaseRadius && Vector3.Distance(target.position, transform.position) > AttackRadius)
        {
            if(currentState == EnemyState.idle || currentState == EnemyState.walk || currentState != EnemyState.stagger)
            { 
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk);
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if ( currentState != newState)
        {
            currentState = newState;
        }
    }
}
