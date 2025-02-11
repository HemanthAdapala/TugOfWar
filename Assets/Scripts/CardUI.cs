using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    private Color cardColor;
    private CardData cardData;
    [SerializeField] private Image cardImage;
    [SerializeField] private Image cardImageOutline;

    public int GetCardValue(){
        return cardData.cardValue;
    }

    public CardData GetCardData(){
        return cardData;
    }

    public Image GetCardImageOutline(){
        return cardImageOutline;
    }

    public Image GetCardImage(){
        return cardImage;
    }

    public void SetCardUIData(CardData data){
        cardData = data;
        cardImage.color = data.cardColor;
        cardColor = data.cardColor;
        cardData.cardValue = data.cardValue;
    }
    
}
