using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class RankSystem : MonoBehaviour
{

    #region FIELDS

    [SerializeField]
    private TMP_Text currentRankText;

    private List<Rank> allRanks = new();

    #endregion

    #region UNITY_METHODS

    void Start()
    {
        allRanks = new List<Rank>
        {
            new Rank { rankName = "Ранг 5", rankMinScore = 100, rankMaxScore = 300 },
            new Rank { rankName = "Ранг 4", rankMinScore = 300, rankMaxScore = 500 },
            new Rank { rankName = "Ранг 3", rankMinScore = 500, rankMaxScore = 700 },
            new Rank { rankName = "Ранг 2", rankMinScore = 700, rankMaxScore = 900 },
            new Rank { rankName = "Ранг 1", rankMinScore = 1000, rankMaxScore = -1 }
        };
        GetCurrentRank();
        gameObject.SetActive(false);
    }

    #endregion

    #region MAIN_METHODS

    public void GetCurrentRank()
    {
        int score = PlayerPrefs.GetInt("Currency", 0);
        foreach (var rank in allRanks)
        {
            if (rank.rankMaxScore == -1)
            {
                if (score >= rank.rankMinScore)
                    currentRankText.text = rank.rankName;
            }                
            else if (score >= rank.rankMinScore && score < rank.rankMaxScore)
                currentRankText.text = rank.rankName;
        }
    }

    #endregion

}
