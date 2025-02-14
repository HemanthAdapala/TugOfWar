using UnityEngine;

public class Avatar : MonoBehaviour
{
    private Material avatarMaterial;
    private CardData cardData;

    public void SetAvatarData(CardData cardData)
    {
        this.cardData = cardData;
        avatarMaterial = this.gameObject.GetComponent<Renderer>().material;
        avatarMaterial.color = cardData.cardColor;
    }

    public CardData GetCardDataOfAvatar()
    {
        return cardData;
    }
}
