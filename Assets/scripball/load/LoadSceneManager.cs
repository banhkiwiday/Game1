using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    public Button loadSceneButton;

    void Start()
    {
        
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string savedScene = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(savedScene);
        }
        else
        {
         
            loadSceneButton.onClick.AddListener(LoadScene);
        }
    }

    void LoadScene()
    {

        PlayerPrefs.SetString("SavedScene", "Scene 1");
        PlayerPrefs.Save();

        
        SceneManager.LoadScene("Scene 1");
    }
}
