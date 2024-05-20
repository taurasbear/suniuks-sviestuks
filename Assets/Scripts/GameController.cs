using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
  public GameObject youWinUI;
  public GameObject youLoseUI;
  public GameObject youCrushedButterUI;
  public GameObject ESC;
  public GameObject settings;
  public TextMeshProUGUI deathsScore;
  public TextMeshProUGUI keysScore;

  //////////////////////////////////////////////////// BUTTON AND UI BEHAVIOUR /////////////////////////////////////////////////////////////
  public void YouWin()
  {
    youWinUI.SetActive(true);
    Time.timeScale = 0f;
  }
  public void YouLose()
  {
    youLoseUI.SetActive(true);
    Time.timeScale = 0f;
  }
  public void YouCrushedButter()
  {
    youCrushedButterUI.SetActive(true);
    Time.timeScale = 0f;
  }

  public void ESCMenuActive()
  {
    ESC.SetActive(true);
    //Debug.Log("--> Should've OPENED ESC menu");
    Time.timeScale = 0f;
  }
  public void ESCMenuInActive()
  {
    ESC.SetActive(false);
    //Debug.Log("--> Should've CLOSED ESC menu");
    Time.timeScale = 1f;
  }
  public bool IsESCMenuActive()
  {
    return ESC.activeSelf;
  }
  public void PlayAgain()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    Time.timeScale = 1f;
  }
  public void NextLevel()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    Time.timeScale = 1f;
  }
  public void MainMeniu()
  {
    SceneManager.LoadScene("StartMenu");
    Time.timeScale = 1f;
  }
  public void SettingsOpen()
  {
    ESC.SetActive(false);
    settings.SetActive(true);
    //Debug.Log("--> SettingsOpen() did fuck all");

    //Time.timeScale = 0f;
  }
  public void SettingsClose()
  {
    ESC.SetActive(true);
    settings.SetActive(false);
    //Debug.Log("--> SettingsClose() did fuck all");
  }
  public bool IsSettingsMenuActive()
  {
    return settings.activeSelf;
  }
}
