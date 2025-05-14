using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bxh : MonoBehaviour
{
    public static bxh Instance;
    public GameObject panel;
    public user nguoichoihientai;
    public List<user> userList;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayfabManager.Instance.GetLeaderboard();
        PlayfabManager.Instance.GetCurrentUserRank();
    }
}
