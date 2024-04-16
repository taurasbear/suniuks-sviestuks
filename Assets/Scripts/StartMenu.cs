using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void ChooseLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Settings()
    {
        Debug.Log("Error: ButtonSettings function has yet to be implemented");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
