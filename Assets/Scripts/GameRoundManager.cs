using System;
using TMPro;
using UnityEngine;

public class GameRoundManager : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI roundUpdaterText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private CardsPanelController cardsPanelController;
    [SerializeField] private Transform roundUpdaterPanel;

    public event EventHandler OnRoundCountDownStarted;
    public event EventHandler OnRoundCountDownEnded;

    public event EventHandler<LobbyCardData> OnSentCardDataToLobby;
    public class LobbyCardData{
        public CardData playerCard;
        public CardData opponentCard;
    }
    private bool isCountingDown;
    private float remainingTime;

    private CardData playerCard;
    private CardData opponentCard;

    



    public void SetUpRound(RoundData data){
        Show();
        InitializeCardsPanel();
        UpdateRoundText(data.Round,data.MaxRound);
        StartCountDown(data.CountDown);
    }

    private void InitializeCardsPanel()
    {
        if(!cardsPanelController.gameObject.activeSelf){
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
        
        if (remainingTime <= 0)
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
        if(playerCard != null && opponentCard != null){
            OnSentCardDataToLobby?.Invoke(this, new LobbyCardData{playerCard = playerCard, opponentCard = opponentCard});
        }
        //Start from here getting null reference
        Debug.Log("PlayerCard: " + playerCard.cardName + " OpponentCard: " + opponentCard.cardName);
    }

    private void UpdateRoundText(int round, int maxRound)
    {
        roundUpdaterText.text = "Round " + round + "/" + maxRound;
    }

    private void UpdateCountdownDisplay()
    {
        countDownText.text = Mathf.CeilToInt(remainingTime).ToString();
    }

    private void Show()
    {
        if(!roundUpdaterPanel.gameObject.activeSelf)
            roundUpdaterPanel.gameObject.SetActive(true);
    }

    private void Hide()
    {
        if(roundUpdaterPanel.gameObject.activeSelf)
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