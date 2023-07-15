/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class gravityabillity : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] int score;
    [SerializeField] float gravity;
    Rigidbody2D rb;

    [SerializeField] int scoreValue = 1;

    [SerializeField] Animator animator;
    [SerializeField] bool isWalking = false;
    public float Hp = 25f;
    public float gravAcc;
    private Color defaultColor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        points.text = score + " " + "/10";
        defaultColor = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        //gravAcc += 0.1f * gravity;
        rb.gravityScale = gravity;
        if (Input.GetKey(KeyCode.Q))
        {
            gravity = -1;
        }


        if (Input.GetKey(KeyCode.E))
        {
            gravity = 1;
        }
        
        animator.SetBool("IsWalking", isWalking);
    }
    public void TakeDamage(float damage)
    {
        Hp -= damage;
        if (Hp <= 0f)
        {
            Die();
        }
        else
        {
            StartCoroutine(ShowVulnerableEffect());
        }
    }

    private IEnumerator ShowVulnerableEffect()
    {
        // Change the sprite color to red
        GetComponent<SpriteRenderer>().color = Color.red;

        // Wait for a short duration
        yield return new WaitForSeconds(0.2f);

        // Change the sprite color back to the default color
        GetComponent<SpriteRenderer>().color = defaultColor;
    }





    void Die()
    {
        animator.SetBool("IsDead", true);
        StartCoroutine(DestroyAfterDelay(0.4f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collectable")
        {
            score++;
            points.text = score + " " + "/10";
            Destroy(collision.gameObject);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isWalking = true;
        }
        else if (collision.gameObject.CompareTag("ceiling"))
        {
            isWalking = true;
            FlipPlayer();
        }
    }

    private void FlipPlayer()
    {
        // Flip the player on the y-axis
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);

        // Play the walking animation
        animator.SetBool("IsWalking", isWalking);
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isWalking = false;
        }
        else
        {
            if (collision.gameObject.CompareTag("ceiling"))
            {
                FlipPlayerBack();
            }
        }
    }
   

    private void FlipPlayerBack()
    {
       
        transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);

        isWalking = false;
    }


}*/
/*
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gravityabillity : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] int score;
    [SerializeField] float gravity;
    Rigidbody2D rb;

    [SerializeField] int scoreValue = 1;

    [SerializeField] Animator animator;
    [SerializeField] bool isWalking = false;
    public float Hp = 25f;
    public float gravAcc;
    private Color defaultColor;

    public AudioClip collectibleSound; // Assign the collectible sound in the Inspector
    public AudioClip damageSound; // Assign the damage sound in the Inspector
    public AudioClip dieSound; // Assign the die sound in the Inspector

    private AudioSource canvasAudioSource; // Reference to the canvas's AudioSource component

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        points.text = score + " " + "/10";
        defaultColor = GetComponent<SpriteRenderer>().color;

        // Assign the canvas's AudioSource component
        canvasAudioSource = GameObject.Find("Canvas").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //gravAcc += 0.1f * gravity;
        rb.gravityScale = gravity;
        if (Input.GetKey(KeyCode.Q))
        {
            gravity = -1;
        }

        if (Input.GetKey(KeyCode.E))
        {
            gravity = 1;
        }

        animator.SetBool("IsWalking", isWalking);
    }

    public void TakeDamage(float damage)
    {
        Hp -= damage;
        if (Hp <= 0f)
        {
            Die();
        }
        else
        {
            StartCoroutine(ShowVulnerableEffect());
            canvasAudioSource.PlayOneShot(damageSound); // Play the damage sound using the canvas's AudioSource
        }
    }

    private IEnumerator ShowVulnerableEffect()
    {
        // Change the sprite color to red
        GetComponent<SpriteRenderer>().color = Color.red;

        // Wait for a short duration
        yield return new WaitForSeconds(0.2f);

        // Change the sprite color back to the default color
        GetComponent<SpriteRenderer>().color = defaultColor;
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        canvasAudioSource.PlayOneShot(dieSound); // Play the die sound using the canvas's AudioSource
        StartCoroutine(DestroyAfterDelay(0.4f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collectable")
        {
            score++;
            points.text = score + " " + "/10";
            Destroy(collision.gameObject);

            // Play the collectible sound using the canvas's AudioSource
            canvasAudioSource.PlayOneShot(collectibleSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isWalking = true;
        }
        else if (collision.gameObject.CompareTag("ceiling"))
        {
            isWalking = true;
            FlipPlayer();
        }
    }

    private void FlipPlayer()
    {
        // Flip the player on the y-axis
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);

        // Play the walking animation
        animator.SetBool("IsWalking", isWalking);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isWalking = false;
        }
        else
        {
            if (collision.gameObject.CompareTag("ceiling"))
            {
                FlipPlayerBack();
            }
        }
    }

    private void FlipPlayerBack()
    {
        transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
        isWalking = false;
    }
}
*/using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gravityabillity : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] int score;
    [SerializeField] float gravity;
    Rigidbody2D rb;

    [SerializeField] int scoreValue = 1;

    [SerializeField] Animator animator;
    [SerializeField] bool isWalking = false;
    public float Hp = 25f;
    public float gravAcc;
    private Color defaultColor;
    public Slider healthSlider;
    public AudioClip collectibleSound; // Assign the collectible sound in the Inspector
    public AudioClip damageSound; // Assign the damage sound in the Inspector
    public AudioClip dieSound; // Assign the die sound in the Inspector

    private AudioSource canvasAudioSource; // Reference to the canvas's AudioSource component

    public GameObject winPanel; // Reference to the win panel

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        points.text = score + " /10";
        defaultColor = GetComponent<SpriteRenderer>().color;

        // Assign the canvas's AudioSource component
        canvasAudioSource = GameObject.Find("Canvas").GetComponent<AudioSource>();

        // Set the initial color of the slider based on HP value
        UpdateSliderColor();
    }

    // Update is called once per frame
    void Update()
    {
        //gravAcc += 0.1f * gravity;
        rb.gravityScale = gravity;
        if (Input.GetKey(KeyCode.Q))
        {
            gravity = -1;
        }

        if (Input.GetKey(KeyCode.E))
        {
            gravity = 1;
        }

        animator.SetBool("IsWalking", isWalking);

        // Check if the player has collected 10 apples
        if (score >= 10)
        {
            WinGame();
        }
    }

    public void TakeDamage(float damage)
    {
        Hp -= damage;
        healthSlider.value = Hp;
        UpdateSliderColor();
        if (Hp <= 0f)
        {
            Die();
        }
        else
        {
            StartCoroutine(ShowVulnerableEffect());
            canvasAudioSource.PlayOneShot(damageSound); // Play the damage sound using the canvas's AudioSource
        }
    }

    private IEnumerator ShowVulnerableEffect()
    {
        // Change the sprite color to red
        GetComponent<SpriteRenderer>().color = Color.red;

        // Wait for a short duration
        yield return new WaitForSeconds(0.2f);

        // Change the sprite color back to the default color
        GetComponent<SpriteRenderer>().color = defaultColor;
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        canvasAudioSource.PlayOneShot(dieSound); // Play the die sound using the canvas's AudioSource

        StartCoroutine(DestroyAfterDelay(0.4f));
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        // Call the LoseGame method from the GameManager script
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.LoseGame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collectable")
        {
            score++;
            points.text = score + " /10";
            Destroy(collision.gameObject);

            // Play the collectible sound using the canvas's AudioSource
            canvasAudioSource.PlayOneShot(collectibleSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isWalking = true;
        }
        else if (collision.gameObject.CompareTag("ceiling"))
        {
            isWalking = true;
            FlipPlayer();
        }
    }

    private void FlipPlayer()
    {
        // Flip the player on the y-axis
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);

        // Play the walking animation
        animator.SetBool("IsWalking", isWalking);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isWalking = false;
        }
        else
        {
            if (collision.gameObject.CompareTag("ceiling"))
            {
                FlipPlayerBack();
            }
        }
    }

    private void FlipPlayerBack()
    {
        transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
        isWalking = false;
    }

    void WinGame()
    {
        // Activate the win panel
        winPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game (optional)

        // Call the WinGame method from the GameManager script
        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.WinGame();
    }

    /*void UpdateSliderColor()
    {
        // Update the color of the slider based on HP value
        if (Hp <= 5f)
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.red;
        }
        else if (Hp <= 10f)
        {
            healthSlider.fillRect.GetComponent<Image>().color = new Color(1f, 0.65f, 0f); // Orange color (255, 165, 0)
        }
        else
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.green;
        }
    }*/
    void UpdateSliderColor()
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
        float normalizedHp = Mathf.Clamp01(Hp / 40f); // Normalize HP between 0 and 40
        Color interpolatedColor = gradient.Evaluate(normalizedHp);

        // Set the color of the slider fill based on the interpolated color
        healthSlider.fillRect.GetComponent<Image>().color = interpolatedColor;
    }

}
