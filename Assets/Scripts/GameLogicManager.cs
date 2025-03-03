using System.Collections.Generic;
using UnityEngine;

public class GameLogicManager
{
    public void InitializeGameLogic(List<Avatar> selectedCards, GameLogicType gameLogicType){
        Debug.Log("GameLogicManager initialized");
    }

    public float CalculatePullStrength(List<Avatar> selectedCards){
        Debug.Log("Calculating pull strength");
        return 0;
    }
}

public enum GameLogicType{
    Player,
    Opponent
}
