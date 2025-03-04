using UnityEngine;

public class AvatarIdleState : IAvatarState
{
    private Avatar avatar;

    public AvatarIdleState(Avatar avatar)
    {
        this.avatar = avatar;
    }

    public void EnterState()
    {
        DebugHelper.LogColor("AvatarIdleEnterState", Color.green);
    }

    public void ExitState()
    {
        DebugHelper.LogColor("AvatarIdleExitState", Color.red);
    }

    public void UpdateState()
    {
        DebugHelper.LogColor("AvatarIdleUpdateState", Color.blue);
    }
}
