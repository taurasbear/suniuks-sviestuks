using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerMovement : MonoBehaviour
{
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    // private Animator anim;
    public static bool isAir;
    private bool isWallSliding;
    private float wallSlidingSpeed = 1f;
    private bool isFacingRight = true;
    private float horizontal;


    [SerializeField] private bool isAlive = true;
    private float respawnTime = 1f;

    private Stopwatch wallSlideTimer;
    private Vector2 respawnPoint;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
      
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        wallSlideTimer = new Stopwatch();
        SetRespawnPoint(transform.position);
      //  anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (rb.name == "Player1")
        {
            if(!isAlive)
            {
                return;
            }
            bool isMovingRight = Input.GetKey(KeyCode.D);
            bool isMovingLeft = Input.GetKey(KeyCode.A);
            bool jump = Input.GetKeyDown(KeyCode.W);
            HandleMovement(isMovingRight, isMovingLeft);

            HandleFacing();

            WallSlide(isMovingRight, isMovingLeft);
            Jump(jump);
        }
        if (rb.name == "Player2")
        {
            if (!isAlive)
            {
                return;
            }
            bool isMovingRight = Input.GetKey(KeyCode.RightArrow);
            bool isMovingLeft = Input.GetKey(KeyCode.LeftArrow);
            bool jump = Input.GetKeyDown(KeyCode.UpArrow);
            HandleMovement(isMovingRight, isMovingLeft);
            
            HandleFacing();

            WallSlide(isMovingRight, isMovingLeft);
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
    private void HandleFacing()
    {
        if (isFacingRight && rb.velocity.x < 0)
        {
            Flip();
        }
        else if (!isFacingRight && rb.velocity.x > 0)
        {
            Flip();
        }
    }
    private void Jump(bool jump)
    {
        if (rb.name == "Player2")
        {
            if (jump && IsGrounded() || jump && IsWalled())
            {
                // jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        if (rb.name == "Player1")
        {
            if (jump && IsGrounded())
            {
                // jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

    }

    private void WallSlide(bool isMovingRight, bool isMovingLeft)
    {
        if(isMovingRight || isMovingLeft)
        {
            wallSlideTimer.Restart();
        }
        if(IsWalled() && !IsGrounded() && wallSlideTimer.Elapsed.TotalSeconds < 0.2)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;  
        }
    }

    public void SetRespawnPoint(Vector2 point)
    {
        respawnPoint = point;
    }
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        isAlive = true;
        coll.enabled = true;
        transform.position = respawnPoint;
        
    }
    public void Die()
    {
        isAlive = false;
        coll.enabled = false;
        StartCoroutine(Respawn());
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

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        rb.transform.Rotate(0f, 180f, 0f);
    }




    // Methods to help with testing
    public Vector2 GetRespawnPoint()
    {
        return respawnPoint;
    }
    public bool GetIsAlive()
    {
        return isAlive;
    }
    public bool GetColliderEnabled()
    {
        return coll.enabled;
    }
    public float GetRespawnTime()
    {
        return respawnTime;
    }   
}
