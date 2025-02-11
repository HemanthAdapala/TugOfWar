using UnityEngine;

public class CardHolderUI : MonoBehaviour
{
    [HideInInspector] public Transform cardHolder => transform;
    private CardData cardData;



    public CardData GetHolderCardData()
    {
        return cardData;
    }

    public void SetCardData(CardData data)
    {
        cardData = data;
    }
}
