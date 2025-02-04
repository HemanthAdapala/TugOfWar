using System;
using Unity.VisualScripting;
using UnityEngine;

public class CardsPanelController : MonoBehaviour
{
    [SerializeField] private CardData[] _cardDatas;
    [SerializeField] private CardDataUI _cardDataPrefab;
    [SerializeField] private Transform _cardsParent;
    
    private void OnEnable()
    {
        
    }

    public void Initialize()
    {
        foreach (var cardData in _cardDatas)
        {
            var card = Instantiate(_cardDataPrefab, _cardsParent);
            CardDataUI cardUI = card.GetComponent<CardDataUI>();
            cardUI.Initialize(cardData);
        }
    }
}
