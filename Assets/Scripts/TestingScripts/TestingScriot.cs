using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class TestingScriot : MonoBehaviour
{
    private float moveUpTime = 5f;
    private float stayUpTime = 5f;
    private float moveDownTime = 5f;
    private float stayDownTime = 5f;
    
    private bool isMoving = false;
    
    private Vector3 initialPosition;
    private Vector3 startPos;
    private Vector3 targetPos;
    bool isMoved = false;
    

    private void Start()
    {
        initialPosition = transform.position;
        startPos = transform.position;
        targetPos = Vector3.up * 10f;
    }

    private void Update()
    {
        var holdTimer = 0f;
        transform.position = Vector3.MoveTowards(transform.position,targetPos,Time.deltaTime * moveUpTime);
        startPos = transform.position;

        if (startPos == targetPos)
        {
            isMoved = true;
            while (isMoved)
            {
                holdTimer += Time.deltaTime;
                Debug.Log(holdTimer);
                if (holdTimer >= stayUpTime)
                {
                    transform.position = Vector3.MoveTowards(transform.position,startPos,Time.deltaTime * moveDownTime);
                    isMoved = false;
                }
            }
            
            
            
        }
        
    }
}
