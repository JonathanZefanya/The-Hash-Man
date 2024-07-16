using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHealth : PowerUp
{
    public FloatValue healthPoints;
    public FloatValue playerHealth;
    private float halfHeartMultiplier = 2f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            healthPoints.runtimeValue += 1;
            playerHealth.runtimeValue = healthPoints.runtimeValue * halfHeartMultiplier;
            powerUpSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
