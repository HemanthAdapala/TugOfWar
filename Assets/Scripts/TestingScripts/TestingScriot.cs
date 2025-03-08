using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestingScriot : MonoBehaviour
{
    public Button plusButton;
    public Button minusButton;
    public Rigidbody[] testObject;

    public float speed = 1f;
    public ForceMode forceMode = ForceMode.Force;

    private float timer = 0f;
    public float interval = 1f; // Execute every 1 second

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer >= interval)
        {
            timer = 0f; // Reset timer
            MoveGameObject(); // Call the function every second
        }
    }

    private void MoveGameObject()
    {
        for (int i = 0; i < testObject.Length; i++)
        {
            Vector3 movement = new Vector3(0, 0, -1);
            testObject[i].AddForce(movement * speed, forceMode);
        }
    }
}
