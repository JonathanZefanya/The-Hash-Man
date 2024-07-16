using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float knockTime;
    [SerializeField] private string collisionTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        //{
        //    other.GetComponent<pot>().Smash();
        //}

        if(other.gameObject.CompareTag(collisionTag))
        {
            Rigidbody2D hit = other.GetComponentInParent<Rigidbody2D>();
            if(hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);

                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit, knockTime);
                }

                if(other.gameObject.CompareTag("Player") && other.isTrigger)
                {
                    if (other.GetComponentInParent<PlayerMain>().currentState != PlayerState.stagger)
                    {
                        hit.GetComponentInParent<PlayerMain>().currentState = PlayerState.stagger;
                        other.GetComponentInParent<PlayerMain>().Knock(knockTime);
                    }
                }

            }
        }
    } 
}
