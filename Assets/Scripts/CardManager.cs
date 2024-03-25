using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>(); // Card deck
    public List<Card> hand = new List<Card>(); // Players hand

    // Method for drawing a card
    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            int index = Random.Range(0, deck.Count); // Pull a random card
            hand.Add(deck[index]); // Add card to hand
            deck.RemoveAt(index); // Remove card from deck
            // Update UI her
        }
    }

    // Add initialising of card deck and other logic here
}
