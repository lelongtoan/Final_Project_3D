using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public static LoadScene instance;
    [SerializeField] GameObject loadingScene;

    [SerializeField] Slider loadingSlider;
    [SerializeField] Text loadingText;
    AsyncOperation loadOp;
    public string nameScene;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (loadingScene == null)
        {
            Transform parent = gameObject.transform;
            Transform loadingTransform = parent.Find("Loading");
            if (loadingTransform != null)
            {
                loadingScene = loadingTransform.gameObject;
            }
            else
            {
                Debug.LogError("Loading object not found!");
            }
        }
        if (SceneManager.GetActiveScene().name == nameScene)
        {
            loadingScene.SetActive(false);
            nameScene = "";
            StopAllCoroutines();
        }
    }

    public void LoadSceneMenu(string scene)
    {
        nameScene = scene;
        Debug.Log("-----------------");
        loadingScene.SetActive(true);
        StartCoroutine(LoadSceneASync(scene));
    }
    IEnumerator LoadSceneASync(string scene)
    {
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
        loadingText.text = "Ấn Vào Để Tiếp Tục.";
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        loadOp.allowSceneActivation = true;
    }
}
