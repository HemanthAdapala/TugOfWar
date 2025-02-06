using UnityEngine;
using System;
using System.Threading.Tasks;

public class Timer : MonoBehaviour
{
    public float duration;
    public bool isRunning;

    public  event EventHandler OnTimerStarted;
    public event EventHandler OnTimerFinished;

    public async void StartTimer()
    {
        isRunning = true;
        OnTimerStarted?.Invoke(this, EventArgs.Empty);

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            await Task.Yield(); // Yield control back to the main thread
        }

        isRunning = false;
        OnTimerFinished?.Invoke(this, EventArgs.Empty);
    }
}