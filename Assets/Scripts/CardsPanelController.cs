using System;
using Unity.VisualScripting;
using UnityEngine;

public class CardsPanelController : MonoBehaviour
{
    [SerializeField] private CardData[] _cardDatas;
    [SerializeField] private CardDataUI _cardDataPrefab;
    [SerializeField] private Transform _cardsParent;

    private CardData playerCard;
    private CardData opponentCard;

    public void Initialize()
    {
        foreach (var cardData in _cardDatas)
        {
            var card = Instantiate(_cardDataPrefab, _cardsParent);
            CardDataUI cardUI = card.GetComponent<CardDataUI>();
            cardUI.SetCardData(cardData);
        }
    }

    public CardData GetOpponentCard()
    {
        return opponentCard;
    }

    public CardData GetPlayerCard()
    {
        return playerCard;
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
