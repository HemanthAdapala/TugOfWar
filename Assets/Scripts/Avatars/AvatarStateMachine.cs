using UnityEngine;

public class AvatarStateMachine : MonoBehaviour
{
    public AvatarStateMachine(Avatar avatar)
    {
        avatarIdleState = new AvatarIdleState(avatar);
        avatarPullingState = new AvatarPullingState(avatar);
        avatarPulledState = new AvatarPulledState(avatar);
    }


    public IAvatarState currentState { get; private set; }

    public AvatarIdleState avatarIdleState;
    public AvatarPullingState avatarPullingState;
    public AvatarPulledState avatarPulledState;

    [SerializeField] private AvatarAnimator avatarAnimator;
    [SerializeField] private AvatarAudio avatarAudio;
    [SerializeField] private AvatarMovement avatarMovement;

    private void InitializeAvatarComponents()
    {
        avatarAnimator = this.gameObject.GetComponent<AvatarAnimator>();
        avatarAudio = this.gameObject.GetComponent<AvatarAudio>();
        avatarMovement = this.gameObject.GetComponent<AvatarMovement>();
    }

    public void Initialize(IAvatarState state)
    {
        currentState = state;
        currentState.EnterState(this);
        if (avatarAnimator == null || avatarAudio == null || avatarMovement == null)
        {
            InitializeAvatarComponents();
        }
    }

    public void TransitionToState(IAvatarState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void Update()
    {
        currentState.UpdateState(this);
    }
}

public interface IAvatarState
{
    void EnterState(AvatarStateMachine avatarStateMachine);
    void UpdateState(AvatarStateMachine avatarStateMachine);
    void ExitState(AvatarStateMachine avatarStateMachine);
}
