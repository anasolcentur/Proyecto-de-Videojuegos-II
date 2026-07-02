using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HiloGameplayManager : MonoBehaviour
{
    [Header("Textos de la UI")]
    [SerializeField] private TextMeshProUGUI connectionText;
    [SerializeField] private TextMeshProUGUI fragmentsText;
    [SerializeField] private TextMeshProUGUI messageText;

    [Header("Botón principal")]
    [SerializeField] private Button pullThreadButton;
    [SerializeField] private TextMeshProUGUI pullThreadButtonText;

    private const int MaxProgress = 3;

    private int emotionalConnection = 0;
    private int memoryFragments = 0;

    private void Start()
    {
        if (pullThreadButton != null)
        {
            pullThreadButton.onClick.AddListener(PullThread);
        }

        UpdateUI();
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

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (connectionText != null)
        {
            connectionText.text = "Conexión emocional: " + emotionalConnection + " / " + MaxProgress;
        }

        if (fragmentsText != null)
        {
            fragmentsText.text = "Fragmentos de memoria: " + memoryFragments + " / " + MaxProgress;
        }

        if (messageText != null)
        {
            if (memoryFragments == 0)
            {
                messageText.text = "El hilo rojo espera tu decisión.";
            }
            else if (memoryFragments == 1)
            {
                messageText.text = "Un eco lejano responde al hilo.";
            }
            else if (memoryFragments == 2)
            {
                messageText.text = "Un recuerdo comienza a tomar forma.";
            }
            else
            {
                messageText.text = "El primer recuerdo se desbloqueó. El destino comienza a despertar.";
            }
        }

        if (memoryFragments >= MaxProgress)
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
        else
        {
            if (pullThreadButtonText != null)
            {
                pullThreadButtonText.text = "TIRAR DEL HILO";
            }
        }
    }
}