using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NUnit;
using UnityEngine.UI;
using UnityEngine.TestTools;
using NUnit.Framework;

public class UITests : MonoBehaviour
{
	private GameObject testObject;
	private GameObject camera;
	private GameObject ground;
	private GameObject wall;
	private PlayerMovement player;

	[Test]
	public void Back_LoadsPreviousScene()
	{
		// Arrange
		var initialSceneIndex = SceneManager.GetActiveScene().buildIndex;
		var chooseLevel = new GameObject().AddComponent<ChooseLevel>();

		// Act
		chooseLevel.Back();

		// Assert
		Assert.AreEqual(initialSceneIndex-1, SceneManager.GetActiveScene().buildIndex);
	}

	[Test]
	public void Level1_LoadsNextScene()
	{
		// Arrange
		var initialSceneIndex = SceneManager.GetActiveScene().buildIndex;
		var chooseLevel = new GameObject().AddComponent<ChooseLevel>();

		// Act
		chooseLevel.Level1();

		// Assert
		Assert.AreEqual(initialSceneIndex + 1, SceneManager.GetActiveScene().buildIndex);
	}
}
