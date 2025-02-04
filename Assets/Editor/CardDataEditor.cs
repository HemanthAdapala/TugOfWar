using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardData))]
public class CardDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Draw the default inspector
        DrawDefaultInspector();

        // Get the target CardData object
        CardData cardData = (CardData)target;

        if (cardData.cardName != cardData.name)
        {
            cardData.cardName = cardData.name;
            EditorUtility.SetDirty(cardData); // Mark the object as dirty to save changes
        }

        // Add a button to randomize stats
        if (GUILayout.Button("Randomize Stats"))
        {
            RandomizeStats(cardData);
        }
    }

    private void RandomizeStats(CardData cardData)
    {
        // Randomize core stats
        cardData.strength = Random.Range(1, 101);
        cardData.speed = Random.Range(1, 101);
        cardData.stamina = Random.Range(1, 101);
        cardData.technique = Random.Range(1, 101);
        cardData.weight = Random.Range(1, 101);

        // Randomize derived stats (optional)
        cardData.pullPower = cardData.strength + cardData.technique;
        cardData.defense = cardData.weight + cardData.stamina;

        // Randomize ability (optional)
        cardData.uniqueAbility = (CardData.Ability)Random.Range(0, System.Enum.GetValues(typeof(CardData.Ability)).Length);

        Debug.Log($"Randomized stats for {cardData.cardName}!");
    }
}