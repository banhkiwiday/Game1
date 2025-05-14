using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Over : MonoBehaviour
{
    public Button playAgainButton;
    public ShowInterstitialAd interstitialAd;

    void Start()
    {
        playAgainButton.onClick.AddListener(OnPlayAgainButtonClicked);
    }

    public void OnPlayAgainButtonClicked()
    {
        if (interstitialAd != null)
        {
            interstitialAd.ShowAd(); 
        }
        GameManager.Instance.ResetGame();
    }
}