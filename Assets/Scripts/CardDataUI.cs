using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System; // Add DOTween namespace

public class CardDataUI : MonoBehaviour
{
    #region Fields
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


    public event Action<CardData> OnCardClicked;
    #endregion

    public void SetCardData(CardData cardData)
    {
        cardName.text = cardData.cardName;
        if(cardData.cardArtwork != null){
        cardImage.sprite = cardData.cardArtwork;
        }
        cardRole.text = cardData.role;
        strength.text = "Strength -" + cardData.strength.ToString(CultureInfo.InvariantCulture);
        speed.text = "Speed - " + cardData.speed.ToString(CultureInfo.InvariantCulture);
        stamina.text = "Stamina - " + cardData.stamina.ToString(CultureInfo.InvariantCulture);
        technique.text = "Technique - " + cardData.technique.ToString(CultureInfo.InvariantCulture);
        weight.text = "Weight - " + cardData.weight.ToString(CultureInfo.InvariantCulture);

        pullPower.text = "Pull Power - " + cardData.pullPower.ToString(CultureInfo.InvariantCulture);
        defense.text = "Defence - " + cardData.defense.ToString(CultureInfo.InvariantCulture);
        abilityDescription.text = cardData.abilityDescription;
        if(lore != null){
        lore.text = cardData.lore;
        }
        uniqueAbility.text = cardData.uniqueAbility.ToString();
        button.onClick.AddListener(() => 
        {
            OnClickButton(cardData);
        });
    }

    private void OnClickButton(CardData cardData)
    {
        Debug.Log("Button Clicked");
        OnCardClicked?.Invoke(cardData);
    }

    private void OnDestroy() {
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        button.onClick.RemoveAllListeners();
        OnCardClicked = null;
    }
}