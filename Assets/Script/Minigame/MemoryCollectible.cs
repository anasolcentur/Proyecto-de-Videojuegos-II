using UnityEngine;

public class MemoryCollectible : MonoBehaviour
{
    [Header("Collectible Settings")]
    [SerializeField] private int points = 1;

    private bool wasCollected = false;
    private MinigameManager minigameManager;

    private void Awake()
    {
        minigameManager = FindFirstObjectByType<MinigameManager>();

        Debug.Assert(minigameManager != null, "MemoryCollectible: MinigameManager was not found in the scene.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (wasCollected)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        wasCollected = true;

        if (minigameManager != null)
        {
            minigameManager.CollectFragment(points);
        }

        Destroy(gameObject);
    }
}