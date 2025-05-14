using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance;
    public Button playAgainButton;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
    }

    public void OnPlayAgainButtonClicked()
    {
        GameManager.Instance.ResetGame();
    }
}