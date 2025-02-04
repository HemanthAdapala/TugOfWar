using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Material material;
    public float speed = 5f;


    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }
}
