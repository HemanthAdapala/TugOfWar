using System;
using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private GameRoundManager roundUpdater;
    [SerializeField] private Transform canvasParent;
    
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
        
    }

    private void SetUpRoundUpdater()
    {
        GameRoundManager roundUpdaterObject = Instantiate(roundUpdater,canvasParent);
        RoundData data = new RoundData(1, 10);
        roundUpdaterObject.SetUpRound(data);
    }
}
