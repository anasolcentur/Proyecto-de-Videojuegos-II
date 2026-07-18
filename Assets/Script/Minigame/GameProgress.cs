using UnityEngine;

public static class GameProgress
{
    public const int MaxLevels = 3;

    public static int SelectedLevel { get; private set; } = 1;
    public static int CompletedLevels { get; private set; } = 0;

    public static int GetNextLevel()
    {
        return Mathf.Clamp(CompletedLevels + 1, 1, MaxLevels);
    }

    public static bool HasCompletedAllLevels()
    {
        return CompletedLevels >= MaxLevels;
    }

    public static void SelectNextLevel()
    {
        if (HasCompletedAllLevels())
        {
            Debug.Log("All levels are already completed.");
            return;
        }

        SelectedLevel = GetNextLevel();

        Debug.Log("Selected level: " + SelectedLevel);
    }

    public static void CompleteSelectedLevel()
    {
        if (SelectedLevel > CompletedLevels)
        {
            CompletedLevels = Mathf.Clamp(SelectedLevel, 0, MaxLevels);
        }

        Debug.Log("Completed levels: " + CompletedLevels + " / " + MaxLevels);
    }

    public static void ResetProgress()
    {
        SelectedLevel = 1;
        CompletedLevels = 0;

        Debug.Log("Game progress reset.");
    }
}