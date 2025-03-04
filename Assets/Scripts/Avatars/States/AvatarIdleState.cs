using UnityEngine;

public class AvatarIdleState : IAvatarState
{
    private Avatar avatar;

    public AvatarIdleState(Avatar avatar)
    {
        this.avatar = avatar;
    }

    public void EnterState(AvatarStateMachine avatarStateMachine)
    {
        DebugHelper.LogColor("AvatarIdleState", Color.green);
    }

    public void ExitState(AvatarStateMachine avatarStateMachine)
    {
        DebugHelper.LogColor("AvatarExitState", Color.red);
    }

    public void UpdateState(AvatarStateMachine avatarStateMachine)
    {
        DebugHelper.LogColor("AvatarUpdateState", Color.blue);
    }
}
