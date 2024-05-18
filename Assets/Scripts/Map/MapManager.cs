using System;
using UnityEngine;
using System.Collections.Generic;

using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class MapManager : MonoBehaviour
{
    [Header("Map Size")]
    public int width = 13;
    public int height = 9;

    [Header("Hexes")]
    public GameObject[] hexPrefabs;
    public int[] tileCounts;

    [Header("Troops")]
    public GameObject axisTroopPrefab;
    public GameObject alliedTroopPrefab;

    public enum MapSection { LeftFlank, Center, RightFlank }

    public static MapManager Instance;

    private Hex selectedTroopHex;
    private List<Hex> highlightedHexes = new List<Hex>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GenerateMap();
        PlaceInitialTroops();
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
                hexComponent.hexSide = DetermineHexSide(hexPosition.x);
                hexComponent.Initialize(); // Ensure the Hex is initialized
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

    HexSide DetermineHexSide(float x)
    {
        if (x < 6.058f)
        {
            return HexSide.Left;
        }
        else if (x <= 14.7225f)
        {
            return HexSide.Middle;
        }
        else
        {
            return HexSide.Right;
        }
    }

    void PlaceInitialTroops()
    {
        // Axis troop positions (blue circles)
        List<Vector2Int> axisPositions = new List<Vector2Int>
        {
            new Vector2Int(0, 8), new Vector2Int(1, 8), // Left
            new Vector2Int(5, 8), new Vector2Int(6, 8), new Vector2Int(7, 8), // Middle
            new Vector2Int(11, 8), new Vector2Int(12, 8) // Right
        };

        // Allied troop positions (yellow circles)
        List<Vector2Int> alliedPositions = new List<Vector2Int>
        {
            new Vector2Int(0, 0), new Vector2Int(1, 0), // Left
            new Vector2Int(5, 0), new Vector2Int(6, 0), new Vector2Int(7, 0), // Middle
            new Vector2Int(11, 0), new Vector2Int(12, 0) // Right
        };

        PlaceTroops(axisPositions, axisTroopPrefab);
        PlaceTroops(alliedPositions, alliedTroopPrefab);
    }

    void PlaceTroops(List<Vector2Int> positions, GameObject troopPrefab)
    {
        foreach (Vector2Int pos in positions)
        {
            Hex hex = GetHexAtPosition(pos.x, pos.y);
            if (hex != null)
            {
                GameObject troop = Instantiate(troopPrefab, hex.transform.position, Quaternion.identity);
                hex.troop = troop;
                hex.Initialize(); // Ensure the Hex is initialized
                hex.SetOccupied(true);
            }
        }
    }

    Hex GetHexAtPosition(int x, int y)
    {
        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            if (hex.coordinates.x == x && hex.coordinates.y == y)
            {
                return hex;
            }
        }
        return null;
    }

    public void MoveTroops(CardProperties cardProperties)
    {
        // Find all hexes that belong to the allowed flanks
        List<Hex> allowedHexes = new List<Hex>();

        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            if (IsHexAllowed(hex, cardProperties.flank))
            {
                allowedHexes.Add(hex);
            }
        }

        // Restrict movement to the specified number of troops
        MoveTroopsToHexes(allowedHexes, cardProperties.numberOfTroops);
    }

    private bool IsHexAllowed(Hex hex, Flank flank)
    {
        bool allowed = false;

        switch (flank)
        {
            case Flank.Left:
                allowed = hex.hexSide == HexSide.Left;
                break;
            case Flank.Middle:
                allowed = hex.hexSide == HexSide.Middle;
                break;
            case Flank.Right:
                allowed = hex.hexSide == HexSide.Right;
                break;
            case Flank.RightLeft:
                allowed = (hex.hexSide == HexSide.Left || hex.hexSide == HexSide.Right);
                break;
        }

        Debug.Log($"IsHexAllowed: Hex ({hex.coordinates.x}, {hex.coordinates.y}) on {hex.hexSide}, Flank = {flank}, Allowed = {allowed}");

        return allowed;
    }

    private void MoveTroopsToHexes(List<Hex> hexes, int numberOfTroops)
    {
        int troopsMoved = 0;
        foreach (Hex hex in hexes)
        {
            if (troopsMoved < numberOfTroops && !hex.isOccupied)
            {
                Debug.Log($"Moving troop to hex at position {hex.coordinates}");
                hex.SetOccupied(true);
                troopsMoved++;
            }
            else if (troopsMoved >= numberOfTroops)
            {
                break;
            }
        }
    }

    public void OnTroopSelected(Hex selectedHex)
    {
        if (!GameManager.Instance.canMoveTroops) return;

        // Check if the selected troop is in the allowed flank
        Debug.Log($"OnTroopSelected: Selected Hex ({selectedHex.coordinates.x}, {selectedHex.coordinates.y}) on {selectedHex.hexSide}");
        if (IsHexAllowed(selectedHex, GameManager.Instance.allowedFlank))
        {
            selectedTroopHex = selectedHex;
            HighlightAvailableHexes(selectedHex);
        }
    }

    void HighlightAvailableHexes(Hex selectedHex)
    {
        // Clear previous highlights
        foreach (Hex hex in highlightedHexes)
        {
            hex.Highlight(false);
        }
        highlightedHexes.Clear();

        // Highlight new hexes
        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            if (!hex.isOccupied && IsHexAdjacent(selectedHex, hex))
            {
                hex.Highlight(true);
                highlightedHexes.Add(hex);
            }
        }
    }

    bool IsHexAdjacent(Hex fromHex, Hex toHex)
    {
        // Define adjacency logic here. For simplicity, let's assume direct neighbors
        int dx = Mathf.Abs(fromHex.coordinates.x - toHex.coordinates.x);
        int dy = Mathf.Abs(fromHex.coordinates.y - toHex.coordinates.y);
        return (dx + dy == 1);
    }

    void Update()
    {
        if (selectedTroopHex != null && GameManager.Instance.canMoveTroops && GameManager.Instance.allowedMoves > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D collider = Physics2D.OverlapPoint(mousePosition);
                if (collider != null)
                {
                    Hex clickedHex = collider.GetComponent<Hex>();
                    if (clickedHex != null && highlightedHexes.Contains(clickedHex))
                    {
                        MoveTroop(selectedTroopHex, clickedHex);
                        GameManager.Instance.allowedMoves--;
                        if (GameManager.Instance.allowedMoves <= 0)
                        {
                            GameManager.Instance.canMoveTroops = false;
                        }
                    }
                }
            }
        }
    }

    void MoveTroop(Hex fromHex, Hex toHex)
    {
        // Move troop to the new hex
        toHex.troop = fromHex.troop;
        toHex.SetOccupied(true);
        fromHex.troop = null;
        fromHex.SetOccupied(false);

        // Move the troop GameObject to the new position
        toHex.troop.transform.position = toHex.transform.position;

        // Clear highlights
        foreach (Hex hex in highlightedHexes)
        {
            hex.Highlight(false);
        }
        highlightedHexes.Clear();

        selectedTroopHex = null;
    }
}