using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class Timer : MonoBehaviour
{
    public float startTimer = 0f;
    public float maxTimer = 150f;
    public float intervalBetweenSetTimer = 30f;
    public float waitTimerForNextSet = 3f;
    public TextMeshProUGUI timerText;

    private float currentTime;
    private bool isTimerRunning;

    private async void Start()
    {
        currentTime = startTimer;
        isTimerRunning = true;

        await RunTimer();
    }

    private async Task RunTimer()
    {
        while (isTimerRunning)
        {
            // Increment the timer
            currentTime += Time.deltaTime;

            // Check if the timer has reached the max time
            if (currentTime >= maxTimer)
            {
                currentTime = maxTimer;
                isTimerRunning = false;
                UpdateTimerDisplay();
                break;
            }

            // Check if the timer has reached an interval
            if (currentTime % intervalBetweenSetTimer < Time.deltaTime)
            {
                // Pause the timer for the wait time
                isTimerRunning = false;
                UpdateTimerDisplay();
                await Task.Delay((int)(waitTimerForNextSet * 1000)); // Convert seconds to milliseconds
                isTimerRunning = true;
            }

            UpdateTimerDisplay();
            await Task.Yield(); // Allow the task to yield control back to the main thread
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = FormatTime(currentTime);
        }
    }

    private static string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return $"{minutes:00}:{seconds:00}";
    }
}