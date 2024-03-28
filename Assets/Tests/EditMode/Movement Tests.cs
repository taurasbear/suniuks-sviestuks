using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTests
{
    [Test]
    public void RespawnPointTest()
    {
        PlayerMovement player = new PlayerMovement();
        Vector2 newRespawnPoint = new Vector2(1, 1); // could be paramiterized
        player.SetRespawnPoint(newRespawnPoint);
        Assert.AreEqual(newRespawnPoint, player.GetRespawnPoint());
    }

}
