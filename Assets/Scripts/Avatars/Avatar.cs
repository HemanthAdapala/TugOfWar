using System;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField] private AvatarStateMachine avatarStateMachine;

    private AvatarState avatarState;
    public AvatarState AvatarState
    {
        get
        {
            return avatarState;
        }
        set
        {
            avatarState = value;
        }
    }

    private Material avatarMaterial;
    private CardData cardData;

    private SpawnerSide currentSpawnSide;
    public SpawnerSide CurrentSpawnSide
    {
        get
        {
            return currentSpawnSide;
        }
        set
        {
            currentSpawnSide = value;
        }
    }

    void Start()
    {
        avatarStateMachine = new AvatarStateMachine(this);
        avatarStateMachine.Initialize(avatarStateMachine.avatarIdleState);
    }



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

    public void MovedToDestination(SpawnerSide currentSpawnSide)
    {
        Debug.Log("Avatar moved to destination!");
        this.currentSpawnSide = currentSpawnSide;
        Debug.Log("Current Spawn Side: " + this.currentSpawnSide);
        //Calculate the Average by player if there is only one player or by team if there are multiple players from GameLobby
        GameLobby.Instance.CalculateAverageStatsByPlayer(this.currentSpawnSide);
    }
}

