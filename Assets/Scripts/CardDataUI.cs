using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // Add DOTween namespace

public class CardDataUI : MonoBehaviour
{
    //MAIN PAGE
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private Image cardImage;
    [SerializeField] private TextMeshProUGUI cardRole;
    [SerializeField] private TextMeshProUGUI strength;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI stamina;
    [SerializeField] private TextMeshProUGUI technique;
    [SerializeField] private TextMeshProUGUI weight;

    //FLIPPED PAGE
    [SerializeField] private TextMeshProUGUI pullPower;
    [SerializeField] private TextMeshProUGUI defense;
    [SerializeField] private TextMeshProUGUI abilityDescription;
    [SerializeField] private TextMeshProUGUI lore;
    [SerializeField] private TextMeshProUGUI uniqueAbility;


    [SerializeField] private Button button; 

    private bool _isAnimating = false;
    private Sequence _flipSequence;

    public void SetCardData(CardData cardData)
    {
        cardName.text = cardData.cardName;
        cardImage.sprite = cardData.cardArtwork;
        cardRole.text = cardData.role;
        strength.text = cardData.strength.ToString(CultureInfo.InvariantCulture);
        speed.text = cardData.speed.ToString(CultureInfo.InvariantCulture);
        stamina.text = cardData.stamina.ToString(CultureInfo.InvariantCulture);
        technique.text = cardData.technique.ToString(CultureInfo.InvariantCulture);
        weight.text = cardData.weight.ToString(CultureInfo.InvariantCulture);

        pullPower.text = cardData.pullPower.ToString(CultureInfo.InvariantCulture);
        defense.text = cardData.defense.ToString(CultureInfo.InvariantCulture);
        abilityDescription.text = cardData.abilityDescription;
        lore.text = cardData.lore;
        uniqueAbility.text = cardData.uniqueAbility.ToString();
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