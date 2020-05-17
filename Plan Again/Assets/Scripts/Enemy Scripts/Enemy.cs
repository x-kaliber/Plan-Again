using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    [Header("State Machine")]
    public EnemyState currentState;

    [Header("Enemy Stats")]
    public FloatValue maxHealth;
    public float Health;
    public int BaseAttack;
    public string EnemyName;
    public float MoveSpeed;
    public Vector2 homePosition;

    [Header("Death Signal")]
    public Signal roomSignal;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float DeathEffectDelay = 1f;
    public LootTable thisLoot;

    private void Awake()
    {
        Health = maxHealth.initialValue;
    }

    private void OnEnable()
    {

        transform.position = homePosition;
        Health = maxHealth.initialValue;
        currentState = EnemyState.idle;
    }

    private void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            DeathEffect();
            MakeLoot();
            if (roomSignal != null)
            { 
            roomSignal.Raise();
            }
            this.gameObject.SetActive(false);
        }
    }

    private void MakeLoot()
    {
        if(thisLoot != null)
        {
            PowerUp current = thisLoot.LootPowerUp();
            if(current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, DeathEffectDelay);
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
