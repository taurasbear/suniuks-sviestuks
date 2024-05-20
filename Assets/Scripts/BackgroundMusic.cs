using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
  private AudioSource audioSource;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
    audioSource.Play();
  }
}
