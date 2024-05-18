using System;
using UnityEngine;

public enum Flank
{
    Left,
    Middle,
    Right,
    RightLeft
}

public class CardProperties : MonoBehaviour
{
    public Flank flank;
    public int numberOfTroops;

    void Start()
    {
        Debug.Log($"CardProperties Start: Flank = {flank}, NumberOfTroops = {numberOfTroops}");
    }
}