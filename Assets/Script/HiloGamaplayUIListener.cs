using TMPro;
using UnityEngine;

public class HiloGameplayUIListener : MonoBehaviour
{
    [Header("Textos de la UI")]
    [SerializeField] private TextMeshProUGUI connectionText;
    [SerializeField] private TextMeshProUGUI fragmentsText;
    [SerializeField] private TextMeshProUGUI messageText;

    private const int MaxProgress = 3;

    private void Awake()
    {
        Debug.Assert(connectionText != null, "HiloGameplayUIListener: falta asignar Connection Text.");
        Debug.Assert(fragmentsText != null, "HiloGameplayUIListener: falta asignar Fragments Text.");
        Debug.Assert(messageText != null, "HiloGameplayUIListener: falta asignar Message Text.");
    }

    private void OnEnable()
    {
        HiloGameplayEvents.OnHiloProgressChanged += UpdateUI;
    }

    private void OnDisable()
    {
        HiloGameplayEvents.OnHiloProgressChanged -= UpdateUI;
    }

    private void UpdateUI(int connection, int fragments, string message)
    {
        if (connectionText != null)
        {
            connectionText.text = "Conexion emocional: " + connection + " / " + MaxProgress;
        }

        if (fragmentsText != null)
        {
            fragmentsText.text = "Fragmentos de memoria: " + fragments + " / " + MaxProgress;
        }

        if (messageText != null)
        {
            messageText.text = message;
        }
    }
}