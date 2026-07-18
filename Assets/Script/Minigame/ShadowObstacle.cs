using UnityEngine;

public class ShadowObstacle : MonoBehaviour
{
    [Header("Obstacle Settings")]
    [SerializeField] private float timePenalty = 5f;
    [SerializeField] private float cooldown = 1f;

    private MinigameManager minigameManager;
    private bool canApplyPenalty = true;

    private void Awake()
    {
        minigameManager = FindFirstObjectByType<MinigameManager>();

        Debug.Assert(minigameManager != null, "ShadowObstacle: MinigameManager was not found in the scene.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canApplyPenalty)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        canApplyPenalty = false;

        if (minigameManager != null)
        {
            minigameManager.ApplyTimePenalty(timePenalty);
        }

        Invoke(nameof(ResetPenalty), cooldown);
    }

    private void ResetPenalty()
    {
        canApplyPenalty = true;
    }
}