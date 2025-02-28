using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private Vector2 velocity;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.linearVelocity = Vector2.up * speed;

        LaunchBallInRandomDirection();
    }

    private void LaunchBallInRandomDirection()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;

        rb.linearVelocity = new Vector2(speed * x, speed * y).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Calculate the reflected direction using reflection formula
        rb.linearVelocity = Vector2.Reflect(rb.linearVelocity, collision.contacts[0].normal);
    }



}
