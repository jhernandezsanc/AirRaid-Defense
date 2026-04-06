using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    // Reference to the UI elements for the loading screen
    public TMP_Text loadingText;
    void Start() {
        // Start loading the game scene asynchronously and update the loading screen UI accordingly
        StartCoroutine(LoadGameAsync());
    }
    // Coroutine to load the game scene asynchronously and update the loading screen UI with progress information
    IEnumerator LoadGameAsync() {
        // Start loading the game scene asynchronously and prevent it from activating until loading is complete
        AsyncOperation operation = SceneManager.LoadSceneAsync("Game_Scene");
        operation.allowSceneActivation = false;
        // While the scene is still loading, update the loading bar and text with the current progress
        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (loadingBar != null) {
                loadingBar.value = progress;
            }

            if (loadingText != null) {
                loadingText.text = "Loading... " + Mathf.RoundToInt(progress * 100f) + "%";
            }

            if (operation.progress >= 0.9f) {
                if (loadingText != null) {
                    loadingText.text = "Loading... 100%";
                }

                yield return new WaitForSeconds(0.5f);
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}