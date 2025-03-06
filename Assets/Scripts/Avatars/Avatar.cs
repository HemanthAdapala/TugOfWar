using System;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField] private AvatarStateMachine avatarStateMachine;
    [SerializeField] public AvatarAnimator avatarAnimator;
    [SerializeField] public AvatarAudio avatarAudio;
    [SerializeField] public AvatarMovement avatarMovement;

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

    void InitializeStateMachine()
    {
        avatarStateMachine = new AvatarStateMachine(this);
        avatarStateMachine.Initialize(avatarStateMachine.avatarIdleState);
        if (avatarAnimator == null || avatarAudio == null || avatarMovement == null)
        {
            InitializeAvatarComponents();
        }
    }

    void FixedUpdate()
    {
        avatarStateMachine.Update();
    }

    public void SetAvatarData(CardData cardData, SpawnerSide currentSpawnSide)
    {
        this.cardData = cardData;
        avatarMaterial = this.gameObject.GetComponent<Renderer>().material;
        avatarMaterial.color = cardData.cardColor;
        this.currentSpawnSide = currentSpawnSide;
        GameLobby.Instance.CalculateAverageStatsByPlayer(this.currentSpawnSide);
        InitializeStateMachine();
        //Transition to pulling state
        avatarStateMachine.TransitionToState(avatarStateMachine.avatarPullingState);
    }

    public CardData GetCardDataOfAvatar()
    {
        return cardData;
    }
}

