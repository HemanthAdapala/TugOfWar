using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class SelectedObjectInstantiator : MonoBehaviour
{
    public GameObject selectedObject;
    public Transform targetTransform; // Assign in Inspector
    public Transform initialTransform; // Assign in Inspector
    public float objectSpacing = 1.5f; // Space between objects
    private readonly List<GameObject> _movingObjects = new List<GameObject>(); // Store instantiated objects

    private void Start()
    {
        InitializeRandomObject();
    }

    async void InitializeRandomObject()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 initialPosition = initialTransform.position;
            GameObject newObject = Instantiate(selectedObject, initialPosition, Quaternion.identity);
            newObject.transform.SetParent(this.transform);

            MoveObjectToTarget(newObject);
            //Store in the list
            _movingObjects.Add(newObject);
            await Task.Delay(5000);
        }
    }

    private void MoveObjectToTarget(GameObject obj)
    {
        if (targetTransform == null)
        {
            Debug.LogError("Target Transform is not assigned!");
            return;
        }

        // Default target position (first object)
        Vector3 finalTargetPosition = targetTransform.position;

        // If there are previous objects, adjust the position to be behind the last one
        if (_movingObjects.Count > 0)
        {
            GameObject lastObject = _movingObjects[^1];
            finalTargetPosition = lastObject.transform.position - new Vector3(0, 0, objectSpacing);
        }

        // Calculate the intermediate position (X movement first)
        Vector3 intermediatePosition = new Vector3(finalTargetPosition.x, obj.transform.position.y, obj.transform.position.z);

        // Move in sequence
        Sequence moveSequence = DOTween.Sequence();
        moveSequence.Append(obj.transform.DOMoveX(intermediatePosition.x, 1f).SetEase(Ease.Linear)) // Move in X
                    .Append(obj.transform.DOMoveZ(finalTargetPosition.z, 1f).SetEase(Ease.Linear)) // Move in Z with spacing
                    .OnComplete(() => Debug.Log("Object reached the adjusted target!"));
    }
}
