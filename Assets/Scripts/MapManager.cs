using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [Header("Map Size")]
    public int width = 13; // Maximum width
    public int height = 9; // Height of the map

    [Header("Hexes")]
    public GameObject[] hexPrefabs; // Assign different Hex prefabs in the Inspector
    public int[] tileCounts; // Number of each tile type to use

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        List<GameObject> tilesToPlace = new List<GameObject>();

        // Validate total count
        int totalTiles = 0;
        for (int i = 0; i < tileCounts.Length; i++)
        {
            totalTiles += tileCounts[i];
            for (int j = 0; j < tileCounts[i]; j++)
            {
                tilesToPlace.Add(hexPrefabs[i]);
            }
        }

        if (totalTiles != width * height)
        {
            Debug.LogError("Total tiles count does not match map size!");
            return;
        }

        // Shuffle the list to randomize tile placement
        for (int i = 0; i < tilesToPlace.Count; i++)
        {
            GameObject temp = tilesToPlace[i];
            int randomIndex = Random.Range(i, tilesToPlace.Count);
            tilesToPlace[i] = tilesToPlace[randomIndex];
            tilesToPlace[randomIndex] = temp;
        }

        int tileIndex = 0;
        for (int y = 0; y < height; y++)
        {
            int rowWidth = (y % 2 == 0) ? width : width - 1;

            for (int x = 0; x < rowWidth; x++)
            {
                Vector2 hexPosition = CalculateHexPosition(x, y);
                Instantiate(tilesToPlace[tileIndex], hexPosition, Quaternion.identity, this.transform);
                tileIndex++;
            }
        }
    }

    Vector2 CalculateHexPosition(int x, int y)
    {
        float hexHeight = 2.0f;
        float hexWidth = Mathf.Sqrt(3) * hexHeight / 2;
        float verticalSpacing = 0.75f * hexHeight;
        float horizontalSpacing = hexWidth;
        float offsetX = (y % 2 == 0) ? 0 : hexWidth / 2;
        float posX = x * horizontalSpacing + offsetX;
        float posY = y * verticalSpacing;

        return new Vector2(posX, posY);
    }
}