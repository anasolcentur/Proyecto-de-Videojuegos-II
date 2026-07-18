using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    private const string GameplaySceneName = "Gameplay";
    private const string OptionsSceneName = "Opciones";
    private const string CreditsSceneName = "Creditos";
    private const string MenuSceneName = "Menu";

    public void PlayGame()
    {
        GameProgress.ResetProgress();
        SceneManager.LoadScene(GameplaySceneName);
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene(OptionsSceneName);
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(CreditsSceneName);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(MenuSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego.");

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}