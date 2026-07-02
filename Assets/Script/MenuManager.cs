using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private const string SceneGameplay = "Gameplay";
    private const string SceneOptions = "Opciones";
    private const string SceneCredits = "Creditos";
    private const string SceneMenu = "Menu";

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneGameplay);
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene(SceneOptions);
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(SceneCredits);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneMenu);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}