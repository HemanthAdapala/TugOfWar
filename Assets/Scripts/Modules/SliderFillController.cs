using System;
using System.Collections;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SliderFillController : MonoBehaviour
{
    [Header("Slider Settings")]
    [SerializeField] private Slider sliderComponent; // Unity Slider component
    [SerializeField] private int sliderMaxParts = 10; // Total divisions in the slider
    [SerializeField] private float fillDuration = 1f; // Duration to lerp between parts
    [SerializeField] private float waitTimeBetweenParts = 0.3f; // Delay before filling the next part
    [SerializeField] private int subtractValue = 5; // Value to subtract on button press
    [SerializeField] private Ease sliderEase = Ease.InSine;

    private int currentSliderValue;
    
    // Event for when the slider value changes
    public event EventHandler<SliderValueChangedEventArgs> OnSliderValueChanged;
    public class SliderValueChangedEventArgs : EventArgs{
        public int value;
    }

    private Coroutine fillCoroutine;

    void Start()
    {
        if (sliderComponent == null)
        {
            Debug.LogError("Slider Component is not assigned!");
            return;
        }

        sliderComponent.maxValue = sliderMaxParts;  // Set max value
        sliderComponent.value = 0; // Start at 0

        // Start the fill animation
        StartFillAnimation(0);
    }

    public void StartFillAnimation(float startFillValue)
    {
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }
        fillCoroutine = StartCoroutine(FillSliderSmoothly(startFillValue));
    }

    private IEnumerator FillSliderSmoothly(float startFillValue)
    {
        yield return new WaitForSeconds(1f);
        int startIndex = Mathf.CeilToInt(startFillValue);

        for (int i = startIndex; i <= sliderMaxParts; i++)
        {
            currentSliderValue = i;
            DOTween.To(() => sliderComponent.value, x => sliderComponent.value = x, i, fillDuration)
                .SetEase(sliderEase);

            OnSliderValueChanged?.Invoke(this, new SliderValueChangedEventArgs { value = i });
            yield return new WaitForSeconds(fillDuration + waitTimeBetweenParts);
        }
    }

    public void SubtractAndRefill(int value)
    {
        currentSliderValue -= value;
        float newFillValue = Mathf.Clamp(sliderComponent.value - value, 0, sliderMaxParts);
        sliderComponent.value = newFillValue;
        StartFillAnimation(newFillValue);
    }

    public int GetSliderValue()
    {
        return currentSliderValue;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SliderFillController))]
public class SliderFillControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SliderFillController script = (SliderFillController)target;
        if (GUILayout.Button("Subtract and Refill"))
        {
            script.SubtractAndRefill(5);
        }
    }
}
#endif

public enum EnergySliderState
{
    Idle,
    Active,
    Depleted,
    Charging,
    Discharging,
    OverLoaded,
}
