using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTests
{
    [UnityTest]
    public IEnumerator DeathRespawn()
    {
        var gameObject = new GameObject();
        var player1 = gameObject.AddComponent<PlayerMovement>();
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        player1.Die();
        Assert.AreEqual(false, player1.GetIsAlive());
        Assert.AreEqual(false, player1.GetColliderEnabled());
        yield return new WaitForSeconds(player1.GetRespawnTime());
        Assert.AreEqual(true, player1.GetIsAlive());
        Assert.AreEqual(true, player1.GetColliderEnabled());
        
    }
}
