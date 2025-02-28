using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LabRat : MonoBehaviour
{
    public List<int> duplicateArr = new List<int>();

    void Start()
    {
        int[] arr = { 1, 2, 3, 4, 5, 6, 3, 7, 8, 9, 1 };

        // Call the function to find duplicates
        int duplicateelements = FindDuplicates(arr);
    }


    public int FindDuplicates(int[] arr)
    {
        int element = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            element = arr[i];
            if (!duplicateArr.Contains(element))
            {
                duplicateArr.Add(arr[i]);
            }
            else
            {
                Debug.Log("Duplicate element found: " + element);
            }
        }

        return element;


    }
}





