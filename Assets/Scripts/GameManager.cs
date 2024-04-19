using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MapManager mapManager; // Reference to the MapManager to access map-related functions

    void Start()
    {
        // Initialization code here (if needed)
    }

    // Example of checking if a unit can move based on a card played
    public bool CanMoveUnit(Vector2 unitPosition, Card card)
    {
        MapManager.MapSection unitSection = mapManager.GetMapSection(unitPosition);
        switch (card.movementFlank)
        {
            case Card.Flank.LeftFlank:
                return unitSection == MapManager.MapSection.LeftFlank;
            case Card.Flank.Center:
                return unitSection == MapManager.MapSection.Center;
            case Card.Flank.RightFlank:
                return unitSection == MapManager.MapSection.RightFlank;
            case Card.Flank.LeftRightFlank:
                return unitSection == MapManager.MapSection.LeftFlank || unitSection == MapManager.MapSection.RightFlank;
            default:
                return false;
        }
    }
}