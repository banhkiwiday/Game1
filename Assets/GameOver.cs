using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public UnityEngine.UI.Text scoreText;
    public GameObject gameOverPanel;
    public Button playAgainButton;
    public Button loadSceneButton;
    public CanvasGroup flashPanel;



    private void Start()
    {
        GameManager.Instance.scoreText = scoreText;
        GameManager.Instance.gameOverPanel = gameOverPanel;

        load1.Instance.loadSceneButton = loadSceneButton;
        GameOverFlashEffect.Instance.flashPanel = flashPanel;
        GameOverUI.Instance.playAgainButton = playAgainButton;
    }
}
