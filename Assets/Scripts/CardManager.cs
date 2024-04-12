using System.Collections.Generic;
using UnityEngine;

    public class CardManager : MonoBehaviour
    {
        public List<Card> deck = new List<Card>();
        public List<Card> hand = new List<Card>();

        public int initialCards;

        void Start()
        {
            InitializeDeck();
            DrawInitialCards(initialCards);  // Example: Draw 5 cards at the start
        }

        // Method for playing a card from the hand
        public void PlayCard(Card card)
        {
            if (hand.Contains(card))
            {
                card.ActivateEffect();
                hand.Remove(card);
                Destroy(card.gameObject); // Assuming you want to destroy the card after use
            }
        }

        void InitializeDeck()
        {
            //Left
            deck.Add(CreateCard("Hard Advance Left", "Command your troops to a hard advance on the left flank.", CardType.MoveLeftFlank, 3));
            deck.Add(CreateCard("Advance Left", "Command your troops to advance on the left flank.", CardType.MoveLeftFlank, 2));
            deck.Add(CreateCard("Recon Left", "Command your troops to a small advance on the left flank.", CardType.MoveLeftFlank, 1));
        
            //Center
            deck.Add(CreateCard("Hard Advance Center", "Command your troops to a hard advance on the center flank.", CardType.MoveCenter, 3));
            deck.Add(CreateCard("Advance Center", "Command your troops to advance on the center flank.", CardType.MoveCenter, 2));
            deck.Add(CreateCard("Recon Center", "Command your troops to a small advance on the center flank.", CardType.MoveCenter, 1));
        
            //Right
            deck.Add(CreateCard("Hard Advance Right", "Command your troops to a hard advance on the right flank.", CardType.MoveRightFlank, 3));
            deck.Add(CreateCard("Advance Right", "Command your troops to advance on the right flank.", CardType.MoveRightFlank, 2));
            deck.Add(CreateCard("Recon Right", "Command your troops to a small advance on the right flank.", CardType.MoveRightFlank, 1));

            //Right & Left
            deck.Add(CreateCard("Hard Advance Left and/or Right", "Command your troops to a hard advance on left and/or right flank.", CardType.MoveRightLeftFlank, 3));
            deck.Add(CreateCard("Advance Left and/or Right", "Command your troops to advance on the left and/or right flank.", CardType.MoveRightLeftFlank, 2));
            deck.Add(CreateCard("Recon Left and/or Right", "Command your troops to a small advance on the left and/or right flank.", CardType.MoveRightLeftFlank, 1));
        
        
            // Add more cards with different types and effects
        }
    
        Card CreateCard(string name, string desc, CardType type, int count)
        {
            Card newCard = new GameObject(name).AddComponent<Card>();
            newCard.cardName = name;
            newCard.description = desc;
            newCard.cardType = type;
            newCard.troopCount = count;
            // You could also assign a sprite based on type
            return newCard;
        }

        void DrawInitialCards(int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; i++)
                DrawCard();
        }

        public void DrawCard()
        {
            if (deck.Count > 0)
            {
                int index = Random.Range(0, deck.Count);
                hand.Add(deck[index]);
                deck.RemoveAt(index);
                // Update UI here
            }
        }
    }