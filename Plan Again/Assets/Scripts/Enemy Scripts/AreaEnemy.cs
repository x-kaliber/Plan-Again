using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Log
{
    public Collider2D boundary;

    public override void CheckDistance()
    {
        //The Enemy moves in a specific area only. It will come after you only if you are in it's chase area and with in the bounds of the collider
        if (Vector3.Distance(target.position, transform.position) <= ChaseRadius && Vector3.Distance(target.position, transform.position) > AttackRadius
            && boundary.bounds.Contains(target.transform.position))
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk || currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
                anim.SetBool("WakeUp", true);
            }
        }
        //The enemy will stop comming after if you leave it's chace area OR if you leave the bounds you have set.
        else if (Vector3.Distance(target.position, transform.position) > ChaseRadius || !boundary.bounds.Contains(target.transform.position))
        {
            anim.SetBool("WakeUp", false);
        }
    }
}
