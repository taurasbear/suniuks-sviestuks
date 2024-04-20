using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOnButter : MonoBehaviour
{
    public GameController gameManager;
    [SerializeField] private LayerMask isOnTopOfButter;
    [SerializeField] private Rigidbody2D rb;
    private BoxCollider2D coll;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (rb.name == "Player1" && IsOnButterCheck())
        {
            var player = gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Die();
                gameManager.YouCrushedButter();
            }
        }
    }
    private bool IsOnButterCheck()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, isOnTopOfButter);
    }
}
