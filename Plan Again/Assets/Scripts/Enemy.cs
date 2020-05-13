using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public int Health;
    public int BaseAttack;
    public string EnemyName;
    public float MoveSpeed;

    public void Knock(Rigidbody2D myRigidbody, float KnockTime)
    {
        StartCoroutine(KnockCo(myRigidbody,KnockTime));
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float KnockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(KnockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
