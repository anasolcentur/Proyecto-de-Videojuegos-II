using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(GoToMenu), 2f);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}