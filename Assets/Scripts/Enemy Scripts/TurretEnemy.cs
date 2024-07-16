using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : log
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float shootDelay;
    [SerializeField] private float shootDelaySeconds;
    [SerializeField] private bool canShoot = true;

    public override void Update()
    {
        base.Update();
        if(canShoot == false)
        {
            shootDelaySeconds -= Time.deltaTime;
            if (shootDelaySeconds <= 0)
            {
                canShoot = true;
                shootDelaySeconds = shootDelay;
            }
        }
    }

    public override void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && Vector3.Distance(target.position, transform.position) > attackRadius)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                if (canShoot)
                {
                    Vector3 tempVector = target.transform.position - transform.position;
                    GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                    current.GetComponent<Projectile>().Shoot(tempVector);
                    canShoot = false;
                    ChangeState(EnemyState.walk);
                    anim.SetBool("wakeUp", true);
                }
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            anim.SetBool("wakeUp", false);
        }
    }
}
