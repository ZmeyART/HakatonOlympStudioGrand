using TMPro;
using UnityEngine;


public class CurrencyOnSceneController : MonoBehaviour
{

    #region FIELDS

    [SerializeField]
    private TMP_Text currencyCounter;


    public int CurrencyAmount
    {
        get => PlayerPrefs.GetInt("Currency", 0);
        set
        {
            if (value >= 0)
                PlayerPrefs.SetInt("Currency", value);
            else
                PlayerPrefs.SetInt("Currency", 0);
            PlayerPrefs.Save();
            UpdateInfo();
        }
    }


    #endregion

    #region UNITY_METHODS

    private void Start() => UpdateInfo();

    #endregion

    #region MAIN_METHODS

    public void UpdateInfo() => currencyCounter.text = CurrencyAmount.ToString();

    #endregion

}
