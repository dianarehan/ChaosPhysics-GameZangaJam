/*using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;

    private Rigidbody2D rb;
    private Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Move the fireball in the specified direction
        rb.velocity = direction * speed;
    }

    public void SetDirection(Vector2 dir)
    {
        // Set the direction of the fireball
        direction = dir.normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the fireball collides with an enemy or damaging object
        if (other.tag=="Enemy")
        {
            // Deal damage to the enemy
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);

            }
        }
        

        // Destroy the fireball if x > 15
        if (transform.position.x > 15f || transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }

}*/
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float range = 4f;
    public float lifetime = 3f;

    private Rigidbody2D rb;
    private Vector2 direction;
    private float initialPositionX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPositionX = transform.position.x;
    }

    private void FixedUpdate()
    {
        // Move the fireball in the specified direction
        rb.velocity = direction * speed;

        // Check if the fireball has reached its maximum range
        if (Mathf.Abs(transform.position.x - initialPositionX) >= range)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 dir)
    {
        // Set the direction of the fireball
        direction = dir.normalized;

        // Destroy the fireball after the specified lifetime
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the fireball collides with an enemy or damaging object
        if (other.tag == "Enemy")
        {
            // Deal damage to the enemy
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}


