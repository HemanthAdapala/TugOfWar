using UnityEngine;

public class GameInitiator : MonoBehaviour
{
    [SerializeField] private RoundUpdater roundUpdater;
    [SerializeField] private Transform canvasParent;
    
    void Awake()
    {
        //Set RoundUpdater
        SetUpRoundUpdater();
        //Set Cards Data
        SetCardsData();
        //Start the Game
        StartGame();
        
    }

    private void StartGame()
    {
        
    }

    private void SetCardsData()
    {
        
    }

    private void SetUpRoundUpdater()
    {
        RoundUpdater roundUpdaterObject = Instantiate(roundUpdater,canvasParent);
        RoundData data = new RoundData(1, 5);
        roundUpdaterObject.Initialize(data);
    }
}
