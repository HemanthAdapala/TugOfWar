using UnityEngine;

[CreateAssetMenu(fileName = "NewPreset", menuName = "TugOfWar/Preset")]
public class Preset : ScriptableObject
{
    public string presetName;
    public string presetDescription;
    public DeckData[] decks;

    public void OValidate()
    {
        if (string.IsNullOrEmpty(presetName) || presetName != this.name)
        {
            presetName = this.name;
        }
    }
}

[CreateAssetMenu(fileName = "NewDeck", menuName = "TugOfWar/Deck")]
public class DeckData : ScriptableObject
{
    public string deckName;
    public string deckDescription;
    public CardData[] cards;

    public void OValidate()
    {
        if (string.IsNullOrEmpty(deckName) || deckName != this.name)
        {
            deckName = this.name;
        }
    }
}


[CreateAssetMenu(fileName = "NewCard", menuName = "TugOfWar/Card")]
public class CardData : ScriptableObject
{
    [Header("Visual Identity")]
    public string cardName; // Name of the card
    public Color cardColor; // Color of the card
    public int cardValue; // Value of the card
    public GameObject cardPrefab;
    public Sprite cardArtwork; // Visual of the character
    public string role; // E.g., "Anchor", "Puller", "Trickster"

    [Header("Avatar")]
    public AvatarData avatar;

    [Header("Core Stats")]
    [Range(1, 100)] public int strength;      // Raw pulling power
    [Range(1, 100)] public int speed;         // How fast they pull/react
    [Range(1, 100)] public int stamina;       // Endurance (reduces fatigue)
    [Range(1, 100)] public int technique;     // Efficiency of power usage
    [Range(1, 100)] public int weight;        // Resistance to being pulled

    [Header("Derived Attributes (Optional)")]
    [Tooltip("Calculated from strength + technique")]
    public float pullPower; 
    [Tooltip("Calculated from weight + stamina")]
    public float defense; 

    [Header("Player-Friendly Descriptions")]
    [TextArea(3, 5)] public string abilityDescription; // E.g., "Quick Burst: Sudden tugs!"
    [TextArea(3, 5)] public string lore; // Optional flavor text

    [Header("Abilities")]
    public Ability uniqueAbility;

    public enum Ability { None, QuickTug, StubbornAnchor, MomentumBuilder, FeintPull, EarthquakePull, GustDash }

    // Automatically set cardName to match the ScriptableObject's name
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(cardName) || cardName != this.name)
        {
            cardName = this.name;
        }
    }
}