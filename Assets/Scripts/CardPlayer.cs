using TMPro;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;

    public void SetPlayerData(CardData cardData)
    {
        playerName.text = cardData.cardName;
    }
}
