using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapManager;

public class GamePiecesMovement : MonoBehaviour
{
    public MapManager mapManager; // Reference to the MapManager to determine zones

    public GameObject[] allTroops;

    void Start()
    {
        allTroops = GameObject.FindGameObjectsWithTag("Troop"); // Assuming each troop has a tag "Troop"
    }

    // Move Troops Based on a given zone and count
    public void MoveTroops(string zone, int troopCount)
    {
        MapSection targetSection = DetermineMapSection(zone);
        List<GameObject> selectedTroops = SelectTroopsForMovement(targetSection, troopCount);
        MoveSelectedTroops(selectedTroops);
    }

    private MapSection DetermineMapSection(string zone)
    {
        switch (zone)
        {
            case "left": return MapSection.LeftFlank;
            case "center": return MapSection.Center;
            case "right": return MapSection.RightFlank;
            default: return MapSection.Center; // Default case
        }
    }

    private List<GameObject> SelectTroopsForMovement(MapSection section, int troopCount)
    {
        List<GameObject> selectedTroops = new List<GameObject>();
        foreach (GameObject troop in allTroops)
        { // allTroops needs to be defined or passed to this method
            Vector2 troopPosition = troop.transform.position;
            if (mapManager.GetMapSection(troopPosition) == section)
            {
                selectedTroops.Add(troop);
                if (selectedTroops.Count == troopCount) break;
            }
        }
        return selectedTroops;
    }

    private void MoveSelectedTroops(List<GameObject> troops)
    {
        // Implement movement logic here
    }
}