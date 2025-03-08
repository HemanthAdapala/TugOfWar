using System;
using UnityEngine;

[RequireComponent(typeof(AvatarAnimator))]
[RequireComponent(typeof(AvatarAudio))]
[RequireComponent(typeof(AvatarMovement))]
public class Avatar : MonoBehaviour
{
    [SerializeField] private AvatarStateMachine avatarStateMachine;
    [SerializeField] public AvatarAnimator avatarAnimator;
    [SerializeField] public AvatarAudio avatarAudio;
    [SerializeField] public AvatarMovement avatarMovement;




    private AvatarState avatarState;
    private Material avatarMaterial;
    private CardData cardData;
    private SpawnerSide currentSpawnSide;

    public AvatarState AvatarState
    {
        get => avatarState;
        set => avatarState = value;
    }

    public SpawnerSide CurrentSpawnSide
    {
        get => currentSpawnSide;
        set => currentSpawnSide = value;
    }

    public CardData GetCardDataOfAvatar() => cardData;

    private void Awake()
    {
        InitializeAvatarComponents();
        InitializeStateMachine();
    }

    private void InitializeAvatarComponents()
    {
        avatarAnimator = GetComponent<AvatarAnimator>();
        avatarAudio = GetComponent<AvatarAudio>();
        avatarMovement = GetComponent<AvatarMovement>();
    }

    private void InitializeStateMachine()
    {
        if (avatarStateMachine == null)
        {
            avatarStateMachine = new AvatarStateMachine(this);
            avatarStateMachine.Initialize(avatarStateMachine.avatarIdleState);
        }
    }

    private void FixedUpdate()
    {



        avatarStateMachine.FixedUpdate();
        avatarMovement.FixedUpdateMovement();
    }

    public void SetAvatarData(CardData cardData, SpawnerSide currentSpawnSide)
    {
        this.cardData = cardData;
        this.currentSpawnSide = currentSpawnSide;

        avatarMaterial = GetComponent<Renderer>().material;
        avatarMaterial.color = cardData.cardColor;

        GameLobby.Instance.CalculateAverageStatsByPlayer(this.currentSpawnSide);
    }
}
