using System;
using System.Collections.Generic;
using UnityEngine;

public class CardsPanelController : MonoBehaviour
{
    [SerializeField] private SliderFillController sliderFillController;
    [SerializeField] public CardHolderUI[] cardHolders;
    //For Testing
    [SerializeField] public CardData[] cardsData;
    private List<CardUI> spawnedCards = new List<CardUI>();

    [SerializeField] public GameObject cardPrefab;

    private void Awake() {
        sliderFillController.OnSliderValueChanged += OnSliderValueChangedEvent_CardsPanelController;
    }


    public void InstantiateCardsInCardHolders()
    {
        foreach (var card in cardHolders)
        {
            var cardRef = Instantiate(cardPrefab, card.cardHolder);
            CardUI cardUI = cardRef.GetComponent<CardUI>();
            cardUI.SetCardUIData(cardsData[UnityEngine.Random.Range(0, cardsData.Length)]);
            spawnedCards.Add(cardUI);
            Debug.Log(spawnedCards.Count);
            SetCardDataToCardHolder(cardUI.GetCardData());
        }
    }

    public void SetCardDataToCardHolder(CardData cardData){
        foreach (var card in cardHolders)
        {
            card.SetCardData(cardData);
        }
    }
    //Check for every slider value changed event and check if the card value is equal to the slider value and if it is then enable the raycast target of the card or disable it
    private void OnSliderValueChangedEvent_CardsPanelController(object sender, SliderFillController.SliderValueChangedEventArgs e)
    {
        foreach (var card in spawnedCards)
        {
            Debug.Log(card.GetCardData().cardName + " " + card.GetCardValue() + " " + e.value);
            if(card.GetCardValue() >= e.value){
                card.GetCardImageOutline().gameObject.SetActive(true);
            }
            else{
                card.GetCardImageOutline().gameObject.SetActive(false);
            }
        }
    }
}
