using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

// Handles loading different scenes in the game, including transitioning from the menu to the game scene with a fade-out effect for the menu music 
// added a load screen in between to ensure the game music has time to fade out before the game scene starts, preventing audio overlap and ensuring a smoother transition
public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        StartCoroutine(LoadGameWithFade());
    }

    IEnumerator LoadGameWithFade()
    {
        MenuMusicPlayer music = FindObjectOfType<MenuMusicPlayer>();

        if (music != null)
        {
            music.FadeOut(1f);
            yield return new WaitForSecondsRealtime(1f);
            Destroy(music.gameObject);
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene("Game_Loading");
    }
    public void LoadLegend()
    {
        SceneManager.LoadScene("Game_Instruct");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Game_Start");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

}