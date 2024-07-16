using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class DamageSystem : MonoBehaviour
{
    public float damage;
    [SerializeField] private string collisionTag;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(collisionTag) && other.isTrigger) 
        {
            HealthSystem temp = other.GetComponent<HealthSystem>();
            if (temp)
            {
                temp.Damage(damage);
            }
        }
    }
}
