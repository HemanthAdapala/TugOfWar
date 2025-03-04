using System;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField] private AvatarStateMachine avatarStateMachine;
    [SerializeField] private AvatarAnimator avatarAnimator;
    [SerializeField] private AvatarAudio avatarAudio;
    [SerializeField] private AvatarMovement avatarMovement;

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


    private void InitializeAvatarComponents()
    {
        avatarAnimator = this.gameObject.GetComponent<AvatarAnimator>();
        avatarAudio = this.gameObject.GetComponent<AvatarAudio>();
        avatarMovement = this.gameObject.GetComponent<AvatarMovement>();
    }

    void Start()
    {
        avatarStateMachine = new AvatarStateMachine(this);
        avatarStateMachine.Initialize(avatarStateMachine.avatarIdleState);
        if (avatarAnimator == null || avatarAudio == null || avatarMovement == null)
        {
            InitializeAvatarComponents();
        }
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
        //TODO:- This is just for testing
        //Need to check the conditions who is gonna pull based on average stats
        avatarStateMachine.TransitionToState(avatarStateMachine.avatarPullingState);
    }
}

