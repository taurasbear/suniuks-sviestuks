using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
  //////////////////////////////////////////////////// BUTTON BEHAVIOUR /////////////////////////////////////////////////////////////

  public void Settings()
  {
    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    SceneManager.LoadScene("Settings");
  }
  public void ChooseLevel()
  {
    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    SceneManager.LoadScene("ChooseLevel");
  }

  public void Exit()
  {
    Application.Quit();
  }
}
