using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Sprite[] backgrounds;
    public Image backgroundImage;
    public SpriteRenderer backgroundSpriteRenderer;
    public Sprite defaultBackground;
    public Button restartButton;

    private int currentBackgroundIndex = -1;

    void Start()
    {
        ChangeBackground();
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame); 
        }
        else
        {
            Debug.LogError("Restart Button is not assigned!");
        }
    }

    public void ChangeBackground()
    {
        if (backgrounds.Length > 0)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, backgrounds.Length);
            } while (randomIndex == currentBackgroundIndex);

            currentBackgroundIndex = randomIndex;

            if (backgroundImage != null)
            {
                backgroundImage.sprite = backgrounds[randomIndex];
            }
            if (backgroundSpriteRenderer != null)
            {
                backgroundSpriteRenderer.sprite = backgrounds[randomIndex];
            }
        }
        else
        {
            Debug.LogWarning("No backgrounds assigned. Using default background.");
            if (defaultBackground != null)
            {
                if (backgroundImage != null)
                {
                    backgroundImage.sprite = defaultBackground;
                }
                if (backgroundSpriteRenderer != null)
                {
                    backgroundSpriteRenderer.sprite = defaultBackground;
                }
            }
        }
    }

    public void RestartGame()
    {
        ChangeBackground();
    
    }
}