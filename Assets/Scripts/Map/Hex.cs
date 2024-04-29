using UnityEngine;

public class Hex : MonoBehaviour
{
    public Vector2Int coordinates;
    public bool isOccupied = false;
    public GameObject troop;

    public void SetOccupied(bool isOccupied)
    {
        isOccupied = true;
        GetComponent<SpriteRenderer>().color = isOccupied ? Color.red : Color.white;
    }

}