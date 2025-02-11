using System;
using System.Collections;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SliderFillController : MonoBehaviour
{
    [Header("Slider Settings")]
    [SerializeField] private Image sliderImage; // The UI Image component used as a slider
    [SerializeField] private int sliderMaxParts = 10; // Total divisions in the slider
    [SerializeField] private float fillDuration = 0.5f; // Duration to lerp between parts
    [SerializeField] private float waitTimeBetweenParts = 0.3f; // Delay before filling the next part
    [SerializeField] private int subtractValue = 5; // Value to subtract on button press
    
    [SerializeField] private float sliderRoundSpeed = 0f;
    
    [SerializeField] private Ease sliderEase = Ease.InSine;
    
    //Event for when the slider value changes and Round to the nearest integer
    public event EventHandler<SliderValueChangedEventArgs> OnSliderValueChanged;
    public class SliderValueChangedEventArgs : EventArgs{
        public int value;
    }

    private Coroutine fillCoroutine;

    void Start()
    {
        if (sliderImage == null)
        {
            Debug.LogError("Slider Image is not assigned!");
            return;
        }
        
        sliderImage.fillAmount = 0f; // Start with an empty slider

        // Start the fill animation
        StartFillAnimation(0f);
    }

    public void StartFillAnimation(float startFillAmount)
    {
        if (fillCoroutine != null)
        {
            StopCoroutine(fillCoroutine);
        }
        fillCoroutine = StartCoroutine(FillSliderSmoothly(startFillAmount));
    }

    private IEnumerator FillSliderSmoothly(float startFillAmount)
    {
        float currentFill = startFillAmount;
        
        for (int i = Mathf.CeilToInt(startFillAmount * sliderMaxParts); i <= sliderMaxParts; i++)
        {
            float nextFillAmount = i / (float)sliderMaxParts;
            
            sliderImage.DOFillAmount(nextFillAmount, fillDuration).SetEase(sliderEase);
            OnSliderValueChanged?.Invoke(this,new SliderValueChangedEventArgs{value = i});
            yield return new WaitForSeconds(fillDuration + waitTimeBetweenParts);
        }
    }



    public void SubtractAndRefill()
    {
        float newFillAmount = Mathf.Clamp(sliderImage.fillAmount - (subtractValue / (float)sliderMaxParts), 0f, 1f);
        sliderImage.fillAmount = newFillAmount;
        StartFillAnimation(newFillAmount);
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
            script.SubtractAndRefill();
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
