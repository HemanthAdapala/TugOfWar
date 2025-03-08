using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CustomWeightingStats", menuName = "TugOfWar/CustomWeightingStats")]
public class CustomWeightingStats : ScriptableObject
{
    [Header("Weighting Factors")]
    [Tooltip("Weight factor for Strength")]
    public float strengthFactor = 0.3f;
    [Tooltip("Weight factor for Speed")]
    public float speedFactor = 0.1f;
    [Tooltip("Weight factor for Stamina")]
    public float staminaFactor = 0.2f;
    [Tooltip("Weight factor for Technique")]
    public float techniqueFactor = 0.25f;
    [Tooltip("Weight factor for Weight")]
    public float weightFactor = 0.15f;

    /// <summary>
    /// Calculates the Final Pull Strength for a single player using a weighted average.
    /// </summary>
    /// <param name="player">Player's CardData</param>
    /// <returns>Weighted average pull strength</returns>
    public float CalculateAverageStatsByPlayer(CardData player)
    {
        // Multiply each stat by its weighting factor
        float weightedStrength = player.strength * strengthFactor;
        float weightedSpeed = player.speed * speedFactor;
        float weightedStamina = player.stamina * staminaFactor;
        float weightedTechnique = player.technique * techniqueFactor;
        float weightedWeight = player.weight * weightFactor;

        // Sum up the weighted values
        float totalWeightedSum = weightedStrength + weightedSpeed + weightedStamina + weightedTechnique + weightedWeight;

        // Sum of all weighting factors
        float totalFactors = strengthFactor + speedFactor + staminaFactor + techniqueFactor + weightFactor;

        // Final pull strength is the weighted average
        return totalWeightedSum / totalFactors;
    }

    /// <summary>
    /// Calculates the team average Final Pull Strength by averaging the players' Final Pull Strength.
    /// </summary>
    /// <param name="selectedTeamCards">List of Avatars on the team</param>
    /// <returns>Team average pull strength</returns>
    public float CalculateAverageStatsByTeam(List<Avatar> selectedTeamCards)
    {
        var teamPlayerStrengths = new List<float>();

        // Get each player's final pull strength using the player's CardData
        foreach (var avatar in selectedTeamCards)
        {
            CardData dataRef = avatar.GetCardDataOfAvatar();
            float playerAverage = CalculateAverageStatsByPlayer(dataRef);
            teamPlayerStrengths.Add(playerAverage);
        }

        return teamPlayerStrengths.Count > 0 ? CalculateAverage(teamPlayerStrengths) : 0.0f;
    }

    /// <summary>
    /// Helper method to compute the average of a list of floats.
    /// </summary>
    private float CalculateAverage(List<float> stats)
    {
        float sum = 0.0f;
        foreach (float stat in stats)
        {
            sum += stat;
        }
        return stats.Count > 0 ? sum / stats.Count : 0.0f;
    }

    public float CalculateTeamComparison(SpawnerSide side)
    {
        float playerAverage = CalculateAverageStatsByTeam(
            side == SpawnerSide.Right ?
            GameLobby.Instance.playerSelectedRightCards :
            GameLobby.Instance.playerSelectedLeftCards
        );

        float opponentAverage = CalculateAverageStatsByTeam(
            side == SpawnerSide.Right ?
            GameLobby.Instance.opponentSelectedRightCards :
            GameLobby.Instance.opponentSelectedLeftCards
        );

        return playerAverage - opponentAverage;
    }
}
