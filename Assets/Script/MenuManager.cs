using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}