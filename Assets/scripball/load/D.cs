using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class D : MonoBehaviour


{
    public Button loadSceneButton;

    void Start()
    {
        loadSceneButton.onClick.AddListener(LoadScene);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("HuongDan");
    }
}

