using UnityEngine;
using System.Collections.Generic;
using System;

public class GameLobby : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private CardObjectSpawner playerSpawner;
    [SerializeField] private CardObjectSpawner opponentSpawner;

    [SerializeField] private GameRoundManager gameRoundManager;

    private List<CardData> playerSelectedCards = new List<CardData>();
    private List<CardData> opponentSelectedCards = new List<CardData>();

    public void StartGame()
    {
        // Initialize cards for the round
        playerSelectedCards.Clear();
        opponentSelectedCards.Clear();
        timer.StartTimer();
    }

    private void Start() {
        gameRoundManager.OnSentCardDataToLobby += OnSentCardDataToLobby_GameRoundManager;
    }

    private void OnSentCardDataToLobby_GameRoundManager(object sender, GameRoundManager.LobbyCardData e)
    {
        OnPlayerSelectedCard(e.playerCard);
        OnOpponentSelectedCard(e.opponentCard);
    }

    private void OnEnable()
    {
        // Subscribe to card click events
        CardDataUI.OnCardClicked += HandleCardClick;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        CardDataUI.OnCardClicked -= HandleCardClick;
    }

    private void HandleCardClick(CardData data)
    {
        Debug.Log("Card clicked: " + data.cardName);
    }

    // Called when the player selects a card (e.g., via UI)
    public void OnPlayerSelectedCard(CardData cardData)
    {
        playerSelectedCards.Add(cardData);
        playerSpawner.SpawnCardObject(cardData);
        CheckIfBothPlayersReady();
    }

    // Called when the opponent (AI/network) selects a card
    public void OnOpponentSelectedCard(CardData cardData)
    {
        opponentSelectedCards.Add(cardData);
        opponentSpawner.SpawnCardObject(cardData);
        CheckIfBothPlayersReady();
    }

    private void CheckIfBothPlayersReady()
    {
        if (playerSelectedCards.Count == opponentSelectedCards.Count)
        {
            // Start the tug-of-war match logic
            playerSpawner.StartMovingObjects();
            opponentSpawner.StartMovingObjects();
        }
    }
}