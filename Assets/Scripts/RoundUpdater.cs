using System;
using TMPro;
using UnityEngine;

public class RoundUpdater : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI roundUpdaterText;
    [SerializeField] private TextMeshProUGUI countDownText;

    public event EventHandler OnRoundStart;
    public event EventHandler OnRoundEnd;
    public event EventHandler OnCountDownStart;
    public event EventHandler OnCountDownEnd;

    private bool isCountingDown;
    private float remainingTime;

    public void Initialize(RoundData data)
    {
        Show();
        UpdateRoundText(data.Round);
        StartRound();
        StartCountDown(data.CountDown);
    }

    private void StartRound()
    {
        OnRoundStart?.Invoke(this, EventArgs.Empty);
    }

    private void StartCountDown(float time)
    {
        remainingTime = time;
        isCountingDown = true;
        OnCountDownStart?.Invoke(this, EventArgs.Empty);
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
        OnCountDownEnd?.Invoke(this, EventArgs.Empty);
        OnRoundEnd?.Invoke(this, EventArgs.Empty);
        Hide();
    }

    private void UpdateRoundText(int round)
    {
        roundUpdaterText.text = "Round " + round;
    }

    private void UpdateCountdownDisplay()
    {
        countDownText.text = Mathf.CeilToInt(remainingTime).ToString();
    }

    private void Show()
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);
    }

    private void Hide()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}

public class RoundData
{
    private int _round;
    private float _countDown;
    
    public int Round => _round;
    public float CountDown => _countDown;
    
    public RoundData(int round, float countDown)
    {
        this._round = round;
        this._countDown = countDown;
    }
}