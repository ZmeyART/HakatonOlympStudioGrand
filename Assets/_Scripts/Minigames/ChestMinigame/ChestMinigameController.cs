using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ChestMinigameController : MonoBehaviour
{

    #region FIELDS

    [SerializeField]
    private ChestTaskScriptableObject[] tasks;
    [SerializeField]
    private Button[] answerButtons;
    [SerializeField]
    private TMP_Text currentTaskText;
    [SerializeField]
    private ChestDropSystem dropSystem;
    

    private string currentAnswer;
    private ChestTaskScriptableObject currentTask;


    #endregion

    #region UNITY_METHODS

    private void OnEnable() => StartGame();


    #endregion

    #region MAIN_METHODS

    public void StartGame()
    {
        currentTaskText.color = Color.white;
        currentTask = tasks[Random.Range(0, tasks.Length)];
        currentTaskText.text = currentTask.secret;
        SetButtonsData();
    }

    private void SetButtonsData()
    {
        answerButtons[0].GetComponentInChildren<TMP_Text>().text = currentTask.letter1;
        answerButtons[1].GetComponentInChildren<TMP_Text>().text = currentTask.letter2;
        answerButtons[2].GetComponentInChildren<TMP_Text>().text = currentTask.letter3;
        answerButtons[0].onClick.AddListener(() =>
        {
            currentAnswer = currentTask.answer1;
            CheckAnswer(currentTask.letter1);
        });
        answerButtons[1].onClick.AddListener(() =>
        {
            currentAnswer = currentTask.answer2;
            CheckAnswer(currentTask.letter2);
        });
        answerButtons[2].onClick.AddListener(() =>
        {
            currentAnswer = currentTask.answer3;
            CheckAnswer(currentTask.letter3);
        });
    }

    private void CheckAnswer(string answer) => StartCoroutine(CheckAnswerRoutine(answer));

    private IEnumerator CheckAnswerRoutine(string answer)
    {
        currentTaskText.text = currentAnswer;
        print($"Current answer : {currentAnswer}");
        if (answer == currentTask.correctLetter)
        {
            currentTaskText.color = Color.green;
            dropSystem.DropItem();
        }
        else
            currentTaskText.color = Color.red;
        yield return new WaitForSeconds(1f);
        currentTaskText.color = Color.white;
        gameObject.SetActive(false);
    }

    #endregion

}
