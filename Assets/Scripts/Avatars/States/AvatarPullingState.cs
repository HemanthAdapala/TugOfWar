using UnityEngine;

public class AvatarPullingState : IAvatarState
{
    private Avatar avatar;

    private float pullMultiplier = 0.05f;
    private float minPullDistance = 0.1f;
    private float maxPullDistance = 2.0f;

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

    public void UpdateState()
    {
        DebugHelper.LogColor("AvatarPullingUpdateState", Color.blue);
        // Pulling state logic
        var playerTeamAverage = GameLobby.Instance.GetPlayerTeamAverage(avatar.CurrentSpawnSide);
        var opponentTeamAverage = GameLobby.Instance.GetOpponentTeamAverage(avatar.CurrentSpawnSide);

        if (playerTeamAverage > opponentTeamAverage)
        {
            avatar.avatarMovement.StartPullingAnimation();
        }
        else
        {
            return;
        }
    }
}
