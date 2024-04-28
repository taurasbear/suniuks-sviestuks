using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratedTests : MonoBehaviour
{
    private GameObject testObject;
	private GameObject ground;
    private GameObject wall;
    private PlayerMovement player;
    [SetUp]
    public void Setup()
    {
        testObject = GameObject.Instantiate(new GameObject());
        ground = GameObject.Instantiate(new GameObject());
        wall = GameObject.Instantiate(new GameObject());

        player = testObject.AddComponent<PlayerMovement>();

    }
    [Test]
    public void player_can_move_left_and_right()
    {
        // Arrange
        player.Start();

        // Act
        player.SetMovingRight(true);
        player.SetMovingLeft(false);
        player.Update();

        // Assert
        Assert.AreEqual(player.GetSpeed(), player.GetMoveSpeed());

        // Act
        player.SetMovingRight(false);
        player.SetMovingLeft(true);
        player.Update();

        // Assert
        Assert.AreEqual(player.GetSpeed(), -player.GetMoveSpeed());
    }
    public void test_handle_movement_sets_velocity_correctly()
    {
        // Arrange
        var playerMovement = new PlayerMovement();
        playerMovement.Start();

        // Act
        playerMovement.TestHandleMovement(true, false);

        // Assert
        Assert.AreEqual(playerMovement.GetMoveSpeed(), playerMovement.GetSpeed());
    }
}
