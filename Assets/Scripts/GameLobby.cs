using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;

public class GameLobby : MonoBehaviour
{
    //Singleton
    public static GameLobby Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] private Timer timer;
    [SerializeField] private CardObjectSpawner playerSpawner;
    [SerializeField] private CardObjectSpawner opponentSpawner;
    [SerializeField] private GameRoundManager gameRoundManager;
    [SerializeField] private CustomWeightingStats customWeightingStats;

    [SerializeField] private TextMeshProUGUI playerRightSideCanvas;
    [SerializeField] private TextMeshProUGUI playerLeftSideCanvas;



    private List<Avatar> playerSelectedRightCards;
    private List<Avatar> playerSelectedLeftCards;
    private List<Avatar> opponentSelectedRightCards;
    private List<Avatar> opponentSelectedLeftCards;

    private GameLogicManager gameLogicManager;

    private void Start()
    {

        playerSelectedRightCards = new List<Avatar>();
        playerSelectedLeftCards = new List<Avatar>();
        opponentSelectedRightCards = new List<Avatar>();
        opponentSelectedLeftCards = new List<Avatar>();

        gameLogicManager = new GameLogicManager();
    }

    public List<Avatar> GetPlayerSelectedCardsBySide(SpawnerSide side)
    {
        if (side == SpawnerSide.Right)
        {
            return playerSelectedRightCards;
        }
        else if (side == SpawnerSide.Left)
        {
            return playerSelectedLeftCards;
        }
        else
        {
            return new List<Avatar>();
        }
    }

    public List<Avatar> GetOpponentSelectedCardsBySide(SpawnerSide side)
    {
        if (side == SpawnerSide.Right)
        {
            return opponentSelectedRightCards;
        }
        else if (side == SpawnerSide.Left)
        {
            return opponentSelectedLeftCards;
        }
        else
        {
            return new List<Avatar>();
        }
    }

    public void AddPlayerSelectedCard(Avatar avatar, SpawnerSide currentSpawnSide)
    {
        if (currentSpawnSide == SpawnerSide.Right)
        {
            playerSelectedRightCards.Add(avatar);
        }
        else if (currentSpawnSide == SpawnerSide.Left)
        {
            playerSelectedLeftCards.Add(avatar);
        }
    }

    public Transform GetLastObjectPosition(SpawnerSide side)
    {
        if (side == SpawnerSide.Right)
        {
            return playerSelectedRightCards[playerSelectedRightCards.Count - 2].transform;
        }
        else if (side == SpawnerSide.Left)
        {
            return playerSelectedLeftCards[playerSelectedLeftCards.Count - 2].transform;
        }
        else
        {
            return null;
        }
    }

    public void CalculateAverageStatsByPlayer(SpawnerSide spawnerSide)
    {
        if (spawnerSide == SpawnerSide.Right)
        {
            var average = customWeightingStats.CalculateAverageStatsByTeam(playerSelectedRightCards);
            playerRightSideCanvas.text = "Average: " + average.ToString("F2");
        }
        else if (spawnerSide == SpawnerSide.Left)
        {
            var average = customWeightingStats.CalculateAverageStatsByTeam(playerSelectedLeftCards);
            playerLeftSideCanvas.text = "Average: " + average.ToString("F2");
        }
    }

    public void CalculateAverageStatsByTeam()
    {
        float teamAverage = customWeightingStats.CalculateAverageStatsByTeam(playerSelectedRightCards);
        Debug.Log("Team Average: " + teamAverage);
    }
}