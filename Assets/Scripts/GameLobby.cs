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

    private void Start() {
        // Initialize cards for the round
        playerSelectedCards.Clear();
        opponentSelectedCards.Clear();
        //timer.StartTimer();
        gameRoundManager.OnSentCardDataToLobby += OnSentCardDataToLobby_GameRoundManager;
    }

    private void OnSentCardDataToLobby_GameRoundManager(object sender, GameRoundManager.LobbyCardData e)
    {
        OnPlayerSelectedCard(e.playerCard);
        OnOpponentSelectedCard(e.opponentCard);
    }

    // Called when the player selects a card (e.g., via UI)
    public void OnPlayerSelectedCard(CardData cardData)
    {
        playerSelectedCards.Add(cardData);
        playerSpawner.SpawnCardObject(cardData);
    }

    // Called when the opponent (AI/network) selects a card
    public void OnOpponentSelectedCard(CardData cardData)
    {
        opponentSelectedCards.Add(cardData);
        opponentSpawner.SpawnCardObject(cardData);
    }
}