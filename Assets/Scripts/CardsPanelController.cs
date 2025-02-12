using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardsPanelController : MonoBehaviour
{
    //Singleton
    public static CardsPanelController Instance;

    private void Awake() {
        Instance = this;
        sliderFillController.OnSliderValueChanged += OnSliderValueChangedEvent_CardsPanelController;
    }

    [SerializeField] private SliderFillController sliderFillController;
    [SerializeField] public CardHolderUI[] cardHolders;
    //For Testing
    [SerializeField] public CardData[] cardsData;
    private List<CardUI> spawnedCards = new List<CardUI>();

    [SerializeField] public GameObject cardPrefab;

    [SerializeField] private Transform nextQueueCardParent;

    private CardUI nextQueueCard = null;


    public void InstantiateCardsInCardHolders()
    {
        foreach (var card in cardHolders)
        {
            var cardRef = Instantiate(cardPrefab, card.cardHolder);
            CardUI cardUI = cardRef.GetComponent<CardUI>();
            cardUI.SetCardUIData(cardsData[UnityEngine.Random.Range(0, cardsData.Length)]);
            spawnedCards.Add(cardUI);
            SetCardDataToCardHolder(cardUI.GetCardData());
        }
        //Instantiate the next queue card
        InstantiateNextQueueCard(nextQueueCardParent);
    }

    public void SetNextQueueCardData(){
        nextQueueCard.SetCardUIData(cardsData[UnityEngine.Random.Range(0, cardsData.Length)]);
        nextQueueCard.gameObject.SetActive(true);
    }

    private CardUI InstantiateNextQueueCard(Transform parent){
        var nextCard = Instantiate(cardPrefab, parent);
        CardUI cardRef = nextCard.GetComponent<CardUI>();
        cardRef.GetCardImageOutline().gameObject.SetActive(false);
        nextCard.transform.SetParent(parent);
        nextQueueCard = cardRef;
        SetNextQueueCardData();
        return nextQueueCard;
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
            if(card.GetCardValue() >= e.value){
                card.GetCardImageOutline().gameObject.SetActive(true);
            }
            else{
                card.GetCardImageOutline().gameObject.SetActive(false);
            }
        }
    }

    public void UpdateSliderValue(int value){
        //Update the slider value by subtracting the value from the slider fill controller
        sliderFillController.SubtractAndRefill(value);
    }

    public void RemoveCardFromList(CardUI cardUI)
    {
        CheckForSpawnedCards();

        int index = Array.IndexOf(cardHolders, cardUI.GetComponentInParent<CardHolderUI>());
        spawnedCards.Remove(cardUI);
        Destroy(cardUI.gameObject);
        AddNextQueueCardToCardHolder(index);
    }

    private void CheckForSpawnedCards()
    {
        //Add the next queue card to the spawned cards list
        spawnedCards.Add(nextQueueCard);
        //Check for SpawnedCards list if the card value is greater than the slider value and if it is then disable the raycast target of the card
        foreach (var card in spawnedCards)
        {
            if (card.GetCardValue() >= sliderFillController.GetSliderValue())
            {
                card.GetCardImageOutline().gameObject.SetActive(true);
            }
        }
    }

    private void AddNextQueueCardToCardHolder(int index)
    {
        //Get the index of the Removed card in CardHolders array
        var transform = cardHolders[index].GetCardHolderTransform();
        MoveNextQueueCardToCardHolder(transform,() => {
            //Instantiate the next queue card
            var newQueueCard = InstantiateNextQueueCard(nextQueueCardParent);
            nextQueueCard = newQueueCard;
            SetCardDataToCardHolder(nextQueueCard.GetCardData());
        });
    }

    //Using DoTween to move the next queue card to the card holder
    private void MoveNextQueueCardToCardHolder(Transform transform,Action OnComplete)
    {
        nextQueueCard.transform.DOMove(transform.position, 0.5f);
        nextQueueCard.transform.DOScale(Vector3.one, 0.5f);
        nextQueueCard.transform.SetParent(transform);
        OnComplete?.Invoke();
    }
}
