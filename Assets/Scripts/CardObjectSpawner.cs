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


    // Each spawner manages its own list.
    private List<GameObject> movingObjects = new List<GameObject>();
    private bool isMoving = false;  // Ensures sequential movement
    private Transform targetPoint;

    private void Start()
    {
        currentSpawnSide = SpawnerSide.None;
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
        GameObject avatarRef = Instantiate(avatarDataRef.avatarPrefab, spawnPoint.position, Quaternion.identity, transform);
        movingObjects.Add(avatarRef);
        var avatarData = avatarRef.GetComponent<Avatar>();
        avatarData.SetAvatarData(cardData);

        return avatarData;
    }

    /// <summary>
    /// Spawns a card and moves it into position.
    /// </summary>
    public void SpawnEntity(CardData cardData)
    {
        Debug.Log("Spawned Entity: " + cardData.cardName);
        currentSpawnSide = ChooseSpawnSide(); // Choose the side to spawn the avatar
        var avatar = SpawnAvatar(cardData);
        CalculateTargetPoint();
        GameLobby.Instance.AddPlayerSelectedCard(avatar, currentSpawnSide);

        var isLeader = CheckIfAvatarIsLeader(currentSpawnSide);
        if(isLeader)
        {
            MoveAvatarToInitialPoint(avatar,currentSpawnSide,0);
        }
        else
        {
            MoveAvatarToTargetPoint(avatar,spacing);
        }
    }

    private bool CheckIfAvatarIsLeader(SpawnerSide currentSpawnSide)
    {
        if(GameLobby.Instance.GetPlayerSelectedCardsBySide(currentSpawnSide).Count == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MoveAvatarToInitialPoint(Avatar avatar, SpawnerSide currentSpawnSide,float spacing)
    {
        var initialTargetPoint = GetTargetPositionBySide(currentSpawnSide);
        MoveInSequence(avatar.gameObject, initialTargetPoint,spacing);
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
        if(currentSpawnSide == SpawnerSide.Right)
        {
            targetPoint = initialTargetRightPoint;
        }
        else if(currentSpawnSide == SpawnerSide.Left)
        {
            targetPoint = initialTargetLeftPoint;
        }
    }

    /// <summary>
    /// Moves the newly spawned object into position.
    /// For the first object it moves to targetPoint.
    /// For subsequent objects, it moves behind the previous object (with spacing).
    /// </summary>
    public void MoveInSequence(GameObject objToMove, Transform targetPos, float spacing)
    {
        var sequence = DOTween.Sequence();

        // Step 1: Move in X direction first
        sequence.Append(objToMove.transform.DOMoveX(targetPos.position.x, 1f).SetEase(Ease.Linear));

        // Step 2: After X movement completes, move in Z direction
        sequence.Append(objToMove.transform.DOMoveZ(targetPos.position.z - spacing, 1f).SetEase(Ease.Linear));

        sequence.Play();
    }


    
    

    public SpawnerSide ChooseSpawnSide() => currentSpawnSide switch {
        SpawnerSide.None => SpawnerSide.Right,
        SpawnerSide.Right => SpawnerSide.Left,
        SpawnerSide.Left => SpawnerSide.Right,
        _ => currentSpawnSide
    };

    private Transform GetTargetPositionBySide(SpawnerSide side)
    {
        if(side == SpawnerSide.Right)
        {
            return initialTargetRightPoint;
        }
        else if(side == SpawnerSide.Left)
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
