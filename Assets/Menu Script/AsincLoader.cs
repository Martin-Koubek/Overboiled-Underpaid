using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;

public class AsincLoader : MonoBehaviour
{
    [Header("Hlavní menu")]
    [SerializeField] private GameObject loadScene;
    [SerializeField] private GameObject mainMenu;

    [Header("postup naèítání")]
    [SerializeField] private Slider loadSlider;

    public void LoadLevelBtn(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadScene.SetActive(true);

        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        while (!loadOperation.isDone)
        {
            float progresValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadSlider.value = progresValue;
            yield return null;
        }
    }
}
