using System;
using UnityEngine;

public class AvatarMovement : MonoBehaviour
{
    [SerializeField] private float pullMultiplier = 0.05f;
    [SerializeField] private float minPullDistance = 0.1f;
    [SerializeField] private float maxPullDistance = 2f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float intervalTimer = 0.0f;

    private Vector3 movementDirection = Vector3.forward;
    private float distancePerSecond = 0.1f;
    private Vector3 moveStep;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        moveStep = movementDirection.normalized * distancePerSecond;
    }

    private float CalculatePullForce()
    {
        Avatar avatar = GetComponent<Avatar>(); // Get reference to Avatar
        if (avatar == null) return 0;

        var playerTeamAverage = GameLobby.Instance.GetPlayerTeamAverage(avatar.CurrentSpawnSide);
        var opponentTeamAverage = GameLobby.Instance.GetOpponentTeamAverage(avatar.CurrentSpawnSide);

        var averageDifference = playerTeamAverage - opponentTeamAverage;
        var rawPullValue = averageDifference * pullMultiplier;

        var sign = Mathf.Sign(rawPullValue);
        Debug.Log("Raw Pull Value: " + rawPullValue);
        var clampedMagnitude = Mathf.Clamp(Mathf.Abs(rawPullValue), minPullDistance, maxPullDistance);

        return sign * clampedMagnitude;
    }

    public void FixedUpdateMovement()
    {
        intervalTimer += Time.fixedDeltaTime;
        if (intervalTimer >= 3f)
        {
            intervalTimer = 0f;
            float pullForce = CalculatePullForce();
            Debug.Log("FixedUpdateMovement");

            // Ensure movement happens in the correct direction
            if (pullForce > 0)
            {
                moveStep = -movementDirection.normalized * distancePerSecond;
            }
            else
            {
                moveStep = movementDirection.normalized * distancePerSecond;
            }

            rb.AddForce(moveStep * pullForce * 10f, ForceMode.Impulse);

        }
    }
}
