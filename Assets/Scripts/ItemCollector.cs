using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
  private int keys = 0;
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Key"))
    {
      Destroy(collision.gameObject);
      keys++;
    }
  }
  public int GetKeyCount()
  {
    return keys;
  }
}
