using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Movement Limits")]
    [SerializeField] private float minX = -4.5f;
    [SerializeField] private float maxX = 4.5f;
    [SerializeField] private float minY = -3.5f;
    [SerializeField] private float maxY = 3.5f;

    private Rigidbody2D rb;
    private Vector2 movementInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Debug.Assert(rb != null, "PlayerMovement: Rigidbody2D is missing.");
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movementInput = new Vector2(horizontal, vertical).normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movementInput * moveSpeed;

        Vector2 clampedPosition = rb.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        rb.position = clampedPosition;
    }
}