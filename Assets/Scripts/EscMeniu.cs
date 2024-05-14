using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMeniu : MonoBehaviour
{
  public GameController gameManager;
  public static bool flag = false;
  // Update is called once per frame
  void Update()
    {
    if (Input.GetKeyDown(KeyCode.Escape) && flag == false)
    {
      gameManager.ESCMenuActive();
      flag = true;
    }
    else
    if (Input.GetKeyDown(KeyCode.Escape) && flag == true)
    {
      gameManager.ESCMenuInActive();
      flag = false;
    }


  }
}
