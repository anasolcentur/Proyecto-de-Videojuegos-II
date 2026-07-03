using System;

public static class HiloGameplayEvents
{
    public static event Action<int, int, string> OnHiloProgressChanged;

    public static void RaiseHiloProgressChanged(int connection, int fragments, string message)
    {
        OnHiloProgressChanged?.Invoke(connection, fragments, message);
    }
}