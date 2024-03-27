using UnityEngine;

[System.Serializable]
public class Card : MonoBehaviour
{
    public string cardName;
    public string description;
    public Sprite cardImage; // For 2D
    public CardType cardType; // Enum to represent card type

    // Define what the card does. Could be more sophisticated based on your game design
    public void ActivateEffect()
    {
        // Implement effect logic here, e.g., moving troops, drawing more cards, etc.
    }
}

public enum CardType
{
    MoveLeftFlank,
    MoveCenter,
    MoveRightFlank,
    // Add more as needed
}