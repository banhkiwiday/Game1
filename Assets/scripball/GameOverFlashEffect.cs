using System.Collections;
using UnityEngine;

public class GameOverFlashEffect : MonoBehaviour
{
    public CanvasGroup flashPanel;
    public float flashDuration = 1.2f;
    public float delayBeforeGameOver = 1f;
    private bool isFlashing = false;
    public static GameOverFlashEffect Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (flashPanel != null)
        {
            flashPanel.alpha = 0f;
            flashPanel.gameObject.SetActive(false);
        }
    }

    public void TriggerGameOverFlash()
    {
        if (!isFlashing)
        {
            StartCoroutine(FlashEffectCoroutine());
        }
        else
        {
            Debug.Log("Effect already running, extending...");
        }
    }

    private IEnumerator FlashEffectCoroutine()
    {
        isFlashing = true;
        flashPanel.gameObject.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            flashPanel.alpha = Mathf.Lerp(0f, 1f, elapsedTime / flashDuration);
            yield return null;
        }

        yield return new WaitForSeconds(delayBeforeGameOver);

        elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            flashPanel.alpha = Mathf.Lerp(1f, 0f, elapsedTime / flashDuration);
            yield return null;
        }

        flashPanel.gameObject.SetActive(false);
        isFlashing = false;
        Debug.Log("Flash effect complete!");
    }
}
