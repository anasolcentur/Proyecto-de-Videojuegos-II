using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ResponsiveCamera : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float baseOrthographicSize = 5.5f;
    [SerializeField] private float minimumVisibleHalfWidth = 5.8f;

    private Camera sceneCamera;

    private void Awake()
    {
        sceneCamera = GetComponent<Camera>();

        Debug.Assert(sceneCamera != null, "ResponsiveCamera: Camera component is missing.");
        Debug.Assert(sceneCamera.orthographic, "ResponsiveCamera: Camera should be orthographic.");
    }

    private void Start()
    {
        AdjustCameraSize();
    }

    private void Update()
    {
        AdjustCameraSize();
    }

    private void AdjustCameraSize()
    {
        if (sceneCamera == null)
        {
            return;
        }

        float aspectRatio = sceneCamera.aspect;
        float sizeNeededForWidth = minimumVisibleHalfWidth / aspectRatio;

        sceneCamera.orthographicSize = Mathf.Max(baseOrthographicSize, sizeNeededForWidth);
    }
}