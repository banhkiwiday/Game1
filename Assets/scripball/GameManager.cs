using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject[] balls;
    public GameObject bossBallPrefab;
    public int initialBallsToSpawn = 3;
    public float initialSpawnInterval = 5f;
    public float spawnIntervalDecrease = 0.2f;
    public float initialVelocityMax = 5f;
    public float velocityIncrease = 0.3f;
    public int collisionThresholdIncrease = 1;
    public int bossLevelInterval = 10;

    private List<GameObject> activeBalls = new List<GameObject>();
    private float spawnTimer;
    public bool isGameOver = false;
    public int score = 0;
    public int highScore = 0;
    public UnityEngine.UI.Text scoreText;
    public GameObject gameOverPanel;
    public GameOverFlashEffect gameOverFlashEffect;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (GameObject ball in balls)
        {
            ball.SetActive(false);
        }
        SpawnBalls(initialBallsToSpawn);
        spawnTimer = initialSpawnInterval;
        UpdateScoreText();
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (isGameOver) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            SpawnBalls(Random.Range(1, 3));
            initialSpawnInterval = Mathf.Max(1f, initialSpawnInterval - spawnIntervalDecrease);
            spawnTimer = initialSpawnInterval;

            if (score > 0 && score % bossLevelInterval == 0)
            {
                SpawnBossBall();
            }
        }
    }

    public void NormalBallClicked(GameObject clickedBall)
    {
        if (isGameOver) return;

        activeBalls.Remove(clickedBall);
        clickedBall.SetActive(false);
        SpawnBalls(Random.Range(1, 3));
    }

    public void BlinkingBallClicked(GameObject clickedBall)
    {
        if (isGameOver) return;

        activeBalls.Remove(clickedBall);
        clickedBall.SetActive(false);
        score++;
        UpdateScoreText();
    }

    private void SpawnBalls(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject ballToSpawn = GetRandomBall();
            if (ballToSpawn != null)
            {
                ballToSpawn.SetActive(true);
                activeBalls.Add(ballToSpawn);

                float randomY = Random.Range(-3f, 3f);
                ballToSpawn.transform.position = new Vector2(ballToSpawn.transform.position.x, randomY);

                Rigidbody2D rb = ballToSpawn.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(2f, initialVelocityMax));
                }

                SpriteRenderer sr = ballToSpawn.GetComponent<SpriteRenderer>();
                if (sr != null) sr.color = Color.white;

                BallBounce ballBounce = ballToSpawn.GetComponent<BallBounce>();
                if (ballBounce != null)
                {
                    ballBounce.collisionThreshold += collisionThresholdIncrease;
                }
            }
        }
        initialVelocityMax += velocityIncrease;
    }

    private void SpawnBossBall()
    {
        if (bossBallPrefab != null)
        {
            GameObject bossBall = Instantiate(bossBallPrefab);
            bossBall.SetActive(true);
            activeBalls.Add(bossBall);

            Rigidbody2D rb = bossBall.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.velocity = new Vector2(Random.Range(-3f, 3f), Random.Range(3f, 6f));
            }
        }
    }

    private GameObject GetRandomBall()
    {
        if (balls.Length > 0)
        {
            int randomIndex = Random.Range(0, balls.Length);
            return balls[randomIndex];
        }
        return null;
    }

    private void UpdateScoreText()
    {
        if (scoreText != null) scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        Debug.Log("Game Over!");
        isGameOver = true;

        foreach (GameObject ball in activeBalls)
        {
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;
            }
        }

        if (gameOverFlashEffect != null)
        {
            gameOverFlashEffect.TriggerGameOverFlash();
        }
        else
        {
            Debug.LogError("GameOverFlashEffect is not assigned!");
        }

        Handheld.Vibrate();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        PlayfabManager.Instance.SubmitHighScore(score);
        bxh.Instance.panel.gameObject.SetActive(true);
    }

    public void ResetGame()
    {
        isGameOver = false;
        score = 0;
        UpdateScoreText();

        foreach (GameObject ball in activeBalls)
        {
            ball.SetActive(false);
            ball.transform.position = new Vector2(ball.transform.position.x, 0);
        }
        activeBalls.Clear();

        initialSpawnInterval = 5f;
        initialVelocityMax = 5f;

        SpawnBalls(initialBallsToSpawn);
        gameOverPanel.SetActive(false);

    }
}
