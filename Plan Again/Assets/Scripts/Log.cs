using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{

    public Transform target;
    public Transform HomePosition;
    public float ChaseRadius;
    public float AttackRadius;
    

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if(Vector3.Distance(target.position, transform.position) <= ChaseRadius && Vector3.Distance(target.position, transform.position) > AttackRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
        }
    }
}
