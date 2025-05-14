using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadTutorial : MonoBehaviour
{

    public Button loadSceneButton;

  
    void Start()
    {
      
        loadSceneButton.onClick.AddListener(LoadTutorialScene);
    }

    void LoadTutorialScene()
    {
        
        SceneManager.LoadScene("Tutorial");
    }
}