using System;
using UnityEngine;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    [Header("Map Size")]
    public int width = 13;
    public int height = 9;

    [Header("Hexes")]
    public GameObject[] hexPrefabs;
    public int[] tileCounts;

    public enum MapSection { LeftFlank, Center, RightFlank }

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {

        if (hexPrefabs == null || tileCounts == null || hexPrefabs.Length != tileCounts.Length)
        {
            Debug.LogError("Hex prefabs or tile counts are not properly configured.");
            return;
        }

        List<GameObject> tilesToPlace = new List<GameObject>();
        int totalTiles = 0;
        for (int i = 0; i < tileCounts.Length; i++)
        {
            if (hexPrefabs[i] == null)
            {
                Debug.LogError("Hex prefab at index " + i + " is null.");
                continue;
            }

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
                GameObject hex = Instantiate(tilesToPlace[tileIndex], hexPosition, Quaternion.identity, this.transform);
                if (hex == null)
                {
                    Debug.LogError("Failed to instantiate hex at index: " + tileIndex);
                    continue;
                }
                Hex hexComponent = hex.GetComponent<Hex>();
                if (hexComponent == null)
                {
                    Debug.LogError("Hex component not found on instantiated hex prefab at index: " + tileIndex);
                    continue;
                }
                hexComponent.coordinates = new Vector2Int(x, y);
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

    public MapSection GetMapSection(Vector2 position)
    {
        float sectionWidth = width * CalculateHexPosition(1, 0).x / 3;
        if (position.x < sectionWidth)
            return MapSection.LeftFlank;
        else if (position.x < sectionWidth * 2)
            return MapSection.Center;
        else
            return MapSection.RightFlank;
    }

}