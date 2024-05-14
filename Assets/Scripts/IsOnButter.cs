using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//////////////////////////////////////////////////// STANDING ON BUTTER BEHAVIOUR /////////////////////////////////////////////////////////////
public class IsOnButter : MonoBehaviour
{
    public GameController gameManager;
    [SerializeField] private LayerMask dog;
    [SerializeField] private Rigidbody2D rb;
    private BoxCollider2D coll;
    [SerializeField] private Transform crushCheck;

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
    return Physics2D.OverlapCircle(crushCheck.position, 0.1f, dog);
  }
  }
