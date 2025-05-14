using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using System;

public class PlayfabManager : MonoBehaviour
{
    public string nameUser;
    public string leaderboardName = "HighScore";
    public static PlayfabManager Instance;
    public GameObject nhapTenObj;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Login();
    }

    private void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true,
            }
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLogin, OnError);
    }
    public void SubmitUserNameName(string userName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = userName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSubmitUserName, OnError);
    }
    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.ErrorMessage);
    }
    private void OnLogin(LoginResult result)
    {
        Debug.Log("OnLogin");
        if (result.InfoResultPayload != null)
        {
            nameUser = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        if (String.IsNullOrEmpty(nameUser))
        {
            nhapTenObj.SetActive(true);
        }
        else
        {
            nhapTenObj.SetActive(false);
        }

        GetHighScore();
    }
    private void OnSubmitUserName(UpdateUserTitleDisplayNameResult result)
    {
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = leaderboardName,
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboard, OnError);
    }

    private void OnGetLeaderboard(GetLeaderboardResult result)
    {
        List<user> userRankList = bxh.Instance.userList;
        for (int i = 0; i < result.Leaderboard.Count; i++)
        {
            var userRank = result.Leaderboard[i];
            userRankList[i].rank.text = (userRank.Position + 1).ToString();
            userRankList[i].name.text = userRank.DisplayName;
            userRankList[i].diem.text = userRank.StatValue.ToString();

        }
    }

    public void GetCurrentUserRank()
    {
        var request = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = leaderboardName,
            MaxResultsCount = 1
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(request, OnGetCurrentUserRank, OnError);
    }
    private void OnGetCurrentUserRank(GetLeaderboardAroundPlayerResult result)
    {
       user user = bxh.Instance.nguoichoihientai;
        if (result.Leaderboard != null && result.Leaderboard.Count > 0)
        {
            var userRank = result.Leaderboard[0];
            user.rank.text = (userRank.Position + 1).ToString();
            user.name.text = userRank.DisplayName;
            user.diem.text = userRank.StatValue.ToString();
        }
    }
    public void GetHighScore()
    {
        var request = new GetPlayerStatisticsRequest
        {
            StatisticNames = new List<string> { leaderboardName }
        };
        PlayFabClientAPI.GetPlayerStatistics(request, OnGetHighScore, OnError);
    }
    private void OnGetHighScore(GetPlayerStatisticsResult result)
    {
        foreach (var statistic in result.Statistics)
        {
            if (statistic.StatisticName == leaderboardName)
            {
                GameManager.Instance.highScore = statistic.Value;
            }
        }
    }

    public void SubmitHighScore(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = leaderboardName,
                    Value = score
                }
        }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSubmitHighScore, OnError);
    }

    private void OnSubmitHighScore(UpdatePlayerStatisticsResult result)
    {
    }
}
