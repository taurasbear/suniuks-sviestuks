using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTests
{
    private GameObject testObject;
    private PlayerMovement player;
    [SetUp]
    public void Setup()
    {
        testObject = GameObject.Instantiate(new GameObject());
        player = testObject.AddComponent<PlayerMovement>();
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
        // player moves to right
        player.TestHandleMovement(true, false);

        yield return new WaitForSeconds(2f);
        
        // Assert
        var newPosition = player.transform.position;
        Assert.Greater(newPosition.x, position.x);

        Debug.Log($"Position: {position}");
        Debug.Log($"New Position: {newPosition}");
    }

    [UnityTest]
    public IEnumerator MovementLeftTest()
    {
        var position = player.transform.position;
        // player moves to right
        player.TestHandleMovement(false, true);

        yield return new WaitForSeconds(2f);

        // Assert
        var newPosition = player.transform.position;
        Assert.Less(newPosition.x, position.x);

        Debug.Log($"Position: {position}");
        Debug.Log($"New Position: {newPosition}");
    }

    [UnityTest]
    public IEnumerator MovementNoneTest()
    {
        var position = player.transform.position;
        // player moves to right
        player.TestHandleMovement(false, false);

        yield return new WaitForSeconds(2f);

        // Assert
        var newPosition = player.transform.position;
        Assert.AreEqual(newPosition.x, position.x);

        Debug.Log($"Position: {position}");
        Debug.Log($"New Position: {newPosition}");
    }

}