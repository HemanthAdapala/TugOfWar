using TMPro;
using UnityEngine;

public class CardPlayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;

    private CardData cardData;

    private Material playerMaterial;

    void Start()
    {
        playerMaterial = GetComponent<Renderer>().material;
        playerMaterial.color = cardData.cardColor;
    }

    public void SetPlayerData(CardData cardData)
    {
        playerName.text = cardData.cardName;
    }
}
