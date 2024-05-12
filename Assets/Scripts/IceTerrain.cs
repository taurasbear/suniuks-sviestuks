using UnityEngine;

public class IceTerrain : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D collision)
  {
    // Check if the colliding object is the player character
    if (collision.gameObject.CompareTag("Player"))
    {
      // Get a reference to the PlayerMovement script attached to the player character
      PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();

      // Check if the PlayerMovement script is attached
      if (playerMovement != null)
      {
        // Handle interaction with the ice terrain here
        playerMovement.HandleIceTerrainInteraction(this.GetComponent<Collider2D>());
      }
      else
      {
        Debug.LogWarning("PlayerMovement script not found on the player character.");
      }
    }
  }

}
