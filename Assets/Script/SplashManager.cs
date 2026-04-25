using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    void Start()
    {
        Invoke("GoToMenu", 2f);
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}