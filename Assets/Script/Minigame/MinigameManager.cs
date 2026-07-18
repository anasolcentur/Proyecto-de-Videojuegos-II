using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI fragmentsCounterText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    [Header("Scene Objects")]
    [SerializeField] private GameObject[] fragmentObjects;
    [SerializeField] private GameObject[] obstacleObjects;

    [Header("Current Level Settings")]
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int fragmentsToWin = 3;
    [SerializeField] private float levelTime = 30f;

    private int collectedFragments = 0;
    private float remainingTime;
    private bool minigameFinished = false;

    private void Awake()
    {
        Debug.Assert(titleText != null, "MinigameManager: title text is missing.");
        Debug.Assert(fragmentsCounterText != null, "MinigameManager: fragments counter text is missing.");
        Debug.Assert(timerText != null, "MinigameManager: timer text is missing.");
        Debug.Assert(winPanel != null, "MinigameManager: win panel is missing.");
        Debug.Assert(losePanel != null, "MinigameManager: lose panel is missing.");
    }

    private void Start()
    {
        currentLevel = GameProgress.SelectedLevel;

        ConfigureLevel();

        remainingTime = levelTime;

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }

        UpdateFragmentsCounter();
        UpdateTimerText();
    }

    private void Update()
    {
        if (minigameFinished)
        {
            return;
        }

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            UpdateTimerText();
            LoseMinigame();
            return;
        }

        UpdateTimerText();
    }

    private void ConfigureLevel()
    {
        if (currentLevel == 1)
        {
            fragmentsToWin = 3;
            levelTime = 30f;

            ActivateObjects(fragmentObjects, 3);
            ActivateObjects(obstacleObjects, 5);

            if (titleText != null)
            {
                titleText.text = "RECUERDO INICIAL";
            }
        }
        else if (currentLevel == 2)
        {
            fragmentsToWin = 5;
            levelTime = 25f;

            ActivateObjects(fragmentObjects, 5);
            ActivateObjects(obstacleObjects, 9);

            if (titleText != null)
            {
                titleText.text = "RECUERDO FRAGMENTADO";
            }
        }
        else
        {
            fragmentsToWin = 7;
            levelTime = 20f;

            ActivateObjects(fragmentObjects, 7);
            ActivateObjects(obstacleObjects, 12);

            if (titleText != null)
            {
                titleText.text = "RECUERDO PROFUNDO";
            }
        }

        Debug.Log("Configured minigame level: " + currentLevel);
    }

    private void ActivateObjects(GameObject[] objectsToControl, int amountToActivate)
    {
        if (objectsToControl == null)
        {
            return;
        }

        for (int i = 0; i < objectsToControl.Length; i++)
        {
            if (objectsToControl[i] != null)
            {
                objectsToControl[i].SetActive(i < amountToActivate);
            }
        }
    }

    public void CollectFragment(int points)
    {
        if (minigameFinished)
        {
            return;
        }

        collectedFragments += points;

        UpdateFragmentsCounter();

        Debug.Log("Collected fragments: " + collectedFragments + " / " + fragmentsToWin);

        if (collectedFragments >= fragmentsToWin)
        {
            Debug.Log("Win condition reached.");
            WinMinigame();
        }
    }

    public void ApplyTimePenalty(float penaltySeconds)
    {
        if (minigameFinished)
        {
            return;
        }

        remainingTime -= penaltySeconds;

        if (remainingTime < 0f)
        {
            remainingTime = 0f;
        }

        UpdateTimerText();

        Debug.Log("Shadow touched. Time penalty applied: " + penaltySeconds + " seconds.");

        if (remainingTime <= 0f)
        {
            LoseMinigame();
        }
    }

    private void UpdateFragmentsCounter()
    {
        if (fragmentsCounterText != null)
        {
            fragmentsCounterText.text = "Fragmentos: " + collectedFragments + " / " + fragmentsToWin;
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            int seconds = Mathf.CeilToInt(remainingTime);
            timerText.text = "Tiempo: " + seconds;
        }
    }

    private void WinMinigame()
    {
        minigameFinished = true;

        GameProgress.CompleteSelectedLevel();

        Debug.Log("Minigame completed. Memory unlocked.");

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    private void LoseMinigame()
    {
        minigameFinished = true;

        Debug.Log("Minigame failed. Time is over.");

        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
    }

    public void RetryMinigame()
    {
        SceneManager.LoadScene("Minigame");
    }

    public void ReturnToGameplay()
    {
        SceneManager.LoadScene("Gameplay");
    }
}