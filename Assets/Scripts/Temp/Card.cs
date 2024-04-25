using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;
    public int handIndex;
    private GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        if (gm == null)
        {
            Debug.LogError("GameManager not found in the scene!");
        }
    }

    void OnMouseDown()
    {
        if (!hasBeenPlayed && gm != null && !gm.cardPlayedThisTurn)
        {
            transform.position += Vector3.up * 5;
            hasBeenPlayed = true;
            gm.availableCardSlots[handIndex] = true;
            gm.cardPlayedThisTurn = true;
            MoveToDiscardPile();
        }
    }

    void MoveToDiscardPile()
    {
        if (gm != null)
        {
            gm.discardPile.Add(this);
            gm.playedCardsThisTurn.Add(this);
        }
    }
}