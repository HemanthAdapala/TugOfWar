using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class CardObjectSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float spacing = 1.5f;

    [Header("Card Prefab")]
    [SerializeField] private GameObject cardPrefab; // Prefab with visuals/stats

    private List<GameObject> spawnedObjects = new List<GameObject>();

    public void SpawnCardObject(CardData cardData)
    {
        if (cardData == null || cardPrefab == null)
        {
            Debug.LogError("CardData or prefab missing!");
            return;
        }

        // Instantiate the object
        GameObject newObj = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity, transform);
        spawnedObjects.Add(newObj);

        // Customize based on CardData (e.g., set strength/speed visuals)
        CardData cardObject = newObj.GetComponent<CardData>();
        if (cardObject != null)
        {
            //cardObject.Initialize(cardData);
        }

        // Position the object behind the last spawned object
        Vector3 targetPosition = CalculateTargetPosition();
        AnimateObject(newObj, targetPosition);
    }

    private Vector3 CalculateTargetPosition()
    {
        if (spawnedObjects.Count == 0)
        {
            return targetPoint.position;
        }
        else
        {
            GameObject lastObject = spawnedObjects[spawnedObjects.Count - 1];
            return lastObject.transform.position - new Vector3(0, 0, spacing);
        }
    }

    private void AnimateObject(GameObject obj, Vector3 targetPosition)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(obj.transform.DOMoveX(targetPosition.x, 1f).SetEase(Ease.Linear))
                .Append(obj.transform.DOMoveZ(targetPosition.z, 1f).SetEase(Ease.Linear))
                .OnComplete(() => Debug.Log("Object reached target!"));
    }

    public void StartMovingObjects()
    {
        // Trigger gameplay logic (e.g., start tug-of-war)
        foreach (var obj in spawnedObjects)
        {
            // Add tug-of-war movement logic here
        }
    }
}