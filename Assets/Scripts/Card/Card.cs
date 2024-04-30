using UnityEngine;

using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class Card : MonoBehaviour
{

    private GameManager gameManager;

    [Header("Game Manager Variables")]
    public bool hasBeenPlayed;
    public int handIndex;

    [Header("Card Properties")]
    public Flank cardFlank;
    public int troopsAmount;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    void OnMouseDown()
    {
        if (!hasBeenPlayed && gameManager != null && !gameManager.cardPlayedThisTurn)
        {
            transform.position += Vector3.up * 5;
            hasBeenPlayed = true;
            gameManager.availableCardSlots[handIndex] = true;
            gameManager.cardPlayedThisTurn = true;
            MoveToDiscardPile();
            Debug.Log("Card played with flank: " + cardFlank.ToString() + " and troops: " + troopsAmount);

            // Move troop amount

        }
    }

    void MoveToDiscardPile()
    {
        if (gameManager != null)
        {
            gameManager.discardPile.Add(this);
            gameManager.playedCardsThisTurn.Add(this);
        }
    }
}