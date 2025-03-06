using UnityEngine;

public class AvatarPulledState : IAvatarState
{
    private Avatar avatar;

    public AvatarPulledState(Avatar avatar)
    {
        this.avatar = avatar;
    }

    public void EnterState()
    {
        DebugHelper.LogColor("AvatarPulledEnterState", Color.green);
    }

    public void ExitState()
    {
        DebugHelper.LogColor("AvatarPulledExitState", Color.red);
    }

    public void UpdateState()
    {
        DebugHelper.LogColor("AvatarPulledUpdateState", Color.blue);
    }
}
