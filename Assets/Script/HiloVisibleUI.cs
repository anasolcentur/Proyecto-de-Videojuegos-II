using UnityEngine;
using UnityEngine.UI;

public class HiloVisibleUI : MonoBehaviour
{
    [Header("Puntos principales")]
    [SerializeField] private RectTransform playerPoint;
    [SerializeField] private RectTransform completeMemoryPoint;

    [Header("Puntos de fragmentos")]
    [SerializeField] private RectTransform fragmentPoint1;
    [SerializeField] private RectTransform fragmentPoint2;
    [SerializeField] private RectTransform fragmentPoint3;

    [Header("Imágenes de fragmentos")]
    [SerializeField] private Image fragmentImage1;
    [SerializeField] private Image fragmentImage2;
    [SerializeField] private Image fragmentImage3;

    [Header("Imagen del recuerdo completo")]
    [SerializeField] private Image completeMemoryImage;

    [Header("Hilo visible")]
    [SerializeField] private RectTransform threadLine;
    [SerializeField] private Image threadImage;

    [Header("Configuración visual")]
    [SerializeField] private float threadThickness = 10f;
    [SerializeField] private float moveSpeed = 800f;
    [SerializeField] private float collectDistance = 12f;

    [Header("Colores")]
    [SerializeField] private Color visibleFragmentColor = new Color(1f, 0.77f, 0f, 1f);
    [SerializeField] private Color hiddenFragmentColor = new Color(1f, 1f, 1f, 0f);
    [SerializeField] private Color incompleteMemoryColor = new Color(1f, 1f, 1f, 0.25f);
    [SerializeField] private Color completedMemoryColor = new Color(1f, 0.75f, 0f, 1f);
    [SerializeField] private Color outOfRangeThreadColor = Color.red;
    [SerializeField] private Color completedThreadColor = new Color(1f, 0.75f, 0f, 1f);

    private Vector2 initialPlayerPosition;
    private Vector2 targetPlayerPosition;

    private int currentFragments = 0;

    private bool fragment1Collected = false;
    private bool fragment2Collected = false;
    private bool fragment3Collected = false;

    private void Awake()
    {
        Debug.Assert(playerPoint != null, "HiloVisibleUI: falta asignar Player Point.");
        Debug.Assert(completeMemoryPoint != null, "HiloVisibleUI: falta asignar Complete Memory Point.");
        Debug.Assert(fragmentPoint1 != null, "HiloVisibleUI: falta asignar Fragment Point 1.");
        Debug.Assert(fragmentPoint2 != null, "HiloVisibleUI: falta asignar Fragment Point 2.");
        Debug.Assert(fragmentPoint3 != null, "HiloVisibleUI: falta asignar Fragment Point 3.");
        Debug.Assert(fragmentImage1 != null, "HiloVisibleUI: falta asignar Fragment Image 1.");
        Debug.Assert(fragmentImage2 != null, "HiloVisibleUI: falta asignar Fragment Image 2.");
        Debug.Assert(fragmentImage3 != null, "HiloVisibleUI: falta asignar Fragment Image 3.");
        Debug.Assert(completeMemoryImage != null, "HiloVisibleUI: falta asignar Complete Memory Image.");
        Debug.Assert(threadLine != null, "HiloVisibleUI: falta asignar Thread Line.");
        Debug.Assert(threadImage != null, "HiloVisibleUI: falta asignar Thread Image.");

        if (playerPoint != null)
        {
            initialPlayerPosition = playerPoint.anchoredPosition;
            targetPlayerPosition = initialPlayerPosition;
        }

        ResetVisualState();
    }

    private void OnEnable()
    {
        HiloGameplayEvents.OnHiloProgressChanged += HandleProgressChanged;
    }

    private void OnDisable()
    {
        HiloGameplayEvents.OnHiloProgressChanged -= HandleProgressChanged;
    }

    private void Update()
    {
        if (playerPoint == null || completeMemoryPoint == null || threadLine == null || threadImage == null)
        {
            return;
        }

        MovePlayerVisual();
        CheckFragmentCollection();
        UpdateThreadLine();
        UpdateThreadColor();
    }

    private void HandleProgressChanged(int connection, int fragments, string message)
    {
        currentFragments = Mathf.Clamp(fragments, 0, 3);

        if (currentFragments == 0)
        {
            targetPlayerPosition = initialPlayerPosition;
        }
        else if (currentFragments == 1 && fragmentPoint1 != null)
        {
            targetPlayerPosition = fragmentPoint1.anchoredPosition;
        }
        else if (currentFragments == 2 && fragmentPoint2 != null)
        {
            targetPlayerPosition = fragmentPoint2.anchoredPosition;
        }
        else if (currentFragments >= 3 && fragmentPoint3 != null)
        {
            targetPlayerPosition = fragmentPoint3.anchoredPosition;
        }
    }

    private void MovePlayerVisual()
    {
        playerPoint.anchoredPosition = Vector2.MoveTowards(
            playerPoint.anchoredPosition,
            targetPlayerPosition,
            moveSpeed * Time.deltaTime
        );
    }

    private void CheckFragmentCollection()
    {
        if (!fragment1Collected && currentFragments >= 1 && IsPlayerCloseTo(fragmentPoint1))
        {
            fragment1Collected = true;
            HideFragment(fragmentImage1);
        }

        if (!fragment2Collected && currentFragments >= 2 && IsPlayerCloseTo(fragmentPoint2))
        {
            fragment2Collected = true;
            HideFragment(fragmentImage2);
        }

        if (!fragment3Collected && currentFragments >= 3 && IsPlayerCloseTo(fragmentPoint3))
        {
            fragment3Collected = true;
            HideFragment(fragmentImage3);
            CompleteMemoryUnlockVisual();
        }
    }

    private bool IsPlayerCloseTo(RectTransform point)
    {
        if (playerPoint == null || point == null)
        {
            return false;
        }

        float distance = Vector2.Distance(playerPoint.anchoredPosition, point.anchoredPosition);
        return distance <= collectDistance;
    }

    private void HideFragment(Image fragmentImage)
    {
        if (fragmentImage == null)
        {
            return;
        }

        fragmentImage.color = hiddenFragmentColor;
    }

    private void CompleteMemoryUnlockVisual()
    {
        if (completeMemoryImage != null)
        {
            completeMemoryImage.color = completedMemoryColor;
        }
    }

    private void UpdateThreadLine()
    {
        Vector2 startPosition = playerPoint.anchoredPosition;
        Vector2 endPosition = completeMemoryPoint.anchoredPosition;

        Vector2 direction = endPosition - startPosition;
        float distance = direction.magnitude;

        threadLine.anchoredPosition = startPosition + direction * 0.5f;
        threadLine.sizeDelta = new Vector2(distance, threadThickness);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        threadLine.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void UpdateThreadColor()
    {
        if (threadImage == null)
        {
            return;
        }

        threadImage.color = fragment3Collected ? completedThreadColor : outOfRangeThreadColor;
    }

    private void ResetVisualState()
    {
        fragment1Collected = false;
        fragment2Collected = false;
        fragment3Collected = false;

        if (fragmentImage1 != null)
        {
            fragmentImage1.color = visibleFragmentColor;
        }

        if (fragmentImage2 != null)
        {
            fragmentImage2.color = visibleFragmentColor;
        }

        if (fragmentImage3 != null)
        {
            fragmentImage3.color = visibleFragmentColor;
        }

        if (completeMemoryImage != null)
        {
            completeMemoryImage.color = incompleteMemoryColor;
        }

        if (threadImage != null)
        {
            threadImage.color = outOfRangeThreadColor;
        }
    }
}