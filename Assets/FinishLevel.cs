using TMPro;
using UnityEngine;

public class FinishLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winTitle;
    [SerializeField] private BoxCollider2D player1;
    [SerializeField] private BoxCollider2D player2;

    private BoxCollider2D coll;

    private void OnTriggerEnter2D(Collider2D other)
    {       
        if(other == player1 || other == player2)
        {
            coll = GetComponent<BoxCollider2D>();
            if (coll.IsTouching(player1) && coll.IsTouching(player2))
            {
                winTitle.enabled = true;
            }
        }       
    }
}
