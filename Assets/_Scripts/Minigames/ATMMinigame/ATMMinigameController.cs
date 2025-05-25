using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;


public class ATMMinigameController : MonoBehaviour
{

    #region FIELDS

    [SerializeField]
    private ATMSlot[] allSlots;
    [SerializeField]
    private TMP_Text title, timerText;
    [SerializeField]
    private int valuesInNeed = 3, slotsAmount = 9, maxAttempts = 3;
    [SerializeField]
    private Image[] currentBlocks;
    [SerializeField]
    private PlayerMovement player;
    [SerializeField]
    private GameObject interactionButton, minigameUI;
    [SerializeField]
    private CurrencyOnSceneController currencyOnSceneController;
    [SerializeField]
    private float timeToComplete;
    [SerializeField]
    private RankSystem rankSystem;


    private string valueInNeed;
    private int currentStage = 0, correctAnswers = 0;
    private float eventTimer = 0;
    private readonly List<string> allPosibleValues = new() { "A7", "B1", "C6", "T4", "G9", "J8", "H2" };


    #endregion

    #region UNITY_METHODS

    private void Start()
    {
        player.InATMStepped += () =>
        {
            interactionButton.SetActive(true);
        };
        player.OutATMStepped += () =>
        {
            interactionButton.SetActive(false);
        };
        eventTimer = timeToComplete;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if(gameObject.activeInHierarchy)
            eventTimer -= Time.deltaTime;
        if(eventTimer <= 0)
            gameObject.SetActive(false);
        timerText.text = $"00:0{(int)eventTimer}";
    }

    private void OnEnable() => StartGame();

    private void OnDisable() => ResetGame();

    #endregion

    #region MAIN_METHODS

    public void GetAnswer(bool isEqual)
    {
        currentBlocks[currentStage].color = isEqual ? Color.green : Color.red;
        correctAnswers += isEqual ? 1 : 0;
        currentStage++;
        currencyOnSceneController.CurrencyAmount += isEqual ? correctAnswers * 10 : 0;
        LeaderboardManager.Instance.AddPlayerScore();
        rankSystem.GetCurrentRank();
        if (currentStage >= maxAttempts)
            gameObject.SetActive(false);       
    }

    public void StartGame()
    {
        ResetGame();
        interactionButton.SetActive(false);
        if (valueInNeed != null)
            allPosibleValues.Add(valueInNeed);
        valueInNeed = allPosibleValues[Random.Range(0, allPosibleValues.Count)];
        title.text = $"Найдите блоки : {valueInNeed}";
        allPosibleValues.Remove(valueInNeed);
        int notSpawnedValues = valuesInNeed;
        while(notSpawnedValues > 0)
        {
            ATMSlot randomSlot = allSlots[Random.Range(0, allSlots.Length)];
            if (randomSlot.IsEmpty)
            {
                randomSlot.SetSlotValue(valueInNeed);
                notSpawnedValues--;
            }
        }
        RandomFillAllSlots();
    }

    private void RandomFillAllSlots()
    {
        foreach(ATMSlot slot in allSlots)
        {
            slot.SetNewValue(valueInNeed);
            if(slot.IsEmpty)
                slot.SetSlotValue(allPosibleValues[Random.Range(0, allPosibleValues.Count)]);
        }
    }

    public void ResetGame()
    {
        foreach (Image image in currentBlocks)
            image.color = Color.white;
        foreach (ATMSlot slot in allSlots)
            slot.ClearSlot();
        currentStage = 0;
        correctAnswers = 0;
        eventTimer = timeToComplete;
    }

    #endregion

}
