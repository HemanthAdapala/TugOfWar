using UnityEngine;

public class AvatarPullingState : IAvatarState
{
    private Avatar avatar;

    public AvatarPullingState(Avatar avatar)
    {
        this.avatar = avatar;
    }

    public void EnterState()
    {
        DebugHelper.LogColor("AvatarPullingEnterState", Color.green);
    }

    public void ExitState()
    {
        DebugHelper.LogColor("AvatarPullingExitState", Color.green);
    }

    public void UpdateState()
    {
        DebugHelper.LogColor("AvatarPullingUpdateState", Color.green);
    }
}
