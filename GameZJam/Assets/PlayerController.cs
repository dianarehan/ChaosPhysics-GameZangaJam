/*using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public int maxHP = 50;

    private Rigidbody2D rb;
    private bool isGrounded;
    public int currentHP;
    private bool canAttack = true;
    private bool isFacingRight = true;
    private bool isJumping = false;
    private Animator animator;
    public Slider healthSlider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Movement input
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Set the Speed parameter for the animator
        animator.SetFloat("Speed", Mathf.Abs(moveX));

        // Flip the player sprite based on movement direction
        if (moveX > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveX < 0 && isFacingRight)
        {
            Flip();
        }

        // Jumping input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Attack input
        if (canAttack && Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
        animator.SetBool("IsJumping", true);
    }

    private void Attack()
    {
        // Instantiate a fireball projectile
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        // Set the fireball's direction based on player's facing direction
        if (transform.localScale.x < 0)
        {
            fireball.GetComponent<Fireball>().SetDirection(Vector2.left);
        }
        else
        {
            fireball.GetComponent<Fireball>().SetDirection(Vector2.right);
        }

        // Prevent immediate attack spamming
        canAttack = false;
        Invoke("ResetAttack", 0.5f);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHP -= (int)damageAmount;
        healthSlider.value = currentHP;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle player death (e.g., show game over screen)
        

        // Set the "IsDead" parameter to true in the animator
        animator.SetBool("IsDead", true);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            if (isJumping)
            {
                isJumping = false;
                animator.SetBool("IsJumping", false);
            }
        }
    }
}
*/
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public int maxHP = 50;

    private Rigidbody2D rb;
    private bool isGrounded;
    public int currentHP;
    private bool canAttack = true;
    private bool isFacingRight = true;
    private bool isJumping = false;
    private Animator animator;
    public Slider healthSlider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
        animator = GetComponent<Animator>();
        healthSlider.maxValue = maxHP;
        healthSlider.value = currentHP;
        UpdateSliderColor();
    }

    private void Update()
    {
        // Check if the player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Movement input
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // Set the Speed parameter for the animator
        animator.SetFloat("Speed", Mathf.Abs(moveX));

        // Flip the player sprite based on movement direction
        if (moveX > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveX < 0 && isFacingRight)
        {
            Flip();
        }

        // Jumping input
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // Attack input
        if (canAttack && Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
        animator.SetBool("IsJumping", true);
    }

    private void Attack()
    {
        // Instantiate a fireball projectile
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, Quaternion.identity);

        // Set the fireball's direction based on player's facing direction
        if (transform.localScale.x < 0)
        {
            fireball.GetComponent<Fireball>().SetDirection(Vector2.left);
        }
        else
        {
            fireball.GetComponent<Fireball>().SetDirection(Vector2.right);
        }

        // Prevent immediate attack spamming
        canAttack = false;
        Invoke("ResetAttack", 0.5f);
    }

    private void ResetAttack()
    {
        canAttack = true;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHP -= (int)damageAmount;
        healthSlider.value = currentHP;
        UpdateSliderColor();
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle player death (e.g., show game over screen)

        // Set the "IsDead" parameter to true in the animator
        animator.SetBool("IsDead", true);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            if (isJumping)
            {
                isJumping = false;
                animator.SetBool("IsJumping", false);
            }
        }
    }

    private void UpdateSliderColor()
    {
        // Define the color gradient
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKeys = new GradientColorKey[5];

        // Define the color keys for the gradient in the desired order
        colorKeys[0] = new GradientColorKey(Color.green, 0f);
        colorKeys[1] = new GradientColorKey(new Color(0.8f, 1f, 0f), 0.25f); // Light green
        colorKeys[2] = new GradientColorKey(new Color(1f, 0.85f, 0f), 0.5f); // Light orange
        colorKeys[3] = new GradientColorKey(new Color(1f, 0.65f, 0f), 0.75f); // Orange
        colorKeys[4] = new GradientColorKey(Color.red, 1f);

        // Define the alpha keys for the gradient (optional)
        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0] = new GradientAlphaKey(1f, 0f);
        alphaKeys[1] = new GradientAlphaKey(1f, 1f);

        // Assign the color and alpha keys to the gradient
        gradient.SetKeys(colorKeys, alphaKeys);

        // Get the interpolated color from the gradient based on HP value
        float normalizedHP = Mathf.Clamp01((float)currentHP / maxHP);
        Color interpolatedColor = gradient.Evaluate(normalizedHP);

        // Set the color of the slider fill based on the interpolated color
        healthSlider.fillRect.GetComponent<Image>().color = interpolatedColor;
    }


}
