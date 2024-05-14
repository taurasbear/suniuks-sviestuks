using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class FinishLevel : MonoBehaviour
{
  //[SerializeField] private TextMeshProUGUI winTitle;
  [SerializeField] private GameObject Player1;
  [SerializeField] private GameObject Player2;
  [SerializeField] private int KeyCountToWin;

  private BoxCollider2D coll;
  private BoxCollider2D player1Coll;
  private BoxCollider2D player2Coll;
  private ItemCollector player1Item;
  private ItemCollector player2Item;
  public GameController gameManager;
  private void Start()
  {
    player1Coll = Player1.GetComponent<BoxCollider2D>();
    player2Coll = Player2.GetComponent<BoxCollider2D>();
    player1Item = Player1.GetComponent<ItemCollector>();
    player2Item = Player2.GetComponent<ItemCollector>();
  }
  private void Update()
  {
    gameManager.keysScore.text = "Keys: " + ((int)player1Item.GetKeyCount() + (int)player2Item.GetKeyCount()) + "/"+ KeyCountToWin;
  }
  //////////////////////////////////////////////////// FINISH UI ACTIVATION /////////////////////////////////////////////////////////////
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (!(KeyCountToWin > 0))
    {
      Debug.Log($"KeyCountToWin was null in FinishLevel.cs\nScene: {SceneManager.GetActiveScene().name}");
      return;
    }
    if (other == player1Coll || other == player2Coll)
    {
      coll = GetComponent<BoxCollider2D>();
      if (coll.IsTouching(player1Coll) && coll.IsTouching(player2Coll) && (player1Item.GetKeyCount() + player2Item.GetKeyCount()) == KeyCountToWin)
      {
        gameManager.YouWin();
      }
    }
  }
}
