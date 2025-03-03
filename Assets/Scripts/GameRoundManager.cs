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
    [SerializeField] private Transform gameoverPanel;
    [SerializeField] private Timer timer;
    [SerializeField] private SliderFillController sliderFillController;

    public event EventHandler OnRoundCountDownStarted;
    public event EventHandler OnRoundCountDownEnded;

    public class LobbyCardData
    {
        public CardData playerCard;
        public CardData opponentCard;
    }

    private bool isCountingDown;
    private float remainingTime;
    private int currentRound = 1;
    private int maxRounds = 3;
    private bool isTimerInitialized = false;


    private void Awake()
    {
        //Disable the gameover panel
        gameoverPanel.gameObject.SetActive(false);

        //Disable the timer
        timer.gameObject.SetActive(false);
        //Disable the cards panel
        cardsPanelController.gameObject.SetActive(false);

        //Check if Round Updater GameObject is disabled then Enable the Round Updater GameObject
        if (!roundUpdaterPanel.gameObject.activeSelf)
        {
            roundUpdaterPanel.gameObject.SetActive(true);
        }

        timer.OnTimerFinished += () =>
        {
            OnTimerFinishedEvent_Timer();
        };
    }

    private void OnTimerFinishedEvent_Timer()
    {
        Debug.Log("Timer Finished");
        EndCountDown();
        roundUpdaterPanel.gameObject.SetActive(false);
        gameoverPanel.gameObject.SetActive(true);
    }

    private async void Start()
    {
        await StartNewRound();
        OnRoundCountDownStarted += OnRoundCountDownStartedEvent_GameRoundManager;
        OnRoundCountDownEnded += OnRoundCountDownEndedEvent_GameRoundManager;
    }

    private void OnRoundCountDownEndedEvent_GameRoundManager(object sender, EventArgs e)
    {
        //Check if Timer is Disabled then Enable the timer
        if (!timer.gameObject.activeSelf)
        {
            timer.gameObject.SetActive(true);
            StartTimer(); // Start the round timer
            //Check if slider fill controller is disabled then Enable the slider fill controller
            if (!sliderFillController.gameObject.activeSelf)
            {
                sliderFillController.gameObject.SetActive(true);
            }

            //Check if cards panel is disabled then Enable the cards panel
            if (!cardsPanelController.gameObject.activeSelf)
            {
                cardsPanelController.gameObject.SetActive(true);
                cardsPanelController.InstantiateCardsInCardHolders();
            }

        }
    }


    private void OnRoundCountDownStartedEvent_GameRoundManager(object sender, EventArgs e)
    {
        Debug.Log("Round CountDown Started");
    }

    private async Task StartNewRound()
    {
        if (currentRound > maxRounds) return;

        Debug.Log($"Setting up Round {currentRound}");

        SetUpRound(new RoundData(currentRound, 3));
        Show();
        await Task.Delay(100); // Add small delay between setup steps
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
        //InitializeCardsPanel();
        UpdateRoundText(currentRound, maxRounds);
        StartCountDown(data.CountDown);
    }

    private void InitializeCardsPanel()
    {
        if (!cardsPanelController.gameObject.activeSelf)
        {
            cardsPanelController.gameObject.SetActive(true);
        }
        //cardsPanelController.Initialize();
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
        OnRoundCountDownEnded?.Invoke(this, EventArgs.Empty);
        Hide();
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