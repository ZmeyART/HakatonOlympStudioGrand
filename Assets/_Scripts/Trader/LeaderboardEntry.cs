using UnityEngine;


[System.Serializable]
public class LeaderboardEntry : MonoBehaviour
{

    #region FIELDS

    public string playersName;
    public int playersScore;


    public LeaderboardEntry(string playersName, int playersScore)
    {
        this.playersName = playersName;
        this.playersScore = playersScore;
    }

    #endregion

}
