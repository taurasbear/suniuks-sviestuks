using UnityEngine;

public class WallJumpScript : MonoBehaviour
{
    public float jumpForce = 10f;
    public float wallJumpXLimit = 0.1f; // Minimum x-axis velocity to allow wall jump
    public float wallJumpTimeWindow = 0.2f; // Time window to allow wall jump after leaving the wall
    public LayerMask wallLayer;

    private bool isTouchingWall = false;
    private bool canWallJump = false;
    private float lastTimeWallJumped;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if player can wall jump again
        if (canWallJump && Time.time - lastTimeWallJumped > wallJumpTimeWindow)
        {
            canWallJump = false;
        }

        // Wall jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canWallJump && Mathf.Abs(rb.velocity.x) < wallJumpXLimit)
            {
                // Wall jump
                rb.velocity = new Vector2(-rb.velocity.x, jumpForce); // Flip direction of the velocity
                canWallJump = false;
                lastTimeWallJumped = Time.time;
            }
        }
    }

    void FixedUpdate()
    {
        // Check if player is touching the wall
        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 0.6f, wallLayer);

        // Enable wall jump if touching wall
        if (isTouchingWall)
        {
            canWallJump = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Disable wall jump if touching ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            canWallJump = false;
        }
    }
}
