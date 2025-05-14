using System.Collections;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public float bounceForce = 5f;
    public float minYPosition = -3f;
    public float maxYPosition = 3f;
    public float horizontalForce = 2f;
    public float initialVelocityMin = 2f;
    public float initialVelocityMax = 5f;
    public bool isBlinkingRed = false;
    public AudioClip redFlashSound;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public int collisionThreshold;
    private int collisionCount = 0;
    private AudioSource audioSource;
    private Vector2 initialVelocity;
   

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 2f;
        rb.gravityScale = 0.5f;

        float randomY = Random.Range(minYPosition, maxYPosition);
        transform.position = new Vector2(transform.position.x, randomY);

        initialVelocity = new Vector2(Random.Range(-horizontalForce, horizontalForce), Random.Range(initialVelocityMin, initialVelocityMax));
        rb.velocity = initialVelocity;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();

        sr = GetComponent<SpriteRenderer>();
        collisionThreshold = Random.Range(5, 17);
    }

    private void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        if (transform.position.y > maxYPosition)
        {
            transform.position = new Vector2(transform.position.x, maxYPosition);
            rb.velocity = new Vector2(rb.velocity.x, -bounceForce);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    HandleTouch();
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance.isGameOver || !GetComponent<Collider2D>().enabled) return;

        if (collision.gameObject.layer == 8)
        {
            rb.velocity = new Vector2(Random.Range(-horizontalForce, horizontalForce), -bounceForce);
            Debug.Log("Va ");
        }
        else if (collision.gameObject.layer == 9)
        {
            rb.velocity = new Vector2(Random.Range(-horizontalForce, horizontalForce), bounceForce);
            Debug.Log("Va ");
        }
        else
        {
            BallBounce otherBall = collision.gameObject.GetComponent<BallBounce>();
            if (otherBall != null)
            {
                collisionCount++;
                if (!isBlinkingRed && collisionCount >= collisionThreshold)
                {
                    collisionCount = 0;
                    StartFlashing();
                }

                Vector2 normal = collision.contacts[0].normal;
                Vector2 reflectVelocity = Vector2.Reflect(rb.velocity, normal);
                rb.velocity = reflectVelocity.normalized * bounceForce;
            }
            else
            {
                Debug.Log("Va cham " + collision.gameObject.name);
            }
        }
    }

    private void HandleTouch()
    {
        if (GameManager.Instance.isGameOver)
        {
            Debug.Log("Game Over");
            return;
        }

        if (isBlinkingRed)
        {
         
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            GameManager.Instance.BlinkingBallClicked(gameObject);

            ResetBall();

            gameObject.SetActive(false);
        }
        else
        {
            GameManager.Instance.NormalBallClicked(gameObject);
            gameObject.SetActive(false);
        }
    }

    public void StartFlashing()
    {
        if (!isBlinkingRed)
        {
            isBlinkingRed = true;
            StartCoroutine(FlashRedForDuration());
        }
    }

    private IEnumerator FlashRedForDuration()
    {
        if (sr == null) yield break;

        if (redFlashSound != null && audioSource != null && audioSource.enabled)
        {
            audioSource.PlayOneShot(redFlashSound);
        }

        Color originalColor = sr.color;
        float elapsed = 0f;
        while (elapsed < 4.10f)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.25f);
            sr.color = originalColor;
            yield return new WaitForSeconds(0.25f);
            elapsed += 0.5f;
        }

        if (isBlinkingRed)
        {
            sr.color = Color.red;
            GameManager.Instance.GameOver();
        }

        isBlinkingRed = false;
    }

    public void ResetBall()
    {
        isBlinkingRed = false;
        collisionCount = 0;
        if (sr != null) sr.color = Color.white;
    }
}
