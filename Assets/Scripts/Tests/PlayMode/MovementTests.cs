using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UIElements;
using TMPro;

public class MovementTests
{
  private GameObject testObject;
  private GameObject camera;
  private GameObject ground;
  private GameObject wall;
  private PlayerMovement player;
  [SetUp]
  public void Setup()
  {
    testObject = GameObject.Instantiate(new GameObject());
    ground = GameObject.Instantiate(new GameObject());
    wall = GameObject.Instantiate(new GameObject());
    /*camera = GameObject.Instantiate(new GameObject());
    camera.AddComponent<Camera>();
    camera.transform.position = new Vector3(0f, 0f, -5f);*/

    player = testObject.AddComponent<PlayerMovement>();
    //var playerCollider = testObject.AddComponent<BoxCollider2D>();
    //splayerCollider.size = new Vector2(1, 1); // Set player collider size
    //playerCollider.transform.position = new Vector3(0, 5, 0);

    /*ground.layer = LayerMask.NameToLayer("Terrain");
    var groundCollider = ground.AddComponent<BoxCollider2D>();
    groundCollider.size = new Vector2(50f, 1f); // Set ground collider size
    groundCollider.transform.position = new Vector3(0, -1, 0); // Position ground object above player*/



  }
  [UnityTest]
  public IEnumerator DeathTest()
  {
    player.Die();
    Assert.AreEqual(false, player.GetIsAlive());
    Assert.AreEqual(false, player.GetColliderEnabled());
    yield return null;
  }

  [UnityTest]
  public IEnumerator RespawnTest()
  {
    player.Die();
    yield return new WaitForSeconds(player.GetRespawnTime());
    Assert.AreEqual(true, player.GetIsAlive());
    Assert.AreEqual(true, player.GetColliderEnabled());
  }

  [UnityTest]
  public IEnumerator MovementRightTest()
  {
    var position = player.transform.position;
    player.TestHandleMovement(true, false);

    yield return new WaitForSeconds(2f);

    var newPosition = player.transform.position;
    Assert.Greater(newPosition.x, position.x);

    Debug.Log($"Position: {position}");
    Debug.Log($"New Position: {newPosition}");
  }

  [UnityTest]
  public IEnumerator MovementLeftTest()
  {
    var position = player.transform.position;

    player.TestHandleMovement(false, true);

    yield return new WaitForSeconds(2f);

    var newPosition = player.transform.position;
    Assert.Less(newPosition.x, position.x);

    Debug.Log($"Position: {position}");
    Debug.Log($"New Position: {newPosition}");
  }


  [UnityTest]
  public IEnumerator FlippingLeftTest()
  {
    player.GetFlip();
    yield return new WaitForSeconds(2f);

    bool changedDirection = player.GetIsFacingRight();
    Assert.AreEqual(false, changedDirection);

    Debug.Log($"Facing Right: {changedDirection}");
  }

  [UnityTest]
  public IEnumerator FlippingRightTest()
  {
    player.GetFlip();
    yield return new WaitForSeconds(2f);

    player.GetFlip();
    yield return new WaitForSeconds(2f);

    bool changedDirection = player.GetIsFacingRight();
    Assert.AreEqual(true, changedDirection);

    Debug.Log($"Facing Right: {changedDirection}");
  }

  [UnityTest]
  public IEnumerator HandleFacingLeftTest()
  {
    player.VelocityChanged(-2f);
    yield return new WaitForSeconds(2f);

    player.GetHandleFacing();
    yield return new WaitForSeconds(2f);

    bool changedDirection = player.GetIsFacingRight();
    Assert.AreEqual(false, changedDirection);

    Debug.Log($"Facing Right: {changedDirection}");
  }
  [UnityTest]
  public IEnumerator HandleFacingRightTest()
  {
    player.VelocityChanged(-2f);
    player.GetHandleFacing();

    yield return new WaitForSeconds(2f);

    player.VelocityChanged(2f);
    player.GetHandleFacing();

    bool changedDirection = player.GetIsFacingRight();
    Assert.AreEqual(true, changedDirection);

    Debug.Log($"Facing Right: {changedDirection}");
  }
  /*[UnityTest]
  public IEnumerator PlayerIsNotGroundedWhenNotTouchingGround()
  {
      // Move the player far above the ground
      player.transform.position = new Vector3(0, 5f, 0);

      // Wait for one frame to ensure physics updates
      yield return null;
      bool isTouching = player.GetIsGrounded();
      // Check if the player is not grounded
      Assert.IsFalse(isTouching);
      Debug.Log($"IsTouching: {isTouching}");
  }*/
  /*[UnityTest]
     public IEnumerator MovementNoneTest()
     {
         var position = player.transform.position;

         player.TestHandleMovement(false, false);

         yield return new WaitForSeconds(2f);

         var newPosition = player.transform.position;

         Assert.AreEqual(newPosition.x, position.x);

         Debug.Log($"Position: {position}");
         Debug.Log($"New Position: {newPosition}");
     }*/


