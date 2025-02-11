using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    private Color cardColor;
    private CardData cardData;

    public void SetData(CardData data){
        cardData = data;
        Image image = GetComponent<Image>();
        image.color = data.cardColor;
        cardColor = data.cardColor;
        cardData.cardValue = data.cardValue;
    }
    
}
