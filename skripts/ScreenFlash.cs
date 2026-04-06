using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Handles the screen flash effect when the player takes damage, creating a red flash that fades in and out to indicate damage taken
public class ScreenFlash : MonoBehaviour
{
    public static ScreenFlash Instance;

    private Image flashImage;

    public float flashDuration = 0.3f;
    public float maxAlpha = 0.4f;
    // Initialize the singleton instance and get the Image component for the flash effect  
    void Awake() {
        Instance = this;
        flashImage = GetComponent<Image>();
    }

    // Public method to trigger the screen flash effect
    public void Flash() {
        StartCoroutine(FlashCoroutine());
    }
    // Coroutine to handle the flash effect, fading in and then fading out the red overlay to create a flashing effect on the screen
    IEnumerator FlashCoroutine() {
        float time = 0f;

        // fade in
        while (time < flashDuration / 2) {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, maxAlpha, time / (flashDuration / 2));
            flashImage.color = new Color(1f, 0f, 0f, alpha); // alpha adjusted to create the fade-in effect
            yield return null;
        }

        time = 0f;

        // fade out
        while (time < flashDuration / 2) {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(maxAlpha, 0f, time / (flashDuration / 2));
            flashImage.color = new Color(1f, 0f, 0f, alpha); // alpha adjusted to create the fade-out effect
            yield return null;
        }

        flashImage.color = new Color(1f, 0f, 0f, 0f); // reset the color to fully transparent
    }
}