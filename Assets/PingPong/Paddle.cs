using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 8f;
    public bool isLeftPaddle; // Toggle for left or right paddle
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    void Update()
    {
        float move = 0;

        if (isLeftPaddle)
        {
            move = Input.GetAxis("Vertical") * speed;
        }
        else
        {
            move = Input.GetAxis("Vertical2") * speed;
        }

        rb.linearVelocity = new Vector2(0, move);
    }
}
