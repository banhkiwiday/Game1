using UnityEngine;
using UnityEngine.UI;

public class ContinueGameManager : MonoBehaviour
{
    public GameObject continueButtonPanel; 
    public Button continueButton;

    private bool gamePaused = false; 

    void Start()
    {
        
        if (continueButtonPanel != null) continueButtonPanel.SetActive(false);
        if (continueButton != null) continueButton.onClick.AddListener(ContinueGame);
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus) 
        {
            PauseGame(); 
        }
        else 
        {
            if (gamePaused)
            {
                DisplayContinuePanel(); 
            }
        }
    }

    void PauseGame()
    {
        
        Time.timeScale = 0f;
        AudioListener.pause = true; 
        gamePaused = true;
    }

    void DisplayContinuePanel()
    {
        
        if (continueButtonPanel != null) continueButtonPanel.SetActive(true);
    }

    public void ContinueGame()
    {
    
        Time.timeScale = 1f; 
        AudioListener.pause = false; 
        gamePaused = false;

       
        if (continueButtonPanel != null) continueButtonPanel.SetActive(false);

        Debug.Log("GAME TIEP TUC");
    }
}
