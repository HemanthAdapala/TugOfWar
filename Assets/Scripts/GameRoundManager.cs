using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class GameRoundManager : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI roundUpdaterText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private CardsPanelController cardsPanelController;
    [SerializeField] private Transform roundUpdaterPanel;
    [SerializeField] private Timer timer;

    public event EventHandler OnRoundCountDownStarted;
    public event EventHandler OnRoundCountDownEnded;
    public event EventHandler<LobbyCardData> OnSentCardDataToLobby;

    public class LobbyCardData{
        public CardData playerCard;
        public CardData opponentCard;
    }

    private bool isCountingDown;
    private float remainingTime;
    private int currentRound = 1;
    private int maxRounds = 5;
    private bool isTimerInitialized = false; 

    private CardData playerCard;
    private CardData opponentCard;

    private async void Start()
    {
        await StartNewRound();
    }

    private async Task StartNewRound()
    {
        if (currentRound > maxRounds) return;

        Debug.Log($"Setting up Round {currentRound}");

        SetUpRound(new RoundData(currentRound, 10));

        // Wait 10 seconds for card selection
        Debug.Log("Waiting 10 seconds for card selection...");
        await Task.Delay(TimeSpan.FromSeconds(10));

        Debug.Log("10 seconds over. Starting the timer now.");
        StartTimer(); // Start the round timer
    }

    private void StartTimer()
    {
        if (!isTimerInitialized)
        {
            if (timer != null && !timer.gameObject.activeSelf)
            {
                timer.gameObject.SetActive(true);
            }

            timer.StartTimer();
            timer.OnTimerStarted += OnTimerStartedEvent_Timer;
            timer.OnIntervalReached += OnIntervalReachedEvent_Timer;

            isTimerInitialized = true; 
        }
    }

    private void OnTimerStartedEvent_Timer()
    {
        Debug.Log("Timer Started");
    }

    private async void OnIntervalReachedEvent_Timer()
    {
        if (currentRound >= maxRounds) return;

        currentRound++;
        Debug.Log($"Round {currentRound} Started");

        await StartNewRound(); // Restart process
    }

    public void SetUpRound(RoundData data)
    {
        currentRound = data.Round;
        Show();
        InitializeCardsPanel();
        UpdateRoundText(currentRound, maxRounds);
        StartCountDown(data.CountDown);
    }

    private void InitializeCardsPanel()
    {
        if (!cardsPanelController.gameObject.activeSelf)
        {
            cardsPanelController.gameObject.SetActive(true);
        }
        cardsPanelController.Initialize();
    }

    private void StartCountDown(float time)
    {
        OnRoundCountDownStarted?.Invoke(this, EventArgs.Empty);
        remainingTime = time;
        isCountingDown = true;
        Debug.Log("CountDown Started");
    }

    private void Update()
    {
        if (!isCountingDown) return;
        UpdateTimer();
    }

    private void UpdateTimer()
{
    remainingTime -= Time.deltaTime;

    // Ensure the timer stops exactly at the correct values (2:00, 1:30, 1:00, etc.)
    if (Mathf.CeilToInt(remainingTime) <= 0)
    {
        EndCountDown();
        return;
    }

    UpdateCountdownDisplay();
}


    private void EndCountDown()
    {
        remainingTime = 0;
        isCountingDown = false;
        UpdateCountdownDisplay();

        Debug.Log("CountDown End");
        CheckForCurrentCardData();
        OnRoundCountDownEnded?.Invoke(this, EventArgs.Empty);
        Hide();
    }

    private void CheckForCurrentCardData()
    {
        if (playerCard == null || opponentCard == null)
        {
            Debug.LogWarning("PlayerCard or OpponentCard is null! Waiting for selection.");
            return;
        }

        OnSentCardDataToLobby?.Invoke(this, new LobbyCardData { playerCard = playerCard, opponentCard = opponentCard });
        Debug.Log($"PlayerCard: {playerCard.cardName}, OpponentCard: {opponentCard.cardName}");

        playerCard = null;
        opponentCard = null;
    }

    private void UpdateRoundText(int round, int maxRound)
    {
        roundUpdaterText.text = $"Round {round}/{maxRound}";
    }

    private void UpdateCountdownDisplay()
{
    int displayTime = Mathf.CeilToInt(remainingTime);
    countDownText.text = displayTime.ToString();
}


    private void Show()
    {
        if (!roundUpdaterPanel.gameObject.activeSelf)
            roundUpdaterPanel.gameObject.SetActive(true);
    }

    private void Hide()
    {
        if (roundUpdaterPanel.gameObject.activeSelf)
            roundUpdaterPanel.gameObject.SetActive(false);
    }

    public void SendCardDataToGameRoundManager(CardData pc, CardData oc)
    {
        playerCard = pc;
        opponentCard = oc;
    }
}



public class RoundData
{
    private int _currentround;
    private int _maxRound;
    private float _countDown;


    public int Round => _currentround;
    public float CountDown => _countDown;

    public int MaxRound
    {
        get => _maxRound;
        set => _maxRound = value;
    }
    
    public RoundData(int round, float countDown)
    {
        this._currentround = round;
        this._countDown = countDown;
    }
}