using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Level1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Level2()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void Level3()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
    public void Level4()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }
    public void Level5()
    {
        Debug.Log("Error: ButtonLevel5 function has yet to be implemented");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
    }
    public void Level6()
    {
        Debug.Log("Error: ButtonLevel6 function has yet to be implemented");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 6);
    }
}
