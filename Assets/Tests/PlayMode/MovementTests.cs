using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

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
        var playerCollider = testObject.AddComponent<BoxCollider2D>();
        playerCollider.size = new Vector2(1, 1); // Set player collider size
        playerCollider.transform.position = new Vector3(0, 5, 0);

        ground.layer = LayerMask.NameToLayer("Terrain");
        var groundCollider = ground.AddComponent<BoxCollider2D>();
        groundCollider.size = new Vector2(50f, 1f); // Set ground collider size
        groundCollider.transform.position = new Vector3(0, -1, 0); // Position ground object above player

        

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
    public IEnumerator MovementNoneTest()
    {
        var position = player.transform.position;

        player.TestHandleMovement(false, false);

        yield return new WaitForSeconds(2f);

        var newPosition = player.transform.position;

        Assert.AreEqual(newPosition.x, position.x);

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

    [UnityTest]
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
    }

    [UnityTest]
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
    }
}