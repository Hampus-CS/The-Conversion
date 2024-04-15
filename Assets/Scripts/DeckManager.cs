using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private List<Card> deck = new List<Card>(); // All cards in the deck
    [SerializeField] private List<Card> hand = new List<Card>(); // Cards currently in hand

    void Start()
    {
        ShuffleDeck();
        DrawInitialHand(5); // Draw 5 cards initially
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public void DrawInitialHand(int initialCount)
    {
        for (int i = 0; i < initialCount; i++)
        {
            DrawCard();
        }
    }

    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            Card card = deck[0];
            hand.Add(card);
            deck.RemoveAt(0);
        }
        else
        {
            Debug.Log("No more cards in the deck.");
        }
    }

    public void PlayCard(Card card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            card.ActivateEffect();
            DrawCard(); // Draw a new card after playing one
        }
    }
}
