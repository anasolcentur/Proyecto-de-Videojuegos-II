using UnityEngine;

public class HiloVisualDebugger : MonoBehaviour
{
    [Header("Puntos del hilo")]
    [SerializeField] private Transform threadOrigin;
    [SerializeField] private Transform memoryFragment;

    [Header("Configuración visual")]
    [SerializeField] private float detectionRadius = 3f;
    [SerializeField] private bool drawDebugLineInPlayMode = true;
    [SerializeField] private bool drawGizmos = true;

    private readonly Color outOfRangeColor = Color.red;
    private readonly Color inRangeColor = new Color(1f, 0.75f, 0f);

    private void Awake()
    {
        Debug.Assert(threadOrigin != null, "HiloVisualDebugger: falta asignar Thread Origin.");
        Debug.Assert(memoryFragment != null, "HiloVisualDebugger: falta asignar Memory Fragment.");
    }

    private void Update()
    {
        if (!drawDebugLineInPlayMode)
        {
            return;
        }

        if (threadOrigin == null || memoryFragment == null)
        {
            return;
        }

        Color lineColor = IsFragmentInRange() ? inRangeColor : outOfRangeColor;

        Debug.DrawLine(
            threadOrigin.position,
            memoryFragment.position,
            lineColor
        );
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos || threadOrigin == null)
        {
            return;
        }

        Gizmos.color = new Color(1f, 0.85f, 0.25f);
        Gizmos.DrawWireSphere(threadOrigin.position, detectionRadius);

        if (memoryFragment == null)
        {
            return;
        }

        Gizmos.color = IsFragmentInRange() ? new Color(1f, 0.75f, 0f) : Color.red;
        Gizmos.DrawLine(threadOrigin.position, memoryFragment.position);
        Gizmos.DrawSphere(memoryFragment.position, 0.3f);
    }

    private bool IsFragmentInRange()
    {
        if (threadOrigin == null || memoryFragment == null)
        {
            return false;
        }

        float distance = Vector3.Distance(threadOrigin.position, memoryFragment.position);

        return distance <= detectionRadius;
    }
}