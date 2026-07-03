using UnityEngine;

public class HiloGameplayDebugListener : MonoBehaviour
{
    private void OnEnable()
    {
        HiloGameplayEvents.OnHiloProgressChanged += LogProgress;
    }

    private void OnDisable()
    {
        HiloGameplayEvents.OnHiloProgressChanged -= LogProgress;
    }

    private void LogProgress(int connection, int fragments, string message)
    {
        Debug.Log(
            "Evento OnHiloProgressChanged recibido. " +
            "Conexión: " + connection +
            " | Fragmentos: " + fragments +
            " | Mensaje: " + message
        );
    }
}