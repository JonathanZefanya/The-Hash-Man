using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthSystem
{
    [SerializeField] private Signals healthSignal;
    [SerializeField] private FloatValue healthPoints;
    [SerializeField] private float heartMultiplier = 2f;
    [SerializeField] private PauseMenu pauseMenu;

    public override void Damage(float damageAmount)
    {
        base.Damage(damageAmount);
        maxHealth.runtimeValue = currentHealth;
        healthSignal.Raise();
        if(currentHealth <= 0)
        {
            pauseMenu.DeathScreen();
        }
    }
    public override void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > healthPoints.runtimeValue * heartMultiplier)
        {
            currentHealth = healthPoints.runtimeValue * heartMultiplier;
        }
        maxHealth.runtimeValue = currentHealth;
        healthSignal.Raise();
    }
}