  /*[UnityTest]
  public IEnumerator PlayerIsGroundedWhenTouchingGround()
  {
      // Move the player slightly above the ground
      player.transform.position = new Vector3(0, -0.999f, 0);
      Debug.Log($"--> Player pos: {player.transform.position}");
      Debug.Log($"--> Ground pos: {ground.transform.position}");
      Debug.Log($"--> Is Grounded: {player.GetIsGrounded()}");
      yield return new WaitForSeconds(1f);
      // Check if the player is grounded
      bool isGrounded = player.GetIsGrounded();
      yield return new WaitForSeconds(1f);
      Assert.IsTrue(isGrounded);
  }*/
  //////// END TO END TESTING /////
}

public class PlayerMovement_EndToEnd
{
  GameObject Message;
  [UnityTest]
  public IEnumerator PlayersReachDoor_YouWinUIActivates()
  {
    // Load the test scene
    yield return SceneManager.LoadSceneAsync("TestLevel1");

    // Get references to player objects and door location
    GameObject player1 = GameObject.Find("Player1");
    GameObject player2 = GameObject.Find("Player2");
    GameObject door = GameObject.Find("Door");
    Vector3 doorPosition = door.transform.position;
    // Simulate player movement to the door location
    //player1.transform.position = door.transform.position;
    //player2.transform.position = door.transform.position;

    yield return new WaitForSeconds(0.1f);

    while ((player1.transform.position.x != doorPosition.x) || (player2.transform.position.x != doorPosition.x))
    {
      player2.transform.position = Vector3.MoveTowards(player2.transform.position, door.transform.position, 0.1f);
      player1.transform.position = Vector3.MoveTowards(player1.transform.position, door.transform.position, 0.1f);
      yield return null;
    }
    // Wait for a frame to allow Unity to update
    yield return null;

    // Check if "YouWin" UI activates
    GameObject youWinUI = GameObject.Find("YouWin");
    Assert.IsTrue(youWinUI.activeSelf);
  }

  [UnityTest]
  public IEnumerator Player2_Jump_Off_Player1()
  {
    // Load the test scene
    yield return SceneManager.LoadSceneAsync("TestLevel2");

    // Get Test Log Object
    Message = GameObject.Find("TestLog/Log/Message");
    TextMeshProUGUI messageText = Message.GetComponent<TextMeshProUGUI>();

    // Get references to player objects and door location
    GameObject player1 = GameObject.Find("Player1");
    GameObject player2 = GameObject.Find("Player2"); 

    Vector2 firstPlayer2Pos = player1.transform.position;

    PlayerMovement player2Movement = player2.GetComponent<PlayerMovement>();

    // Enable testing
    player2Movement.isTest = true;

    messageText.text = "waiting...";
    yield return new WaitForSeconds(2f);

    player2Movement.SetIsJump(true);
    yield return new WaitForSeconds(0.1f);

    player2Movement.SetIsMovingLeft(true);
    player2Movement.SetIsJump(false);

    float margin = 0.1f;

    while(Mathf.Abs(player1.transform.position.x - player2.transform.position.x) >= margin)
    {
      messageText.text = "trying to allign";
      yield return null;
    }
    player2Movement.SetIsMovingLeft(false);

    // Wait for a frame to allow Unity to update
    messageText.text = $"waiting on top. player1 position.x: {player1.transform.position.x}, player2 position.x: {player2.transform.position.x}, difference:{Mathf.Abs(player1.transform.position.x - player2.transform.position.x)}";
    yield return new WaitForSeconds(4f);
    
    Vector2 player2PosOnPlayer1 = player2.transform.position;

    messageText.text = "1st check";
    yield return new WaitForSeconds(2f);
    Assert.Greater(player2PosOnPlayer1.y, firstPlayer2Pos.y);

    player2Movement.SetIsJump(true);
    messageText.text = "2nd check";
    yield return new WaitForSeconds(0.3f);
    player2Movement.SetIsJump(false);

    Assert.Greater(player2.transform.position.y, player2PosOnPlayer1.y);

    player2Movement.isTest = false;

  }

  [UnityTest]
  public IEnumerator Player2_WallJump()
  {
    // Load the test scene
    yield return SceneManager.LoadSceneAsync("TestLevel3");

    // Get references to player objects and door location
    GameObject player2 = GameObject.Find("Player2");

    Vector2 oldPlayer2Pos = player2.transform.position;

    PlayerMovement player2Movement = player2.GetComponent<PlayerMovement>();

    yield return new WaitForSeconds(2f);

    //Enable testing
    player2Movement.isTest = true;

    player2Movement.SetIsMovingLeft(true);
    yield return new WaitForSeconds(0.02f);

    player2Movement.SetIsJump(true);
    yield return new WaitForSeconds(0.05f);
    player2Movement.SetIsJump(false);

    yield return new WaitForSeconds(0.4f);
    player2Movement.SetIsJump(true);

    yield return new WaitForSeconds(0.1f);
    player2Movement.SetIsJump(false);

    // Wait for a frame to allow Unity to update
    yield return new WaitForSeconds(0.4f);

    Assert.IsTrue(!player2Movement.IsGrounded());
    Assert.IsTrue(player2Movement.GetRbVelocity().x != 0);
    Assert.IsTrue(player2Movement.GetRbVelocity().y != 0);

    player2Movement.isTest = false;

  }
}

