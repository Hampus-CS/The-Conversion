using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>(); // Kortleken
    public List<Card> hand = new List<Card>(); // Spelarens hand

    // Metod f�r att dra kort
    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            int index = Random.Range(0, deck.Count); // Dra ett slumpm�ssigt kort
            hand.Add(deck[index]); // L�gg till kortet i handen
            deck.RemoveAt(index); // Ta bort kortet fr�n leken
            // Uppdatera UI h�r
        }
    }

    // L�gg till initialisering av kortleken och annan logik h�r
}
