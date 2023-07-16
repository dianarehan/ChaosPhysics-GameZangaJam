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
    private float damageAmount = 10f;

    private bool isAttackCooldown = false;
    public float attackCooldownDuration = 2f;
    private bool canDamagePlayer = true;

    Collider2D armCollider; 
    ContactFilter2D playerFilter;
    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();

        
        armCollider = transform.GetChild(0).GetComponent<Collider2D>();
        playerFilter = new ContactFilter2D();
        playerFilter.SetLayerMask(LayerMask.GetMask("Player"));
        playerFilter.useLayerMask = true;

        // Set the arm collider to be a trigger
        if (armCollider != null)
        {
            armCollider.isTrigger = true;
        }
    }
    //this is working
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

            // Check if the enemy is attacking
            if (!isAttacking)
            {
                rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
            }
            else
            {
                // Stop the enemy's movement during the attack animation
                rb.velocity = Vector2.zero;
            }

            // Check if the player is within attack range
            if (distanceToPlayer <= attackRange && !isAttacking)
            {
                AttackStart();
                animator.SetTrigger("IsAttacking");
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

    public void AttackStart()
    {
        isAttacking = true;
    }
    public void AttackEnd()
    {
        isAttacking = false;
        armCollider.enabled = false;
        rb.velocity = Vector2.zero;
    }
    //this is working
    private void Attack()
    {   armCollider.enabled = true;
        Collider2D[] playerToDmg = new Collider2D[2];
        Physics2D.OverlapCollider(armCollider, playerFilter, playerToDmg);
        foreach(Collider2D col in playerToDmg)
        {
            if (col != null)
            {
                col.GetComponent<PlayerController>().TakeDamage(damageAmount);
            }
        }
        animator.SetTrigger("IsAttacking");

        // Stop the enemy's movement during the attack animation
        rb.velocity = Vector2.zero;
        Invoke("AttackEnd", 1f);
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the enemy's arm collider has touched the player's collider
        if (collision.CompareTag("Player"))
        {
            // Deal damage to the player
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damageAmount);
            }
        }
    }





    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        // Update the health slider value
        healthSlider.value = currentHealth;

        // Update the slider's fill color based on the health gradient
        UpdateSliderColor();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void UpdateSliderColor()
    {
        // Define the color gradient
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[6];

        // Define the color keys for the gradient
        colorKeys[0] = new GradientColorKey(Color.red, 0f);            // HP <= 5 - Red
        colorKeys[1] = new GradientColorKey(new Color(1f, 0.65f, 0f), 0.25f);  // HP <= 10 - Orange
        colorKeys[2] = new GradientColorKey(new Color(1f, 0.85f, 0f), 0.4f);   // HP <= 20
        colorKeys[3] = new GradientColorKey(new Color(1f, 1f, 0f), 0.55f);     // HP <= 30
        colorKeys[4] = new GradientColorKey(new Color(0.8f, 1f, 0f), 0.7f);     // HP <= 40
        colorKeys[5] = new GradientColorKey(Color.green, 1f);          // HP > 40 - Green

        // Define the alpha keys for the gradient (optional)
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1f, 0f);
        alphaKeys[1] = new GradientAlphaKey(1f, 1f);

        // Assign the color and alpha keys to the gradient
        gradient.SetKeys(colorKeys, alphaKeys);

        // Get the interpolated color from the gradient based on HP value
        float normalizedHp = Mathf.Clamp01((float)currentHealth / maxHealth); // Normalize HP between 0 and 1
        Color interpolatedColor = gradient.Evaluate(normalizedHp);

        // Set the color of the slider fill based on the interpolated color
        healthSlider.fillRect.GetComponent<Image>().color = interpolatedColor;
    }
    void Die()
        {
        animator.SetBool("IsDead", true);
            Destroy(gameObject,1.5f);
        }

        void Flip()
        {
            isFacingRight = !isFacingRight;
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }
    }