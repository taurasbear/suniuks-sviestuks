using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject youWinUI;
    public GameObject youLoseUI;
    public GameObject youCrushedButterUI;
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

}
