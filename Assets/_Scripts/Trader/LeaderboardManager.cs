using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class LeaderboardManager : MonoBehaviour
{

    #region INSTANCE

    public static LeaderboardManager Instance {  get; private set; }

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    #endregion

    #region FIELDS

    [SerializeField]
    private Transform leaderboardContainer;
    [SerializeField]
    private GameObject leaderboardEntryPrefab;


    private LeaderboardEntry playerEntry;
    private List<LeaderboardEntry> leaderboard = new();
    private string playerName = "Ты";
    private int playerScore = 0;


    #endregion

    #region UNITY_METHODS

    void Start()
    {
        leaderboard.Add(new ("Серега", 120));
        leaderboard.Add(new ("Игорь", 90));
        leaderboard.Add(new ("Иван", 150));
        leaderboard.Add(new("Варфоламей", 0));
        playerEntry = new(playerName, playerScore);
        leaderboard.Add(playerEntry);
        AddPlayerScore();
    }

    #endregion

    #region MAIN_METHODS

    public void AddPlayerScore()
    {
        playerScore = PlayerPrefs.GetInt("Currency", 0);
        playerEntry.playersScore = playerScore;
        UpdateLeaderboardUI();
    }

    void UpdateLeaderboardUI()
    {
        leaderboard = leaderboard.OrderByDescending(entry => entry.playersScore).ToList();
        foreach (Transform child in leaderboardContainer)
            Destroy(child.gameObject);
        for (int i = 0; i < leaderboard.Count; i++)
        {
            GameObject entryObj = Instantiate(leaderboardEntryPrefab, leaderboardContainer);
            entryObj.GetComponentInChildren<TMP_Text>().text = $"{i + 1}. {leaderboard[i].playersName} - {leaderboard[i].playersScore}";
        }
        print("UpdateLeaderboardUI worked");
    }

    #endregion

}
