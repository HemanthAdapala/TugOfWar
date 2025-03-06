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

    private List<Avatar> playerSelectedRightAvatars;
    private List<Avatar> playerSelectedLeftAvatars;
    private bool isMoving = false;  // Ensures sequential movement
    private Transform targetPoint;

    private void Start()
    {
        currentSpawnSide = SpawnerSide.None;
        playerSelectedRightAvatars = new List<Avatar>();
        playerSelectedLeftAvatars = new List<Avatar>();
    }

    public void SpawnMysteryBox(CardData cardData)
    {
        if (cardData == null)
        {
            Debug.LogError("CardData or prefab missing!");
            return;
        }
        currentSpawnSide = ChooseSpawnSide();
        var mysteryBoxRef = InstantiateMysteryBox(cardData.avatar.mysteryBoxPrefab);
        var isLeader = CheckIfAvatarIsLeader(currentSpawnSide);
        if (isLeader)
        {
            Debug.Log("Leader Avatar");
            //Move the mystery box to the initial target point
            //Once the mystery box is moved to the target point, spawn the avatar
            MoveMysteryBoxToInitialPoint(mysteryBoxRef, currentSpawnSide, 0, cardData);
        }
        else
        {
            Debug.Log("Not Leader Avatar");
            //Move the mystery box to the target point
            //Once the mystery box is moved to the target point, spawn the avatar
            MoveMysteryBoxToTargetPoint(mysteryBoxRef, spacing, cardData);
        }
    }

    private GameObject InstantiateMysteryBox(GameObject mysteryBoxPrefab)
    {
        GameObject mysteryBoxInstance = Instantiate(mysteryBoxPrefab, spawnPoint.position, Quaternion.identity, transform);
        return mysteryBoxInstance;
    }

    private void MoveMysteryBoxToInitialPoint(GameObject box, SpawnerSide currentSpawnSide, float spacing, CardData cardData)
    {
        var initialTargetPoint = GetTargetPositionBySide(currentSpawnSide);
        MoveInSequence(box, initialTargetPoint, spacing, cardData);
    }


    private void MoveMysteryBoxToTargetPoint(GameObject box, float spacing, CardData cardData)
    {
        var lastObjectPosition = GameLobby.Instance.GetLastObjectPosition(currentSpawnSide);
        if (lastObjectPosition != null)
        {
            MoveInSequence(box, lastObjectPosition, spacing, cardData);
        }
    }


    public Avatar SpawnAvatar(CardData cardData, Vector3 position = default)
    {
        if (cardData == null)
        {
            Debug.LogError("CardData or prefab missing!");
            return null;
        }

        var avatarDataRef = cardData.avatar;
        // Instantiate the object at the spawn point
        GameObject avatarInstance = Instantiate(avatarDataRef.avatarPrefab, position, Quaternion.identity, transform);
        var avatarData = avatarInstance.GetComponent<Avatar>();
        GameLobby.Instance.AddPlayerSelectedCard(avatarData, currentSpawnSide);
        avatarData.SetAvatarData(cardData, currentSpawnSide);

        return avatarData;
    }

    /// <summary>
    /// Spawns a card and moves it into position.
    /// </summary>
    private bool CheckIfAvatarIsLeader(SpawnerSide currentSpawnSide)
    {
        if (GameLobby.Instance.GetPlayerSelectedCardsBySide(currentSpawnSide).Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Moves the newly spawned object into position.
    /// For the first object it moves to targetPoint.
    /// For subsequent objects, it moves behind the previous object (with spacing).
    /// </summary>
    public void MoveInSequence(GameObject instance, Transform targetPos, float spacing, CardData cardData)
    {
        var sequence = DOTween.Sequence();

        // Step 1: Move in X direction first
        sequence.Append(instance.transform.DOMoveX(targetPos.position.x, 1f).SetEase(Ease.Linear));

        // Step 2: After X movement completes, move in Z direction
        sequence.Append(instance.transform.DOMoveZ(targetPos.position.z - spacing, 1f).SetEase(Ease.Linear));

        // OnComplete callback should be set before Play()
        sequence.OnComplete(() =>
        {
            Debug.Log("Sequence completed");
            var avatar = SpawnAvatar(cardData, instance.transform.position);

            Debug.Log("Clean Up");
            instance.transform.DOKill(); // Stops all DOTween animations on the instance
            Destroy(instance);

            AddAvatarToSpawningSideList(avatar.gameObject);
        });

        sequence.Play(); // Play should come after all setup
    }


    private void AddAvatarToSpawningSideList(GameObject avatar)
    {
        SpawnerSide spawnerSide = avatar.GetComponent<Avatar>().CurrentSpawnSide;
        if (spawnerSide == SpawnerSide.Right)
        {
            playerSelectedRightAvatars.Add(avatar.GetComponent<Avatar>());
            Debug.Log("Player Selected Right Cards: " + playerSelectedRightAvatars.Count);
        }
        else if (spawnerSide == SpawnerSide.Left)
        {
            playerSelectedLeftAvatars.Add(avatar.GetComponent<Avatar>());
            Debug.Log("Player Selected Left Cards: " + playerSelectedLeftAvatars.Count);
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


}
