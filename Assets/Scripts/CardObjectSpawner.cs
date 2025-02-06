using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System;

public class CardObjectSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float spacing = 1.5f; // Space between cards

    [Header("Card Prefab")]
    [SerializeField] private GameObject cardPrefab; // Prefab with visuals/stats

    private List<GameObject> movingObjects = new List<GameObject>();
    private bool isMoving = false; // Ensure sequential movement

    public void SpawnCardObject(CardData cardData)
    {
        if (cardData == null || cardPrefab == null)
        {
            Debug.LogError("CardData or prefab missing!");
            return;
        }

        // Instantiate the object at the spawn point
        GameObject newObj = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity, transform);

        // Add the object to the list
        movingObjects.Add(newObj);

        // If not already moving, start movement
        if (!isMoving)
        {
            MoveNextObject();
        }
    }

    private void MoveNextObject()
    {
        if (movingObjects.Count == 0)
        {
            isMoving = false;
            return;
        }

        isMoving = true;

        // Get the next object to move
        GameObject objToMove = movingObjects[0];

        // Calculate the correct target position
        Vector3 finalTargetPosition = CalculateTargetPosition();

        // Calculate intermediate position (X movement first)
        Vector3 intermediatePosition = new Vector3(finalTargetPosition.x, objToMove.transform.position.y, objToMove.transform.position.z);

        // Move in two steps (X first, then Z)
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.Append(objToMove.transform.DOMoveX(intermediatePosition.x, 1f).SetEase(Ease.Linear)) // Move in X
                    .Append(objToMove.transform.DOMoveZ(finalTargetPosition.z, 1f).SetEase(Ease.Linear)) // Move in Z with spacing
                    .OnComplete(() =>
                    {
                        Debug.Log("Object reached the adjusted target!");

                        // Remove from list after movement is completed
                        movingObjects.RemoveAt(0);

                        // Move the next object in queue
                        MoveNextObject();
                    });
    }

    private Vector3 CalculateTargetPosition()
    {
        if (targetPoint == null)
        {
            Debug.LogError("Target Transform is not assigned!");
            return Vector3.zero;
        }

        // First object goes directly to the target point
        if (movingObjects.Count == 1)
        {
            return targetPoint.position;
        }

        // New object moves behind the last placed object
        GameObject lastObject = movingObjects[movingObjects.Count - 2];
        return lastObject.transform.position - new Vector3(0, 0, spacing);
    }
}
