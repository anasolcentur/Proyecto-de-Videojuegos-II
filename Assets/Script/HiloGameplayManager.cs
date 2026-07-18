using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HiloGameplayManager : MonoBehaviour
{
    [Header("Boton principal")]
    [SerializeField] private Button pullThreadButton;
    [SerializeField] private TextMeshProUGUI pullThreadButtonText;

    private const int MaxProgress = 3;

    private int emotionalConnection = 0;
    private int memoryFragments = 0;

    private void Awake()
    {
        Debug.Assert(pullThreadButton != null, "HiloGameplayManager: falta asignar Pull Thread Button.");
        Debug.Assert(pullThreadButtonText != null, "HiloGameplayManager: falta asignar Pull Thread Button Text.");
    }

    private void Start()
    {
        emotionalConnection = GameProgress.CompletedLevels;
        memoryFragments = GameProgress.CompletedLevels;

        if (pullThreadButton != null)
        {
            pullThreadButton.onClick.AddListener(PullThread);
        }

        UpdateButtonState();
        RaiseProgressEvent();
    }

    private void OnDestroy()
    {
        if (pullThreadButton != null)
        {
            pullThreadButton.onClick.RemoveListener(PullThread);
        }
    }

    public void PullThread()
    {
        if (GameProgress.HasCompletedAllLevels())
        {
            return;
        }

        GameProgress.SelectNextLevel();

        SceneManager.LoadScene("Minigame");
    }

    private void RaiseProgressEvent()
    {
        string message = GetCurrentMessage();

        HiloGameplayEvents.RaiseHiloProgressChanged(
            emotionalConnection,
            memoryFragments,
            message
        );
    }

    private string GetCurrentMessage()
    {
        if (memoryFragments == 0)
        {
            return "El hilo rojo espera tu decision.";
        }

        if (memoryFragments == 1)
        {
            return "Primer recuerdo recuperado. El hilo responde.";
        }

        if (memoryFragments == 2)
        {
            return "Dos recuerdos recuperados. La memoria toma forma.";
        }

        return "El recuerdo completo se desbloqueo. El destino comienza a despertar.";
    }

    private void UpdateButtonState()
    {
        if (pullThreadButton == null || pullThreadButtonText == null)
        {
            return;
        }

        if (GameProgress.HasCompletedAllLevels())
        {
            pullThreadButton.interactable = false;
            pullThreadButtonText.text = "RECUERDO DESBLOQUEADO";
            return;
        }

        int nextLevel = GameProgress.GetNextLevel();

        pullThreadButton.interactable = true;
        pullThreadButtonText.text = "ENTRAR AL RECUERDO " + nextLevel;
    }
}