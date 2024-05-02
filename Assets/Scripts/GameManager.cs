using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    [Header("Card Lists")]
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> playedCardsThisTurn = new List<Card>();

    [Header("Card Slots")]
    public Transform[] cardSlots;
    public bool[] availableCardSlots;

    [Header("Turn")]
    public bool cardPlayedThisTurn = false;

    [Header("Return discarded cards to the deck")]
    public int discardLimit;

    [Header("Troop settings")]
    public GameObject troopPrefab; // Drag your troop prefab here in the inspector
    public int numberOfTroops = 5; // Set the number of troops to spawn
    public float spawnY = -3.5f; // Y position for placing troops at the bottom of the map

    /*
    [Header("Text")]
    public TMP_Text deckSizeText;
    public TMP_Text discradPileSizeText;
    */

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        /*
        if (deckSizeText != null && discradPileSizeText != null)
        {
            deckSizeText.text = deck.Count.ToString();
            discradPileSizeText.text = discardPile.Count.ToString();
        }
        */
    }

    void InitializeGame()
    {
        ShuffleDeck();
        DealInitialHand();
        SpawnTroopsAtBottom();
    }

    void DealInitialHand()
    {
        for (int i = 0; i < 5; i++)
        {
            if (deck.Count > 0 && i < cardSlots.Length && availableCardSlots[i])
            {
                DrawCard(i);
            }
        }
    }

    public void DrawCard(int slotIndex)
    {
        if (deck.Count > 0 && availableCardSlots[slotIndex])
        {
            Card randCard = deck[Random.Range(0, deck.Count)];
            randCard.gameObject.SetActive(true);
            randCard.hasBeenPlayed = false;
            randCard.handIndex = slotIndex;
            randCard.transform.position = cardSlots[slotIndex].position;
            availableCardSlots[slotIndex] = false;
            deck.Remove(randCard);

            if (deck.Count < 10)
            {
                Shuffle();
            }
        }
    }

    public void Shuffle()
    {
        if (discardPile.Count >= discardLimit)
        {
            foreach (Card card in new List<Card>(discardPile))
            {
                card.hasBeenPlayed = false;
                deck.Add(card);
                card.gameObject.SetActive(true);
            }
            discardPile.Clear();
        }
    }

    public void EndTurn()
    {
        foreach (Card card in playedCardsThisTurn)
        {
            card.gameObject.SetActive(false);
            card.transform.position = new Vector3(-27, 0, 0);
        }
        playedCardsThisTurn.Clear();
        cardPlayedThisTurn = false;
        DrawACardForNextTurn();
    }

    void DrawACardForNextTurn()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            if (availableCardSlots[i])
            {
                DrawCard(i);
                break;
            }
        }
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    void SpawnTroopsAtBottom()
    {
        float startX = CalculateStartX(); // Calculate starting X position based on number of troops and map size
        for (int i = 0; i < numberOfTroops; i++)
        {
            // Instantiate the troop at the calculated position
            GameObject troop = Instantiate(troopPrefab, new Vector3(startX + i * 1.0f, spawnY, 0), Quaternion.identity);
            // Adjust the X position and other properties as needed
        }
    }

    float CalculateStartX()
    {
        // Example calculation for starting X position (this may need to be adjusted based on your game's map and layout)
        return -numberOfTroops / 2.0f;
    }

}