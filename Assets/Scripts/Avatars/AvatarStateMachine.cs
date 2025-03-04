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



    public void Initialize(IAvatarState state)
    {
        currentState = state;
        currentState.EnterState();
    }

    public void TransitionToState(IAvatarState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }

    public void Update()
    {
        if (currentState == null) return;
        currentState.UpdateState();
    }
}

public interface IAvatarState
{
    void EnterState();
    void UpdateState();
    void ExitState();
}
