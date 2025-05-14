using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerPersistent : MonoBehaviour
{
    private const string LastSceneKey = "LastLoadedScene";
    public string defaultStartScene = "Scene1"; 

    private void Awake()
    {
      
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            LoadLastScene();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private static SceneManagerPersistent _instance;
    public static SceneManagerPersistent Instance { get { return _instance; } }

    public void LoadScene(string sceneName)
    {
        
        PlayerPrefs.SetString(LastSceneKey, SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName);
    }

    private void LoadLastScene()
    {
        if (PlayerPrefs.HasKey(LastSceneKey))
        {
            string lastSceneName = PlayerPrefs.GetString(LastSceneKey);
            if (lastSceneName != "SceneVip")
            {
                SceneManager.LoadScene(lastSceneName);
            }
            else
            {
                SceneManager.LoadScene(defaultStartScene);
            }
        }
        else
        {
           
            SceneManager.LoadScene(defaultStartScene);
        }
    }
}