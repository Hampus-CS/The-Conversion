using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>(); // Full deck as ScriptableObjects
    public List<Card> hand = new List<Card>(); // Cards in hand

    void Start()
    {
        DrawCards(5); // Initial draw to fill the player's hand at the start
    }

    public void DrawCards(int numberOfCards)
    {
        for (int i = 0; i < numberOfCards; i++)
        {
            if (deck.Count > 0)
            {
                int index = Random.Range(0, deck.Count);
                hand.Add(deck[index]);
                deck.RemoveAt(index);
            }
        }
        UpdateHandUI(); // Update the UI after drawing cards
    }

    public void UseCard(int cardIndex)
    {
        if (hand.Count > cardIndex)
        {
            Card cardUsed = hand[cardIndex];
            hand.RemoveAt(cardIndex);
            deck.Add(cardUsed); // Optionally shuffle this card back into the deck
            DrawCards(1); // Draw a new card to replace the used one
            UpdateHandUI(); // Update the UI to reflect the new hand
        }
    }

    void UpdateHandUI()
    {
        // Update UI here
        Debug.Log("Updated Hand UI with " + hand.Count + " cards.");
    }
}