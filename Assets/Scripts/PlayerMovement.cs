using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

///////////////////////////////////////////////////// HEADER VARIABLES /////////////////////////////////////////////////////////////
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
  //Player body
  private BoxCollider2D coll;
  private SpriteRenderer sprite;
  //Wall and being in air
  public static bool isAir;
  private bool isWallSliding;
  private bool isFacingRight = true;
  private float wallSlidingSpeed = 1f;
  private float horizontal;
  private float wallJumpForce = 8.5f;
  private float lightWallJumpForce = 3f;
  private Stopwatch wallSlideTimer;
  [SerializeField] private float wallSlideDuration = 0.4f;
  [SerializeField] private Transform wallCheck;
  [SerializeField] private LayerMask wallLayer;

  private float wallJumpCount = 0;
  private float jumpTimer = 0f;
  private float jumpCooldown = 1f;

  //Death and respawn
  [SerializeField] private bool isAlive = true;
  private float respawnTime = 1f;
  private Vector2 respawnPoint;

  //Movement
  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private LayerMask jumpableGround;
  [SerializeField] private float moveSpeed = 8.5f;
  [SerializeField] private float jumpForce = 13f;

  //Enum
  private enum WallJumpKey { None, Left, Right, Up }
  private WallJumpKey wallJumpKey = WallJumpKey.None;
  private enum MovementState { idle, running, jumping, falling }

  bool isMovingRight = false;
  bool isMovingLeft = false;
  bool isMovingUp = false;
  bool jump = false;

  // For frozing
  bool isFrozen = false;
  private float warmUpDuration = 10f; // Duration in seconds for warming up
  private float warmUpRadius = 1.5f; // Adjust this value as needed
  private bool isNearOtherPlayer = false;
  private Color freezeColor = new Color(0.2f, 0.5f, 0.6f, 1f);

  // Dog check
  [SerializeField] private Transform dogCheck;
  [SerializeField] private LayerMask dogLayer;

  // For sounds
  public AudioSource dogJumping;
  public AudioSource dogWalking;
  public AudioSource butterJumping;
  public AudioSource butterWalking;
  public AudioSource sviestuksSuniuks;
  private bool isDogSoundPlaying = false;
  private bool isWalkPlaying = false;
  private Stopwatch stopWalkTimer;

  //For PlayMode tests
  public bool isTest { get; set; } = false;

  //Animator
  private Animator anim;

  public GameController gameManager;

  //////////////////////////////////////////////////// PROGRAM START /////////////////////////////////////////////////////////////
  /////////////////////////////////////////////////////// MAIN //////////////////////////////////////////////////////////////////
  // Start is called before the first frame update
  public void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    coll = GetComponent<BoxCollider2D>();
    sprite = GetComponent<SpriteRenderer>();
    wallSlideTimer = new Stopwatch();
    stopWalkTimer = new Stopwatch();
    SetRespawnPoint(transform.position);

    if(rb.name == "Player2")
    {
      butterWalking.pitch = 1.1f;
    }
    anim = GetComponent<Animator>();
    UnityEngine.Debug.Log(anim);
  }

  // Update is called once per frame
  public void Update()
  {
    horizontal = Input.GetAxisRaw("Horizontal");

    if (rb.name == "Player1")
    {
      rb.constraints = RigidbodyConstraints2D.FreezeRotation;
      if (!isAlive)
      {
        return;
      }
      if (!isTest)
      {
        isMovingRight = Input.GetKey(KeyCode.D);
        isMovingLeft = Input.GetKey(KeyCode.A);
        jump = Input.GetKeyDown(KeyCode.W);
      }

      HandleMovement(isMovingRight, isMovingLeft);
      HandleFacing();
      Jump(jump, isMovingRight, isMovingLeft);
    }
    if (rb.name == "Player2")
    {
      if (!isAlive)
      {
        return;
      }
      if (!isTest)
      {
        isMovingRight = Input.GetKey(KeyCode.RightArrow);
        isMovingLeft = Input.GetKey(KeyCode.LeftArrow);
        isMovingUp = Input.GetKey(KeyCode.UpArrow);
        jump = Input.GetKeyDown(KeyCode.UpArrow);
      }

      HandleMovementKeys(ref isMovingRight, ref isMovingLeft, ref isMovingUp);
      HandleMovement(isMovingRight, isMovingLeft);

      HandleFacing();

      WallSlide(isMovingRight, isMovingLeft);
      Jump(jump, isMovingRight, isMovingLeft);
      PlayOnTopSound();
    }
    HandleFreeze();
    // UpdateAnimationState();
  }
  //////////////////////////////////////////////////// END OF MAIN /////////////////////////////////////////////////////////////
  //////////////////////////////////////////////////// MOVEMENT /////////////////////////////////////////////////////////////
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
    else if (!isTest)
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
      HandleWalkSound();
    }
    else if (isMovingLeft)
    {
      horizontalInput = -1f;
      HandleWalkSound();
    }
    else
    {
      HandleStopWalk();
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
  //////////////////////////////////////////////////// WALL JUMP AND JUMP /////////////////////////////////////////////////////////////
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
        PlayButterJump();
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
        PlayDogJump();
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
      if (isMovingRight)
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
  /////////// ICE ICE BABY ////////////////
  public void HandleIceTerrainInteraction(Collider2D iceTerrainCollider)
  {
    // UnityEngine.Debug.Log("HandleIceTerrainInteraction called");

    if (rb.name == "Player2" && !isFrozen)
    {
      // Freeze the player character
      rb.constraints = RigidbodyConstraints2D.FreezeAll;
      isFrozen = true;
      sprite.color = freezeColor;

      // UnityEngine.Debug.Log("Player 2 is frozen");
    }
  }
  void HandleFreeze()
  {
    if (rb.name == "Player2")
    {
      // Check if the player character is near the other player and if it's frozen
      if (IsNearOtherPlayer("Player1") && isFrozen)
      {
        anim.SetBool("Warming", true);
        // Decrease warm-up duration
        warmUpDuration -= Time.deltaTime;

        // Check if warm-up duration has elapsed
        if (warmUpDuration <= 0f)
        {
          // Warm up the player character
          WarmUp();
          warmUpDuration = 10f; // Reset warm-up duration
        }
      }
    }
  }
  
  private bool IsNearOtherPlayer(string otherPlayerName)
  {
    GameObject otherPlayer = GameObject.Find(otherPlayerName);
    anim = otherPlayer.GetComponent<Animator>();
    if (otherPlayer != null)
    {
      float distance = Vector2.Distance(transform.position, otherPlayer.transform.position);
      return distance <= warmUpRadius;
    }
    return false;
  }

  public void WarmUp()
  {
    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    isFrozen = false;
    sprite.color = Color.white;

    anim.SetBool("Warming", false);
  }


  //////////////////////////////////////////////////// RESPAWN AND DEATH /////////////////////////////////////////////////////////////
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



  //////////////////////////////////////////////////// SOUNDS /////////////////////////////////////////////////////////////
  public void PlayOnTopSound()
  {
    if (isOnDog() && !isDogSoundPlaying)
    {
      sviestuksSuniuks.Play();
      isDogSoundPlaying = true;
    }
    else if (!isOnDog())
    {
      isDogSoundPlaying = false;
    }
  }
  public void PlayDogJump()
  {
    dogJumping.Play();
    HandleStopWalk();
  }
  public void PlayDogWalk()
  {
    dogWalking.Play();
  }

  public void PlayButterJump()
  {
    butterJumping.Play();
    HandleStopWalk();
  }

  public void PlayButterWalk()
  {
    butterWalking.Play();
  }

  public void HandleWalkSound()
  {
    if (rb.name == "Player1" && !isWalkPlaying && IsGrounded() && stopWalkTimer.Elapsed.TotalMilliseconds > 200)
    {
      PlayDogWalk();
      isWalkPlaying = true;
    }
    else if (rb.name == "Player2" && !isWalkPlaying && IsGrounded() && stopWalkTimer.Elapsed.TotalMilliseconds > 200)
    {
      PlayButterWalk();
      isWalkPlaying = true;
    }
  }

  public void HandleStopWalk()
  {
    if (rb.name == "Player1")
    {
      dogWalking.Stop();
      isWalkPlaying = false;
      stopWalkTimer.Restart();
    }
    else if (rb.name == "Player2")
    {
      butterWalking.Stop();
      isWalkPlaying = false;
      stopWalkTimer.Restart();
    }
  }

  //////////////////////////////////////////////////// ANIMATIONS /////////////////////////////////////////////////////////////
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

  //////////////////////////////////////////////////// BOOL CHECKING /////////////////////////////////////////////////////////////
  public bool IsGrounded()
  {
    return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
  }

  private bool IsWalled()
  {
    return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
  }
  private bool isOnDog()
  {
    return Physics2D.OverlapCircle(dogCheck.position, 0.2f, dogLayer);
  }
  private void Flip()
  {
    isFacingRight = !isFacingRight;
    rb.transform.Rotate(0f, 180f, 0f);
  }




  //////////////////////////////////////////////////// FOR TESTING /////////////////////////////////////////////////////////////
  public bool GetIsMovingUp()
  {
    return isMovingUp;
  }
  public bool GetIsMovingLeft()
  {
    return isMovingLeft;
  }
  public bool GetIsMovingRight()
  {
    return isMovingRight;
  }
  public bool GetIsJump()
  {
    return jump;
  }
  public void SetIsMovingUp(bool flag)
  {
    isMovingUp = flag;
  }
  public void SetIsMovingLeft(bool flag)
  {
    isMovingLeft = flag;
  }
  public void SetIsMovingRight(bool flag)
  {
    isMovingRight = flag;
  }
  public void SetIsJump(bool flag)
  {
    jump = flag;
  }
  public Vector2 GetRbVelocity()
  {
    return rb.velocity;
  }
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
  public float GetMoveSpeed()
  {
    return moveSpeed;
  }
  public float GetSpeed()
  {
    return rb.velocity.x;
  }
  //////////////////////////////////////////////////// END /////////////////////////////////////////////////////////////
}