using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public MapManager mapManager; // Reference to the MapManager

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Method to check if a unit can move
    public bool CanMoveUnit(Vector2 unitPosition, Card card)
    {
        MapManager.MapSection unitSection = mapManager.GetMapSection(unitPosition);
        return card.movementFlank switch
        {
            Card.Flank.LeftFlank => unitSection == MapManager.MapSection.LeftFlank,
            Card.Flank.Center => unitSection == MapManager.MapSection.Center,
            Card.Flank.RightFlank => unitSection == MapManager.MapSection.RightFlank,
            Card.Flank.LeftRightFlank => unitSection == MapManager.MapSection.LeftFlank || unitSection == MapManager.MapSection.RightFlank,
            _ => false
        };
    }
}
