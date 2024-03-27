using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public Vector2Int coordinates; // Grid coordinates
    public bool isOccupied = false;
    public GameObject troop; // The troop occupying this hex, if any
}