using UnityEngine;
using System.Collections.Generic;
using TMPro;


public class ChestSpawnSystem : MonoBehaviour
{

    #region FIELDS

    [SerializeField]
    private List<Transform> chestSpawnPositions;
    [SerializeField]
    private int chestLimit;
    [SerializeField]
    private GameObject chestPref;
    [SerializeField]
    private TMP_Text timerText;
    [SerializeField]
    private float timeLimit;


    private bool isTimeUp = false;

    #endregion

    #region UNITY_METHODS

    private void Start()
    {
        for (int i = 0; i < chestLimit; i++)
        {
            Transform randomPosition = chestSpawnPositions[Random.Range(0, chestSpawnPositions.Count)];
            chestSpawnPositions.Remove(randomPosition);
            Instantiate(chestPref, randomPosition.position, randomPosition.rotation);
        }
    }

    private void Update()
    {
        timeLimit -= Time.deltaTime;
        if (timeLimit <= 0 && !isTimeUp)
        {
            SceneLoader.Instance.LoadScene("MainLevel");
            isTimeUp = true;
        }
        timerText.text = timeLimit >= 10 ? $"00:{(int)timeLimit}" : $"00:0{(int)timeLimit}";
    }

    #endregion

}
