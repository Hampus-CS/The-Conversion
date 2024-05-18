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
    public bool canMoveTroops = false;
    public Flank allowedFlank;
    public int allowedMoves;

    [Header("Return discarded cards to the deck")]
    public int discardLimit;

    public static GameManager Instance { get; private set; }

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
        InitializeGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            EndTurn();
        }
    }

    void InitializeGame()
    {
        ShuffleDeck();
        DealInitialHand();
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
        canMoveTroops = false;
        DrawACardForNextTurn();
        Debug.Log("EndTurn called. State reset. Ready for next card.");
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

    public void PlayCard(Card card)
    {
        Debug.Log($"GameManager PlayCard: Flank = {card.cardFlank}, Troops = {card.troopsAmount}");
        cardPlayedThisTurn = true;
        allowedFlank = card.cardFlank;
        allowedMoves = card.troopsAmount;
        canMoveTroops = true;
        Debug.Log($"GameManager PlayCard: CanMoveTroops = {canMoveTroops}, AllowedFlank = {allowedFlank}, AllowedMoves = {allowedMoves}");
    }
}