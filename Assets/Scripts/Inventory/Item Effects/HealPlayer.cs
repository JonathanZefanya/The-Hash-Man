using System.Collections;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private float healAmount;
    public void Heal()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerHealth>();
        playerHealth.Heal(healAmount);
    }

    public void CompleteRecover()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerHealth>();
        playerHealth.CompleteRecover();
    }
}
