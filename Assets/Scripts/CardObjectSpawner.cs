using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public enum SpawnerSide
{
    Left,
    Right
}

public class CardObjectSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public SpawnerSide spawnerSide;  // Set this to Left or Right in the Inspector

    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform targetPoint;  // The target for the first spawned object
    [SerializeField] private float spacing = 1.5f;       // Spacing between cards

    [Header("Card Prefab")]
    [SerializeField] private GameObject cardPrefab;

    // Each spawner manages its own list.
    private List<GameObject> movingObjects = new List<GameObject>();
    private bool isMoving = false;  // Ensures sequential movement

    /// <summary>
    /// Spawns a card and moves it into position.
    /// </summary>
    public void SpawnCardObject(CardData cardData)
    {
        if (cardData == null || cardPrefab == null)
        {
            Debug.LogError("CardData or prefab missing!");
            return;
        }

        // Instantiate the object at the spawn point
        GameObject player = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity, transform);
        movingObjects.Add(player);
        var playerData = player.GetComponent<CardPlayer>();
        playerData.SetPlayerData(cardData);

        // Only start moving if nothing is currently moving.
        if (!isMoving)
        {
            MoveObject(player);
        }
    }

    /// <summary>
    /// Moves the newly spawned object into position.
    /// For the first object it moves to targetPoint.
    /// For subsequent objects, it moves behind the previous object (with spacing).
    /// </summary>
    private void MoveObject(GameObject objToMove)
    {
        isMoving = true;

        // Calculate target position
        Vector3 targetPos;
        if (movingObjects.Count == 1)
        {
            // First object goes to the targetPoint
            targetPos = targetPoint.position;
        }
        else
        {
            // Subsequent objects target the previous object’s position with an offset.
            // Use the second-to-last object (index = Count - 2) since the last is the one we're moving.
            GameObject previousObj = movingObjects[movingObjects.Count - 2];
            Vector3 previousPos = previousObj.transform.position;

            // Depending on the side, the offset is applied in a different direction.
            // For example, Left spawner might decrease Z while Right spawner increases Z (or vice-versa, depending on your scene setup).
            if (spawnerSide == SpawnerSide.Left)
            {
                targetPos = previousPos - new Vector3(0, 0, spacing);
            }
            else  // SpawnerSide.Right
            {
                targetPos = previousPos + new Vector3(0, 0, spacing);
            }
        }

        // Optionally, if you want to separate movement into steps (e.g., move in X then in Z),
        // you can adjust the sequence below. Here we move in two steps (X first, then Z).
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.Append(objToMove.transform.DOMoveX(targetPos.x, 1f).SetEase(Ease.Linear))
                    .Append(objToMove.transform.DOMoveZ(targetPos.z, 1f).SetEase(Ease.Linear))
                    .OnComplete(() =>
                    {
                        // Once movement is complete, no further movement is needed until a new card is spawned.
                        isMoving = false;
                    });
    }
}
