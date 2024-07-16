using System.Collections;
using UnityEngine;

// Different player states
public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

// Where the player is facing
public enum PlayerFace
{
    up,
    down,
    left,
    right,
}

public class PlayerMain : MonoBehaviour
{
    public PlayerState currentState;
    public PlayerFace currentFace;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;  
    [SerializeField] private Signals playerHit;
    [SerializeField] private Signals spendArrow;
    private PlayerHealth playerHealth;
    public Vector2 playerPosition;
    public VectorValue startingPosition;
    public GameObject projectile;
    public SpriteRenderer playerSprite;

    [Header ("Combat Stuffs")]
    [SerializeField] private GameObject HitBoxDown;
    [SerializeField] private GameObject HitBoxUp;
    [SerializeField] private GameObject HitBoxRight;
    [SerializeField] private GameObject HitBoxLeft;
    public Color painFlash;
    public Color regularNoFlash;
    public float flashingDuration;
    public int flashCount;
    public Collider2D triggerCollider;

    [Header("Inventory Stuffs")]
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;

    [Header("Items and Projectiles")]
    public Item bow;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        currentFace = PlayerFace.down;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        transform.position = startingPosition.runtimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = transform.position;
        // Player attacking
        if (currentState == PlayerState.interact)
        {
            return;
        }

        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if(Input.GetButtonDown("secondary attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            if(playerInventory.ItemCheck(bow))
            {
                StartCoroutine(SecondaryAttackCo());
            }
        }
    }

    void FixedUpdate()
    {
        startingPosition.runtimeValue = playerPosition;
        // Player in an interaction
        if (currentState == PlayerState.interact)
        {
            return;
        }
        // Player Movement
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo() // Attacking animation coroutine
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        // Activating hitboxes
        switch (currentFace)
        {
            case PlayerFace.down:
                HitBoxDown.SetActive(true);
                break;
            case PlayerFace.up:
                HitBoxUp.SetActive(true);
                break;
            case PlayerFace.right:
                HitBoxRight.SetActive(true);
                break;
            case PlayerFace.left:
                HitBoxLeft.SetActive(true);
                break;
        }

        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.3f);
        if(currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }

        // Deactivating hitboxes
        HitBoxDown.SetActive(false);
        HitBoxUp.SetActive(false);
        HitBoxLeft.SetActive(false);
        HitBoxRight.SetActive(false);

    }

    private IEnumerator SecondaryAttackCo() // Attacking animation coroutine
    {
        currentState = PlayerState.attack;
        yield return null;
        SpawnArrow();
        yield return new WaitForSeconds(0.3f);
        if (currentState != PlayerState.interact)
        {
            currentState = PlayerState.walk;
        }
    }

    private void SpawnArrow()
    {
        if (playerInventory.arrow > 0)
        {
            spendArrow.Raise();
            Vector2 arrowDirection = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.SetUp(arrowDirection, ArrowFacing());
        }
    }

    Vector3 ArrowFacing()
    {
        float arrowFace = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX"))*Mathf.Rad2Deg;
        return new Vector3(0, 0, arrowFace-45);
    }

        public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.interact)
            {
                animator.SetBool("receive item", true);
                currentState = PlayerState.interact;
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("receive item", false);
                currentState = PlayerState.idle;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }

    void UpdateAnimationAndMove() // Moving animation
    {
                if(change != Vector3.zero)
        {
            MoveCharacter();
            UpdateFace();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
{
    animator.SetBool("moving", false);
}
    }

    private void UpdateFace()
    {
        if(change.x > 0)
        {
            currentFace = PlayerFace.right;
            return;
        }
        else if(change.x < 0)
        {
            currentFace = PlayerFace.left;
            return;
        }
        if(change.y > 0)
        {
            currentFace = PlayerFace.up;
            return;
        }
        else if(change.y < 0)
        {
            currentFace = PlayerFace.down;
            return;
        }
    }

    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
    }

    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
    }

    private IEnumerator KnockCo(float knockTime)
    {
        //playerHit.Raise();
        if (myRigidbody != null)
        {
            StartCoroutine(HurtCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private IEnumerator HurtCo()
    {
        int temp = 0;
        triggerCollider.enabled = false;
        while(temp < flashCount)
        {
            playerSprite.color = painFlash;
            yield return new WaitForSeconds(flashingDuration);
            playerSprite.color = regularNoFlash;
            yield return new WaitForSeconds(flashingDuration);
            temp++;
        }
        triggerCollider.enabled = true;
    }
}
