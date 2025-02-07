using UnityEngine;
using TMPro;
using System;
using System.Threading.Tasks;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText; // UI Text to display time

    public float duration;             // Total time before ending
    public float intervalTimer;        // Time after which it pauses
    public float waitForSelectionTime; // Pause duration
    public bool isRunning;

    public event Action OnTimerStarted;
    public event Action OnTimerFinished;
    public event Action OnIntervalReached;
    public event Action OnWaitForSelectionComplete;

    private async Task TimerLoop()
    {
        float remainingTime = duration;
        isRunning = true;
        OnTimerStarted?.Invoke();
        UpdateTimerUI(remainingTime); // Initial UI update

        while (remainingTime > 0)
        {
            float intervalElapsed = 0f;

            // Run timer until intervalTimer is reached
            while (intervalElapsed < intervalTimer && remainingTime > 0)
            {
                await Task.Delay(100); // Update every 100ms
                intervalElapsed += 0.1f;
                remainingTime -= 0.1f;
                UpdateTimerUI(remainingTime);
            }

            // Trigger interval event and wait
            if (remainingTime > 0)
            {
                OnIntervalReached?.Invoke();
                await Task.Delay(TimeSpan.FromSeconds(waitForSelectionTime));
                OnWaitForSelectionComplete?.Invoke();
            }
        }

        isRunning = false;
        UpdateTimerUI(0); // Ensure UI shows 00:00
        OnTimerFinished?.Invoke();
    }

    public async void StartTimer()
    {
        if (!isRunning)
        {
            await TimerLoop();
        }
    }

    private void UpdateTimerUI(float time)
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(time / 60);
            int seconds = Mathf.FloorToInt(time % 60);
            timerText.text = $"{minutes:D2}:{seconds:D2}"; // Format as MM:SS
        }
    }
}
