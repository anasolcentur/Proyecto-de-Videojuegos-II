using TMPro;
using UnityEngine;
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
        if (pullThreadButton != null)
        {
            pullThreadButton.onClick.AddListener(PullThread);
        }

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
        if (memoryFragments >= MaxProgress)
        {
            return;
        }

        emotionalConnection++;
        memoryFragments++;

        RaiseProgressEvent();

        if (memoryFragments >= MaxProgress)
        {
            CompleteMemoryUnlock();
        }
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
            return "Un eco lejano responde al hilo.";
        }

        if (memoryFragments == 2)
        {
            return "Un recuerdo comienza a tomar forma.";
        }

        return "El primer recuerdo se desbloqueo. El destino comienza a despertar.";
    }

    private void CompleteMemoryUnlock()
    {
        if (pullThreadButton != null)
        {
            pullThreadButton.interactable = false;
        }

        if (pullThreadButtonText != null)
        {
            pullThreadButtonText.text = "RECUERDO DESBLOQUEADO";
        }
    }
}