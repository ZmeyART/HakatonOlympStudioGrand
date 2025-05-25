using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour
{

    #region INSTANCE

    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }

    #endregion

    #region FIELDS

    [SerializeField]
    private Slider loadingBar;

    #endregion

    #region MAIN_METHODS

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        StartCoroutine(LoadAsync(sceneName));
    }

    IEnumerator LoadAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        loadingBar.value = 0f;
        while (!asyncLoad.isDone)
        {
            if (loadingBar.value < 3.5f)
            {
                loadingBar.value += 0.5f;
                yield return new WaitForSecondsRealtime(0.5f);
            }
            if (asyncLoad.progress >= 0.9f && loadingBar.value >= 3.5f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
        gameObject.SetActive(false);
    }

    #endregion

}
