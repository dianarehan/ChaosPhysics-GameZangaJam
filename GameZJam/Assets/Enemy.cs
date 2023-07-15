using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float chaseRange = 5f;
    public float attackRange = 2f;
    public Transform leftBoundary;
    public Transform rightBoundary;

    private Rigidbody2D rb;
    private bool isFacingRight = true;
    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    public float damageAmount = 10f;

    private bool isAttackCooldown = false;
    public float attackCooldownDuration = 2f;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the attack is on cooldown
        if (isAttackCooldown)
        {
            return;
        }

        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Check if the player is within the chase range
            if (distanceToPlayer <= chaseRange)
            {
                // Move towards the player
                Vector2 direction = (player.position - transform.position).normalized;

                // Flip the enemy if necessary
                if (direction.x > 0 && !isFacingRight)
                {
                    Flip();
                }
                else if (direction.x < 0 && isFacingRight)
                {
                    Flip();
                }

                rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

                // Check if the player is within attack range
                if (distanceToPlayer <= attackRange && !isAttacking)
                {
                    Attack();
                }
            }
            else
            {
                // Patrol between left and right boundaries
                if (isFacingRight)
                {
                    rb.velocity = new Vector2(patrolSpeed, rb.velocity.y);
                    if (transform.position.x >= rightBoundary.position.x)
                    {
                        Flip();
                    }
                }
                else
                {
                    rb.velocity = new Vector2(-patrolSpeed, rb.velocity.y);
                    if (transform.position.x <= leftBoundary.position.x)
                    {
                        Flip();
                    }
                }
            }
        }
    }

    private void Attack()
    {
        // Set IsAttacking parameter to trigger the attack animation
        animator.SetBool("IsAttacking", true);

        // Stop the enemy's movement during the attack animation
        rb.velocity = Vector2.zero;

        // Attack logic
        // Implement your own logic here for enemy attack behavior

        // Check if the player is within the attack range
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            // Find the child object with the arm collider
            Collider2D armCollider = GetComponentInChildren<Collider2D>();

            // Check if the arm collider has touched the player
            if (armCollider != null && armCollider.IsTouching(player.GetComponent<Collider2D>()))
            {
                // Deal damage to the player
                PlayerController playerController = player.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    playerController.TakeDamage(damageAmount);
                }
            }
        }

        // Start the attack cooldown
        StartCoroutine(AttackCooldownCoroutine());
    }

    private IEnumerator AttackCooldownCoroutine()
    {
        // Set the attack on cooldown
        isAttackCooldown = true;

        // Wait for the attack cooldown duration
        yield return new WaitForSeconds(attackCooldownDuration);

        // Reset the attack cooldown
        isAttackCooldown = false;

        // Reset IsAttacking parameter after the attack cooldown
        animator.SetBool("IsAttacking", false);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        healthSlider.value = currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Perform any necessary actions when the enemy is defeated
        Destroy(gameObject);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }
}
