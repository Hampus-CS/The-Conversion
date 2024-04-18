using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private List<Card> deck = new List<Card>(); // All cards in the deck
    private List<Card> hand = new List<Card>(); // Cards currently in hand
    [SerializeField] private List<Transform> cardPositions = new List<Transform>(); // POS of cards in deck 

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
        if (deck.Count > 0 && hand.Count < cardPositions.Count)
        {
            if (deck.Count > 0 && hand.Count < cardPositions.Count)
            {
            Card card = deck[0];
            hand.Add(card);
            deck.RemoveAt(0);
            card.transform.position = cardPositions[hand.Count - 1].position;
            }
            else
            {
                Debug.LogError("Card or position is null.");
            }
        }
        else
        {
            Debug.LogError("No more cards in the deck or hand is full.");
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
    void OnDisable()
    {  
        hand.Clear();
    }

}
