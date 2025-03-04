using UnityEngine;

public class AvatarPullingState : IAvatarState
{
    private Avatar avatar;

    public AvatarPullingState(Avatar avatar)
    {
        this.avatar = avatar;
    }

    public void EnterState(AvatarStateMachine avatarStateMachine)
    {
        DebugHelper.LogColor("AvatarPullingEnterState", Color.green);
    }

    public void ExitState(AvatarStateMachine avatarStateMachine)
    {
        DebugHelper.LogColor("AvatarPullingExitState", Color.green);
    }

    public void UpdateState(AvatarStateMachine avatarStateMachine)
    {
        DebugHelper.LogColor("AvatarPullingUpdateState", Color.green);
    }
}
