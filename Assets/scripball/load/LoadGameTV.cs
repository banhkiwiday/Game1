using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameTV : MonoBehaviour
{
    public Button loadSceneButton;

    void Start()
    {
        loadSceneButton.onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("GameTV");
    }
}
