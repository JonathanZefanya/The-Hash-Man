using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCState
{
    idle,
    walk
}
public class MovingNPC : Interactable
{
    public NPCState currentState;
    private Vector3 directionVector;
    private Transform npcTransform;
    public float speed;
    public float minMoveTime; // public reference value for move time
    public float maxMoveTime;
    private float moveTimes; // value that will change
    public float minWaitTime; // public reference value for wait time
    public float maxWaitTime;
    private float waitTimes; // value that will change
    private Rigidbody2D npcRigidbody;
    private Animator anim;

    public Collider2D boundary;

    // Start is called before the first frame update
    void Start()
    {
        waitTimes = Random.Range(minWaitTime, maxWaitTime);
        moveTimes = Random.Range(minMoveTime, maxMoveTime);
        currentState = NPCState.walk;
        anim = GetComponent<Animator>();
        npcTransform = GetComponent<Transform>();
        npcRigidbody = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    // Update is called once per frame
    public void Update()
    {
        if (currentState == NPCState.walk)
        {
            moveTimes -= Time.deltaTime;
            if (moveTimes <= 0)
            {
                moveTimes = Random.Range(minMoveTime, maxMoveTime);
                currentState = NPCState.idle;
            }
        }
        else
        {
            waitTimes -= Time.deltaTime;
            if (waitTimes <= 0)
            {
                ChangeDirectionMidway();
                waitTimes = Random.Range(minWaitTime, maxWaitTime);
                currentState = NPCState.walk;
            }
        }
    }

    private void FixedUpdate()
    {
        if(currentState == NPCState.walk)
        {
            if (!playerInRange)
            {
                Move();
                anim.SetBool("walking", true);
            }
            else
            {
                currentState = NPCState.idle;
            }
        }
        else
        {
            anim.SetBool("walking", false);
        }
    }

    private void ChangeDirectionMidway()
    {
        Vector3 temp = directionVector;
        ChangeDirection();
        int loops = 0;
        while (temp == directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }
    }

    private void Move()
    {
        Vector3 temp = npcTransform.position + directionVector * speed * Time.deltaTime;
        if (boundary.bounds.Contains(temp))
        {
            npcRigidbody.MovePosition(temp);
        }
        else
        {
            ChangeDirection();
        }
        npcRigidbody.MovePosition(npcTransform.position + directionVector * speed * Time.deltaTime);
    }

    void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                // Walk Down
                directionVector = Vector3.down;
                break;
            case 1:
                // Walk Up
                directionVector = Vector3.up;
                break;
            case 2:
                // Walk left
                directionVector = Vector3.left;
                break;
            case 3:
                // Walk right
                directionVector = Vector3.right;
                break;
            default:
                break;
        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        anim.SetFloat("moveX", directionVector.x);
        anim.SetFloat("moveY", directionVector.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ChangeDirectionMidway();
    }
}
