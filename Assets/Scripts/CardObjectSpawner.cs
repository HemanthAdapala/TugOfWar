using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System;

public enum SpawnerSide
{
    Left,
    Right,
    None
}

public class CardObjectSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public SpawnerSide currentSpawnSide;  // Set this to Left or Right in the Inspector

    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform initialTargetRightPoint;  // The target for the first spawned object
    [SerializeField] private Transform initialTargetLeftPoint;
    [SerializeField] private float spacing;       // Spacing between cards

    private List<Avatar> playerSelectedRightCards;
    private List<Avatar> playerSelectedLeftCards;
    private bool isMoving = false;  // Ensures sequential movement
    private Transform targetPoint;

    private void Start()
    {
        currentSpawnSide = SpawnerSide.None;
        playerSelectedRightCards = new List<Avatar>();
        playerSelectedLeftCards = new List<Avatar>();
    }

    public void SpawnEntity(CardData cardData)
    {
        Debug.Log("Spawned Entity: " + cardData.cardName);
        currentSpawnSide = ChooseSpawnSide(); // Choose the side to spawn the avatar
        var avatar = SpawnAvatar(cardData);
        CalculateTargetPoint();
        GameLobby.Instance.AddPlayerSelectedCard(avatar, currentSpawnSide);

        var isLeader = CheckIfAvatarIsLeader(currentSpawnSide);
        if (isLeader)
        {
            MoveAvatarToInitialPoint(avatar, currentSpawnSide, 0);
            //Start pulling if player has move average than opponent team or pulled by opponent team
        }
        else
        {
            MoveAvatarToTargetPoint(avatar, spacing);
            //Start pulling if player has move average than opponent team or pulled by opponent team
        }
    }

    public Avatar SpawnAvatar(CardData cardData)
    {
        if (cardData == null)
        {
            Debug.LogError("CardData or prefab missing!");
            return null;
        }

        var avatarDataRef = cardData.avatar;
        // Instantiate the object at the spawn point
        GameObject avatarInstance = Instantiate(avatarDataRef.avatarPrefab, spawnPoint.position, Quaternion.identity, transform);
        var avatarData = avatarInstance.GetComponent<Avatar>();
        avatarData.SetAvatarData(cardData);
        avatarData.CurrentSpawnSide = currentSpawnSide;

        return avatarData;
    }

    /// <summary>
    /// Spawns a card and moves it into position.
    /// </summary>
    private bool CheckIfAvatarIsLeader(SpawnerSide currentSpawnSide)
    {
        if (GameLobby.Instance.GetPlayerSelectedCardsBySide(currentSpawnSide).Count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MoveAvatarToInitialPoint(Avatar avatar, SpawnerSide currentSpawnSide, float spacing)
    {
        var initialTargetPoint = GetTargetPositionBySide(currentSpawnSide);
        MoveInSequence(avatar.gameObject, initialTargetPoint, spacing);
    }

    private void MoveAvatarToTargetPoint(Avatar avatar, float spacing)
    {
        var lastObjectPosition = GameLobby.Instance.GetLastObjectPosition(currentSpawnSide);
        if (lastObjectPosition != null)
        {
            MoveInSequence(avatar.gameObject, lastObjectPosition, spacing);
        }
    }



    private void CalculateTargetPoint()
    {
        if (currentSpawnSide == SpawnerSide.Right)
        {
            targetPoint = initialTargetRightPoint;
        }
        else if (currentSpawnSide == SpawnerSide.Left)
        {
            targetPoint = initialTargetLeftPoint;
        }
    }

    /// <summary>
    /// Moves the newly spawned object into position.
    /// For the first object it moves to targetPoint.
    /// For subsequent objects, it moves behind the previous object (with spacing).
    /// </summary>
    public void MoveInSequence(GameObject avatar, Transform targetPos, float spacing)
    {
        var sequence = DOTween.Sequence();

        // Step 1: Move in X direction first
        sequence.Append(avatar.transform.DOMoveX(targetPos.position.x, 1f).SetEase(Ease.Linear));

        // Step 2: After X movement completes, move in Z direction
        sequence.Append(avatar.transform.DOMoveZ(targetPos.position.z - spacing, 1f).SetEase(Ease.Linear));

        sequence.OnComplete(() =>
        {
            Debug.Log("Sequence completed");
            //Trigger event Avatar movement is completed
            var avatarRef = avatar.GetComponent<Avatar>();
            var currentSpawnSide = avatarRef.CurrentSpawnSide;
            avatarRef.MovedToDestination(currentSpawnSide);
        });

        sequence.Play().onComplete += () =>
        {
            AddAvatarToSpawningSideList(avatar);
        };
    }

    private void AddAvatarToSpawningSideList(GameObject avatar)
    {
        SpawnerSide spawnerSide = avatar.GetComponent<Avatar>().CurrentSpawnSide;
        if (spawnerSide == SpawnerSide.Right)
        {
            playerSelectedRightCards.Add(avatar.GetComponent<Avatar>());
            Debug.Log("Player Selected Right Cards: " + playerSelectedRightCards.Count);
        }
        else if (spawnerSide == SpawnerSide.Left)
        {
            playerSelectedLeftCards.Add(avatar.GetComponent<Avatar>());
            Debug.Log("Player Selected Left Cards: " + playerSelectedLeftCards.Count);
        }
    }

    public SpawnerSide ChooseSpawnSide() => currentSpawnSide switch
    {
        SpawnerSide.None => SpawnerSide.Right,
        SpawnerSide.Right => SpawnerSide.Left,
        SpawnerSide.Left => SpawnerSide.Right,
        _ => currentSpawnSide
    };

    private Transform GetTargetPositionBySide(SpawnerSide side)
    {
        if (side == SpawnerSide.Right)
        {
            return initialTargetRightPoint;
        }
        else if (side == SpawnerSide.Left)
        {
            return initialTargetLeftPoint;
        }
        else
        {
            return null;
        }
    }

    public Transform MoveToInitialRightPoint()
    {
        return initialTargetRightPoint;
    }

    public Transform MoveToInitialLeftPoint()
    {
        return initialTargetLeftPoint;
    }


}
