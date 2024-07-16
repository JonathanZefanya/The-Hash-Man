using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myRigidbody;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetUp(Vector2 velocity, Vector3 direction)
    {
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Room") && !other.gameObject.CompareTag("invisBoundary"))
        {
            Destroy(this.gameObject);
        }
    }
}
