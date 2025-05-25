using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ChestDropSystem : MonoBehaviour
{

    #region FIELDS

    [SerializeField]
    private Sprite[] allItems;
    [SerializeField]
    private Image itemsDropWindow;


    #endregion

    #region MAIN_METHODS

    public void DropItem() => StartCoroutine(DropItemRoutine());

    private IEnumerator DropItemRoutine()
    {
        itemsDropWindow.sprite = allItems[Random.Range(0, allItems.Length)];
        itemsDropWindow.gameObject.SetActive(true);
        LeanTween.alpha(itemsDropWindow.GetComponent<RectTransform>(), 1f, 1f);
        yield return new WaitForSeconds(1f);
        LeanTween.alpha(itemsDropWindow.GetComponent<RectTransform>(), 0f, 1f);
        yield return new WaitForSeconds(1f);
        itemsDropWindow.gameObject.SetActive(false);
    }

    #endregion

}
