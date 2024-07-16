using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public FloatValue maxHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth.runtimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Heal(float healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth.runtimeValue)
        {
            currentHealth = maxHealth.runtimeValue;
        }
    }

    public virtual void CompleteRecover()
    {
        currentHealth = maxHealth.runtimeValue;
    }

    public virtual void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if(currentHealth < 0)
        {
            currentHealth = 0;
        }
    }

    public virtual void InstantKill()
    {
        currentHealth = 0;
    }
}
