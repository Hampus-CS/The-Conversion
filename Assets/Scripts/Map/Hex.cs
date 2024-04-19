using UnityEngine;

public class Hex : MonoBehaviour
{
    public Vector2Int coordinates; // Grid coordinates
    public bool isOccupied = false; // Is there a troop occuyping this hex?
    public GameObject troop; // The troop occupying this hex, if any

    public void SetOccupied(bool isOccupied)
    {
        isOccupied = true;
        // Change color or add a visual indicator
        GetComponent<SpriteRenderer>().color = isOccupied ? Color.red : Color.white;
    }

}