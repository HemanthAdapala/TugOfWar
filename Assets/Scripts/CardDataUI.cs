using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // Add DOTween namespace

public class CardDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI stamina;
    [SerializeField] private TextMeshProUGUI weight;
    [SerializeField] private TextMeshProUGUI pullSpeed;
    [SerializeField] private Button button; 

    private bool _isAnimating = false;
    private Sequence _flipSequence;

    public void Initialize(CardData cardData)
    {
        // Debug.Log("CardDataUI initialized:- " + cardData.name);
        // cardNameText.text = cardData.name;
        // stamina.text = cardData.stamina.ToString(CultureInfo.InvariantCulture);
        // weight.text = cardData.weight.ToString(CultureInfo.InvariantCulture);
        // pullSpeed.text = cardData.pullSpeed.ToString(CultureInfo.InvariantCulture);
        // button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        if (_isAnimating) return; // Prevent multiple animations
        FlipCard();
    }

    private void FlipCard()
    {
        if (_isAnimating) return; // Prevent multiple animations
        _isAnimating = true;

        // Kill any existing animation
        _flipSequence?.Kill();

        // Create new sequence
        _flipSequence = DOTween.Sequence();

        // Flip to 180 degrees (back side), wait 1 second, then flip back to 0 degrees (front side)
        _flipSequence.Append(transform.DORotate(new Vector3(0, 180, 0), 0.5f).SetEase(Ease.InOutQuad)) // Smooth rotation to back
            .AppendInterval(1f) // Wait for 1 second
            .Append(transform.DORotate(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InOutQuad)) // Rotate back to front
            .OnComplete(() => _isAnimating = false); // Reset flag when animation completes
    }


    private void OnDestroy()
    {
        // Clean up DOTween
        _flipSequence?.Kill();
    }
}