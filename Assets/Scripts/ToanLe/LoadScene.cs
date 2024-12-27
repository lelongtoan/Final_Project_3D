using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public static LoadScene instance;
    [SerializeField] GameObject loadingScene;

    [SerializeField] Slider loadingSlider;
    [SerializeField] TextMeshProUGUI loadingText;
    AsyncOperation loadOp;
    public string nameScene;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {

        if (SceneManager.GetActiveScene().name == nameScene)
        {
            loadingScene.SetActive(false);
            nameScene = "";
        }
    }
    public void LoadSceneMenu(string scene)
    {
        loadingScene.SetActive(true);
        StartCoroutine(LoadSceneASync(scene));
    }
    IEnumerator LoadSceneASync(string scene)
    {
        nameScene = scene;
        loadOp = SceneManager.LoadSceneAsync(scene);
        loadOp.allowSceneActivation = false;
        while (loadOp.progress < 0.9f)
        {
            float progressValue = Mathf.Clamp01(loadOp.progress/0.9f);
            loadingSlider.value = progressValue;
            Debug.Log(progressValue);
            yield return null;
        }
        loadingSlider.value = 1f;
        loadingText.text = "Nhan Chuot De Tiep Tuc";
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        loadOp.allowSceneActivation = true;
    }
}
