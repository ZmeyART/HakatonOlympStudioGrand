using UnityEngine;


[CreateAssetMenu(fileName = "chestWord", menuName = "ChestMinigame/Create")]
public class ChestTaskScriptableObject : ScriptableObject
{
    public string secret;
    public string letter1, answer1, letter2, answer2, letter3, answer3;
    public string correctLetter;
}
