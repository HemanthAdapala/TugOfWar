using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class CardObjectSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float spacing = 1.5f; // Space between cards

    [Header("Card Prefab")]
    [SerializeField] private GameObject cardPrefab; // Prefab with visuals/stats

    private List<GameObject> movingObjects = new List<GameObject>();
    private bool isMoving = false; // Ensures sequential movement

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

        // Start movement only for the new object
        if (!isMoving)
        {
            MoveObject(newObj);
        }
    }

    private void MoveObject(GameObject objToMove)
    {
        isMoving = true;

        // Determine target position
        Vector3 targetPos = (movingObjects.Count == 1) ? targetPoint.position : movingObjects[movingObjects.Count - 2].transform.position - new Vector3(0, 0, spacing);

        // Move in two steps (X first, then Z)
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.Append(objToMove.transform.DOMoveX(targetPos.x, 1f).SetEase(Ease.Linear)) // Move in X
                    .Append(objToMove.transform.DOMoveZ(targetPos.z, 1f).SetEase(Ease.Linear)) // Move in Z
                    .OnComplete(() =>
                    {
                        isMoving = false; // Allow new movement only when a new card is added
                    });
    }
}
