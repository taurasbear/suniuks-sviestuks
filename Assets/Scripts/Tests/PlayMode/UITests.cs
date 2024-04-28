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
	[UnityTest]
	public IEnumerator Back_Changes_Scene_To_Previous_One()
	{
		// Arrange
		
		var chooseLevel = new GameObject().AddComponent<ChooseLevel>();
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

		var expectedSceneIndex = currentSceneIndex - 1;

		// Act
		chooseLevel.Back();
		yield return null; // Wait for one frame to allow the scene to change

		// Assert
		Assert.AreEqual(expectedSceneIndex, SceneManager.GetActiveScene().buildIndex);
	}

	[UnityTest]
	public IEnumerator Level1_Changes_Scene_To_Next_One()
	{
		// Arrange
		SceneManager.LoadScene("ChooseLevel");
		yield return null;
		var chooseLevel = new GameObject().AddComponent<ChooseLevel>();
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		var expectedSceneIndex = currentSceneIndex + 1;

		// Act
		chooseLevel.Level1();
		yield return null; // Wait for one frame to allow the scene to change

		// Assert
		Assert.AreEqual(expectedSceneIndex, SceneManager.GetActiveScene().buildIndex);
	}

	[UnityTest]
	public IEnumerator Level2_Changes_Scene_To_Next_One()
	{
		// Arrange
		SceneManager.LoadScene("ChooseLevel");
		yield return null;
		var chooseLevel = new GameObject().AddComponent<ChooseLevel>();
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		var expectedSceneIndex = currentSceneIndex + 2;

		// Act
		chooseLevel.Level2();
		yield return null; // Wait for one frame to allow the scene to change

		// Assert
		Assert.AreEqual(expectedSceneIndex, SceneManager.GetActiveScene().buildIndex);
	}
	[UnityTest]
	public IEnumerator Level3_Changes_Scene_To_Next_One()
	{
		// Arrange
		SceneManager.LoadScene("ChooseLevel");
		yield return null;
		var chooseLevel = new GameObject().AddComponent<ChooseLevel>();
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		var expectedSceneIndex = currentSceneIndex + 3;

		// Act
		chooseLevel.Level3();
		yield return null; // Wait for one frame to allow the scene to change

		// Assert
		Assert.AreEqual(expectedSceneIndex, SceneManager.GetActiveScene().buildIndex);
	}

	[UnityTest]
	public IEnumerator Level4_Changes_Scene_To_Next_One()
	{
		// Arrange
		SceneManager.LoadScene("ChooseLevel");
		yield return null;
		var chooseLevel = new GameObject().AddComponent<ChooseLevel>();
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		var expectedSceneIndex = currentSceneIndex + 4;

		// Act
		chooseLevel.Level4();
		yield return null; // Wait for one frame to allow the scene to change

		// Assert
		Assert.AreEqual(expectedSceneIndex, SceneManager.GetActiveScene().buildIndex);
	}

	[UnityTest]
	public IEnumerator QuickStart_Changes_Scene_To_Level1_And_Resets_TimeScale()
	{
		// Arrange
		var chooseLevel = new GameObject().AddComponent<ChooseLevel>();
		Time.timeScale = 0f; // Set a different time scale to check if it gets reset

		// Act
		chooseLevel.QuickStart();
		yield return null; // Wait for one frame to allow the scene to change

		// Assert
		Assert.AreEqual("Level1", SceneManager.GetActiveScene().name);
		Assert.AreEqual(1f, Time.timeScale);
	}

	[UnityTest]
	public IEnumerator PlayAgain_Loads_Same_Scene()
	{
		// Arrange
		var chooseLevel = new GameObject().AddComponent<GameController>();
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

		// Act
		chooseLevel.PlayAgain();
		yield return null; // Wait for one frame to allow the scene to change

		// Assert
		Assert.AreEqual(currentSceneIndex, SceneManager.GetActiveScene().buildIndex);
	}

	[UnityTest]
	public IEnumerator NextLevel_Loads_Next_Scene()
	{
		// Arrange
		var chooseLevel = new GameObject().AddComponent<GameController>();
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

		// Act
		chooseLevel.NextLevel();
		yield return null; // Wait for one frame to allow the scene to change

		// Assert
		Assert.AreEqual(currentSceneIndex + 1, SceneManager.GetActiveScene().buildIndex);
	}

	[UnityTest]
	public IEnumerator YouWin_Activates_YouWinUI_And_Stops_Time()
	{
		// Arrange
		var gameController = new GameObject().AddComponent<GameController>();
		gameController.youWinUI = new GameObject();
		gameController.youWinUI.SetActive(false);

		// Act
		gameController.YouWin();
		yield return null; // Wait for one frame to allow the UI to change

		// Assert
		Assert.IsTrue(gameController.youWinUI.activeSelf);
		Assert.AreEqual(0f, Time.timeScale);
	}

	// Repeat the above test for YouLose and YouCrushedButter methods, just change the method called and the UI object used

	[UnityTest]
	public IEnumerator PlayAgain_Reloads_Current_Scene_And_Resets_TimeScale()
	{
		// Arrange
		var gameController = new GameObject().AddComponent<GameController>();
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		Time.timeScale = 0f; // Set a different time scale to check if it gets reset

		// Act
		gameController.PlayAgain();
		yield return null; // Wait for one frame to allow the scene to change

		// Assert
		Assert.AreEqual(currentSceneIndex, SceneManager.GetActiveScene().buildIndex);
		Assert.AreEqual(1f, Time.timeScale);
	}

	[UnityTest]
	public IEnumerator NextLevel_Changes_Scene_To_Next_One_And_Resets_TimeScale()
	{
		// Arrange
		var gameController = new GameObject().AddComponent<GameController>();
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		var expectedSceneIndex = currentSceneIndex + 1;
		Time.timeScale = 0f; // Set a different time scale to check if it gets reset

		// Act
		gameController.NextLevel();
		yield return null; // Wait for one frame to allow the scene to change

		// Assert
		Assert.AreEqual(expectedSceneIndex, SceneManager.GetActiveScene().buildIndex);
		Assert.AreEqual(1f, Time.timeScale);
	}

	[UnityTest]
	public IEnumerator MainMeniu_Changes_Scene_To_StartMenu_And_Resets_TimeScale()
	{
		// Arrange
		var gameController = new GameObject().AddComponent<GameController>();
		Time.timeScale = 0f; // Set a different time scale to check if it gets reset

		// Act
		gameController.MainMeniu();
		yield return null; // Wait for one frame to allow the scene to change

		// Assert
		Assert.AreEqual("StartMenu", SceneManager.GetActiveScene().name);
		Assert.AreEqual(1f, Time.timeScale);
	}
}
