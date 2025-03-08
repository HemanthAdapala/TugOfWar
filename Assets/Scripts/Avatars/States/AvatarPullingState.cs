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
        DebugHelper.LogColor("AvatarPullingExitState", Color.red);
    }

    public void FixedUpdateState()
    {
        DebugHelper.LogColor("AvatarPullingUpdateState", Color.blue);

    }


}
