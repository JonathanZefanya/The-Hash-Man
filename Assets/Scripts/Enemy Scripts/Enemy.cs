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

public enum EnemyFace
{
    down,
    up,
    right,
    left
}

public class Enemy : MonoBehaviour
{
    [Header("State Machine")]
    public EnemyState currentState;
    public EnemyFace currentFace;

    [Header("Enemy Stats")]
    public EnemyHealth health;
    public string enemyName;
    public float moveSpeed;
    public Vector2 originPosition;
    public SpriteRenderer enemySprite;

    [Header("Effects")]
    public GameObject deathEffect;
    private float deathEffectDelay = 0.6f;
    public LootTable thisLoot;
    public LootTable itemLoot;
    public Color painFlash;
    public Color regularNoFlash;
    public float flashingDuration;
    public int flashCount;

    [Header("Death Signals")]
    public Signals roomSignal;

    private void Awake()
    {
        originPosition = transform.position;
        health = this.gameObject.GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        if(health != null)
        {
            health.currentHealth = health.maxHealth.initialValue;
        }
        transform.position = originPosition;
        currentState = EnemyState.idle;
    }

    public virtual void Update()
    {
        if (health != null)
        {
            if (health.currentHealth <= 0)
            {
                TakeDamage();
            }
        }
    }
    public void TakeDamage()
    {
        if (health.currentHealth <= 0)
        {
            DeathEffect();
            dropLoot();
            if (roomSignal != null)
            {
                roomSignal.Raise();
            }
            this.gameObject.SetActive(false);
            enemySprite.color = regularNoFlash;
        }
    }

    public void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, deathEffectDelay);
        }
    }

    public void dropLoot()
    {
        if (thisLoot != null)
        {
            PowerUp current = thisLoot.specialLoot();
            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }

        if(itemLoot != null)
        {
            ItemPickup now = itemLoot.ItemLoot();
            if (now != null)
            {
                Instantiate(now.gameObject, transform.position, Quaternion.identity);
            }
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            StartCoroutine(HurtCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.GetComponent<Enemy>().currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator HurtCo()
    {
        int temp = 0;
        while (temp < flashCount)
        {
            enemySprite.color = painFlash;
            yield return new WaitForSeconds(flashingDuration);
            enemySprite.color = regularNoFlash;
            yield return new WaitForSeconds(flashingDuration);
            temp++;
        }
    }
}
