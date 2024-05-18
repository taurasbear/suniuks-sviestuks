using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class SettingsMenu : MonoBehaviour
{
  public GameController gameManager;

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape) && gameManager.IsSettingsMenuActive())
    {
      gameManager.SettingsClose();
    }
  }
}
