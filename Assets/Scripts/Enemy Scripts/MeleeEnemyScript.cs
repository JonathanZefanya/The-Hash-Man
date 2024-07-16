using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyScript : log
{
    public GameObject HitBoxDown;
    public GameObject HitBoxUp;
    public GameObject HitBoxLeft;
    public GameObject HitBoxRight;

    void Start()
    {
        currentState = EnemyState.idle;
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                anim.SetBool("moving", true);
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
                ChangeState(EnemyState.walk);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger) 
            { 
                StartCoroutine(AttackCo()); 
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("moving", false);
        }
    }

    public IEnumerator AttackCo()
    {
        currentState = EnemyState.attack;
        anim.SetBool("attack", true);
        anim.SetBool("moving", false);
        // Activating hitboxes
        switch (currentFace)
        {
            case EnemyFace.down:
                HitBoxDown.SetActive(true);
                break;
            case EnemyFace.up:
                HitBoxUp.SetActive(true);
                break;
            case EnemyFace.right:
                HitBoxRight.SetActive(true);
                break;
            case EnemyFace.left:
                HitBoxLeft.SetActive(true);
                break;
        }
        yield return new WaitForSeconds(0.5f);
        currentState = EnemyState.idle;
        anim.SetBool("attack", false);

        // Deactivating hitboxes
        HitBoxDown.SetActive(false);
        HitBoxUp.SetActive(false);
        HitBoxLeft.SetActive(false);
        HitBoxRight.SetActive(false);
    }
}
