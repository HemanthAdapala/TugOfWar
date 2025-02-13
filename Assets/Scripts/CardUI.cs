using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
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
        cardImage.sprite = data.cardArtwork;
        cardImageOutline.sprite = data.cardArtwork;
        cardData.cardValue = data.cardValue;
    }
    
}
