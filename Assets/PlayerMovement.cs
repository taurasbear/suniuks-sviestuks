using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    // private Animator anim;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
      
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
      //  anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (rb.name == "Player1")
        {
            bool isMovingRight = Input.GetKey(KeyCode.D);
            bool isMovingLeft = Input.GetKey(KeyCode.A);
            bool jump = Input.GetKeyDown(KeyCode.W);
            HandleMovement(isMovingRight, isMovingLeft);
            Jump(jump);
        }
        if (rb.name == "Player2")
        {
            bool isMovingRight = Input.GetKey(KeyCode.RightArrow);
            bool isMovingLeft = Input.GetKey(KeyCode.LeftArrow);
            bool jump = Input.GetKeyDown(KeyCode.UpArrow);
            HandleMovement(isMovingRight, isMovingLeft);
            Jump(jump);
        }

        // UpdateAnimationState();
    }
    private void HandleMovement(bool isMovingRight,bool isMovingLeft)
    {
        float horizontalInput = 0f;
        if (isMovingRight)
        {
            horizontalInput = 1f;
        }
        else if (isMovingLeft)
        {
            horizontalInput = -1f;
        }

        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }
    private void Jump(bool jump)
    {

        if (jump && IsGrounded())
        {
            // jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        
    }

    //private void UpdateAnimationState()
    //{
    //    MovementState state;

    //    if (dirX > 0f)
    //    {
    //        state = MovementState.running;
    //        sprite.flipX = false;
    //    }
    //    else if (dirX < 0f)
    //    {
    //        state = MovementState.running;
    //        sprite.flipX = true;
    //    }
    //    else
    //    {
    //        state = MovementState.idle;
    //    }

    //    if (rb.velocity.y > .1f)
    //    {
    //        state = MovementState.jumping;
    //    }
    //    else if (rb.velocity.y < -.1f)
    //    {
    //        state = MovementState.falling;
    //    }

    //    anim.SetInteger("state", (int)state);
    //}

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
