using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>(); // Kortleken
    public List<Card> hand = new List<Card>(); // Spelarens hand

    // Metod för att dra kort
    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            int index = Random.Range(0, deck.Count); // Dra ett slumpmässigt kort
            hand.Add(deck[index]); // Lägg till kortet i handen
            deck.RemoveAt(index); // Ta bort kortet från leken
            // Uppdatera UI här
        }
    }

    // Lägg till initialisering av kortleken och annan logik här
}
