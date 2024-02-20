using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TMPro.TextMeshProUGUI progressText;


    public void LoadLevel (int sceneIndex) {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            // Debug.Log(progress);
            slider.value = progress;
            // progressText = GetComponent<TMPro.TextMeshProUGUI>().text
            progressText.text = progress * 100f + "%";
            // progressText.SetText(progress * 100f + "%");
            Debug.Log(progressText.text);
            yield return null;
        }
    }
}
