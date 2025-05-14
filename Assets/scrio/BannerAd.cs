using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdManager : MonoBehaviour
{
    string gameId = "5816180"; 
    bool testMode = false;    

    void Start()
    {
#if UNITY_IOS
            gameId = "5816181"; // iOS ID
#endif

       
        Advertisement.Initialize(gameId, testMode);

    
        LoadAndShowBanner();
    }

    void LoadAndShowBanner()
    {
      
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

     
        Advertisement.Banner.Load("banner", new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        });
    }

    void OnBannerLoaded()
    {
        Debug.Log("Banner u");
        Advertisement.Banner.Show("banner");
    }

    void OnBannerError(string message)
    {
        Debug.LogError($"ggg: {message}");
    }
}
