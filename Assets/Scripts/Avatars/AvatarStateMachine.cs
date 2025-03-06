using UnityEngine;

public class AvatarStateMachine
{
    public AvatarStateMachine(Avatar avatar)
    {
        avatarIdleState = new AvatarIdleState(avatar);
        avatarPullingState = new AvatarPullingState(avatar);
        avatarPulledState = new AvatarPulledState(avatar);
    }


    public IAvatarState CurrentState { get; private set; }

    public AvatarIdleState avatarIdleState;
    public AvatarPullingState avatarPullingState;
    public AvatarPulledState avatarPulledState;



    public void Initialize(IAvatarState state)
    {
        CurrentState = state;
        state.EnterState();
    }

    public void TransitionToState(IAvatarState state)
    {
        CurrentState.ExitState();
        CurrentState = state;
        state.EnterState();
    }

    public void Update()
    {
        if (CurrentState == null) return;
        CurrentState.UpdateState();
    }
}

public interface IAvatarState
{
    void EnterState();
    void UpdateState();
    void ExitState();
}
