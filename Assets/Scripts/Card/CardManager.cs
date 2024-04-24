using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class CardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>(); // Full deck as ScriptableObjects
    public List<Card> hand = new List<Card>(); // Cards in hand
    public GameObject cardPrefab; // Assign this in the Inspector
    public Transform handTransform; // The parent transform under Canvas for the hand cards

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
                int index = UnityEngine.Random.Range(0, deck.Count);
                Card drawnCard = deck[index];
                deck.RemoveAt(index);

                // Instantiate the card prefab and initialize it
                GameObject cardGO = Instantiate(cardPrefab, handTransform);
                CardUI cardUI = cardGO.GetComponent<CardUI>();
                if (cardUI != null)
                {
                    cardUI.cardData = drawnCard; // Pass the drawn card data to the UI
                    cardUI.UpdateCardUI(); // This function should set the card image and name
                }

                hand.Add(drawnCard); // Add the drawn card to the hand
            }
        }
        UpdateHandUI(); // Call the method to update the hand UI
    }

    public void UseCard(Card card)
    {
        if (hand.Contains(card))
        {
            hand.Remove(card);
            deck.Add(card); // Optionally shuffle this card back into the deck
            DrawCards(1); // Draw a new card to replace the used one
            UpdateHandUI(); // Update the UI to reflect the new hand
        }
    }

    void UpdateHandUI()
    {
        Debug.Log("Updated Hand UI with " + hand.Count + " cards.");
        // Here you might want to update the display of each card, 
        // e.g., hide/show cards, update order, etc.
    }
}