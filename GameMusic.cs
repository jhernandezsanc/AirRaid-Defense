using UnityEngine;
using System.Collections;

public class GameMusicFadeIn : MonoBehaviour
{
    public float fadeDuration = 1.5f;
    private AudioSource audioSource;
    // Initialize the audio source and start the fade-in process
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(FadeIn());
    }
    // Coroutine to handle the fade-in effect for the game music
    IEnumerator FadeIn()
    {
        // Store the target volume and start with volume at 0, then gradually increase it to the target volume over the specified fade duration
        float targetVolume = audioSource.volume;
        // Start with volume at 0 for fade-in effect
        audioSource.volume = 0f;
        audioSource.Play();

        float time = 0f;
        // Gradually increase the volume from 0 to the target volume over the fade duration
        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, targetVolume, time / fadeDuration);
            yield return null;
        }
        // Ensure the volume is set to the target volume at the end of the fade-in
        audioSource.volume = targetVolume;
    }

    // Public method to start fading out the game music
    public void FadeOut(float duration)
    {
        StartCoroutine(FadeOutCoroutine(duration));
    }

    // Coroutine to handle the fade-out effect for the game music
    IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = audioSource.volume;
        float time = 0f;

        // Gradually decrease the volume from the start volume to 0 over the specified duration
        while (time < duration)
        {
            time += Time.unscaledDeltaTime; // important for pause
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }
        // Ensure the volume is set to 0 at the end of the fade-out and stop the audio source
        audioSource.volume = 0f;
        audioSource.Stop();
    }
}