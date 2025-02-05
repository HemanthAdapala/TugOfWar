using System;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private RoundUpdater roundUpdater;
    [SerializeField] private Transform canvasParent;
    
    [SerializeField] private CardsPanelController cardsPanelController;
    
    void Awake()
    {
        //Set RoundUpdater
        SetUpRoundUpdater();
        
    }

    private void StartGame()
    {
        
    }

    private void SetCardsData()
    {
        CardsPanelController cardsPanelControllerObject = Instantiate(cardsPanelController,canvasParent);
        cardsPanelControllerObject.Initialize();
    }

    private void SetUpRoundUpdater()
    {
        RoundUpdater roundUpdaterObject = Instantiate(roundUpdater,canvasParent);
        RoundData data = new RoundData(1, 5);
        roundUpdaterObject.Initialize(data, () =>
        {
            SetCardsData();
        });
    }
}
