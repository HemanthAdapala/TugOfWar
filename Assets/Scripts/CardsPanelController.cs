using System;
using UnityEngine;

public class CardsPanelController : MonoBehaviour
{
    [SerializeField] private CardData[] _cardDatas;
    [SerializeField] private CardDataUI _cardDataPrefab;
    [SerializeField] private Transform _cardsParent;

    private GameRoundManager _gameRoundManager;


    private CardData playerCard;
    private CardData opponentCard;

    public void Initialize()
    {
        _gameRoundManager = FindAnyObjectByType<GameRoundManager>();
        
        foreach (var cardData in _cardDatas)
        {
            var card = Instantiate(_cardDataPrefab, _cardsParent);
            CardDataUI cardUI = card.GetComponent<CardDataUI>();
            cardUI.SetCardData(cardData);
            cardUI.OnCardClicked += OnClickButtonEvent_CardDataUI;
        }
    }

    private void OnClickButtonEvent_CardDataUI(CardData data)
    {
        //TODO:- Handle Opponent Card info her but right now this is just for testing
        if (data != null)
        {
            SetPlayerCard(data);
            SetOpponentCard();
            Debug.Log($"Player Card: {data.cardName}, Opponent Card: {opponentCard.cardName} From {this.name}");
        }
        _gameRoundManager.SendCardDataToGameRoundManager(playerCard,opponentCard);
    }

    public CardData GetOpponentCard()
    {
        return opponentCard;
    }

    public CardData GetPlayerCard()
    {
        return playerCard;
    }

    public void SetPlayerCard(CardData cardData)
    {
        playerCard = cardData;
    }

    public void SetOpponentCard()
    {
        opponentCard = _cardDatas[UnityEngine.Random.Range(0, _cardDatas.Length)];
    }

    public void SetRandomCardForPlayer()
    {
        playerCard = _cardDatas[UnityEngine.Random.Range(0, _cardDatas.Length)];
    }

    public void SetRandomCardForOpponent()
    {
        opponentCard = _cardDatas[UnityEngine.Random.Range(0, _cardDatas.Length)];
    }

    public CardData GetRandomCardForPlayer()
    {
        SetRandomCardForPlayer();
        return playerCard;
    }


    public CardData GetRandomCardForOpponent()
    {
        SetRandomCardForOpponent();
        return opponentCard;
    }
}
