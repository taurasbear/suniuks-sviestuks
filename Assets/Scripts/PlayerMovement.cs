using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Codice.Client.BaseCommands.Update.Fast.Transformers;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
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
    [SerializeField] private float wallSlideDuration = 0.4f;
    private Vector2 respawnPoint;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 8.5f;
    [SerializeField] private float jumpForce = 13f;
    private float wallJumpForce = 8.5f;
    private float lightWallJumpForce = 3f;
    private enum WallJumpKey { None, Left, Right, Up }
    private WallJumpKey wallJumpKey = WallJumpKey.None;


    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    private float wallJumpCount = 0;
    private float jumpTimer = 0f;
    private float jumpCooldown = 1f;
    private enum MovementState { idle, running, jumping, falling }

    bool isMovingRight = false;
    bool isMovingLeft = false;
    bool isMovingUp = false;

    public GameController gameManager;

    // Start is called before the first frame update
    public void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        wallSlideTimer = new Stopwatch();
        SetRespawnPoint(transform.position);
        //  anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (rb.name == "Player1")
        {
            if (!isAlive)
            {
                return;
            }
            //if (IsOnButter())
            //{
            //    gameManager.YouLose();
            //}
            isMovingRight = Input.GetKey(KeyCode.D);
            isMovingLeft = Input.GetKey(KeyCode.A);
            bool jump = Input.GetKeyDown(KeyCode.W);
            HandleMovement(isMovingRight, isMovingLeft);

            HandleFacing();

            //WallSlide(isMovingRight, isMovingLeft);
            Jump(jump, isMovingRight, isMovingLeft);
        }
        if (rb.name == "Player2")
        {
            if (!isAlive)
            {
                return;
            }
            isMovingRight = Input.GetKey(KeyCode.RightArrow);
            isMovingLeft = Input.GetKey(KeyCode.LeftArrow);
            isMovingUp = Input.GetKey(KeyCode.UpArrow);
            bool jump = Input.GetKeyDown(KeyCode.UpArrow);
            HandleMovementKeys(ref isMovingRight, ref isMovingLeft, ref isMovingUp);
            HandleMovement(isMovingRight, isMovingLeft);

            HandleFacing();

            WallSlide(isMovingRight, isMovingLeft);
            Jump(jump, isMovingRight, isMovingLeft);
        }


        // UpdateAnimationState();
    }
    private void HandleMovementKeys(ref bool isMovingRight, ref bool isMovingLeft, ref bool isMovingUp)
    {
        if (isMovingRight && wallJumpKey == WallJumpKey.Right)
        {
            isMovingRight = false;
        }
        else if (isMovingLeft && wallJumpKey == WallJumpKey.Left)
        {
            isMovingLeft = false;
        }
        else if (isMovingUp && wallJumpKey == WallJumpKey.Up)
        {
            isMovingUp = false;
        }
        else
        {
            wallJumpKey = WallJumpKey.None;
        }
    }
    private void HandleMovement(bool isMovingRight, bool isMovingLeft)
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

        if (wallJumpKey == WallJumpKey.None || IsGrounded())
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
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
    public void Jump(bool jump, bool isMovingRight, bool isMovingLeft)
    {
        if (rb.name == "Player2")
        {
            if (jump && IsGrounded())
            {
                // jumpSoundEffect.Play();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (jump && !IsGrounded() && IsWalled() && wallJumpCount < 1)
            {
                //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                //wallJumpCount += 1;
                HandleWallJump(isMovingRight, isMovingLeft);
                wallJumpCount += 1;
            }
            if (!IsWalled())
            {
                wallJumpCount = 0;
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
    private void HandleWallJump(bool isMovingRight, bool isMovingLeft)
    {
        if (!isFacingRight)
        {
            if (isMovingLeft)
            {
                rb.velocity = new Vector2(wallJumpForce, jumpForce);
                wallJumpKey = WallJumpKey.Left;
            }
            else
            {
                rb.velocity = new Vector2(lightWallJumpForce, jumpForce);
                wallJumpKey = WallJumpKey.Up;
            }
        }
        else if (isFacingRight)
        {
            if(isMovingRight)
            {
                rb.velocity = new Vector2(-wallJumpForce, jumpForce);
                wallJumpKey = WallJumpKey.Right;
            }
            else
            {
                rb.velocity = new Vector2(-lightWallJumpForce, jumpForce);
                wallJumpKey = WallJumpKey.Up;
            }
        }
    }

    private void WallSlide(bool isMovingRight, bool isMovingLeft)
    {
        if (isMovingRight || isMovingLeft)
        {
            wallSlideTimer.Restart();
        }
        if (IsWalled() && !IsGrounded() && wallSlideTimer.Elapsed.TotalSeconds < wallSlideDuration)
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




    // Methods for testing
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
    public void SetMovingRight(bool movingRight)
    {
        isMovingRight = movingRight;
    }
    public void SetMovingLeft(bool movingLeft)
    {
        isMovingLeft = movingLeft;
    }
    public void SetRigidBodyName(string name)
    {
        rb.name = name;
    }
    public void TestHandleMovement(bool isMovingRight, bool isMovingLeft)
    {
        HandleMovement(isMovingRight, isMovingLeft);
    }
    public bool GetIsFacingRight()
    {
        return isFacingRight;
    }

    public void GetFlip()
    {
        Flip();
    }

    public void GetHandleFacing()
    {
        HandleFacing();
    }
    public void VelocityChanged(float velocity)
    {
        rb.velocity = new Vector2(velocity, rb.velocity.y);
    }
    public bool GetIsGrounded()
    {
        return IsGrounded();
    }
    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
    public float GetSpeed()
    {
        return rb.velocity.x;
    }

}