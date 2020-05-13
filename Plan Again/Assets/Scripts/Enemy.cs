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
    public FloatValue maxHealth;
    public float Health;
    public int BaseAttack;
    public string EnemyName;
    public float MoveSpeed;

    private void Awake()
    {
        Health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float KnockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody,KnockTime));
        TakeDamage(damage);
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
