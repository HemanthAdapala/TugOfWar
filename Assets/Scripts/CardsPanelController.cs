using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsPanelController : MonoBehaviour
{

    [SerializeField] private SliderFillController sliderFillController;
    [SerializeField] public CardHolderUI[] cardHolders;
    [SerializeField] public CardData[] testingCardDatas;
    private List<CardUI> testingCardDataList = new List<CardUI>();

    [SerializeField] public GameObject cardPrefab;

    private void Start() {
        sliderFillController.OnSliderValueChanged += OnSliderValueChangedEvent_CardsPanelController;
    }


    public void InitializeCards()
    {
        foreach (var card in cardHolders)
        {
            var cardObject = Instantiate(cardPrefab, card.cardHolder);
            CardUI testingCardDataUI = cardObject.GetComponent<CardUI>();
            testingCardDataUI.SetData(testingCardDatas[UnityEngine.Random.Range(0, testingCardDatas.Length)]);
            testingCardDataList.Add(testingCardDataUI);
            Debug.Log(testingCardDataList.Count);
            //cardObject.GetComponent<CardHolderUI>().SetCardData(testingCardDataUI.cardData);
        }
    }

    //Check for every slider value changed event and check if the card value is equal to the slider value and if it is then enable the raycast target of the card or disable it
    private void OnSliderValueChangedEvent_CardsPanelController(object sender, SliderFillController.SliderValueChangedEventArgs e)
    {
        foreach (var card in testingCardDatas)
        {
            if (card.cardValue == e.value)
            {
                card.cardPrefab.GetComponent<Image>().raycastTarget = true;
            }
            else
            {
                card.cardPrefab.GetComponent<Image>().raycastTarget = false;
            }
        }
    }
}
