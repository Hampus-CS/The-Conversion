using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class Card : MonoBehaviour
{

    private GameManager gm;

    [Header("Game Manager Variables")]
    public bool hasBeenPlayed;
    public int handIndex;


    [Header("Card Properties")]
    public Flank cardFlank;
    public int troopsAmount;

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
            Debug.Log("Card played with flank: " + cardFlank.ToString() + " and troops: " + troopsAmount);
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