using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public Sprite cardImage; // Assignable in the inspector
    public enum Flank { LeftFlank, Center, RightFlank, LeftRightFlank }
    public Flank movementFlank;
    public int controlUnits; // Number of infantry/tanks controlled by this card
}