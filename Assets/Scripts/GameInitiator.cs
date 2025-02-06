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

    private void SetUpRoundUpdater()
    {
        if(roundUpdater != null && !roundUpdater.gameObject.activeSelf){
            roundUpdater.gameObject.SetActive(true);
        }
        RoundData data = new RoundData(1, 10);
        roundUpdater.SetUpRound(data);
    }
}
