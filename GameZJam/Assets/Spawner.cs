using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject elementPrefab; // The prefab of the element to spawn
    public int spawnCount = 5; // Number of elements to spawn
    public float spawnInterval = 10f; // Time interval between spawns
    public float spawnAreaWidth = 10f; // Width of the area where elements will be spawned
    public float fallSpeed = 5f; // Speed at which the elements fall
    public float damageAmount = 10f; // Amount of damage the elements inflict on the player

    private void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnElements());
    }

    private IEnumerator SpawnElements()
    {
        while (true)
        {
            // Spawn a group of elements
            for (int i = 0; i < spawnCount; i++)
            {
                // Randomly generate a position within the spawn area
                Vector2 spawnPosition = new Vector2(Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f), transform.position.y);

                // Spawn the element at the generated position
                GameObject element = Instantiate(elementPrefab, spawnPosition, Quaternion.identity);

                // Rotate the element 90 degrees about the z-axis
                element.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

                // Apply a downward force to make the element fall
                Rigidbody2D rb = element.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(Vector2.down * fallSpeed, ForceMode2D.Impulse);
                }
            }

            // Wait for the specified spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the spawned element has collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Deal damage to the player
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damageAmount);
            }

            // Destroy the spawned element
            Destroy(collision.gameObject);
        }
    }
}
