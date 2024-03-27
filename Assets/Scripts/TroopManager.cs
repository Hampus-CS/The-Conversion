using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopManager : MonoBehaviour
{
    public GameObject troopPrefab; // Your Troop prefab

    // Method to spawn a troop on a hex
    public void SpawnTroop(Vector2 position)
    {
        Instantiate(troopPrefab, position, Quaternion.identity);
    }

    // Add methods for moving troops, possibly interacting with the Hex class or MapManager
}