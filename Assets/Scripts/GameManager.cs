using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON

    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                SetUpInstance();
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static void SetUpInstance()
    {
        instance = FindAnyObjectByType<GameManager>();
        if (instance == null)
        {
            var manager = new GameObject
            {
                name = "GameManager"
            };
            instance = manager.AddComponent<GameManager>();
            DontDestroyOnLoad(manager);
        }
    }

    #endregion


    [Header("Timers")]
    [SerializeField] private float preRoundDuration = 10f;
    [SerializeField] private float gameplayDuration = 30f;
    [SerializeField] private int totalRounds = 5;

    private Timer preRoundTimer;
    private Timer gameplayTimer;
    private int currentRound = 1;
    private GameState currentState;
    

    [SerializeField] private Timer timer;

    private void Start()
    {
        preRoundTimer = gameObject.AddComponent<Timer>();
        gameplayTimer = gameObject.AddComponent<Timer>();

        StartNewRound();
    }

    private void StartNewRound()
    {
        if (currentRound > totalRounds)
        {
            Debug.Log("Game Over!");
            currentState = GameState.GameOver;
            return;
        }

        Debug.Log($"Round {currentRound} started!");
        currentState = GameState.PreRound;

        // Start the pre-round timer
        preRoundTimer.duration = preRoundDuration;
        preRoundTimer.OnTimerFinished += OnPreRoundTimerFinished;
        preRoundTimer.StartTimer();
    }


    private void OnPreRoundTimerFinished(object sender, EventArgs e)
    {
        preRoundTimer.OnTimerFinished -= OnPreRoundTimerFinished;

        Debug.Log("Pre-round timer finished. Starting gameplay phase.");
        currentState = GameState.Gameplay;

        // Start the gameplay timer
        gameplayTimer.duration = gameplayDuration;
        gameplayTimer.OnTimerFinished += OnGameplayTimerFinished;
        gameplayTimer.StartTimer();
    }

    private void OnGameplayTimerFinished(object sender, EventArgs e)
    {
        gameplayTimer.OnTimerFinished -= OnGameplayTimerFinished;

        Debug.Log("Gameplay phase finished. Ending round.");
        currentState = GameState.RoundEnd;

        // Proceed to the next round
        currentRound++;
        StartNewRound();
    }

    public void StartGame(){

    }
}

public enum GameState
{
    PreRound,    // Waiting for players to pick cards
    Gameplay,    // Gameplay phase after card selection
    RoundEnd,    // End of the current round
    GameOver     // End of the game
}
