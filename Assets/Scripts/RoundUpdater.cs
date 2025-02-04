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

    private void Awake()
    {
                
    }

    public void SetData(RoundData data)
    {
        roundUpdaterText.text = "Round " + data.Round;
        countDownText.text = data.CountDown.ToString();
    }

    public void OnEnable()
    {
        
    }

    public void Show()
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);
    }

    public void Hide()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}

public class RoundData
{
    private int _round;
    private int _countDown;
    
    public int Round => _round;
    public int CountDown => _countDown;
    
    public RoundData(int round, int countDown)
    {
        this._round = round;
        this._countDown = countDown;
    }
}
