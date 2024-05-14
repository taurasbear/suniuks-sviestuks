using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class ObstacleDeath : MonoBehaviour
{
  public static int counter = 0;
  public GameController gameManager;
  //////////////////////////////////////////////////// OBSTACLE DAMAGE /////////////////////////////////////////////////////////////
  private void Start()
  {
    counter = 0;
  }
  private void Update()
  {
    gameManager.deathsScore.text = "Deaths: " + counter + "/3";
  }
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
                // Trigger the death sequence
				        playerMovement.Die();
                 counter++;
            }
            else
            {
                Debug.LogWarning("PlayerMovement script not found on the player character.");
            }

            if(counter >= 3) 
            {
                gameManager.YouLose();
            }

        }
  }
}

