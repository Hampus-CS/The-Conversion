using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map Size")]
    public int width = 13; // Maximum width
    public int height = 9; // Height of the map

    [Header("Hexes")]
    public GameObject hexPrefab; // Assign Hex prefab in the Inspector

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int y = 0; y < height; y++)
        {
            // Determine the number of hexes in this row
            int rowWidth = (y % 2 == 0) ? width : width - 1;

            for (int x = 0; x < rowWidth; x++)
            {
                // Calculate hex position with staggered rows
                Vector2 hexPosition = CalculateHexPosition(x, y);
                Instantiate(hexPrefab, hexPosition, Quaternion.identity, this.transform);
            }
        }
    }

    Vector2 CalculateHexPosition(int x, int y)
    {
        float hexHeight = 2.0f; // Assuming each hex has a height of 2 units; adjust as needed
        float hexWidth = Mathf.Sqrt(3) * hexHeight / 2; // Calculate hex width based on height
        float verticalSpacing = 0.75f * hexHeight;
        float horizontalSpacing = hexWidth;
        // Offset every other row by half the width of a hex
        float offsetX = (y % 2 == 0) ? 0 : hexWidth / 2;
        float posX = x * horizontalSpacing + offsetX;
        float posY = y * verticalSpacing;

        return new Vector2(posX, posY);
    }
}