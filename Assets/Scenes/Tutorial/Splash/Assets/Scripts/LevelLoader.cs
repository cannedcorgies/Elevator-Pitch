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
    public TMPro.TextMeshProUGUI elevatorFloorNum;


    public void LoadLevel (int sceneIndex) {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously (int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);

        while (!operation.isDone) {

            float progress = Mathf.Clamp01(operation.progress / .9f);
            // slider.value = progress;

            // updating elevator floor num based on the progress
            elevatorFloorNum.text = progress * 100f + "%";
            
            progressText.text = progress * 100f + "%";
            yield return null;
        }
        loadingScreen.SetActive(false);
    }
}
