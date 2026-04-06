using UnityEngine;
using System.Collections;
// handles the menu music, ensuring it persists across scenes and can be faded out when transitioning to the game scene
public class MenuMusicPlayer : MonoBehaviour
{
    private static MenuMusicPlayer instance;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void FadeOut(float duration)
    {
        StartCoroutine(FadeOutCoroutine(duration));
    }

    IEnumerator FadeOutCoroutine(float duration)
    {
        float startVolume = audioSource.volume;

        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime; 
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        audioSource.volume = 0f;
    }
}