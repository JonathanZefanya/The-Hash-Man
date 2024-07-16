
public class EnemyHealth : HealthSystem
{
    private void OnEnable()
    {
        maxHealth.runtimeValue = maxHealth.initialValue;
    }
    public override void Damage(float damageAmount)
    {
        base.Damage(damageAmount);
        maxHealth.runtimeValue = currentHealth;
    }

}
