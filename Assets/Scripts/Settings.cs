using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
  public Slider masterVol, musicVol;
  public AudioMixer audioMixer;

  private void Start()
  {
    masterVol.value = PlayerPrefs.GetFloat("MasterVol", 0);
    musicVol.value = PlayerPrefs.GetFloat("MusicVol", 0);
  }
  public void ChangeMasterVolume()
  {
    audioMixer.SetFloat("MasterVol", masterVol.value);
    PlayerPrefs.SetFloat("MasterVol", masterVol.value);
  }
  public void ChangeMusicVolume()
  {
    audioMixer.SetFloat("MusicVol", musicVol.value);
    PlayerPrefs.SetFloat("MusicVol", musicVol.value);

  }
  public void StartMenu()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("StartMenu");
  }
}
