using UnityEngine;

public class ElementDamage : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage to inflict on the player

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the element has collided with the ground or the player
        if (collision.gameObject.CompareTag("ground") || collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to the player if collided with the player
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.TakeDamage(damageAmount);
                }
            }

            // Destroy the element
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        // Check if the element's y position is less than -15
        if (transform.position.y < -15f)
        {
            // Destroy the element
            Destroy(gameObject);
        }
    }
}
