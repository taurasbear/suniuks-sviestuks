using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    //////////////////////////////////////////////////// DEATH ZONE BEHAVIOUR /////////////////////////////////////////////////////////////
    public GameController gameManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var player = collision.gameObject.GetComponent<PlayerMovement>();
        if(player != null)
        {
            player.Die();
            gameManager.YouLose();
        }   
    }
}

