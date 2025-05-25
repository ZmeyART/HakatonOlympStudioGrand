using System.Collections.Generic;
using UnityEngine;


public class TraderController : MonoBehaviour
{

    #region FIEDLS

    [SerializeField]
    private List<GameObject> traderWindows;
    [SerializeField]
    private CurrencyOnSceneController currencyOnSceneController;


    private int currentTab = 0;


    #endregion

    #region MAIN_METHODS

    public void BuyAccess()
    {
        if (currencyOnSceneController.CurrencyAmount >= 500)
        {
            currencyOnSceneController.CurrencyAmount -= 500;
            SceneLoader.Instance.LoadScene("SecretRoom");
        }
    }

    public void SwitchTab(int tab)
    {
        for (int i = 0; i < traderWindows.Count; i++)
            traderWindows[i].SetActive(tab == i);
        currentTab = tab;
    }

    #endregion

}
