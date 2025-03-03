using UnityEngine;
using System.Collections.Generic;
using System;
using TMPro;
using System.Collections;

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

    [SerializeField] private TextMeshProUGUI opponentRightSideCanvas;
    [SerializeField] private TextMeshProUGUI opponentLeftSideCanvas;


    //Testing Cases
    [SerializeField] private float opponentRightSideAverage;
    [SerializeField] private float opponentLeftSideAverage;
    [SerializeField] public bool isOpponentAverageCalculated;



    private List<Avatar> playerSelectedRightCards;
    private List<Avatar> playerSelectedLeftCards;
    private List<Avatar> opponentSelectedRightCards;
    private List<Avatar> opponentSelectedLeftCards;

    private event EventHandler<PlayerStatsChangedEventArgs> OnPlayerStatsChanged;
    private class PlayerStatsChangedEventArgs : EventArgs
    {
        public SpawnerSide spawnerSide;
        public float playerStats;
    }

    private GameLogicManager gameLogicManager;

    private void Start()
    {
        playerSelectedRightCards = new List<Avatar>();
        playerSelectedLeftCards = new List<Avatar>();
        opponentSelectedRightCards = new List<Avatar>();
        opponentSelectedLeftCards = new List<Avatar>();

        gameLogicManager = new GameLogicManager();

    }

    #region TEST CASES

    public void StartRandomOpponentAverageCalculationTest()
    {
        StartCoroutine(StartRandomOpponentAverageCalculation());
    }

    public IEnumerator StartRandomOpponentAverageCalculation()
    {
        int randomDuration = 0;
        while (isOpponentAverageCalculated)
        {
            randomDuration = UnityEngine.Random.Range(1, 10);
            DebugHelper.LogColor("Random Duration: " + randomDuration, Color.yellow);
            var randomAverage = RandomOpponentAverageCalculation();
            DebugHelper.LogColor("Random Average: " + randomAverage, Color.yellow);
            opponentLeftSideAverage = randomAverage;
            opponentRightSideAverage = randomAverage;
            CalculateAverageStatsByOpponent();

            yield return new WaitForSeconds(randomDuration);
        }
    }

    private void CalculateAverageStatsByOpponent()
    {
        opponentRightSideCanvas.text = $"Average: {opponentRightSideAverage:F2}";
        opponentLeftSideCanvas.text = $"Average: {opponentLeftSideAverage:F2}";
    }

    private float RandomOpponentAverageCalculation()
    {
        return UnityEngine.Random.Range(1f, 100f);
    }

    #endregion


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
        var team = spawnerSide == SpawnerSide.Right ? playerSelectedRightCards : playerSelectedLeftCards;
        var average = customWeightingStats.CalculateAverageStatsByTeam(team);
        var canvas = spawnerSide == SpawnerSide.Right ? playerRightSideCanvas : playerLeftSideCanvas;
        canvas.text = $"Average: {average:F2}";
        OnPlayerStatsChanged?.Invoke(this, new PlayerStatsChangedEventArgs { spawnerSide = spawnerSide, playerStats = average });
    }

    public void CalculateAverageStatsByTeam()
    {
        float teamAverage = customWeightingStats.CalculateAverageStatsByTeam(playerSelectedRightCards);
        Debug.Log("Team Average: " + teamAverage);
    }


}