using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ATMSlot : MonoBehaviour
{

    #region FIELDS

    [SerializeField]
    private TMP_Text slotText;
    [SerializeField]
    private ATMMinigameController atmMinigameController;


    private bool isEmpty = true;
    private string valueInNeed;


    public bool IsEmpty
    {
        get => isEmpty; set => isEmpty = value;
    }

    #endregion

    #region MAIN_METHODS

    public void SetNewValue(string newValue) => valueInNeed = newValue;

    public void SetSlotValue(string slotValue)
    {       
        slotText.text = slotValue;
        isEmpty = false;       
    }

    public void ClearSlot()
    {
        slotText.text = "";
        GetComponent<Button>().interactable = true;
        isEmpty = true;
    }

    public void OnClick()
    {
        atmMinigameController.GetAnswer(slotText.text == valueInNeed);
        GetComponent<Button>().interactable = false;
    }

    #endregion

}
