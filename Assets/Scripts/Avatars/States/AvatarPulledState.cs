using UnityEngine;

public class AvatarPulledState : IAvatarState
{
    private Avatar avatar;

    public AvatarPulledState(Avatar avatar)
    {
        this.avatar = avatar;
    }

    public void EnterState(AvatarStateMachine avatarStateMachine)
    {
        DebugHelper.LogColor("AvatarPulledEnterState", Color.green);
    }

    public void ExitState(AvatarStateMachine avatarStateMachine)
    {
        DebugHelper.LogColor("AvatarPulledExitState", Color.green);
    }

    public void UpdateState(AvatarStateMachine avatarStateMachine)
    {
        DebugHelper.LogColor("AvatarPulledUpdateState", Color.green);
    }
}
